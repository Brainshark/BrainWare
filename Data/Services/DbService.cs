using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;

namespace Data.Services
{
    public class DbService : IDbService
    {
        private readonly SqlConnection _connection;

        public DbService()
        {
            _connection = new SqlConnection("Data Source=(LocalDb)\\MSSQLLocalDB;Initial Catalog=BrainWAre;Integrated Security=SSPI;AttachDBFilename=C:\\brainshark\\BrainWare\\Web\\App_Data\\BrainWare.mdf");

            _connection.Open();
        }

        public DbDataReader ExecuteReader(string query, IDictionary<string, string> parameters = null)
        {       
            var sqlQuery = new SqlCommand(query, _connection);
            if (parameters != null && parameters.Keys.Any())
            {
                foreach(var key in parameters.Keys)
                {
                    sqlQuery.Parameters.AddWithValue(key, parameters[key]);
                }
            }

            return sqlQuery.ExecuteReader();
        }

        public int ExecuteNonQuery(string query)
        {
            var sqlQuery = new SqlCommand(query, _connection);

            return sqlQuery.ExecuteNonQuery();
        }

    }
}