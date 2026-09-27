using System.Security.Claims;
using Eshop.Application.Services.Interfaces;
using Eshop.Domain.ViewModels.User;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;

namespace Eshop.Web.Controllers
{
    public class AccountController(IUserService userService) : Controller
    {
        #region Actions

        #region Register

        [HttpGet("/Register")]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost("/Register")]
        public IActionResult Register(RegisterUserViewModel model)
        {
            if (!model.ConfirmRules)
                ModelState.AddModelError("ConfirmRules", "قوانین را نپذیرفته اید");
            if (!ModelState.IsValid)
                return View(model);
            RegisterResult result = userService.RegisterUser(model);
            switch (result)
            {
                case RegisterResult.Success:
                    return View("Success", model);
                case RegisterResult.Failed:
                    ModelState.AddModelError("Email", "خطایی رخ داده است");
                    break;
                case RegisterResult.EmailInvalid:
                    ModelState.AddModelError("Email", "این ایمیل قبلا ثبت نام کرده است");
                    break;
            }

            return View(model);
        }

        #endregion

        #region Login

        [HttpGet("/Login")]
        public IActionResult Login()
        {
             //ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost("/Login")]
        public IActionResult Login(LoginUserViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);
            LoginResult result = userService.LoginUser(model);
            if (result.IsSuccess)
            {
               var claims=new List<Claim>()
               {
                   new Claim(ClaimTypes.Name,model.Email),
                    new Claim("UserId",result.User.UserId.ToString()),
                    new Claim("Role",result.User.RoleId.ToString())
               };
               ClaimsIdentity identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
               ClaimsPrincipal principal = new ClaimsPrincipal(identity);
               var properties = new AuthenticationProperties()
               {
                   IsPersistent = model.Rememberme
               };
               HttpContext.SignInAsync(principal, properties);
               return Redirect("/");
            }

            if (result.IsLocked)
            {
                var remaining = (result.LockedOut.Value - DateTime.Now).TotalMinutes;
                ModelState.AddModelError("Email", $"نام کاربری شما {Math.Ceiling(remaining)} دقیقه مسدود است");
            }

            else if (!result.IsSuccess)
            {
                ModelState.AddModelError("Email", "نام کاربری کلمه عبور یافت نشد");
            }

            return View();
        }

       
        #endregion
        #region Logout

        [Route("/Logout")]
        public IActionResult Logout()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login"); 
        }
        

        #endregion

        #region ForgotPassword
        [HttpGet("/Forgot-Password")]
        public IActionResult ForgotPassword()
        {
            return View();
        }
            [HttpPost("/Forgot-Password")]
                public IActionResult ForgotPassword(ForgotPasswordViewModel model)
                {
                    if (!ModelState.IsValid)
                        return View(model);
                    ForgotPasswordResult result = userService.Forgotpassword(model);
                    switch (result)
                    {
                        case ForgotPasswordResult.Success:
                            return RedirectToAction("ResetPassword");
                        case ForgotPasswordResult.UserNotFound:
                            ModelState.AddModelError("Email", "ایمیل وارد شده پیدا نشد.");
                            break;
                    }
                    return View(model);
                }

        #endregion
        #region ResetPassword
        [HttpGet("/Reset-Password")]
        public IActionResult ResetPassword()
        {
            return View();
        }
        [HttpPost("/Reset-Password")]
        public IActionResult ResetPassword(ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);
            ResetPasswordResult result=userService.ResetPassword(model);
            switch (result)
            {
                case ResetPasswordResult.UserNotFound:
                    ModelState.AddModelError("ConfirmCode", "کد وارد شده صحیح نیست");
                    break;
                case ResetPasswordResult.Failed:
                    ModelState.AddModelError("ConfirmCode", "خطایی رخ داده است");
                    break;
                case ResetPasswordResult.Success:
                    return RedirectToAction("Login");
                    break;
            }
            return View(model);
        }
    

        #endregion
        #endregion

        #region AccsessDenied
        [Route("/Accsess-Denied")]
        public IActionResult AccsessDenied()
        {
            return View();
        }

        #endregion
    }
}