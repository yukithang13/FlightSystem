using System.ComponentModel.DataAnnotations;

namespace FlightSystem.Model
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "User Name is required")]
        public string UserName { get; set; } = null!;

        [Required, EmailAddress]
        public string Email { get; set; } = null!;

        [Required(ErrorMessage = "Permission is required")]
        public string Permission { get; set; } = null!;

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; } = null!;


    }
}
