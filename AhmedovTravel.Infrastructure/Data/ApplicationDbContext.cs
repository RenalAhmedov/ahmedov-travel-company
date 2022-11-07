using AhmedovTravel.Infrastructure.Data.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AhmedovTravel.Infrastrucutre.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Destination> Destinations { get; set; }
        public DbSet<Hotel> Hotels { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Town> Towns { get; set; }
        public DbSet<UserDestination> UsersDestinations { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<UserDestination>()
                  .HasKey(ud => new { ud.UserId, ud.DestinationId });

            builder.Entity<User>()
                .Property(u => u.UserName)
                .HasMaxLength(20)
                .IsRequired();

            builder.Entity<User>()
               .Property(u => u.Email)
               .HasMaxLength(60)
               .IsRequired();









            base.OnModelCreating(builder);
        }
    }
}