var DescripcionItem = null;
var NumeroPallet = null;
var IdentificadorDetalle = null;
var IdentificadorPaking = null;
var NumeroPalletLocal = null;
var temp = null;
var contador = 0;
var cantidadPallet = 0;
var datosCabecera = null;
var datosDetalle = null;
var variable = null;

$(document).ready(function () {
    $('#txtLargoPallet').val(114);
    $('#txtAnchoPallet').val(114);
    $('#txtAltoPallet').val(114);
    $("#txtOrigenAct").text("Cd Gye");
    $(".loading-icon").css("display", "none");
    $(document).on('click', '.fa', function (event) {
        event.preventDefault();
        $(this).closest('tr').remove();
    });
    $("#image").removeClass("hide");
});

function Volumen() {
    var total = ((parseFloat($('#txtLargoPallet').val()) * parseFloat($('#txtAnchoPallet').val()) * parseFloat($('#txtAltoPallet').val())).toFixed(2))/1000000;
    $('#txtVolumenPallet').val(total.toFixed(3));
}
function NumeroContenedores() {
    $('#txtContenedorUno').val("");
    $('#txtContenedorDos').val("");
    $('#txtContenedorTres').val("");

    if ($("#txtNumeroContenedores option:selected").val() == 1) {
        document.getElementById("OcultarContenidoDiv1").style.display = "";
        document.getElementById("OcultarContenidoDiv2").style.display = "none";
        document.getElementById("OcultarContenidoDiv3").style.display = "none";

    }
     if ($("#txtNumeroContenedores option:selected").val() == 2) {
         document.getElementById("OcultarContenidoDiv1").style.display = "";
         document.getElementById("OcultarContenidoDiv2").style.display = "";
         document.getElementById("OcultarContenidoDiv3").style.display = "none";
    }
     if ($("#txtNumeroContenedores option:selected").val() == 3) {
         document.getElementById("OcultarContenidoDiv1").style.display = "";
         document.getElementById("OcultarContenidoDiv2").style.display = "";
         document.getElementById("OcultarContenidoDiv3").style.display = "";
    }
    if ($("#txtNumeroContenedores option:selected").val() == "") {
        document.getElementById("OcultarContenidoDiv1").style.display = "none";
        document.getElementById("OcultarContenidoDiv2").style.display = "none";
        document.getElementById("OcultarContenidoDiv3").style.display = "none";
    }

}
function ConsultarIngresosPacking() {
    $(".result").text("");
    $(".loading-icon").removeClass("hide");
    $(".btn").attr("disabled", true);
    $(".btn-txt").text("Espere...");
    ConsultarCabeceraOrdenVenta();
}
$('#LinkClose').on("click", function (e) {
    $("#MensajeGuardado").hide('fade');
});
$('#LinkClose2').on("click", function (e) {
    $("#MensajeErrorGuardado").hide('fade');
});
$('#LinkClose3').on("click", function (e) {
    $("#MensajeErrorGeneral").hide('fade');
});
$('#LinkClose10').on("click", function (e) {
    $("#MensajePackingCompletos").hide('fade');
});
$('#LinkCloseCompleteCampos').on("click", function (e) {
    $("#MensajeCompleteCamposNecesarios").hide('fade');
});


