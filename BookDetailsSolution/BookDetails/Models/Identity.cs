using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BookDetails.Models
{
        public class AppDbContext : IdentityDbContext<IdentityUser>
        {
            public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        }
}
