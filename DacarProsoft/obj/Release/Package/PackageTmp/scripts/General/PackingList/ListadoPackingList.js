var data = null;
var IdentificadorDetalle = null;
var NumeroPalletLocal = null;
var temp = null;
var temp2 = null;
var tempPalletId = null;
var tempPackingId = null;
var IdentificadorPaking = null;
var valor1 = null;
var valor2 = null;
var PackingIdentificador = null;
var NombreCliente = null;
var NumeroPedido = null;
var IngresoDetallePacking = null;
var etiqueta = null;
var etiquetaPalletPacking = null;
var CantidadPallets = null;
var GuiaPackingList = null;
var DestinoPacking = null;
var ContenedorNumer = null;
var valorDocEntry = null;
var detFactImpr = null;
var formaPago = null;
$(document).ready(function () {

    $("#txtNuevoLargoPallet").val(114);
    $("#txtNuevoAltoPallet").val(114);
    $("#txtNuevoAnchoPallet").val(114);
    $(".loading-icon").css("display", "none");
    $(document).on('click', '.fa', function (event) {
        event.preventDefault();
        $(this).closest('tr').remove();
    });
    $("#image").removeClass("hide");
});
//txtAnchoPallet
function ConsultarIngresosPacking() {
    $(".result").text("");
    $(".loading-icon").removeClass("hide");
    $(".btn").attr("disabled", true);
    $(".btn-txt").text("Espere...");
    mostrarIngresosPallet();
}


function Volumen() {
    var total = ((parseFloat($('#txtLargoPallet').val()) * parseFloat($('#txtAnchoPallet').val()) * parseFloat($('#txtAltoPallet').val())).toFixed(2))/1000000;
    $('#txtVolumenPallet').val(total.toFixed(3));
}
//txtNuevoAnchoPallet

function Volumen2() {
    var total = ((parseFloat($('#txtNuevoLargoPallet').val()) * parseFloat($('#txtNuevoAnchoPallet').val()) * parseFloat($('#txtNuevoAltoPallet').val())).toFixed(2))/1000000;
    $('#txtNuevoVolumenPallet').val(total.toFixed(3));
}
function mostrarIngresosPallet() {
    var valor = $("#TipoBusqueda").val();
    $.ajax({
        url: "../PackingList/ObtenerPalletIngresados?tipo=" + valor,
        type: "GET"
             , success: function (msg) {
                 ConfigDev.dataSource = msg;
                 ConfigDev.keyExpr = "PackingId",
                 ConfigDev.columnAutoWidth = true,
                 ConfigDev.showBorders = true,
                  ConfigDev.allowColumnReordering = true,
                 //ConfigDev.allowColumnResizing = true,
                    ConfigDev.filterRow = { visible: false },
                     ConfigDev.filterPanel = { visible: true },
                     ConfigDev.headerFilter = { visible: true },
                   ConfigDev.columnFixing = {
                       enabled: true
                     },
                     ConfigDev.searchPanel= {
                     visible: true,
                         width: 240,
                             placeholder: "Buscar..."
                 },
                 ConfigDev.columns = [
                     { dataField: "PackingId", visible: false },
                       {
                           dataField: "NumeroDocumento", caption: "Numero Documento", allowEditing: false, allowHeaderFiltering: false
                       },
                       {
                           dataField: "NumeroOrden", caption: "Numero Orden", allowEditing: false, headerFilter: false, allowHeaderFiltering: false, headerFilter: {
                               allowSearch: true,
                           }
                       },
                      {
                          dataField: "NombreCliente", caption: "Cliente", allowEditing: false, headerFilter: false, allowHeaderFiltering: false, headerFilter: {
                              allowSearch: true,
                          }
                     },
                     {
                         dataField: "Mes", caption: "Mes", allowEditing: false, headerFilter: true, allowHeaderFiltering: true, alignment: "right",
                     },
                     {
                         dataField: "FechaRegistro", caption: "Fecha Registro", allowEditing: false, headerFilter: false, allowHeaderFiltering: false
                     },
                      {
                          dataField: "Origen", visible: false
                      },
                       {
                           dataField: "Destino", visible: false
                     },
                     {
                         dataField: "NumeroContenedor", caption: "Contenedor", allowEditing: false, headerFilter: false, allowHeaderFiltering: false
                     },
                       {
                           dataField: "CantidadPallet", caption: "Pallet Totales", alignment: "right", allowEditing: false, headerFilter: false, allowHeaderFiltering: false
                       }, {
                           dataField: "PalletFaltantes", caption: "Pallet Faltantes", allowEditing: false, headerFilter: false, allowHeaderFiltering: false
                       },
                       {
                           dataField: "Estado", caption: "Estado", alignment: "right", allowEditing: false
                       },
                       ,
                       {
                           dataField: "DetalleIngresado", caption: "Detalle Ingresado", alignment: "right", allowEditing: false
                       },
                      {
                          caption: "Acciones",
                          cellTemplate: function (container, options) {

                              var btEliminar = "<button class='btn-primary' onclick='EliminarPackingCompl(" + JSON.stringify(options.data) + ")'>Eliminar</button>";
                              var btnDetalle = "<button class='btn-success' onclick='ModalConsultarPalletsIngresado(" + JSON.stringify(options.data) + ")'>Consultar Packing List</button>";
                              var btnPackingList = "<button class='btn-primary' onclick='ConsultaEstado(" + JSON.stringify(options.data) + ")'>Ingreso Pallets</button>";

                              //var btEliminar = "<i class='fas fa-trash-alt' onclick=" + "'EliminarPackingCompl(" + JSON.stringify(options.data) + ")'> </i>";
                              //var btnDetalle = "<button class='btn-primary' onclick='ModalConsultarPalletsIngresado(" + JSON.stringify(options.data) + ")'>Pallets</button>";
                              var lblEspacio = "<a> </a>"
                              //var btnPackingList = "<button class='btn-warning' onclick='ConsultaEstado(" + JSON.stringify(options.data) + ")'>Ingreso Pallets</button>";

                              $("<div>")
                                  .append($(btnDetalle), $(lblEspacio), $(btnPackingList), $(lblEspacio), $(btEliminar))
                                  .appendTo(container);
                          }
                      }     
                 ];
                 $(".btn").attr("disabled", false);
                 $(".btn-txt").text("Consultar");
                 $("#tblIngresosdePacking").dxDataGrid(ConfigDev);
             },
        error: function (msg) {
            $("#MensajeErrorGeneral").show('fade');
            setTimeout(function () {
                $("#MensajeErrorGeneral").fadeOut(1500);
            }, 3000);
        }
    })
}
function EliminarPackingCompl(modelo) {
    tempPackingId = modelo.PackingId;
    $("#ModalEliminarPacking").modal("show");
}

