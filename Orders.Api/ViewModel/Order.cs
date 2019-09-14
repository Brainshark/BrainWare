using System;
using System.Collections.Generic;
using System.Linq;

namespace Orders.Api.ViewModel
{
    using System.Security.AccessControl;

    public class OrderVM
    {
        public int OrderId { get; set; }

        public string CompanyName { get; set; }

        public string Description { get; set; }

        public decimal OrderTotal { get; set; }

        public List<OrderProductVM> OrderProducts { get; set; }

    }


    public class OrderProductVM
    {
        public int OrderId { get; set; }

        public int ProductId { get; set; }

        public ProductVM Product { get; set; }
    
        public int Quantity { get; set; }

        public decimal Price { get; set; }

    }

    public class ProductVM
    {
        public string Name { get; set; }

        public decimal Price { get; set; }
    }
}