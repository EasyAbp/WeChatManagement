using System.Threading.Tasks;
using Xunit;

namespace EasyAbp.WeChatManagement.Common.WeChatAppUsers
{
    public class WeChatAppUserAppServiceTests : CommonApplicationTestBase
    {
        private readonly IWeChatAppUserAppService _weChatAppUserAppService;

        public WeChatAppUserAppServiceTests()
        {
            _weChatAppUserAppService = GetRequiredService<IWeChatAppUserAppService>();
        }

        [Fact]
        public async Task Test1()
        {
            // Arrange

            // Act

            // Assert
        }
    }
}
