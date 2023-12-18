// using Microsoft.AspNetCore.Components;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;






// using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;



namespace Ecommerce;

[Route("api/[controller]")]
[ApiController]

public class ProductsController: ControllerBase
{
    private readonly Iproduct _productService;
    private readonly IMapper _mapper;
    private ResponseDto _response;

    public ProductsController(Iproduct productI, IMapper mapper)
    {
        _productService=productI;
        _mapper=mapper;
        _response= new ResponseDto();
    }

    [HttpGet]
    public async Task<ActionResult<List<Product>>> GetAllProducts([FromQuery] Pagination productPagination){
        var productList=await _productService.GettAllProduct(productPagination.Page);
        return  Ok(productList);
    
    }

   [HttpGet("filter")]
   public async Task<ActionResult<Product>> FilterProducts(string ProductName, int Price){
    try{

        var filteredProducts=await _productService.FilterProduct(ProductName,Price);
        if(filteredProducts==null){
            return NotFound("Product not found");
        }
        return Ok(filteredProducts);

    } catch(Exception ex){
        Console.WriteLine(ex.InnerException?.Message);
        return StatusCode(500, "Internal server");
    }
   }
    

    [HttpPost]
    // [Authorize(Policy ="Admin")]
    public async Task<ActionResult<string>> AddProduct(AddProductDto newProduct){
        var AddedProduct= _mapper.Map<Product>(newProduct);
        var response= await _productService.AddProduct(AddedProduct);
        return Ok(response);
        // return "Product added successfully";

    }

    

    [HttpGet("{Id}")]
    public async Task<ActionResult<Product>> GetProduct(Guid Id){
        var singleProduct= await _productService.GetOneProduct(Id);
        if(singleProduct==null){
            // return Ok(singleProduct);
            // _response.StatusCode=HttpStatusCode.NotFound;
            return NotFound("Product not found");
        }

        return  Ok(singleProduct);

    }

    [HttpPut("{Id}")]
    // [Route("api/update")]
    [Authorize(Policy ="Admin")]

    public async Task<ActionResult<Product>> UpdateProduct(Guid Id, AddProductDto updProduct){
        var singleProduct= await _productService.GetOneProduct(Id);
        if(singleProduct==null){
            // Console.WriteLine("Product not found");
            return NotFound("Product not found");
        }
        var updatedProduct= _mapper.Map(updProduct,singleProduct);
        var response =await _productService.UpdateProduct(updatedProduct);

        return Ok(response);

    }
    [HttpDelete("{Id}")]
    [Authorize(Policy ="Admin")]
     public async Task<ActionResult<Product>> DeleteProduct(Guid Id){
        var singleProduct= await _productService.GetOneProduct(Id);
        if(singleProduct==null){
            // Console.WriteLine("Product not found");
            return NotFound("Product not found");
        }
    
        var response =await _productService.DeleteProduct(singleProduct);

        return Ok(response);

    }

}
