using Meters.inGaia.Challenge.API.Models;
using Meters.inGaia.Challenge.API.Repositories.MeterPriceRepository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Meters.inGaia.Challenge.API.Core.Infrastructure;
using Meters.inGaia.Challenge.API.Models.Enums;

namespace Meters.inGaia.Challenge.API.Repositories.MeterPriceRepository
{
    public class MeterPriceRepository : RepositoryBase, IMeterPriceRepository
    {
        public MeterPriceRepository(
            ILogger<MeterPrice> logger,
            IOptions<ApplicationSettings> applicationSettings)
            : base(logger, applicationSettings)
        {
        }

        public async Task<MeterPrice> GetMeterPrice()
        {
            MeterPrice result;

            logger.LogDebug(MsgConnectionInit);

            using (var conn = GetDataBaseConnection())
            {
                logger.LogDebug(MsgConnectionOpen);

                var sql = @"SELECT		TOP 1 MP.ID, MP.Metertype, MP.value 
                            FROM		METERPRICE MP
                            INNER JOIN	METERTYPE MT
                            ON			MT.TYPE = @METERTYPE 
                            ORDER BY	ID DESC";

                var param = new
                {
                    MeterType = nameof(MeterTypeEnum.M2)
                };

                try
                {
                    result = await conn.QueryFirstAsync<MeterPrice>(sql, param);
                }
                catch (Exception e)
                {
                    throw e;
                }
            }

            logger.LogDebug(MsgConnectionClose);

            return result;
        }
    }
}
