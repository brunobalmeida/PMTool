using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PMTool.Models;
using PmToolClassLibrary;
using Microsoft.AspNetCore.Identity;
using PMTool.Services;
using System.Collections.Generic;

namespace PMTool.Controllers
{

    public class ClientsController : Controller
    {
        private readonly PmToolDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IEmailSender _emailSender;

        public ClientsController(PmToolDbContext context, UserManager<ApplicationUser> userManager, IEmailSender emailSender)
        {
            _context = context;
            _userManager = userManager;
            _emailSender = emailSender;
        }

        // GET: Clients
        public async Task<IActionResult> Index()
        {
            var pmToolDbContext = _context.Client.Include(c => c.Person)
                .Where(a=>a.ClientActiveFlag == 1)
                .OrderBy(a=> a.Person.FirstName);
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
        public async Task<IActionResult> Create([Bind("ClientId,PersonId,BusinessDescription,WebAddress,DomainLoginUrl,DomainUsername,DomainPassword,HostingLoginUrl,HostingUserName,HostingPassword,WpLoginUrl,WpUserName,WpPassword,GoogleAnalyticsUrl,GoogleAnalyticsUsername,GoogleAnalyticsPassword,GoogleSearchConsoleUrl,GoogleSearchConsoleUsername,GoogleSearchConsolePassword,BingWemasterToolsUrl,BingWemasterToolsUsername,BingWemasterToolsPassword,GoogleMyBusinessUrl,GoogleMyBusinessUsername,GoogleMyBusinessPassword,KeyWords,TargetKeyPhases,TargetAreas,CompetitorsUrl,SocialMedia,SocialMedia2,SocialMedia3,SocialMedia4,OtherMarketingTypes,MonthlyBudget,MonthlyClientTarget,ExpandPlaning,MarketingGoals")] Client client, string taEmail)
        {
            IList<string> stringList;
            stringList = taEmail.Split(',').ToList();
            //replace

            foreach(string item in stringList)
            {
                if (!Validations.EmailValidation(item))
                {
                    TempData["message"] = "Email can not be empty or is not in a valid format.";
                    return RedirectToAction("Index");
                }
                try
                {
                    //if (ModelState.IsValid)
                    //{

                    //var pmToolDbContext = _context.OwnersLicense.Include(a => a.Person);
                    var user = await _userManager.GetUserAsync(User);

                    //string emailToRegister = taEmail;
                    string flagEmpOrClient = "Client";
                    var emailCheck = user.Email;
                    int licenseId = _context.Person.FirstOrDefault(a => a.Email == emailCheck).OwnersLicenseId;
                    var licenseEmail = item;
                    var callbackUrl = Url.EmailRegistrationLink(licenseId, licenseEmail, Request.Scheme, flagEmpOrClient);
                    await _emailSender.SendEmailRegistrationAsync(licenseEmail, callbackUrl);
                    TempData["message"] = "The client has been successfully added and the verification email sent.";
                    
                    //_context.Add(employee);
                    //await _context.SaveChangesAsync();
                    //TempData["message"] = "Record Successfully added.";
                    //return RedirectToAction(nameof(Index));
                    //}
                }
                catch (Exception e)
                {
                    ModelState.AddModelError("", $"Create error:{e.GetBaseException().Message}");
                }
            }
            return RedirectToAction(nameof(Index));
            //Create();
            //return View(client);

            //try
            //{
            //    if (ModelState.IsValid)
            //    {
            //        _context.Add(client);
            //        await _context.SaveChangesAsync();
            //        TempData["message"] = "The Client's data has been successfully added to the database.";
            //        return RedirectToAction(nameof(Index));
            //    }
            //}
            //catch (Exception e)
            //{
            //    ModelState.AddModelError("", $"Create error:{e.GetBaseException().Message}");
            //}
            //Create();
            //return View(client);
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
                ModelState.AddModelError("", "Invalid ID, please try again.");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(client);
                    await _context.SaveChangesAsync();
                    TempData["message"] = "The record has been successfully updated";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClientExists(client.ClientId))
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
            try
            {
                var client = await _context.Client.SingleOrDefaultAsync(m => m.ClientId == id);
                _context.Client.Remove(client);
                await _context.SaveChangesAsync();
                TempData["message"] = "The record has been successfully Deleted";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception e)
            {
                TempData["message"] = $"Delete error: {e.GetBaseException().Message}";
            }
            return Redirect($"/clients/delete/{id}");
        }

        private bool ClientExists(int id)
        {
            return _context.Client.Any(e => e.ClientId == id);
        }


        //Method to change the client flag status 
        public async Task<IActionResult> DeleteClient(int id)
        {
            try
            {
                var client = _context.Client.SingleOrDefault(a => a.PersonId == id);
                client.ClientActiveFlag = 0;
                await _context.SaveChangesAsync();
                TempData["message"] = "The client has been removed.";
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", $"Delete error:{e.GetBaseException().Message}");
            }

            return RedirectToAction("index", "clients");
        }


    }
}
