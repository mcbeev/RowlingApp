using Kentico.Kontent.Delivery;
using Microsoft.Extensions.Configuration;
using System.Net.Http;

namespace RowlingApp.Services
{
    public class KontentDeliveryService
    {        
        private IConfiguration _configuration;
        private HttpClient _httpClient;
        private ITypeProvider _typeProvider;
        private IDeliveryClient _deliveryClient;

        public KontentDeliveryService(IConfiguration configuration, HttpClient httpClient, ITypeProvider typeProvider)
        {
            _configuration = configuration;
            _httpClient = httpClient;
            _typeProvider = typeProvider;
        }

        public IDeliveryClient GetDeliveryClient()
        {
            if (_deliveryClient == null)
            {
                var projectId = _configuration.GetValue<string>("DeliveryOptions:ProjectId");
                _deliveryClient = DeliveryClientBuilder.WithProjectId(projectId).WithHttpClient(_httpClient).WithTypeProvider(_typeProvider).Build();
            }

            return _deliveryClient;
        }
        
    }
}
