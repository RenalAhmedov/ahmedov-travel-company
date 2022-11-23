using AhmedovTravel.Infrastructure.DataConstants;
using System.ComponentModel.DataAnnotations;

namespace AhmedovTravel.Infrastructure.Data.Entities
{
    public class Transport
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(TransportConstants.TransportNameMaxLength)]
        public string TransportType { get; set; } = null!;

        [Required]
        public string ImageUrl { get; set; } = null!;
    }
}
