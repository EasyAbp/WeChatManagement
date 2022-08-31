/*
if (token != null && token != '') {
    var intervalID = window.setInterval(function () {
        if (expireTime == 0) {
            document.querySelector("#bindCode").setAttribute("class", "expire-layer")
            document.querySelector(".count").innerHTML = "二维码已过期";
            setTimeout(function () {
                clearInterval(intervalID);
            }, 1000)
            return;
        }
        document.querySelector(".count").innerHTML = `${expireTime} 秒后过期`;
        expireTime--;

    }, 1000);
}
*/
//(function ($) {

//    $(document).ready(function () {

        abp.widgets.WeChatMiniProgramPcBind = function ($widget) {

            var widgetManager = $widget.data('abp-widget-manager');
            var $pcBindArea = $widget.find('.weChat-miniProgram-pcBind-area');

            function getFilters() {
                return {};
            }

            function init() {
                if (token == null || token === '') return;

                var intervalClock = window.setInterval(function () {
                    if (expireTime == 0) {
                        document.querySelector("#bindCode").setAttribute("class", "expire-layer")
                        document.querySelector(".count").innerHTML = "二维码已失效";
                        setTimeout(function () {
                            clearInterval(intervalClock);
                        }, 1000)
                        return;
                    }
                    document.querySelector(".count").innerHTML = `${expireTime} 秒后过期`;
                    expireTime--;

                }, 1000);

                var intervalID = window.setInterval(function () {
                    easyAbp.weChatManagement.miniPrograms.login.login.pcBindStatus({ token: token, times: expireTime }, {
                        success: function (data) {
                            if (data.expired) {
                                document.querySelector(".count").innerHTML = "二维码已失效";
                                clearInterval(intervalID);
                            } else if (data.hasBound) {
                                document.location.reload();
                            } else if (data.isSucess) {
                                expireTime -= 3;
                                document.querySelector(".count").innerHTML = `$已扫码`;
                                clearInterval(intervalClock);
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
//    });
//})(jQuery);
