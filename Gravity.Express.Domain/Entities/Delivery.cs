using Gravity.Express.Domain.Common;

namespace Gravity.Express.Domain.Entities;

public class Delivery : EntityBase
{
    public Guid ECommercePlatformId { get; set; } // İlgili e-ticaret platformunun ID'si

    public Guid OrderId { get; set; } // İlgili siparişin ID'si

    public string DeliveryAddress { get; set; } // Teslimatın yapılacağı adres

    public string Status { get; set; } // Teslimatın durumu, örneğin: Hazırlanıyor, Yolda, Teslim Edildi

    public string TrackingNumber { get; set; } // Teslimat için takip numarası

    public DateTime EstimatedDeliveryDate { get; set; } // Tahmini teslimat tarihi

    public DateTime? ActualDeliveryDate { get; set; } // Gerçekleşen teslimat tarihi

    public Order Order { get; set; } // İlgili sipariş

    public ECommercePlatform ECommercePlatform { get; set; } // İlgili e-ticaret platformu
}
