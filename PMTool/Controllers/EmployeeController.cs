using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PMTool.Models;
using Microsoft.AspNetCore.Identity;
using PMTool.Services;
using PmToolClassLibrary;

namespace PMTool.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly PmToolDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IEmailSender _emailSender;

        public EmployeeController(PmToolDbContext context, UserManager<ApplicationUser> userManager, IEmailSender emailSender)
        {
            _context = context;
            _userManager = userManager;
            _emailSender = emailSender;
        }

        // GET: Employee
        public async Task<IActionResult> Index()
        {
            var pmToolDbContext = _context.Employee.Include(e => e.Person);
            return View(await pmToolDbContext.ToListAsync());
        }

        // GET: Employee/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.Employee
                .Include(e => e.Person)
                .SingleOrDefaultAsync(m => m.EmployeeId == id);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        // GET: Employee/Create
        public IActionResult Create()
        {
            ViewData["PersonId"] = new SelectList(_context.Person, "PersonId", "Email");
            return View();
        }

        // POST: Employee/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EmployeeId,PersonId,EmployeeNumber")] Employee employee, string taEmail)
        {

            if (!Validations.EmailValidation(taEmail))
            {
                TempData["message"] = "Email can not be empty or is not in a valid format.";
                return RedirectToAction("Index");
            }
            try
            {
                if (ModelState.IsValid)
                {
                    
                    //var pmToolDbContext = _context.OwnersLicense.Include(a => a.Person);
                    var user = await _userManager.GetUserAsync(User);

                    //string emailToRegister = taEmail;
                    string flagEmpOrClient = "Employee";
                    var emailCheck = user.Email;
                    int licenseId = _context.Person.FirstOrDefault(a => a.Email == emailCheck).OwnersLicenseId;
                    var licenseEmail = taEmail;
                    var callbackUrl = Url.EmailRegistrationLink(licenseId, licenseEmail, Request.Scheme, flagEmpOrClient);
                    await _emailSender.SendEmailRegistrationAsync(licenseEmail, callbackUrl);
                    TempData["message"] = "The employee has been successfully added and the verification email sent.";
                    return RedirectToAction(nameof(Index));
                    //_context.Add(employee);
                    //await _context.SaveChangesAsync();
                    //TempData["message"] = "Record Successfully added.";
                    //return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", $"Create error:{e.GetBaseException().Message}");
            }
            Create();
            return View(employee);
        }

        // GET: Employee/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.Employee.SingleOrDefaultAsync(m => m.EmployeeId == id);
            if (employee == null)
            {
                return NotFound();
            }
            ViewData["PersonId"] = new SelectList(_context.Person, "PersonId", "Email", employee.PersonId);
            return View(employee);
        }

        // POST: Employee/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EmployeeId,PersonId,EmployeeNumber")] Employee employee)
        {
            if (id != employee.EmployeeId)
            {
                ModelState.AddModelError("", "Invalid ID, please try again.");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(employee);
                    await _context.SaveChangesAsync();
                    TempData["message"] = "The record has been successfully updated";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmployeeExists(employee.EmployeeId))
                    {
                        ModelState.AddModelError("", "The record does not exist, try again");
                    }
                    else
                    {
                        ModelState.AddModelError("", "This record has already been updated");
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            Create();
            return View(employee);
        }

        // GET: Employee/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.Employee
                .Include(e => e.Person)
                .SingleOrDefaultAsync(m => m.EmployeeId == id);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        // POST: Employee/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var employee = await _context.Employee.SingleOrDefaultAsync(m => m.EmployeeId == id);
                _context.Employee.Remove(employee);
                await _context.SaveChangesAsync();
                TempData["message"] = "The record has been successfully Deleted";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception e)
            {
                TempData["message"] = $"Delete error: {e.GetBaseException().Message}";
            }
            return Redirect($"/employee/delete/{id}");
        }

        private bool EmployeeExists(int id)
        {
            return _context.Employee.Any(e => e.EmployeeId == id);
        }
    }
}
