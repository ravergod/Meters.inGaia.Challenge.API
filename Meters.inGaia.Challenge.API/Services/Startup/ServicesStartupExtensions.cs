using Meters.inGaia.Challenge.API.Services.Property;
using Meters.inGaia.Challenge.API.Services.Property.Interface;
using Microsoft.Extensions.DependencyInjection;

namespace Meters.inGaia.Challenge.API.Services.Startup
{
    public static class ServicesStartupExtensions
    {
        public static void AddApplicationServices(this IServiceCollection services)
        {
            services.AddSingleton<IPropertyService, PropertyService>();
        }
    }
}
