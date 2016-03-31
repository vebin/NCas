using System.Data;
using System.Data.SqlClient;
using NCas.Common;

namespace NCas.Domain.Repositories
{
    public abstract class BaseRepository
    {
        protected virtual IDbConnection GetConnection()
        {
            return new SqlConnection(ConfigSettings.CasConnectionString);
        }
    }
}