function ModalConsultarPalletsIngresado(modelo) {
    console.log("Consulta");
    console.log(modelo.NombreCliente);
    console.log(modelo.NumeroOrden);
    NombreCliente =modelo.NombreCliente ;
    NumeroPedido = modelo.NumeroOrden;
    IngresoDetallePacking = modelo.DetalleIngresado;
    CantidadPallets = modelo.CantidadPallet;
    PackingIdentificador = modelo.PackingId;
    ContenedorNumer = modelo.NumeroContenedor;
    DestinoPacking = modelo.Destino;
    $.ajax({
        url: "../PackingList/ObtenerPalletList?PackingId=" + modelo.PackingId,
        type: "GET"
           , success: function (msg) {
               ConfigDev.dataSource = msg;
               ConfigDev.keyExpr = "PalletPacking1",
               ConfigDev.columnAutoWidth = true,
               ConfigDev.showBorders = true,
                ConfigDev.allowColumnReordering = true,
               //ConfigDev.allowColumnResizing = true,
                   ConfigDev.filterRow = { visible: false },
                   ConfigDev.filterPanel = { visible: false },
                   ConfigDev.headerFilter = { visible: false },
                   ConfigDev.columnFixing = {
                     enabled: true
                   },
                   ConfigDev.paging= { 
                   pageSize: 30,
                   },
                   ConfigDev.pager= {
                   visible: false,
                   },
               ConfigDev.columns = [
                   { dataField: "PalletPacking1", visible: false },
                   { dataField: "PackingId", visible: false },
                   {
                       dataField: "PalletNumber", caption: "# Pallet", allowEditing: false, width:60
                     },
                     {
                         dataField: "AnchoPallet", visible: false
                     },
                    {
                        dataField: "LargoPallet", visible: false
                    },
                    {
                        dataField: "AltoPallet", visible: false
                    },
                    {
                        dataField: "Cantidad", caption: "Cantidad Items", allowEditing: false
                    },
                     {
                         dataField: "VolumenPallet", caption: "Volumen(m³)", allowEditing: false,
                         format: {
                             type: "fixedPoint",
                             precision: 2,
                         },
                     },
                     {
                         dataField: "PesoNeto", caption: "Peso Neto(kg)", alignment: "right",
                         format: {
                             type: "fixedPoint",
                             precision: 2,
                         },
                     }, {
                         dataField: "PesoBruto", caption: "Peso Bruto(kg)", allowEditing: false,
                         format: {
                             type: "fixedPoint",
                             precision: 2,
                         },
                     },                 
                    {
                        caption: "Acciones",
                        cellTemplate: function (container, options) {
                            //var btnDetalle = "<button class='btn-primary' onclick='ModalConsultarDetallePalletIngresado(" + JSON.stringify(options.data) + ")'>Detalle</button>";
                            var lblEspacio = "<a> </a>"
                            //var btnPackingList = "<button class='btn-warning' onclick='ConsultarEtiqueta(" + JSON.stringify(options.data) + ")'>Imprimir</button>";
                            var btnEliminar = "<button class='btn-warning' onclick='EliminarPallet(" + JSON.stringify(options.data) + ")'>Eliminar</button>";
                            var btnDetalle = "<button class='btn-primary' onclick='ModalConsultarDetallePalletIngresado(" + JSON.stringify(options.data) + ")'>Detalle</button>";
                            var btnPackingList = "<button class='btn-primary' onclick='ConsultarEtiqueta(" + JSON.stringify(options.data) + ")'>Imprimir</button>";
                            //var btnEliminar = "<i class='fas fa-trash-alt' onclick='EliminarPallet(" + JSON.stringify(options.data) + ")'> </i>";
                            $("<div>")
                                .append($(btnDetalle), $(lblEspacio), $(btnPackingList), $(lblEspacio), $(btnEliminar))
                                .appendTo(container);
                        }
                    }
               ];

               $("#tblListadoPalletsIngresados").dxDataGrid(ConfigDev);
           },
        error: function (msg) {
            $("#MensajeErrorGeneral").show('fade');
            setTimeout(function () {
                $("#MensajeErrorGeneral").fadeOut(1500);
            }, 3000);
        }
    })
    $("#ModalListadoDePallets").modal("show");
}

$('#AfirmacionEliminacion').on("click", function (e) {
    AfirmacionEliminacion();
});
$('#AfirmacionEliminacionPacking').on("click", function (e) {
    AfirmacionEliminacionPackin();
});

$('#btnActualizarCantidadPallets').on("click", function (e) {
    $("#ModalActualizarPallet").modal("show");
    $('#txtCantidadActualPallets').val(CantidadPallets);
    ;
});

$('#ActualizarPalletsPacking').on("click", function (e) {
    var valorNuevaCant = $('#txtNuevaCantidadPallets').val();
    if (valorNuevaCant.length == 0 || valorNuevaCant==0) {
        $("#MensajePackingValorCeroPallets").show('fade');
        setTimeout(function () {
            $("#MensajePackingValorCeroPallets").fadeOut(1500);
        }, 3000);
        return;
    }
    actualizarValorPallets();
    PackingIdentificador;
});

function actualizarValorPallets() {
    $.ajax({
        url: "../PackingList/ActualizarCantidadPallet",
        type: "POST",
        data: {
            PackingId: PackingIdentificador, NuevaCantidad: $('#txtNuevaCantidadPallets').val()
        },
        success: function (msg) {
            if (msg == 'True') {
                mostrarIngresosPallet();
                $("#ModalActualizarPallet").modal("hide");
                $("#ModalListadoDePallets").modal("hide");

                  $("#MensajeActulalizacionCorrecta").show('fade');
                  setTimeout(function () {
                      $("#MensajeActulalizacionCorrecta").fadeOut(1500);
                  }, 3000);
                  
            } else {
                  $("#MensajeErrorGuardado").show('fade');
                  setTimeout(function () {
                      $("#MensajeErrorGuardado").fadeOut(1500);
                  }, 3000);
            }
        },
        error: function (msg) {
            $("#MensajeErrorGeneral").show('fade');
            setTimeout(function () {
                $("#MensajeErrorGeneral").fadeOut(1500);
            }, 3000);
        }
    })
}

function EliminarPallet(modelo) {
    tempPalletId = modelo.PalletPacking1;
    $("#ModalEliminar").modal("show");
}

function AfirmacionEliminacion() {
    $.ajax({
        url: "../PackingList/EliminarPallet",
        type: "POST",
        data: {
            PalletId: tempPalletId,
        },
        success: function (msg) {
;            if (msg == 'True') {
                $("#ModalEliminar").modal("hide");
                $("#ModalListadoDePallets").modal("hide");
                mostrarIngresosPallet();
                $("#MensajeEliminacionCorecta").show('fade');
                setTimeout(function () {
                    $("#MensajeEliminacionCorecta").fadeOut(1500);
                }, 3000);
            } else {
                $("#ModalEliminar").modal("hide");
                $("#MensajeEliminacionIncorecta").show('fade');
                setTimeout(function () {
                    $("#MensajeEliminacionIncorecta").fadeOut(1500);
                }, 3000);
            }
        },
        error: function (msg) {
            $("#MensajeErrorGeneral").show('fade');
            setTimeout(function () {
                $("#MensajeErrorGeneral").fadeOut(1500);
            }, 3000);
        }
    })
}

function AfirmacionEliminacionPackin() {
    $.ajax({
        url: "../PackingList/EliminarPackingCompleto",
        type: "POST",
        data: {
            PackingId: tempPackingId,
        },
        success: function (msg) {
            ; if (msg == 'True') {
                $("#ModalEliminarPacking").modal("hide");
                mostrarIngresosPallet();
                $("#MensajeEliminacionCorecta").show('fade');
                setTimeout(function () {
                    $("#MensajeEliminacionCorecta").fadeOut(1500);
                }, 3000);
            } else {
                $("#ModalEliminarPacking").modal("hide");
                $("#MensajeEliminacionIncorecta").show('fade');
                setTimeout(function () {
                    $("#MensajeEliminacionIncorecta").fadeOut(1500);
                }, 3000);
            }

        },
        error: function (msg) {
            $("#MensajeErrorGeneral").show('fade');
            setTimeout(function () {
                $("#MensajeErrorGeneral").fadeOut(1500);
            }, 3000);
        }
    })
}

function ModalConsultarDetallePalletIngresado(modelo) {
    $.ajax({
        url: "../PackingList/ObtenerDetallePalletIngresados?PackingId=" + modelo.PackingId + "&PalletId=" + modelo.PalletPacking1,
        type: "GET"
           , success: function (msg) {
               $('#txtIdPallet').val(msg[0].PalletPacking1);
               valor1 = msg[0].PalletPacking1;
               $('#txtIdPacking').val(msg[0].PackingId);
               valor2 = msg[0].PackingId;
               $('#lblNumberPalletNuevo').html("Pallet #" + msg[0].PalletNumber);
               $('#txtLargoPallet').val(msg[0].LargoPallet);
               $('#txtAnchoPallet').val(msg[0].AnchoPallet);
               $('#txtAltoPallet').val(msg[0].AltoPallet);
               $('#txtVolumenPallet').val(msg[0].VolumenPallet);
               $('#txtPesoNeto').val(msg[0].PesoNeto);
               $('#txtPesoBruto').val(msg[0].PesoBruto);
               ModalConsultarDetalleTablaPackingIngresado();
           },
        error: function (msg) {
            $("#MensajeErrorGeneral").show('fade');
            setTimeout(function () {
                $("#MensajeErrorGeneral").fadeOut(1500);
            }, 3000);
        }
    })

}


function ConsultarEtiqueta(modelo) {
    $("#ModalAfirmacionFondo").modal("show");
    etiquetaPalletPacking = modelo.PalletPacking1;
}

$('#AfirmacionEticketa').on("click", function (e) {
    etiqueta = "NO";
    generarPDF(etiquetaPalletPacking, etiqueta);
    $("#ModalAfirmacionFondo").modal("hide");

});

$('#NgacionEticketa').on("click", function (e) {
    etiqueta = "SI";
    generarPDF(etiquetaPalletPacking, etiqueta);
    $("#ModalAfirmacionFondo").modal("hide");

});

function generarPDF(variable,variable2) {
    var url = "../PackingList/PalletPdf3?PackingId=" + PackingIdentificador + "&PalletId=" + variable + "&Fondo=" + variable2;
    window.open(url);
    $("#ModalListadoDePallets").modal("hide");
}

