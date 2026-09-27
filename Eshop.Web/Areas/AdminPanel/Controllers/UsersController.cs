using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Eshop.Data.Context;
using Eshop.Domain.Models.Users;
using Eshop.Application.Services.Interfaces;
using Eshop.Domain.ViewModels.User;

namespace Eshop.Web.Areas.AdminPanel.Controllers
{
    [Area("AdminPanel")]
    public class UsersController : Controller
    {
        private readonly EshopDbContext _context;
        private readonly IUserService _userService;
        private readonly IConfiguration _configuration;
        private readonly IRoleService _roleService;

    

        public UsersController(EshopDbContext context,IUserService userService,IConfiguration configuration,IRoleService roleService)
        {
            _context = context;
            _userService = userService;
            _configuration = configuration;
            _roleService = roleService;
        }

        #region GetAllUser

        // GET: AdminPanel/Users
        public async Task<IActionResult> Index(string search,int pageId=1)
        {
            int pagesize = int.Parse(_configuration.GetSection("pageSize").Value);
            int pagecount = _userService.GetPageUserCount(pagesize);
            ViewData["pagecount"]=pagecount;
            ViewData["pageId"]= pageId;
            ViewData["search"] = search;
            var result=_userService.GetUsersForAdmin(pageId, pagesize,search);
           
            return View(result);
        }

        #endregion

        #region Details

        // GET: AdminPanel/Users/Details/5
        public async Task<IActionResult> Details(int id)
        {

            var user = _userService.GetUserById(id);
            if (user == null)
                return NotFound();
            return View(user);
        }

        #endregion

        #region Create

        // GET: AdminPanel/Users/Create
        public IActionResult Create()
        {
            var roles=_roleService.GetAllRoles();
            ViewData["RoleId"] = new SelectList(roles, "RoleId", "RoleTitle");
            return View();
        }

        // POST: AdminPanel/Users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateUserViewModel model)
        {
            if (!ModelState.IsValid)
            { 
                var roles=_roleService.GetAllRoles();
                ViewData["RoleId"] = new SelectList(roles, "RoleId", "RoleTitle");
                return View(model);
            }
            var result=_userService.CreateUser(model);
            switch (result)
            {
                case CreateUserResult.Success:
                    return RedirectToAction(nameof(Index));
                   
                case CreateUserResult.EmailInvalid:
                    ModelState.AddModelError("Email", "ایمیل وارد شده تکراری است.");
                    break;
                case CreateUserResult.Error:
                    ModelState.AddModelError("Email", "خطایی رخ داده است");
                    break;
            }

            return View(model);
        }

        #endregion

        #region Edit

        // GET: AdminPanel/Users/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var user = _userService.GetUserForEdit(id);
            if (user == null)
            {
                return NotFound();
            }
            var roles=_roleService.GetAllRoles();
            ViewData["RoleId"] = new SelectList(roles, "RoleId", "RoleTitle");
            return View(user);
        }

        // POST: AdminPanel/Users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(EditUserViewModel model)
        {

            if (!ModelState.IsValid)
            {
                var roles=_roleService.GetAllRoles();
                ViewData["RoleId"] = new SelectList(roles, "RoleId", "RoleTitle");
                return View(model);
            }
          var result=_userService.UpdateUser(model);
          switch (result)
          {
              case EditUserResult.Success:
                  return RedirectToAction(nameof(Index));
                  break;
              case EditUserResult.Error:
                  ModelState.AddModelError("Email", "خطایی رخ داده است");
                  break;
              case EditUserResult.UserNotFound:
                  ModelState.AddModelError("Email", "کاربر وارد شده یافت نشد.");
                  break;
          }
            return View(model);
            
        }

        #endregion

        // GET: AdminPanel/Users/Delete/5
        public IActionResult Delete(int id)
        {
            var result=_userService.DeleteUser(id);
            return RedirectToAction(nameof(Index));
        }
        
        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.UserId == id);
        }
    }
}
