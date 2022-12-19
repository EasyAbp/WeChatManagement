using EasyAbp.WeChatManagement.Common.Web.Menus;
using EasyAbp.WeChatManagement.Officials.Localization;
using EasyAbp.WeChatManagement.Officials.Permissions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.UI.Navigation;

namespace EasyAbp.WeChatManagement.Officials.Web.Menus;

public class OfficialsMenuContributor : IMenuContributor
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
        var l = context.GetLocalizer<OfficialsResource>(); //Add main menu items.

        var miniProgramManagementMenuItem = new ApplicationMenuItem(OfficialsMenus.Prefix, l["Menu:MiniProgramManagement"]);

        if (await context.IsGrantedAsync(OfficialsPermissions.UserInfo.Default))
        {
            miniProgramManagementMenuItem.AddItem(
                new ApplicationMenuItem(OfficialsMenus.UserInfo, l["Menu:UserInfo"], "/WeChatManagement/Officials/UserInfos/UserInfo")
            );
        }

        if (!miniProgramManagementMenuItem.Items.IsNullOrEmpty())
        {
            var weChatManagementMenuItem = context.Menu.Items.GetOrAdd(i => i.Name == CommonMenus.Prefix,
                () => new ApplicationMenuItem("EasyAbpWeChatManagement", l["Menu:EasyAbpWeChatManagement"]));

            weChatManagementMenuItem.Items.Add(miniProgramManagementMenuItem);
        }
    }
}
