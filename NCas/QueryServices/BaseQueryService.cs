using System.Data;
using System.Data.SqlClient;
using NCas.Common;

namespace NCas.QueryServices
{
    public abstract class BaseQueryService
    {
        protected virtual IDbConnection GetConnection()
        {
            return new SqlConnection(ConfigSettings.CasConnectionString);
        }
    }
}
