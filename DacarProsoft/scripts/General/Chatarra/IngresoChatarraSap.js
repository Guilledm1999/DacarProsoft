var modeloEvent = null;
var configDevDataSource = null;
var consultCab = null;
var consultDet = null;
var selectTipoIngreso = null;
var temp = null;
var temp2 = null;
var desviacionIndividual = null;
var subtotalIndividual = null;
var calculosIndividuales = null;
var estatus = null;

var fecha = moment().format('yyyy-MM-DD');

$(document).ready(function () {
    $("#cargaImg").hide();
    $("#FechaBusquedaInicio").val(fecha);
    $("#FechaBusquedaFin").val(fecha);
    $(".loading-icon").css("display", "none");

});

function ConsultarIngresosChatarraSap() {
    $("#ChatarrasCabecera").hide();
    $(".result").text("");
    $(".loading-icon").removeClass("hide");
    $(".btn").attr("disabled", true);
    $(".btn-txt").text("Espere...");
    ConsultarCabeceraIngresosChatarra();
}

function ConsultarCabeceraIngresosChatarra() {
    var select = $("#FechaBusqueda option:selected").text();
    var valorInicio = $("#FechaBusquedaInicio").val();
    var valorFin = $("#FechaBusquedaFin").val();
    selectTipoIngreso = $("#TipoIngreso option:selected").text();
    var select2 = $("#grupoCliente").val();
    selectTipoCliente = $("#grupoCliente option:selected").text();

    if (selectTipoCliente == "--Todos--") {
        select2 = 0;
    }
    var metodo1 = "../Chatarra/ConsultaCabeceraIngresoMercanciasSap?tipoIngreso=" + selectTipoIngreso;
    var metodo2 = "../Chatarra/ConsultaCompraCabeceraIngresoMercanciasSap?tipoIngreso=" + selectTipoIngreso;
    //var metodo1 = "../Chatarra/ConsultaCabeceraIngresoMercanciasPorTipoClienteSap?tipoIngreso=" + selectTipoIngreso + "&codigoCliente=" + select2;
    //var metodo2 = "../Chatarra/ConsultaCompraCabeceraIngresoMercanciasPorTipoClienteSap?tipoIngreso=" + selectTipoIngreso + "&codigoCliente=" + select2;
    if (selectTipoIngreso == "Nota de Credito") {
        consultCab = metodo1;
    } else {
        consultCab = metodo2;
    }
    $("#cargaImg").show();
    $.ajax({
        url: consultCab,
        type: "GET"
       , success: function (msg) {
           $("#cargaImg").hide();

           ConfigDev.dataSource = msg;
           ConfigDev.columnAutoWidth = true,
           ConfigDev.keyExpr = "DocEntry",
           ConfigDev.showBorders = true,
           ConfigDev.paging = {
               pageSize: 10
           },
              ConfigDev.filterRow = { visible: false },
               ConfigDev.filterPanel = { visible: false },
               ConfigDev.headerFilter = { visible: true },
           ConfigDev.columns = [
                { dataField: "DocEntry", visible: false },
                  {
                      caption: "Acciones",
                      cellTemplate: function (container, options) {
                          var lblDetalle = "<button class='btn-primary' onclick='ModalConsultarTipoCalculo(" + JSON.stringify(options.data) + ")'>Ingresar Peso</button>";
                          $("<div>")
                              .append($(lblDetalle))
                              .appendTo(container);
                      }
                  },
               { dataField: "DocNum", caption: "# Documento" },
                { dataField: "NumeroPedido", caption: "Pedido" },
                { dataField: "DocDate", caption: "Fecha Documento", },
                { dataField: "GrupoName", caption: "Tipo Cliente" },
                { dataField: "ClienteClase", caption: "Cliente Clase" },
                { dataField: "ClienteLinea", caption: "Cliente Linea" },
                  { dataField: "CedulaCliente", caption: "Identificacion" },
                { dataField: "NombreCliente", caption: "Nombre Cliente" },
                 { dataField: "CardCode", visible: false },
                 { dataField: "Comments", caption: "Comentarios" },
                 { dataField: "TipoIngreso", caption: "Fecha de Orden", visible: false },
                 { dataField: "KilosReales", visible: false },
              
           ];

           $(".btn").attr("disabled", false);
           $(".btn-txt").text("Consultar");
           $("#ChatarrasCabecera").dxDataGrid(ConfigDev);
           $("#ChatarrasCabecera").show();

       },
       error: function (msg) {
           $("#cargaImg").hide();
            $(".btn").attr("disabled", false);
            $(".btn-txt").text("Consultar");
            $("#MensajeErrorInesperado").show('fade');
            setTimeout(function () {
                $("#MensajeErrorInesperado").fadeOut(1500);
            }, 3000);
        }

    })
}

