var numeroGlobalPedido = null;
var numeroOrden = null;
var temp;

$(document).ready(function () {
    $(".loading-icon").css("display", "none");
    $(document).on('click', '.fa', function (event) {
        event.preventDefault();
        $(this).closest('tr').remove();
    });
    $("#image").removeClass("hide");
});

$('#myModal').modal({
    backdrop: 'static',
    keyboard: false,
    show: false
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
function PedidosRecibidos() {
    $.ajax({
        url: "../Pedidos/ObtenerPalletIngresados",
        type: "GET"
        , success: function (msg) {
            //ItemsPedidoCliente = msg;  
            $("#tblPedidosRegistrados").dxDataGrid({
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
                        dataField: "FechaEmision", caption: "Fecha Emision", alignment: "left", dataType: "date"

                    },
                    {
                        dataField: "FechaRequerida", caption: "Fecha Requerida", alignment: "left", dataType: "date"


                    },
                    {
                        dataField: "TerminoImportacion", caption: "Termino Importacion", alignment: "left"
                    },
                    {
                        caption: "Acciones",
                        cellTemplate: function (container, options) {

                            var btnDetalle = "<button type='button' class='btn-primary' onclick='ConsultarDetallePedido(" + JSON.stringify(options.data) + ")'>Detalle</button>";

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

function ConsultarDetallePedido(modelo) {
    $("#txtTipoVenta").val("EXPORTACION");
    $("#txtVendedor").val("PLANTA");
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
        url: "../Pedidos/ObtenerDetallePedido?PedidoId=" + NumeroPedido,
        type: "GET"
        , success: function (msg) {
            temp = msg;

            $("#tblDetallePedido").dxDataGrid({
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
                editing: {
                    mode: "batch",
                    allowUpdating: true,
                    selectTextOnEditStart: true,
                    startEditAction: "click"
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
                        dataField: "ModeloBateria", caption: "Modelo",allowEditing: false
                    },
                    {
                        dataField: "Marca", caption: "Modelo",allowEditing: false
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
                        dataField: "Cantidad", caption: "Cantidad Solicitada", allowEditing: false

                    },
                    {
                        dataField: "CantidadConfirmada", caption: "Cantidad Confirmada"
                        ,
                        setCellValue: function (newData, value, currentRowData) {
                            newData.CantidadConfirmada = value;
                            newData.PrecioTotal = (currentRowData.PrecioUnitario * value).toFixed(2);
                            newData.PesoNeto = currentRowData.PesoBateria * value;

                        },
                    },

                    {
                        dataField: "PrecioUnitario", caption: "Precio Unitario($)", allowEditing: false
                    },
                    {
                        dataField: "PrecioTotal", caption: "Precio Total($)", allowEditing: false
                    },
                    {
                        dataField: "PesoBateria", caption: "Peso Bateria(KG)", allowEditing: false
                    },
                    {
                        dataField: "PesoNeto", caption: "Peso Total(KG)", allowEditing: false
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
                                return "Totales: "+(e.value)
                            }
                        }
                    }, {
                            column: "CantidadConfirmada",
                        summaryType: "sum",
                        valueFormat: "currency",
                        customizeText: function (e) {
                            if (e.value != 0 && e.value != "") {
                                $("#txtCantidadTotalConfirmada").val(e.value);
                                return (e.value)

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

                                return "$"+(e.value).toFixed(2)
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
                                return (e.value).toFixed(2)+"KG"
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
$('#RegistrarPedidoEnSap').on("click", function (e) {
    document.getElementById("FormularioRegistro").addEventListener('submit', validarFormularioRegistro);
});

function validarFormularioRegistro(evento) {
    evento.preventDefault();
    var txtFechaDocumento = document.getElementById('txtFechaDocumento').value;
    if (txtFechaDocumento.length == 0) {
        $("#MensajeValidacionFormulario").show('fade');
        setTimeout(function () {
            $("#MensajeValidacionFormulario").fadeOut(1500);
        }, 3000); return;
    }

    var txtFechaDespacho = document.getElementById('txtFechaDespacho').value;
    if (txtFechaDespacho.length == 0) {
        $("#MensajeValidacionFormulario").show('fade');
        setTimeout(function () {
            $("#MensajeValidacionFormulario").fadeOut(1500);
        }, 3000); return;
    }

    var txtTipoVenta = document.getElementById('txtTipoVenta').value;
    if (txtTipoVenta.length == 0) {
        $("#MensajeValidacionFormulario").show('fade');
        setTimeout(function () {
            $("#MensajeValidacionFormulario").fadeOut(1500);
        }, 3000); return;
    }

    var txtVendedor = document.getElementById('txtVendedor').value;
    if (txtVendedor.length == 0) {
        $("#MensajeValidacionFormulario").show('fade');
        setTimeout(function () {
            $("#MensajeValidacionFormulario").fadeOut(1500);
        }, 3000); return;
    }

    var txtObservaciones = document.getElementById('txtObservaciones').value;
    if (txtObservaciones.length == 0) {
        $("#MensajeValidacionFormulario").show('fade');
        setTimeout(function () {
            $("#MensajeValidacionFormulario").fadeOut(1500);
        }, 3000); return;
    }

    enviarRegistro();
}

function enviarRegistro() {

    $("#pleaseWaitDialog").modal("show");
    var progreso = 0;
    var idIterval = setInterval(function () {
        // Aumento en 5 el progeso
        progreso += 5;
        $('#BarraProceso').css('width', progreso + '%');
    }, 1000);


    $.ajax({
        url: "../Pedidos/RegistrarPedidoEnSap",
        type: "POST",
        data: {
            PedidoId: numeroGlobalPedido, Orden: numeroOrden,
            FechaDocumento: $('#txtFechaDocumento').val(), FechaDespacho: $('#txtFechaDespacho').val(), TipoVenta: $('#txtTipoVenta').val(), Vendedor: $('#txtVendedor').val(),
            Observaciones: $('#txtObservaciones').val(), array: temp, CantidadNueva: $('#txtCantidadTotalConfirmada').val(), PrecioNuevo: $('#txtPrecioTotal').val(), PesoNuevo: $('#txtPesoTotal').val(),
        },
        success: function (e) {
            console.log("el mensaje de respuesta es:"+e);
            if (e == "True") {
                PedidosRecibidos();
                clearInterval(idIterval);
                $("#pleaseWaitDialog").modal("hide");
                $("#ModalDetallePedido").modal("hide");

                $("#MensajeIngresoExitoso").show('fade');
                setTimeout(function () {
                    $("#MensajeIngresoExitoso").fadeOut(1500);
                }, 3000); return;
            } else {

                if (e =="Error") {
                    $("#pleaseWaitDialog").modal("hide");
                    $("#ModalDetallePedido").modal("hide");
                    $("#MensajeErrorConexionSap").show('fade');
                    setTimeout(function () {
                        $("#MensajeErrorConexionSap").fadeOut(1500);
                    }, 3000); return;
                }
                if (e == "Registrada") {
                    $("#pleaseWaitDialog").modal("hide");
                    $("#ModalDetallePedido").modal("hide");
                    $("#MensajeErrorIngreso").show('fade');
                    setTimeout(function () {
                        $("#MensajeErrorIngreso").fadeOut(1500);
                    }, 3000); return;
                }
                else {
                    $("#pleaseWaitDialog").modal("hide");
                    $("#ModalDetallePedido").modal("hide");

                    $("#MensajeErrorGeneralSap").text(e);
                    $("#MensajeErrorGeneralSap").show('fade');
                    setTimeout(function () {
                        $("#MensajeErrorGeneralSap").fadeOut(1500);
                    }, 3000); return;
                }

               
            }

        },
        error: function (msg) {
            $("#pleaseWaitDialog").modal("hide");
            $("#ModalDetallePedido").modal("hide");
            $("#MensajeErrorInesperado").show('fade');
            setTimeout(function () {
                $("#MensajeErrorInesperado").fadeOut(1500);
            }, 3000); return;
        }
    })
}


$('#CancelarPedido').on("click", function (e) {
    $("#ModalCancelacionPedido").modal("show");
});

$('#AfirmacionCancelacion').on("click", function (e) {
    ValidarCancelacion();
});

function ValidarCancelacion() {
    var txtObservaciones = document.getElementById('txtObservaciones').value;
    if (txtObservaciones.length == 0) {
        $("#ModalCancelacionPedido").modal("hide");
        $("#MensajeCancelacionDelPedido").show('fade');
        setTimeout(function () {
            $("#MensajeCancelacionDelPedido").fadeOut(1500);
        }, 3000); return;
    } else {
        RegistrarCancelacion();

    }

}
function RegistrarCancelacion() {
    $.ajax({
        url: "../Pedidos/CancelarPedido",
        type: "POST",
        data: {
            PedidoId: numeroGlobalPedido
        },
        success: function (e) {

            if (e == "True") {
                PedidosRecibidos();
                $("#ModalDetallePedido").modal("hide");
                $("#ModalCancelacionPedido").modal("hide");

                $("#MensajeCancelarIngreso").show('fade');
                setTimeout(function () {
                    $("#MensajeCancelarIngreso").fadeOut(1500);
                }, 3000); return;

            } else {
                $("#MensajeErrorCancelarIngreso").show('fade');
                setTimeout(function () {
                    $("#MensajeErrorCancelarIngreso").fadeOut(1500);
                }, 3000); return;
            }

        },
        error: function (msg) {
            $("#MensajeErrorInesperado").show('fade');
            setTimeout(function () {
                $("#MensajeErrorInesperado").fadeOut(1500);
            }, 3000); return;
        }
    })
}
