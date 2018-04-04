using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PMTool.Models;
using Microsoft.AspNetCore.Http;

namespace PMTool.Controllers
{
    [Authorize(Roles = "Admin, ProjectAdmin, Employee")]
    public class TaskInfoController : Controller
    {
        private readonly PmToolDbContext _context;

        public TaskInfoController(PmToolDbContext context)
        {
            _context = context;
        }

        // GET: TaskInfo
        public async Task<IActionResult> Index(Int32? id)
        {
            if (id != null)
            {
                HttpContext.Session.SetString("id", id.ToString());
            }else if (HttpContext.Session.GetString("id") != null)
            {
                id = Convert.ToInt32(HttpContext.Session.GetString("id"));
            }
            else
            {
                TempData["message"] = "You should choose a task to view the comments";
            }

            var pmToolDbContext = _context.TaskInfo.Where(a=>a.TaskId == id).Include(t => t.Task);
            return View(await pmToolDbContext.ToListAsync());
        }

        // GET: TaskInfo/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var taskInfo = await _context.TaskInfo
                .Include(t => t.Task)
                .SingleOrDefaultAsync(m => m.TaskInfoId == id);
            if (taskInfo == null)
            {
                return NotFound();
            }

            return View(taskInfo);
        }

        // GET: TaskInfo/Create
        public IActionResult Create()
        {
            ViewData["TaskId"] = new SelectList(_context.Task, "TaskId", "TaskDescription");
            return View();
        }

        // POST: TaskInfo/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TaskInfoId,TaskId,TaskNote")] TaskInfo taskInfo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(taskInfo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["TaskId"] = new SelectList(_context.Task, "TaskId", "TaskDescription", taskInfo.TaskId);
            return View(taskInfo);
        }

        // GET: TaskInfo/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var taskInfo = await _context.TaskInfo.SingleOrDefaultAsync(m => m.TaskInfoId == id);
            if (taskInfo == null)
            {
                return NotFound();
            }
            ViewData["TaskId"] = new SelectList(_context.Task, "TaskId", "TaskDescription", taskInfo.TaskId);
            return View(taskInfo);
        }

        // POST: TaskInfo/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TaskInfoId,TaskId,TaskNote")] TaskInfo taskInfo)
        {
            if (id != taskInfo.TaskInfoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(taskInfo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TaskInfoExists(taskInfo.TaskInfoId))
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
            ViewData["TaskId"] = new SelectList(_context.Task, "TaskId", "TaskDescription", taskInfo.TaskId);
            return View(taskInfo);
        }

        // GET: TaskInfo/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var taskInfo = await _context.TaskInfo
                .Include(t => t.Task)
                .SingleOrDefaultAsync(m => m.TaskInfoId == id);
            if (taskInfo == null)
            {
                return NotFound();
            }

            return View(taskInfo);
        }

        // POST: TaskInfo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var taskInfo = await _context.TaskInfo.SingleOrDefaultAsync(m => m.TaskInfoId == id);
            _context.TaskInfo.Remove(taskInfo);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TaskInfoExists(int id)
        {
            return _context.TaskInfo.Any(e => e.TaskInfoId == id);
        }
    }
}
