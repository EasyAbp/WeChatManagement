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

    private Task ConfigureMainMenuAsync(MenuConfigurationContext context)
    {
        //Add main menu items.
        context.Menu.AddItem(new ApplicationMenuItem(OfficialsMenus.Prefix, displayName: "Officials", "~/Officials", icon: "fa fa-globe"));

        return Task.CompletedTask;
    }
}
