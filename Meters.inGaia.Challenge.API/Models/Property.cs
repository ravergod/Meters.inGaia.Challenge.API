using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Meters.inGaia.Challenge.API.Models
{
    public class Property
    {
        public decimal PropertySizeInSquareMeters { get; set; }

        public decimal Value { get; set; }

        public string Error { get; set; }
    }
}
