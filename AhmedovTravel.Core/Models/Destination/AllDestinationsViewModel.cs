namespace AhmedovTravel.Core.Models.Destination
{
    public class AllDestinationsViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; } = null!;

        public string Town { get; set; } = null!;

        public decimal Price { get; set; } 

        public string ImageUrl { get; set; } = null!;

        public decimal Rating { get; set; } 
    }
}
