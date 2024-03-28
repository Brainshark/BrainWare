namespace Api.Infrastructure
{
    using Models;

    public class OrderService : IOrderService
    {
        private readonly IEntityHandler _entityHandler;
        public OrderService(IEntityHandler entityHandler) 
        {
            _entityHandler = entityHandler;
        }
        public List<OrderResult> GetOrdersForCompany(int CompanyId)
        {
            List<Order> orders = _entityHandler.GetOrdersForCompany(CompanyId);

            var orderResults = new List<OrderResult>();

            foreach (var order in orders)
            {
                var orderProductResults = new List<OrderProductResult>();

                foreach(Orderproduct op in order.Orderproducts)
                {
                    orderProductResults.Add(new OrderProductResult
                    {
                        OrderId = op.OrderId,
                        ProductId = op.ProductId,
                        Price = op.Price!.Value,
                        Quantity = op.Quantity,
                        Product = new ProductResult
                        {
                             Name = op.Product!.Name,
                             Price = op.Product.Price!.Value
                        }
                    });
                }

                orderResults.Add(new OrderResult
                {
                    CompanyName = order.Company.Name,
                    Description = order.Description,
                    OrderId = order.OrderId,
                    OrderProducts = orderProductResults,
                    OrderTotal = orderProductResults.Sum(op => op.Price * op.Quantity)
                });
            }

            return orderResults;
        }
    }
}