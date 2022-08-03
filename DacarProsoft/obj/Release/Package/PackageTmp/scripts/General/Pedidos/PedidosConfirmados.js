var numeroGlobalPedido = null;
var numeroOrden = null;
var numeroGlobalPedidoFechas = null;
var estadoGobal = null;

$(document).ready(function () {
    $(".loading-icon").css("display", "none");
    $(document).on('click', '.fa', function (event) {
        event.preventDefault();
        $(this).closest('tr').remove();
    });
    $("#image").removeClass("hide");

   
});

function ConsultarIngresosPedidosConfirmados() {
    $(".result").text("");
    $(".loading-icon").removeClass("hide");
    $(".btn").attr("disabled", true);
    $(".btn-txt").text("Espere...");
    PedidosConfirmados();
}


$('#LinkClose').on("click", function (e) {
    $("#MensajeErrorInesperado").hide('fade');
});
$('#LinkClose2').on("click", function (e) {
    $("#MensajeErrorFechaAct").hide('fade');
});
$('#LinkClose3').on("click", function (e) {
    $("#MensajeActualizacionExitosa").hide('fade');
});
$('#LinkClose4').on("click", function (e) {
    $("#MensajeCompleteFecha").hide('fade');
});
$('#LinkClose5').on("click", function (e) {
    $("#MensajeErrorActualizacionEstado").hide('fade');
});
$('#LinkClose6').on("click", function (e) {
    $("#MensajeActualizacionExitosaEstado").hide('fade');
});


