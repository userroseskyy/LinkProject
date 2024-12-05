
using LinkProject.Areas.Admin.Models.Dto.UserDto;

using LinkProject.Models.Dto;
using LinkProject.Models.Role;
using LinkProject.Models.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Role = LinkProject.Models.Role.Role;



namespace LinkProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    
    public class UsersController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _RoleManager;


        public UsersController(UserManager<User> userManager, RoleManager<LinkProject.Models.Role.Role> RoleManager)
        {
            _userManager = userManager;
            _RoleManager = RoleManager;
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
            User newUser = new User()
            {
                FirstName= regester.FirstName,
                LastName= regester.LastName,
                Email=regester.Email,
                UserName=regester.Email,
            };
            var result = _userManager.CreateAsync(newUser, regester.Password).Result;
            if (result.Succeeded)
            {
                return RedirectToAction("Index", "users", new { area = "Admin" });
            }



            TempData["Message"]=string.Join(Environment.NewLine, result.Errors.Select(e => e.Description));



            return View();
        }
        [HttpGet]
        public IActionResult Edit(string id)
        {
            var user = _userManager.FindByIdAsync(id).Result;
            UserEditDto editUser = new UserEditDto()
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
            var user = _userManager.FindByIdAsync(userEdit.Id).Result;
            user.FirstName=userEdit.FirstName;
            user.LastName=userEdit.LastName;
            user.Email=userEdit.Email;
            user.UserName=userEdit.UserName;
            user.PhoneNumber=userEdit.PhoneNamber;
            var result = _userManager.UpdateAsync(user).Result;
            if (result.Succeeded)
            {
                return RedirectToAction("Index", "users", new { area = "Admin" });

            }
            return View();



        }
        [HttpGet]
        public IActionResult Delete(string id)
        {
            var user = _userManager.FindByIdAsync(id).Result;

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
            var user = _userManager.FindByIdAsync(userDelete.Id).Result;

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
        public IActionResult Details(string id)
        {
            var user = _userManager.FindByIdAsync(id).Result;
            UserDetailDto userDetail = new UserDetailDto()
            {
                Id=user.Id,
                FullName=$"{user.FirstName} {user.LastName}",
                UserName=user.UserName,
                Email=user.Email,
                PhoneNumber=user.PhoneNumber
            };
            return View(userDetail);
        }
        //گرفتن نقش های کاربر
        [HttpGet]
        public IActionResult UserRole(string id)
        {
            var user=_userManager.FindByIdAsync(id).Result;

            var roles = _userManager.GetRolesAsync(user).Result;

            UserRoleDto addUserRole = new UserRoleDto()
            {
                Id=user.Id,
                FullName=$"{user.FirstName} {user.LastName}",
                RoleName=roles.ToList(),
            };

            return View(addUserRole);
           
        }
        /// <summary>
        /// افزودن نقش به کاربر 
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult AddUserRole(string Id)
        {
            var user = _userManager.FindByIdAsync(Id).Result;
            var roles = _RoleManager.Roles.Select(p => new SelectListItem
            {
                Text=p.Name,
                Value=p.Name
            }).ToList();
           
            AddUserRoleDto addUserRole = new AddUserRoleDto()
            {
                Id=user.Id,
                FullName=$"{user.FirstName} {user.LastName}",
                UserName = user.UserName,
                Roles=roles

            };
            return View(addUserRole);

        }
        [HttpPost]
        public IActionResult AddUserRole(AddUserRoleDto addUserRoleDto)
        {
            var user = _userManager.FindByIdAsync(addUserRoleDto.Id).Result;

          var res=  _userManager.IsInRoleAsync(user, addUserRoleDto.Role).Result;

            if (res==true) 
            {
                addUserRoleDto.Roles = _RoleManager.Roles.Select(p => new SelectListItem
                {
                    Text = p.Name,
                    Value = p.Name
                }).ToList();
                ModelState.AddModelError("", "این نقش از قبل به کاربر تخصیص داده شده است.");
                return View(addUserRoleDto);
            }
            var result=_userManager.AddToRoleAsync(user,addUserRoleDto.Role).Result;
            if (result.Succeeded) 
            {
                return RedirectToAction("index");
            }
           
            return View();

        }
        [HttpGet]
        public IActionResult DeleteUserRole(DeleteUserRoleDto deleteUserRole) 
        {
            var user = _userManager.FindByIdAsync(deleteUserRole.id).Result;
            var result = _userManager.RemoveFromRoleAsync(user, deleteUserRole.roleName).Result;
            if (result.Succeeded)
            {
                return RedirectToAction("UserRole", new { id = user.Id });
            }
            return View();
        }
       




    }
}
