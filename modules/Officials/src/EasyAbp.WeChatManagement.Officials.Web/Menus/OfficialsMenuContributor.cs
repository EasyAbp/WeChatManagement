using System.Collections.Generic;
using System.Threading.Tasks;
using EasyAbp.WeChatManagement.Common.Web.Menus;
using EasyAbp.WeChatManagement.Officials.Localization;
using EasyAbp.WeChatManagement.Officials.Permissions;
using Volo.Abp.UI.Navigation;

namespace EasyAbp.WeChatManagement.Officials.Web.Menus
{
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

            var OfficialManagementMenuItem = new ApplicationMenuItem(OfficialsMenus.Prefix, l["Menu:OfficialManagement"]);
            
            if (await context.IsGrantedAsync(OfficialsPermissions.UserInfo.Default))
            {
                OfficialManagementMenuItem.AddItem(
                    new ApplicationMenuItem(OfficialsMenus.UserInfo, l["Menu:UserInfo"], "/WeChatManagement/Officials/UserInfos/UserInfo")
                );
            }
            
            if (!OfficialManagementMenuItem.Items.IsNullOrEmpty())
            {
                var weChatManagementMenuItem = context.Menu.Items.GetOrAdd(i => i.Name == CommonMenus.Prefix,
                    () => new ApplicationMenuItem("EasyAbpWeChatManagement", l["Menu:EasyAbpWeChatManagement"]));
                
                weChatManagementMenuItem.Items.Add(OfficialManagementMenuItem);
            }
        }
    }
}
