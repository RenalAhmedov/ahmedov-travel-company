using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AhmedovTravel.Infrastructure.Data.Entities
{
    public class UserDestination  
    {
        [Required]
        public string UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        public User User { get; set; }

        [Required]
        public Guid DestinationId { get; set; }

        [ForeignKey(nameof(DestinationId))]
        public Destination Destination { get; set; }

        [Required]
        public bool IsActive { get; set; }

    }
}
