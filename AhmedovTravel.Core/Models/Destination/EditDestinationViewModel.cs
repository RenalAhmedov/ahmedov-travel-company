using AhmedovTravel.Infrastructure.DataConstants;
using System.ComponentModel.DataAnnotations;

namespace AhmedovTravel.Core.Models.Destination
{
    public class EditDestinationViewModel
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [StringLength(DestinationConstants.TitleMaxLength), MinLength(DestinationConstants.TitleMinLength)]
        public string Title { get; set; }

        [Required]
        [StringLength(DestinationConstants.TownNameMaxLength), MinLength(DestinationConstants.TownNameMinLength)]
        public string Town { get; set; }

        [Required]
        public string ImageUrl { get; set; } = null!;

        [Required]
        [Range(typeof(decimal), "0.0", "10.0", ConvertValueInInvariantCulture = true)]
        public decimal Rating { get; set; }

        [Required]
        [Range(DestinationConstants.PriceMinAmount, DestinationConstants.PriceMaxAmount, ConvertValueInInvariantCulture = true)]
        public decimal Price { get; set; }

    }
}
