var numeroGlobalPedido = null;
var numeroOrden = null;
var temp;
var tempTablaOrden = null;

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
$('#LinkCloseActPed').on("click", function (e) {
    $("#MensajeActualizacionPedido").hide('fade');
});
function PedidosRecibidos() {
    $.ajax({
        url: "../Pedidos/ObtenerPedidosIngresados",
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

    consultarEstadoPedido(modelo.NumeroPedidoId);

    console.log("lo q llega: " + modelo)
    $("#txtTipoVenta").val("EXPORTACION");
    $("#txtVendedor").val("PLANTA");
    $("#txtFechaDocumento").val("");
    $("#txtObservaciones").val("");
    $("#ModalDetallePedido").modal("show");
    $("#txtFechaDocumento").val(modelo.FechaEmision);
    numeroOrden = modelo.OrdenCompra;
    numeroGlobalPedido = modelo.NumeroPedidoId;
    DetalleFinal(modelo.NumeroPedidoId);
    DetallePedido(modelo.NumeroPedidoId, modelo.EstadoPed);
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


function getLocale() {
    const storageLocale = sessionStorage.getItem('locale');
    return storageLocale != null ? storageLocale : 'es';
}

function DetallePedido(NumeroPedido, estado) {
    var valorBool;
    var startUpdating = false;

    if (estado == "Solicitado") {
        document.getElementById("btnModificarPedido").disabled = false;
        valorBool = true;

    } else {
        document.getElementById("btnModificarPedido").disabled = true;
        valorBool = false;

    }

    $.ajax({
        url: "../Pedidos/ObtenerDetallePedido?PedidoId=" + NumeroPedido,
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
                editing: {
                    mode: "batch",
                    allowUpdating: valorBool,
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
                        dataField: "Marca", caption: "Marca",allowEditing: false
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
                        dataField: "CantidadConfirmada", caption: "Cantidad Confirmada", allowEditing: true
                        ,
                        setCellValue: function (newData, value, currentRowData) {
                            newData.CantidadConfirmada = value;
                            newData.PrecioTotal = (currentRowData.PrecioUnitario * value).toFixed(2);
                            newData.PesoNeto = currentRowData.PesoBateria * value;

                        },
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
                onRowUpdating: function (e) {
                    startUpdating = true;
                },
                onContentReady: function (e) {
                    if (startUpdating) {
                        startUpdating = false;
                        document.getElementById("btnModificarPedido").disabled = false;
                    }
                }, onEditorPreparing: function (e) {
                    e.editorOptions.onValueChanged = function (arg) {
                        document.getElementById("btnModificarPedido").disabled = true;
                        e.setValue(arg.value);
                    }
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
    //document.getElementById("FormularioRegistro").addEventListener('submit', validarFormularioRegistro);
    validarFormularioRegistro();
});

$('#btnModificarPedido').on("click", function (e) {
    //document.getElementById("FormularioRegistro").addEventListener('submit', validarFormularioRegistro);
    enviarActualizacionPedido();
});

function validarFormularioRegistro() {
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

function enviarActualizacionPedido() {
    document.getElementById("btnModificarPedido").disabled = true;

    $.ajax({
        url: "../Pedidos/ModificarPedidoCliente",
        type: "POST",
        data: {
            PedidoId: numeroGlobalPedido, array: temp, CantidadNueva: $('#txtCantidadTotalConfirmada').val(), PrecioNuevo: $('#txtPrecioTotal').val(), PesoNuevo: $('#txtPesoTotal').val(), Observacion: $('#txtObservaciones').val(),
            Orden: numeroOrden,
        },
        success: function (e) {

            if (e == "True") {

                PedidosRecibidos();

                $("#ModalDetallePedido").modal("hide");
                document.getElementById("btnModificarPedido").disabled = false;

                $("#MensajeActualizacionPedido").show('fade');
                setTimeout(function () {
                    $("#MensajeActualizacionPedido").fadeOut(1500);
                }, 3000); return;
                
            } else {
                $("#MensajeErrorIngreso").show('fade');
                setTimeout(function () {
                    $("#MensajeErrorIngreso").fadeOut(1500);
                }, 3000); return;
            }
        },
        error: function (msg) {
            $("#ModalDetallePedido").modal("hide");
            $("#ModalModificarPedido").modal("hide");

            $("#MensajeErrorInesperado").show('fade');
            setTimeout(function () {
                $("#MensajeErrorInesperado").fadeOut(1500);
            }, 3000); return;
        }
    })
}
function consultarEstadoPedido(valor) {
    $.ajax({
        url: "../Pedidos/AprobacionCliente",
        type: "POST",
        data: {
            PedidoId: valor
        },
        success: function (e) {

            if (e == "True") {
                document.getElementById("RegistrarPedidoEnSap").disabled = false;

            } else {
                document.getElementById("RegistrarPedidoEnSap").disabled = true;
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
function enviarRegistro() {
    document.getElementById("RegistrarPedidoEnSap").disabled = true;

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
                document.getElementById("RegistrarPedidoEnSap").disabled = false;

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
