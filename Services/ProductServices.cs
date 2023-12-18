
using System.ComponentModel;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce;

public class ProductServices : Iproduct
{
    private readonly EcomDbContext _dbContext;

    public ProductServices(EcomDbContext _context)
    {
        _dbContext = _context;

    }

    public async Task<string> AddProduct(Product product)
    {
        await _dbContext.Products.AddAsync(product);
        await _dbContext.SaveChangesAsync();
        return "Product added successfully";
    }

    public async Task<bool> DeleteProduct(Product product)
    {
        _dbContext.Products.Remove(product);
        await _dbContext.SaveChangesAsync();
        return true;
    }

    public async Task<Product> FilterProducNameAndPrice(string ProductName, int Price)
    {
        var filteredProducts = await _dbContext.Products.Where(p => p.ProductName.ToLower().Contains(ProductName.ToLower()) && p.Price == Price).ToListAsync();
        if (filteredProducts.Count > 0)
        {
            return filteredProducts[0];
        }
        return new Product();
    }

    public async Task<List<Product>> FilterProduct(string ProductName, int Price)
    {
        IQueryable<Product> queryProduct=  _dbContext.Products;
        if(!string.IsNullOrEmpty(ProductName.ToLower()) && Price > 0){
            queryProduct=queryProduct.Where(p=>p.ProductName.ToLower()==ProductName.ToLower() || p.Price <= Price);
            
        }
        return await queryProduct.ToListAsync();
        
    }
    public async Task<Product> GetOneProduct(Guid Id)
    {
        var returnedProduct = await _dbContext.Products.Where(pid => pid.ProductID == Id).FirstOrDefaultAsync();
        if (returnedProduct == null)
        {
            return new Product();
        }

        return returnedProduct;

    }



    public async Task<List<Product>> GettAllProduct(int Page)
    {
        try
        {
            var pageResult = 3;
            var pageCount = Math.Ceiling(_dbContext.Products.Count() / (double)pageResult);
            var allproducts = _dbContext.Products.OrderBy(p => p.ProductID).AsQueryable();

            var productpagination = await allproducts
            .Skip((Page - 1) * pageResult)
            .Take(pageResult)
            .ToListAsync();

            return productpagination;

        }
        catch (Exception ex)
        {
            Console.Write(ex.InnerException?.Message);
            return new List<Product>();
        }

    }

    

    public async Task<string> UpdateProduct(Product product)
    {
        // _dbContext.Products.Update(product);
        await _dbContext.SaveChangesAsync();
        return "Product updated successfully";
    }


}
