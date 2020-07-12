$(function () {

    var l = abp.localization.getResource('EasyAbpWeChatManagementMiniPrograms');

    var service = easyAbp.weChatManagement.miniPrograms.miniPrograms.miniProgram;
    var createModal = new abp.ModalManager(abp.appPath + 'WeChatManagement/MiniPrograms/MiniPrograms/MiniProgram/CreateModal');
    var editModal = new abp.ModalManager(abp.appPath + 'WeChatManagement/MiniPrograms/MiniPrograms/MiniProgram/EditModal');

    var dataTable = $('#MiniProgramTable').DataTable(abp.libs.datatables.normalizeConfiguration({
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
                                visible: abp.auth.isGranted('EasyAbp.WeChatManagement.MiniPrograms.MiniProgram.Update'),
                                action: function (data) {
                                    editModal.open({ id: data.record.id });
                                }
                            },
                            {
                                text: l('Delete'),
                                visible: abp.auth.isGranted('EasyAbp.WeChatManagement.MiniPrograms.MiniProgram.Delete'),
                                confirmMessage: function (data) {
                                    return l('MiniProgramDeletionConfirmationMessage', data.record.id);
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
            { data: "weChatComponentId" },
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

    $('#NewMiniProgramButton').click(function (e) {
        e.preventDefault();
        createModal.open();
    });
});