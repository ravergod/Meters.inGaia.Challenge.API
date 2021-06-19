using Meters.inGaia.Challenge.API.Models.Enums;

namespace Meters.inGaia.Challenge.API.Models
{
    public class MeterPrice
    {
        public int Id { get; set; }

        public MeterTypeEnum MeterType { get; set; }

        public decimal Value { get; set; }
    }
}
