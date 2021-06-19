using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Meters.inGaia.Challenge.API.Core.Infrastructure
{
    public class ApplicationSettings
    {
        public DatabaseSettings DatabaseSettings { get; set; }
    }

    public class DatabaseSettings
    {
        public string DefaultConnectionString { get; set; }
    }
}