function ModalConsultarTipoCalculo(modelo) {
    temp = modelo;
    $("#ModalParaCalculo").modal("show");
}

$('#CalculoIndividual').on("click", function (e) {
    $("#ModalParaCalculo").modal("hide");
    ModalConsultarDetalleIngresosChatarraIndividual(temp);
});

$('#CalculoTotal').on("click", function (e) {
    $("#ModalParaCalculo").modal("hide");

    ModalConsultarDetalleIngresosChatarra(temp);
});
function ModalConsultarDetalleIngresosChatarra(modelo) {
    modeloEvent = modelo;
    $('#txtPesoNetoBulto').val("");
    $("#TipoBodega").val("");
    $('#txtCalPesoNetoBulto').val("");
    $("#txtPesoNetoBulto").val("");
    $('#TituloIngreso').html("Detalle de Ingreso Chatarra #" + modelo.DocNum);
    $('#txtPesoNetoBulto').val(modelo.KilosReales);

    var metodo1 = "../Chatarra/ConsultaDetalleIngresoMercanciasSapSinFu?DocEntry=" + modelo.DocEntry;
    var metodo2 = "../Chatarra/ConsultaCompraDetalleIngresoMercanciasSapSinFu?DocEntry=" + modelo.DocEntry;

    if (selectTipoIngreso == "Nota de Credito") {
        consultDet = metodo1;
        $.ajax({
            url: "../Chatarra/ConsultaBodega",
            type: "POST",
            data: {
                DocEntry: modelo.DocEntry
            }
 , success: function (msg) {
     $("#TipoBodega").val(msg);
    
 },
        })
    } else {
        consultDet = metodo2;
        $.ajax({
            url: "../Chatarra/ConsultaBodegaCompra",
            type: "POST",
            data: {
                DocEntry: modelo.DocEntry
            }
, success: function (msg) {
    $("#TipoBodega").val(msg);

},
        })
    }
    console.log(consultDet);
    $.ajax({
        url: consultDet,
        type: "GET"
       , success: function (msg) {

           ConfigDev.dataSource = msg;
           //configDevDataSource = ConfigDev.dataSource;
           ConfigDev.paging = {
               pageSize: 6
           },
           ConfigDev.columnAutoWidth = true,
           ConfigDev.showBorders = true,
           ConfigDev.keyExpr = "DocEntry",
           ConfigDev.headerFilter = false,
           ConfigDev.filterPanel = false,
           ConfigDev.filterRow=false,
           ConfigDev.selection= {
               mode: "single"
           },
           ConfigDev.columns = [
                { dataField: "DocEntry", visible: false },
               { dataField: "ItemCode", caption: "Codigo Item" },
                { dataField: "Description", caption: "Descripción" },
                  { dataField: "Cantidad", caption: "Cantidad" },
                  { dataField: "WhsCode", visible: false },
                  {
                      dataField: "PesoTeoricoUnitario", caption: "Peso Teorico Unitario(Kg)", alignment: "right", allowHeaderFiltering: false, calculateCellValue: function (rowData) {
                          return (rowData.PesoTeoricoUnitario).toFixed(2);
                      }
                  },
                  {
                      dataField: "PesoTeoricoSubtotal", caption: "Peso Teorico Subtotal(Kg)", alignment: "right", allowHeaderFiltering: false, calculateCellValue: function (rowData) {
                          return (rowData.PesoTeoricoSubtotal).toFixed(2);
                      }
                  },
                  {
                      dataField: "PesoTeoricoAjustado", caption: "Peso Unitario Ajustado(Kg)", dataType: "decimal", alignment: "right", allowHeaderFiltering: false, calculateCellValue: function (rowData) {
                      return (rowData.PesoTeoricoAjustado).toFixed(2);
                  } },
                  {
                      dataField: "PesoTeoricoAjustadoTotal", caption: "Peso Total Ajustado(Kg)", dataType: "decimal", alignment: "right", allowHeaderFiltering: false, calculateCellValue: function (rowData) {
                          return (rowData.PesoTeoricoAjustadoTotal).toFixed(2);
                      }
                  }
           ];
           ConfigDev.summary= {
               totalItems: [
               {
                       name: "PesoTotal",
                       column: "PesoTeoricoSubtotal",
                       summaryType: "sum",
                       displayFormat: "Peso TeoricoTotal: {0} Kg",
                       showInColumn: "PesoTeoricoSubtotal",
                       customizeText: function (e) {
                           if (e.value != 0 && e.value != "") {
                               $("#txtSumaryPesos").val(e.value);
                               return "Peso Total: " + (e.value).toFixed(2)
                           }
                       }
               }, {
                   column: "Cantidad",
                   summaryType: "sum",
                   showInColumn: "Cantidad",
                   displayFormat: "Total: {0}",
                   customizeText: function (e) {
                       if (e.value != 0 && e.value != "") {
                           return "Total: " + (e.value)
                       }
                   }

               },
               ],
           }
           $(".btn").attr("disabled", false);
           $(".btn-txt").text("Consultar");
           configDevDataSource = ConfigDev;

           $("#ChatarraDetalle").dxDataGrid(ConfigDev);
       },
        error: function (msg) {
            $(".btn").attr("disabled", false);
            $(".btn-txt").text("Consultar");
            $("#MensajeErrorInesperado").show('fade');
            setTimeout(function () {
                $("#MensajeErrorInesperado").fadeOut(1500);
            }, 3000);
        },
       
    })
    $("#ModalDetalleChatarra").modal("show");
}

