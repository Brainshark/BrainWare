using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;

namespace BL.Mappers
{
    public class OrderMapper : MapperBase<Order>
    {
        protected override Order Map(IDataRecord record)
        {
            try
            {
                Order o = new Order();
                o.Id = (record["order_id"] == DBNull.Value ? 0 : (int)record["order_id"]);
                o.CompanyId = (record["company_id"] == DBNull.Value ? 0 : (int)record["company_id"]);
                o.Description = record["description"].ToString();
                o.OrderProducts = new List<OrderProduct>();
                return o;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error while mapping order: " + ex.ToString());
                return null;
            }
        }
    }
}
