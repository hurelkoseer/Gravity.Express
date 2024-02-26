namespace Gravity.Express.API.Models;

public class CreateCustomerRequest
{
    public required string Name { get; set; }
    public bool? IsPassive { get; set; }
}
