using AhmedovTravel.Infrastructure.DataConstants;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AhmedovTravel.Infrastructure.Data.Entities
{
    public class User : IdentityUser
    {
        public int? DestinationId { get; set; }

        [ForeignKey(nameof(DestinationId))]
        public Destination Destination { get; set; }

        public ICollection<UserDestination> UsersDestinations { get; set; } = new List<UserDestination>();

        public ICollection<Town> UserTowns { get; set; } = new List<Town>();

        public ICollection<Hotel> UserHotels { get; set; } = new List<Hotel>();

        public ICollection<Room> UserRooms { get; set; } = new List<Room>();

        [Required]
        public bool IsActive { get; set; }
    }
}
