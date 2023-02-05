using Microsoft.AspNetCore.Mvc;
using Web.Areas.Admin.Services.Abstract;
using Web.Areas.Admin.ViewModels.Account;

namespace Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpGet]

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(AccountLoginVM model)
        {
            var isSucceeded = await _accountService.LoginAsync(model);
            if (isSucceeded) return RedirectToAction("index", "Dashboard");
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> LogOut()
        {
            await _accountService.LogOutAsync();
            return RedirectToAction(nameof(Login));
        }
    }
}
