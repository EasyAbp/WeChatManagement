using System.Threading.Tasks;
using EasyAbp.WeChatManagement.Common.WeChatAppUsers;
using Xunit;

namespace EasyAbp.WeChatManagement.Common.EntityFrameworkCore.WeChatAppUsers
{
    public class WeChatAppUserRepositoryTests : CommonEntityFrameworkCoreTestBase
    {
        private readonly IWeChatAppUserRepository _weChatAppUserRepository;

        public WeChatAppUserRepositoryTests()
        {
            _weChatAppUserRepository = GetRequiredService<IWeChatAppUserRepository>();
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
