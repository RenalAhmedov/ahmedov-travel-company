using AhmedovTravel.Core.Contracts;
using AhmedovTravel.Core.Services;
using AhmedovTravel.Infrastructure.Data.Common;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class AhmedovTravelServiceCollectionExtension
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IRepository, Repository>();
            services.AddScoped<IDestinationService, DestinationService>();
            services.AddScoped<ITransportService, TransportService>();
            services.AddScoped<IHotelService, HotelService>();

            return services;
        }
    }
}
