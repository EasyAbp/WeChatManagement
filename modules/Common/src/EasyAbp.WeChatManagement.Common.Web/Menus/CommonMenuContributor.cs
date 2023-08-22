using System.Collections.Generic;
using System.Threading.Tasks;
using EasyAbp.WeChatManagement.Common.Localization;
using EasyAbp.WeChatManagement.Common.Permissions;
using Volo.Abp.UI.Navigation;

namespace EasyAbp.WeChatManagement.Common.Web.Menus
{
    public class CommonMenuContributor : IMenuContributor
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
            var l = context.GetLocalizer<CommonResource>(); //Add main menu items.

            var weChatManagementMenuItem = context.Menu.GetAdministration().Items.GetOrAdd(i => i.Name == CommonMenus.Prefix,
                () => new ApplicationMenuItem(CommonMenus.Prefix, l["Menu:EasyAbpWeChatManagement"], icon: "fa fa-weixin"));

            if (await context.IsGrantedAsync(CommonPermissions.WeChatApp.Default))
            {
                weChatManagementMenuItem.AddItem(
                    new ApplicationMenuItem(CommonMenus.WeChatApp, l["Menu:WeChatApp"], "/WeChatManagement/Common/WeChatApps/WeChatApp")
                );
            }
            if (await context.IsGrantedAsync(CommonPermissions.WeChatAppUser.Default))
            {
                weChatManagementMenuItem.AddItem(
                    new ApplicationMenuItem(CommonMenus.WeChatAppUser, l["Menu:WeChatAppUser"], "/WeChatManagement/Common/WeChatAppUsers/WeChatAppUser")
                );
            }
        }
    }
}