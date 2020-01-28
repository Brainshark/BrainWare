using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL.Mappers;
using Models;

namespace BL.Readers
{
    public class OrderReader : AbstractReader<Order>
    {
        protected override string CommandText
        {
            get { return "SELECT o.description, o.order_id, o.company_id FROM [order] o WHERE o.company_id = @companyId"; }
        }
        protected override CommandType CommandType
        {
            get { return System.Data.CommandType.Text; }
        }
        protected override Collection<IDataParameter> GetParameters(IDbCommand command, List<KeyValuePair<string, object>> parameters)
        {
            Collection<IDataParameter> collection = new Collection<IDataParameter>();

            foreach (var parameter in parameters)
            {
                IDataParameter param = command.CreateParameter();
                param.ParameterName = parameter.Key;
                param.Value = parameter.Value;
                collection.Add(param);
            }

            return collection;    
        }

        protected override MapperBase<Order> GetMapper()
        {
            MapperBase<Order> mapper = new OrderMapper();
            return mapper;
        }
    }
}
