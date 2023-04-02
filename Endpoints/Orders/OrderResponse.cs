namespace IWantApp.Endpoints.Orders
{
    public record OrderResponse(string CustomerId, string email, IEnumerable<OrderProduct> Products, decimal total, string DeliveryAddress);

    public record OrderProduct (Guid Id, string Name, decimal Price);
    
}