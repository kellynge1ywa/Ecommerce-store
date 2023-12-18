using AutoMapper;

namespace Ecommerce;

public class ProductProfiles:Profile
{
    public ProductProfiles()
    {
        CreateMap<AddProductDto, Product>().ReverseMap();
        CreateMap<AddOrderDto, Order>().ReverseMap();
        CreateMap<RegisterUserDto, User>().ReverseMap();
        
        
    }

}
