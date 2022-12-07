using AhmedovTravel.Infrastructure.DataConstants;
using System.ComponentModel.DataAnnotations;

namespace AhmedovTravel.Core.Models.Hotel
{
    public class AddHotelViewModel
    {
        [Required]
        [StringLength(HotelConstants.HotelNameMaxLength), MinLength(HotelConstants.HotelNameMinLength)]
        public string Name { get; set; }

        [Required]
        [StringLength(HotelConstants.HotelDescriptionMaxLength), MinLength(HotelConstants.HotelDescriptionMinLength)]
        public string Description { get; set; }

        [Required]
        public string ImageUrl { get; set; } = null!;

        [Required]
        [Range(typeof(decimal), "0.0", "10.0", ConvertValueInInvariantCulture = true)]
        public decimal HotelRating { get; set; }

    }
}
