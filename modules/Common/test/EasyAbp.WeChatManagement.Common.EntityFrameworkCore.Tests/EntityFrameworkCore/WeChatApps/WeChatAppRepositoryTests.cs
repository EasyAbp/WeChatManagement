using System.Threading.Tasks;
using EasyAbp.WeChatManagement.Common.WeChatApps;
using Xunit;

namespace EasyAbp.WeChatManagement.Common.EntityFrameworkCore.WeChatApps
{
    public class WeChatAppRepositoryTests : CommonEntityFrameworkCoreTestBase
    {
        private readonly IWeChatAppRepository _weChatAppRepository;

        public WeChatAppRepositoryTests()
        {
            _weChatAppRepository = GetRequiredService<IWeChatAppRepository>();
        }

        [Fact]
        public async Task Test1()
        {
            await WithUnitOfWorkAsync(async () =>
            {
                // Arrange

                // Act

                //Assert
            });
        }
    }
}
