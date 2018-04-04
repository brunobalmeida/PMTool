using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PMTool.Models;
using PMTool.Models.ViewModels;

namespace PMTool.Controllers
{
    [Authorize(Roles ="Admin, Developers")]
    public class UserManagementController : Controller
    {
        private UserManager<ApplicationUser> _userManager;
        private RoleManager<IdentityRole> _roleManager;

        public UserManagementController(
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<IActionResult> Index()
        {
            var users = _userManager.Users.OrderBy(a => a.UserName);
            List<UserListViewModel> listUsers = new List<UserListViewModel>();

            foreach (var item in users)
            {
                UserListViewModel user = new UserListViewModel();

                user.UserName = item.UserName;
                user.Email = item.Email;
                user.IsAdmin = await _userManager.IsInRoleAsync(item, "Admin");
                user.IsLocal = item.PasswordHash == null;
                user.IsLocked = item.LockoutEnd > DateTime.Now;
                user.IsClient = await _userManager.IsInRoleAsync(item, "Client");
                user.IsEmployee = await _userManager.IsInRoleAsync(item, "Employee");
                user.IsOwner= await _userManager.IsInRoleAsync(item, "Owner");
                listUsers.Add(user);
            }

            listUsers = listUsers.OrderBy(a => a.IsLocked).OrderBy(a => a.UserName).ToList();
            
            return View(listUsers);
        }
    }
}