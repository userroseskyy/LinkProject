using LinkProject.Areas.Profile.Models.User;
using LinkProject.Data;
using LinkProject.Models.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LinkProject.Areas.Profile.Controllers
{
    [Area("Profile")]
    public class UserAccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly DataBaseContext _Context;

        public UserAccountController(UserManager<User> userManager, DataBaseContext Context)
        {
            _userManager = userManager;
            _Context = Context;
        }
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult AddIcon()
        {

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddIcon(IFormFile iconFile, string name)
        {
            if (iconFile != null && iconFile.Length > 0)
            {
                
                var uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/icons");
                Directory.CreateDirectory(uploadPath);

                var fileName = Guid.NewGuid() + Path.GetExtension(iconFile.FileName);
                var filePath = Path.Combine(uploadPath, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await iconFile.CopyToAsync(stream);
                }

                
                var icon = new Icon
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = name,
                    Url = "/icons/" + fileName
                };

                _Context.Icons.Add(icon);
                await _Context.SaveChangesAsync();

                return RedirectToAction("Index");
            }

           
            return View();
        }
    }
    }
