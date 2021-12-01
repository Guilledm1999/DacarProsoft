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
        url: "../Reportes/ReporteGeneralPedidos?Tipo=" + tipo + "&FechaInicio=" + fechaInicio + "&FechaFin=" + fechaFin,
        type: "GET"
        , success: function (msg) {
            //ItemsPedidoCliente = msg;  
            $("#tblReporteGeneral").dxDataGrid({
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
                headerFilter: {
                    visible: true
                },
                searchPanel: {
                    visible: true,
                    width: 240,
                    placeholder: "Buscar..."
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
                    var worksheet = workbook.addWorksheet('Reporte Pedidos Exterior');

                    DevExpress.excelExporter.exportDataGrid({
                        component: e.component,
                        worksheet: worksheet,
                        autoFilterEnabled: true
                    }).then(function () {
                        workbook.xlsx.writeBuffer().then(function (buffer) {
                            saveAs(new Blob([buffer], { type: 'application/octet-stream' }), 'PedidosExterior.xlsx');
                        });
                    });
                    e.cancel = true;
                },
                columns: [

                    { dataField: "NumeroPedidoId", visible: false },
                    {
                        dataField: "CardCode", caption: "CardCode", alignment: "left", visible: false
                    },
                    {
                        dataField: "NombreCliente", caption: "Nombre Cliente", alignment: "left" ,headerFilter: {
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
                        dataField: "Pais", caption: "Pais", alignment: "left"
                    },
                    {
                        dataField: "TerminoImportacion", caption: "Termino Importacion", alignment: "left"
                    },
                    {
                        dataField: "CantidadNueva", caption: "Cantidad Confirmada", alignment: "left", allowFiltering: false
                    },
                    {
                        dataField: "PrecioTotal", caption: "Precio Total", alignment: "left", allowFiltering: false,
                        format: { style: "currency", currency: "USD", useGrouping: true, minimumSignificantDigits: 2 }

                    },
                    {
                        dataField: "FechaEmision", caption: "Fecha Emision", alignment: "left", dataType: "date"
                    },
                    {
                        dataField: "FechaRequerida", caption: "Fecha Requerida", alignment: "left", dataType: "date"
                    },
                    {
                        dataField: "FechaNuevoDespacho", caption: "Fecha Despacho ", alignment: "left", dataType: "date"
                    },
                   
                   {
                        dataField: "Ciudad", caption: "Ciudad", alignment: "left", visible: false
                    }, {
                        dataField: "Direccion", caption: "Direccion", alignment: "left", visible: false
                    }, {
                        dataField: "FechaCargaLista", caption: "Fecha Carga Lista", alignment: "left", dataType: "date"
                    }, {
                        dataField: "FechaDespachoPuerto", caption: "Fecha Despacho Puerto", alignment: "left", dataType: "date"
                    }, {
                        dataField: "FechaZarpe", caption: "Fecha Zarpe", alignment: "left", dataType: "date"
                    }, {
                        dataField: "FechaArribo", caption: "Fecha Arribo", alignment: "left", dataType: "date"
                    }, {
                        dataField: "FechaEntrega", caption: "Fecha Entrega", alignment: "left", dataType: "date"
                    },
                    {
                        dataField: "FechaIngresadaSap", caption: "Fecha Ingresada Sap", alignment: "left", dataType: "date", visible: false
                    },
                    {
                        dataField: "Cantidad", caption: "Cantidad Cliente", alignment: "left", visible: false
                    }, {
                        dataField: "PesoNetoTotal", caption: "Peso Neto", alignment: "left", visible: false
                    }, {
                        dataField: "Observaciones", caption: "Observaciones", alignment: "left", visible: false
                    },
                    //{
                    //    caption: "Acciones",
                    //    cellTemplate: function (container, options) {

                    //        var btnDetalle = "<button type='button' class='btn-primary' onclick='ConsultarDetallePedido(" + JSON.stringify(options.data) + ")'>Detalle</button>";

                    //        $("<div>")
                    //            .append($(btnDetalle))
                    //            .appendTo(container);
                    //    }
                    //}
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