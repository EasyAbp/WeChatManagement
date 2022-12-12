using System.Collections.Generic;
using System.Threading.Tasks;
using EasyAbp.WeChatManagement.Common.Web.Menus;
using EasyAbp.WeChatManagement.ThirdPartyPlatforms.Localization;
using Volo.Abp.UI.Navigation;

namespace EasyAbp.WeChatManagement.ThirdPartyPlatforms.Web.Menus;

public class ThirdPartyPlatformsMenuContributor : IMenuContributor
{
    public async Task ConfigureMenuAsync(MenuConfigurationContext context)
    {
        if (context.Menu.Name == StandardMenus.Main)
        {
            await ConfigureMainMenuAsync(context);
        }
    }

    private async Task ConfigureMainMenuAsync(MenuConfigurationContext context)
    {
        var l = context.GetLocalizer<ThirdPartyPlatformsResource>();

        var thirdPartyPlatformManagementMenuItem =
            new ApplicationMenuItem(ThirdPartyPlatformsMenus.Prefix, l["Menu:ThirdPartyPlatformManagement"]);

        if (!thirdPartyPlatformManagementMenuItem.Items.IsNullOrEmpty())
        {
            var weChatManagementMenuItem = context.Menu.Items.GetOrAdd(i => i.Name == CommonMenus.Prefix,
                () => new ApplicationMenuItem("EasyAbpWeChatManagement", l["Menu:EasyAbpWeChatManagement"]));

            weChatManagementMenuItem.Items.Add(thirdPartyPlatformManagementMenuItem);
        }
    }
}