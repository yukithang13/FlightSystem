using System.ComponentModel.DataAnnotations;

namespace FlightSystem.Model
{
    public class ForgotPasswordRequest
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
