using System.Threading.Tasks;
using Meters.inGaia.Challenge.API.Models;

namespace Meters.inGaia.Challenge.API.Services.PropertyService.Interfaces
{
    public interface IPropertyService
    {
        Task<MeterPrice> GetSquareMeterPrice();

        Task<Property> GetPropertyValue(string meters);
    }
}
