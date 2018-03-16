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
    public class TaskController : Controller
    {
        private readonly PmToolDbContext _context;

        public TaskController(PmToolDbContext context)
        {
            _context = context;
        }

        // GET: Task
        public async Task<IActionResult> Index(int? id, String listName)
        {
            if (id != null)
            {
                HttpContext.Session.SetString("listName", listName);
            }else if(HttpContext.Session.GetString("listName") != null)
            {
                listName = (HttpContext.Session.GetString("listName"));
            }
         
            var pmToolDbContext = _context.Task.Where(t=>t.TaskListId == id).Include(t => t.Employee).Include(t => t.TaskList);
            return View(await pmToolDbContext.ToListAsync());
        }

        // GET: Task/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var task = await _context.Task
                .Include(t => t.Employee)
                .Include(t => t.TaskList)
                .SingleOrDefaultAsync(m => m.TaskId == id);
            if (task == null)
            {
                return NotFound();
            }

            return View(task);
        }

        // GET: Task/Create
        public IActionResult Create()
        {
            var listPerson = _context.Employee.Include(a=>a.Person);
            var items = new List<SelectListItem>();

            foreach (var name in listPerson)
            {
                items.Add(new SelectListItem()
                {
                    Text = name.Person.FirstName,
                    Value = name.EmployeeId.ToString(),
                    
                });
            }
            ViewData["ListPerson"] = new SelectList(items, "Value", "Text");
            //ViewData["EmployeeId"] = new SelectList(_context.Employee.Include(a=>a.Person).Select(a=>a.Person.FirstName), "EmployeeId", "EmployeeId");
            ViewData["TaskListId"] = new SelectList(_context.TaskList, "TaskListId", "TaskListId");
            return View();
        }

        // POST: Task/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TaskId,EmployeeId,TaskListId,TaskName,TaskWeight,TaskDescription,ExpectedDate,TaskDuration,TaskActiveFlag")] Models.Task task)
        {
            if (ModelState.IsValid)
            {
                _context.Add(task);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EmployeeId"] = new SelectList(_context.Employee, "EmployeeId", "EmployeeId", task.EmployeeId);
            ViewData["TaskListId"] = new SelectList(_context.TaskList, "TaskListId", "TaskListId", task.TaskListId);
            return View(task);
        }

        // GET: Task/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var task = await _context.Task.SingleOrDefaultAsync(m => m.TaskId == id);
            if (task == null)
            {
                return NotFound();
            }
            ViewData["EmployeeId"] = new SelectList(_context.Employee, "EmployeeId", "EmployeeId", task.EmployeeId);
            ViewData["TaskListId"] = new SelectList(_context.TaskList, "TaskListId", "TaskListId", task.TaskListId);
            return View(task);
        }

        // POST: Task/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TaskId,EmployeeId,TaskListId,TaskName,TaskWeight,TaskDescription,ExpectedDate,TaskDuration,TaskActiveFlag")] Models.Task task)
        {
            if (id != task.TaskId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(task);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TaskExists(task.TaskId))
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
            ViewData["EmployeeId"] = new SelectList(_context.Employee, "EmployeeId", "EmployeeId", task.EmployeeId);
            ViewData["TaskListId"] = new SelectList(_context.TaskList, "TaskListId", "TaskListId", task.TaskListId);
            return View(task);
        }

        // GET: Task/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var task = await _context.Task
                .Include(t => t.Employee)
                .Include(t => t.TaskList)
                .SingleOrDefaultAsync(m => m.TaskId == id);
            if (task == null)
            {
                return NotFound();
            }

            return View(task);
        }

        // POST: Task/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var task = await _context.Task.SingleOrDefaultAsync(m => m.TaskId == id);
            _context.Task.Remove(task);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TaskExists(int id)
        {
            return _context.Task.Any(e => e.TaskId == id);
        }
    }
}