function ModalConsultarDetalleTablaPackingIngresado() {
    $.ajax({
        url: "../PackingList/ConsultarPalletsDetalleIngreseados?PackingId=" + valor2 + " &PalletId=" + valor1,
        type: "GET"
           , success: function (msg) {
               ConfigDev.dataSource = msg;
               ConfigDev.keyExpr = "ItemCode",
               ConfigDev.columnAutoWidth = true,
               ConfigDev.showBorders = true,
                ConfigDev.allowColumnReordering = true,
               //ConfigDev.allowColumnResizing = true,
                  ConfigDev.filterRow = { visible: false },
                   ConfigDev.filterPanel = { visible: false },
                   ConfigDev.headerFilter = { visible: false },
                 ConfigDev.columnFixing = {
                     enabled: true
                 },
               ConfigDev.columns = [

                   { dataField: "PalletPackingDetalleId", visible: false },
                   { dataField: "PalletPacking1", visible: false },
                   { dataField: "PackingId", visible: false },
                     {
                         dataField: "ItemCode", caption: "Codigo Articulo", allowEditing: false
                     },
                     {
                         dataField: "DescriptionCode", caption: "Modelo Articulo", allowEditing: false
                     },
                    {
                        dataField: "CantidadItem", caption: "Cantidad Articulo", allowEditing: false
                    }
               ];
               $("#tblDetallePalletTablaIngresado").dxDataGrid(ConfigDev);
           },
           error: function (msg) {
               $("#MensajeErrorGeneral").show('fade');
               setTimeout(function () {
                   $("#MensajeErrorGeneral").fadeOut(1500);
               }, 3000);
           }
    })
    $("#ModalPalletIngresado").modal("show");
}
function VerNumeroPallet(packingId) {
    $.ajax({
        url: "../PackingList/ObtenerNumeroPallet?IdentificadorPacking=" + packingId,
        type: "GET",
        success: function (msg) {
            NumeroPalletLocal = msg;
            $('#lblNumberPalletNuevo').html("Pallet  #" + msg);
            $('#txtNumeroPalle').val(msg);
        },
        error: function (msg) {

            $("#MensajeErrorGeneral").show('fade');
            setTimeout(function () {
                $("#MensajeErrorGeneral").fadeOut(1500);
            }, 3000);
            NumeroPalletLocal = 0;
        }
    })
}

function VerNumeroPalletIngreso(packingId) {
    $.ajax({
        url: "../PackingList/ObtenerNumeroPallet?IdentificadorPacking=" + packingId,
        type: "GET",
        success: function (msg) {
            NumeroPalletLocal = msg;
            $('#lblNumberPalletNuevo').html("Pallet  #" + msg);
            $('#txtNumeroPalle').val(msg);
        },
        error: function (msg) {
            $("#MensajeErrorGeneral").show('fade');
            setTimeout(function () {
                $("#MensajeErrorGeneral").fadeOut(1500);
            }, 3000);
            NumeroPalletLocal = 0;

        }
    })
}
function ConsultaEstado(modelo) {
    $.ajax({
        url: "../PackingList/ConsultarEstadoPacking?PackingId=" + modelo.PackingId,
        type: "GET",
        success: function (msg) {

            if (msg == "False") {
                IngresoNuevoPacking(modelo);
            }
            else {
                $("#MensajePackingCompletos").show('fade');
                setTimeout(function () {
                    $("#MensajePackingCompletos").fadeOut(1500);
                }, 3000);
            }
        },
        error: function (msg) {
            $("#MensajeErrorGeneral").show('fade');
            setTimeout(function () {
                $("#MensajeErrorGeneral").fadeOut(1500);
            }, 3000);
        }
    })
}
$('#LinkClose5').on("click", function (e) {
    $("#MensajeGuardado").hide('fade');
});
$('#LinkClose6').on("click", function (e) {
    $("#MensajeErrorGuardado").hide('fade');
});
$('#LinkClose7').on("click", function (e) {
    $("#MensajeErrorGeneral").hide('fade');
});
$('#LinkClose10').on("click", function (e) {
    $("#MensajePackingCompletos").hide('fade');
});
$('#LinkClose11').on("click", function (e) {
    $("#MensajeEliminacionCorecta").hide('fade');
});
$('#LinkClose12').on("click", function (e) {
    $("#MensajeEliminacionIncorecta").hide('fade');
});
$('#LinkClose13').on("click", function (e) {
    $("#MensajeActualizado").hide('fade');
});
$('#LinkCloseCeroPallets').on("click", function (e) {
    $("#MensajePackingValorCeroPallets").hide('fade');
});

function IngresoNuevoPacking(modelo) {
    document.getElementById("RegistrarPallet").disabled = true;
    var startUpdating = false;

    $("#txtNuevoLargoPallet").val("");
    $("#txtNuevoAltoPallet").val("");
    $("#txtNuevoAnchoPallet").val("");
    $("#txtNuevoVolumenPallet").val("");
    $("#txtNuevoPesoNeto").val("");
    $("#txtNuevoPesoBruto").val("");
    $("#txtNuevoLargoPallet").val(114);
    $("#txtNuevoAltoPallet").val(114);
    $("#txtNuevoAnchoPallet").val(114);

    $("#txtNuevoVolumenPallet").val("");
    Volumen2();

    IdentificadorPaking = modelo.PackingId;
    VerNumeroPalletIngreso(modelo.PackingId);

    $.ajax({
        url: "../PackingList/ObtenerDetallePallet?IdentificadorPacking=" + modelo.PackingId,
        type: "GET"
          , success: function (msg) {
              temp = msg;
              temp2 = msg;
              ConfigDev.dataSource = temp;
              ConfigDev.columnAutoWidth = true,
              ConfigDev.showBorders = true,
              ConfigDev.keyExpr = "ItemCode",
               ConfigDev.allowColumnReordering = true,
               ConfigDev.headerFilter = false,
               ConfigDev.filterPanel = false,
               ConfigDev.filterRow = false,
               ConfigDev.selection = {
                   mode: "single"
                  }, 
               ConfigDev.editing = {
                      mode: "batch",
                      allowUpdating: true,
                      selectTextOnEditStart: true,
                      startEditAction: "click",
                      confirmDelete: false

               },

              //ConfigDev.onBeforeSend= function(method, ajaxOptions) {
              //    ajaxOptions.xhrFields = { withCredentials: true };
              //}
              //ConfigDev.repaintChangesOnly= true, 
              //     ConfigDev.editing = {
              //         mode: "batch",
              //         allowUpdating: true,
              //         selectTextOnEditStart: true,
              //         startEditAction: "click"
              //     },
                ConfigDev.columnFixing = {
                    enabled: true
                },
              ConfigDev.columns = [

                  { dataField: "PackingDtlId", visible: false },
                   {
                       dataField: "ItemCode", caption: "Codigo Item", allowEditing: false,
                   },
                    {
                        dataField: "DescriptionItem", caption: "Modelo", fixed: true, allowEditing: false
                    },
                    {
                        dataField: "CantidadItem", caption: "Cantidad Pedido", allowEditing: false
                    },
                   {
                       dataField: "Pallet", caption: "Cantidad en Pallet",
                       
                       setCellValue: function (newData, value, currentRowData) {
                           newData.Pallet = value;

                           newData.TotalItem = currentRowData.TotalItem2 + value;
                           newData.SaldoItem = currentRowData.SaldoItem2 - value;
                       }
                   },
                   {
                       dataField: "TotalItem", caption: "Acumulado", allowEditing: false
                  },
                  { dataField: "TotalItem2", visible: false },

                    {
                        dataField: "SaldoItem", caption: "Saldo Pendiente", allowEditing: false
                  },
                  { dataField: "SaldoItem2", visible: false },

                       {
                           dataField: "Status", caption: "Estado", allowEditing: false
                       }

                  ];
              ConfigDev.summary= {
                  recalculateWhileEditing: true,
                      totalItems: [
                          {
                              name: "DescriptionItem",
                              column: "DescriptionItem",
                              summaryType: "count",
                              displayFormat: "Total: {0}",
                              showInColumn: "DescriptionItem",
                              customizeText: function (e) {
                                  if (e.value != 0 && e.value != "") {
                                      return "Totales:"
                                  }
                              }
                          }, {
                              name: "CantidadItem",
                              column: "CantidadItem",
                              summaryType: "sum",
                              displayFormat: "Total: {0}",
                              showInColumn: "CantidadItem",
                              customizeText: function (e) {
                                  if (e.value != 0 && e.value != "") {
                                      return (e.value)
                                  }
                              }
                          },
                          {
                              name: "TotalItem",
                              column: "TotalItem",
                              summaryType: "sum",
                              displayFormat: "Total: {0}",
                              showInColumn: "TotalItem",
                              customizeText: function (e) {
                                  if (e.value != 0 && e.value != "") {
                                      return (e.value)
                                  }
                              }
                          },
                          {
                              name: "SaldoItem",
                              column: "SaldoItem",
                              summaryType: "sum",
                              displayFormat: "Total: {0}",
                              showInColumn: "SaldoItem",
                              customizeText: function (e) {
                                  if (e.value != 0 && e.value != "") {
                                      return (e.value)
                                  }
                              }
                          }
                          ,
                          {
                              name: "Pallet",
                              column: "Pallet",
                              summaryType: "sum",
                              displayFormat: "Total: {0}",
                              showInColumn: "Pallet",
                              customizeText: function (e) {
                                  if (e.value != 0 && e.value != "") {
                                      return (e.value)
                                  }
                              }
                          }
                      ],
                },
              ConfigDev.onRowUpdating= function (e) {
                  startUpdating = true;
              },
                  ConfigDev.onContentReady= function (e) {
                      if (startUpdating) {
                          startUpdating = false;
                          document.getElementById("RegistrarPallet").disabled = false;
                      }
                  }, ConfigDev.onEditorPreparing= function (e) {
                      e.editorOptions.onValueChanged = function (arg) {
                          document.getElementById("RegistrarPallet").disabled = true;
                          e.setValue(arg.value);
                      }
                  },
                  
              $("#tblDetallePalletTablaNueva").dxDataGrid(ConfigDev);
          },
        error: function (msg) {
          
            $("#MensajeErrorGeneral").show('fade');
            setTimeout(function () {
                $("#MensajeErrorGeneral").fadeOut(1500);
            }, 3000);
        }
    })
    $("#ModalPalletIngresoNuevo").modal("show");
}

