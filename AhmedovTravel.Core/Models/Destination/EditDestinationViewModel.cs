using AhmedovTravel.Core.Constants;
using AhmedovTravel.Infrastructure.DataConstants;
using System.ComponentModel.DataAnnotations;

namespace AhmedovTravel.Core.Models.Destination
{
    public class EditDestinationViewModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(DestinationConstants.TitleMaxLength), MinLength(DestinationConstants.TitleMinLength)]
        [RegularExpression(ValidationRegex.PropertyRegex,
            ErrorMessage = "Contains unallowed characters")]
        public string Title { get; set; }

        [Required]
        [StringLength(DestinationConstants.TownNameMaxLength), MinLength(DestinationConstants.TownNameMinLength)]
        [RegularExpression(ValidationRegex.PropertyRegex,
            ErrorMessage = "Contains unallowed characters")]
        public string Town { get; set; }

        [RegularExpression(ValidationRegex.DescriptionAndMessageRegex,
            ErrorMessage = "Contains unallowed characters")]
        public string ImageUrl { get; set; }

        [Required]
        [Range(typeof(decimal), "0.0", "10.0", ConvertValueInInvariantCulture = true)]
        public decimal Rating { get; set; }

        [Required]
        [Range(DestinationConstants.PriceMinAmount, DestinationConstants.PriceMaxAmount, ConvertValueInInvariantCulture = true)]
        public decimal Price { get; set; }

    }
}
