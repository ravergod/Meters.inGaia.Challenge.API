using System.Threading.Tasks;
using Meters.inGaia.Challenge.API.Models;
using Meters.inGaia.Challenge.API.Repositories.MeterPrice.Interface;
using Meters.inGaia.Challenge.API.Services.Property;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;
using PropertyModel = Meters.inGaia.Challenge.API.Models.Property;

namespace Meters.inGaia.Challenge.API.Unit.Tests.Services.Property
{
    public class PropertyServiceTest
    {
        private readonly Mock<ILogger<PropertyService>> mockLogger;
        private readonly Mock<IMeterPriceRepository> mockMeterPriceRepository;

        private PropertyService propertyService;

        public PropertyServiceTest()
        {
            mockLogger = new Mock<ILogger<PropertyService>>();
            mockMeterPriceRepository = new Mock<IMeterPriceRepository>();

            CreateService();
        }

        [Fact]
        public async void GetSquareMeterPrice_Successfully()
        {
            // Arrange
            var squareMeterPrice = new MeterPrice { Id = 1, MeterType = Models.Enums.MeterTypeEnum.M2, Value = 1 };
            mockMeterPriceRepository.Setup(_ => _.GetMeterPrice()).Returns(Task.FromResult(squareMeterPrice));

            // Act
            var result = await propertyService.GetSquareMeterPrice();

            // Assert
            Assert.Equal(squareMeterPrice, result);
            Assert.NotNull(squareMeterPrice);
        }

        [Fact]
        public async void GetSquareMeterPrice_When_Response_Is_Null()
        {
            // Arrange
            MeterPrice squareMeterPrice = null;
            mockMeterPriceRepository.Setup(_ => _.GetMeterPrice()).Returns(Task.FromResult(squareMeterPrice));

            // Act
            var result = await propertyService.GetSquareMeterPrice();

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async void GetPropertyValue_When_Meters_Format_Is_Wrong()
        {
            // Arrange
            string meters = "asd"; // No letters, no comma, no empty space, and between 10 and 10000.
            var squareMeterPrice = new MeterPrice { Id = 1, MeterType = Models.Enums.MeterTypeEnum.M2, Value = 1 };
            mockMeterPriceRepository.Setup(_ => _.GetMeterPrice()).Returns(Task.FromResult(squareMeterPrice));

            // Act
            var result = await propertyService.GetPropertyValue(meters);

            // Assert
            Assert.NotEmpty(result.Error);
        }

        [Fact]
        public async void GetPropertyValue_When_Meters_Format_Is_Out_Of_Range()
        {
            // Arrange
            string meters = "10001"; // No letters, no comma, no empty space, and between 10 and 10000.
            var squareMeterPrice = new MeterPrice { Id = 1, MeterType = Models.Enums.MeterTypeEnum.M2, Value = 1 };
            mockMeterPriceRepository.Setup(_ => _.GetMeterPrice()).Returns(Task.FromResult(squareMeterPrice));

            // Act
            var result = await propertyService.GetPropertyValue(meters);

            // Assert
            Assert.NotEmpty(result.Error);
        }

        [Fact]
        public async void GetPropertyValue_When_Meters_Format_Is_Ok()
        {
            // Arrange
            string meters = "5000";
            var propertyValue = new PropertyModel { PropertySizeInSquareMeters = 5000, Value = 5000 };
            var squareMeterPrice = new MeterPrice { Id = 1, MeterType = Models.Enums.MeterTypeEnum.M2, Value = 1 };
            mockMeterPriceRepository.Setup(_ => _.GetMeterPrice()).Returns(Task.FromResult(squareMeterPrice));

            // Act
            var result = await propertyService.GetPropertyValue(meters);

            // Assert
            Assert.NotNull(propertyValue);
            Assert.Equal(propertyValue.Value, result.Value);
            Assert.Equal(propertyValue.PropertySizeInSquareMeters, result.PropertySizeInSquareMeters);
        }

        private void CreateService()
        {
            propertyService = new PropertyService(
                    mockLogger.Object,
                    mockMeterPriceRepository.Object
                    );
        }
    }
}
