using System.Collections.Generic;
using System.Data.Common;

namespace Data.Services
{
    public interface IDbService
    {
        int ExecuteNonQuery(string query);
        DbDataReader ExecuteReader(string query, IDictionary<string, string> parameters = null);
    }
}