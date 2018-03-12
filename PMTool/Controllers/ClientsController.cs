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
    public class ClientsController : Controller
    {
        private readonly PmToolDbContext _context;

        public ClientsController(PmToolDbContext context)
        {
            _context = context;
        }

        // GET: Clients
        public async Task<IActionResult> Index()
        {
            var pmToolDbContext = _context.Client.Include(c => c.Person);
            return View(await pmToolDbContext.ToListAsync());
        }

        // GET: Clients/Details/5
        public async Task<IActionResult> Details(int? id, string firstName, string lastName)
        {
            if (firstName != null)
            {
                ViewBag.firstName = firstName;
            }

            if (lastName != null)
            {
                ViewBag.lastName = lastName;
            }

            if (id == null)
            {
                return NotFound();
            }

            var client = await _context.Client
                .Include(c => c.Person)
                .SingleOrDefaultAsync(m => m.ClientId == id);
            if (client == null)
            {
                return NotFound();
            }

            return View(client);
        }

        // GET: Clients/Create
        public IActionResult Create()
        {
           
            ViewData["PersonId"] = new SelectList(_context.Person, "PersonId", "FirstName");
            return View();
        }

        // POST: Clients/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ClientId,PersonId,BusinessDescription,WebAddress,DomainLoginUrl,DomainUsername,DomainPassword,HostingLoginUrl,HostingUserName,HostingPassword,WpLoginUrl,WpUserName,WpPassword,GoogleAnalyticsUrl,GoogleAnalyticsUsername,GoogleAnalyticsPassword,GoogleSearchConsoleUrl,GoogleSearchConsoleUsername,GoogleSearchConsolePassword,BingWemasterToolsUrl,BingWemasterToolsUsername,BingWemasterToolsPassword,GoogleMyBusinessUrl,GoogleMyBusinessUsername,GoogleMyBusinessPassword,KeyWords,TargetKeyPhases,TargetAreas,CompetitorsUrl,SocialMedia,SocialMedia2,SocialMedia3,SocialMedia4,OtherMarketingTypes,MonthlyBudget,MonthlyClientTarget,ExpandPlaning,MarketingGoals")] Client client)
        {
            if (ModelState.IsValid)
            {
                _context.Add(client);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PersonId"] = new SelectList(_context.Person, "PersonId", "Email", client.PersonId);
            return View(client);
        }

        // GET: Clients/Edit/5
        public async Task<IActionResult> Edit(int? id, string firstName)
        {
            if (firstName != null)
            {
                ViewBag.firstName = firstName;
            }

           
            if (id == null)
            {
                return NotFound();
            }

            var client = await _context.Client.SingleOrDefaultAsync(m => m.ClientId == id);
            if (client == null)
            {
                return NotFound();
            }
            ViewData["PersonId"] = new SelectList(_context.Person, "PersonId", "FirstName", client.PersonId);
            return View(client);
        }

        // POST: Clients/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ClientId,PersonId,BusinessDescription,WebAddress,DomainLoginUrl,DomainUsername,DomainPassword,HostingLoginUrl,HostingUserName,HostingPassword,WpLoginUrl,WpUserName,WpPassword,GoogleAnalyticsUrl,GoogleAnalyticsUsername,GoogleAnalyticsPassword,GoogleSearchConsoleUrl,GoogleSearchConsoleUsername,GoogleSearchConsolePassword,BingWemasterToolsUrl,BingWemasterToolsUsername,BingWemasterToolsPassword,GoogleMyBusinessUrl,GoogleMyBusinessUsername,GoogleMyBusinessPassword,KeyWords,TargetKeyPhases,TargetAreas,CompetitorsUrl,SocialMedia,SocialMedia2,SocialMedia3,SocialMedia4,OtherMarketingTypes,MonthlyBudget,MonthlyClientTarget,ExpandPlaning,MarketingGoals")] Client client)
        {
            if (id != client.ClientId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(client);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClientExists(client.ClientId))
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
            ViewData["PersonId"] = new SelectList(_context.Person, "PersonId", "Email", client.PersonId);
            return View(client);
        }

        // GET: Clients/Delete/5
        public async Task<IActionResult> Delete(int? id, string firstName)
        {
            if (firstName != null)
            {
                ViewBag.firstName = firstName;
            }

            if (id == null)
            {
                return NotFound();
            }

            var client = await _context.Client
                .Include(c => c.Person)
                .SingleOrDefaultAsync(m => m.ClientId == id);
            if (client == null)
            {
                return NotFound();
            }

            return View(client);
        }

        // POST: Clients/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var client = await _context.Client.SingleOrDefaultAsync(m => m.ClientId == id);
            _context.Client.Remove(client);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClientExists(int id)
        {
            return _context.Client.Any(e => e.ClientId == id);
        }
    }
}
