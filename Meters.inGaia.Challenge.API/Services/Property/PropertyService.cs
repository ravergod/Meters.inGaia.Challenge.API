using Meters.inGaia.Challenge.API.Repositories.MeterPrice.Interface;
using Meters.inGaia.Challenge.API.Services.Property.Interface;
using Microsoft.Extensions.Logging;
using System;
using System.Globalization;
using System.Threading.Tasks;
using MeterPriceModel = Meters.inGaia.Challenge.API.Models.MeterPrice;
using PropertyModel = Meters.inGaia.Challenge.API.Models.Property;

namespace Meters.inGaia.Challenge.API.Services.Property
{
    public class PropertyService : IPropertyService
    {
        private readonly ILogger logger;
        private readonly IMeterPriceRepository meterPriceRepository;

        public PropertyService(
            ILogger<PropertyService> logger,
            IMeterPriceRepository meterPriceRepository)
        {
            this.logger = logger;
            this.meterPriceRepository = meterPriceRepository;
        }

        /// <summary>
        /// Get square meter price.
        /// </summary>
        /// <returns><see cref="MeterPriceModel"/>.</returns>
        public async Task<MeterPriceModel> GetSquareMeterPrice()
        {
            return await meterPriceRepository.GetMeterPrice();
        }

        /// <summary>
        /// Execute property value calculation based on the value of the square meter.
        /// </summary>
        /// <param name="meters"></param>
        /// <returns><see cref="Property"/>.</returns>
        public async Task<PropertyModel> GetPropertyValue(string meters)
        {
            var property = new PropertyModel();
            decimal decimalMeters;

            if (!decimal.TryParse(meters, NumberStyles.Any, CultureInfo.InvariantCulture, out decimalMeters)
                || string.IsNullOrWhiteSpace(meters)
                || meters.Contains(','))
            {
                property.SetError("Meters out of format");
                logger.LogError($"Meters out of format. String provided: {meters}");
                return property;
            }

            if (decimalMeters < 10 || decimalMeters > 10000)
            {
                property.SetError("Meters out of range. Minimun 10, maximum 10000.");
                logger.LogError($"Meters out of range. Meter(s) provided: {decimalMeters}");
                return property;
            }

            var squareMeterPrice = await GetSquareMeterPrice();

            if (squareMeterPrice == null)
            {
                return null;
            }

            var response = squareMeterPrice.Value * decimalMeters;

            property.SetPropertySize(Math.Round(decimalMeters, 2));
            property.SetValue(Math.Round(response, 2));

            return property;
        }
    }
}
