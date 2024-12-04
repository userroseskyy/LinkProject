using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel;

namespace LinkProject.Areas.Admin.Models.Dto.UserDto
{
    public class UserRoleDto
    {
        public string Id { get; set; }
        [DisplayName("نام و نام خانوادگی")]
        public string FullName { get; set; }
        [DisplayName("نقش ها")]
        public List<string> RoleName { get; set; }
     
        
    }
}
