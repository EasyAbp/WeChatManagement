using System.Collections.Generic;
using System.Threading.Tasks;
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
                await ConfigureMainMenu(context);
            }
        }

        private async Task ConfigureMainMenu(MenuConfigurationContext context)
        {
            var l = context.GetLocalizer<MiniProgramsResource>(); //Add main menu items.

            var miniProgramManagementMenuItem = new ApplicationMenuItem(MiniProgramsMenus.Prefix, l["Menu:MiniProgramManagement"]);

            if (await context.IsGrantedAsync(MiniProgramsPermissions.MiniProgram.Default))
            {
                miniProgramManagementMenuItem.AddItem(
                    new ApplicationMenuItem(MiniProgramsMenus.MiniProgram, l["Menu:MiniProgram"], "/WeChatManagement/MiniPrograms/MiniPrograms/MiniProgram")
                );
            }
            if (await context.IsGrantedAsync(MiniProgramsPermissions.MiniProgramUser.Default))
            {
                miniProgramManagementMenuItem.AddItem(
                    new ApplicationMenuItem(MiniProgramsMenus.MiniProgramUser, l["Menu:MiniProgramUser"], "/WeChatManagement/MiniPrograms/MiniProgramUsers/MiniProgramUser")
                );
            }
            if (await context.IsGrantedAsync(MiniProgramsPermissions.UserInfo.Default))
            {
                miniProgramManagementMenuItem.AddItem(
                    new ApplicationMenuItem(MiniProgramsMenus.UserInfo, l["Menu:UserInfo"], "/WeChatManagement/MiniPrograms/UserInfos/UserInfo")
                );
            }
            
            if (!miniProgramManagementMenuItem.Items.IsNullOrEmpty())
            {
                var weChatManagementMenuItem = context.Menu.Items.GetOrAdd(i => i.Name == MiniProgramsMenus.ModuleGroupPrefix,
                    () => new ApplicationMenuItem("EasyAbpWeChatManagement", l["Menu:EasyAbpWeChatManagement"]));
                
                weChatManagementMenuItem.Items.Add(miniProgramManagementMenuItem);
            }
        }
    }
}
