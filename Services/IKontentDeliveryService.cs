using Kentico.Kontent.Delivery;

namespace RowlingApp.Services
{
    public interface IKontentDeliveryService
    {
        IDeliveryClient GetDeliveryClient();
    }
}
