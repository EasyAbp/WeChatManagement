$(function () {

    var l = abp.localization.getResource('EasyAbpWeChatManagementMiniPrograms');

    var service = easyAbp.weChatManagement.miniPrograms.miniProgramUsers.miniProgramUser;

    var dataTable = $('#MiniProgramUserTable').DataTable(abp.libs.datatables.normalizeConfiguration({
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
            { data: "miniProgramId" },
            { data: "userId" },
            { data: "unionId" },
            { data: "openId" },
            { data: "sessionKey" },
            { data: "sessionKeyChangedTime" },
        ]
    }));
});