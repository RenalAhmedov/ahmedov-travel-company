using Microsoft.AspNetCore.Identity;

namespace AhmedovTravel.Infrastructure.Data.Entities
{
    public class User : IdentityUser
    {
        public ICollection<UserDestination> UserDestinations { get; set; }
    }
}