function registrarPallet() {
    $.ajax({
        url: "../PackingList/RegistrarPalletPacking",
        type: "POST",
        data: {
            Array2: temp2, Array: temp, idPacking: IdentificadorPaking, PalletNumber: $('#txtNumeroPalle').val(), LargoPallet: $("#txtNuevoLargoPallet").val(), AltoPallet: $("#txtNuevoAltoPallet").val(), AnchoPallet: $("#txtNuevoAnchoPallet").val(), VolumenPallet: $("#txtNuevoVolumenPallet").val(), PesoNeto: $("#txtNuevoPesoNeto").val(), PesoBruto: $("#txtNuevoPesoBruto").val()
        },
        success: function (msg) {
            console.log("el msg es");
            console.log(msg);
            if(msg=="True"){
                mostrarIngresosPallet();
                $("#MensajeGuardado").show('fade');
                setTimeout(function () {
                    $("#MensajeGuardado").fadeOut(1500);
                }, 3000);
                $("#ModalPalletIngresoNuevo").modal("hide");
            } else {
                $("#MensajeErrorInesperado").show('fade');
                setTimeout(function () {
                    $("#MensajeErrorInesperado").fadeOut(1500);
                }, 3000);
            }
        },
        error: function (msg) {
            $("#MensajeErrorGuardado").show('fade');
            setTimeout(function () {
                $("#MensajeErrorGuardado").fadeOut(1500);
            }, 3000);
            $("#ModalPalletIngresoNuevo").modal("hide");
        }
    })
}

$('#RegistrarPallet').on("click", function (e) {
    registrarPallet();
});

function GenerarQr() {
    $.ajax({
        url: "../PackingList/GenerarQr",
        type: "POST",
        //Array: temp, idPacking: IdentificadorPaking, PalletNumber: $('#txtNumeroPalle').val(), LargoPallet: $("#txtLargoPallet").val(), AltoPallet: $("#txtAltoPallet").val(), AnchoPallet: $("#txtAnchoPallet").val(), VolumenPallet: $("#txtVolumenPallet").val(), PesoNeto: $("#txtPesoNeto").val(), PesoBruto: $("#txtPesoBruto").val()
        data: {
            PalletNumero: $('#txtNumeroPalle').val(), NumeroDoc: $('#txtDocAct').val(), Orden: $('#txtOrdenAct').val(), Cliente: datosCabecera.CardName, Volumen: $('#txtVolumenPallet').val(), GrossWeight: $('#txtPesoBruto').val(),
            NetWeight: $('#txtPesoNeto').val(), Origen: $("#txtOrigenAct").val(), Destino: $("#txtDestinoAct option:selected").text(), items: temp
        }, success: function (e) {

            var imgsrc = e;
            CierraPopup();
            CrearPdf(e);
        },

        error: function (msg) {
            $("#MensajeErrorGeneral").show('fade');
            setTimeout(function () {
                $("#MensajeErrorGeneral").fadeOut(1500);
            }, 3000);
        }
    })
}

$('#DetallePackingList').on("click", function (e) {
    if (IngresoDetallePacking == "SI") {
        $('#txtContenedorPackingList').attr("readonly", false);
        $('#txtFechaPacking').attr("readonly", false);
        $('#txtBookingPacking').attr("readonly", false);
        $('#txtFacturaPackingList').attr("readonly", false);
        $('#txtEmbarcacionPackingList').attr("readonly", false);
        $("#txtIntercambioEirPackingList").attr("readonly", false);
        $("#txtReferenciaPackingList").attr("readonly", false);
        $('#txtProductosPackingList').attr("readonly", false);
        $("#txtNombreDelCliente").attr("readonly", true);
        $("#txtPedidoPackingList").attr("readonly", true);
        document.getElementById('RegistrarDetallePackingList').disabled = true;
        document.getElementById('ImprimirRegistroPackingList').disabled = false;
        document.getElementById('ActualizarDetallePackingList').disabled = false;


    } else {
        $('#txtContenedorPackingList').attr("readonly", false);
        $('#txtFechaPacking').attr("readonly", false);
        $('#txtBookingPacking').attr("readonly", false);
        $('#txtFacturaPackingList').attr("readonly", false);
        $('#txtEmbarcacionPackingList').attr("readonly", false);
        $("#txtIntercambioEirPackingList").attr("readonly", false);
        $("#txtReferenciaPackingList").attr("readonly", false);
        $('#txtProductosPackingList').attr("readonly", false);
        $("#txtNombreDelCliente").attr("readonly", false);
        $("#txtPedidoPackingList").attr("readonly", false);
        document.getElementById('RegistrarDetallePackingList').disabled = false;
        document.getElementById('ImprimirRegistroPackingList').disabled = true;
        document.getElementById('ActualizarDetallePackingList').disabled = true;

    }
    $('#txtContenedorPackingList').val("");
    $('#txtFechaPacking').val("");
    $('#txtBookingPacking').val("");
    $('#txtFacturaPackingList').val("");
    $('#txtEmbarcacionPackingList').val("");
    $("#txtIntercambioEirPackingList").val("");
    $("#txtReferenciaPackingList").val("");
    $('#txtProductosPackingList').val("");

    MostrarCabeceraDetallePackingList(PackingIdentificador);
    $("#ModalDetallePackingList").modal("show");
    $("#txtNombreDelCliente").val(NombreCliente);
    $("#txtPedidoPackingList").val(NumeroPedido);

    //BuscarDetallesPalletPackingList(PackingIdentificador);
    //BuscarItemsPackingList(PackingIdentificador);
});

function MostrarCabeceraDetallePackingList(PackingIdentificador) {
    $.ajax({
        url: "../PackingList/DetallesGeneralesPalletsPackingList?PackingId=" + PackingIdentificador,
        type: 'GET',
        dataType: 'json',
        success: function (data) {         
            //Muestro datos en la tabla (SI MUESTRA DATOS)
            $.each(data, function () {
                $('#txtNombreDelCliente').val(NombreCliente);
                $('#txtContenedorPackingList').val(data[0].ContenedorPackingList);
                $('#txtFechaPacking').val(data[0].FechaDePackingList);
                $('#txtBookingPacking').val(data[0].ReservaPackingList);
                $('#txtFacturaPackingList').val(data[0].FacturaPedido);
                $('#txtPedidoPackingList').val(NumeroPedido);
                $('#txtEmbarcacionPackingList').val(data[0].EmbarcacionPackingList);
                $("#txtIntercambioEirPackingList").val(data[0].IntercambioEirPackingList);
                $("#txtReferenciaPackingList").val(data[0].ReferenciasPackingList);
                $('#txtProductosPackingList').val(data[0].ProductosPackingList);
            });
        },
        error: function (msg) {
            $("#MensajeErrorGeneral").show('fade');
            setTimeout(function () {
                $("#MensajeErrorGeneral").fadeOut(1500);
            }, 3000);
        },
    }
   );
}

