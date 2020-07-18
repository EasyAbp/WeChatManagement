(function ($) {

    $(document).ready(function () {

        abp.widgets.WeChatMiniProgramPcLogin = function ($widget) {
            
            var widgetManager = $widget.data('abp-widget-manager');
            var $pcLoginArea = $widget.find('.weChat-miniProgram-pcLogin-area');

            function getFilters() {
                return {
                    miniProgramName: $pcLoginArea.attr('data-miniProgram-name')
                };
            }

            function init() {
                var intervalID = window.setInterval(function () {
                    easyAbp.weChatManagement.miniPrograms.login.login.pcLogin({token: token}, {
                        success: function (data) {
                            if (data.isSuccess){
                                clearInterval(intervalID);
                                var urlParams = new URLSearchParams(document.location.search.slice(1));
                                console.log(urlParams);
                                var returnUrl = urlParams.get('ReturnUrl') ?? '';
                                var returnUrlHash = urlParams.get('ReturnUrlHash') ?? '';
                                document.location.href = document.location.origin + decodeURI(returnUrl + returnUrlHash);
                            }
                        }
                    })
                }, 3000);
            }
            return {
                init: init,
                getFilters: getFilters
            };
        };

        var widgetManager = new abp.WidgetManager({filterForm: 'WeChatMiniProgramPcLogin'});
        widgetManager.init();
    });
})(jQuery);