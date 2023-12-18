namespace Ecommerce;

public class Product
{
    public Guid ProductID {get;set;}

    public string ProductName {get;set;} = string.Empty;

    public string Description {get;set;} = string.Empty;

    public int Price {get;set;}
    public int Total {get;set;}




}
