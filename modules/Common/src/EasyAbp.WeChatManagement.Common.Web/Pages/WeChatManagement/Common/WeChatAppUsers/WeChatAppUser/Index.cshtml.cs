using System.Threading.Tasks;

namespace EasyAbp.WeChatManagement.Common.Web.Pages.WeChatManagement.Common.WeChatAppUsers.WeChatAppUser
{
    public class IndexModel : CommonPageModel
    {
        public virtual async Task OnGetAsync()
        {
            await Task.CompletedTask;
        }
    }
}
