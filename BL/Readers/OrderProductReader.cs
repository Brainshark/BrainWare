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
    public class OrderProductReader : AbstractReader<OrderProduct>
    {
        protected override string CommandText
        {
            get { return "SELECT op.price, op.quantity, op.order_id, op.product_id, p.name AS product_name, p.price AS product_price FROM orderproduct op INNER JOIN product p ON op.product_id=p.product_id INNER JOIN [order] o ON o.order_id = op.order_id WHERE o.company_id = @companyId"; }
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

        protected override MapperBase<OrderProduct> GetMapper()
        {
            MapperBase<OrderProduct> mapper = new OrderProductMapper();
            return mapper;
        }
    }
}
