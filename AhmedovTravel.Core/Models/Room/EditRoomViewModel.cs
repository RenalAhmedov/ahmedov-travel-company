﻿using AhmedovTravel.Core.Constants;
using AhmedovTravel.Infrastructure.Data.Entities;
using System.ComponentModel.DataAnnotations;

namespace AhmedovTravel.Core.Models.Room
{
    public class EditRoomViewModel
    {
        public int Id { get; set; }

        [Required]
        public int Persons { get; set; }

        [Required]
        [RegularExpression(ValidationRegex.DescriptionAndMessageRegex,
            ErrorMessage = "Contains unallowed characters")]
        public string ImageUrl { get; set; }

        [Required]
        [Range(typeof(decimal), "15.00", "200.0", ConvertValueInInvariantCulture = true)]
        public decimal PricePerNight { get; set; }

        public int RoomTypeId { get; set; }

        public IEnumerable<RoomType> RoomTypes { get; set; } = new List<RoomType>();
    }
}
