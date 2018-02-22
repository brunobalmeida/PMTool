using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PMTool.Models;

namespace PMTool.Controllers
{
    public class PersonController : Controller
    {
        private readonly PmToolDbContext _context;

        public PersonController(PmToolDbContext context)
        {
            _context = context;
        }

        // GET: Person
        public async Task<IActionResult> Index()
        {
            var pmToolDbContext = _context.Person.Include(p => p.OwnersLicense).Include(p => p.Province);
            return View(await pmToolDbContext.ToListAsync());
        }

        // GET: Person/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var person = await _context.Person
                .Include(p => p.OwnersLicense)
                .Include(p => p.Province)
                .SingleOrDefaultAsync(m => m.PersonId == id);
            if (person == null)
            {
                return NotFound();
            }

            return View(person);
        }

        // GET: Person/Create
        public IActionResult Create(Int32? ownersLicenseId, string companyName)
        {
            if (ownersLicenseId != null)
            {
                HttpContext.Session.SetString(nameof(ownersLicenseId), ownersLicenseId.ToString());
                HttpContext.Session.SetString(nameof(companyName), companyName);
            }
            else if (HttpContext.Session.GetString("ownersLicenseId") != null)
            {
                ownersLicenseId = int.Parse(HttpContext.Session.GetString("ownersLicenseId"));
            }
            


            ViewData["OwnersLicenseId"] = new SelectList(_context.OwnersLicense, "OwnersLicenseId", "Active");
            ViewData["ProvinceId"] = new SelectList(_context.Province, "ProvinceId", "ProvinceCode");
            return View();
        }

        // POST: Person/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PersonId,FirstName,LastName,MiddleName,Address,Email,OwnersLicenseId,Address2,ProvinceId,PostalCode,PhoneNumber,PersonImage")] Person person)
        {
            if (ModelState.IsValid)
            {
                _context.Add(person);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["OwnersLicenseId"] = new SelectList(_context.OwnersLicense, "OwnersLicenseId", "Active", person.OwnersLicenseId);
            ViewData["ProvinceId"] = new SelectList(_context.Province, "ProvinceId", "ProvinceCode", person.ProvinceId);
            return View(person);
        }

        // GET: Person/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var person = await _context.Person.SingleOrDefaultAsync(m => m.PersonId == id);
            if (person == null)
            {
                return NotFound();
            }
            ViewData["OwnersLicenseId"] = new SelectList(_context.OwnersLicense, "OwnersLicenseId", "Active", person.OwnersLicenseId);
            ViewData["ProvinceId"] = new SelectList(_context.Province, "ProvinceId", "ProvinceCode", person.ProvinceId);
            return View(person);
        }

        // POST: Person/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PersonId,FirstName,LastName,MiddleName,Address,Email,OwnersLicenseId,Address2,ProvinceId,PostalCode,PhoneNumber,PersonImage")] Person person)
        {
            if (id != person.PersonId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(person);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PersonExists(person.PersonId))
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
            ViewData["OwnersLicenseId"] = new SelectList(_context.OwnersLicense, "OwnersLicenseId", "Active", person.OwnersLicenseId);
            ViewData["ProvinceId"] = new SelectList(_context.Province, "ProvinceId", "ProvinceCode", person.ProvinceId);
            return View(person);
        }

        // GET: Person/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var person = await _context.Person
                .Include(p => p.OwnersLicense)
                .Include(p => p.Province)
                .SingleOrDefaultAsync(m => m.PersonId == id);
            if (person == null)
            {
                return NotFound();
            }

            return View(person);
        }

        // POST: Person/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var person = await _context.Person.SingleOrDefaultAsync(m => m.PersonId == id);
            _context.Person.Remove(person);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PersonExists(int id)
        {
            return _context.Person.Any(e => e.PersonId == id);
        }
    }
}
