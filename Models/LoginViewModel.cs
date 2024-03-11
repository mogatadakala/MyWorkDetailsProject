using System.ComponentModel.DataAnnotations;

namespace MyWorkDetailsProject.Models
{
    public class LoginViewModel
    {
        [Required]
        public string UserID { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
