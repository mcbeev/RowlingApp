using Kentico.Kontent.Delivery;
using Kentico.Kontent.Delivery.Abstractions;
using Microsoft.Extensions.Configuration;

namespace RowlingApp.Services
{
    public class KontentDeliveryService
    {        
        private IConfiguration _configuration;
        private IDeliveryClientFactory _clientFactory;
        private IDeliveryClient _deliveryClient;

        public KontentDeliveryService(IConfiguration configuration, IDeliveryClientFactory clientFactory)
        {
            _configuration = configuration;
            _clientFactory = clientFactory;
            
        }

        public IDeliveryClient GetDeliveryClient()
        {
            if (_deliveryClient == null)
            {
                //var projectId = _configuration.GetValue<string>("DeliveryOptions:ProjectId");
                //_deliveryClient = DeliveryClientBuilder.WithProjectId(projectId).WithDeliveryHttpClient(_httpClient).Build();

                _deliveryClient = _clientFactory.Get("production");


            }

            return _deliveryClient;
        }
    }
}
