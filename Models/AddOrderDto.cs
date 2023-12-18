using System.ComponentModel.DataAnnotations.Schema;

namespace Ecommerce;

public class AddOrderDto
{
    public DateTime OrderDate {get;set;}

    public string? Status {get;set;}

    public int OrderTotal {get;set;}

    public List<string> ProductIDs {get;set;} = new List<string>();

}
