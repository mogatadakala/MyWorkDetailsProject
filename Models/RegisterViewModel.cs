using System.ComponentModel.DataAnnotations;

namespace MyWorkDetailsProject.Models
{
    public class RegisterViewModel
    {
        [Key]
        [Required]
        [Display(Name = "User Name")]
        public string? UserID { get; set; }
        [Required(ErrorMessage = "Please Enter First Name..")]
        [Display(Name = "First Name")]
        public string? FName { get; set; }
        [Required(ErrorMessage = "Please Enter Last Name..")]
        [Display(Name = "Last Name")]
        public string? LName { get; set; }
        [Required(ErrorMessage = "Please Enter Gender..")]
        public string? Gender { get; set; }
        [Required(ErrorMessage = "Please Enter Email Id..")]
        public string? Email { get; set; }
        [Required(ErrorMessage = "Please Enter Mobile Number..")]
        public string? Phone { get; set; }
        [Required(ErrorMessage = "Please Enter City..")]
        public string? City { get; set; }
        [Required(ErrorMessage = "Please Enter Country..")]
        public string? Country { get; set; }
        [Required(ErrorMessage = "Please Enter Password...")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string? Password { get; set; }
        [Required(ErrorMessage = "Please Enter the Confirm Password...")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        [Compare("Password")]
        public string? ConfirmPassword { get; set; }
    }
}
