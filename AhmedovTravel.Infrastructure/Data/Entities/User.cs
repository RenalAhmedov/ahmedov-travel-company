using AhmedovTravel.Infrastructure.DataConstants;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net.Sockets;

namespace AhmedovTravel.Infrastructure.Data.Entities
{
    public class User : IdentityUser
    {
        [Required]
        [MaxLength(UserConstants.FirstNameMaxLength)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(UserConstants.LastNameMaxLength)]
        public string LastName { get; set; }

        public Guid? DestinationId { get; set; }

        [ForeignKey(nameof(DestinationId))]
        public Destination? Destination { get; set; }

        public ICollection<UserDestination> UsersDestinations { get; set; } = new List<UserDestination>();

        //[InverseProperty("")]
        public ICollection<Town> UserTowns { get; set; } = new List<Town>();

        //[InverseProperty("")]
        public ICollection<Hotel> UserHotels { get; set; } = new List<Hotel>();

        //[InverseProperty("")]
        public ICollection<Room> UserRooms { get; set; } = new List<Room>();

        [Required]
        public bool IsActive { get; set; }
    }
}
