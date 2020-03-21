using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Models
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public ApplicationDbContext()
        {
            
        }


        public DbSet<Accessoire> Accessoires { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Beestje> Beestjes { get; set; }


    }
}
