using Kentico.Kontent.Delivery;
using Kentico.Kontent.Delivery.Abstractions;

namespace RowlingApp.Services
{
    public interface IKontentDeliveryService
    {
        IDeliveryClient GetDeliveryClient();
    }
}
