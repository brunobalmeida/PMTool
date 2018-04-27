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
            string employeeId = employee.EmployeeId.ToString();
            var clientId = _context.Client.SingleOrDefault(a => a.PersonId == person.PersonId);
            HttpContext.Session.SetString("personName", personName);
            HttpContext.Session.SetString("userId", personId);
            HttpContext.Session.SetString("employeeId", employeeId);


            if (person.GetPicture() != null)
            {
                var profilePicture = person.GetPicture();
                HttpContext.Session.SetString("profilePicture", profilePicture); 
            }

            

            //if (employeeId != null) 
            //{
            //    var pmToolDbContextEmployee = _context.Project.Include(p => p.Client)
            //    .Include(p => p.Employee).Where(a=>a.EmployeeId == employeeId.EmployeeId)
            //    .OrderByDescending(a => a.ProjectOpen);
            //    return View(await pmToolDbContextEmployee.ToListAsync());
            //}
            //if (clientId != null)
            //{
            //    var pmToolDbContextClient = _context.Project.Include(p => p.Client)
            //    .Include(p => p.Employee).Where(a => a.ClientId == clientId.ClientId)
            //    .OrderByDescending(a => a.ProjectOpen);
            //    return View(await pmToolDbContextClient.ToListAsync());
            //}

            var pmToolDbContextClient = _context.Project.OrderByDescending(a => a.ProjectOpen);
            return View(await pmToolDbContextClient.ToListAsync());



            TempData["message"] = "We could not find your data inside dataBase, please try to login again.";
            return RedirectToAction("logout", "account");

        }

        // GET: Projects/Create
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            var clients = _context.Client.Include(a => a.Person).ToList();
            var employee = _context.Employee.Include(a => a.Person).ToList();


            ViewData["ClientId"] = new SelectList(clients, "ClientId", "Person.FirstName");
            //ViewData["ClientId"] = new SelectList(_context.Client, "ClientId", "ClientId");
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
