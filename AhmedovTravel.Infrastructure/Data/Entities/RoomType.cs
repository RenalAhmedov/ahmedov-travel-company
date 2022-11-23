using AhmedovTravel.Infrastructure.DataConstants;
using System.ComponentModel.DataAnnotations;

namespace AhmedovTravel.Infrastructure.Data.Entities
{
    public class RoomType
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(RoomTypeConstants.RoomTypeMaxLength)]
        public string Name { get; set; }
    }
}
