using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Models
{
    using System.Security.AccessControl;

    public class Order
    {
        public int Id { get; set; }

        public int CompanyId { get; set; }

        public string CompanyName { get; set; }

        public string Description { get; set; }

        public decimal OrderTotal { get; set; }

        public List<OrderProduct> OrderProducts { get; set; }

    }

}