using System;
using System.Globalization;
using System.Threading.Tasks;
using Meters.inGaia.Challenge.API.Models;
using Meters.inGaia.Challenge.API.Repositories.MeterPriceRepository.Interfaces;
using Meters.inGaia.Challenge.API.Services.PropertyService.Interfaces;
using Microsoft.Extensions.Logging;

namespace Meters.inGaia.Challenge.API.Services.PropertyService
{
    public class PropertyService : IPropertyService
    {
        private readonly ILogger<PropertyService> logger;
        private readonly IMeterPriceRepository meterPriceRepository;

        public PropertyService(IMeterPriceRepository meterPriceRepository)
        {
            this.meterPriceRepository = meterPriceRepository;
        }

        public async Task<MeterPrice> GetSquareMeterPrice()
        {
            return await meterPriceRepository.GetMeterPrice();
        }

        public async Task<Property> GetPropertyValue(string meters)
        {
            decimal decimalMeters;

            if (!decimal.TryParse(meters, NumberStyles.Any, CultureInfo.InvariantCulture, out decimalMeters) 
                || string.IsNullOrWhiteSpace(meters)
                || meters.Contains(','))
            {
                return new Property { Error = "Meters out of format"};
            }

            if (decimalMeters < 10 || decimalMeters > 10000)
            {
                return new Property { Error = "Meters out of range. Minimun 10, maximum 10000." };
            }

            var squareMeterPrice = await GetSquareMeterPrice();

            if (squareMeterPrice == null)
            {
                return null;
            }

            var response = squareMeterPrice.Value * decimalMeters;

            return new Property
            {
                PropertySizeInSquareMeters = Math.Round(decimalMeters, 2),
                Value = response
            };
        }
    }
}
