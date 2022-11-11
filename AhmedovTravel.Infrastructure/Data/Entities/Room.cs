using AhmedovTravel.Infrastructure.Data.Entities.Enums;
using AhmedovTravel.Infrastructure.DataConstants;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AhmedovTravel.Infrastructure.Data.Entities
{
    public class Room
    {
        [Key]
        public Guid? Id { get; set; }

        [Required]
        public RoomTypeEnum? RoomType { get; set; }

        [Required]
        [MaxLength(RoomConstants.RoomPersonsMaxLength)]
        public int Persons { get; set; }

        [Required]
        [Column(TypeName = "money")]
        [Precision(18,2)]
        public decimal PricePerNight { get; set; }

        public ICollection<Hotel> HotelRooms { get; set; } = new List<Hotel>();

        [Required]
        public bool IsActive { get; set; }
    }
}
