using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Stix.Api.Auth
{
    public class AuthDdbContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>
    {
        public AuthDdbContext(DbContextOptions<AuthDdbContext> options) : base(options) { }
    }
}
