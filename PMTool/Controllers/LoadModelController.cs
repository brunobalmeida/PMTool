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
    [Authorize]
    public class LoadModelController : Controller
    {
        private readonly PmToolDbContext _context;

        public LoadModelController(PmToolDbContext context)
        {
            _context = context;
        }

        // GET: LoadModel
        public async Task<IActionResult> Index()
        {
            return View(await _context.ModelProject.ToListAsync());
        }

        // GET: LoadModel/Details/5
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

        // GET: LoadModel/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: LoadModel/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int? id)
        {
            var modelProject = await _context.ModelProject.SingleOrDefaultAsync(m => m.ModelProjectId == id);
            var newProject = new Project
            {
                projectName = modelProject.
            };
            //if (ModelState.IsValid)
            //{
            //    _context.Add(modelProject);
            //    await _context.SaveChangesAsync();
            //    return RedirectToAction(nameof(Index));
            //}
            //return View(modelProject);
        }

        // GET: LoadModel/Edit/5
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

        // POST: LoadModel/Edit/5
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

        // GET: LoadModel/Delete/5
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

        // POST: LoadModel/Delete/5
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
