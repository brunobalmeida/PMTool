using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PMTool.Models;

namespace PMTool.Controllers
{
    [Authorize(Roles = "Owner")]
    public class CountryController : Controller
    {
        private readonly PmToolDbContext _context;

        public CountryController(PmToolDbContext context)
        {
            _context = context;
        }

        // GET: Country
        public async Task<IActionResult> Index()
        {
            return View(await _context.Country.ToListAsync());
        }
        
        // GET: Country/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Country/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CountryId,CountryName")] Country country)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Add(country);
                    await _context.SaveChangesAsync();
                    TempData["message"] = "The Country has been successfully added to the Database.";
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", $"Create error:{e.GetBaseException().Message}");
            }
            return View(country);
        }

        // GET: Country/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var country = await _context.Country.SingleOrDefaultAsync(m => m.CountryId == id);
            if (country == null)
            {
                return NotFound();
            }
            return View(country);
        }

        // POST: Country/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CountryId,CountryName")] Country country)
        {
            if (id != country.CountryId)
            {
                ModelState.AddModelError("", "Invalid ID, please try again.");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(country);
                    await _context.SaveChangesAsync();
                    TempData["message"] = "The record has been successfully updated";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CountryExists(country.CountryId))
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
                return RedirectToAction(nameof(Index));
            }
            return View(country);
        }

        // GET: Country/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var country = await _context.Country
                .SingleOrDefaultAsync(m => m.CountryId == id);
            if (country == null)
            {
                return NotFound();
            }

            return View(country);
        }

        // POST: Country/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var country = await _context.Country.SingleOrDefaultAsync(m => m.CountryId == id);
                _context.Country.Remove(country);
                await _context.SaveChangesAsync();
                TempData["message"] = "The record has been successfully Deleted";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception e )
            {
                TempData["message"] = $"Delete error: {e.GetBaseException().Message}";
            }
            return Redirect($"/country/delete/{id}");
        }

        private bool CountryExists(int id)
        {
            return _context.Country.Any(e => e.CountryId == id);
        }
    }
}