function ModalConsultarDetalleIngresosChatarraIndividual(modelo) {
    modeloEvent = modelo;
    $("#TipoBodega").val("");
    $('#txtCalPesoNetoBulto').val("");
    $('#TituloIngreso').html("Detalle de Ingreso Chatarra #" + modelo.DocNum);
 
    var metodo1 = "../Chatarra/ConsultaDetalleIngresoMercanciasSapSinFuIndividual?DocEntry=" + modelo.DocEntry;
    var metodo2 = "../Chatarra/ConsultaCompraDetalleIngresoMercanciasSapSinFuIndividual?DocEntry=" + modelo.DocEntry;

    if (selectTipoIngreso == "Nota de Credito") {
        consultDet = metodo1;
        $.ajax({
            url: "../Chatarra/ConsultaBodega",
            type: "POST",
            data: {
                DocEntry: modelo.DocEntry
            }
      , success: function (msg) {
          $("#TipoBodegaInd").val(msg);

 },
        })
    } else {
        consultDet = metodo2;
        $.ajax({
            url: "../Chatarra/ConsultaBodegaCompra",
            type: "POST",
            data: {
                DocEntry: modelo.DocEntry
            }
, success: function (msg) {
    $("#TipoBodegaInd").val(msg);
},
        })
    }
    $.ajax({
        url: consultDet,
        type: "GET"
       , success: function (msg) {
           temp2 = msg;
           ConfigDev.dataSource = temp2;
           console.log("Esto trae el ajax");
           console.log(temp2);
           ConfigDev.keyExpr = "ItemCode",
           ConfigDev.showBorders= true,
           ConfigDev.paging = {
               pageSize: 6
           },
               ConfigDev.filterRow = { visible: false },
               ConfigDev.filterPanel = { visible: false },
               ConfigDev.headerFilter = { visible: false },

           ConfigDev.editing = {
               mode: "batch",
               allowUpdating: true,
               selectTextOnEditStart: true,
               startEditAction: "click"
           },
            
           ConfigDev.columns = [
                { dataField: "DocEntry", visible: false },
               { dataField: "ItemCode", caption: "Codigo Item", allowEditing: false },
                { dataField: "Description", caption: "Descripción", allowEditing: false },
                  { dataField: "Cantidad", caption: "Cantidad", allowEditing: false },
                  {
                      dataField: "PesoTeoricoUnitario", caption: "Peso Teorico Unitario(kg)", alignment: "right", allowEditing: false,
                     
                  },
                  {
                      dataField: "PesoTeoricoSubtotal", caption: "Peso Teorico Subtotal(kg)", alignment: "right", allowEditing: false
                     
                  },
                    {
                        dataField: "PesoNetoTipo", caption: "Peso Individual Total(kg)", alignment: "right", validationRules: [{
                            type: "required"
                        }]              
                    },
                  {
                      dataField: "PesoTeoricoAjustado", caption: "Peso Unitario Ajustado(kg)", alignment: "right", allowEditing: false

               
                  },
                  {
                      dataField: "PesoTeoricoAjustadoTotal", caption: "Peso Total Ajustado(kg)", alignment: "right", allowEditing: false
                  
                  },
                  {
                      dataField: "DesviacionIndividual", caption: "Desviacion Individual", alignment: "right", allowEditing: false
                   
                  }
           ];
           ConfigDev.summary = {
               totalItems: [
               {
                   name: "PesoTotal",
                   column: "PesoTeoricoSubtotal",
                   summaryType: "sum",
                   displayFormat: "Peso TeoricoTotal: {0} Kg",
                   showInColumn: "PesoTeoricoSubtotal",
                   customizeText: function (e) {
                       if (e.value != 0 && e.value != "") {
                           $("#txtSumaryPesos").val(e.value);
                           return "Peso Total: " + (e.value).toFixed(2)
                       }
                   }
               }, {
                   column: "Cantidad",
                   summaryType: "sum",
                   showInColumn: "Cantidad",
                   displayFormat: "Total: {0}",
                   customizeText: function (e) {
                       if (e.value != 0 && e.value != "") {
                           return "Total: " + (e.value)
                       }
                   }

               },
               ],
           }
           $(".btn").attr("disabled", false);
           $(".btn-txt").text("Consultar");
           configDevDataSource = ConfigDev;
           temp2 = ConfigDev.dataSource;

           $("#ChatarraDetalleIndividual").dxDataGrid(ConfigDev);
 
       },
        error: function (msg) {
            $(".btn").attr("disabled", false);
            $(".btn-txt").text("Consultar");
            $("#MensajeErrorInesperado").show('fade');
            setTimeout(function () {
                $("#MensajeErrorInesperado").fadeOut(1500);
            }, 3000);

        },

    })
   
    $("#ModalDetalleChatarraIndividual").modal("show");
}


