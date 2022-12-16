$(function () {

    var l = abp.localization.getResource('EasyAbpWeChatManagementCommon');

    var service = easyAbp.weChatManagement.common.weChatApps.weChatApp;
    var createModal = new abp.ModalManager(abp.appPath + 'WeChatManagement/Common/WeChatApps/WeChatApp/CreateModal');
    var editModal = new abp.ModalManager(abp.appPath + 'WeChatManagement/Common/WeChatApps/WeChatApp/EditModal');

    var dataTable = $('#WeChatAppTable').DataTable(abp.libs.datatables.normalizeConfiguration({
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
                                text: l('Edit'),
                                visible: abp.auth.isGranted('EasyAbp.WeChatManagement.Common.WeChatApp.Update'),
                                action: function (data) {
                                    editModal.open({ id: data.record.id });
                                }
                            },
                            {
                                text: l('Delete'),
                                visible: abp.auth.isGranted('EasyAbp.WeChatManagement.Common.WeChatApp.Delete'),
                                confirmMessage: function (data) {
                                    return l('WeChatAppDeletionConfirmationMessage', data.record.id);
                                },
                                action: function (data) {
                                        service.delete(data.record.id)
                                        .then(function () {
                                            abp.notify.info(l('SuccessfullyDeleted'));
                                            dataTable.ajax.reload();
                                        });
                                }
                            }
                        ]
                }
            },
            {
                data: "type",
                render: function (data) {
                    switch (data) {
                        case 0:
                            return l("WeChatAppType.MiniProgram");
                        case 1:
                            return l("WeChatAppType.Official");
                        case 2:
                            return l("WeChatAppType.Work");
                        case 3:
                            return l("WeChatAppType.OpenPlatform");
                        case 4:
                            return l("WeChatAppType.ThirdPartyPlatform");
                        default:
                            return null;
                    }
                }
            },
            { data: "componentWeChatAppId" },
            { data: "name" },
            { data: "displayName" },
            { data: "openAppIdOrName" },
            { data: "appId" },
            { data: "appSecret" },
            { data: "token" },
            { data: "encodingAesKey" },
            { data: "isStatic" },
        ]
    }));

    createModal.onResult(function () {
        dataTable.ajax.reload();
    });

    editModal.onResult(function () {
        dataTable.ajax.reload();
    });

    $('#NewWeChatAppButton').click(function (e) {
        e.preventDefault();
        createModal.open();
    });
});