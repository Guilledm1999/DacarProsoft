var bodegaG = null;
var modeloEvent = null;
var CodDocEntry = null;
var configDevDataSource = null;
var modeloEventCalc = null;
var temp = null;
var botonCal = 0;
var DetalleCalculoIndividual = null;
var modTemp = null;
var modIng = null;


$(document).ready(function () {
    $(".loading-icon").css("display", "none");
});

function ConsultaDeIngresos() {
    $(".result").text("");
    $(".loading-icon").removeClass("hide");
    $(".btn").attr("disabled", true);
    $(".btn-txt").text("Espere...");
    InformeIngresosDeChatarra();
}

function InformeIngresosDeChatarra() {
    var select = $("#anioClass option:selected").text();
    //var val = $("#grupoCliente option:selected").val();
    //var select2 = $("#grupoCliente option:selected").text();
    document.getElementById("OcultarBoton").style.display = "";
 $.ajax({
     //url: "../Chatarra/ConsultaModificarIngresoChatarraLocal?anio=" + select + " &codigoCliente=" + val + " &codigos=" + select2,
     url: "../Chatarra/ConsultaModificarIngresoChatarraLocal?anio=" + select,

        type: "GET"
       , success: function (msg) {
           $("#cargaImg").hide();
           ConfigDev.dataSource = msg;
           ConfigDev.columnAutoWidth = true,
           ConfigDev.keyExpr = "DocEntry",
           ConfigDev.showBorders = true,
            ConfigDev.allowColumnReordering = false,
              ConfigDev.filterRow = { visible: false },
               ConfigDev.filterPanel = { visible: false },
               ConfigDev.headerFilter = { visible: true },
             ConfigDev.columnFixing = {
                 enabled: true
             },
          
           ConfigDev.columns = [            
                { dataField: "DocEntry", visible: false },
                { dataField: "CardCode", visible: false },
                  {
                      caption: "Acciones",fixed: true,
                    
                      cellTemplate: function (container, options) {
                          var btn = "<button class='btn-primary' onclick='ModalModificarIngresos(" + JSON.stringify(options.data) + ")'>Detalle</button>";
                    
                          $("<div>")
                              .append($(btn))
                              .appendTo(container);
                      }
                  },
                 {
                     dataField: "NumeroDocumento", caption: "# Documento", allowEditing: false, fixed: false
                 },
                 {
                     dataField: "NumeroPedido", caption: "# Pedido", allowEditing: false, fixed: false
                 },
                 {
                     dataField: "CedulaCliente", caption: "Identificacion", allowEditing: false, width: 130
                 },
                {
                    dataField: "NombreCliente", caption: "Cliente", allowEditing: false, fixed: false, width: 250
                },      
                {
               dataField: "GroupCode", caption: "Tipo Cliente", allowEditing: false
                 },
                 {
                     dataField: "ClienteLinea", caption: "Cliente Linea", allowEditing: false
                 },
                  {
                      dataField: "ClienteClase", caption: "Cliente Clase", allowEditing: false
                  },
               
                 {
                     dataField: "MesIngreso", caption: "Mes Ingreso", allowEditing: false

                 },
                 {
                     dataField: "TipoIngreso", caption: "Tipo Ingreso", allowEditing: false
                 },
                  {
                      dataField: "CantidadTotal", caption: "Cantidad", allowFiltering: false, allowEditing: false
,

                  },
                  ,
                {
                    dataField: "PesoTeoricoTotalCalculado", caption: "Peso Teorico", alignment: "right", allowFiltering: false, width: 130, allowEditing: false
,
                    
                calculateCellValue: function (rowData) {
                    return (rowData.PesoTeoricoTotalCalculado).toFixed(2);               

                }
                },
                   
                 {
                     dataField: "PesoBultoIngresado", caption: "Peso Ingresado", alignment: "right", allowFiltering: false, width: 130, allowEditing: false
,
                     calculateCellValue: function (rowData) {
                         return (rowData.PesoBultoIngresado).toFixed(2);                        

                     }
                 },
                 
                 {
                     dataField: "PesoAjustadoTotal", caption: "Peso Ajustado Total", alignment: "right", visible: false, allowFiltering: false, allowEditing: false

                    ,
                   
                 },
                {
                    dataField: "Desviacion", caption: "Desviacion", alignment: "right", allowFiltering: false, allowEditing: false, customizeText: function (cellInfo) {
                        return cellInfo.value + "%";
                    }             
                },
                
                 {
                     dataField: "Bodega", caption: "Bodega", allowEditing: false

                 },
                {
                    dataField: "Comments", caption: "Comentarios", allowFiltering: false, allowEditing: false

                   
                },
                 {
                     dataField: "FechaIngreso", caption: "Fecha Ingreso", allowEditing: false
                 },

                  { dataField: "ModoIngreso", visible: false, allowEditing: false },

           ];

           ConfigDev.summary = {
               totalItems: [
               {
                       name: "TipoIngreso",
                       column: "TipoIngreso",
                   summaryType: "count",
                   displayFormat: "Cantidad Total",
                       showInColumn: "TipoIngreso",
                   customizeText: function (e) {
                       if (e.value != 0 && e.value != "") {
                           $("#txtSumaryPesos").val(e.value);
                           return "Totales: ";
                       }
                   }
               }
                   ,
                   {
                       name: "CantidadTotal",
                       column: "CantidadTotal",
                       summaryType: "sum",
                       displayFormat: "Cantidad Total",
                       showInColumn: "CantidadTotal",
                       customizeText: function (e) {
                           if (e.value != 0 && e.value != "") {
                               $("#txtSumaryPesos").val(e.value);
                               return (e.value);
                           }
                       }
                   }
                   , {
                   column: "PesoTeoricoTotalCalculado",
                   summaryType: "sum",
                   showInColumn: "PesoTeoricoTotalCalculado",
                   displayFormat: "Total: {0}",
                   valueFormat: "currency",
                   customizeText: function (e) {
                       if (e.value != 0 && e.value != "") {
                           $("#txtPesoTeorico").val(e.value);
                           const noTruncarDecimales = { maximumFractionDigits: 2 };
                           ValTotal = (e.value).toLocaleString('en-US', noTruncarDecimales);
                           return ValTotal;
                       }
                   }

               },
          
               {
                   column: "PesoBultoIngresado",
                   summaryType: "sum",
                   showInColumn: "PesoBultoIngresado",
                   displayFormat: "Total: {0}",
                   valueFormat: "currency",
                   customizeText: function (e) {
                       if (e.value != 0 && e.value != "") {
                           $("#txtPesoIngresado").val(e.value);
                           const noTruncarDecimales = { maximumFractionDigits: 2 };
                           ValTotal = (e.value).toLocaleString('en-US', noTruncarDecimales);
                           return ValTotal;
                       }
                   }

               },
                 {
                     column: "Desviacion",
                     summaryType: "sum",
                     showInColumn: "Desviacion",
                     displayFormat: "Total: {0}",
                     valueFormat: "currency",
                     customizeText: function (e) {
                         if (e.value != 0 && e.value != "") {
                            var pesoTeorico= $("#txtPesoTeorico").val();
                            var pesoIngresado = $("#txtPesoIngresado").val();
                            var desviacion = null;
                            var subtotal = (pesoIngresado / pesoTeorico) * 100;
                            if (subtotal > 100) {
                                desviacion = subtotal - 100;
                            } else {
                                desviacion = (100 - subtotal) * -1;
                            }
                            return "Prom: " + desviacion.toFixed(2)+"%";
                         }
                     }

                 },
             
               ],
           }
           $(".btn").attr("disabled", false);
           $(".btn-txt").text("Consultar");
           $("#IngresosdeChatarras").dxDataGrid(ConfigDev);

       },
       error: function (msg) {
           $(".btn").attr("disabled", false);
           $(".btn-txt").text("Consultar");
           $("#MensajeErrorInesperado").show('fade');
           $("#cargaImg").hide();
           setTimeout(function () {
               $("#MensajeErrorInesperado").fadeOut(1500);
           }, 3000);

       }
    })
}
$('#LinkClose').on("click", function (e) {
    $("#MensajeErrorInesperado").hide();
});

