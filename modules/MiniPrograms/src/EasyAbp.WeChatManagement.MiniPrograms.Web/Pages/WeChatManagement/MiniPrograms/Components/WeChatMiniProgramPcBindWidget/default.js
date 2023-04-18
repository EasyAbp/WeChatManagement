(function ($) {
    $(document).ready(function () {

        abp.widgets.WeChatMiniProgramPcBind = function ($widget) {

            intervalID = '';

            function getLocalize(key) {
                return abp.localization.localize(key, 'EasyAbpWeChatManagementMiniPrograms')
            }

            var preInfo = {
                appId: '',
                avatarUrl: '',
                code: '',
                lookupUseRecentlyTenant: false,
                nickName: '',
            }

            $(document.querySelector('#btn_bind')).on("click", function () {
                easyAbp.weChatManagement.miniPrograms.login.login.bind(preInfo, { abpHandleError: false })
                    .then(function () {
                        abp.notify.info(getLocalize("Succeed"));
                        setTimeout(function () {
                            window.location.reload();
                        }, 1500)
                    }).catch(function (res) {
                        abp.notify.error(getLocalize(res.code));
                        $(document.querySelector('#btn_destroy')).click();
                    });
            })

            $(document.querySelector('#btn_destroy')).bind("click", function () {
                document.querySelector("#bindCode").removeAttribute("class", "hidden")
                document.querySelector("#ready").setAttribute("class", "hidden")
                var intervalID = window.setInterval(function () {
                    easyAbp.weChatManagement.miniPrograms.login.login.getPcBindingAuthorizationInfo({ token: token }, {
                        success: function (data) {
                            preInfo = data;
                            if (data.code != null) {
                                document.querySelector("#bindCode").setAttribute("class", "hidden")
                                document.querySelector("#ready").removeAttribute("class", "hidden")
                                document.querySelector("#avatar").setAttribute("src", data.avatarUrl)
                                document.querySelector("#code").innerHTML = getLocalize("Verification") + "：" + (data.code.substr(0, 6));
                                document.querySelector("#nickname").innerHTML = data.nickName;
                                clearInterval(intervalID);
                            }
                        }
                    });
                }, 2000);
            })

            function getFilters() {
                return {};
            }

            function init() {
                if (token == null || token === '') return;
                var intervalID = window.setInterval(function () {
                    easyAbp.weChatManagement.miniPrograms.login.login.getPcBindingAuthorizationInfo({ token: token }, {
                        success: function (data) {
                            preInfo = data;
                            if (data.code != null) {
                                document.querySelector("#bindCode").setAttribute("class", "hidden")
                                document.querySelector("#ready").removeAttribute("class", "hidden")
                                document.querySelector("#avatar").setAttribute("src", data.avatarUrl)
                                document.querySelector("#code").innerHTML = getLocalize("Verification") + "：" + (data.code.substr(0, 6));
                                document.querySelector("#nickname").innerHTML = data.nickName;
                                clearInterval(intervalID);
                            }
                        }
                    });
                }, 3000);
            }
            return {
                init: init,
                getFilters: getFilters
            };
        };

        var widgetManager = new abp.WidgetManager({ filterForm: 'WeChatMiniProgramPcBind' });
        widgetManager.init();
    });
})(jQuery);