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

            builder.Property(u => u.UserName)
              .HasMaxLength(UserConstants.UserNameMaxLength);

            builder.Property(u => u.Email)
                .HasMaxLength(UserConstants.EmailMaxLength);

            //builder.HasData(CreateUsers());
        }

        
    }
}