function ConsultarCabeceraOrdenVenta() {
    console.log("Ingreso Metodo");
    var texto;
    var urlCont = null;
    var valor = $("#TipoBusqueda").val();
    if (valor == "1") {
        variable = 1;
        console.log("ingreso If 1");
        texto = "EXPORTACION";
        urlCont = "../PackingList/ConsultaOrdenVentaListCabecera?Exportacion=" + texto;
    }
    if (valor == "2") {
        variable = 1;
        console.log("ingreso If 2");
        texto = "OFICINA";
        urlCont = "../PackingList/ConsultaOrdenVentaListCabecera?Exportacion=" + texto;
    }
    if (valor == "3") {
        variable = 2;
        console.log("ingreso If 3");
        texto = "Y";
        urlCont = "../PackingList/ConsultaFacturaReservaListCabecera?factReserv=" + texto;
    }
    if (valor == "4") {
        variable = 2;
        console.log("ingreso If 4");
        texto = "N";
        urlCont = "../PackingList/ConsultaFacturaReservaListCabecera?factReserv=" + texto;
    }
    if (valor == "5") {
        variable = 2;
        console.log("ingreso If 3");
        texto = "Y";
        urlCont = "../PackingList/ConsultaFacturaReservaListCabeceraCancelada?factReserv=" + texto;
    }

    $.ajax({
        url: urlCont,
        type: "GET"
          , success: function (msg) {
              ConfigDev.dataSource = msg;
              ConfigDev.keyExpr = "DocEntry",
              ConfigDev.columnAutoWidth = true,
              ConfigDev.showBorders = true,
               ConfigDev.allowColumnReordering = true,
              //ConfigDev.allowColumnResizing = true,
                 //ConfigDev.filterRow = { visible: true },
                  // ConfigDev.filterPanel = { visible: true },
                  ConfigDev.headerFilter = { visible: true },
                ConfigDev.columnFixing = {
                    enabled: true
                  },
                  ConfigDev.searchPanel = {
                      visible: true,
                      width: 240,
                      placeholder: "Buscar..."
                  },

              ConfigDev.columns = [

                  { dataField: "DocEntry", visible: false },
                   {
                       dataField: "SypExportacion", visible: false,
                   },
                   {
                       dataField: "NumeroOrden", caption: "Numero Orden", fixed: true, allowEditing: false, alignment: "left", headerFilter: true, allowHeaderFiltering: false, headerFilter: {
                           allowSearch: true,
                       }
                  }
                  ,
                  {
                      caption: "Detalle Orden", width:100,

                      cellTemplate: function (container, options) {

                          var btnDetalle = "<button class='btn-primary' onclick='ModalConsultarDetalleIngresosChatarra(" + JSON.stringify(options.data) + ")'>Detalle</button>";
                          //var lblEspacio = "<a> </a>"
                          //var btnPackingList = "<button class='btn-warning' onclick='IngresoPalletAct(" + JSON.stringify(options.data) + ")'>Registrar Orden</button>";



                          $("<div>")
                              .append($(btnDetalle)/*, $(lblEspacio), $(btnPackingList)*/)
                              .appendTo(container);
                      }
                  },
                    {
                        dataField: "DocNum", caption: "Numero Documento", allowEditing: false, alignment: "left", headerFilter: true, allowHeaderFiltering: false, headerFilter: {
                            allowSearch: true,
                        },
                  },
                  {
                      dataField: "Mes", caption: "Mes", allowEditing: false, alignment: "left", headerFilter: true, allowHeaderFiltering: true
                  },
                  {
                      dataField: "DocDate", caption: "Fecha Contabilizacion", allowEditing: false, alignment: "left", headerFilter: true, allowHeaderFiltering: false, visible:false
                    },
                   {
                       dataField: "TaxDate", caption: "Fecha Documento", allowEditing: false, alignment: "left", headerFilter: true, allowHeaderFiltering: false
                   },
                   {
                       dataField: "CardCode", caption: "Identificacion", allowEditing: false, alignment: "left", headerFilter: true, allowHeaderFiltering: false, visible: false, headerFilter: {
                           allowSearch: true,
                       }
                   },
                    {
                        dataField: "CardName", caption: "Cliente", allowEditing: false, alignment: "left", headerFilter: true, allowHeaderFiltering: false, headerFilter: {
                            allowSearch: true,
                        },
                    },
                    {
                      dataField: "DocTotal", caption: "Valor Total", alignment: "right", visible: false, headerFilter: true, allowHeaderFiltering: false, calculateCellValue: function (rowData) {
                            return (rowData.DocTotal).toFixed(2);
                        }
                    },  
                   {
                       caption: "Acciones", width: 140,

                       cellTemplate: function (container, options) {

                           //var btnDetalle = "<button class='btn-primary' onclick='ModalConsultarDetalleIngresosChatarra(" + JSON.stringify(options.data) + ")'>Detalle de Orden</button>";
                           //var lblEspacio = "<a> </a>"
                           var btnPackingList = "<button class='btn-warning' onclick='IngresoPalletAct(" + JSON.stringify(options.data) + ")'>Registrar Orden</button>";
                          


                           $("<div>")
                               .append(/*$(btnDetalle), $(lblEspacio),*/ $(btnPackingList))
                               .appendTo(container);
                       }
                   }
              ];
              $(".btn").attr("disabled", false);
              $(".btn-txt").text("Consultar");
              $("#tblPackingList").dxDataGrid(ConfigDev);
              $("#tblOrdenesVentas").dxDataGrid(ConfigDev);

          },
          error: function (msg) {
              $(".btn").attr("disabled", false);
              $(".btn-txt").text("Consultar");
              $("#MensajeErrorGeneral").show('fade');
              setTimeout(function () {
                  $("#MensajeErrorGeneral").fadeOut(1500);
              }, 3000);
          }
    })
}

