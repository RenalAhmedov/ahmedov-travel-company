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

            builder.Property(u => u.UserName)
              .HasMaxLength(UserConstants.UserNameMaxLength);

            builder.Property(u => u.Email)
                .HasMaxLength(UserConstants.EmailMaxLength);

        }

        
    }
}
