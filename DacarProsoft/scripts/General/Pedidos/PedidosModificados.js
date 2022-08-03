$(document).ready(function () {
    $(".loading-icon").css("display", "none");
    $(document).on('click', '.fa', function (event) {
        event.preventDefault();
        $(this).closest('tr').remove();
    });
    $("#image").removeClass("hide");
});

function ConsultarIngresosPedidos() {
    $(".result").text("");
    $(".loading-icon").removeClass("hide");
    $(".btn").attr("disabled", true);
    $(".btn-txt").text("Espere...");
    PedidosRecibidos();
}

$('#LinkClose').on("click", function (e) {
    $("#MensajeErrorInesperado").hide('fade');
});
$('#LinkClose2').on("click", function (e) {
    $("#MensajeIngresoExitoso").hide('fade');
});
$('#LinkClose3').on("click", function (e) {
    $("#MensajeErrorIngreso").hide('fade');
});
$('#LinkClose4').on("click", function (e) {
    $("#MensajeCancelarIngreso").hide('fade');
});
$('#LinkClose5').on("click", function (e) {
    $("#MensajeErrorCancelarIngreso").hide('fade');
});
$('#LinkClose6').on("click", function (e) {
    $("#MensajeValidacionFormulario").hide('fade');
});
$('#LinkClose7').on("click", function (e) {
    $("#MensajeCancelacionDelPedido").hide('fade');
});
$('#LinkClose10').on("click", function (e) {
    $("#MensajeErrorConexionSap").hide('fade');
});
$('#LinkClose11').on("click", function (e) {
    $("#MensajeErrorGeneralSap").hide('fade');
});

function getLocale() {
    const storageLocale = sessionStorage.getItem('locale');
    return storageLocale != null ? storageLocale : 'es';
}

