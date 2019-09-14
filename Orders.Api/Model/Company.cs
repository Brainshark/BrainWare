using System;
using System.Collections.Generic;

namespace Orders.Api.Model
{
    public partial class Company
    {
        public Company()
        {
            Order = new HashSet<Order>();
        }

        public int CompanyId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Order> Order { get; set; }
    }
}
