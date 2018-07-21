using System.Collections.Generic;

namespace Data.Models
{
    public class OrderProduct
    {
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }

        public override bool Equals(object obj)
        {
            var product = obj as OrderProduct;
            return product != null &&
                   OrderId == product.OrderId &&
                   ProductId == product.ProductId &&
                   Product.Equals(product.Product) &&
                   Quantity == product.Quantity &&
                   Price == product.Price;
        }

        public override int GetHashCode()
        {
            var hashCode = 1608138497;
            hashCode = hashCode * -1521134295 + OrderId.GetHashCode();
            hashCode = hashCode * -1521134295 + ProductId.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<Product>.Default.GetHashCode(Product);
            hashCode = hashCode * -1521134295 + Quantity.GetHashCode();
            hashCode = hashCode * -1521134295 + Price.GetHashCode();
            return hashCode;
        }
    }
}
