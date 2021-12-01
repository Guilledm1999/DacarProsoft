$(document).ready(function () {

    PedidosRecibidos();

    $(".loading-icon").css("display", "none");
    $(document).on('click', '.fa', function (event) {
        event.preventDefault();
        $(this).closest('tr').remove();
    });
//    $("#image").removeClass("hide");
});

$('#LinkClose').on("click", function (e) {
    $("#MensajeErrorInesperado").hide('fade');
});
$('#LinkClose2').on("click", function (e) {
    $("#MensajeIngreseTodosLosCampos").hide('fade');
});


function ConsultarReporte() {
    $(".result").text("");
    $(".loading-icon").removeClass("hide");
    $(".btn").attr("disabled", true);
    $(".btn-txt").text("Espere...");
    //var txtFechaInicio = document.getElementById('txtFechaInicio').value;
    //var txtFechaFin = document.getElementById('txtFechaFin').value;

    var txtFechaInicio = $("#txtFechaInicio").val();
    var txtFechaFin = $("#txtFechaFin").val();

    if (txtFechaInicio.length == 0 && txtFechaFin.length == 0) {
        $(".btn").attr("disabled", false);
        $(".btn-txt").text("Consultar");
        $("#MensajeIngreseTodosLosCampos").show('fade');
        setTimeout(function () {
            $("#MensajeIngreseTodosLosCampos").fadeOut(1500);
        }, 3000); return;
    }
    else {
        PedidosRecibidos();
    }
}

function PedidosRecibidos() {
    //var fechaInicio = $('#txtFechaInicio').val();
    //var fechaFin = $('#txtFechaFin').val();
    $.ajax({
        url: "../Reportes/ReporteGeneralDeGarantias"/*?FechaInicio=" + fechaInicio + "&FechaFin=" + fechaFin*/,
        type: "GET"
        , success: function (msg) {
            //ItemsPedidoCliente = msg;  
            $("#tblReporteGarantias").dxDataGrid({
                dataSource: msg,
                keyExpr: 'IngresoGarantiaId',
                showBorders: true,
                columnAutoWidth: false,
                showBorders: true,
                showColumnLines: true,
                rowAlternationEnabled: false,
                showRowLines: true,
                paging: {
                    pageSize: 10
                },
                selection: {
                    mode: 'multiple'
                },
                searchPanel: {
                    visible: true,
                    width: 240,
                    placeholder: "Buscar..."
                },
                headerFilter: {
                    visible: true
                },
                pager: {
                    visible: true,
                    allowedPageSizes: [5, 10, 50],
                    showPageSizeSelector: true,
                    showInfo: true,
                    showNavigationButtons: true
                },
                export: {
                    enabled: true,
                    allowExportSelectedData: true
                },

                onExporting: function (e) {
                    var workbook = new ExcelJS.Workbook();
                    var worksheet = workbook.addWorksheet('Reporte Control');

                    DevExpress.excelExporter.exportDataGrid({
                        component: e.component,
                        worksheet: worksheet,
                        autoFilterEnabled: true
                    }).then(function () {
                        workbook.xlsx.writeBuffer().then(function (buffer) {
                            saveAs(new Blob([buffer], { type: 'application/octet-stream' }), 'GarantiasIngresadas.xlsx');
                        });
                    });
                    e.cancel = true;
                },
                columns: [

                    { dataField: "IngresoGarantiaId", visible: false },
                    {
                        dataField: "Cedula", caption: "Cedula", alignment: "left", headerFilter: {
                            allowSearch: true
                        }
                    },
                    {
                        dataField: "Nombre", caption: "Nombre", alignment: "left"
                    },
                    {
                        dataField: "Apellido", caption: "Apellido", alignment: "left"
                    },
                    {
                        dataField: "Email", caption: "Email", alignment: "left"
                    },
                    {
                        dataField: "Distribuidor", caption: "Distribuidor", alignment: "left"
                    },
                    {
                        dataField: "Ciudad", caption: "Ciudad", alignment: "left", allowFiltering: false
                    },
                    {
                        dataField: "ModeloBateria", caption: "Modelo Bateria", alignment: "left"
                    },
                    //{
                    //    dataField: "NumeroBateria", caption: "Numero Bateria", alignment: "left"
                    //},
                    {
                        dataField: "NumeroGarantia", caption: "Numero Garantia", alignment: "left", allowFiltering: false
                    },
                    {
                        dataField: "RegistroGarantia", caption: "Registro Garantia", alignment: "left", dataType: "date"
                    }
                ],
            });
            $(".btn").attr("disabled", false);
            $(".btn-txt").text("Consultar");
        },
        error: function (msg) {
            $(".btn").attr("disabled", false);
            $(".btn-txt").text("Consultar");
            $("#MensajeErrorInesperado").show('fade');
            setTimeout(function () {
                $("#MensajeErrorInesperado").fadeOut(1500);
            }, 3000); return;
        }
    })

}