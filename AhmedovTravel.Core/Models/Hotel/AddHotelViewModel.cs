using AhmedovTravel.Core.Constants;
using AhmedovTravel.Infrastructure.DataConstants;
using System.ComponentModel.DataAnnotations;

namespace AhmedovTravel.Core.Models.Hotel
{
    public class AddHotelViewModel
    {
        [Required]
        [StringLength(HotelConstants.HotelNameMaxLength, MinimumLength = HotelConstants.HotelNameMinLength,
             ErrorMessage = "The Name field must be between 1 and 85 characters.")]
        [RegularExpression(ValidationRegex.PropertyRegex,
            ErrorMessage = "Contains unallowed characters")]
        public string Name { get; set; } = null!;

        [Required]
        [StringLength(HotelConstants.HotelDescriptionMaxLength, MinimumLength = HotelConstants.HotelDescriptionMinLength,
             ErrorMessage = "The Description field must be between 20 and 1000 characters.")]
        [RegularExpression(ValidationRegex.DescriptionAndMessageRegex,
            ErrorMessage = "Contains unallowed characters")]
        public string Description { get; set; } = null!;

        [Required]
        [RegularExpression(ValidationRegex.DescriptionAndMessageRegex,
            ErrorMessage = "Contains unallowed characters")]
        public string ImageUrl { get; set; } = null!;

        [Required]
        [Range(typeof(decimal), "1.0", "10.0", ConvertValueInInvariantCulture = true)]
        public decimal HotelRating { get; set; }

    }
}
