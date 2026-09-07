using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SchoolManagementSystem.Models;
using SchoolManagementSystem.Services.Implementations;
using SchoolManagementSystem.Services.Interfaces;
using SchoolManagementSystem.ViewModels;

namespace SchoolManagementSystem.Controllers
{
    public class AccountController : Controller
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ICaptchaService _captchaService;

        public AccountController(
            SignInManager<ApplicationUser> signInManager,
            UserManager<ApplicationUser> userManager, ICaptchaService captchaService)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _captchaService = captchaService;
        }

        [HttpGet]
        public IActionResult Login(string? returnUrl = null)
        {
            var captcha = _captchaService.GenerateCaptcha();

            HttpContext.Session.SetString("CaptchaCode", captcha);

            ViewData["CaptchaCode"] = captcha;
            ViewData["ReturnUrl"] = returnUrl;

            return View();
        }
        [HttpGet]
        public IActionResult RefreshCaptcha()
        {
            var captcha = _captchaService.GenerateCaptcha();

            HttpContext.Session.SetString("CaptchaCode", captcha);

            return Json(new { captchaCode = captcha });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(
            LoginViewModel model,
            string? returnUrl = null)
        {
            if (!ModelState.IsValid)
            {
                ViewData["CaptchaCode"] =
                    HttpContext.Session.GetString("CaptchaCode");

                ViewData["ReturnUrl"] = returnUrl;

                return View(model);
            }
            //capch 
            var captchaCode = HttpContext.Session.GetString("CaptchaCode");

            if (string.IsNullOrEmpty(captchaCode) ||
                !_captchaService.ValidateCaptcha(model.Captcha ?? "", captchaCode))
            {
                ModelState.AddModelError(
                    "Captcha",
                    "کد امنیتی صحیح نیست.");

                var newCaptcha = _captchaService.GenerateCaptcha();
                HttpContext.Session.SetString("CaptchaCode", newCaptcha);
                ViewData["CaptchaCode"] = newCaptcha;

                return View(model);
            }
            //***
            var user = await _userManager.FindByEmailAsync(model.Email);

            if (user == null)
            {
                ModelState.AddModelError(
                    string.Empty,
                    "ایمیل یا رمز عبور اشتباه است.");

                return View(model);
            }

            var result = await _signInManager.PasswordSignInAsync(
                user.UserName!,
                model.Password,
                model.RememberMe,
                lockoutOnFailure: true);

            if (result.Succeeded)
            {
                if (!string.IsNullOrEmpty(returnUrl) &&
                    Url.IsLocalUrl(returnUrl))
                {
                    return Redirect(returnUrl);
                }

                return RedirectToAction("Index", "Home");
            }

            if (result.IsLockedOut)
            {
                ModelState.AddModelError(
                    string.Empty,
                    "حساب کاربری موقتاً قفل شده است.");
            }
            else
            {
                ModelState.AddModelError(
                    string.Empty,
                    "ایمیل یا رمز عبور اشتباه است.");
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();

            return RedirectToAction("Index", "Home");
        }
    }
}