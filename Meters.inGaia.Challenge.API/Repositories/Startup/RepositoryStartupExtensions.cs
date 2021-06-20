using Meters.inGaia.Challenge.API.Repositories.MeterPrice;
using Meters.inGaia.Challenge.API.Repositories.MeterPrice.Interface;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Meters.inGaia.Challenge.API.Repositories.Startup
{
    public static class RepositoryStartupExtensions
    {
        public static void AddRepositories(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IMeterPriceRepository, MeterPriceRepository>();
        }
    }
}
