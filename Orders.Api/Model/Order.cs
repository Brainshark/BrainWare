using System;
using System.Collections.Generic;

namespace Orders.Api.Model
{
    public partial class Order
    {
        public Order()
        {
            Orderproduct = new HashSet<Orderproduct>();
        }

        public int OrderId { get; set; }
        public string Description { get; set; }
        public int CompanyId { get; set; }

        public virtual ICollection<Orderproduct> Orderproduct { get; set; }
        public virtual Company Company { get; set; }
    }
}
