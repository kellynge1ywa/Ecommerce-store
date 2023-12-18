
using Microsoft.EntityFrameworkCore;

namespace Ecommerce;

public class OrderServices : Iorder

{
    private readonly EcomDbContext _dbContext;
    public OrderServices(EcomDbContext _context)
    {
        _dbContext=_context;
        
    }

    public async Task<string> AddOrder( Order order,List<string>ProductIds)
    {
        await _dbContext.Orders.AddAsync(order);
        var orderProductIds= ProductIds.Select(p => new OrderProducts{
            OrderId = order.OrderId,
            ProductID = new Guid(p),
        });
        await _dbContext.OrderProducts.AddRangeAsync(orderProductIds);
        await _dbContext.SaveChangesAsync();
        return "Order added successfully";
    }

    public async Task<string> DeleteOrder(Order order)
    {
        _dbContext.Orders.Remove(order);
        await _dbContext.SaveChangesAsync();
        return "Order deleted successfully";
    }

    public async Task<List<Order>> GetAllOrders()
    {
        // var orders = (
        //     from o in _dbContext.Orders
        //     join op in _dbContext.OrderProducts on o.OrderId equals op.OrderId

        // )
        return await _dbContext.Orders.Include(O=>O.Products).ThenInclude(k=>k.Products).ToListAsync();
    }

    public async  Task<Order> GetOneOrder(Guid Id) 
    {
        var oneOrder= await _dbContext.Orders.Include(O=>O.Products).FirstOrDefaultAsync(O=>O.OrderId==Id);
        if(oneOrder == null){
            return new Order();
        }
        return oneOrder;
    }

    public async Task<string> UpdateOrder(Order order)
    {
        _dbContext.Orders.Update(order);
        await _dbContext.SaveChangesAsync();
        return "Order updated successfully";
    }
}
