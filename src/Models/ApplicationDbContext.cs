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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<BookingBeestje>().HasKey(sc => new { sc.BookingId, sc.BeestjeId});
        }


        public DbSet<Accessoire> Accessoires { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Beestje> Beestjes { get; set; }

        public DbSet<BookingBeestje> BookinmBeestjes { get; set; }

    }
}