function ModalConsultarDetalleIngresosChatarra(modelo) {
    var urlcontrolador = null;

    //if (variable = 1) {

    urlcontrolador = "../PackingList/ConsultaOrdenVentaDetalle?DocEntry=" + modelo.DocEntry;
    //console.log("url:" + urlcontrolador);
    //console.log("doc entry:" + modelo.DocEntry);
    //}
    //if (variable = 2) {
       // urlcontrolador = "../PackingList/ConsultaFactReservaDetalle?DocEntry=" + modelo.DocEntry;
    //}

    $.ajax({
        url: urlcontrolador,
        type: "GET"
       , success: function (msg) {
           ConfigDev.dataSource = msg;
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
               { dataField: "WhsCode", caption: "Almacen" },
                { dataField: "ItemCode", caption: "Codigo Item" },
               { dataField: "Descripcion", caption: "Descripcion" },
               { dataField: "Text", caption: "Descripcion Cliente" },

                  {
                      dataField: "Cantidad", caption: "Cantidad"
                  },
                  {
                      dataField: "Precio", caption: "Precio Unitario", alignment: "right", visible: false, calculateCellValue: function (rowData) {
                          return (rowData.Precio).toFixed(2);
                      }
                  },
                  {
                      dataField: "PrecioTotal", caption: "Precio Total", alignment: "right", visible: false, calculateCellValue: function (rowData) {
                          return (rowData.PrecioTotal).toFixed(2);
                      }
                  }
           ];
           ConfigDev.summary = {
               totalItems: [
               {
                   name: "Cantidad",
                   column: "Cantidad",
                   summaryType: "sum",
                   displayFormat: "Total: {0}",
                   showInColumn: "PesoTeoricoSubtotal",
                   customizeText: function (e) {
                       if (e.value != 0 && e.value != "") {
                           return "Total: " + (e.value)
                       }
                   }
               }],
           }
          
           $(".btn").attr("disabled", false);
           $(".btn-txt").text("Consultar");
           configDevDataSource = ConfigDev;

           $("#tblDetalleOrdenesVentas").dxDataGrid(ConfigDev);
       },
        error: function (msg) {
            $(".btn").attr("disabled", false);
            $(".btn-txt").text("Consultar");
            $("#MensajeErrorGeneral").show('fade');
            setTimeout(function () {
                $("#MensajeErrorGeneral").fadeOut(1500);
            }, 3000);
        },

    })

    $("#ModalDetalleOrdenVenta").modal("show");
}


function IngresoPallet(modelo) {
    console.log("Pallet");
    console.log(NumeroPallet);

    $('.table tbody tr').slice(0).remove();
    $('.table tbody tr').slice(0).empty();

    $(".numero").remove();
    $(".descripcion").remove();
    $(".cantidad").remove();
    $(".fa").remove();
    $(".i").remove();

    $("#txtVolumen").val("");
    $("#txtGrossWeight").val("");
    $("#txtNetWeight").val("");
    $("#txtPalletNumber").val("");

    let data_modelo = [];

    $("#txtOrden").val(modelo.DocNum);
    $("#txtCliente").val(modelo.CardName);

   
    $.ajax({
        url: "../PackingList/ConsultaOrdenVentaDetalle?DocEntry=" + modelo.DocEntry,
        type: "GET",
        dataType: 'json'

     , success: function (msg) {

         $.ajax({
             url: "../PackingList/ObtenerNumeroPallet",
             type: "POST",
             data: {
                 Orden: modelo.DocNum
             }
   , success: function (msg) {
       $("#txtPalletNumber").val(msg);
    
       console.log(msg);

   },
             error: function (msg) {
                 console.log("Error");
                 $("#MensajeErrorGeneral").show('fade');
                 setTimeout(function () {
                     $("#MensajeErrorGeneral").fadeOut(1500);
                 }, 3000);
             },

         })
        
         var selectElement = document.getElementById("TipoDescripcion");

         while (selectElement.length > 0) {
             selectElement.remove(0);
         }

         for (var i = 0, len = msg.length; i < len; i++) {
             console.log(msg[i]['Descripcion']);
             data_modelo.push(msg[i]['Descripcion']);
         }
         var select = document.getElementById("TipoDescripcion"); //Seleccionamos el select
         for (var i = 0; i < data_modelo.length; i++) {
             var option = document.createElement("option"); //Creamos la opcion
             option.innerHTML = data_modelo[i]; //Metemos el texto en la opción
             select.appendChild(option); //Metemos la opción en el select
         }
       

     },
        error: function (msg) {
            $("#MensajeErrorGuardado").show('fade');
            setTimeout(function () {
                $("#MensajeErrorGuardado").fadeOut(1500);
            }, 3000);
        },

    })

    $("#ModalIngresoPallet").modal("show");
}

