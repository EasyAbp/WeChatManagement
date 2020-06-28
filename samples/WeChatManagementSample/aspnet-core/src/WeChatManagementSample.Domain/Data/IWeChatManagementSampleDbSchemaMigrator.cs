using System.Threading.Tasks;

namespace WeChatManagementSample.Data
{
    public interface IWeChatManagementSampleDbSchemaMigrator
    {
        Task MigrateAsync();
    }
}
