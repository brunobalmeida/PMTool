using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PMTool.Models;
using Microsoft.EntityFrameworkCore;

namespace PMTool.Controllers
{
    public class HomeController : Controller
    {
        private readonly PmToolDbContext _context;

        public HomeController(PmToolDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var pmToolDbContext = _context.Project.Include(p => p.Client).Include(p => p.Employee);
            return View(await pmToolDbContext.ToListAsync());
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