function ModalConsultarDetalleIngresosChatarraConPeso(factorUnitario) {
    $('#TituloIngreso').html("Detalle de Ingreso Chatarra #" + modeloEvent.DocNum);
    var subtoPeAj;
    var metodo1 = "../Chatarra/ConsultaDetalleIngresoMercanciasSap?DocEntry=" + modeloEvent.DocEntry + " &factorUnitario=" + factorUnitario;
    var metodo2 = "../Chatarra/ConsultaCompraDetalleIngresoMercanciasSap?DocEntry=" + modeloEvent.DocEntry + " &factorUnitario=" + factorUnitario;

    if (selectTipoIngreso == "Nota de Credito") {
        consultDet = metodo1;
    } else {
        consultDet = metodo2;
    }

    $.ajax({
        url: consultDet,
        type: "GET"
       , success: function (msg) {
           ConfigDev.dataSource = msg;
           configDevDataSource = ConfigDev.dataSource;
           ConfigDev.paging = {
               pageSize: 6
           },
           ConfigDev.columnAutoWidth = true,
           ConfigDev.showBorders = true,
           ConfigDev.keyExpr = "DocEntry",
           ConfigDev.headerFilter = false,
           ConfigDev.filterPanel = false,
           ConfigDev.filterRow = false,
           ConfigDev.selection = {
               mode: "single"
           },

           ConfigDev.columns = [
                { dataField: "DocEntry", visible: false },
               { dataField: "ItemCode", caption: "Codigo Item" },
                { dataField: "Description", caption: "Descripción" },
                  { dataField: "Cantidad", caption: "Cantidad" },
                  {
                      dataField: "PesoTeoricoUnitario", caption: "Peso Teorico Unitario(Kg)", alignment: "right"
                      , calculateCellValue: function (rowData) {
                          return (rowData.PesoTeoricoUnitario).toFixed(2);
                      }
                  },
                  {
                      dataField: "PesoTeoricoSubtotal", caption: "Peso Teorico Subtotal(Kg)", alignment: "right"
                      , calculateCellValue: function (rowData) {
                          return (rowData.PesoTeoricoSubtotal).toFixed(2);
                      }
                  },
                  {
                      dataField: "PesoTeoricoAjustado", dataField: "PesoTeoricoSubtotal", caption: "Peso Unitario Ajustado(Kg)", dataType: "decimal", alignment: "right",
                      calculateCellValue: function (rowData) {
                          return (rowData.PesoTeoricoAjustado).toFixed(2);
                      }
                  },
                  {
                      dataField: "PesoTeoricoAjustadoTotal", caption: "Peso Total Ajustado(Kg)", dataType: "decimal", alignment: "right",
                      calculateCellValue: function (rowData) {
                          return (rowData.PesoTeoricoAjustadoTotal).toFixed(2);
                      } 
                  }
           ];
           ConfigDev.summary = {
               totalItems: [
               {
                   name: "PesoTotal",
                   column: "PesoTeoricoSubtotal",
                   summaryType: "sum",
                   displayFormat: "Peso TeoricoTotal: {0} Kg",
                   showInColumn: "PesoTeoricoSubtotal",
                   customizeText: function (e) {
                       if (e.value != 0 && e.value != "") {
                           $("#txtSumaryPesos").val(e.value);
                           return "Peso Total: " + (e.value).toFixed(2)
                       }
                   }
               }, {
                   column: "Cantidad",
                   summaryType: "sum",
                   showInColumn: "Cantidad",
                   displayFormat: "Total: {0}",
                   customizeText: function (e) {
                       if (e.value != 0 && e.value != "") {
                           return "Total: " + (e.value)
                       }
                   }

               },
               {
                   column: "PesoTeoricoAjustadoTotal",
                   summaryType: "sum",
                   displayFormat: "Total Ajustado: {0}",
                   customizeText: function (e) {
                       if (e.value != 0 && e.value != "") {
                           $("#txtSumaryPesosAjustados").val(e.value);
                           return "Total Ajustado: " + (e.value).toFixed(2)
                       }
                   }

               }
               ],
           }
           $(".btn").attr("disabled", false);
           $(".btn-txt").text("Consultar");


           $("#ChatarraDetalle").dxDataGrid(ConfigDev);

       },
        error: function (msg) {
            $(".btn").attr("disabled", false);
            $(".btn-txt").text("Consultar");
            $("#MensajeErrorInesperado").show('fade');
            setTimeout(function () {
                $("#MensajeErrorInesperado").fadeOut(1500);
            }, 3000);
        },
    })
    $("#ModalDetalleChatarra").modal("show");
}


