using Meters.inGaia.Challenge.API.Models.Enums;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Meters.inGaia.Challenge.API.Models
{
    public class MeterPrice
    {
        public int Id { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public MeterTypeEnum MeterType { get; set; }

        public decimal Value { get; set; }
    }
}
