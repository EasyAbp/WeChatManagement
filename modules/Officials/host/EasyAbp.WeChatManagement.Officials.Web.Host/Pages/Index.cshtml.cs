using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;

namespace EasyAbp.WeChatManagement.Officials.Pages;

public class IndexModel : OfficialsPageModel
{
    public void OnGet()
    {

    }

    public async Task OnPostLoginAsync()
    {
        await HttpContext.ChallengeAsync("oidc");
    }
}
