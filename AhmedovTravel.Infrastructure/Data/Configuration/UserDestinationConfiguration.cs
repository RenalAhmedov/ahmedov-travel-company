using AhmedovTravel.Infrastructure.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AhmedovTravel.Infrastructure.Data.Configuration
{
    internal class UserDestinationConfiguration : IEntityTypeConfiguration<UserDestination>
    {
        public void Configure(EntityTypeBuilder<UserDestination> builder)
        {
            builder.HasKey(ud => new { ud.UserId, ud.DestinationId });

            builder.HasOne(u => u.Destination)
                .WithMany(ud => ud.UsersDestinations)
                .HasForeignKey(e => e.DestinationId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(ud => ud.Destination)
                .WithMany(t => t.UsersDestinations)
                .HasForeignKey(di => di.DestinationId)
                .OnDelete(DeleteBehavior.Restrict);

            //UserDestination Foreign keys
            builder.Property(di => di.DestinationId)
          .IsRequired(false);

            builder.Property(ui => ui.UserId)
                .IsRequired(false);

            //IsActive default value for all entities
            builder.Property(c => c.IsActive)
                .HasDefaultValue(true);
        }
    }
}
