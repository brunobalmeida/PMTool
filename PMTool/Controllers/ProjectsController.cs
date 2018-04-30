using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PMTool.Models;

namespace PMTool.Controllers
{
    [Authorize]
    public class ProjectsController : Controller
    {
        private readonly PmToolDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public ProjectsController(PmToolDbContext context,
            UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Projects
        public async Task<IActionResult> Index()
        {
            
            var user = _userManager.GetUserName(User);
            
            var person = _context.Person.FirstOrDefault(a => a.Email == user);
            var employee = _context.Employee.FirstOrDefault(a => a.PersonId == person.PersonId);
            string personName = person.FirstName;
            string personId = person.PersonId.ToString();

            var clientId = _context.Client.SingleOrDefault(a => a.PersonId == person.PersonId);
            HttpContext.Session.SetString("personName", personName);
            HttpContext.Session.SetString("userId", personId);

            var isEmployee = _context.Employee.FirstOrDefault(a => a.PersonId == int.Parse(personId));
            var isClient = _context.Client.FirstOrDefault(a => a.PersonId == int.Parse(personId));

            if (person.GetPicture() != null)
            {
                var profilePicture = person.GetPicture();
                HttpContext.Session.SetString("profilePicture", profilePicture); 
            }
            try
            {
                if (employee != null)
                {
                    int employeeId = employee.EmployeeId;
                    HttpContext.Session.SetString("employeeId", employeeId.ToString());
                    var id = employeeId;
                    var list = _context.Task.Where(a => a.TaskActiveFlag == 1).Where(a => a.EmployeeId == id).OrderByDescending(a => a.ExpectedDate).ToList();
                    ViewData["list"] = list.ToList();
                }
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", $"Something went wrong. {e.GetBaseException().Message}");

            }

            if (isEmployee!=null)
            {
                var pmToolContext = _context.Project.OrderByDescending(a => a.ProjectOpen).Where(a=>a.EmployeeId == isEmployee.EmployeeId);
                return View(await pmToolContext.ToListAsync());
            }

            if (isClient != null)
            {
                var pmToolContext = _context.Project.OrderByDescending(a => a.ProjectOpen).Where(a => a.ClientId == isClient.ClientId);
                return View(await pmToolContext.ToListAsync());
            }


            var pmToolDbContextClient = _context.Project.OrderByDescending(a => a.ProjectOpen);
            return View(await pmToolDbContextClient.ToListAsync());

        }

        // GET: Projects/Create
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            var clients = _context.Client.Include(a => a.Person).ToList();
            var employee = _context.Employee.Include(a => a.Person).ToList();


            ViewData["ClientId"] = new SelectList(clients, "ClientId", "Person.FirstName");
            ViewData["EmployeeId"] = new SelectList(employee, "EmployeeId", "Person.FirstName");

            return View();
        }

        // POST: Projects/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProjectId,ClientId,EmployeeId,ProjectOpen,ProjectName,StartDate,EndDate,ProjectDescription")] Project project)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Add(project);
                    await _context.SaveChangesAsync();
                    TempData["message"] = "The Project has been successfully created.";
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", $"Create error:{e.GetBaseException().Message}");
            }
            Create();
            return View(project);
        }

        // GET: Projects/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var project = await _context.Project.SingleOrDefaultAsync(m => m.ProjectId == id);
            if (project == null)
            {
                return NotFound();
            }
            Create();
            return View(project);
        }

        // POST: Projects/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProjectId,ClientId,EmployeeId,ProjectOpen,ProjectName,StartDate,EndDate,ProjectDescription")] Project project)
        {
            if (id != project.ProjectId)
            {
                ModelState.AddModelError("", "Invalid ID, please try again.");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(project);
                    await _context.SaveChangesAsync();
                    TempData["message"] = "The record has been successfully updated";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProjectExists(project.ProjectId))
                    {
                        ModelState.AddModelError("", "The record does not exist, try again");
                    }
                    else
                    {
                        ModelState.AddModelError("", "This record has already been updated");
                    }
                }
                catch(Exception e)
                {
                    ModelState.AddModelError("", $"An error has happen on editing. {e.GetBaseException().Message}");
                }
                TempData["message"] = "The project has been updated.";
                return RedirectToAction(nameof(Index));
            }
            Create();
            return View(project);
        }

        // GET: Projects/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var project = await _context.Project
                .Include(p => p.Client)
                .Include(p => p.Employee)
                .SingleOrDefaultAsync(m => m.ProjectId == id);
            if (project == null)
            {
                return NotFound();
            }

            return View(project);
        }

        // POST: Projects/Delete/5
        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var project = await _context.Project.SingleOrDefaultAsync(m => m.ProjectId == id);
                _context.Project.Remove(project);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception e)
            {
                TempData["message"] = $"Delete error: {e.GetBaseException().Message}";
            }
            return Redirect($"/projects/delete/{id}");
        }

        private bool ProjectExists(int id)
        {
            return _context.Project.Any(e => e.ProjectId == id);
        }
    }
}
