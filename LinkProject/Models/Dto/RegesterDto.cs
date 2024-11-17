using System.ComponentModel.DataAnnotations;

namespace LinkProject.Models.Dto
{
    public class RegesterDto
    {
        [Required(ErrorMessage = "نام را وارد کنید")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "نام خانوادگی را وارد کنید")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "ایمیل را وارد کنید")]
        [EmailAddress]
        public string Email { get; set; }
        [Required(ErrorMessage = "پسورد را وارد کنید")]
        [DataType(DataType.Password)]
        public string Password {  get; set; }
        [Required(ErrorMessage = "پسورد را وارد کنید")]
        [DataType(DataType.Password)]
        [Compare(nameof(Password))]
        public string ConfirmPassword   { get; set; }
    }
}
