using System;

namespace EasyAbp.WeChatManagement.Officials.Login.Dtos
{
    [Serializable]
    public class GetLoginAuthorizeUrlOutput
    {
        public string HandlePage { get; set; }

        public string AuthorizeUrl { get; set; }
    }
}