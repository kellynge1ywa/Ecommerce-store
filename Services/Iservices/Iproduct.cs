using Microsoft.AspNetCore.Mvc;

namespace Ecommerce;

public interface Iproduct
{
    Task<List<Product>> GettAllProduct(int Page);
    Task<Product> GetOneProduct(Guid Id);

    Task<string> AddProduct(Product product);
    Task<string> UpdateProduct(Product product);
    Task<bool>  DeleteProduct(Product product);
    Task<List<Product>> FilterProduct(string ProductName, int Price);

    
}
