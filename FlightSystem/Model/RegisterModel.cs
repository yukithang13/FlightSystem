using System.ComponentModel.DataAnnotations;

namespace FlightSystem.Model
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "User Name is required")]
        public string UserName { get; set; } = null!;

        [Required, EmailAddress]
        public string Email { get; set; } = null!;

        public string Permission { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; } = null!;


    }
}