function PedidosRecibidos() {
    $.ajax({
        url: "../Pedidos/ObtenerPedidosModificados",
        type: "GET"
        , success: function (msg) {

            //ItemsPedidoCliente = msg;  
            const locale = getLocale();
            DevExpress.localization.locale(locale);

            $("#tblPedidosRegistrados").dxDataGrid({
                dataSource: msg,
                keyExpr: 'NumeroPedidoId',
                showBorders: true,
                columnAutoWidth: true,
                allowColumnResizing: true,
                headerFilter: { visible: true },
                showBorders: true,
                paging: {
                    pageSize: 10
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
                columns: [

                    { dataField: "NumeroPedidoId", visible: false },
                    {
                        dataField: "NombreCliente", caption: "Cliente", alignment: "left"
                    },
                    {
                        dataField: "OrdenCompra", caption: "Orden Compra", allowFiltering: false
                    },
                    {
                        dataField: "Sucursal", caption: "Sucursal", alignment: "left"

                    },
                    {
                        dataField: "Pais", caption: "Pais", alignment: "left"

                    },
                    {
                        dataField: "FechaEmision", caption: "Fecha Emision", alignment: "left", dataType: "date"

                    },
                    {
                        dataField: "FechaRequerida", caption: "Fecha Requerida", alignment: "left", dataType: "date"


                    },
                    {
                        dataField: "TerminoImportacion", caption: "Termino Importacion", alignment: "left"
                    },
                    {
                        dataField: "EstadoPed", caption: "Estado", alignment: "left"
                    },
                    //{
                    //    caption: "Acciones",
                    //    cellTemplate: function (container, options) {

                    //        var btnDetalle = "<button type='button' class='btn-primary' onclick='ConsultarDetallePedido(" + JSON.stringify(options.data) + ")'>Detalle</button>";

                    //        $("<div>")
                    //            .append($(btnDetalle))
                    //            .appendTo(container);
                    //    }
                    //},
                    {
                        caption: "Accion", type: "buttons",
                        buttons: [{
                            text: "Detail",
                            icon: "menu",
                            hint: "Detail",
                            onClick: function (e) {
                                // Execute your command here
                                ConsultarDetallePedido(e.row.data);
                            }
                        }]
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

function ConsultarDetallePedido(modelo) {
    console.log("lo q llega: " + modelo)
    $("#txtTipoVenta").val("EXPORTACION");
    //$("#txtVendedor").val("PLANTA");
    $("#txtFechaDocumento").val("");
    $("#txtObservaciones").val("");
    $("#ModalDetallePedido").modal("show");
    $("#txtFechaDocumento").val(modelo.FechaEmision);
    numeroOrden = modelo.OrdenCompra;
    numeroGlobalPedido = modelo.NumeroPedidoId;
    DetalleFinal(modelo.NumeroPedidoId);
    DetallePedido(modelo.NumeroPedidoId);
}

function DetalleFinal(NumeroPedido) {
    $.ajax({
        url: "../Pedidos/ObtenerDetallaFinalPedido?PedidoId=" + NumeroPedido,
        type: "Get",
        success: function (msg) {
            DetalleCliente = msg;
            $("#txtObservaciones").val(msg[0]['Observaciones']);

        },
        error: function (msg) {
            $("#MensajeErrorInesperado").show('fade');
            setTimeout(function () {
                $("#MensajeErrorInesperado").fadeOut(1500);
            }, 3000); return;
        },
    })
}

function DetallePedido(NumeroPedido) {

    $.ajax({
        url: "../Pedidos/ObtenerDetalleActualizadoPedido?PedidoId=" + NumeroPedido,
        type: "GET"
        , success: function (msg) {
            temp = msg;
            const locale = getLocale();
            DevExpress.localization.locale(locale);

            $("#tblDetallePedido").dxDataGrid({
                dataSource: temp,
                columnAutoWidth: true,
                showBorders: true,
                keyExpr: "PedidoClienteDetalleId",
                allowColumnReordering: true,
                allowColumnResizing: true,

                headerFilter: false,
                filterPanel: false,
                filterRow: false,
                columnFixing: {
                    enabled: true
                },
                paging: {
                    pageSize: 10
                },
                //editing: {
                //    mode: "batch",
                //    allowUpdating: true,
                //    selectTextOnEditStart: true,
                //    startEditAction: "click"
                //},
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
                        dataField: "ModeloBateria", caption: "Modelo", allowEditing: false
                    },
                    {
                        dataField: "Marca", caption: "Marca", allowEditing: false
                    },
                    {
                        dataField: "NumeroParteCliente", caption: "Numero Parte", allowEditing: false
                    },

                    {
                        dataField: "EtiquetaDatosTecnicos", caption: "Datos Tecnicos", allowEditing: false
                    },
                    {
                        dataField: "Polaridad", caption: "Polaridad", allowEditing: false
                    },
                    {
                        dataField: "TipoTerminal", caption: "Tipo Terminal", allowEditing: false
                    },
                    {
                        dataField: "Cantidad", caption: "Cantidad Solicitada", allowEditing: false,
                        format: {
                            type: "fixedPoint",
                            precision: 2,

                        },
                        customizeText: function (cellInfo) {
                            const noTruncarDecimales = { maximumFractionDigits: 2, minimumFractionDigits: 2 };
                            return (cellInfo.value).toLocaleString('en-US', noTruncarDecimales);
                        }

                    },
                    {
                        dataField: "CantidadConfirmada", caption: "Cantidad Confirmada", allowEditing: false,
                        format: {
                            type: "fixedPoint",
                            precision: 2,

                        },
                        customizeText: function (cellInfo) {
                            const noTruncarDecimales = { maximumFractionDigits: 2, minimumFractionDigits: 2 };
                            return (cellInfo.value).toLocaleString('en-US', noTruncarDecimales);
                        }
                    },

                    {
                        dataField: "PrecioUnitario", caption: "Precio Unitario($)", allowEditing: false,
                        format: {
                            type: "fixedPoint",
                            precision: 2,

                        },
                        customizeText: function (cellInfo) {
                            const noTruncarDecimales = { maximumFractionDigits: 2, minimumFractionDigits: 2 };
                            return (cellInfo.value).toLocaleString('en-US', noTruncarDecimales);
                        }
                    },
                    {
                        dataField: "PrecioTotal", caption: "Precio Total($)", allowEditing: false,
                        format: {
                            type: "fixedPoint",
                            precision: 2,

                        },
                        customizeText: function (cellInfo) {
                            const noTruncarDecimales = { maximumFractionDigits: 2, minimumFractionDigits: 2 };
                            return (cellInfo.value).toLocaleString('en-US', noTruncarDecimales);
                        }
                    },
                    {
                        dataField: "PesoBateria", caption: "Peso Bateria(kg)", allowEditing: false,
                        format: {
                            type: "fixedPoint",
                            precision: 2,

                        },
                        customizeText: function (cellInfo) {
                            const noTruncarDecimales = { maximumFractionDigits: 2, minimumFractionDigits: 2 };
                            return (cellInfo.value).toLocaleString('en-US', noTruncarDecimales);
                        }
                    },
                    {
                        dataField: "PesoNeto", caption: "Peso Total(kg)", allowEditing: false,
                        format: {
                            type: "fixedPoint",
                            precision: 2,

                        },
                        customizeText: function (cellInfo) {
                            const noTruncarDecimales = { maximumFractionDigits: 2, minimumFractionDigits: 2 };
                            return (cellInfo.value).toLocaleString('en-US', noTruncarDecimales);
                        }
                    }

                ],
                summary: {
                    recalculateWhileEditing: true,
                    totalItems: [
                        {
                            column: "Cantidad",
                            summaryType: "sum",
                            valueFormat: "currency",
                            customizeText: function (e) {
                                if (e.value != 0 && e.value != "") {
                                    const noTruncarDecimales = { maximumFractionDigits: 0, minimumFractionDigits: 0 };
                                    ValTotal = (e.value).toLocaleString('en-US', noTruncarDecimales);

                                    return "Totales: " + (ValTotal)
                                }
                            }
                        }, {
                            column: "CantidadConfirmada",
                            summaryType: "sum",
                            valueFormat: "currency",
                            customizeText: function (e) {
                                if (e.value != 0 && e.value != "") {
                                    $("#txtCantidadTotalConfirmada").val(e.value);
                                    const noTruncarDecimales = { maximumFractionDigits: 0, minimumFractionDigits: 0 };
                                    ValTotal = (e.value).toLocaleString('en-US', noTruncarDecimales);
                                    return ValTotal
                                }
                            }
                        }
                        , {
                            column: "PrecioTotal",
                            summaryType: "sum",
                            valueFormat: "currency",
                            customizeText: function (e) {
                                if (e.value != 0 && e.value != "") {
                                    $("#txtPrecioTotal").val(e.value);
                                    const noTruncarDecimales = { maximumFractionDigits: 2, minimumFractionDigits: 2 };
                                    ValTotal = (e.value).toLocaleString('en-US', noTruncarDecimales);
                                    return "$" + ValTotal
                                }
                            }
                        }
                        , {
                            column: "PesoNeto",
                            summaryType: "sum",
                            valueFormat: "currency",
                            customizeText: function (e) {
                                if (e.value != 0 && e.value != "") {
                                    $("#txtPesoTotal").val(e.value);
                                    const noTruncarDecimales = { maximumFractionDigits: 2, minimumFractionDigits: 2 };
                                    ValTotal = (e.value).toLocaleString('en-US', noTruncarDecimales);
                                    return ValTotal + "kg"
                                }
                            }
                        }
                    ]
                },
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