using System;
using System.Globalization;
using System.Threading.Tasks;
using Meters.inGaia.Challenge.API.Core.Resources;
using Meters.inGaia.Challenge.API.Repositories.MeterPrice.Interface;
using Meters.inGaia.Challenge.API.Services.Property.Interface;
using Microsoft.Extensions.Logging;
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

            var error = ValidateRequest(meters);

            if (!string.IsNullOrEmpty(error))
            {
                property.SetError(error);
                return property;
            }

            decimal decimalMeters = decimal.Parse(meters);

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

        private string ValidateRequest(string meters)
        {
            decimal decimalMeters;

            if (!decimal.TryParse(meters, NumberStyles.Any, CultureInfo.InvariantCulture, out decimalMeters)
                || string.IsNullOrWhiteSpace(meters)
                || meters.Contains(','))
            {
                logger.LogWarning(string.Format(BusinessMessage.MetersOutOfFormat, meters));
                return string.Format(BusinessMessage.MetersOutOfFormat, meters);
            }

            if (decimalMeters < 10 || decimalMeters > 10000)
            {
                logger.LogWarning(string.Format(BusinessMessage.MetersOutOfRange, meters));
                return string.Format(BusinessMessage.MetersOutOfRange, meters);
            }

            return null;
        }
    }
}