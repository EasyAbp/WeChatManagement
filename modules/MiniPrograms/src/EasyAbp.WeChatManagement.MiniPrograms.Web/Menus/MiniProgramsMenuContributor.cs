using System.Collections.Generic;
using System.Threading.Tasks;
using EasyAbp.WeChatManagement.Common.Web.Menus;
using EasyAbp.WeChatManagement.MiniPrograms.Localization;
using EasyAbp.WeChatManagement.MiniPrograms.Permissions;
using Volo.Abp.UI.Navigation;

namespace EasyAbp.WeChatManagement.MiniPrograms.Web.Menus
{
    public class MiniProgramsMenuContributor : IMenuContributor
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
            var l = context.GetLocalizer<MiniProgramsResource>(); //Add main menu items.

            var miniProgramManagementMenuItem = new ApplicationMenuItem(MiniProgramsMenus.Prefix, l["Menu:MiniProgramManagement"]);
            
            if (await context.IsGrantedAsync(MiniProgramsPermissions.UserInfo.Default))
            {
                miniProgramManagementMenuItem.AddItem(
                    new ApplicationMenuItem(MiniProgramsMenus.UserInfo, l["Menu:UserInfo"], "/WeChatManagement/MiniPrograms/UserInfos/UserInfo")
                );
            }
            
            if (!miniProgramManagementMenuItem.Items.IsNullOrEmpty())
            {
                var weChatManagementMenuItem = context.Menu.GetAdministration().Items.GetOrAdd(i => i.Name == CommonMenus.Prefix,
                    () => new ApplicationMenuItem("EasyAbpWeChatManagement", l["Menu:EasyAbpWeChatManagement"], icon: "fa fa-weixin"));
                
                weChatManagementMenuItem.Items.Add(miniProgramManagementMenuItem);
            }
        }
    }
}
