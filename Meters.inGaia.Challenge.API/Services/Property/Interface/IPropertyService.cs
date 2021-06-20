using System.Threading.Tasks;
using Meters.inGaia.Challenge.API.Models;
using PropertyModel = Meters.inGaia.Challenge.API.Models.Property;

namespace Meters.inGaia.Challenge.API.Services.Property.Interface
{
    public interface IPropertyService
    {
        Task<MeterPrice> GetSquareMeterPrice();

        Task<PropertyModel> GetPropertyValue(string meters);
    }
}
