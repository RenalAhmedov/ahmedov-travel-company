using AhmedovTravel.Infrastructure.Data.Entities;
using AhmedovTravel.Infrastructure.DataConstants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AhmedovTravel.Infrastructure.Data.Configuration
{
    internal class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(u => u.IsActive)
              .HasDefaultValue(true);

            builder.Property(e => e.DestinationId)
               .IsRequired(false);

            builder.HasOne(u => u.Destination)
                .WithMany(ud => ud.UserChosenDestination)
                .HasForeignKey(e => e.DestinationId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Property(u => u.FirstName)
               .HasMaxLength(UserConstants.FirstNameMaxLength);

            builder.Property(u => u.LastName)
               .HasMaxLength(UserConstants.LastNameMaxLength);

            builder.Property(u => u.UserName)
              .HasMaxLength(UserConstants.UserNameMaxLength);

            builder.Property(u => u.Email)
                .HasMaxLength(UserConstants.EmailMaxLength);

            //builder.HasData(CreateUsers());
        }

        //private List<IdentityUser> CreateUsers()
        //{
        //    var users = new List<IdentityUser>();
        //    var hasher = new PasswordHasher<IdentityUser>();

        //    var user = new IdentityUser()
        //    {
        //        Id = "dea12856-c198-4129-b3f3-b893d8395082",
        //        UserName = "agent@mail.com",
        //        NormalizedUserName = "agent@mail.com",
        //        Email = "agent@mail.com",
        //        NormalizedEmail = "agent@mail.com"
        //    };

        //    user.PasswordHash =
        //         hasher.HashPassword(user, "agent123");

        //    users.Add(user);

        //    user = new IdentityUser()
        //    {
        //        Id = "6d5800ce-d726-4fc8-83d9-d6b3ac1f591e",
        //        UserName = "guest@mail.com",
        //        NormalizedUserName = "guest@mail.com",
        //        Email = "guest@mail.com",
        //        NormalizedEmail = "guest@mail.com"
        //    };

        //    user.PasswordHash =
        //    hasher.HashPassword(user, "guest123");

        //    users.Add(user);

        //    return users;
        //}
    }
}
