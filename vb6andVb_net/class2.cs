public class Customer
{
    public int CustomerId { get; set; }
    public string Name { get; set; }
}
public class OrderDetails
{
    public int OrderId { get; set; }
    public string ProductName { get; set; }
}
public OrderDetails GetOrderDetails(Customer cust)
{
    return new OrderDetails
    {
        OrderId = 101,
        ProductName = "Laptop"
    };
}
