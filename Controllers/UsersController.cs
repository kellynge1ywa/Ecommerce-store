using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce;

[Route("api/[controller]")]
[ApiController]
public class UsersController: ControllerBase
{
    private readonly Iuser _userServices;
    private readonly IMapper _IMapper;
    private readonly Ijwt _ijwtServices;

    public UsersController(Iuser userI, IMapper userMapper,Ijwt ijwt)
    {
        _userServices=userI;
        _IMapper=userMapper;
        _ijwtServices=ijwt;
        
    }

    [HttpPost("register")]
    public async Task<ActionResult<string>> RegisterUser(RegisterUserDto newUser){
        var registeredUser= _IMapper.Map<User>(newUser);
        registeredUser.Password=BCrypt.Net.BCrypt.HashPassword(newUser.Password);
        var checkEmail= await _userServices.GetUser(newUser.Email);
        if(checkEmail != null){
            return BadRequest("Email exists");
        }
        var response = await _userServices.RegisterUser(registeredUser);
         return Ok(response);
         

    }

    [HttpPost("login")]
    public async Task<ActionResult<User>> UserLogin(LoginUserDto userlogin){

        var checkUser= await _userServices.GetUser(userlogin.Email);
        if(checkUser ==null){
            return BadRequest("Invalid credential");
        }
        var isPasswordCorrect=BCrypt.Net.BCrypt.Verify(userlogin.Password,checkUser.Password);
        if(!isPasswordCorrect){
            return BadRequest("Invalid credential");
        }

        var token=_ijwtServices.GenerateToken(checkUser);

        return Ok(token);
    }

    [HttpGet("{ID}/userOrders")]

    public async Task<ActionResult<List<Order>>> UserOrders(Guid ID){

        var userorders=await _userServices.UserOrdersandProducts(ID);
        if(userorders== null){
            return NotFound();
        }

        return Ok(userorders);

    }

    

     

}
