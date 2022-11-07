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

 
            //Delete behaviour setup
            builder.Entity<UserDestination>()
                .HasOne(ud => ud.User)
                .WithMany(u => u.UserDestinations)
                .HasForeignKey(ui => ui.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<UserDestination>()
                .HasOne(ud => ud.Destination)
                .WithMany(t => t.UsersDestinations)
                .HasForeignKey(di => di.DestinationId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Town>()
                .HasOne(t => t.Hotel)
                .WithMany(t => t.TownHotels)
                .HasForeignKey(t => t.HotelId)
                .OnDelete(DeleteBehavior.Restrict);






            base.OnModelCreating(builder);
        }
    }
}