using System.Security.Claims;
using Eshop.Application.Services.Interfaces;
using Eshop.Domain.ViewModels.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Eshop.Web.Areas.UserPanel.Controllers
{
    [Area("UserPanel")]
    [Authorize]
    public class HomeController(IUserService userService) : Controller
    {
        #region Actions

        #region Index

        public IActionResult Index()
        {
            return View();
        }

        #endregion

        #region ChangePassword

        public IActionResult ChangePassword()
        {
            return View();
        }
        [HttpPost]
        public IActionResult ChangePassword(ChangePasswordViewModel model)
        {
            if(!ModelState.IsValid)
                return View(model);
            string curentuseremail = User.FindFirstValue(ClaimTypes.Name);
            ChangePasswordResult result = userService.ChangePassword(curentuseremail, model);
             ViewData["Result"]=result;
            return View();
        }

        #endregion

        #endregion
    }
}
