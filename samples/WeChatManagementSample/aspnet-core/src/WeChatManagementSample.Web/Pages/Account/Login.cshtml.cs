using System.Threading.Tasks;
using EasyAbp.WeChatManagement.MiniPrograms.Settings;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Volo.Abp.Account.Web;
using Volo.Abp.Account.Web.Pages.Account;

namespace WeChatManagementSample.Web.Pages.Account
{
    public class CustomLoginModel : LoginModel
    {
        public const string PasswordMethodName = "Password";
        public const string WeChatMiniProgramMethodName = "WeChatMiniProgram";
        
        [HiddenInput]
        [BindProperty(SupportsGet = true)]
        public string Method { get; set; } = WeChatMiniProgramMethodName;
        
        [BindProperty(SupportsGet = true)]
        public string MiniProgramName { get; set; }
        

        public CustomLoginModel(
            IAuthenticationSchemeProvider schemeProvider,
            IOptions<AbpAccountOptions> accountOptions) : base(schemeProvider, accountOptions)
        {
        }

        public override async Task<IActionResult> OnGetAsync()
        {
            MiniProgramName ??= await SettingProvider.GetOrNullAsync(MiniProgramsSettings.PcLogin.DefaultProgramName);
            
            return await base.OnGetAsync();
        }
    }
}