function IngresoPalletAct(modelo) {
    //NumeroOrden
    datosCabecera = modelo;
    cantidadPallet = $("#txtCantidadPallet").val();
    contador = 0;
    IdentificadorDetalle = modelo.DocEntry;
    let data_modelo = [];
    $("#txtOrdenAct").val();
    $("#txtCantidadPallet").val();
    $("#txtDocAct").val(modelo.DocNum);
    $("#txtClienteAct").val(modelo.CardName);
    $("#txtOrdenAct").val(modelo.NumeroOrden);

    $("#ModalIngresoPalletAct").modal("show");
}


$('#RegistrarPaking').on("click", function (e) {
    validarIngresos();
});
function validarIngresos() {
    var valor1 = $("#txtOrdenAct").val();
    var valor3 = $("#txtNumeroContenedores").val();
  
    if (valor1.length == 0) {
        $("#MensajeCompleteCamposNecesarios").show('fade');
        setTimeout(function () {
            $("#MensajeCompleteCamposNecesarios").fadeOut(1500);
        }, 3000);
        return;
    }

    if (valor3.length == 0) {
        $("#MensajeCompleteCamposNecesarios").show('fade');
        setTimeout(function () {
            $("#MensajeCompleteCamposNecesarios").fadeOut(1500);
        }, 3000);
        return;
    }
    RegistrarPacking();
}

