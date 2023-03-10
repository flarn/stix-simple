using System.ComponentModel.DataAnnotations;

namespace Stix.Api.Auth
{
    public class SetRole
    {
        [Required]
        [EmailAddress]
        public string EmailAddress { get; set; }

        [Required]
        public string Role { get; set; }
    }
}
