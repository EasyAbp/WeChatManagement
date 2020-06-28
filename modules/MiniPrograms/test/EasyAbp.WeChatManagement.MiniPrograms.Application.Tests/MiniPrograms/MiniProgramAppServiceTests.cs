using Shouldly;
using System.Threading.Tasks;
using Xunit;

namespace EasyAbp.WeChatManagement.MiniPrograms.MiniPrograms
{
    public class MiniProgramAppServiceTests : MiniProgramsApplicationTestBase
    {
        private readonly IMiniProgramAppService _miniProgramAppService;

        public MiniProgramAppServiceTests()
        {
            _miniProgramAppService = GetRequiredService<IMiniProgramAppService>();
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
