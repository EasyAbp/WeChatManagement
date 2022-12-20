$(function () {

    var service = easyAbp.weChatManagement.thirdPartyPlatforms.authorization.authorization;
    var modal = new abp.ModalManager(abp.appPath + 'WeChatManagement/ThirdPartyPlatforms/Authorization/CreateRequestModal');

    $('#CreateRequestButton').click(function (e) {
        e.preventDefault();
        service.preAuth({
            thirdPartyPlatformWeChatAppId: $('#ViewModel_ThirdPartyPlatformWeChatAppId').find(':selected').val(),
            authorizerName: $('#ViewModel_AuthorizerName').val(),
            allowOfficial: $('#ViewModel_AllowOfficial').val(),
            allowMiniProgram: $('#ViewModel_AllowMiniProgram').val(),
            specifiedAppId: $('#ViewModel_SpecifiedAppId').val(),
            categoryIds: $('#ViewModel_CategoryIds').val()
        }).then(res => {
            modal.open({
                "Input.ThirdPartyPlatformWeChatAppId": $('#ViewModel_ThirdPartyPlatformWeChatAppId').find(':selected').val(),
                "Input.AuthorizerName": $('#ViewModel_AuthorizerName').val(),
                "Input.AllowOfficial": $('#ViewModel_AllowOfficial').val(),
                "Input.AllowMiniProgram": $('#ViewModel_AllowMiniProgram').val(),
                "Input.SpecifiedAppId": $('#ViewModel_SpecifiedAppId').val(),
                "Input.CategoryIds": $('#ViewModel_CategoryIds').val(),
                "preAuthCode": res.preAuthCode,
                "token": res.token
            });
        })
    });
});