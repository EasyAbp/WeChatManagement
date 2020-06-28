using Shouldly;
using System.Threading.Tasks;
using Xunit;

namespace EasyAbp.WeChatManagement.MiniPrograms.MiniProgramUsers
{
    public class MiniProgramUserAppServiceTests : MiniProgramsApplicationTestBase
    {
        private readonly IMiniProgramUserAppService _miniProgramUserAppService;

        public MiniProgramUserAppServiceTests()
        {
            _miniProgramUserAppService = GetRequiredService<IMiniProgramUserAppService>();
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
