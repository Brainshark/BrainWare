using System.Collections.Generic;
using System.Data;
using System.Data.Common;

namespace Data.Services
{
    public interface IDbService
    {
        int ExecuteNonQuery(string query);
        IDataReader ExecuteReader(string query, IDictionary<string, string> parameters = null);
    }
}