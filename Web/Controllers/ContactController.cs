using Microsoft.AspNetCore.Mvc;
using Web.Services.Abstract;

namespace Web.Controllers
{
    public class ContactController : Controller
    {
        private readonly IContactService _contactService;

        public ContactController(IContactService ContactService)
        {
            _contactService = ContactService;
        }
        public async Task<IActionResult> Index()
        {
            var model = await _contactService.GetAsync();
            return View(model);
        }
    }
}