//$('#ComprobarContrasena').on("click", function (e) {
//    var contrasena = $('#ContrasenaIngresada').val();
//    $.ajax({
//        url: "../Chatarra/ControlCambios",
//        type: "GET",
//        data: {
//        },
//        success: function (e) {
//            if (e == contrasena) {
//                console.log("Ingreso x verdadero");
//                ModalModificarIngresos(modTemp);
//                $("#ModalIngresoContrasena").modal("hide");
//            }
//            else {
//                console.log("Ingreso x falso");
//                $("#ModalIngresoContrasena").modal("hide");
//                $("#ErrorContrasena").show('fade');
//                setTimeout(function () {
//                    $("#ErrorContrasena").fadeOut(1500);
//                }, 3000);
//            }
//        },
//        error: function (msg) {
//            $("#MensajeErrorInesperado").show('fade');
//            setTimeout(function () {
//                $("#MensajeErrorInesperado").fadeOut(1500);
//            }, 3000);
//        }
//    })
//});

function ModalModificarIngresos(modelo) {
    CodDocEntry = modelo.DocEntry;
    modeloEvent = modelo;
    modIng = modelo.ModoIngreso;
    $('#txtPesoNetoBulto').val("");
    $("#TipoBodega").val("");
    $('#txtCalPesoNetoBulto').val("");
    $("#txtPesoNetoBulto").val("");

    $('#TituloIngreso').html("Detalle de Ingreso Chatarra #" + modelo.NumeroDocumento);
    $("#TipoBodega").val(modelo.Bodega);

    if (modelo.ModoIngreso == 1) {
      
        Detalle1(modelo.DocEntry, modelo.ModoIngreso, modelo.Desviacion, modelo.PesoBultoIngresado);
    }
    else {
      
        Detalle2(modelo.DocEntry, modelo.ModoIngreso);
    }
    $("#ModalDetalleChatarraModificacion").modal("show");
}