$('#RegistrarChatarra').on("click", function (e) {
    e.stopPropagation();
    RegistrarChatarra(modeloEvent, configDevDataSource);
});

$('#LinkClose').on("click", function (e) {
    $("#MensajeGuardado").hide('fade');
});
$('#LinkClose2').on("click", function (e) {
    $("#MensajeErrorInesperado").hide('fade');
});
$('#LinkClose3').on("click", function (e) {
    $("#MensajeErrorGuardar").hide('fade');
});
$('#LinkClose4').on("click", function (e) {
    $("#MensajeIngresePeso").hide('fade');
});
$('#LinkClose5').on("click", function (e) {
    $("#MensajeErrorVariacion").hide('fade');
});
$('#LinkClose6').on("click", function (e) {
    $("#MensajeCalcule").hide('fade');

});

$('#LinkClose8').on("click", function (e) {
$("#MensajeIngreseValores").hide('fade');
});

function CalcularDesviacion() {
    var PesoIngresado = $("#txtPesoNetoBulto").val();
    var TotalPesos = $("#txtSumaryPesos").val();
    var subtotal = ((PesoIngresado / TotalPesos) * 100);
    var desviacion = 0;

    if (PesoIngresado != "") {
        if (subtotal > 100) {
            desviacion = subtotal - 100;
        } else {
            desviacion = (100 - subtotal) * -1;
        }

        $('#txtCalPesoNetoBulto').val((desviacion).toFixed(2));

        var factorUnitario = PesoIngresado / TotalPesos
        ModalConsultarDetalleIngresosChatarraConPeso(factorUnitario);
    } else {
        
        //$("#MensajeIngresePeso").show('fade');
        //setTimeout(function () {
        //}, 3000);
        $("#MensajeIngresePeso").show('fade');
        setTimeout(function () {
            $("#MensajeIngresePeso").fadeOut(1500);
        }, 3000);
    }
}

