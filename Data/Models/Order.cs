using System.Collections.Generic;
using System.Linq;

namespace Data.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public string CompanyName { get; set; }
        public string Description { get; set; }
        public decimal OrderTotal { get; set; }
        public List<OrderProduct> OrderProducts { get; set; }

        public override bool Equals(object obj)
        {
            var order = obj as Order;
            return order != null &&
                   OrderId == order.OrderId &&
                   CompanyName == order.CompanyName &&
                   Description == order.Description &&
                   OrderTotal == order.OrderTotal &&
                   OrderProducts.SequenceEqual(order.OrderProducts);
        }

        public override int GetHashCode()
        {
            var hashCode = -218625976;
            hashCode = hashCode * -1521134295 + OrderId.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(CompanyName);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Description);
            hashCode = hashCode * -1521134295 + OrderTotal.GetHashCode();
            // not implementing this for now because this would require an extension method for 
            // getting a hash of a collection
            // the underlying equals implementation should satisfy our needs here
            //hashCode = hashCode * -1521134295 + OrderProducts.GetHashCode();
            return hashCode;
        }
    }
}