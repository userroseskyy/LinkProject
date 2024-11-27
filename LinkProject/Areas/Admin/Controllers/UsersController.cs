
using LinkProject.Areas.Admin.Models.Dto.UserDto;
using LinkProject.Models.Dto;
using LinkProject.Models.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;


namespace LinkProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UsersController : Controller
    {
        private readonly UserManager<User> _userManager;
        public UsersController(UserManager<User> userManager)
        {
            _userManager = userManager;
        }
        [HttpGet]

        public IActionResult Index()
        {
            var users = _userManager.Users.Select(p => new UserListDto
            {
               Id= p.Id,
               FirstName=  p.FirstName,
                LastName=  p.LastName,
                UserName=p.UserName,
                PhoneNumber= p.PhoneNumber,
                EmailConfirmed= p.EmailConfirmed,
                AccessFailedCount=  p.AccessFailedCount

            }).ToList(); 
            
            return View(users);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(RegesterDto regester)
        {
            if (!ModelState.IsValid)
            {
                return View(regester);
            }
            User newUser= new User()
            {
                FirstName= regester.FirstName,
                LastName= regester.LastName,
                Email=regester.Email,
                UserName=regester.Email,
            };
           var result= _userManager.CreateAsync(newUser, regester.Password).Result;
            if (result.Succeeded)
            {
                return RedirectToAction("Index", "users", new {area="Admin"});
            }

             
            
            TempData["Message"]=string.Join(Environment.NewLine, result.Errors.Select(e => e.Description));
         
            
            
            return View();
        }
        [HttpGet]
        public IActionResult Edit(string id)
        {
            var user=_userManager.FindByIdAsync(id).Result;
            UserEditDto editUser= new UserEditDto()
            {
                Id=user.Id,
                FirstName=user.FirstName,
                LastName=user.LastName,
                Email=user.Email,
                UserName=user.UserName,
                PhoneNamber=user.PhoneNumber

            };
           
            return View(editUser);
        } 
        [HttpPost]
        public IActionResult Edit(UserEditDto userEdit)
        {
           var user= _userManager.FindByIdAsync(userEdit.Id).Result;
            user.FirstName=userEdit.FirstName;
            user.LastName=userEdit.LastName;
            user.Email=userEdit.Email;
            user.UserName=userEdit.UserName;
            user.PhoneNumber=userEdit.PhoneNamber;
           var result= _userManager.UpdateAsync(user).Result;
            if (result.Succeeded)
            {
                return RedirectToAction("Index", "users", new { area = "Admin" });

            }
            return View();


           
        }
        [HttpGet]
        public IActionResult Delete(string id)
        {
            var user=_userManager.FindByIdAsync(id).Result;

            UserDeleteDto userDelete = new UserDeleteDto()
            {
                Id=user.Id,
                FullName=$"{user.FirstName} {user.LastName}",
                
                UserName=user.UserName,
                Email=user.Email
            };
            return View(userDelete);
            
            
        } 
        [HttpPost]
        public IActionResult Delete(UserDeleteDto userDelete)
        {
            var user=_userManager.FindByIdAsync(userDelete.Id).Result;

            UserDeleteDto userDeleteDto = new UserDeleteDto()
            {
                Id=user.Id,
                FullName=$"{user.FirstName} {user.LastName}",
                
                UserName=user.UserName,
                Email=user.Email
            };
            var result = _userManager.DeleteAsync(user).Result;

            return RedirectToAction("index");
            
            
        }

       
    }
}
