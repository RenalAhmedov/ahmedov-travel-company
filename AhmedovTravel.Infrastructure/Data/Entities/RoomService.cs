using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using AhmedovTravel.Infrastructure.DataConstants;

namespace AhmedovTravel.Infrastructure.Data.Entities
{
    public class RoomService
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [Column(TypeName = "money")]
        [Precision(18, 2)]
        public decimal Price { get; set; }

        [Required]
        [MaxLength(RoomServiceConstants.RoomServiceDescriptionMaxLength)]
        public string Description { get; set; }

        [Required]
        public string ImageUrl { get; set; }


    }
}
