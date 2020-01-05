using System.Threading.Tasks;

namespace RowlingApp.Services
{
    public interface IKontentManagementBetaService
    {
        Task<bool> CreateContentItemNewVersion(string ContentItemCodeName);

        void PublishContentItem();
    }
}
