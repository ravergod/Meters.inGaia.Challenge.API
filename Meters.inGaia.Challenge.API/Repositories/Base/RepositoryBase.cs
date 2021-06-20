using System.Data.Common;
using System.Data.SqlClient;
using Meters.inGaia.Challenge.API.Core.Infrastructure;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Meters.inGaia.Challenge.API.Repositories.Base
{
    public class RepositoryBase
    {
        protected const string MsgConnectionInit = "Inicializando conexão com a base de dados.";
        protected const string MsgConnectionOpen = "Abrindo a conexão com a base de dados.";
        protected const string MsgConnectionClose = "Fechando a conexão com a base de dados.";

        protected readonly ILogger logger;
        protected readonly IOptions<ApplicationSettings> applicationSettings;

        public RepositoryBase(ILogger logger, IOptions<ApplicationSettings> applicationSettings)
        {
            this.logger = logger;
            this.applicationSettings = applicationSettings;
        }

        protected DbConnection GetSQLDataBaseConnection()
        {
            var conn = new SqlConnection(applicationSettings.Value.DatabaseSettings.DefaultConnectionString);
            return conn;
        }
    }
}
