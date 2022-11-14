using AhmedovTravel.Infrastructure.DataConstants;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AhmedovTravel.Infrastructure.Data.Entities
{
    public class RoomType
    {
        public int? Id { get; set; }


        [Required]
        [MaxLength(RoomTypeConstants.RoomTypeMaxLength)]
        public string Name { get; set; }
    }
}
