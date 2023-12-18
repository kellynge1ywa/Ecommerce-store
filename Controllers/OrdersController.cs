using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce;

[Route("api/[controller]")]
[ApiController]
public class OrdersController:ControllerBase
{
    private readonly Iorder _orderServices;
    private readonly IMapper _Imapper;

    public OrdersController(Iorder orderI, IMapper mapper)
    {
        _orderServices=orderI;
        _Imapper=mapper;
        
    }

    [HttpGet]
    public async Task<ActionResult<List<Order>>> GetAllOrders(){
        var OrderList= await _orderServices.GetAllOrders();
        return Ok(OrderList);
    }

    [HttpPost("{UserId}")]
    
    public async Task<ActionResult<string>> CreateOrder(Guid UserId,AddOrderDto newOrder){
        try{
            var new_order= _Imapper.Map<Order>(newOrder);
            
            new_order.UserID=UserId;
            
        var Response = await _orderServices.AddOrder(new_order,newOrder.ProductIDs);
        return Ok(Response);
 
        } catch (Exception ex){
            return BadRequest( ex.InnerException?.Message);
        }
        
    }

    [HttpGet("{Id}")]

    public async Task<ActionResult<Order>> GetOneOrder(Guid Id){
        var singleOrder= await _orderServices.GetOneOrder(Id);
        if(singleOrder== null){
            return NotFound("Order not found");
        }
        return singleOrder;
    }

    [HttpPut("{Id}")]
    [Authorize(Policy ="Admin")]
    public async Task<ActionResult<string>> UpdateOrder(Guid Id, AddOrderDto uptOrder){
        var singleOrder= await _orderServices.GetOneOrder(Id);
        if(singleOrder== null){
            return NotFound("Order not found");
        }
        var updatedOrder= _Imapper.Map(uptOrder,singleOrder);
        var response = await _orderServices.UpdateOrder(updatedOrder);
        return Ok(response);

    }

    [HttpDelete("{Id}")]
    [Authorize(Policy ="Admin")]
    public async Task<ActionResult<string>> DeleteOrder(Guid Id){
        var singleOrder= await _orderServices.GetOneOrder(Id);
        if(singleOrder== null){
            return NotFound("Order not found");
        }
        var delete=await _orderServices.DeleteOrder(singleOrder);
        return Ok(delete);

    }

}
