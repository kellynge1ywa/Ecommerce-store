using System.ComponentModel.DataAnnotations.Schema;

namespace Ecommerce;

public class OrderProducts
{
    public Guid Id {get;set;}
    [ForeignKey("OrderId")]
    public Guid OrderId {get;set;}
     [ForeignKey("ProductID")]
     public Guid ProductID {get;set;}

     public Product? Products {get;set;}

     

}
