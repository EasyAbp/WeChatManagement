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
                            if (data.isSuccess) {
                                clearInterval(intervalID);
                                var urlParams = new URLSearchParams(document.location.search.slice(1));
                                var returnUrl = urlParams.get('ReturnUrl');
                                var returnUrlHash = urlParams.get('ReturnUrlHash');
                                var targetUrl = document.location.origin;
                                if (returnUrl) targetUrl += decodeURI(returnUrl);
                                if (returnUrlHash) targetUrl += returnUrlHash;
                                document.location.href = targetUrl;
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

        var widgetManager = new abp.WidgetManager({filterForm: 'WeChatMiniProgramPcLogin'});
        widgetManager.init();
    });
})(jQuery);