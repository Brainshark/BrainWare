namespace Api.Models
{
    public class OrderProductResult
    {
        public int OrderId { get; set; }

        public int ProductId { get; set; }

        public ProductResult Product { get; set; }

        public int Quantity { get; set; }

        public decimal Price { get; set; }
    }
}