function Detalle1(DocEntry, Modo, Desviacion, PesoBulto) {
    botonCal = 1;
    $("#labelPesoNeto").show();
    $("#txtPesoNetoBulto").show();
    $("#ChatarraDetalles2").hide();
    $("#txtPesoNetoBulto").val(PesoBulto);
    $("#txtCalPesoNetoBulto").val(Desviacion);

    $.ajax({
        url: "../Chatarra/ConsultaIngresosChatarraLoc?DocEntry=" + DocEntry + " &ModoIngreso=" + Modo,
        type: "GET"
       , success: function (msg) {
           ConfigDev.dataSource = msg;
           configDevDataSource = msg;

           ConfigDev.paging = {
               pageSize: 6
           },
           ConfigDev.columnAutoWidth = true,
           ConfigDev.showBorders = true,
           ConfigDev.headerFilter = false,
           ConfigDev.filterPanel = false,
           ConfigDev.filterRow = false,
           ConfigDev.selection = {
               mode: "single"
           },
           ConfigDev.columns = [
                { dataField: "ChatarraDetalleId", visible: false },
                { dataField: "ChatarraId", visible: false },
                { dataField: "DocEntry", visible: false },
               { dataField: "ItemCode", caption: "Codigo Item" , allowEditing: false},
                { dataField: "Description", caption: "Descripción", allowEditing: false },
                  { dataField: "Cantidad", caption: "Cantidad", allowEditing: false },
                  {
                      dataField: "PesoTeoricoUnitario", caption: "Peso Teorico Unitario(Kg)", alignment: "right", allowEditing: false, allowHeaderFiltering: false, calculateCellValue: function (rowData) {
                          return (rowData.PesoTeoricoUnitario).toFixed(2);
                      }
                  },
                  {
                      dataField: "PesoTeoricoTotal", caption: "Peso Teorico Subtotal(Kg)", alignment: "right", allowEditing: false, allowHeaderFiltering: false, calculateCellValue: function (rowData) {
                          return (rowData.PesoTeoricoTotal).toFixed(2);
                      }
                  },
                  {
                      dataField: "PesoTeoricoAjustado", caption: "Peso Unitario Ajustado(Kg)", dataType: "decimal", allowEditing: false, alignment: "right", allowHeaderFiltering: false, calculateCellValue: function (rowData) {
                          return (rowData.PesoTeoricoAjustado).toFixed(2);
                      }
                  },
                  {
                      dataField: "PesoTeoricoAjustadoTotal", caption: "Peso Total Ajustado(Kg)", dataType: "decimal", alignment: "right", allowEditing: false, allowHeaderFiltering: false, calculateCellValue: function (rowData) {
                          return (rowData.PesoTeoricoAjustadoTotal).toFixed(2);
                      }
                  }
           ];
           ConfigDev.summary = {
               totalItems: [
               {
                   name: "PesoTotal",
                   column: "PesoTeoricoTotal",
                   summaryType: "sum",
                   displayFormat: "Peso TeoricoTotal: {0} Kg",
                   showInColumn: "PesoTeoricoTotal",
                   customizeText: function (e) {
                       if (e.value != 0 && e.value != "") {
                           $("#txtSumaryPesos").val(e.value);
                           return "Total: " + (e.value).toFixed(2)
                       }
                   }
               },
                {
                    name: "PesoTeoricoTotal",
                    column: "PesoTeoricoAjustadoTotal",
                    summaryType: "sum",
                    displayFormat: "Peso Total Ajustado: {0} Kg",
                    showInColumn: "PesoTeoricoAjustadoTotal",
                    customizeText: function (e) {
                        if (e.value != 0 && e.value != "") {
                            return "Total: " + (e.value).toFixed(2)
                        }
                    }
                },
               {
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

           $("#ChatarraDetalle").dxDataGrid(ConfigDev);
           $("#ChatarraDetalle").show();
       }

        ,
        error: function (msg) {
            $(".btn").attr("disabled", false);
            $(".btn-txt").text("Consultar");
            $("#MensajeErrorInesperado").show('fade');
            setTimeout(function () {
                $("#MensajeErrorInesperado").fadeOut(1500);
            }, 3000);
        }
    })
}

function Detalle2(DocEntry, Modo) {
    botonCal = 2;
    $("#labelPesoNeto").hide();
    $("#txtPesoNetoBulto").hide();
    $("#ChatarraDetalle").hide();
    $('#txtCalPesoNetoBulto').val("");

    $.ajax({
        url: "../Chatarra/ConsultaIngresosChatarraLoc?DocEntry=" + DocEntry + " &ModoIngreso=" + Modo,
        type: "GET"
      , success: function (msg2) {

          $.ajax({
              url: "../Chatarra/calcdesv",
              type: "POST",
              data: {
                  Array: msg2
              },
              success: function (e) {
                  $('#txtCalPesoNetoBulto').val(e);
              },
              error: function (msg) {
                  $('#txtCalPesoNetoBulto').val(0);

              }
          })

          temp = msg2;
          ConfigDev.dataSource = temp;
          ConfigDev.keyExpr = "ChatarraDetalleId",
          ConfigDev.showBorders = true,
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
               { dataField: "ChatarraDetalleId", visible: false },
               { dataField: "ChatarraId", visible: false },
               { dataField: "DocEntry", visible: false },
              { dataField: "ItemCode", caption: "Codigo Item", allowEditing: false },
               { dataField: "Description", caption: "Descripción", allowEditing: false },
                 { dataField: "Cantidad", caption: "Cantidad", allowEditing: false },
                 {
                     dataField: "PesoTeoricoUnitario", caption: "Peso Teorico Unitario(Kg)", alignment: "right", allowEditing: false, allowHeaderFiltering: false
                    
                 },
                 {
                     dataField: "PesoTeoricoTotal", caption: "Peso Teorico Subtotal(Kg)", alignment: "right", allowEditing: false, allowHeaderFiltering: false
                     
                 }
                 ,
                 {
                     dataField: "PesoNetoTipo", caption: "Peso Individual Total(kg)", alignment: "right", allowHeaderFiltering: false
               
                 },
                 {
                     dataField: "PesoTeoricoAjustado", caption: "Peso Unitario Ajustado(Kg)", dataType: "decimal", alignment: "right", allowEditing: false, allowHeaderFiltering: false
                    
                 },
                 {
                     dataField: "PesoTeoricoAjustadoTotal", caption: "Peso Total Ajustado(Kg)", dataType: "decimal", alignment: "right", allowEditing: false, allowHeaderFiltering: false
                    
                 },
                 {
                     dataField: "DesviacionIndividual", caption: "Desviacion individual", dataType: "decimal", alignment: "right", allowHeaderFiltering: false, allowEditing: false
                   
                 }
          ];
          ConfigDev.summary = {
              totalItems: [
              {
                  name: "PesoTotal",
                  column: "PesoTeoricoTotal",
                  summaryType: "sum",
                  displayFormat: "Peso TeoricoTotal: {0} Kg",
                  showInColumn: "PesoTeoricoTotal",
                  customizeText: function (e) {
                      if (e.value != 0 && e.value != "") {
                          $("#txtSumaryPesos").val(e.value);
                          return "Total: " + (e.value).toFixed(2)
                      }
                  }
              },
               {
                   name: "PesoTeoricoTotal",
                   column: "PesoTeoricoAjustadoTotal",
                   summaryType: "sum",
                   displayFormat: "Peso Total Ajustado: {0} Kg",
                   showInColumn: "PesoTeoricoAjustadoTotal",
                   customizeText: function (e) {
                       if (e.value != 0 && e.value != "") {
                           return "Total: " + (e.value).toFixed(2)
                       }
                   }
               },
              {
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

          $("#ChatarraDetalles2").dxDataGrid(ConfigDev);
          $("#ChatarraDetalles2").show();


      }
       ,
        error: function (msg) {
            $(".btn").attr("disabled", false);
            $(".btn-txt").text("Consultar");
            $("#MensajeErrorInesperado").show('fade');
            setTimeout(function () {
                $("#MensajeErrorInesperado").fadeOut(1500);
            }, 3000);
        }
    })
}


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
        $("#MensajeIngresePeso").show('fade');
        setTimeout(function () {
            $("#MensajeIngresePeso").fadeOut(1500);
        }, 3000);
    }
}


function ModalConsultarDetalleIngresosChatarraConPeso(factorUnitario) {
    $('#TituloIngreso').html("Detalle de Ingreso Chatarra #" + modeloEvent.DocNum);
    var subtoPeAj;
    $.ajax({
        url: "../Chatarra/ConsultaModificarDetalleIngresoMercanciasLocal?DocEntry=" + modeloEvent.DocEntry + " &factorUnitario=" + factorUnitario,
        type: "GET"
       , success: function (msg) {
           ConfigDev.dataSource = msg;
           modeloEventCalc = ConfigDev.dataSource;
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
                      dataField: "PesoTeoricoTotal", caption: "Peso Teorico Subtotal(Kg)", alignment: "right"
                      , calculateCellValue: function (rowData) {
                          return (rowData.PesoTeoricoTotal).toFixed(2);
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
                   column: "PesoTeoricoTotal",
                   summaryType: "sum",
                   displayFormat: "Peso TeoricoTotal: {0} Kg",
                   showInColumn: "PesoTeoricoTotal",
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
$('#LinkClose7').on("click", function (e) {
    $("#ErrorContrasena").hide('fade');

});

$('#LinkClose9').on("click", function (e) {
    $("#MensajeActulalizacionCorrecta").hide('fade');

});

//function permiso() {
//    $('#ContrasenaIngresada').val("");
//    $("#ModalIngresoContrasena").modal("show");
//}

$('#ComprobarContrasena').on("click", function (e) {
    var contrasena = $('#ContrasenaIngresada').val();
    $.ajax({
        url: "../Chatarra/ControlCambios",
        type: "GET",
        data: {
        },
        success: function (e) {
            if (e == contrasena) {
                if (botonCal == 1) {
                    RegistrarModificacionChatarra(CodDocEntry, modeloEventCalc);

                }
                else {
                    RegistrarModificacionIndividualChatarra(CodDocEntry, temp);
                }
                $("#ModalIngresoContrasena").modal("hide");
            }
            else {
             
                $("#ModalIngresoContrasena").modal("hide");
                $("#ErrorContrasena").show('fade');
                setTimeout(function () {
                    $("#ErrorContrasena").fadeOut(1500);
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
});
$('#BtnImprimir').on("click", function (e) {
    var url = "../Chatarra/ChatarraPdf?CodDocEntry=" + CodDocEntry + "&modIng=" + modIng;
    window.open(url);
    $("#ModalDetalleChatarraModificacion").modal("hide");

    //$.ajax({
    //    url: "../Chatarra/ChatarraPdf?CodDocEntry=" + CodDocEntry,
    //    type: "GET",
    //    success: function (e) {
    //        $("#ModalDetalleChatarraModificacion").modal("hide");
    //        InformeIngresosDeChatarra();
    //    },
    //    error: function (msg) {
    //        $("#MensajeErrorInesperado").show('fade');
    //        setTimeout(function () {
    //            $("#MensajeErrorInesperado").fadeOut(1500);
    //        }, 3000);
    //    }
    //})
});


$('#RegistrarModificacionChatarra').on("click", function (e) {
    $('#ContrasenaIngresada').val("");
    $("#ModalIngresoContrasena").modal("show");
});

$('#BtnCalcular').on("click", function (e) {
    e.stopPropagation();

    if (botonCal == 1) {
        CalcularDesviacion();        
    }
    else {

        CalcularDesviacionIndividual();
    }
});
function CalcularDesviacionIndividual() {
   
    $.ajax({
        url: "../Chatarra/CalcularModificarPesosIndividuales",
        type: "POST",
        data: {
            Array: temp
        }, success: function (msg) {
        
            $.ajax({
                url: "../Chatarra/calcdesv",
                type: "POST",
                data: {
                    Array: msg
                },
                success: function (e) {
                    $('#txtCalPesoNetoBulto').val(e);
                },
                error: function (msg) {
                    $('#txtCalPesoNetoBulto').val(0);

                }
            })

            estatus = msg;
            calculosIndividuales = msg;
            ConfigDev.dataSource = msg;
            ConfigDev.paging = {
                pageSize: 6
            },
            ConfigDev.keyExpr = "ChatarraDetalleId",
            ConfigDev.showBorders = true,
             ConfigDev.editing = {
                 allowUpdating: false,
                 selectTextOnEditStart: false,
             },
                ConfigDev.filterRow = { visible: false },
                ConfigDev.filterPanel = { visible: false },
                ConfigDev.headerFilter = { visible: false },

            ConfigDev.columns = [
                  { dataField: "ChatarraDetalleId", visible: false },
               { dataField: "ChatarraId", visible: false },
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
                       dataField: "PesoTeoricoTotal", caption: "Peso Teorico Subtotal(kg)", alignment: "right", allowEditing: false, calculateCellValue: function (rowData) {
                           return (rowData.PesoTeoricoTotal).toFixed(2);
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
            temp = ConfigDev.dataSource;
            DetalleCalculoIndividual = ConfigDev.dataSource;

            $("#ChatarraDetalles2").dxDataGrid(ConfigDev);
            $("#ChatarraDetalles2").show();
        },
        error: function (msg) {
            $("#MensajeErrorInesperado").show('fade');
            setTimeout(function () {
                $("#MensajeErrorInesperado").fadeOut(1500);
            }, 3000);
        }
    })
}
function RegistrarModificacionChatarra(docEntry, detalle) {

    var TotalPesos = $("#txtSumaryPesos").val();
    var TotalPesosBulto = $("#txtCalPesoNetoBulto").val();
    if (TotalPesosBulto == '') {
        alert("Ingrese valores o calcule");
        $("#MensajeIngreseValores").show('fade');
        setTimeout(function () {
            $("#MensajeIngreseValores").fadeOut(1500);
        }, 3000);
        return false;
    }
    if (TotalPesosBulto != '0' || TotalPesosBulto != '0.00') {
        $.ajax({
            url: "../Chatarra/GuardarActualizacionDetalles",
            type: "POST",
            data: {
                DocEntry: docEntry, detalleChatarra: detalle, PesoTeoricoTotalCal: $("#txtSumaryPesos").val(), PesoBultoIng: $("#txtPesoNetoBulto").val(), PesoAjustadoTot: $("#txtSumaryPesosAjustados").val(), desviacionTot: $("#txtCalPesoNetoBulto").val()
            }, success: function () {
                InformeIngresosDeChatarra();
                $("#ModalDetalleChatarraModificacion").modal("hide");
                $("#MensajeActulalizacionCorrecta").show('fade');
                setTimeout(function () {
                    $("#MensajeActulalizacionCorrecta").fadeOut(1500);
                }, 3000);

            },
            error: function (msg) {
                $("#cargaImg").hide();
                $("#MensajeErrorVariacion").show('fade');
                setTimeout(function () {
                    $("#MensajeErrorVariacion").fadeOut(1500);
                }, 3000);
            }
        })
    }
    else {
        $("#MensajeErrorInesperado").hide('fade');
        setTimeout(function () {
            $("#MensajeErrorInesperado").fadeOut(1500);
        }, 3000);
        return false;
    }
}

function RegistrarModificacionIndividualChatarra(docEntry, detalle) {
    $.ajax({
        url: "../Chatarra/IngresosIndividuales",
        type: "POST",
            data: {
                DocEntry: docEntry, Array: detalle
            },
            success: function () {
                InformeIngresosDeChatarra();
                $("#ModalDetalleChatarraModificacion").modal("hide");
                $("#MensajeActulalizacionCorrecta").show('fade');
                setTimeout(function () {
                    $("#MensajeActulalizacionCorrecta").fadeOut(1500);
                }, 3000);
            },
            error: function (msg) {
                $("#cargaImg").hide();
                $("#MensajeErrorInesperado").show('fade');
                setTimeout(function () {
                    $("#MensajeErrorInesperado").fadeOut(1500);
                }, 3000);
            }
        })    
}

