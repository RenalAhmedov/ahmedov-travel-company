using AhmedovTravel.Infrastructure.Data.Entities.Enums;
using AhmedovTravel.Infrastructure.DataConstants;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AhmedovTravel.Infrastructure.Data.Entities
{
    public class Destination
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(DestinationConstants.TitleMaxLength)]
        public string Title { get; set; } = null!;

        [Required]
        public string ImageUrl { get; set; } = null!;

        public RatingEnum Rating { get; set; }

        [Required]
        [Range(DestinationConstants.PriceMinAmount, DestinationConstants.PriceMaxAmount)]
        public decimal Price { get; set; }

        public TransportEnum? TransportType { get; set; }

        [Required]
        public Guid? TownId { get; set; }

        [Required]
        [ForeignKey(nameof(TownId))]
        public Town? Town { get; set; }

        public ICollection<UserDestination> UserDestinations { get; set; }

        [Required]
        public bool IsActive { get; set; }

        public bool isChosen { get; set; }
    }
}
