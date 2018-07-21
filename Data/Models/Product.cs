using System.Collections.Generic;

namespace Data.Models
{
    public class Product
    {
        public string Name { get; set; }
        public decimal Price { get; set; }

        public override bool Equals(object obj)
        {
            var product = obj as Product;
            return product != null &&
                   Name == product.Name &&
                   Price == product.Price;
        }

        public override int GetHashCode()
        {
            var hashCode = -44027456;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Name);
            hashCode = hashCode * -1521134295 + Price.GetHashCode();
            return hashCode;
        }
    }
}
