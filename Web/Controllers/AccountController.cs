using Microsoft.AspNetCore.Mvc;
using Web.Attributes;
using Web.Services.Abstract;
using Web.ViewModels.Account;

namespace Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpGet]
        [OnlyAnonymous]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(AccountRegisterVM model)
        {
            var isSucceeded = await _accountService.RegisterUserAsync(model);
            if (isSucceeded) return RedirectToAction("login");
            return View(model);
        }

        [HttpGet]
        [OnlyAnonymous]
        public async Task<IActionResult> Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(AccountLoginVM model)
        {
            var isSucceeded = await _accountService.LoginUserAsync(model);
            if (!isSucceeded) return View(model);
            if (!string.IsNullOrEmpty(model.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl))
            {
                return Redirect(model.ReturnUrl);
            }
            else
            {
                return RedirectToAction("index", "home");
            }
        }

        public async Task<IActionResult> Logout()
        {
            await _accountService.LogoutAsync();
            return RedirectToAction("login");
        }
    }
}
