using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace AhmedovTravel.Infrastructure.Data.Entities
{
    public class User : IdentityUser
    {
        public ICollection<UserDestination> UsersDestinations { get; set; } = new List<UserDestination>();
        public ICollection<Hotel> UserHotels { get; set; } = new List<Hotel>();
        public ICollection<Room> UserRooms { get; set; } = new List<Room>();
        public ICollection<Transport> UserTransport { get; set; } = new List<Transport>();

        [Required]
        public bool IsActive { get; set; }
    }
}
