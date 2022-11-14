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
    internal class RoomTypeConfiguration : IEntityTypeConfiguration<RoomType>
    { 
        public void Configure(EntityTypeBuilder<RoomType> builder)
        {
            builder.HasData(new RoomType()
            {
                Id = 1,
                Name = "Single"
            },
            new RoomType()
            {
                Id = 2,
                Name = "Double"
            });
        }
    }
}
