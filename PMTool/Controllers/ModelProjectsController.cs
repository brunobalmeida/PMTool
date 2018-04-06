using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PMTool.Models;
using Microsoft.AspNetCore.Identity;

namespace PMTool.Controllers
{
    public class ModelProjectsController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly PmToolDbContext _context;

        public ModelProjectsController(PmToolDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: ModelProjects
        public async Task<IActionResult> Index()
        {
            var modelProjects = _context.ModelProject;

            ViewData["ModelProjectId"] = new SelectList(modelProjects, "ModelProjectId", "ModelProjectName");
            return View();
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
            

            return View();
        }

        // POST: ModelProjects/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int? id)
        {
            var modelProject = await _context.ModelProject.Include(a => a.ModelTaskList)
                //.Include(a => a.ModelTask).Include(a => a.ModelTaskInfo);
                //.Select(b => b.ModelTask.Select(c => c.ModelTaskInfo)))
                .SingleOrDefaultAsync(m => m.ModelProjectId == id);

            //var modelTask = await _context.ModelTaskList.Include(a => a.ModelTask).SingleOrDefaultAsync(m => m.ModelTaskListId == id);


            //.Include(a => a.ModelTask).Include(a => a.ModelTaskInfo)
            //new Project
            var user = _userManager.GetUserName(User);
            var personId = _context.Person.FirstOrDefault(a => a.Email == user).PersonId;
            var newProject = new Project
            {
                
                ClientId = 1,
                EmployeeId = _context.Employee.FirstOrDefault(a => a.PersonId == personId).EmployeeId,
                ProjectName = modelProject.ModelProjectName,
                ProjectDescription = modelProject.ModelProjectDescription,
                ProjectOpen = 1,
                StartDate = DateTime.Now

            };
            _context.Add(newProject);
            var projectId = newProject.ProjectId;
            //New Task List
            foreach (var itemTaskList in modelProject.ModelTaskList)
            {
                var newTaskList = new TaskList
                {
                    TaskListName = itemTaskList.ModelTaskListName,
                    ProjectId = projectId,
                    TaskListOpen = 1
                };
                _context.Add(newTaskList);
                var modelTaskListId = itemTaskList.ModelTaskListId;
                var modelTasks = await _context.ModelTaskList.Include(b => b.ModelTask).SingleOrDefaultAsync(a => a.ModelTaskListId == modelTaskListId);
                var taskListId = newTaskList.TaskListId;
                //New Task
                foreach (var itemTask in modelTasks.ModelTask)
                {
                    var newTask = new Models.Task
                    {
                        TaskListId = taskListId,
                        TaskName = itemTask.ModelTaskName,
                        TaskWeight = itemTask.ModelTaskWeight,
                        TaskDescription = itemTask.ModelTaskDescription,
                        ExpectedDate = itemTask.ModelExpectedDate,
                        TaskActiveFlag = itemTask.ModelTaskActiveFlag,
                        TaskDuration = itemTask.ModelTaskDuration
                    };
                    _context.Add(newTask);
                    var modelTaskId = itemTask.ModelTaskId;
                    var modelTaskInfos = await _context.ModelTask.Include(b => b.ModelTaskInfo).SingleOrDefaultAsync(a => a.ModelTaskId == modelTaskId);
                    var taskId = newTask.TaskId;
                    foreach (var itemTaskInfo in modelTaskInfos.ModelTaskInfo)
                    {
                        var newTaskInfo = new TaskInfo
                        {
                            TaskId = taskId,
                            TaskNote = itemTaskInfo.ModelTaskNote
                        };
                        _context.Add(newTaskInfo);
                    }
                }
            }

                await _context.SaveChangesAsync();
            TempData["message"] = "The Project has been successfully loaded.";
            return RedirectToAction(nameof(Index));
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
