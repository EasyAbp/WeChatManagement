(function ($) {

    $(document).ready(function () {

        abp.widgets.WeChatOfficialLogin = function ($widget) {

            var widgetManager = $widget.data('abp-widget-manager');
            var $loginArea = $widget.find('.weChat-official-login-area');

            function getFilters() {
                return {
                    officialName: $loginArea.attr('data-official-name')
                };
            }

            function init() {
                var urlParams = new URLSearchParams(document.location.search.slice(1));
                var code = urlParams.get('code');
                var state = urlParams.get('state');
                var appId = urlParams.get('appId');

                if (!code) {
                    return;
                }

                easyAbp.weChatManagement.officials.login.login.login({
                    code: code,
                    state: state,
                    appId: appId
                }, {
                    success: function () {
                        var returnUrl = urlParams.get('ReturnUrl');
                        var returnUrlHash = urlParams.get('ReturnUrlHash');
                        var targetUrl = document.location.origin;
                        if (returnUrl) targetUrl += decodeURI(returnUrl);
                        if (returnUrlHash) targetUrl += returnUrlHash;
                        document.location.href = targetUrl;
                    }
                });
            }
            return {
                init: init,
                getFilters: getFilters
            };
        };

        var widgetManager = new abp.WidgetManager({ filterForm: 'WeChatOfficialLogin' });
        widgetManager.init();
    });
})(jQuery);