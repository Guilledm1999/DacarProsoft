$(document).ready(function () {
    $(".loading-icon").css("display", "none");
    $(document).on('click', '.fa', function (event) {
        event.preventDefault();
        $(this).closest('tr').remove();
    });
    $("#image").removeClass("hide");
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
        }, 3000);
    }

    else {
        PedidosRecibidos();
    }


}


function PedidosRecibidos() {
    var tipo = $("#SelectEstado option:selected").val();
    var fechaInicio = $('#txtFechaInicio').val();
    var fechaFin = $('#txtFechaFin').val();
    $.ajax({
        url: "../Reportes/ReporteGeneralDeControl?Tipo=" + tipo + "&FechaInicio=" + fechaInicio + "&FechaFin=" + fechaFin,
        type: "GET"
        , success: function (msg) {
            //ItemsPedidoCliente = msg;  
            $("#tblReporteControl").dxDataGrid({
                dataSource: msg,
                keyExpr: 'NumeroPedidoId',
                showBorders: true,
                columnAutoWidth: true,
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
                            saveAs(new Blob([buffer], { type: 'application/octet-stream' }), 'PedidosExteriorControl.xlsx');
                        });
                    });
                    e.cancel = true;
                },
                columns: [

                    { dataField: "NumeroPedidoId", visible: false },
                    {
                        dataField: "NombreCliente", caption: "Nombre Cliente", alignment: "left", headerFilter: {
                            allowSearch: true
                        }
                    },
                    {
                        dataField: "OrdenCompra", caption: "Orden Compra", alignment: "left"
                    },
                    {
                        dataField: "Estado", caption: "Estado", alignment: "left"
                    },
                    {
                        dataField: "FechaEmision", caption: "Fecha Emision", alignment: "left", dataType: "date"
                    },
                    {
                        dataField: "FechaIngresoSap", caption: "Fecha Ingresada Sap", alignment: "left", dataType: "date"
                    },
                    {
                        dataField: "TiempoAtencion", caption: "Tiempo Atencion(Dias)", alignment: "left", allowFiltering: false
                    },
                    {
                        dataField: "FechaRequerida", caption: "Fecha Requerida Cliente", alignment: "left", dataType: "date"
                    },
                    {
                        dataField: "FechaDespacho", caption: "Fecha Despacho Vendedor", alignment: "left", dataType: "date"
                    },
                    {
                        dataField: "FechaPlazoEntrega", caption: "Tiempo Entrega(Dias)", alignment: "left", allowFiltering: false
                    },
                    {
                        dataField: "FechaCargaLista", caption: "Fecha Carga Lista", alignment: "left", dataType: "date"
                    },
                    {
                        dataField: "FechaPlazoCargaLista", caption: "Tiempo Carga Lista(Dias)", alignment: "left", allowFiltering: false
                    },
                  
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