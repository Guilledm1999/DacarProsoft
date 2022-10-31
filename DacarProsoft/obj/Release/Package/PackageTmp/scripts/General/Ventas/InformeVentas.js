
$(document).ready(function () {
    $(".loading-icon").css("display", "none");
});

function ConsultaDeVentas() {
    $(".result").text("");
    $(".loading-icon").removeClass("hide");
    $(".btn").attr("disabled", true);
    $(".btn-txt").text("Espere...");
    ConsultarVentas();
}
$('#LinkClose').on("click", function (e) {
    $("#MensajeErrorInesperado").hide('fade');
});

function ActivarConsultarVentas() {
    $('#Resumen').html("");
    $('#Total').html("");
    $('#CostoTotal').html("");
    $('#CostoUnitario').html("");
    $('#Precio').html("");
    $('#PesoUnitario').html("");
    $('#PesoTotal').html("");
    $('#Cantidad').html("");

    ConsultarVentas();
}

function ConsultarVentas() {
    var select = $("#anioClass option:selected").text();
    var valor = $("#anioClass").val();
    var select2 = $("#mesesClass option:selected").text();
    var valor2 = $("#mesesClass").val();
    //console.log(select2, select);
    //console.log("../Ventas/ConsultaDeVentasPorAnio?anio=" + select + "&mes=" + select2);


    $.ajax({
        url: "../Ventas/ConsultaDeVentasPorAnio?anio=" + select + "&mes=" + select2,
        type: "GET"
       , success: function (msg) {
           ConfigDev.dataSource = msg;

           ConfigDev.allowColumnReordering = true,
           ConfigDev.allowColumnResizing = true,
           ConfigDev.columnAutoWidth = true,
           ConfigDev.showBorders = true,
           ConfigDev.columnFixing = {
               enabled: true
               },
               ConfigDev.searchPanel = {
                   visible: true,
                   width: 240,
                   placeholder: "Buscar..."
               },
           ConfigDev.keyExpr = "Secuencia",
           ConfigDev.grouping = {
           autoExpandAll: false,
           },
           ConfigDev.filterRow = { visible: false },
           ConfigDev.filterPanel = { visible: false },
           ConfigDev.headerFilter = { visible: true },
       ConfigDev.groupPanel = {
           visible: false,
       },
           ConfigDev.columns = [
                { dataField: "Secuencia", caption: "Secuencia", fixed: true, sortOrder: 'desc' },
                {
                    dataField: "Cuenta", caption: "Cuenta", fixed: true
                },
                { dataField: "Nombre_Cuenta", caption: "Nombre Cuenta" },
                 { dataField: "Fecha", caption: "Fecha"},
                 { dataField: "Mercado", caption: "Mercado" },
                { dataField: "Vendedor", caption: "Vendedor" },
                { dataField: "Cliente", caption: "Cliente" },
                { dataField: "Clie_tipo", caption: "Tipo de cliente" },
                 { dataField: "Cliente_Linea", caption: "Cliente Linea" },
                { dataField: "Cliente_Clase", caption: "Cliente Clase" },
                 { dataField: "prod_linea", caption: "Producto Linea" },
                { dataField: "Prod_clase", caption: "Producto Clase" },
                { dataField: "Prod_grupo", caption: "Producto Grupo" },
                 { dataField: "Producto", caption: "Producto" },
                { dataField: "Marca", caption: "Marca" },
                 { dataField: "ModeloBC", caption: "Modelo BC" },
                {
                    dataField: "Cantidad", caption: "Cantidad",allowHeaderFiltering :false, headerCellTemplate: function (container, options) {
                        var lblDetCantidad = "<a>Cantidad</a>";
                        var Salto = "<br />";
                        var lblCantidad = "<a class='TotalCantidad'>Cantidad</a>";
                        $("<div>")
                            .append($(lblDetCantidad),$(Salto), $(lblCantidad))
                            .appendTo(container);
                    }
                },
                 {
                     dataField: "Precio", caption: "Precio", allowHeaderFiltering: false, headerCellTemplate: function (container, options) {
                         var lblDetPrecio = "<a>Precio</a>";
                         var Salto = "<br />";
                         var lblPrecio = "<a class='TotalPrecio'>Precio</a>";
                         $("<div>")
                             .append($(lblDetPrecio), $(Salto), $(lblPrecio))
                             .appendTo(container);
                     }
                 },
                { dataField: "Parcial", caption: "Parcial", allowHeaderFiltering: false },
                { dataField: "porc_desc", caption: "Porcentaje de descuento", allowHeaderFiltering: false },
                 { dataField: "Descuento", caption: "Descuento", allowHeaderFiltering: false },
                { dataField: "base0", caption: "Base 0", allowHeaderFiltering: false },
                 {
                     dataField: "Subtotal", caption: "Subtotal",allowHeaderFiltering :false,headerCellTemplate: function (container) {
                         var lblDetSubtotal = "<a>SubTotal </a>";
                         var Salto = "<br />";
                         var lblSubtotal = "<a class='TotalSubtotal'>SubTotal </a>";
                         $("<div>")
                             .append($(lblDetSubtotal), $(Salto), $(lblSubtotal))
                             .appendTo(container);
                     }
                 },
                { dataField: "impuesto", caption: "Impuesto", allowHeaderFiltering: false },
                 {
                     dataField: "Total", caption: "Total", headerCellTemplate: function (container) {
                         var lblDetTotal = "<a>Total </a>";
                         var Salto = "<br />";
                         var lblTotal = "<a class='TotalCabecera'>Total </a>";

                         $("<div>")
                             .append($(lblDetTotal), $(Salto), $(lblTotal))
                             .appendTo(container);
                     }
                 },
                {
                    dataField: "Costo_uni", caption: "Costo Unitario", allowHeaderFiltering: false
                },
                {
                    dataField: "Costo_tot", caption: "Costo Total", allowHeaderFiltering: false, headerCellTemplate: function (container) {
                        var lblDetCostoT = "<a>Costo Total</a>";
                        var Salto = "<br />";
                        var lblCostoT = "<a class='TotalCostoTotal'>Costo Total</a>";
                        $("<div>")
                            .append($(lblDetCostoT), $(Salto), $(lblCostoT))
                            .appendTo(container);
                    }
                },
                 {
                     dataField: "Peso_uni", caption: "Peso Unitario"
                 },
                  {
                      dataField: "Peso_tot", caption: "Peso Total", allowHeaderFiltering: false, headerCellTemplate: function (container) {
                          var lblDetPrecioT = "<a>Peso Total</a>";
                          var Salto = "<br />";
                          var lblPrecioT = "<a class='TotalPesoTotal'>Peso Total</a>";

                          $("<div>")
                              .append($(lblDetPrecioT), $(Salto), $(lblPrecioT))
                              .appendTo(container);
                      }
                  },
                 { dataField: "Tipo_de_Nc", caption: "Tipo de nota de crédito", allowHeaderFiltering: false }
           ];
     
           $(".btn").attr("disabled", false);
           $(".btn-txt").text("Consultar");
           ConfigDev.summary = {
               totalItems: [
                   {
                       name: "Costo_tot",
                       showInColumn: "Costo_tot",
                       displayFormat: "",
                       valueFormat: "currency",
                       summaryType: "custom",
                       customizeText: function (e) {
                           if (e.value != 0 && e.value != "") {
                               $(".TotalCostoTotal").text('$ ' + ((e.value).toLocaleString(undefined, {
                                   minimumFractionDigits: 2,
                                   maximumFractionDigits: 2
                               })));

                               return ""
                           }
                           return "";
                       }
                   },
                   {
                       name: "Total",
                       showInColumn: "Total",
                       displayFormat: "",
                       valueFormat: "currency",
                       summaryType: "custom",
                       customizeText: function (e) {
                           if (e.value != 0 && e.value != "") {
                               $(".TotalCabecera").text('$ ' + ((e.value).toLocaleString(undefined, {
                                   minimumFractionDigits: 2,
                                   maximumFractionDigits: 2
                               })));

                               return ""
                           }
                           return "";
                       }
                   },
                   {
                       name: "SubTotal",
                       showInColumn: "Subtotal",
                       displayFormat: "",
                       valueFormat: "currency",
                       summaryType: "custom",
                       customizeText: function (e) {
                           if (e.value != 0 && e.value != "") {
                               $(".TotalSubtotal").text('$ ' + ((e.value).toLocaleString(undefined, {
                                   minimumFractionDigits: 2,
                                   maximumFractionDigits: 2
                               })));
                               return ""
                           }
                           return "";
                       }
                   },
                   {
                       name: "Precio",
                       showInColumn: "Precio",
                       displayFormat: "",
                       valueFormat: "currency",
                       summaryType: "custom",
                       customizeText: function (e) {
                           if (e.value != 0 && e.value != "") {
                               $(".TotalPrecio").text('$ ' + ((e.value).toLocaleString(undefined, {
                                   minimumFractionDigits: 2,
                                   maximumFractionDigits: 2
                               })));
                               return ""
                           }
                           return "";
                       }
                   },
          
                   {
                       name: "Peso_tot",
                       showInColumn: "Peso_tot",
                       displayFormat: "",
                       valueFormat: "decimal",
                       summaryType: "custom",
                       customizeText: function (e) {
                           if (e.value != 0 && e.value != "") {
                               $(".TotalPesoTotal").text(((e.value).toLocaleString(undefined, {
                                   minimumFractionDigits: 2,
                                   maximumFractionDigits: 2
                               })) + ' Kg');
                               return ""
                           }
                           return "";
                       }
                   },
                   {
                       name: "Cantidad",
                       showInColumn: "Cantidad",
                       displayFormat: "{0}",
                       valueFormat: "currency",
                       summaryType: "custom",
                       customizeText: function (e) {
                           if (e.value != 0 && e.value != "") {
                               $(".TotalCantidad").text(((e.value).toLocaleString(undefined, {
                                   minimumFractionDigits: 0,
                                   maximumFractionDigits: 0
                               })));
                               return ""
                           }
                           return "";
                       }
                   }
               ],
               calculateCustomSummary: function (options) {
                   if (options.name === "Total") {
                       if (options.summaryProcess === "start") {
                           options.totalValue = 0;
                       }
                       if (options.summaryProcess === "calculate") {

                               options.totalValue = options.totalValue + options.value.Total;
                               ObtenerValorCabecera(options.totalValue);

                       }
                   }
                   if (options.name === "Costo_tot") {
                       if (options.summaryProcess === "start") {
                           options.totalValue = 0;
                       }
                       if (options.summaryProcess === "calculate") {
                               options.totalValue = options.totalValue + options.value.Costo_tot;
                               ObtenerCostoTotal(options.totalValue);                        
                       }

                   }
                   if (options.name === "SubTotal") {
                       if (options.summaryProcess === "start") {
                           options.totalValue = 0;
                       }
                       if (options.summaryProcess === "calculate") {
                           options.totalValue = options.totalValue + options.value.Subtotal;
                           ObtenerSubTotal(options.totalValue);
                       }

                   }
                   if (options.name === "Precio") {
                       if (options.summaryProcess === "start") {
                           options.totalValue = 0;
                       }
                       if (options.summaryProcess === "calculate") {
                               options.totalValue = options.totalValue + options.value.Precio;
                               ObtenerPrecio(options.totalValue);
                       }

                   }
                
                   if (options.name === "Peso_tot") {
                       if (options.summaryProcess === "start") {
                           options.totalValue = 0;
                       }
                       if (options.summaryProcess === "calculate") {
                               options.totalValue = options.totalValue + options.value.Peso_tot;
                               ObtenerPesoTotal(options.totalValue);
                       }

                   }
                   if (options.name === "Cantidad") {
                       if (options.summaryProcess === "start") {
                           options.totalValue = 0;
                       }
                       if (options.summaryProcess === "calculate") {
                               options.totalValue = options.totalValue + options.value.Cantidad;
                               ObtenerCantidad(options.totalValue);
                       }

                   }

               }

           }
           $("#tblVentasTotales").dxDataGrid(ConfigDev);
           
       },
        error: function (msg) {
            $(".btn").attr("disabled", false);
            $(".btn-txt").text("Consultar");
            $("#MensajeErrorInesperado").show('fade');
        }

    })
}

