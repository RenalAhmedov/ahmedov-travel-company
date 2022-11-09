using AhmedovTravel.Infrastructure.Data.Configuration;
using AhmedovTravel.Infrastructure.Data.Entities;
using AhmedovTravel.Infrastructure.DataConstants;
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
        public DbSet<User> Users { get; set; }
        public DbSet<Destination> Destinations { get; set; }
        public DbSet<Hotel> Hotels { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Town> Towns { get; set; }
        public DbSet<UserDestination> UsersDestinations { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new UserConfiguration());
            builder.ApplyConfiguration(new DestinationConfiguration());
            builder.ApplyConfiguration(new TownConfiguration());
            builder.ApplyConfiguration(new HotelConfiguration());
            builder.ApplyConfiguration(new RoomConfiguration());
            builder.ApplyConfiguration(new UserDestinationConfiguration());

            builder.Entity<UserDestination>()
                  .HasKey(ud => new { ud.UserId, ud.DestinationId });

            //Delete behaviour setup
            builder.Entity<UserDestination>()
                .HasOne(ud => ud.User)
                .WithMany(u => u.UsersDestinations)
                .HasForeignKey(ui => ui.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<UserDestination>()
                .HasOne(u => u.Destination)
                .WithMany(ud => ud.UsersDestinations)
                .HasForeignKey(di => di.DestinationId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<User>()
                .Property(e => e.DestinationId)
                .IsRequired(false);

            builder.Entity<User>()
                .HasOne(u => u.Destination)
                .WithMany(ud => ud.UserChosenDestination)
                .HasForeignKey(e => e.DestinationId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Town>()
                .HasOne(t => t.Hotel)
                .WithMany(t => t.TownHotels)
                .HasForeignKey(t => t.HotelId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Hotel>()
                .HasOne(t => t.Room)
                .WithMany(hr => hr.HotelRooms)
                .HasForeignKey(t => t.RoomId)
                .OnDelete(DeleteBehavior.Restrict);

            //UserDestination Foreign keys
            builder.Entity<UserDestination>()
                .Property(di => di.DestinationId)
                .IsRequired(false);

            builder.Entity<UserDestination>()
                .Property(ui => ui.UserId)
                .IsRequired(false);


            //ASP.NET Identity User properties setup
            builder.Entity<User>()
                .Property(e => e.UserName)
                .HasMaxLength(UserConstants.UserNameMaxLength);

            builder.Entity<User>()
                .Property(e => e.Email)
                .HasMaxLength(UserConstants.EmailMaxLength);

            //IsActive default value for all entities
            builder.Entity<UserDestination>()
                .Property(c => c.IsActive)
                .HasDefaultValue(true);

            builder.Entity<Destination>()
                .Property(c => c.IsActive)
                .HasDefaultValue(true);

            builder.Entity<Town>()
                .Property(c => c.IsActive)
                .HasDefaultValue(true);

            builder.Entity<Hotel>()
                .Property(c => c.IsActive)
                .HasDefaultValue(true);

            builder.Entity<Room>()
                .Property(c => c.IsActive)
                .HasDefaultValue(true);

            builder.Entity<User>()
                .Property(c => c.IsActive)
                .HasDefaultValue(true);

            base.OnModelCreating(builder);
        }
    }
}