function BuscarItemsPackingList(PackingId) {
    $.ajax({
        url: "../PackingList/ConsultaItemsPackingList?PackingId=" + PackingId,
        type: "GET"
        , success: function (msg) {
            $("#tblResumenGuia").dxDataGrid({
                 dataSource : msg,
                 keyExpr : "ItemCode",
                 columnAutoWidth : true,
                 showBorders : true,
                  allowColumnReordering : true,
                 //ConfigDev.allowColumnResizing = true,
                    filterRow : { visible: false },
                     filterPanel : { visible: false },
                     headerFilter : { visible: false },
                   columnFixing : {
                       enabled: true
                   },
                 columns : [
                     {
                         caption: "Detalle Items en Pallets", alignment: "center",
                         columns: [
                             { dataField: "PalletPackingDetalleId", visible: false },
                             {
                             dataField: "NumeroPallet", caption: "# Pallet", allowEditing: false, alignment: "left"
                         }, {
                                 dataField: "ItemCode", caption: "Codigo Item", allowEditing: false, alignment: "left", visible: false
                         }, {
                             dataField: "Descripcion", caption: "Descripcion", allowEditing: false, alignment: "left"
                         }, {
                             dataField: "Cantidad", caption: "Cantidad", allowEditing: false, alignment: "left"
                         }]
                     },
                ],
                summary: {
                    recalculateWhileEditing: true,
                    totalItems: [
                        {
                            name: "NumeroPallet",
                            column: "NumeroPallet",
                            summaryType: "count",
                            displayFormat: "Total: {0}",
                            showInColumn: "DescriptionItem",
                            customizeText: function (e) {
                                if (e.value != 0 && e.value != "") {
                                    return "Totales:"
                                }
                            }
                        },
                        {
                            name: "Cantidad",
                            column: "Cantidad",
                            summaryType: "sum",
                            displayFormat: "Total: {0}",
                            showInColumn: "Cantidad",
                            customizeText: function (e) {
                                if (e.value != 0 && e.value != "") {
                                    return (e.value)
                                }
                            }
                        }
                    ],
                },
            });
                 //$("#tblItemsDetallePackingListIngresados").dxDataGrid(ConfigDev);

                 //$("#tblResumenGuia").dxDataGrid(ConfigDev);
                 
             },
        error: function (msg) {
            $("#MensajeErrorGeneral").show('fade');
            setTimeout(function () {
                $("#MensajeErrorGeneral").fadeOut(1500);
            }, 3000);
        }
    })
}

function BuscarDetallesPalletPackingList(PackingId) {
    $.ajax({
        url: "../PackingList/DetallesPalletsPackingList?PackingId=" + PackingId,
        type: "GET"
             , success: function (msg) {
                 ConfigDev.dataSource = msg;
                 ConfigDev.keyExpr = "PalletPackinId",
                 ConfigDev.columnAutoWidth = true,
                 ConfigDev.showBorders = true,
                  ConfigDev.allowColumnReordering = true,
                 //ConfigDev.allowColumnResizing = true,
                    ConfigDev.filterRow = { visible: false },
                     ConfigDev.filterPanel = { visible: false },
                     ConfigDev.headerFilter = { visible: false },
                   ConfigDev.columnFixing = {
                       enabled: true
                   },
                 ConfigDev.columns = [
                        {
                            caption: "Detalle Pallets", alignment: "center",
                            columns: [{ dataField: "PalletPackinId", visible: false },
                       {
                           dataField: "NumeroPallet", caption: "# Pallet", allowEditing: false, alignment: "left"
                       },
                       {
                           dataField: "Alto", caption: "Alto", allowEditing: false, alignment: "left"
                       },
                       {
                          dataField: "Ancho", caption: "Ancho", allowEditing: false, alignment: "left"
                       },
                       {
                          dataField: "Volumen", caption: "Volumen", allowEditing: false, alignment: "left"
                       },
                       {
                          dataField: "Cantidad", caption: "Cantidad", allowEditing: false, alignment: "left"
                       },
                       {
                           dataField: "PesoBruto", caption: "Peso Bruto", allowEditing: false
                       },
                       {
                           dataField: "PesoNeto", caption: "Peso Neto", allowEditing: false
                       }]
                        },
                    
                 ];
                 $("#tblDetallePackingListIngresados").dxDataGrid(ConfigDev);
             },
        error: function (msg) {
            $("#MensajeErrorGeneral").show('fade');
            setTimeout(function () {
                $("#MensajeErrorGeneral").fadeOut(1500);
            }, 3000);
        }
    })
}

$('#RegistrarDetallePackingList').on("click", function (e) {
    if ($('#txtNombreDelCliente').val() != null && $('#txtContenedorPackingList').val() != null && $('#txtFechaPacking').val() != null && $('#txtBookingPacking').val() != null && $('#txtFacturaPackingList').val() != null && $('#txtPedidoPackingList').val() != null &&
    $('#txtEmbarcacionPackingList').val() != null && $("#txtIntercambioEirPackingList").val() != null && $("#txtReferenciaPackingList").val() != null && $('#txtProductosPackingList').val() != null) {
        RegistrarDetallePackingListGeneral();
    }
    else {
        alert("Falta Ingresar Valores ");
    }
});
function RegistrarDetallePackingListGeneral() {
    $.ajax({
        url: "../PackingList/GuardarDetallesPalletsPackingList",
        type: "POST",
        data: {
            PackingId: PackingIdentificador, Cliente: NombreCliente, Contenedor: $('#txtContenedorPackingList').val(), fecha: $('#txtFechaPacking').val(), Reserva: $('#txtBookingPacking').val(), Factura: $('#txtFacturaPackingList').val(), Pedido: NumeroPedido,
            Embarcacion: $('#txtEmbarcacionPackingList').val(), IntercambioEir: $("#txtIntercambioEirPackingList").val(), Referencias: $("#txtReferenciaPackingList").val(), Productos: $('#txtProductosPackingList').val()
        }, success: function (e) {
            ConsultarIngresosPacking();
            $("#ModalDetallePackingList").modal("hide");
            $("#ModalListadoDePallets").modal("hide");

            $("#MensajeGuardado").show('fade');
            setTimeout(function () {
                $("#MensajeGuardado").fadeOut(1500);
            }, 3000);
        },
        error: function (msg) {
            $("#MensajeErrorGuardado").show('fade');
            setTimeout(function () {
                $("#MensajeErrorGaurdado").fadeOut(1500);
            }, 3000);
        }
    })
}

$('#ActualizarDetallePackingList').on("click", function (e) {
    ActualizarDetallePackingListGeneral();
});

$('#GenerarFacturaPacking').on("click", function (e) {
    // = modelo.NombreCliente;
    // = modelo.NumeroOrden;

    if (IngresoDetallePacking == "SI") {
        consultarInfoFact();

    } else {
        $("#MensajeIngresarInformacionPacking").show('fade');
        setTimeout(function () {
            $("#MensajeIngresarInformacionPacking").fadeOut(1500);
        }, 3000);
    }
   

    
});
function consultarInfoFact() {
    $.ajax({
        url: "../PackingList/BuscarDatosFactPacking",
        type: "POST",
        async: false,
        data: {
            numeroOrden: NumeroPedido,
        },
        success: function (msg) {
            if (msg != null) {
                formaPago = msg[0].terminoPago;
                $("#txtTelefonoFactPacking").val(msg[0].Telefono);
                $("#txtEnvioFactPackingList").val(msg[0].enviarA);
                $("#txtFactPackingList").val(msg[0].numeroFact);
                $("#txtMetodoEnvioFactPackingList").val(msg[0].metodoEnvio);
                $("#txtFechaFactPackingList").val(msg[0].Fecha);
                $("#txtContenedorFactPackingList").val(ContenedorNumer);
                $("#txtDestinoFactPackingList").val(DestinoPacking);
                $("#txtVendedorFactPackingList").val(msg[0].vendedor);
                valorDocEntry = msg[0].docentry;

                consultarInfoDetFact();
                $("#txtClienteFactPacking").val(NombreCliente);
                $("#txtOrdenFactPackingList").val(NumeroPedido);

                $("#ModalFacturaPackingList").modal("show");
                $("#ModalListadoDePallets").modal("hide");

            }
        },
        error: function (msg) {
            $("#MensajeErrorGeneral").show('fade');
            setTimeout(function () {
                $("#MensajeErrorGeneral").fadeOut(1500);
            }, 3000);
            return;
        }
    })
}


