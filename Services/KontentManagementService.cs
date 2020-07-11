using Kentico.Kontent.Management;
using Microsoft.Extensions.Configuration;

namespace RowlingApp.Services
{
    public class KontentManagementService
    {
        private IConfiguration _configuration;
        
        private ManagementClient _managementClient;

        public KontentManagementService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public ManagementClient GetManagementClient()
        {
            if (_managementClient == null)
            {
                ManagementOptions options = new ManagementOptions
                {
                    ApiKey = _configuration.GetValue<string>("ManagementOptions:APIKey"),
                    ProjectId = _configuration.GetValue<string>("ManagementOptions:ProjectId")
                };
                _managementClient = new ManagementClient(options);
            };

            return _managementClient;
        }

    }
}
