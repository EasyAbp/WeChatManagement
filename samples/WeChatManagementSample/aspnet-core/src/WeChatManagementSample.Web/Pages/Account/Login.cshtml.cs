using System.Threading.Tasks;
using EasyAbp.WeChatManagement.MiniPrograms.Settings;
using EasyAbp.WeChatManagement.Officials.Settings;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
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
        public const string WeChatOfficialMethodName = "WeChatOfficial";

        [HiddenInput]
        [BindProperty(SupportsGet = true)]
        public string Method { get; set; } = WeChatMiniProgramMethodName;

        [BindProperty(SupportsGet = true)]
        public string MiniProgramName { get; set; }

        [BindProperty(SupportsGet = true)]
        public string OfficialName { get; set; }


        public CustomLoginModel(
            IAuthenticationSchemeProvider schemeProvider,
            IOptions<AbpAccountOptions> accountOptions,
            IOptions<IdentityOptions> identityOptions)
            : base(schemeProvider, accountOptions, identityOptions)
        {
        }

        public override async Task<IActionResult> OnGetAsync()
        {
            MiniProgramName ??= await SettingProvider.GetOrNullAsync(MiniProgramsSettings.PcLogin.DefaultProgramName);

            OfficialName ??= await SettingProvider.GetOrNullAsync(OfficialsSettings.Login.DefaultProgramName);

            return await base.OnGetAsync();
        }
    }
}