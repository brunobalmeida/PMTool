using System;
using System.Collections.Generic;
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
    [Authorize(Roles = "Owner")]
    public class ProvinceController : Controller
    {
        private readonly PmToolDbContext _context;

        public ProvinceController(PmToolDbContext context)
        {
            _context = context;
        }

        // GET: Province
        public async Task<IActionResult> Index(Int32? countryId, string countryName)
        {
            if (countryId!=null)
            {
                HttpContext.Session.SetString(nameof(countryId), countryId.ToString());
                HttpContext.Session.SetString(nameof(countryName), countryName);
            }
            else if (HttpContext.Session.GetString("countryId")!=null )
            {
                countryId = int.Parse(HttpContext.Session.GetString("countryId"));
            }
            else
            {
                TempData["message"] = "Please select a country to insert the province/state.";
                return RedirectToAction("index", "country");
            }
            
            var pmToolDbContext = _context.Province
                .Include(p => p.Country)
                .Where(a=>a.CountryId == countryId)
                .OrderBy(a=>a.ProvinceName);
            return View(await pmToolDbContext.ToListAsync());
        }
                
        // GET: Province/Create
        public IActionResult Create()
        {
            ViewData["CountryId"] = new SelectList(_context.Country, "CountryId", "CountryName");
            return View();
        }

        // POST: Province/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProvinceId,ProvinceName,ProvinceCode,CountryId")] Province province)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Add(province);
                    await _context.SaveChangesAsync();
                    TempData["message"] = "The Province has been successfully added to the Database.";
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", $"Create error:{e.GetBaseException().Message}");
            }
            Create();
            return View(province);
        }

        // GET: Province/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var province = await _context.Province.SingleOrDefaultAsync(m => m.ProvinceId == id);
            if (province == null)
            {
                return NotFound();
            }
            ViewData["CountryId"] = new SelectList(_context.Country, "CountryId", "CountryName", province.CountryId);
            return View(province);
        }

        // POST: Province/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProvinceId,ProvinceName,ProvinceCode,CountryId")] Province province)
        {
            if (id != province.ProvinceId)
            {
                ModelState.AddModelError("", "Invalid ID, please try again.");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(province);
                    await _context.SaveChangesAsync();
                    TempData["message"] = "The record has been successfully updated";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProvinceExists(province.ProvinceId))
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
            Create();
            return View(province);
        }

        // GET: Province/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var province = await _context.Province
                .Include(p => p.Country)
                .SingleOrDefaultAsync(m => m.ProvinceId == id);
            if (province == null)
            {
                return NotFound();
            }

            return View(province);
        }

        // POST: Province/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var province = await _context.Province.SingleOrDefaultAsync(m => m.ProvinceId == id);
                _context.Province.Remove(province);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception e)
            {
                TempData["message"] = $"Delete error: {e.GetBaseException().Message}";
            }
            return Redirect($"/province/delete/{id}");
        }

        private bool ProvinceExists(int id)
        {
            return _context.Province.Any(e => e.ProvinceId == id);
        }
    }
}
