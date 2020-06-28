using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace WeChatManagementSample.Data
{
    /* This is used if database provider does't define
     * IWeChatManagementSampleDbSchemaMigrator implementation.
     */
    public class NullWeChatManagementSampleDbSchemaMigrator : IWeChatManagementSampleDbSchemaMigrator, ITransientDependency
    {
        public Task MigrateAsync()
        {
            return Task.CompletedTask;
        }
    }
}