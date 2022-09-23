$(document).ready(function () {
    $(".loading-icon").css("display", "none");
});

function ConsultaDeVentas() {
    $(".result").text("");
    $(".loading-icon").removeClass("hide");
    $(".btn").attr("disabled", true);
    $(".btn-txt").text("Espere...");
    InformeIngresosChatarra();
}

function InformeIngresosChatarra() {
    var select = $("#anioClass option:selected").text();
    //var val = $("#grupoCliente option:selected").val();
    //var select2 = $("#grupoCliente option:selected").text();

 $.ajax({
     //url: "../Chatarra/ConsultaIngresoChatarraLocal?anio=" + select + " &codigoCliente=" + val + " &codigos=" + select2,
     url: "../Chatarra/ConsultaIngresoChatarraLocal?anio=" + select,

        type: "GET"
       , success: function (msg) {
           $("#cargaImg").hide();

           ConfigDev.dataSource = msg;
           ConfigDev.columnAutoWidth = true,
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
                      caption: "Acciones",
                  
                      cellTemplate: function (container, options) {
                          var lblDetalle = "<button class='btn-primary' onclick='ModalConsultarDetalleIngresos(" + JSON.stringify(options.data) + ")'>Detalle</button>";

                          $("<div>")
                              .append($(lblDetalle))
                              .appendTo(container);
                      }
                  },
                 {
                     dataField: "NumeroDocumento", caption: "Num Documento", fixed: false
        },
                 {
                     dataField: "CedulaCliente", caption: "Identificacion", width: 130
                 },
                {
                    dataField: "NombreCliente", caption: "Cliente", fixed: false, width: 250
                },
                
                {
               dataField: "GroupCode", caption: "Tipo cliente"
                },
                {
                    dataField: "ClienteClase", caption: "Cliente Clase"
                },
                {
                    dataField: "ClienteLinea", caption: "Cliente Linea"
                },
                {
                    dataField: "FechaIngreso", caption: "Fecha Ingreso"
                 
                },
                 {
                     dataField: "MesIngreso", caption: "Mes Ingreso"

                 },
                 {
                     dataField: "TipoIngreso", caption: "Tipo Ingreso"
                 },
                 {
                     dataField: "Bodega", caption: "Bodega",
                    
                 },
                  {
                      dataField: "CantidadTotal", caption: "Cantidad", allowFiltering: false
,

                  },
                  ,
                {
                    dataField: "PesoTeoricoTotalCalculado", caption: "Peso Teorico Total", alignment: "right", allowFiltering: false, width: 130
,
                    
                calculateCellValue: function (rowData) {
                    return (rowData.PesoTeoricoTotalCalculado).toFixed(2);

                }
                },
                   
                 {
                     dataField: "PesoBultoIngresado", caption: "Peso Ingresado", alignment: "right", allowFiltering: false, width: 130
,
                     calculateCellValue: function (rowData) {
                         return (rowData.PesoBultoIngresado).toFixed(2);
                        
                     }
                 },
                 
                 {
                     dataField: "PesoAjustadoTotal", caption: "Peso Ajustado Total", alignment: "right", visible: false, allowFiltering: false

                    ,
                   
                 },
                {
                    dataField: "Desviacion", caption: "Desviacion", alignment: "right", allowFiltering: false, customizeText: function (cellInfo) {
                        return cellInfo.value + "%";
                    }

                    
                }, {
                    dataField: "Comments", caption: "Comentarios", allowFiltering: false

                   
                },
           ];

           ConfigDev.summary = {
               totalItems: [
               {
                   name: "CantidadTotal",
                   column: "CantidadTotal",
                   summaryType: "sum",
                   displayFormat: "Cantidad Total",
                   showInColumn: "CantidadTotal",
                   customizeText: function (e) {
                       if (e.value != 0 && e.value != "") {
                           $("#txtSumaryPesos").val(e.value);
                           return "Total: " + (e.value);
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
                           const noTruncarDecimales = { maximumFractionDigits: 2, minimumFractionDigits :2};
                           ValTotal = (e.value).toLocaleString('en-US', noTruncarDecimales);
                           return "Total: " + ValTotal;
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
                           const noTruncarDecimales = { maximumFractionDigits: 2, minimumFractionDigits: 2 };
                           ValTotal = (e.value).toLocaleString('en-US', noTruncarDecimales);
                           return "Total: " + ValTotal;
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
                            return "Total: " + desviacion.toFixed(2) + "%";
                         }
                     }

                 },
             
               ],
           }
           $(".btn").attr("disabled", false);
           $(".btn-txt").text("Consultar");
           $("#IngresosChatarras").dxDataGrid(ConfigDev);

       },
       error: function (msg) {
           $(".btn").attr("disabled", false);
           $(".btn-txt").text("Consultar");
           $("#MensajeErrorInesperado").show('fade');
           $("#cargaImg").hide();

       }
    })
}
$('#LinkClose').on("click", function (e) {
    $("#MensajeErrorInesperado").hide('fade');
});
function ModalConsultarDetalleIngresos(modelo) {
    $('.modal-title').text("Detalle de Ingreso Chatarra #" + modelo.NumeroDocumento);
    $.ajax({
        url: "../Chatarra/ConsultaIngresoChatarraDetalleLocal?DocEntry=" + modelo.DocEntry,
        type: "GET"
       , success: function (msg) {
           ConfigDev.dataSource = msg;
           ConfigDev.columnAutoWidth = true,
           ConfigDev.showBorders = true,
              ConfigDev.filterRow = { visible: false },
               ConfigDev.filterPanel = { visible: false },
               ConfigDev.headerFilter = { visible: false },

           ConfigDev.columns = [
                { dataField: "DocEntry", visible: false },
                 { dataField: "CodigoItem", caption: "Codigo Item" },
                 { dataField: "Descripcion", caption: "Descripcion" },
                 { dataField: "Cantidad", caption: "Cantidad" },
                {
                    dataField: "PesoTeoricoUnitario", caption: "Peso Teorico unitario", alignment: "right",
                    calculateCellValue: function (rowData) {
                        return (rowData.PesoTeoricoUnitario).toFixed(2);
                    }
                },
                 {
                     dataField: "PesoTeoricoTotal", caption: "Peso Teorico Total", alignment: "right", calculateCellValue: function (rowData) {
                         return (rowData.PesoTeoricoTotal).toFixed(2);
                     }
                 },
                 {
                     dataField: "PesoUnitarioAjustado", caption: "Peso Unitario Ajustado", alignment: "right", calculateCellValue: function (rowData) {
                         return (rowData.PesoUnitarioAjustado).toFixed(2);
                     }
                 },
                {
                    dataField: "PesoTotalAjustado", caption: "Peso Ajustado Total", alignment: "right", calculateCellValue: function (rowData) {
                        return (rowData.PesoTotalAjustado).toFixed(2);
                    }
                }    
           ];
           $("#IngresosDetallesChatarrasLocal").dxDataGrid(ConfigDev);
       }, error: function (msg) {
         
           $("#MensajeErrorInesperado").show('fade');
       }
    })
    $("#ModalDetalleChatarraLocal").modal("show");
}

