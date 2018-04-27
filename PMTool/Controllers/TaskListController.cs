using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PMTool.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;

namespace PMTool.Controllers
{
    
    public class TaskListController : Controller
    {
        private readonly PmToolDbContext _context;

        public TaskListController(PmToolDbContext context)
        {
            _context = context;
        }

        // GET: TaskList
        [Authorize]
        public async Task<IActionResult> Index(int? projectId, string projectName)
        {
            if (projectId != null)
            {
                HttpContext.Session.SetString("projectId", projectId.ToString());
                HttpContext.Session.SetString("projectName", projectName);
            }
            else if (HttpContext.Session.GetString("projectId")!=null)
            {
                projectId = int.Parse(HttpContext.Session.GetString("projectId"));
            }
            else
            {
                TempData["message"] = "Please choose a project to see its task list first";
                return RedirectToAction("index", "projects");
            }

            var pmToolDbContext = _context.TaskList.Include(t => t.Project).Include(t => t.Task).Where(x => x.ProjectId == projectId);
            return View(await pmToolDbContext.ToListAsync());
        }

        // GET: TaskList/Create
        [Authorize(Roles = "Admin, ProjectAdmin")]
        public IActionResult Create()
        {
            ViewData["ProjectId"] = new SelectList(_context.Project, "ProjectId", "ProjectName");
            return View();
        }

        // POST: TaskList/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin, ProjectAdmin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TaskListId,TaskListName,ProjectId,TaskListOpen")] TaskList taskList)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Add(taskList);
                    await _context.SaveChangesAsync();
                    TempData["message"] = "Record successfully created";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception e)
                {
                    TempData["message"] = $"Create error: {e.GetBaseException().Message}";
                }
            }
            ViewData["ProjectId"] = new SelectList(_context.Project, "ProjectId", "ProjectDescription", taskList.ProjectId);
            return View(taskList);
        }

        // GET: TaskList/Edit/5
        [Authorize(Roles = "Admin, ProjectAdmin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var taskList = await _context.TaskList.SingleOrDefaultAsync(m => m.TaskListId == id);
            if (taskList == null)
            {
                return NotFound();
            }
            ViewData["ProjectId"] = new SelectList(_context.Project, "ProjectId", "ProjectDescription", taskList.ProjectId);
            return View(taskList);
        }

        // POST: TaskList/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin, ProjectAdmin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TaskListId,TaskListName,ProjectId,TaskListOpen")] TaskList taskList)
        {
            if (id != taskList.TaskListId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(taskList);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TaskListExists(taskList.TaskListId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                catch (Exception e)
                {
                    TempData["message"] = $"Edit error: {e.GetBaseException().Message}";
                }


                return RedirectToAction(nameof(Index));
            }
            ViewData["ProjectId"] = new SelectList(_context.Project, "ProjectId", "ProjectDescription", taskList.ProjectId);
            return View(taskList);
        }

        // GET: TaskList/Delete/5
        [Authorize(Roles = "Admin, ProjectAdmin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var taskList = await _context.TaskList
                .Include(t => t.Project)
                .SingleOrDefaultAsync(m => m.TaskListId == id);
            if (taskList == null)
            {
                return NotFound();
            }

            return View(taskList);
        }

        // POST: TaskList/Delete/5
        [Authorize(Roles = "Admin, ProjectAdmin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var taskList = await _context.TaskList.SingleOrDefaultAsync(m => m.TaskListId == id);
                _context.TaskList.Remove(taskList);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception e)
            {
                TempData["message"] = $"Delete error: {e.GetBaseException().Message}";
            }
            return Redirect($"/tasklist/delete/{id}");
        }

        private bool TaskListExists(int id)
        {
            return _context.TaskList.Any(e => e.TaskListId == id);
        }
    }
}
