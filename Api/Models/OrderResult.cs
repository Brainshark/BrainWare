namespace Api.Models
{
    public class OrderResult
    {
        public int OrderId { get; set; }

        public string CompanyName { get; set; }

        public string Description { get; set; }

        public decimal OrderTotal { get; set; }

        public List<OrderProductResult> OrderProducts { get; set; }
    }
}