function prueba() {
    var request = new XMLHttpRequest();
    request.responseType = "blob";
    request.open("GET", "../PackingList/pdf");
    request.onload = function () {
        var url = window.URL.createObjectURL(this.response);
        var a = document.createElement("a");
        document.body.appendChild(a);
        a.href = url;
        a.download = this.response.name || "download-" + $.now()
        a.click();
    }
    request.send();
}
$('#ImprimirFactRegistroPackingList').on("click", function (e) {
    //prueba();
    generarPdfFact();
    //$.ajax({
    //    url: "../PackingList/pdf",
    //    type: "GET",
    //    success: function (msg) {

    //    },
    //    error: function (msg) {
    //        $("#MensajeErrorGeneral").show('fade');
    //        setTimeout(function () {
    //            $("#MensajeErrorGeneral").fadeOut(1500);
    //        }, 3000);
    //        return;
    //    }
    //})
});
$('#ImprimirRegistroPackingListGuia').on("click", function (e) {
    generarPdfGuia();
});

function generarPdfGuia() {
    var url = "../PackingList/ImprimirGuia?Cliente=" + $("#txtNombreDelClienteGuia").val() + "&packingId=" + PackingIdentificador + "&reserva=" + $("#txtBookingPackingGuia").val() + "&pedido=" + $("#txtPedidoPackingListGuia").val()
        + "&contenedorDetalle=" + $("#txtContenedorGuia").val();
    window.open(url);
    $("#ModalListadoDePallets").modal("hide");
}

function generarPdfFact() {
    var url = "../PackingList/ImprimirFact?numeroFactura=" + $("#txtFactPackingList").val() + "&numeroOrden=" + NumeroPedido + "&fecha=" + $("#txtFechaFactPackingList").val() + "&cliente=" + $("#txtClienteFactPacking").val()+"&enviar="+ $("#txtEnvioFactPackingList").val()+"&telefono="+$("#txtTelefonoFactPacking").val()+
        "&vendedor=" + $("#txtVendedorFactPackingList").val() + "&destino=" + $("#txtDestinoFactPackingList").val()  + "&metodo=" + $("#txtMetodoEnvioFactPackingList").val() + "&valorEntry=" + valorDocEntry + "&packingId=" + PackingIdentificador + "&formaPago=" + formaPago;
    window.open(url);
    $("#ModalListadoDePallets").modal("hide");
}

function ImprimirFact() {
    $.ajax({
        url: "../PackingList/ImprimirFact",
        type: "POST",
        async: false,
        data: {
            numeroFactura: $("#txtFactPackingList").val(), numeroOrden: NumeroPedido, fecha: $("#txtFechaFactPackingList").val(), cliente: $("#txtTelefonoFactPacking").val(), enviar: $("#txtEnvioFactPackingList").val(), telefono: $("#txtTelefonoFactPacking").val()
            , vendedor: $("#txtVendedorFactPackingList").val(), destino: $("#txtDestinoFactPackingList").val(), contenedor: $("#txtContenedorFactPackingList").val(), metodo: $("#txtMetodoEnvioFactPackingList").val(), detFact: detFactImpr
        },
        success: function (msg) {
         
        },
        error: function (msg) {
            $("#MensajeErrorGeneral").show('fade');
            setTimeout(function () {
                $("#MensajeErrorGeneral").fadeOut(1500);
            }, 3000);
            return;
        }
    })
}
function consultarInfoDetFact() {
    $.ajax({
        url: "../PackingList/BuscarDatosDetalleFactPacking",
        type: "POST",
        async: false,
        data: {
            docEntry: valorDocEntry,
        },
        success: function (msg) {
            detFactImpr = msg;
            $("#tblResumenFactura").dxDataGrid({
                dataSource: msg,
                keyExpr: "numeroItem",
                columnAutoWidth: true,
                showBorders: true,
                allowColumnReordering: true,
                //ConfigDev.allowColumnResizing = true,
                filterRow: { visible: false },
                filterPanel: { visible: false },
                headerFilter: { visible: false },
                columnFixing: {
                    enabled: true
                },
                columns: [
                    {
                        caption: "Detalle Items en Pallets", alignment: "center",
                        columns: [
                            {
                                dataField: "numeroItem", caption: "Item", allowEditing: false, alignment: "left"
                            }, {
                                dataField: "CustomerPartNumber", caption: "Codigo Item", allowEditing: false, alignment: "left", visible: false
                            }, {
                                dataField: "DacarPArtNumber", caption: "Descripcion", allowEditing: false, alignment: "left", visible: false
                            }, {
                                dataField: "Description", caption: "Descripcion", allowEditing: false, alignment: "left"
                            }, {
                                dataField: "Quantity", caption: "Cantidad", allowEditing: false, alignment: "left"
                            }, {
                                dataField: "Price", caption: "Precio Unitario", allowEditing: false, alignment: "left", format: {
                                    type: "currency",
                                    precision: 2,
                                },
                            }, {
                                dataField: "TotalPrice", caption: "Precio Total", allowEditing: false, alignment: "left", format: {
                                    type: "currency",
                                    precision: 2,
                                },
                            }]
                    },
                ],
                summary: {
                    recalculateWhileEditing: true,
                    totalItems: [
                        {
                            name: "numeroItem",
                            column: "numeroItem",
                            summaryType: "count",
                            displayFormat: "Total: {0}",
                            showInColumn: "numeroItem",
                            customizeText: function (e) {
                                if (e.value != 0 && e.value != "") {
                                    return "Subtotal:"
                                }
                            }
                        },
                        {
                            name: "Quantity",
                            column: "Quantity",
                            summaryType: "sum",
                            displayFormat: "Total: {0}",
                            showInColumn: "Quantity",
                            customizeText: function (e) {
                                if (e.value != 0 && e.value != "") {
                                    return (e.value)
                                }
                            }
                        }
                        ,
                        {
                            name: "TotalPrice",
                            column: "TotalPrice",
                            summaryType: "sum",
                            displayFormat: "Total: {0}",
                            showInColumn: "TotalPrice",
                            customizeText: function (e) {
                                if (e.value != 0 && e.value != "") {
                                    return "$"+(e.value).toFixed(2)
                                }
                            }
                        }
                    ],
                },
            });
        },
        error: function (msg) {

        }
    })
}

