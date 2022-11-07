using AhmedovTravel.Infrastructure.Data.Entities.Enums;
using AhmedovTravel.Infrastructure.DataConstants;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AhmedovTravel.Infrastructure.Data.Entities
{
    public class Hotel
    {
        [Key]
        public Guid? Id { get; set; }

        [Required]
        [MaxLength(HotelConstants.HotelNameMaxLength)]
        public string Name { get; set; } = null!;

        [MaxLength(HotelConstants.HotelDescriptionMaxLength)]
        public string Description { get; set; }

        public RatingEnum HotelRating { get; set; }

        [Required]
        public Guid? RoomId { get; set; }

        [Required]
        [ForeignKey(nameof(RoomId))]
        public Room? Room { get; set; }

        public ICollection<Room> Rooms { get; set; }
        public ICollection<Town> TownHotels { get; set; }

        [Required]
        public bool IsActive { get; set; }
    }
}
