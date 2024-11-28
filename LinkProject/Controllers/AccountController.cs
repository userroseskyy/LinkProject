using LinkProject.Models.Dto;
using LinkProject.Models.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LinkProject.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager=signInManager;
            _signInManager = signInManager;
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Register(RegesterDto regester)
        {
            if (!ModelState.IsValid)
            {
                return View(regester);
            }
            User newUser=new User()
            {
                FirstName = regester.FirstName,
                LastName = regester.LastName,
                UserName=regester.Email,
                Email = regester.Email
            };
          var result=  _userManager.CreateAsync(newUser , regester.Password).Result;
            if (result.Succeeded) 
            { 
                return RedirectToAction("Index", "Home");
            }

            TempData["Message"]=string.Join(Environment.NewLine,result.Errors.Select(e=> e.Description));

            return View();
            
        }
        [HttpGet]
        public IActionResult Login(string returnUrl="/") 
        {
            
            return View(new LoginDto
            {
                ReturnUrl = returnUrl,
            });
        }
        [HttpPost]
        public IActionResult Login(LoginDto login) 
        {
            if (!ModelState.IsValid)
            { 
                return View(login);
            }
            var user=_userManager.FindByNameAsync(login.userName).Result;

            _signInManager.SignOutAsync();

            if (user!=null) 
            { 
                
                var result=_signInManager.PasswordSignInAsync(user, login.Password,login.IsPersistent,true).Result;
                if (result.Succeeded) 
                {
                    return Redirect(login.ReturnUrl);
                }

            }


            
            return RedirectToAction("Register") ;
        }
        [HttpGet]
        public IActionResult LogOut()
        {
            _signInManager.SignOutAsync();
            return RedirectToAction("Index","Home");
        }

        }
}
