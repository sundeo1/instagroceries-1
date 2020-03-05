using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Instagroceries.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Instagroceries.Controllers
{
    public class RoleController : Controller
    {

        private readonly RoleManager<IdentityRole> _roleManager;

        public RoleController(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }

        // GET: Role/Index
        public IActionResult Index()
        {
            ViewData["Message"] = "Your application Role page.";
            return View();
        }

        //POST: Role/Create
       [HttpPost]
       [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(RoleViewModel roleViewModel)
        {
            if (ModelState.IsValid)
            {
                IdentityRole identityRole = new IdentityRole
                {
                    Name = roleViewModel.RoleName
                };

                IdentityResult result = await _roleManager.CreateAsync(identityRole);

                if (result.Succeeded)
                {
                    return RedirectToAction("Login", "Account");
                }

                foreach (IdentityError identityError in result.Errors)
                {
                    ModelState.AddModelError("", identityError.Description);
                }
            }
            return View(roleViewModel);
        }
    }
}