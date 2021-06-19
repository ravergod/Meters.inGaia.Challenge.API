using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Meters.inGaia.Challenge.API.Models;

namespace Meters.inGaia.Challenge.API.Repositories.MeterPriceRepository.Interfaces
{
    public interface IMeterPriceRepository
    {
        Task<MeterPrice> GetMeterPrice();
    }
}
