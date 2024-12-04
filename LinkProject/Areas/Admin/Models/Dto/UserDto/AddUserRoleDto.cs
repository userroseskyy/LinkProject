using Microsoft.AspNetCore.Mvc.Rendering;

namespace LinkProject.Areas.Admin.Models.Dto.UserDto
{
    public class AddUserRoleDto
    {
        public string Id { get; set; }
        public string FullName { get; set; }
        public string UserName { get; set; }
        public string Role { get; set; }
        public List<SelectListItem> Roles { get; set; }
    }
}
