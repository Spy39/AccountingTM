$(function () {
    $('#date').datepicker({ dateFormat: "dd.mm.yyyy" });
    $('#dateStart').datepicker({ dateFormat: "dd.mm.yyyy" });

    $('#dateCompletedWork').datepicker({ container: '#addCompletedWorkModal', dateFormat: "dd.mm.yyyy" });
    $('#dateReceptionAndTransmission').datepicker({ container: '#addReceptionAndTransmissionModal', dateFormat: "dd.mm.yyyy" });
    $('#dateRepair').datepicker({ container: '#addRepairModal', dateFormat: "dd.mm.yyyy" });
    $('#dateAcceptance').datepicker({ container: '#addStorageModal', dateFormat: "dd.mm.yyyy" });
    $('#dateRemoval').datepicker({ container: '#addStorageModal', dateFormat: "dd.mm.yyyy" });
    $('#dateConservation').datepicker({ container: '#addConservationModal', dateFormat: "dd.mm.yyyy" });
    $('#datePeriod').datepicker({ container: '#addConservationModal', dateFormat: "dd.mm.yyyy" });
    $('#dateDisposalInformation').datepicker({ container: '#addDisposalInformationModal', dateFormat: "dd.mm.yyyy" });

    //Вывод Select

    //Тип технического средства
        $("#typeEquipment").select2({
            width: '100%',
            allowClear: true,
            placeholder: 'Наименование типа',
            ajax: {
                transport: (data, success, failure) => {
                    let params = data.data;
                    let maxResultCount = 30;

                    params.page = params.page || 1;

                    let filter = {};
                    filter.maxResultCount = maxResultCount;
                    filter.skipCount = (params.page - 1) * maxResultCount;
                    filter.keyword = params.term
                    axios.get("/TypeEquipment/GetAll", { params: filter }).then(function (result) {

                        success({
                            results: result.data.items,
                            pagination: {
                                more: (params.page * maxResultCount) < result.data.totalCount
                            }
                        });
                    });
                },
                cache: true
            },
            templateResult: (data) => data.name,
            templateSelection: (data) => data.name
        })

    //Бренд техниеского средства
        $("#brand").select2({
            width: '100%',
            allowClear: true,
            placeholder: 'Наименование бренда',
            ajax: {
                transport: (data, success, failure) => {
                    let params = data.data;
                    let maxResultCount = 30;

                    params.page = params.page || 1;

                    let filter = {};
                    filter.maxResultCount = maxResultCount;
                    filter.skipCount = (params.page - 1) * maxResultCount;
                    filter.keyword = params.term
                    axios.get("/Brand/GetAll", { params: filter }).then(function (result) {

                        success({
                            results: result.data.items,
                            pagination: {
                                more: (params.page * maxResultCount) < result.data.totalCount
                            }
                        });
                    });
                },
                cache: true
            },
            templateResult: (data) => data.name,
            templateSelection: (data) => data.name
        })

})
