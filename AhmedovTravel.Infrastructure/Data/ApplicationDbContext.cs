using AhmedovTravel.Infrastructure.Data.Configuration;
using AhmedovTravel.Infrastructure.Data.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AhmedovTravel.Infrastrucutre.Data
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Destination> Destinations { get; set; }
        public DbSet<Hotel> Hotels { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<RoomType> RoomTypes { get; set; }
        public DbSet<Town> Towns { get; set; }
        public DbSet<UserDestination> UsersDestinations { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new UserDestinationConfiguration());
            builder.ApplyConfiguration(new UserConfiguration());
            builder.ApplyConfiguration(new DestinationConfiguration());
            builder.ApplyConfiguration(new TownConfiguration());
            builder.ApplyConfiguration(new HotelConfiguration());
            builder.ApplyConfiguration(new RoomConfiguration());
            builder.ApplyConfiguration(new RoomTypeConfiguration());

            base.OnModelCreating(builder);
        }
    }
}