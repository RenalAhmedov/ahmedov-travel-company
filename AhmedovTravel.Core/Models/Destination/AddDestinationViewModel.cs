using AhmedovTravel.Core.Constants;
using AhmedovTravel.Infrastructure.DataConstants;
using System.ComponentModel.DataAnnotations;

namespace AhmedovTravel.Core.Models.Destination
{
    public class AddDestinationViewModel
    {
        [Required]
        [StringLength(DestinationConstants.TitleMaxLength, MinimumLength = DestinationConstants.TitleMinLength,
            ErrorMessage = "The Title field must be between 5 and 50 characters.")]
        [RegularExpression(ValidationRegex.PropertyRegex,
            ErrorMessage = "Contains unallowed characters")]
        public string Title { get; set; } = null!;

        [Required]
        [StringLength(DestinationConstants.TownNameMaxLength, MinimumLength = DestinationConstants.TownNameMinLength,
             ErrorMessage = "The Town field must be between 4 and 100 characters.")]
        [RegularExpression(ValidationRegex.PropertyRegex,
            ErrorMessage = "Contains unallowed characters")]
        public string Town { get; set; } = null!;

        [Required]
        [RegularExpression(ValidationRegex.DescriptionAndMessageRegex,
            ErrorMessage = "Contains unallowed characters")]
        public string ImageUrl { get; set; } = null!;

        [Required]
        [Range(typeof(decimal), "1.0", "10.0", ConvertValueInInvariantCulture = true)]
        public decimal Rating { get; set; }

        [Required]
        [Range(DestinationConstants.PriceMinAmount, DestinationConstants.PriceMaxAmount, ConvertValueInInvariantCulture = true)]
        public decimal Price { get; set; }

    }
}
