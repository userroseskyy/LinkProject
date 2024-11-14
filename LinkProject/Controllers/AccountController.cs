using LinkProject.Models.Dto;
using Microsoft.AspNetCore.Mvc;

namespace LinkProject.Controllers
{
    public class AccountController : Controller
    {
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Register(RegesterDto regester)
        {
            return View();
        }
    }
}
