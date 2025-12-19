$(function () {

    var l = abp.localization.getResource('EasyAbpWeChatManagementCommon');

    var service = easyAbp.weChatManagement.common.weChatAppUsers.weChatAppUser;

    var dataTable = $('#WeChatAppUserTable').DataTable(abp.libs.datatables.normalizeConfiguration({
        processing: true,
        serverSide: true,
        paging: true,
        searching: false,
        autoWidth: false,
        scrollCollapse: true,
        order: [[1, "asc"]],
        ajax: abp.libs.datatables.createAjax(service.getList),
        columnDefs: [
            {
                rowAction: {
                    items:
                        [
                            {
                                text: l('Detail'),
                                action: function (data) {
                                }
                            }
                        ]
                }
            },
            { data: "weChatAppId" },
            { data: "userId" },
            { data: "unionId" },
            { data: "openId" },
            { data: "sessionKeyChangedTime", dataFormat: 'datetime' },
        ]
    }));
});