function PedidosConfirmados() {
    var valor = $("#SelectTipoEstado option:selected").val();
    $.ajax({
        url: "../Pedidos/ObtenerPedidosGenerales?estado=" + valor,
        type: "GET"
        , success: function (msg) {
            //ItemsPedidoCliente = msg;  
            $("#tblPedidosConfirmados").dxDataGrid({
                dataSource: msg,
                keyExpr: 'NumeroPedidoId',
                showBorders: true,
                columnAutoWidth: true,
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
                        dataField: "OrdenCompra", caption: "Orden Compra"
                    },
                    {
                        dataField: "Pais", caption: "Pais", alignment: "left"

                    },
                    {
                        dataField: "FechaEmision", caption: "Fecha Emision", alignment: "left"

                    },
                    {
                        dataField: "FechaIngresadaSap", caption: "Fecha Ingresada SAP", alignment: "left", dataType: "date"

                    },
                    {
                        dataField: "FechaNuevaDespacho", caption: "Fecha Despacho ", alignment: "left", dataType: "date"


                    },
                    {
                        dataField: "TerminoImportacion", caption: "Termino Importacion", alignment: "left"
                    },
                    { dataField: "FechaCargaLista", visible: false },
                    { dataField: "FechaDespachoPuerto", visible: false },
                    { dataField: "FechaZarpe", visible: false },
                    { dataField: "FechaArribo", visible: false },
                    { dataField: "FechaEntrega", visible: false },
                    { dataField: "Estado", visible: false },

                    
                    {
                        caption: "Acciones",
                        cellTemplate: function (container, options) {

                            var btnDetalle = "<button type='button' class='btn-primary' onclick='ConsultarDetallePedidoAprobado(" + JSON.stringify(options.data) + ")'>Detalle</button>";
                            var lblEspacio = "<a> </a>"
                            var btnPackingList = "<button class='btn-warning' onclick='IngresoFechas(" + JSON.stringify(options.data) + ")'>ActualizarFechas</button>";

                            $("<div>")
                                .append($(btnDetalle), $(lblEspacio), $(btnPackingList))
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
    estadoGobal = modelo.Estado;
    console.log("el estado es:" + estadoGobal);
    $("#EstadoPedido").empty();

    //$("#EstadoPedido").append('<option value="1">Solicitado</option>');
    $("#EstadoPedido").append('<option value="2">Confirmado</option>');
    $("#EstadoPedido").append('<option value="3">Despachado a Puerto</option>');
    $("#EstadoPedido").append('<option value="4">En Transito</option>');
    $("#EstadoPedido").append('<option value="5">Entregado</option>');
    $("#EstadoPedido").append('<option value="6">Cancelado</option>');


    $("#EstadoPedido").val(estadoGobal);



    $("#txtCargaLista").val(modelo.FechaCargaLista);
    $("#txtFechaDespachoPuerto").val(modelo.FechaDespachoPuerto);
    $("#txtFechaZarpe").val(modelo.FechaZarpe);
    $("#txtFechaArribo").val(modelo.FechaArribo);
    $("#txtFechaEntrega").val(modelo.FechaEntrega);

    $("#ModalDetallePedidoAprobada").modal("show");
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
            //$("#txtCantidadPedidoAprobada").val(msg[0]['CantitadTotal']);
            $("#txtCantidadPedidoAprobada").val(msg[0]['CantidadTotalNueva']);

            $("#txtPrecioFinalAprobada").val(msg[0]['PrecioFinalPedido']);
            
            $("#txtPesoNetoAprobada").val(msg[0]['PesoNetoFinalPedido']);
            $("#txtObservacionesAprobada").val(msg[0]['Observaciones']);
         

        },
        error: function (msg) {
            console.log("Error");
        },
    })
}

function DetallePedido(NumeroPedido) {
    $.ajax({
        url: "../Pedidos/ObtenerDetallePedidoConfirmado?PedidoId=" + NumeroPedido,
        type: "GET"
        , success: function (msg) {
            temp = msg;

            $("#tblDetallePedidoConfirmado").dxDataGrid({
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
                        dataField: "Cantidad", caption: "Cantidad Inicial"
                    },
                    {
                        dataField: "CantidadConfirmada", caption: "Cantidad Confirmada"
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


function IngresoFechas(modelo) {
    numeroGlobalPedidoFechas = modelo.NumeroPedidoId;
    //NumeroPedidoId
    VerificarFecha();
    $("#ModalActualizarFechas").modal("show");

}


function VerificarFecha() {
    $.ajax({
        url: "../Pedidos/BusquedaFechasPedido?PedidoId=" + numeroGlobalPedidoFechas + "&FechaId=" + $("#SelectTiposFecha option:selected").val(),
        type: "Get",
        success: function (msg) {
            if (msg != "No") {
                document.getElementById('txtFechaRegistrada').value = msg;
            } else {
                document.getElementById('txtFechaRegistrada').value = "";
            }
         
        },
        error: function (msg) {
            console.log("Error");
        },
    })
   
}

$('#ActualizarFechasPedido').on("click", function (e) {

    if (txtFechaRegistrada.length == 0) {
        $("#MensajeCompleteFecha").show('fade');
        setTimeout(function () {
            $("#MensajeCompleteFecha").fadeOut(1500);
        }, 3000); return;
    }
    else {
        $.ajax({
            url: "../Pedidos/GuardarFecha",
            type: "POST",
            data: {
                PedidoId: numeroGlobalPedidoFechas, FechaId: $("#SelectTiposFecha option:selected").val(), FechaIng: $('#txtFechaRegistrada').val(),
            },
            success: function (msg) {
                PedidosConfirmados();
                VerificarFecha();
                $("#MensajeActualizacionExitosa").show('fade');
                setTimeout(function () {
                    $("#MensajeActualizacionExitosa").fadeOut(1500);
                }, 3000);
            },
            error: function (msg) {
                $("#MensajeErrorFechaAct").show('fade');
                setTimeout(function () {
                    $("#MensajeErrorFechaAct").fadeOut(1500);
                }, 3000);

            }
        })
    } 
});

$('#ActuaizarEstadoPedido').on("click", function (e) {
    $("#ModalCambioEstado").modal("show");
});

$('#AfirmacionCambioEstado').on("click", function (e) {

    if (estadoGobal != $("#EstadoPedido option:selected").val()) {
        $.ajax({
            url: "../Pedidos/ActualizarEstado",
            type: "POST",
            data: {
                PedidoId: numeroGlobalPedido, Estado: $("#EstadoPedido option:selected").val(),
            },
            success: function (msg) {
                console.log("esto me envia el ajax" + msg);
                if (msg == "True") {
                    PedidosConfirmados();
                    $("#ModalCambioEstado").modal("hide");
                    $("#ModalDetallePedidoAprobada").modal("hide");
                    $("#MensajeActualizacionExitosaEstado").show('fade');
                    setTimeout(function () {
                        $("#MensajeActualizacionExitosaEstado").fadeOut(1500);
                    }, 3000);
                }
                else {
                    $("#MensajeErrorInesperado").show('fade');
                    setTimeout(function () {
                        $("#MensajeErrorInesperado").fadeOut(1500);
                    }, 3000);
                }
          
                
            },
            error: function (msg) {
                $("#MensajeErrorInesperado").show('fade');
                setTimeout(function () {
                    $("#MensajeErrorInesperado").fadeOut(1500);
                }, 3000);

            }
        })
    }
    else {
        $("#ModalCambioEstado").modal("hide");

        $("#MensajeErrorActualizacionEstado").show('fade');
        setTimeout(function () {
            $("#MensajeErrorActualizacionEstado").fadeOut(1500);
        }, 3000);
    }
});

