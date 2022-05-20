var numeroGlobalPedido = null;
var numeroOrden = null;

$(document).ready(function () {
    PedidosConfirmados();
});


$('#LinkClose').on("click", function (e) {
    $("#MensajeErrorInesperado").hide('fade');
});


function PedidosConfirmados() {
    var valor = 3;
    $.ajax({
        url: "../Pedidos/ObtenerPedidosCanceladosGenerales?estado=" + valor,
        type: "GET"
        , success: function (msg) {
            //ItemsPedidoCliente = msg;  
            $("#tblPedidosCancelada").dxDataGrid({
                dataSource: msg,
                keyExpr: 'NumeroPedidoId',
                showBorders: true,
                columnAutoWidth: true,
                showBorders: true,
                paging: {
                    pageSize: 10
                },
                pager: {
                    visible: true,
                    allowedPageSizes: [5, 10, 50],
                    showPageSizeSelector: true,
                    showInfo: true,
                    showNavigationButtons: true
                },
                searchPanel: {
                    visible: true,
                    width: 240,
                    placeholder: "Buscar..."
                },
                columns: [

                    { dataField: "NumeroPedidoId", visible: false },
                    {
                        dataField: "NombreCliente", caption: "Cliente", alignment: "left"
                    },
                    {
                        dataField: "OrdenCompra", caption: "Orden Compra"
                    },
                    {
                        dataField: "Pais", caption: "Pais", alignment: "left"

                    },
                    {
                        dataField: "FechaEmision", caption: "Fecha Ingresada Cliente", alignment: "left", dataType: "date"

                    },
                    {
                        dataField: "FechaRequerida", caption: "Fecha Requerida Cliente ", alignment: "left", dataType: "date"


                    },
                    {
                        dataField: "TerminoImportacion", caption: "Termino Importacion", alignment: "left"
                    },
                    {
                        caption: "Acciones",
                        cellTemplate: function (container, options) {

                            var btnDetalle = "<button type='button' class='btn-primary' onclick='ConsultarDetallePedidoAprobado(" + JSON.stringify(options.data) + ")'>Detalle</button>";

                            $("<div>")
                                .append($(btnDetalle))
                                .appendTo(container);
                        }
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


function ConsultarDetallePedidoAprobado(modelo) {
    $("#ModalDetallePedidoCancelada").modal("show");
    numeroOrden = modelo.OrdenCompra;
    numeroGlobalPedido = modelo.NumeroPedidoId;
    DetalleFinal(modelo.NumeroPedidoId);
    DetallePedido(modelo.NumeroPedidoId);

}

function DetalleFinal(NumeroPedido) {
    $.ajax({
        url: "../Pedidos/ObtenerDetallaFinalPedidoGenerak?PedidoId=" + NumeroPedido,
        type: "Get",
        success: function (msg) {
            DetalleCliente = msg;
            console.log("este es el det client" + msg);


            $("#txtCantidadPedidoCancelada").val(msg[0]['CantitadTotal']);
            $("#txtPrecioFinalCancelada").val(msg[0]['PrecioFinalPedido']);
            $("#txtPesoNetoCancelada").val(msg[0]['PesoNetoFinalPedido']);
            $("#txtPesoBrutoCancelada").val(msg[0]['PesoBrutoFinalPedido']);
            $("#txtObservacionesCancelada").val(msg[0]['Observaciones']);
            //$("#txtFechaDocumentoAprobada").val(msg[0]['FechaIngresadaSap']);
            //$("#txtFechaDespachoAprobada").val(msg[0]['FechaNuevaDespacho']);

        },
        error: function (msg) {
            console.log("Error");
        },
    })
}

function DetallePedido(NumeroPedido) {
    $.ajax({
        url: "../Pedidos/ObtenerDetallePedido?PedidoId=" + NumeroPedido,
        type: "GET"
        , success: function (msg) {
            temp = msg;

            $("#tblDetallePedidoCancelada").dxDataGrid({
                dataSource: temp,
                columnAutoWidth: true,
                showBorders: true,
                keyExpr: "PedidoClienteDetalleId",
                allowColumnReordering: true,
                headerFilter: false,
                filterPanel: false,
                filterRow: false,
                columnFixing: {
                    enabled: true
                },

                paging: {
                    pageSize: 10
                },
                pager: {
                    visible: true,
                    allowedPageSizes: [5, 10, 100],
                    showPageSizeSelector: true,
                    showInfo: true,
                    showNavigationButtons: true
                },
                columns: [
                    { dataField: "PedidoClienteDetalleId", visible: false },
                    {
                        dataField: "ModeloBateria", caption: "Modelo"
                    },
                    {
                        dataField: "Marca", caption: "Modelo"
                    },
                    {
                        dataField: "NumeroParteCliente", caption: "Numero Parte"
                    },

                    {
                        dataField: "EtiquetaDatosTecnicos", caption: "Datos Tecnicos"
                    },
                    {
                        dataField: "Polaridad", caption: "Polaridad"
                    },
                    {
                        dataField: "TipoTerminal", caption: "Tipo Terminal"
                    },
                    {
                        dataField: "Cantidad", caption: "Cantidad"
                    },

                    {
                        dataField: "PrecioUnitario", caption: "Precio Unitario($)"
                    },
                    {
                        dataField: "PrecioTotal", caption: "Precio Total($)"
                    },
                    {
                        dataField: "PesoBateria", caption: "Peso Bateria(KG)"
                    },
                    {
                        dataField: "PesoNeto", caption: "Peso Total(KG)"
                    }

                ],
            });

        },
        error: function (msg) {
            $("#MensajeErrorInesperado").show('fade');
            setTimeout(function () {
                $("#MensajeErrorInesperado").fadeOut(1500);
            }, 3000); return;
        }
    })
}
