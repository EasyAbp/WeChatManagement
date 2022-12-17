using System;
using JetBrains.Annotations;
using Volo.Abp.Data;
using Volo.Abp.Security.Encryption;

namespace EasyAbp.WeChatManagement.Common.WeChatApps;

public static class WeChatAppExtensions
{
    public static string EncryptedVerifyTicketPropertyName { get; set; } = "EncryptedVerifyTicket";

    public static string GetVerifyTicketOrNullAsync(this WeChatApp weChatApp,
        IStringEncryptionService stringEncryptionService)
    {
        return stringEncryptionService.Decrypt(weChatApp.GetProperty<string>(EncryptedVerifyTicketPropertyName));
    }

    public static void SetVerifyTicketAsync(this WeChatApp weChatApp, [CanBeNull] string verifyTicket,
        IStringEncryptionService stringEncryptionService)
    {
        var encryptedVerifyTicket =
            verifyTicket.IsNullOrWhiteSpace() ? null : stringEncryptionService.Encrypt(verifyTicket);

        weChatApp.SetProperty(EncryptedVerifyTicketPropertyName, encryptedVerifyTicket);
    }
}