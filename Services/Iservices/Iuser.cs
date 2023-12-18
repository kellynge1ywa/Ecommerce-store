namespace Ecommerce;

public interface Iuser
{
    Task<User > GetUser(string Email);

    Task<string> RegisterUser(User newUser);

    Task <List<Order>> UserOrdersandProducts(Guid ID);

}
