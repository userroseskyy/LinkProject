
using LinkProject.Areas.Admin.Models.Dto.RoleDto;
using LinkProject.Models.Role;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LinkProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class RolesController : Controller
    {
       
        private readonly RoleManager<Role> _roleManager;
        public RolesController(RoleManager<Role> roleManager)
        {
         _roleManager=roleManager;
        }
        [HttpGet]
        public IActionResult Index()
        {
            var role=_roleManager.Roles.Select(p=> new RoleListDto
            {
                Id= p.Id,
                Name=p.Name,
                Description=p.Description,
            }).ToList();
            return View(role);
        }
        [HttpGet]
        public IActionResult Create() 
        { 
            
            return View();
        }
        [HttpPost]
        public IActionResult Create(AddNewRoleDto newRole) 
        {
            if (!ModelState.IsValid) 
            {
                return View(newRole);
            }
            Role role= new Role()
            {
                Name=newRole.Name,
                Description=newRole.Description,
            };
            var result=_roleManager.CreateAsync(role).Result;
            if (result.Succeeded) 
            { 
                return RedirectToAction("Index");
            }
          
            return View();
        }
        [HttpGet]
        public IActionResult Edit(string id) 
        {
            var role = _roleManager.FindByIdAsync(id).Result;
          EditRoleDto edit=new EditRoleDto()
            {
                Id=role.Id,
                Name=role.Name,
                Description=role.Description,
            };
            return View(edit);
        }
        [HttpPost]
        public IActionResult Edit(EditRoleDto editRole) 
        {
            var role=_roleManager.FindByIdAsync(editRole.Id).Result;

            role.Id=editRole.Id;
            role.Name=editRole.Name;
            role.Description=editRole.Description;

            var result=_roleManager.UpdateAsync(role).Result;
            if (result.Succeeded) 
            {
                return RedirectToAction("Index");
            }
            return View();
        }
        [HttpGet]
        public IActionResult Delete(string id) 
        {
           var role=_roleManager.FindByIdAsync(id).Result;
            DeleteRoleDto delete=new DeleteRoleDto()
            {
                Name=role.Name,
                Description=role.Description,   


            };
            return View(delete);
        }
        [HttpPost]
        public IActionResult Delete(DeleteRoleDto deleteRole)
        {
            var role =_roleManager.FindByIdAsync(deleteRole.Id).Result;
            var res=_roleManager.DeleteAsync(role).Result;
            if (res.Succeeded)
            {
                return RedirectToAction("Index");
            }
            return View(role);
        }

    }
}
