using EasyAbp.WeChatManagement.MiniPrograms.Settings;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace WeChatManagementSample.Web.Pages
{
    public class IndexModel : WeChatManagementSamplePageModel
    {
        [BindProperty(SupportsGet = true)]
        public string MiniProgramName { get; set; }

        public async Task OnGetAsync()
        {
            MiniProgramName ??= await SettingProvider.GetOrNullAsync(MiniProgramsSettings.PcLogin.DefaultProgramName);
        }
    }
}