$('#GenerarGuiaPacking').on("click", function (e) {
    if (IngresoDetallePacking == "SI") {
        BuscarItemsPackingList(PackingIdentificador);
        ConsultarDetalleGuiaPackingList(PackingIdentificador);
        console.log("guia:" + GuiaPackingList);
        if (GuiaPackingList.length === 0) {
            $('#txtPesoBrutoGuia').val("");
            $('#txtPesoTaraGuia').val("");
            $('#txtRazonSocialGuia').val("");
            $('#txtRucGuia').val("");
            $('#txtDireccionGuia').val("");
            $('#txtPlacaGuia').val("");
            $('#txtGuiaSelloA').val("");
            $('#txtGuiaSelloB').val("");
            $('#txtGuiaSelloC').val("");
            $('#txtGuiaSelloD').val("");
            $('#txtElaboradoPorGuia').val("");
            $('#txtAutorizadoPorGuia').val("");

            $('#txtNombreDelClienteGuia').attr("readonly", true);
            $('#txtFechaPackingGuia').attr("readonly", true);
            $('#txtPuntoPartidaGuia').attr("readonly", false);
            $('#txtBookingPackingGuia').attr("readonly", true);
            $('#txtPedidoPackingListGuia').attr("readonly", true);
            $('#txtContenedorGuia').attr("readonly", true);
            $('#txtPesoBrutoGuia').attr("readonly", false);
            $('#txtPesoTaraGuia').attr("readonly", false);
            $('#txtRazonSocialGuia').attr("readonly", false);
            $("#txtRucGuia").attr("readonly", false);
            $("#txtDireccionGuia").attr("readonly", false);
            $('#txtPlacaGuia').attr("readonly", false);
            $("#txtGuiaSelloA").attr("readonly", false);
            $("#txtGuiaSelloB").attr("readonly", false);
            $("#txtGuiaSelloC").attr("readonly", false);
            $('#txtGuiaSelloD').attr("readonly", false);
            $("#txtElaboradoPorGuia").attr("readonly", false);
            $("#txtAutorizadoPorGuia").attr("readonly", false);
            document.getElementById('RegistrarDetallePackingListGuia').disabled = false;
            document.getElementById('ImprimirRegistroPackingListGuia').disabled = true;
            document.getElementById('ActualizarDetallePackingListGuia').disabled = true;
        } else {

            //$('#txtPuntoPartidaGuia').val(GuiaPackingList[0].PuntoPartida);
            $('#txtPesoBrutoGuia').val(GuiaPackingList[0].PesoBruto);
            $('#txtPesoTaraGuia').val(GuiaPackingList[0].PesoTara);
            $('#txtRazonSocialGuia').val(GuiaPackingList[0].RazonSocial);
            $('#txtRucGuia').val(GuiaPackingList[0].Ruc);
            $('#txtDireccionGuia').val(GuiaPackingList[0].Direccion);
            $('#txtPlacaGuia').val(GuiaPackingList[0].Placa);
            $('#txtGuiaSelloA').val(GuiaPackingList[0].SelloA);
            $('#txtGuiaSelloB').val(GuiaPackingList[0].SelloB);
            $('#txtGuiaSelloC').val(GuiaPackingList[0].SelloC);
            $('#txtGuiaSelloD').val(GuiaPackingList[0].SelloD);
            $('#txtElaboradoPorGuia').val(GuiaPackingList[0].ElaboradoPor);
            $('#txtAutorizadoPorGuia').val(GuiaPackingList[0].AutorizadoPor);

            $('#txtNombreDelClienteGuia').attr("readonly", true);
            $('#txtFechaPackingGuia').attr("readonly", true);
            $('#txtPuntoPartidaGuia').attr("readonly", false);
            $('#txtBookingPackingGuia').attr("readonly", true);
            $('#txtPedidoPackingListGuia').attr("readonly", true);
            $('#txtContenedorGuia').attr("readonly", true);
            $('#txtPesoBrutoGuia').attr("readonly", false);
            $('#txtPesoTaraGuia').attr("readonly", false);
            $('#txtRazonSocialGuia').attr("readonly", false);
            $("#txtRucGuia").attr("readonly", false);
            $("#txtDireccionGuia").attr("readonly", false);
            $('#txtPlacaGuia').attr("readonly", false);
            $("#txtGuiaSelloA").attr("readonly", false);
            $("#txtGuiaSelloB").attr("readonly", false);
            $("#txtGuiaSelloC").attr("readonly", false);
            $('#txtGuiaSelloD').attr("readonly", false);
            $("#txtElaboradoPorGuia").attr("readonly", false);
            $("#txtAutorizadoPorGuia").attr("readonly", false);

            document.getElementById('RegistrarDetallePackingListGuia').disabled = true;
            document.getElementById('ImprimirRegistroPackingListGuia').disabled = false;
            document.getElementById('ActualizarDetallePackingListGuia').disabled = false;
        }

        MostrarCabeceraDetalleGuiaPackingList(PackingIdentificador);
        $("#ModalGuiaPackingList").modal("show");
    }
    else {
        $("#MensajeIngresarInformacionPacking").show('fade');
        setTimeout(function () {
            $("#MensajeIngresarInformacionPacking").fadeOut(1500);
        }, 3000);
    }
   
});

$('#LinkCloseCompleteGuia').on("click", function (e) {
    $("#MensajeCompleteCamposGuia").hide('fade');
});
$('#LinkCloseErrorGuardadoGuia').on("click", function (e) {
    $("#MensajeErrorGuardarGuia").hide('fade');
});
$('#LinkCloseIngresarInformacionPacking').on("click", function (e) {
    $("#MensajeIngresarInformacionPacking").hide('fade');
});
$('#RegistrarDetallePackingListGuia').on("click", function (e) {
    var var1 =$('#txtPesoBrutoGuia').val();
    var var2 =$('#txtPesoTaraGuia').val();
    var var3 =$('#txtRazonSocialGuia').val();
    var var4 =$('#txtRucGuia').val();
    var var5 =$('#txtDireccionGuia').val();
    var var6 =$('#txtPlacaGuia').val();
    var var7 =$('#txtGuiaSelloA').val();
    var var8 =$('#txtGuiaSelloB').val();
    var var9 =$('#txtGuiaSelloC').val();
    var var10 =$('#txtGuiaSelloD').val();
    var var11 =$('#txtElaboradoPorGuia').val();
    var var12 =$('#txtAutorizadoPorGuia').val();

    if (var1.length == 0) {
        $("#MensajeCompleteCamposGuia").show('fade');
        setTimeout(function () {
            $("#MensajeCompleteCamposGuia").fadeOut(1500);
        }, 3000);
        return;
    }
    if (var2.length == 0) {
        $("#MensajeCompleteCamposGuia").show('fade');
        setTimeout(function () {
            $("#MensajeCompleteCamposGuia").fadeOut(1500);
        }, 3000);
        return;
    } if (var3.length == 0) {
        $("#MensajeCompleteCamposGuia").show('fade');
        setTimeout(function () {
            $("#MensajeCompleteCamposGuia").fadeOut(1500);
        }, 3000);
        return;
    } if (var4.length == 0) {
        $("#MensajeCompleteCamposGuia").show('fade');
        setTimeout(function () {
            $("#MensajeCompleteCamposGuia").fadeOut(1500);
        }, 3000);
        return;
    } if (var5.length == 0) {
        $("#MensajeCompleteCamposGuia").show('fade');
        setTimeout(function () {
            $("#MensajeCompleteCamposGuia").fadeOut(1500);
        }, 3000);
        return;
    } if (var6.length == 0) {
        $("#MensajeCompleteCamposGuia").show('fade');
        setTimeout(function () {
            $("#MensajeCompleteCamposGuia").fadeOut(1500);
        }, 3000);
        return;
    } if (var7.length == 0) {
        $("#MensajeCompleteCamposGuia").show('fade');
        setTimeout(function () {
            $("#MensajeCompleteCamposGuia").fadeOut(1500);
        }, 3000);
        return;
    } if (var8.length == 0) {
        $("#MensajeCompleteCamposGuia").show('fade');
        setTimeout(function () {
            $("#MensajeCompleteCamposGuia").fadeOut(1500);
        }, 3000);
        return;
    } if (var9.length == 0) {
        $("#MensajeCompleteCamposGuia").show('fade');
        setTimeout(function () {
            $("#MensajeCompleteCamposGuia").fadeOut(1500);
        }, 3000);
        return;
    } if (var10.length == 0) {
        $("#MensajeCompleteCamposGuia").show('fade');
        setTimeout(function () {
            $("#MensajeCompleteCamposGuia").fadeOut(1500);
        }, 3000);
        return;
    } if (var11.length == 0) {
        $("#MensajeCompleteCamposGuia").show('fade');
        setTimeout(function () {
            $("#MensajeCompleteCamposGuia").fadeOut(1500);
        }, 3000);
        return;
    } if (var12.length == 0) {
        $("#MensajeCompleteCamposGuia").show('fade');
        setTimeout(function () {
            $("#MensajeCompleteCamposGuia").fadeOut(1500);
        }, 3000);
        return;
    }

    RegistrarGuiaPackingList();
});

