using Microsoft.AspNetCore.Mvc;

namespace PetList.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Register()
        {
            return View();
        }
    }
}