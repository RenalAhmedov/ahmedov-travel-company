using AhmedovTravel.Infrastructure.DataConstants;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AhmedovTravel.Core.Models.Hotel
{
    public class EditHotelViewModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(HotelConstants.HotelNameMaxLength), MinLength(HotelConstants.HotelNameMinLength)]
        public string Name { get; set; }

        [Required]
        [StringLength(HotelConstants.HotelDescriptionMaxLength), MinLength(HotelConstants.HotelDescriptionMinLength)]
        public string Description { get; set; }

        [Required]
        public string ImageUrl { get; set; } = null!;

        [Required]
        //[Range(typeof(decimal), "0.0", "10.0", ConvertValueInInvariantCulture = true)]
        public decimal HotelRating { get; set; }
    }
}
