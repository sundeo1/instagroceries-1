using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Instagroceries.Models;
using Microsoft.AspNetCore.Authorization;
using Instagroceries.ViewModel;

namespace Instagroceries.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        // GET: Account/Login
        public IActionResult Login(string returnUrl)
        {
            ViewData["Message"] = "Your application Login page.";
            return View(new LoginViewModel()
            {
                ReturnUrl = returnUrl
            });
        }

        // POST: Account/Login
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            if (!ModelState.IsValid)
                return View(loginViewModel);

            var user = await _userManager.FindByNameAsync(loginViewModel.UserName);

            if (user != null)
            {
                var result = await _signInManager.PasswordSignInAsync(user, loginViewModel.Password, false, false);
                if (result.Succeeded)
                {
                    if (string.IsNullOrEmpty(loginViewModel.ReturnUrl))
                        return RedirectToAction("Index", "Admin");
                    return Redirect(loginViewModel.ReturnUrl);
                }
            }
            ModelState.AddModelError("", "Username/Password not found");
            return View(loginViewModel);
        }

        // GET: Account/Register
        public IActionResult Register()
        {
            ViewData["Message"] = "Your application Register page.";
            return View();
        }

        // POST: Account/Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(LoginViewModel loginViewModel)
        {
            if (ModelState.IsValid)
            {
                var user = new IdentityUser() { UserName = loginViewModel.UserName };
                var result = await _userManager.CreateAsync(user, loginViewModel.Password);

                if (result.Succeeded)
                {
                    return RedirectToAction("Login", "Account");
                }
            }
            return View(loginViewModel);
        }

        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Clients");
        }

        // GET: Account/Roles
        [Authorize]
        public IActionResult Role()
        {
            ViewData["Message"] = "Your application Role page.";
            ViewData["Roles"] = _roleManager.Roles;
            return View("Role/Index");
        }

        // POST: Account/Roles
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Role(RoleViewModel roleViewModel)
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
                    return RedirectToAction("Role", "Account");
                }

                foreach (IdentityError identityError in result.Errors)
                {
                    ModelState.AddModelError("", identityError.Description);
                }
            }
            return View(roleViewModel);
        }

        // GET: Account/DeleteRole/5
        public async Task<IActionResult> DeleteRole(string id)
        {
            IdentityRole role = await _roleManager.FindByIdAsync(id);

            if (role != null)
            {
                IdentityResult result = await _roleManager.DeleteAsync(role);
                if (result.Succeeded)
                {
                    return RedirectToAction("Role", "Account");
                }

                foreach (IdentityError identityError in result.Errors)
                {
                    ModelState.AddModelError("", identityError.Description);
                }
            }
            else
            {
                ModelState.AddModelError("", "No role found");
            }

            return RedirectToAction("Role", "Account");
        }

        // GET: Account/Edit/5
        [HttpGet]
        public async Task<IActionResult> EditRole(string id)
        {
            //var test2 = await _context.Test2s.FindAsync(id);
            IdentityRole role = await _roleManager.FindByIdAsync(id);

            if (role != null)
            {
                EditRoleViewModel editRoleViewModel = new EditRoleViewModel
                {
                    Id = role.Id,
                    RoleName = role.Name
                };
                return View("Role/Edit", editRoleViewModel);
            }
            else
            {
                ModelState.AddModelError("", "No role found");
                ViewBag.ErrorMessage = $"Role with Id = {id} cannot be found";
                //return View("NotFound");
                return NotFound();
                //return View("Role/EditRole");
            }
        }

        // POST: Account/Edit/5
        [HttpPost]
        public async Task<IActionResult> EditRole(EditRoleViewModel editRoleViewModel)
        {
            //var test2 = await _context.Test2s.FindAsync(id);
            IdentityRole role = await _roleManager.FindByIdAsync(editRoleViewModel.Id);

            if (role != null)
            {
                role.Name = editRoleViewModel.RoleName;
                var result = await _roleManager.UpdateAsync(role);
                if (result.Succeeded)
                {
                    return RedirectToAction("Role", "Account");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return View("Role/Edit", editRoleViewModel);
            }
            else
            {
                ModelState.AddModelError("", "No role found");
                ViewBag.ErrorMessage = $"Role with Id = {editRoleViewModel.Id} cannot be found";
                //return View("NotFound");
                return NotFound();
                //return View("Role/EditRole");
            }
        }

        // GET: Account/ViewUserRole/5
        [HttpGet]
        public async Task<IActionResult> ViewUserRole(string id)
        {
            ViewBag.roleId = id;

            IdentityRole role = await _roleManager.FindByIdAsync(id);

            if (role == null)
            {
                ModelState.AddModelError("", "No role found");
                ViewBag.ErrorMessage = $"Role with Id = {id} cannot be found";
                return NotFound();
            }
            else
            {
                var userInRole = new List<EditRoleViewModel>();
                var userNotInRole = new List<EditRoleViewModel>();
                //var model = IList<EditRoleViewModel>;

                foreach (var user in _userManager.Users)
                {
                    if(await _userManager.IsInRoleAsync(user, role.Name))
                    {
                        var editRoleViewModel = new EditRoleViewModel
                        {
                            Id = user.Id,
                            RoleName = user.UserName
                        };

                        userInRole.Add(editRoleViewModel);
                    }
                    else
                    {
                        var editRoleViewModel = new EditRoleViewModel
                        {
                            Id = user.Id,
                            RoleName = user.UserName
                        };

                        userNotInRole.Add(editRoleViewModel);
                    }
                    
                }
                ViewData["userInRole"] = userInRole;
                ViewData["userNotInRole"] = userNotInRole;

                return View("Role/ViewUserRole", userInRole);
            }
        }
    }
}