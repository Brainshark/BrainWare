using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{

    abstract public class AbstractReader<T>
    {
        abstract protected string CommandText { get; }
        abstract protected CommandType CommandType { get; }
        abstract protected Collection<IDataParameter> GetParameters(IDbCommand command, List<KeyValuePair<string, object>> parameters);
        abstract protected MapperBase<T> GetMapper();
        
        private static string m_connectionString = "Data Source=(LocalDb)\\MSSQLLocalDB;Initial Catalog=BrainWAre;Integrated Security=SSPI;AttachDBFilename=C:\\Users\\vadim\\Documents\\GitHub\\BrainWare\\Web\\App_Data\\BrainWare.mdf";
        
        protected System.Data.IDbConnection GetConnection()
        {
            IDbConnection connection = new SqlConnection(m_connectionString);
            return connection;
        }

        public Collection<T> Execute(List<KeyValuePair<string, object>> parameters)
        {
            Collection<T> collection = new Collection<T>();
            using (IDbConnection connection = GetConnection())
            {
                IDbCommand command = connection.CreateCommand();
                command.Connection = connection;
                command.CommandText = this.CommandText;
                command.CommandType = this.CommandType;
                foreach (IDataParameter param in this.GetParameters(command, parameters))
                    command.Parameters.Add(param);
                try
                {
                    connection.Open();
                    using (IDataReader reader = command.ExecuteReader())
                    {
                        try
                        {
                            MapperBase<T> mapper = GetMapper();
                            collection = mapper.MapAll(reader);
                            return collection;
                        }
                        catch (Exception ex)
                        {
                            Debug.WriteLine("Error while reading database: " + ex.ToString());
                            return collection;
                        }
                        finally
                        {
                            reader.Close();
                        }
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("Error opening connection: " + ex.ToString());
                    return collection;
                }
                finally
                {
                    connection.Close();
                }
            }
        }  
    }
}
