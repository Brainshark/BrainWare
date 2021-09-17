using System;
using System.Linq;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Threading.Tasks;
using Web.Infrastructure.Util;
using System.Data.SqlClient;

namespace Web.Infrastructure
{
    
    //Opted to go with the ADO route to not show favortism toward an ORM solution.  In general, I'm not a fan of ORMs.  In this instance,
    //where this is a very basic application, an ORM would likely be OK.  In highly transactional environments, it can be a bit difficult to tune for performance.
    //I've seen many companys opt for stored procedure based solutions for more control, or layering stored procedures within their ORM to mitigate bad SQL created by an ORM.

    //With that, I've added some methods that execute stored procedures in a generic manner, and have various helper methods to facilitate parameters and connections.

    //I've also moved the connectionstring to the web.config.  This is a controversial topic (sometimes) as it exposes credentials and other details of your database environment.
    //This can be mitigated through encryption, a "secrets manager service", tokenization, etc... but embedding the connection strings directly in code is not ideal.

    public class Database
    {
        private int timeout = 60; //ideally set externally via config or other means.
        private string _connectionStringName;
        public Database(string connectionStringName)
        {
            _connectionStringName = connectionStringName;
        }

        private SqlConnection CreateConnection()
        {
            var connectionString = ConfigurationManager.ConnectionStrings[_connectionStringName].ConnectionString;
            if (connectionString.IsNullOrEmpty())
            {
                throw new ArgumentNullException("connectionString");
            }
            return new SqlConnection(connectionString);
        }

        public virtual DataTable GetDataTable(string spName, Hashtable parameters)
        {
            var dt = new DataTable();
            try
            {
                using (var cnDT = CreateConnection())
                {
                    var adpDT = new SqlDataAdapter();
                    var cmdDT = new SqlCommand(spName, cnDT) { CommandType = CommandType.StoredProcedure, CommandTimeout = timeout };
                    cmdDT = ParseInputParameters(parameters, cmdDT);
                    adpDT.SelectCommand = cmdDT;
                    adpDT.SelectCommand.Connection.Open();
                    adpDT.Fill(dt);
                    for (var i = 0; i < adpDT.SelectCommand.Parameters.Count; i++)
                    {
                        if (adpDT.SelectCommand.Parameters[i].Direction == ParameterDirection.Output)
                        {
                            dt = ParseOutputParameters(adpDT, dt);
                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                
                var sql = GetSQLString(spName, parameters);
                var message = ex.Message + Environment.NewLine + sql;
                //ideally: logger saves exception
                Console.WriteLine(sql);
            }
            return dt;
        }


        public virtual async Task<DataTable> GetDataTableAsync(string spName, Hashtable parameters)
        {
            var dt = new DataTable();
            try
            {
                using (var cnDT = CreateConnection())
                {
                    var adpDT = new SqlDataAdapter();
                    var cmdDT = new SqlCommand(spName, cnDT) { CommandType = CommandType.StoredProcedure, CommandTimeout = timeout };
                    cmdDT = ParseInputParameters(parameters, cmdDT);
                    adpDT.SelectCommand = cmdDT;
                    await adpDT.SelectCommand.Connection.OpenAsync();
                    await Task.Run(() => adpDT.Fill(dt));
                    for (var i = 0; i < adpDT.SelectCommand.Parameters.Count; i++)
                    {
                        if (adpDT.SelectCommand.Parameters[i].Direction == ParameterDirection.Output)
                        {
                            dt = ParseOutputParameters(adpDT, dt);
                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                var sql = GetSQLString(spName, parameters);
                var message = ex.Message + Environment.NewLine + sql;
                //ideally: logger saves exception
                Console.WriteLine(sql);
            }
            return dt;
        }

        private string GetSQLString(string spName, Hashtable Params)
        {
            var sql = spName + " ";
            if ((Params != null))
            {
                var keys = Params.Keys;
                sql = keys.Cast<object>().Aggregate(sql, (current, key) => current + (key + "='" + Params[key.ToString()] + "',"));
            }
            return sql;
        }

        private SqlCommand ParseInputParameters(Hashtable Params, SqlCommand cmd)
        {
            var outPutParms = 0;
            if ((Params != null))
            {
                var keys = Params.Keys;
                foreach (var key in keys)
                {
                    object paramName = key.ToString();
                    var paramVal = Params[key.ToString()] ?? DBNull.Value;
                    if (key.ToString().Substring(0, 1) == "-")
                    {
                        SqlParameter outputIdParam = new SqlParameter(paramName.ToString().Substring(1, paramName.ToString().Length - 1), SqlDbType.VarChar, 100)
                        {
                            Direction = ParameterDirection.Output
                        };
                        cmd.Parameters.Add(outputIdParam);
                        outPutParms++;
                    }
                    else
                    {
                        cmd.Parameters.Add(new SqlParameter(paramName.ToString(), paramVal.GetType())).Value = paramVal;
                    }
                }
            }
            return cmd;
        }

        private DataTable ParseOutputParameters(SqlDataAdapter adpDT, DataTable dt)
        {
            DataTable table = new DataTable();
            DataRow row = table.NewRow();
            var i = 0;
            foreach (SqlParameter p in adpDT.SelectCommand.Parameters)
            {
                try
                {
                    if (p.Direction == ParameterDirection.Output)
                    {
                        if (p.Value.GetType().ToString() == "System.DBNull")
                        {
                            p.Value = "";
                        }
                        var t = p.Value.SafeDBNull<string>().GetType();
                        table.Columns.Add(p.ParameterName.Remove(0, 1), t);
                        row[i] = p.Value.SafeDBNull<string>() + "";
                        i++;
                    }
                }
                catch (Exception exOutput)
                {
                    //ideally: logger saves exception
                    Console.WriteLine(exOutput.Message);
                }
            }
            if (i > 0)
            {
                table.Rows.Add(row);
                return table;
            }
            return dt;
        }
    }
}