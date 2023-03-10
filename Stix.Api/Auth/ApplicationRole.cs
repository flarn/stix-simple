using Microsoft.AspNetCore.Identity;

namespace Stix.Api.Auth
{
    public class ApplicationRole : IdentityRole<Guid>
    {
        public ApplicationRole(string name)
        {
            Name = name;
        }
    }
}
