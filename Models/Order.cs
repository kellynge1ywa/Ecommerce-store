using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ecommerce;

public class Order
{
    [Key]
    public Guid OrderId {get;set;}

    public DateTime OrderDate {get;set;}

    public string  Status {get;set;} = string.Empty;

    public int OrderTotal {get;set;}
    [ForeignKey("UserId")]
    public Guid UserID {get;set;}

    public List<OrderProducts> Products {get;set;} =new List<OrderProducts>();


}
