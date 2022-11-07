﻿using AhmedovTravel.Infrastructure.DataConstants;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AhmedovTravel.Infrastructure.Data.Entities
{
    public class Town
    {
        [Key]
        public Guid? Id { get; set; }

        [Required]
        [MaxLength(TownConstants.TownNameMaxLength)]
        public string Name { get; set; } = null!;

        [Required]
        [MaxLength(TownConstants.TownDescriptionMaxLength)]
        public string Description { get; set; } = null!;

        [Required]
        public Guid? HotelId { get; set; }

        [Required]
        [ForeignKey(nameof(HotelId))]
        public Hotel? Hotel { get; set; }

        public ICollection<Hotel> Hotels { get; set; }

        [Required]
        public bool isActive { get; set; }
    }
}
