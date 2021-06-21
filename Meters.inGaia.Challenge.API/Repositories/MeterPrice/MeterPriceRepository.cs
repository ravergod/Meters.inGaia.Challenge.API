using System;
using System.Threading.Tasks;
using Dapper;
using Meters.inGaia.Challenge.API.Core.Infrastructure;
using Meters.inGaia.Challenge.API.Models.Enums;
using Meters.inGaia.Challenge.API.Repositories.Base;
using Meters.inGaia.Challenge.API.Repositories.MeterPrice.Interface;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MeterPriceModel = Meters.inGaia.Challenge.API.Models.MeterPrice;

namespace Meters.inGaia.Challenge.API.Repositories.MeterPrice
{
    public class MeterPriceRepository : RepositoryBase, IMeterPriceRepository
    {
        public MeterPriceRepository(
            ILogger<MeterPriceModel> logger,
            IOptions<ApplicationSettings> applicationSettings)
            : base(logger, applicationSettings)
        {
        }

        public async Task<MeterPriceModel> GetMeterPrice()
        {
            MeterPriceModel result;

            logger.LogInformation(MsgConnectionInit);

            using (var conn = GetSQLDataBaseConnection())
            {
                logger.LogInformation(MsgConnectionOpen);

                var sql = @"SELECT		TOP 1 MP.ID, 
                                        MP.METERTYPE, 
                                        MP.VALUE 
                            FROM		METERPRICE MP
                            INNER JOIN	METERTYPE MT
                            ON			MT.TYPE = @METERTYPE 
                            ORDER BY	ID DESC";

                var param = new
                {
                    MeterType = nameof(MeterTypeEnum.M2)
                };

                result = await conn.QueryFirstAsync<MeterPriceModel>(sql, param);
            }

            logger.LogInformation(MsgConnectionClose);

            return result;
        }
    }
}
