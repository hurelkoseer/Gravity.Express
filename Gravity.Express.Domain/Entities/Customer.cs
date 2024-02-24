using Gravity.Express.Domain.Common;

namespace Gravity.Express.Domain.Entities;

public class Customer : EntityBase
{
    public string Name { get; set; }

    public string Email { get; set; }

    public string PhoneNumber { get; set; }

    public string Address { get; set; }

    public ICollection<Order> Orders { get; set; }
}
