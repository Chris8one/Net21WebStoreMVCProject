using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Net21WebStoreMVCProject.Models;
using Net21WebStoreMVCProject.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Net21WebStoreMVCProject.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdministrationController : Controller
    {
        private readonly AppDbContext appDbContext;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<IdentityUser> userManager;
        private readonly ICategoryRepository categoryRepository;
        private readonly ICandyRepository candyRepository;

        public AdministrationController(AppDbContext appDbContext, RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> userManager, ICategoryRepository categoryRepository, ICandyRepository candyRepository)
        {
            this.appDbContext = appDbContext;
            this.roleManager = roleManager;
            this.userManager = userManager;
            this.categoryRepository = categoryRepository;
            this.candyRepository = candyRepository;
        }

        [HttpGet]
        public IActionResult ListUsers()
        {
            var users = userManager.Users;

            return View(users);
        }

        [HttpGet]
        public IActionResult CreateRole()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateRole(CreateRoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                IdentityRole identityRole = new IdentityRole
                {
                    Name = model.RoleName
                };

                IdentityResult result = await roleManager.CreateAsync(identityRole);

                if (result.Succeeded)
                {
                    return RedirectToAction("ListRoles", "Administration");
                }

                foreach (IdentityError error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult ListRoles()
        {
            var roles = roleManager.Roles;

            return View(roles);
        }

        [HttpGet]
        public async Task<IActionResult> EditRole(string id)
        {
            var role = await roleManager.FindByIdAsync(id);

            if (roleManager == null)
            {
                ViewBag.ErrorMessage = $"Role with Id: {id} cannot be found";

                return View("Not Found");
            }

            var model = new EditRoleViewModel
            {
                Id = role.Id,
                RoleName = role.Name
            };

            foreach (var user in userManager.Users)
            {
                if(await userManager.IsInRoleAsync(user, role.Name))
                {
                    model.Users.Add(user.UserName);
                }
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditRole(EditRoleViewModel model)
        {
            var role = await roleManager.FindByIdAsync(model.Id);

            if (roleManager == null)
            {
                ViewBag.ErrorMessage = $"Role with Id: {model.Id} cannot be found";

                return View("Not Found");
            }
            else
            {
                role.Name = model.RoleName;
                var result = await roleManager.UpdateAsync(role);

                if (result.Succeeded)
                {
                    return RedirectToAction("ListRoles");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }

                return View(model);
            }
        }

        [HttpGet]
        public async Task<IActionResult> EditUsersInRole(string roleId)
        {
            ViewBag.roleId = roleId;

            var role = await roleManager.FindByIdAsync(roleId);

            if (role == null)
            {
                ViewBag.ErrorMessage = $"Role with Id: {roleId} cannot be found";

                return View("NotFound");
            }

            var model = new List<UserRoleViewModel>();

            foreach (var user in userManager.Users)
            {
                var userRoleViewModel = new UserRoleViewModel
                {
                    UserId = user.Id,
                    UserName = user.UserName
                };

                if (await userManager.IsInRoleAsync(user, role.Name))
                {
                    userRoleViewModel.IsSelected = true;
                }
                else
                {
                    userRoleViewModel.IsSelected = false;
                }

                model.Add(userRoleViewModel);
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditUsersInRole(List<UserRoleViewModel> model, string roleId)
        {
            var role = await roleManager.FindByIdAsync(roleId);

            if (role == null)
            {
                ViewBag.ErrorMessage = $"Role with Id: {roleId} cannot be found";

                return View("NotFound");
            }

            for (int i = 0; i < model.Count; i++)
            {
                var user = await userManager.FindByIdAsync(model[i].UserId);

                IdentityResult result = null;

                if (model[i].IsSelected && !(await userManager.IsInRoleAsync(user, role.Name)))
                {
                    result = await userManager.AddToRoleAsync(user, role.Name);
                }
                else if (!model[i].IsSelected && await userManager.IsInRoleAsync(user, role.Name))
                {
                    result = await userManager.RemoveFromRoleAsync(user, role.Name);
                }
                else
                {
                    continue;
                }

                if (result.Succeeded)
                {
                    if (i < (model.Count - 1))
                    {
                        continue;
                    }
                    else
                    {
                        return RedirectToAction("EditRole", new { Id = roleId });
                    }
                }
            }

            return RedirectToAction("EditRole", new { Id = roleId });
        }

        public IActionResult CandyList()
        {
            var candies = candyRepository.GetAllCandy.OrderBy(c => c.CandyId);

            return View(candies);
        }

        public IActionResult CreateCandy()
        {
            return View(new CandyProductViewModel
            {
                Category = categoryRepository.GetAllCategories
            });
        }

        [HttpPost]
        public IActionResult CreateCandy(Candy candy)
        {
            appDbContext.Add(candy);
            appDbContext.SaveChanges();

            return RedirectToAction("CandyList");
        }

        public IActionResult DeleteCandy(int id)
        {
            var candy = candyRepository.GetCandyById(id);

            if (candy != null)
            {
                return View(candy);
            }

            return NotFound();
        }

        [HttpPost]
        public IActionResult DeleteCandy(Candy candy)
        {
            appDbContext.Remove(candy);
            appDbContext.SaveChanges();

            return RedirectToAction("CandyList");
        }

        public IActionResult EditCandy(int id)
        {
            var candy = candyRepository.GetCandyById(id);

            if (candy != null)
            {
                return View(new CandyProductViewModel
                {
                    Candy = candy,
                    Category = categoryRepository.GetAllCategories
                });
            }
            
            return NotFound();
        }

        [HttpPost]
        public IActionResult EditCandy(CandyProductViewModel candyProduct)
        {
            if (ModelState.IsValid)
            {
                appDbContext.Entry(candyProduct.Candy).State = EntityState.Modified;
                appDbContext.SaveChanges();

                return RedirectToAction("CandyList");
            }

            candyProduct.Category = categoryRepository.GetAllCategories;

            return View(candyProduct);
        }
    }
}