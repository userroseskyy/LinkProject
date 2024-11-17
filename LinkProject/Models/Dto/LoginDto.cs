
using System.ComponentModel.DataAnnotations;

namespace LinkProject.Models.Dto
{
    public class LoginDto
    {
        [Required(ErrorMessage ="ایمیل را وارد کنید")] 
        public string userName { get; set; }
        [Required(ErrorMessage ="پسورد را وارد کنید")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Display(Name ="بخاطر داشته باش")]
        public bool IsPersistent {  get; set; }=false;
        public string ReturnUrl { get; set; }
    }
}
