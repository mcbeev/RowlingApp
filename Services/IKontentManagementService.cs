using Kentico.Kontent.Management;

namespace RowlingApp.Services
{
    public interface IKontentManagementService
    {
        ManagementClient GetManagementClient();
    }
}
