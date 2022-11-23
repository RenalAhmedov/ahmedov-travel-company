using AhmedovTravel.Infrastructure.DataConstants;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AhmedovTravel.Infrastructure.Data.Entities
{
    public class Destination
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(DestinationConstants.TitleMaxLength)]
        public string Title { get; set; } = null!;

        [Required]
        [MaxLength(DestinationConstants.TownNameMaxLength)]
        public string Town { get; set; } = null!;

        [Required]
        public string ImageUrl { get; set; } = null!;

        [Precision(18, 2)]
        public decimal Rating { get; set; }

        [Required]
        [Column(TypeName = "money")]
        [Precision(18, 2)]
        [Range(DestinationConstants.PriceMinAmount, DestinationConstants.PriceMaxAmount)]
        public decimal Price { get; set; }

        public int? HotelId { get; set; }

        [ForeignKey(nameof(HotelId))]
        public Hotel? Hotel { get; set; }

        public int? TransportId { get; set; }

        [ForeignKey(nameof(TransportId))]
        public Transport? Transport { get; set; }

        public ICollection<Hotel> Hotels { get; set; } = new List<Hotel>();

        public ICollection<UserDestination> UsersDestinations { get; set; } = new List<UserDestination>();

        public ICollection<User> UserChosenDestination { get; set; } = new List<User>();

        [Required]
        public bool IsActive { get; set; }

        public bool IsChosen { get; set; }
    }
}
