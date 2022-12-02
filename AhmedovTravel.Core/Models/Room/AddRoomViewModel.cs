using AhmedovTravel.Infrastructure.Data.Entities;
using AhmedovTravel.Infrastructure.DataConstants;
using System.ComponentModel.DataAnnotations;

namespace AhmedovTravel.Core.Models.Room
{
    public class AddRoomViewModel
    {
        [Required]
        [MaxLength(RoomConstants.RoomPersonsMaxLength), MinLength(RoomConstants.RoomPersonsMinLength)]
        public int Persons { get; set; }

        [Required]
        public string ImageUrl { get; set; }

        [Required]
        //[Range(typeof(decimal), "0.0", "10.0", ConvertValueInInvariantCulture = true)]
        public decimal PricePerNight { get; set; }

        public int RoomTypeId { get; set; }

        public IEnumerable<RoomType> RoomTypes { get; set; } = new List<RoomType>();
    }
}
