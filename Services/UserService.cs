
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce;

public class UserService : Iuser
{
    private readonly EcomDbContext _userServices;
    private ResponseDto _response;

    public UserService(EcomDbContext ecommerceData)
    {
        _userServices=ecommerceData;
        _response= new ResponseDto();
        
    }
    public async Task<User> GetUser(string Email)
    {
        var ourUser =await _userServices.Users.Where(k=>k.Email.ToLower()==Email.ToLower()).FirstOrDefaultAsync();
        if(ourUser==null){
            return null!;
        }
        
        return ourUser;
    }

    public async Task<string> RegisterUser(User newUser)
    {
        _userServices.Users.Add(newUser);
        await _userServices.SaveChangesAsync();
        return "Registration successfully";

    }

   

    public  async Task<List<Order>> UserOrdersandProducts(Guid ID)
    {
            var TheUser= await _userServices.Users.FirstOrDefaultAsync(U=>U.UserId==ID);
            if(TheUser==null){
                return new List<Order>()!;
            }
            List<Order> findUserOrder= await _userServices.Orders.Include(o=>o.Products).ThenInclude(k=>k.Products).Where(U=>U.UserID==ID).ToListAsync();
            
            return findUserOrder;
           
    }
}
