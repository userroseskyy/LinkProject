using Microsoft.AspNetCore.Mvc;

namespace LinkProject.Areas.Admin.Controllers
{
    public class UsersController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
