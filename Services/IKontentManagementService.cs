using Kontent.Ai.Management;

namespace RowlingApp.Services
{
    public interface IKontentManagementService
    {
        ManagementClient GetManagementClient();
    }
}
