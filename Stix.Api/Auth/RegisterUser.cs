using System.ComponentModel.DataAnnotations;

namespace Stix.Api.Auth
{
    public class RegisterUser
    {
        [Required]
        [EmailAddress]
        public string EmailAddress { get; set; }

        [Required]
        [MinLength(8)]
        [MaxLength(30)]
        public string Password { get; set; }
    }
}
