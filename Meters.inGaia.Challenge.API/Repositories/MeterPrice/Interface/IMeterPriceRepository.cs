using System.Threading.Tasks;
using MeterPriceModel = Meters.inGaia.Challenge.API.Models.MeterPrice;

namespace Meters.inGaia.Challenge.API.Repositories.MeterPrice.Interface
{
    public interface IMeterPriceRepository
    {
        Task<MeterPriceModel> GetMeterPrice();
    }
}
