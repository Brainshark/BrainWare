using System;
using System.Collections.Generic;

namespace Orders.Api.Model
{
    public partial class Product
    {
        public Product()
        {
            Orderproduct = new HashSet<Orderproduct>();
        }

        public int ProductId { get; set; }
        public string Name { get; set; }
        public decimal? Price { get; set; }

        public virtual ICollection<Orderproduct> Orderproduct { get; set; }
    }
}
