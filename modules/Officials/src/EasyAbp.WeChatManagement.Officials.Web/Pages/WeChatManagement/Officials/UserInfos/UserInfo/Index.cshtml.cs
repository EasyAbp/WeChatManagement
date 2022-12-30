using System.Threading.Tasks;

namespace EasyAbp.WeChatManagement.Officials.Web.Pages.WeChatManagement.Officials.UserInfos.UserInfo
{
    public class IndexModel : OfficialsPageModel
    {
        public virtual async Task OnGetAsync()
        {
            await Task.CompletedTask;
        }
    }
}