function RegistrarChatarra(cabecera, detalle) {
    var TotalPesos = $("#txtSumaryPesos").val();
    var TotalPesosBulto = $("#txtCalPesoNetoBulto").val();
    if (TotalPesosBulto == '') {
        $("#MensajeIngreseValores").show('fade');
        setTimeout(function () {
            $("#MensajeIngreseValores").fadeOut(1500);
        }, 3000);
        return false;
    }
    if (TotalPesosBulto != '0' || TotalPesosBulto != '0.00') {

        $.ajax({
            url: "../Chatarra/GuardarIngresos",
            type: "POST",
            data: {
                cabecera, Array: detalle, pesoTotal: $("#txtSumaryPesos").val(), PesoBulto: $("#txtPesoNetoBulto").val(), PesoTotalAjustado: $("#txtSumaryPesosAjustados").val(), Desviacion: $("#txtCalPesoNetoBulto").val(), Bodega: $("#TipoBodega").val()
            }, success: function () {
                ConsultarCabeceraIngresosChatarra();
                $("#ModalDetalleChatarra").modal("hide");
                $("#MensajeGuardado").show('fade');
                setTimeout(function () {
                    $("#MensajeGuardado").fadeOut(1500);
                }, 3000);
            },
            error: function (msg) {
                $("#cargaImg").hide();
                $("#MensajeErrorGuardar").show('fade');
                setTimeout(function () {
                    $("#MensajeErrorGuardar").fadeOut(1500);
                }, 3000);
            }
        })
    }
    else {
        $("#MensajeErrorVariacion").show('fade');
        setTimeout(function () {
            $("#MensajeErrorVariacion").fadeOut(1500);
        }, 3000);
        
        return false;
    }
}

