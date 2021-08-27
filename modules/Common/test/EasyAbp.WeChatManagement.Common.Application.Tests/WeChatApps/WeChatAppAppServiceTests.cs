using System.Threading.Tasks;
using Xunit;

namespace EasyAbp.WeChatManagement.Common.WeChatApps
{
    public class WeChatAppAppServiceTests : CommonApplicationTestBase
    {
        private readonly IWeChatAppAppService _weChatAppAppService;

        public WeChatAppAppServiceTests()
        {
            _weChatAppAppService = GetRequiredService<IWeChatAppAppService>();
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
