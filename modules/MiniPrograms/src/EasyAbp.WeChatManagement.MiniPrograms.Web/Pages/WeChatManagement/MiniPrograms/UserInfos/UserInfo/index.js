$(function () {

    var l = abp.localization.getResource('EasyAbpWeChatManagementMiniPrograms');

    var service = easyAbp.weChatManagement.miniPrograms.userInfos.userInfo;

    var dataTable = $('#UserInfoTable').DataTable(abp.libs.datatables.normalizeConfiguration({
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
            { data: "userId" },
            { data: "nickName" },
            { data: "gender" },
            { data: "language" },
            { data: "city" },
            { data: "province" },
            { data: "country" },
            { data: "avatarUrl" },
        ]
    }));
});