function CalcularDesviacionIndividual() {
    $.ajax({
        url: "../Chatarra/CalcularPesosIndividuales",
        type: "POST",
        data: {
            Array: temp2
        }, success: function (msg) {
            estatus = msg;
            calculosIndividuales = msg;
            ConfigDev.dataSource = msg;
            ConfigDev.paging = {
                pageSize: 6
            },
            ConfigDev.keyExpr = "ItemCode",
            ConfigDev.showBorders = true,
             ConfigDev.editing = {
                 allowUpdating: false,
                 selectTextOnEditStart: false,
             },
                ConfigDev.filterRow = { visible: false },
                ConfigDev.filterPanel = { visible: false },
                ConfigDev.headerFilter = { visible: false },

            ConfigDev.columns = [
                 { dataField: "DocEntry", visible: false },
                { dataField: "ItemCode", caption: "Codigo Item", allowEditing: false },
                 { dataField: "Description", caption: "Descripción", allowEditing: false },
                   { dataField: "Cantidad", caption: "Cantidad", allowEditing: false },
                   {
                       dataField: "PesoTeoricoUnitario", caption: "Peso Teorico Unitario(kg)", alignment: "right", allowEditing: false, calculateCellValue: function (rowData) {
                           return (rowData.PesoTeoricoUnitario).toFixed(2);
                       }
                   },
                   {
                       dataField: "PesoTeoricoSubtotal", caption: "Peso Teorico Subtotal(kg)", alignment: "right", allowEditing: false, calculateCellValue: function (rowData) {
                           return (rowData.PesoTeoricoSubtotal).toFixed(2);
                       }

                   },
                     {
                         dataField: "PesoNetoTipo", caption: "Peso Individual Total(kg)", alignment: "right", allowEditing: false, calculateCellValue: function (rowData) {
                             return (rowData.PesoNetoTipo).toFixed(2);
                         }

                     },
                   {
                       dataField: "PesoTeoricoAjustado", caption: "Peso Unitario Ajustado(kg)", alignment: "right", allowEditing: false, calculateCellValue: function (rowData) {
                           return (rowData.PesoTeoricoAjustado).toFixed(2);
                       }


                   },
                   {
                       dataField: "PesoTeoricoAjustadoTotal", caption: "Peso Total Ajustado(kg)", alignment: "right", allowEditing: false, calculateCellValue: function (rowData) {
                           return (rowData.PesoTeoricoAjustadoTotal).toFixed(2);
                       }

                   },
                   {
                       dataField: "DesviacionIndividual", caption: "Desviacion Individual", alignment: "right", allowEditing: false, calculateCellValue: function (rowData) {
                           return (rowData.DesviacionIndividual).toFixed(2);
                       }

                   }
            ];
            ConfigDev.summary = {
                totalItems: [
                {
                    name: "PesoTotal",
                    column: "PesoTeoricoSubtotal",
                    summaryType: "sum",
                    displayFormat: "Peso TeoricoTotal: {0} Kg",
                    showInColumn: "PesoTeoricoSubtotal",
                    customizeText: function (e) {
                        if (e.value != 0 && e.value != "") {
                            $("#txtSumaryPesos").val(e.value);
                            return "Total: " + (e.value).toFixed(2)
                        }
                    }
                }, {
                    name: "PesoTeoricoAjustadoTotal",
                    column: "PesoTeoricoAjustadoTotal",
                    summaryType: "sum",
                    displayFormat: "Peso Ajustado Total: {0} Kg",
                    showInColumn: "PesoTeoricoAjustadoTotal",
                    customizeText: function (e) {
                        if (e.value != 0 && e.value != "") {
                            $("#txtSumaryPesos").val(e.value);
                            return "Total: " + (e.value).toFixed(2)
                        }
                    }
                },
                {
                    name: "PesoNetoTipo",
                    column: "PesoNetoTipo",
                    summaryType: "sum",
                    displayFormat: "Peso Ajustado Total: {0} Kg",
                    showInColumn: "PesoNetoTipo",
                    customizeText: function (e) {
                        if (e.value != 0 && e.value != "") {
                            $("#txtSumaryPesos").val(e.value);
                            return "Total: " + (e.value).toFixed(2)
                        }
                    }
                }, {
                    column: "Cantidad",
                    summaryType: "sum",
                    showInColumn: "Cantidad",
                    displayFormat: "Total: {0}",
                    customizeText: function (e) {
                        if (e.value != 0 && e.value != "") {
                            return "Total: " + (e.value)
                        }
                    }

                }
                ],
            }
            $(".btn").attr("disabled", false);
            $(".btn-txt").text("Consultar");
            configDevDataSource = ConfigDev;
            temp2 = ConfigDev.dataSource;

            $("#ChatarraDetalleIndividual").dxDataGrid(ConfigDev);
        },
        error: function (msg) {
            $("#MensajeErrorInesperado").show('fade');
            setTimeout(function () {
                $("#MensajeErrorInesperado").fadeOut(1500);
            }, 3000);
        }
    })
}


$('#CalcularDesviacionIndividual').on("click", function (e) {
    if (estatus != null) {
        e.stopPropagation();
        RegistrarChatarraIndividual(modeloEvent, calculosIndividuales);
    } else {
        $("#MensajeCalcule").show('fade');
        setTimeout(function () {
            $("#MensajeCalcule").fadeOut(1500);
        }, 3000);
    }
});

function RegistrarChatarraIndividual(cabecera, detalle) {
    console.log(cabecera);
    console.log(detalle);
    var bod = $("#TipoBodegaInd").val();
    console.log(bod);
        $.ajax({
            url: "../Chatarra/GuardarIngresosChatarraIndividuales",
            type: "POST",
            data: {
                cabecera, Array: detalle, Bodega: $("#TipoBodegaInd").val()
            }, success: function () {
                ConsultarCabeceraIngresosChatarra();
                $("#ModalDetalleChatarraIndividual").modal("hide");
                //alert("Guardado con exito");
                $("#MensajeGuardado").show('fade');
                setTimeout(function () {
                    $("#MensajeGuardado").fadeOut(1500);
                }, 3000);
            },
            error: function (msg) {
                $("#cargaImg").hide();
                $("#MensajeErrorGuardar").show('fade');
                setTimeout(function () {
                    $("#MensajeErrorGuardar").fadeOut(1500);
                }, 3000);
            }
        })
        estatus = null;
}
     
