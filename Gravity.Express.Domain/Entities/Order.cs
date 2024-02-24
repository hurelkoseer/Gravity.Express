using Gravity.Express.Domain.Common;

namespace Gravity.Express.Domain.Entities;

public class Order : EntityBase
{
    public Guid CustomerId { get; set; } // Siparişi veren müşterinin ID'si

    public Customer Customer { get; set; } // Siparişi veren müşteri

    public Guid ECommercePlatformId { get; set; } // Siparişin verildiği e-ticaret platformunun ID'si

    public ECommercePlatform ECommercePlatform { get; set; } // Siparişin verildiği e-ticaret platformu

    public DateTime OrderDate { get; set; } // Sipariş tarihi

    public string OrderStatus { get; set; } // Sipariş durumu, örneğin: Onaylandı, Paketleniyor, Gönderildi
}