function RegistrarPacking() {
    var NumeroCont = $("#txtNumeroContenedores").val();
    $.ajax({
        url: "../PackingList/RegistrarPallet",
        type: "POST",
        data: {
            NumeroDocumento: $("#txtDocAct").val(), NumeroOrden: $("#txtOrdenAct").val(), NombreCliente: $("#txtClienteAct").val(), Origen: $("#txtOrigenAct").val(), Destino: $("#txtDestinoAct option:selected").text(), IdentificadorDetalle: IdentificadorDetalle, tipo: $("#TipoBusqueda").val(), vari: variable, Sucursal: $("#txtSucursal").val(),
            numeroContenedor: NumeroCont,
        },
        success: function (msg) {
            if (msg==0) {
                $("#ModalIngresoPalletAct").modal("hide");
                $("#MensajeErrorGuardado").show('fade');
                setTimeout(function () {
                    $("#MensajeErrorGuardado").fadeOut(1500);
                }, 3000);
            }
            else {
                location.href = "../PackingList/ListadoPackingList";
            }

            //alert("Listo");
            //IdentificadorPaking = msg;
            //ConsultaEstado();
            //$("#txtSucursal").val("");
            //$("#txtCantidadPallet").val("");

            //$("#ModalIngresoPalletAct").modal("hide");
            //ConsultarCabeceraOrdenVenta();
            //$("#txtCantidadPallet").val();
        },
        error: function (msg) {
            $("#ModalIngresoPalletAct").modal("hide");
            $("#MensajeErrorGuardado").show('fade');
            setTimeout(function () {
                $("#MensajeErrorGuardado").fadeOut(1500);
            }, 3000);

        }
    })
}
function VerNumeroPallet() {
    $.ajax({
        url: "../PackingList/ObtenerNumeroPallet?IdentificadorPacking=" + IdentificadorPaking,
        type: "GET",
        success: function (msg) {
            NumeroPalletLocal = msg;
            $('#lblNumberPallet').html("Pallet  #" + msg);
            $('#txtNumeroPalle').val(msg);
            
            console.log("El valor de packing es" + msg);
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

function ConsultaEstado() {
    $.ajax({
        url: "../PackingList/ConsultarEstadoPacking?PackingId=" + IdentificadorPaking,
        type: "GET",
        success: function (msg) {
            console.log("El valor que traigo es :" + msg);

            if (msg == "False") {
                console.log("entre x falso");
                AbrirPallet();

            }
            else {
                ConsultarCabeceraOrdenVenta();
                $("#ModalIngresoPallet").modal("hide");
                $("#ModalIngresoPalletAct").modal("hide");
                $("#ModalDetalleOrdenVenta").modal("hide");

                
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
function AbrirPallet() {
    document.getElementById("BtnregistrarPallet").disabled = true;

    var startUpdating = false;

    console.log("Ingrse en el metodo nuevo");
    contador = contador + 1;
    $("#txtLargoPallet").val(114);
    $("#txtAltoPallet").val(114);
    $("#txtAnchoPallet").val(114);

    $("#txtVolumenPallet").val("");
    Volumen();

    $("#txtPesoNeto").val("");
    $("#txtPesoBruto").val("");

    VerNumeroPallet();

    var urlcontrolador2 = null;

    //if (variable = 1) {
    //    urlcontrolador2 = "../PackingList/ConsultaOrdenVentaDetalle?DocEntry=" + modelo.DocEntry;
    //}
    //if (variable = 2) {
    //    urlcontrolador2 = "../PackingList/ConsultaFactReservaDetalle?DocEntry=" + modelo.DocEntry;
    //}

    $.ajax({
        url: "../PackingList/ObtenerDetallePallet?IdentificadorPacking=" + IdentificadorPaking,
        type: "GET"
        , success: function (msg) {
            temp = msg;

            $("#tblDetallePallet").dxDataGrid({
                dataSource : temp,
                columnAutoWidth : true,
                showBorders : true,
                keyExpr  :"PackingDtlId",
                allowColumnReordering : true,
                headerFilter : false,
                filterPanel : false,
                filterRow : false,
                editing : {
                    mode: "batch",
                    allowUpdating: true,
                    selectTextOnEditStart: true,
                    startEditAction: "click"

                },
                columnFixing : {
                    enabled: true
                },

                 paging: {
                    pageSize: 10
                },
                repaintChangesOnly: true,
                pager: {
                    visible: true,
                    allowedPageSizes: [5, 10, 100],
                    showPageSizeSelector: true,
                    showInfo: true,
                    showNavigationButtons: true
                },
                scrolling: {
                    columnRenderingMode: "virtual"
                },
                columns: [
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
                        dataField: "TotalItem", caption: "Acumulado", allowEditing: false
                    },
                    {
                        dataField: "Pallet", caption: "Cantidad en Pallet",
                        setCellValue: function (newData, value, currentRowData) {
                            newData.Pallet = value;
                            newData.TotalItem = currentRowData.TotalItem2 + value;
                            newData.SaldoItem = currentRowData.SaldoItem2 - value;
                        }

                    },
                    { dataField: "TotalItem2", visible: false },
                    {
                        dataField: "SaldoItem", caption: "Saldo Pendiente", allowEditing: false
                    },
                    { dataField: "SaldoItem2", visible: false },
                    {
                        dataField: "Status", caption: "Estado", allowEditing: false
                    }
                ],
                summary: {
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
                onRowUpdating: function (e) {
                    startUpdating = true;
                },
                onContentReady: function (e) {
                    if (startUpdating) {
                        startUpdating = false;
                        document.getElementById("BtnregistrarPallet").disabled = false;
                    }
                }, onEditorPreparing: function (e) {
                    e.editorOptions.onValueChanged = function (arg) {
                        document.getElementById("BtnregistrarPallet").disabled = true;
                        e.setValue(arg.value);
                    }
                },
            });
            $(".btn").attr("disabled", false);
            $(".btn-txt").text("Consultar");

        },
        error: function (msg) {
            $(".btn").attr("disabled", false);
            $(".btn-txt").text("Consultar");
            $("#MensajeErrorGeneral").show('fade');
            setTimeout(function () {
                $("#MensajeErrorGeneral").fadeOut(1500);
            }, 3000);
        }
    })
    $("#ModalIngresoPallet").modal("show");
}
function AbrirPalletPrue() {
    contador = contador + 1;
    $("#txtLargoPallet").val("");
    $("#txtAltoPallet").val("");
    $("#txtAnchoPallet").val("");
    $("#txtVolumenPallet").val("");
    $("#txtPesoNeto").val("");
    $("#txtPesoBruto").val("");

    VerNumeroPallet();
    $.ajax({
        url: "../PackingList/ObtenerDetallePallet?IdentificadorPacking=" + IdentificadorPaking,
        type: "GET"
          , success: function (msg) {

              temp = msg;
              ConfigDev.dataSource = temp;
              ConfigDev.columnAutoWidth = true,
              ConfigDev.showBorders = true,
              ConfigDev.keyExpr = "PackingDtlId",
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
                       dataField: "TotalItem", caption: "Acumulado", allowEditing: false
                  },
                  {
                      dataField: "Pallet", caption: "Cantidad en Pallet",
                      dataType: "number",
                      setCellValue: function (newData, value, currentRowData) {
                          newData.Pallet = value;

                          newData.TotalItem = currentRowData.TotalItem + value;
                          newData.SaldoItem = currentRowData.SaldoItem - value;
                      }

                  },
                    {
                        dataField: "SaldoItem", caption: "Saldo Pendiente", allowEditing: false
                    },
                       {
                           dataField: "Status", caption: "Estado", allowEditing: false
                       }
                 
              ];
              $(".btn").attr("disabled", false);
              $(".btn-txt").text("Consultar");
              $("#tblPackingList").dxDataGrid(ConfigDev);
              $("#tblDetallePallet").dxDataGrid(ConfigDev);

          },
        error: function (msg) {
            $(".btn").attr("disabled", false);
            $(".btn-txt").text("Consultar");
            $("#MensajeErrorGeneral").show('fade');
            setTimeout(function () {
                $("#MensajeErrorGeneral").fadeOut(1500);
            }, 3000);
        }
    })
    $("#ModalIngresoPallet").modal("show");
}


$('#RegistrarPakingLocal').on("click", function (e) {
    registrarPallet();
});


function registrarPallet() {
    $.ajax({
        url: "../PackingList/RegistrarPalletPacking",
        type: "POST",
        data: {
            Array: temp, idPacking: IdentificadorPaking, PalletNumber: $('#txtNumeroPalle').val(), LargoPallet: $("#txtLargoPallet").val(), AltoPallet: $("#txtAltoPallet").val(), AnchoPallet: $("#txtAnchoPallet").val(), VolumenPallet: $("#txtVolumenPallet").val(), PesoNeto: $("#txtPesoNeto").val(), PesoBruto: $("#txtPesoBruto").val()
        },
        success: function (msg) {
            ConsultaEstado();
        },
        error: function (msg) {
            $("#ModalIngresoPalletAct").modal("hide");
            $("#MensajeErrorGuardado").show('fade');
            setTimeout(function () {
                $("#MensajeErrorGuardado").fadeOut(1500);
            }, 3000);

        }
    })
}

function IngresarModelo() {
    var tipo = $('#TipoDescripcion').val();
    var cantidad = $('#txtCantidad').val();
    console.log(cantidad)
    if (cantidad != 0) {
        const row = agregarFila({
            descripcion: $('#TipoDescripcion').val(),
            cantidad: $('#txtCantidad').val()
        });
        $(".table").append(row);
        clean();
    }
    else {
        $("#MensajeErrorGeneral").show('fade');
        setTimeout(function () {
            $("#MensajeErrorGeneral").fadeOut(1500);
        }, 3000);
    }
}


//$('#RegistrarPakingLocal').on("click", function (e) {
//    registrarPallet();
//});
function GenerarQr() {
    console.log("Ingreso a generar qr");
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
function CrearPdf(codigo) {
    console.log("Ingreso a crear pdf");

    $.ajax({
        url: "../PackingList/CrearPdf",
        type: "POST",  
        data: {
            NumeroPallet: $('#txtNumeroPalle').val(), Orden: $('#txtOrdenAct').val(), Cliente: datosCabecera.CardName, Volumen: $('#txtVolumenPallet').val(), GrossWeight: $('#txtPesoBruto').val(),
            NetWeight: $('#txtPesoNeto').val(), Origen: $("#txtOrigenAct").val(), Destino: $("#txtDestinoAct option:selected").text(), items: temp,Qr:codigo
        },
        success: function (e) {

            console.log(e);
            var blob = new Blob([e], { type: 'application/pdf' });

            var URL = window.URL || window.webkitURL;
            var downloadUrl = URL.createObjectURL(blob);

        },
        error: function (msg) {
            $("#MensajeErrorGeneral").show('fade');
            setTimeout(function () {
                $("#MensajeErrorGeneral").fadeOut(1500);
            }, 3000);
        }
    })

   

}

function CierraPopup() {
    $("#ModalIngresoPallet").modal('hide');//ocultamos el modal
    $('body').removeClass('modal-open');//eliminamos la clase del body para poder hacer scroll
    $('.modal-backdrop').remove();//eliminamos el backdrop del modal
}