function ObtenerValorCabecera(valor) {
    $('.TotalCabecera').text('Total:  $' + valor.toLocaleString(undefined, {
        minimumFractionDigits: 2,
        maximumFractionDigits: 2
    }));
}

function ObtenerCostoTotal(valor) {
    $('.TotalCostoTotal').text('Costo Total:  $' + (valor.toLocaleString(undefined, {
        minimumFractionDigits: 2,
        maximumFractionDigits: 2
    })));

}

function ObtenerPrecio(valor) {
    $('.TotalPrecio').text('Precio Total S/D: $' + (valor.toLocaleString(undefined, {
        minimumFractionDigits: 2,
        maximumFractionDigits: 2
    })));

}

function ObtenerPesoTotal(valor) {
    $('.TotalPesoTotal').text('Pesos Totales: ' + (valor.toLocaleString(undefined, {
        minimumFractionDigits: 2,
        maximumFractionDigits: 2
    }))+' Kg');

}
function ObtenerCantidad(valor) {
    $('.TotalCantidad').text('Cantidad Total: ' + (valor.toLocaleString(undefined, {
        minimumFractionDigits: 0,
        maximumFractionDigits: 0
    })));

}

function ObtenerSubTotal(valor) {
    $('.TotalSubtotal').text('SubTotal: $ ' + (valor.toLocaleString(undefined, {
        minimumFractionDigits: 2,
        maximumFractionDigits: 2
    })));

}






