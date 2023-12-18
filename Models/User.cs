using System.ComponentModel.DataAnnotations;

namespace Ecommerce;

public class User
{
    [Key]
    public Guid UserId {get;set;}
    public string Fullname {get;set;} = string.Empty;
    public string Email {get;set;}  = string.Empty;
    public  string Password {get;set;}  = string.Empty;

    public string Role {get;set;} = "user";

    public List<Order> Orders {get;set;} = new List<Order>();


}
