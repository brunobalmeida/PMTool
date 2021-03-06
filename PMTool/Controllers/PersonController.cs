﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PMTool.Models;

namespace PMTool.Controllers
{
    [Authorize]
    public class PersonController : Controller
    {
        private readonly PmToolDbContext _context;

        public PersonController(PmToolDbContext context)
        {
            _context = context;
        }

        // GET: Person/Details/5
        public async Task<IActionResult> Details(int? id, string firstName, string lastName)
        {
            if (id == null)
            {
                return NotFound();
            }

            if (firstName != null )
            {
                ViewBag.firstName = firstName;
            }

            if (firstName != null)
            {
                ViewBag.lastName = lastName;
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
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            try
            {
                int licenseId = int.Parse(Request.Query["licenseId"]);
                HttpContext.Session.SetString(nameof(licenseId), licenseId.ToString());
                string licenseEmail = Request.Query["licenseEmail"];
                HttpContext.Session.SetString(nameof(licenseEmail), licenseEmail);
            }
            catch (Exception)
            {
                ViewData["OwnersLicenseId"] = new SelectList(_context.OwnersLicense, "OwnersLicenseId", "Active");
                ViewData["ProvinceId"] = new SelectList(_context.Province, "ProvinceId", "ProvinceCode");
                return View();
            }
            

            ViewData["OwnersLicenseId"] = new SelectList(_context.OwnersLicense, "OwnersLicenseId", "Active");
            ViewData["ProvinceId"] = new SelectList(_context.Province, "ProvinceId", "ProvinceCode");
            return View();
        }

        // POST: Person/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PersonId,FirstName,LastName,MiddleName,Address,Email,OwnersLicenseId,Address2,ProvinceId,PostalCode,PhoneNumber,PersonImage")] Person person)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Add(person);
                    await _context.SaveChangesAsync();
                    TempData["message"] = "Record Successfully added.";
                    return RedirectToAction("index", "projects");
                }
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", $"Create error:{e.GetBaseException().Message}");
            }
            Create();
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
            ViewData["OwnersLicenseId"] = new SelectList(_context.OwnersLicense, "OwnersLicenseId", "OwnersLicenseId", person.OwnersLicenseId);
            ViewData["ProvinceId"] = new SelectList(_context.Province, "ProvinceId", "ProvinceName", person.ProvinceId);
            return View(person);
        }

        // POST: Person/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PersonId,FirstName,LastName,MiddleName,Address,Email,OwnersLicenseId,Address2,ProvinceId,PostalCode,PhoneNumber,PersonImage")] Person person, IFormFile PersonImage)
        {
            if (id != person.PersonId)
            {
                ModelState.AddModelError("", "Invalid ID, please try again.");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (PersonImage != null)
                    {
                        using (var stream = new MemoryStream())
                        {
                            await PersonImage.CopyToAsync(stream);
                            person.PersonImage = stream.ToArray();
                            person.ImageContentType = PersonImage.ContentType;
                        }
                    }

                    _context.Update(person);
                    await _context.SaveChangesAsync();
                    TempData["message"] = "The record has been successfully updated";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PersonExists(person.PersonId))
                    {
                        ModelState.AddModelError("", "The record does not exist, try again");
                    }
                    else
                    {
                        ModelState.AddModelError("", "This record has already been updated");
                    }
                }
                catch (Exception e)
                {
                    ModelState.AddModelError("", $"Error on Edit: {e.GetBaseException().Message}");
                }
                return RedirectToAction("index","projects");
            }
            Create();
            return View(person);
        }

        // GET: Person/Delete/5
        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var person = await _context.Person.SingleOrDefaultAsync(m => m.PersonId == id);
                _context.Person.Remove(person);
                await _context.SaveChangesAsync();
                TempData["message"] = "The record has been successfully Deleted";
                return RedirectToAction("index","projects");
            }
            catch (Exception e)
            {
                TempData["message"] = $"Delete error: {e.GetBaseException().Message}";
            }

            return Redirect($"/person/delete/{id}");
        }

        private bool PersonExists(int id)
        {
            return _context.Person.Any(e => e.PersonId == id);
        }
    }
}
