using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PMTool.Models;
using Microsoft.AspNetCore.Http;

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

            var taskListId = _context.TaskList.SingleOrDefault(a => a.ProjectId == projectId).TaskListId;
            var tasksInTasklist = _context.Task.Where(a => a.TaskListId == taskListId);

            double taskWeight = 1;
            double completedTaskWeight = 0;
            
            foreach (var item in tasksInTasklist)
            {
                taskWeight += item.TaskWeight;
                if (item.TaskActiveFlag == 0)
                {
                    completedTaskWeight += item.TaskWeight; 
                }
            }

            double taskPercentage = completedTaskWeight / taskWeight;

            ViewBag.TaskPercentage = taskPercentage;


            var pmToolDbContext = _context.TaskList.Include(t => t.Project).Include(t => t.Task).Where(x => x.ProjectId == projectId);
            return View(await pmToolDbContext.ToListAsync());
        }

        // GET: TaskList/Details/5
        public async Task<IActionResult> Details(int? id)
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

        // GET: TaskList/Create
        public IActionResult Create()
        {
            ViewData["ProjectId"] = new SelectList(_context.Project, "ProjectId", "ProjectName");
            return View();
        }

        // POST: TaskList/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TaskListId,TaskListName,ProjectId,TaskListOpen")] TaskList taskList)
        {
            if (ModelState.IsValid)
            {
                _context.Add(taskList);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProjectId"] = new SelectList(_context.Project, "ProjectId", "ProjectDescription", taskList.ProjectId);
            return View(taskList);
        }

        // GET: TaskList/Edit/5
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
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProjectId"] = new SelectList(_context.Project, "ProjectId", "ProjectDescription", taskList.ProjectId);
            return View(taskList);
        }

        // GET: TaskList/Delete/5
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
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var taskList = await _context.TaskList.SingleOrDefaultAsync(m => m.TaskListId == id);
            _context.TaskList.Remove(taskList);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TaskListExists(int id)
        {
            return _context.TaskList.Any(e => e.TaskListId == id);
        }
    }
}
