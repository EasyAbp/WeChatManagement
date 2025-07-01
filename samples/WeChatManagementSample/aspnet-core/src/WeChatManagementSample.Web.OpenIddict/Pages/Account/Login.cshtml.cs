using System.Threading.Tasks;
using EasyAbp.WeChatManagement.MiniPrograms.Settings;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Volo.Abp.Account.Web;
using Volo.Abp.Account.Web.Pages.Account;
using Volo.Abp.Identity;

namespace WeChatManagementSample.Web.Ids4.Pages.Account
{
    public class CustomLoginModel : LoginModel
    {
        public const string PasswordMethodName = "Password";
        public const string WeChatMiniProgramMethodName = "WeChatMiniProgram";

        [HiddenInput]
        [BindProperty(SupportsGet = true)]
        public string Method { get; set; } = WeChatMiniProgramMethodName;

        [BindProperty(SupportsGet = true)] public string MiniProgramName { get; set; }

        public CustomLoginModel(IAuthenticationSchemeProvider schemeProvider,
            IOptions<AbpAccountOptions> accountOptions, IOptions<IdentityOptions> identityOptions,
            IdentityDynamicClaimsPrincipalContributorCache identityDynamicClaimsPrincipalContributorCache,
            IWebHostEnvironment webHostEnvironment) : base(schemeProvider, accountOptions, identityOptions,
            identityDynamicClaimsPrincipalContributorCache, webHostEnvironment)
        {
        }

        public override async Task<IActionResult> OnGetAsync()
        {
            MiniProgramName ??= await SettingProvider.GetOrNullAsync(MiniProgramsSettings.PcLogin.DefaultProgramName);

            return await base.OnGetAsync();
        }
    }
}