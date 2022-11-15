using System.Threading.Tasks;
using Shouldly;
using Xunit;

namespace WeChatManagementSample.Pages
{
    public class Index_Tests : WeChatManagementSampleWebTestBase
    {
        [Fact]
        public async Task Welcome_Page()
        {
            var response = await GetResponseAsStringAsync("/");
            response.ShouldNotBeNull();
        }
    }
}
