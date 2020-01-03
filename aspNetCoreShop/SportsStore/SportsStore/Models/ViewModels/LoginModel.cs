using System.ComponentModel.DataAnnotations;

namespace SportsStore.Models.ViewModels
{
    //whenever a user requests functionality with authorized access
    //identity redirects him to /Account/Login page to confirm authentication
    public class LoginModel
    {
        [Required] public string Name { get; set; }
        
        [Required]
        [UIHint("password")]
        public string Password { get; set; }

        public string ReturnUrl { get; set; } = "/";
    }
}