//Статистика

$('#calendar').datepicker({
    range: true,
    multipleDatesSeparator: ' - ',
});

$('#AddOrderDate').datepicker({ container: '#AddOrderModal .modal-body', dateFormat: "dd.mm.yyyy" });