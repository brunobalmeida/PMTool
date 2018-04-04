using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PMTool.Models;

namespace PMTool.Controllers
{
    public class ModelProjectsController : Controller
    {
        private readonly PmToolDbContext _context;

        public ModelProjectsController(PmToolDbContext context)
        {
            _context = context;
        }

        // GET: ModelProjects
        public async Task<IActionResult> Index()
        {
            return View(await _context.ModelProject.ToListAsync());
        }

        // GET: ModelProjects/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var modelProject = await _context.ModelProject
                .SingleOrDefaultAsync(m => m.ModelProjectId == id);
            if (modelProject == null)
            {
                return NotFound();
            }

            return View(modelProject);
        }

        // GET: ModelProjects/Create
        public IActionResult Create()
        {
            var projects = _context.ModelProject.ToList();

            ViewData["ProjectId"] = new SelectList(projects, "ProjectId", "ProjectId");

            return View();
        }

        // POST: ModelProjects/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int? id)
        {
            var modelProject = await _context.ModelProject.Include(a => a.ModelTaskList).Include(a => a.ModelTask).Include(a => a.ModelTaskInfo).SingleOrDefaultAsync(m => m.ModelProjectId == id);
            //new Project
            var newProject = new Project

            {
                ProjectName = modelProject.ModelName,
                ProjectDescription = modelProject.ModelDescription,
                ProjectOpen = 1

            };
            _context.Add(newProject);
            //New Task List
            foreach (var itemTaskList in modelProject.modelTaskList)
            {
                var projectId = newProject.ProjectId;
                var newTaskList = new TaskList
                {
                    TaskListName = modelProject.modelTaskList.ModelTaskListName,
                    ProjectId = modelProject.modelTaskList.ModelProjectId,
                    TaskListOpen = 1
                };
                _context.Add(newTaskList);
                //New Task
                foreach (var itemTask in modelProject.modelTaskList.modelTask)
                {
                    var taskListId = newTaskList.TaskListId;
                    var newTask = new Models.Task
                    {
                        TaskListId = modelProject.modelTaskList.modelTask.ModelTaskListId,
                        TaskName = modelProject.modelTaskList.modelTask.ModelTaskName,
                        TaskWeight = modelProject.modelTaskList.modelTask.ModelTaskWeight,
                        TaskDescription = modelProject.modelTaskList.modelTask.ModelTaskDescription,
                        ExpectedDate = modelProject.modelTaskList.modelTask.ModelExpectedDate,
                        TaskActiveFlag = modelProject.modelTaskList.modelTask.ModelTaskActiveFlag,
                        TaskDuration = modelProject.modelTaskList.modelTask.ModelTaskDuration
                    };
                    _context.Add(newTask);
                    foreach(var itemTaskInfo in modelProject.modelTaskList.modelTask.modelTaskInfo)
                    {
                        var taskInfoId = newTask.TaskId;
                        var newTaskInfo = new TaskInfo
                        {
                            TaskId = modelProject.modelTaskList.modelTask.modelTaskInfo.ModelTaskId,
                            TaskNote = modelProject.modelTaskList.modelTask.modelTaskInfo.ModelTaskNote
                        };
                        _context.Add(newTaskInfo);
                    }
                }
            }
            //_context.Add(newTaskList);

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
            //if (ModelState.IsValid)
            //{
            //    _context.Add(modelProject);
            //    await _context.SaveChangesAsync();
            //    return RedirectToAction(nameof(Index));
            //}
            //return View(modelProject);
        }

        // GET: ModelProjects/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var modelProject = await _context.ModelProject.SingleOrDefaultAsync(m => m.ModelProjectId == id);
            if (modelProject == null)
            {
                return NotFound();
            }
            return View(modelProject);
        }

        // POST: ModelProjects/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ModelProjectId,ModelName,ModelDescription")] ModelProject modelProject)
        {
            if (id != modelProject.ModelProjectId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(modelProject);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ModelProjectExists(modelProject.ModelProjectId))
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
            return View(modelProject);
        }

        // GET: ModelProjects/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var modelProject = await _context.ModelProject
                .SingleOrDefaultAsync(m => m.ModelProjectId == id);
            if (modelProject == null)
            {
                return NotFound();
            }

            return View(modelProject);
        }

        // POST: ModelProjects/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var modelProject = await _context.ModelProject.SingleOrDefaultAsync(m => m.ModelProjectId == id);
            _context.ModelProject.Remove(modelProject);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ModelProjectExists(int id)
        {
            return _context.ModelProject.Any(e => e.ModelProjectId == id);
        }
    }
}