$('#ActualizarDetallePackingListGuia').on("click", function (e) {
    var var1 = $('#txtPesoBrutoGuia').val();
    var var2 = $('#txtPesoTaraGuia').val();
    var var3 = $('#txtRazonSocialGuia').val();
    var var4 = $('#txtRucGuia').val();
    var var5 = $('#txtDireccionGuia').val();
    var var6 = $('#txtPlacaGuia').val();
    var var7 = $('#txtGuiaSelloA').val();
    var var8 = $('#txtGuiaSelloB').val();
    var var9 = $('#txtGuiaSelloC').val();
    var var10 = $('#txtGuiaSelloD').val();
    var var11 = $('#txtElaboradoPorGuia').val();
    var var12 = $('#txtAutorizadoPorGuia').val();

    if (var1.length == 0) {
        $("#MensajeCompleteCamposGuia").show('fade');
        setTimeout(function () {
            $("#MensajeCompleteCamposGuia").fadeOut(1500);
        }, 3000);
        return;
    }
    if (var2.length == 0) {
        $("#MensajeCompleteCamposGuia").show('fade');
        setTimeout(function () {
            $("#MensajeCompleteCamposGuia").fadeOut(1500);
        }, 3000);
        return;
    } if (var3.length == 0) {
        $("#MensajeCompleteCamposGuia").show('fade');
        setTimeout(function () {
            $("#MensajeCompleteCamposGuia").fadeOut(1500);
        }, 3000);
        return;
    } if (var4.length == 0) {
        $("#MensajeCompleteCamposGuia").show('fade');
        setTimeout(function () {
            $("#MensajeCompleteCamposGuia").fadeOut(1500);
        }, 3000);
        return;
    } if (var5.length == 0) {
        $("#MensajeCompleteCamposGuia").show('fade');
        setTimeout(function () {
            $("#MensajeCompleteCamposGuia").fadeOut(1500);
        }, 3000);
        return;
    } if (var6.length == 0) {
        $("#MensajeCompleteCamposGuia").show('fade');
        setTimeout(function () {
            $("#MensajeCompleteCamposGuia").fadeOut(1500);
        }, 3000);
        return;
    } if (var7.length == 0) {
        $("#MensajeCompleteCamposGuia").show('fade');
        setTimeout(function () {
            $("#MensajeCompleteCamposGuia").fadeOut(1500);
        }, 3000);
        return;
    } if (var8.length == 0) {
        $("#MensajeCompleteCamposGuia").show('fade');
        setTimeout(function () {
            $("#MensajeCompleteCamposGuia").fadeOut(1500);
        }, 3000);
        return;
    } if (var9.length == 0) {
        $("#MensajeCompleteCamposGuia").show('fade');
        setTimeout(function () {
            $("#MensajeCompleteCamposGuia").fadeOut(1500);
        }, 3000);
        return;
    } if (var10.length == 0) {
        $("#MensajeCompleteCamposGuia").show('fade');
        setTimeout(function () {
            $("#MensajeCompleteCamposGuia").fadeOut(1500);
        }, 3000);
        return;
    } if (var11.length == 0) {
        $("#MensajeCompleteCamposGuia").show('fade');
        setTimeout(function () {
            $("#MensajeCompleteCamposGuia").fadeOut(1500);
        }, 3000);
        return;
    } if (var12.length == 0) {
        $("#MensajeCompleteCamposGuia").show('fade');
        setTimeout(function () {
            $("#MensajeCompleteCamposGuia").fadeOut(1500);
        }, 3000);
        return;
    }
    ActualizarGuiaPackingList();
});

function RegistrarGuiaPackingList() {
    $.ajax({
        url: "../PackingList/GuardarDetallesGuiaPackingList",
        type: "POST",
        data: {
            PackingId: PackingIdentificador, pesoBruto: $('#txtPesoBrutoGuia').val(), pesoTara: $('#txtPesoTaraGuia').val(), razonSocial: $('#txtRazonSocialGuia').val(), ruc: $('#txtRucGuia').val(), direccion: $('#txtDireccionGuia').val(), placa: $('#txtPlacaGuia').val(),
            selloA: $('#txtGuiaSelloA').val(), selloB: $("#txtGuiaSelloB").val(), selloC: $("#txtGuiaSelloC").val(), selloD: $('#txtGuiaSelloD').val(), elaboradoPor: $('#txtElaboradoPorGuia').val()
            , autorizadoPor: $('#txtAutorizadoPorGuia').val(), puntoPartida: $('#txtPuntoPartidaGuia option:selected').text()
        },
        success: function (e) {
            ConsultarIngresosPacking();
            $("#ModalGuiaPackingList").modal("hide");
            $("#ModalListadoDePallets").modal("hide");

            $("#MensajeGuardado").show('fade');
            setTimeout(function () {
                $("#MensajeGuardado").fadeOut(1500);
            }, 3000);
        },
        error: function (msg) {
            $("#MensajeErrorGuardarGuia").show('fade');
            setTimeout(function () {
                $("#MensajeErrorGuardarGuia").fadeOut(1500);
            }, 3000);
        }
    }) 
}
function ActualizarGuiaPackingList() {
    $.ajax({
        url: "../PackingList/ActualizarDetallesGuiaPalletsPackingList",
        type: "POST",
        data: {
            PackingId: PackingIdentificador, pesoBruto: $('#txtPesoBrutoGuia').val(), pesoTara: $('#txtPesoTaraGuia').val(), razonSocial: $('#txtRazonSocialGuia').val(), ruc: $('#txtRucGuia').val(), direccion: $('#txtDireccionGuia').val(), placa: $('#txtPlacaGuia').val(),
            selloA: $('#txtGuiaSelloA').val(), selloB: $("#txtGuiaSelloB").val(), selloC: $("#txtGuiaSelloC").val(), selloD: $('#txtGuiaSelloD').val(), elaboradoPor: $('#txtElaboradoPorGuia').val()
            , autorizadoPor: $('#txtAutorizadoPorGuia').val(), puntoPartida: $('#txtPuntoPartidaGuia option:selected').text()
        },
        success: function (e) {
            ConsultarIngresosPacking();
            $("#ModalGuiaPackingList").modal("hide");
            $("#ModalListadoDePallets").modal("hide");

            $("#MensajeActulalizacionCorrecta").show('fade');
            setTimeout(function () {
                $("#MensajeActulalizacionCorrecta").fadeOut(1500);
            }, 3000);
        },
        error: function (msg) {
            $("#MensajeErrorGuardarGuia").show('fade');
            setTimeout(function () {
                $("#MensajeErrorGuardarGuia").fadeOut(1500);
            }, 3000);
        }
    })
}

function ConsultarDetalleGuiaPackingList(PackingIdentificador) {
    $.ajax({
        url: "../PackingList/DetallesGuiasPackingList?PackingId=" + PackingIdentificador,
        type: 'GET',
        async: false,
        success: function (data) {
            GuiaPackingList = data;
        }  
    });
}
function MostrarCabeceraDetalleGuiaPackingList(PackingIdentificador) {
    $.ajax({
        url: "../PackingList/DetallesGeneralesPalletsPackingList?PackingId=" + PackingIdentificador,
        type: 'GET',
        dataType: 'json',
        success: function (data) {
            //Muestro datos en la tabla (SI MUESTRA DATOS)
            $.each(data, function () {
                $('#txtNombreDelClienteGuia').val(NombreCliente);
                $('#txtContenedorGuia').val(data[0].ContenedorPackingList);
                $('#txtFechaPackingGuia').val(data[0].FechaDePackingList);
                $('#txtBookingPackingGuia').val(data[0].ReservaPackingList);
                $('#txtPedidoPackingListGuia').val(NumeroPedido);
            });
        },
        error: function (msg) {
            $("#MensajeErrorGeneral").show('fade');
            setTimeout(function () {
                $("#MensajeErrorGeneral").fadeOut(1500);
            }, 3000);
        },
    }
    );
}

function ActualizarDetallePackingListGeneral() {
    $.ajax({
        url: "../PackingList/ActualizarDetallesPalletsPackingList",
        type: "POST",
        data: {
            PackingId: PackingIdentificador, Cliente: $('#txtNombreDelCliente').val(), Contenedor: $('#txtContenedorPackingList').val(), fecha: $('#txtFechaPacking').val(), Reserva: $('#txtBookingPacking').val(), Factura: $('#txtFacturaPackingList').val(), Pedido: $('#txtPedidoPackingList').val(),
            Embarcacion: $('#txtEmbarcacionPackingList').val(), IntercambioEir: $("#txtIntercambioEirPackingList").val(), Referencias: $("#txtReferenciaPackingList").val(), Productos: $('#txtProductosPackingList').val()
        }, success: function (e) {
            ConsultarIngresosPacking();
            $("#ModalDetallePackingList").modal("hide");
            $("#ModalListadoDePallets").modal("hide");

            $("#MensajeActulalizacionCorrecta").show('fade');
            setTimeout(function () {
                $("#MensajeActulalizacionCorrecta").fadeOut(1500);
            }, 3000);
        },
        error: function (msg) {
            $("#MensajeErrorGuardado").show('fade');
            setTimeout(function () {
                $("#MensajeErrorGaurdado").fadeOut(1500);
            }, 3000);
        }
    })
}

$('#ImprimirRegistroPackingList').on("click", function (e) {
    generarInformePackingListPDF();
});

function generarInformePackingListPDF() {
    $("#ModalAfirmacionFondoPacking").modal("show");
}

$('#AfirmacionEticketaPacking').on("click", function (e) {
    etiqueta = "NO";
    generarPDFPackingList(etiqueta);
    $("#ModalAfirmacionFondoPacking").modal("hide");
});

$('#NgacionEticketaPacking').on("click", function (e) {
    etiqueta = "SI";
    generarPDFPackingList(etiqueta);
    $("#ModalAfirmacionFondoPacking").modal("hide");
});

function generarPDFPackingList(variable) {
    var url = "../PackingList/InformePackingList?PackingId=" + PackingIdentificador + "&Fondo=" + variable;
    window.open(url);
    $("#ModalListadoDePallets").modal("hide");
}