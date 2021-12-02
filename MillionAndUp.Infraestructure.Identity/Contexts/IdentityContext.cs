using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MillionAndUp.Infraestructure.Identity.Models;

namespace MillionAndUp.Infraestructure.Identity.Contexts
{
    public class IdentityContext: IdentityDbContext<ApplicationUser>
    {
        public IdentityContext(DbContextOptions<IdentityContext> options): base (options)
        {

        }
    }
}
