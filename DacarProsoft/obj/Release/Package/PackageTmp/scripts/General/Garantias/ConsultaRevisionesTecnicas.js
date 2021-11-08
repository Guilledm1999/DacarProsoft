$(document).ready(function () {

    RevisionesTecnicas();

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
        RevisionesTecnicas();
    }
}

function RevisionesTecnicas() {
    //var fechaInicio = $('#txtFechaInicio').val();
    //var fechaFin = $('#txtFechaFin').val();
    $.ajax({
        url: "../Garantias/ConsultarRevisionesTecnica"/*?FechaInicio=" + fechaInicio + "&FechaFin=" + fechaFin*/,
        type: "GET"
        , success: function (msg) {
            //ItemsPedidoCliente = msg;  
            $("#tblReporteRevisionesTecnicas").dxDataGrid({
                dataSource: msg,
                keyExpr: 'IngresoRevisionGarantiaId',
                showBorders: true,
                columnAutoWidth: false,
                showBorders: true,
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
                            saveAs(new Blob([buffer], { type: 'application/octet-stream' }), 'RevisionesTecnicas.xlsx');
                        });
                    });
                    e.cancel = true;
                },
                columns: [

                    { dataField: "IngresoRevisionGarantiaId", visible: false },
                    {
                        dataField: "Cedula", caption: "Cedula", alignment: "left", headerFilter: {
                            allowSearch: true
                        }
                    },
                    {
                        dataField: "Cliente", caption: "Cliente", alignment: "left"
                    },
                    {
                        dataField: "NumeroGarantia", caption: "Numero Garantia", alignment: "left"
                    },
                    {
                        dataField: "NumeroComprobante", caption: "Numero Comprobante", alignment: "left"
                    },
                    {
                        dataField: "NumeroRevision", caption: "Numero Revision", alignment: "left"
                    },
                    {
                        dataField: "Provincia", caption: "Provincia", alignment: "left", allowFiltering: false
                    },
                    {
                        dataField: "Direccion", caption: "Direccion", alignment: "left"
                    },
                    {
                        dataField: "Vendedor", caption: "Vendedor", alignment: "left"
                    },
                    {
                        dataField: "FacturaCliente", caption: "Numero Garantia", alignment: "left", allowFiltering: false, visible: false
                    },
                    {
                        dataField: "TestBateria", caption: "Numero Garantia", alignment: "left", allowFiltering: false, visible: false
                    },
                    {
                        dataField: "Marca", caption: "Marca", alignment: "left", allowFiltering: false
                    },
                    {
                        dataField: "Modelo", caption: "Modelo", alignment: "left", allowFiltering: false
                    },
                    {
                        dataField: "Lote", caption: "Lote", alignment: "left", allowFiltering: false, visible: false
                    },
                    {
                        dataField: "Prorrateo", caption: "Prorrateo", alignment: "left", allowFiltering: false, visible: false
                    },
                    {
                        dataField: "Meses", caption: "Meses", alignment: "left", allowFiltering: false, visible: false
                    },
                    {
                        dataField: "PorcentajeVenta", caption: "Porcentaje Venta", alignment: "left", allowFiltering: false, visible: false
                    },
                    {
                        dataField: "Voltaje", caption: "Voltaje", alignment: "left", allowFiltering: false, visible: false
                    },
                    {
                        dataField: "FechaVenta", caption: "Fecha Venta", alignment: "left", dataType: "date"
                    },
                    {
                        dataField: "FechaIngreso", caption: "Fecha Ingreso", alignment: "left", dataType: "date"
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