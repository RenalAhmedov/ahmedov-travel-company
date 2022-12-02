namespace AhmedovTravel.Core.Models.Room
{
    public class RoomViewModel
    {
        public int Id { get; set; }

        public int Persons { get; set; }

        public string ImageUrl { get; set; }

        public decimal PricePerNight { get; set; }

        public string? RoomType { get; set; }
    }
}
