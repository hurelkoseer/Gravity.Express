using Gravity.Express.Domain.Common;

namespace Gravity.Express.Domain.Entities;

public class ECommercePlatform : EntityBase
{
    public string Name { get; set; }

    public string ApiUrl { get; set; }

    public string ApiKey { get; set; }

    public List<Delivery> Deliveries { get; set; } = new List<Delivery>();
}
