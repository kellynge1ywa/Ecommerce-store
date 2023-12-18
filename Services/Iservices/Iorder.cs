namespace Ecommerce;

public interface Iorder
{
    Task<Order> GetOneOrder(Guid Id);

    Task<string> AddOrder(Order order, List<string>ProductIds);

    Task <List<Order>> GetAllOrders();
    Task<string> UpdateOrder(Order order);
    Task<string> DeleteOrder(Order order);

}
