var packingId = null;
var palletId = null;
var resTempo = null;
var modelTemp = null;

$(document).ready(function () {
    mostrarIngresosPallet();
});

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
    $("#MensajePackingSinDetalle").hide('fade');
});
$('#LinkClose11').on("click", function (e) {
    $("#MensajeEliminacionCorecta").hide('fade');
});
$('#LinkClose12').on("click", function (e) {
    $("#MensajeEliminacionIncorecta").hide('fade');
});

function mostrarIngresosPallet() {
    $.ajax({
        url: "../Calidad/ObtenerPalletIngresadosLiberacionLocal",
        type: "GET"
        , success: function (msg) {
            const locale = getLocale();
            DevExpress.localization.locale(locale);
            $("#tblIngresosdePacking").dxDataGrid({
                dataSource: msg,
                keyExpr: 'DocEntry',
                allowColumnReordering: true,
                allowColumnResizing: true,
                columnAutoWidth: true,
                showBorders: true,
                paging: {
                    pageSize: 10
                },
                headerFilter: {
                    visible: true
                },
                pager: {
                    visible: true,
                    allowedPageSizes: [5, 10, 100],
                    showPageSizeSelector: true,
                    showInfo: true,
                    showNavigationButtons: true
                },
                searchPanel: {
                    visible: true,
                    placeholder: "Buscar..."
                },
                columns: [
                    {
                        dataField: "DocNum", caption: "# Secuencial Sap", allowEditing: false, allowHeaderFiltering: false
                    },
                    {
                        dataField: "DocEntry", caption: "Codigo Pedido", allowEditing: false, allowHeaderFiltering: false
                    },      
                    {
                        dataField: "NumeroOrden", caption: "Numero Orden", allowEditing: false, headerFilter: false, allowHeaderFiltering: false
                    },
                    {
                        dataField: "CardName", caption: "Cliente", allowEditing: false, headerFilter: true, allowHeaderFiltering: true
                    },
                    {
                        dataField: "TaxDate", caption: "Fecha Registro Sap", allowEditing: false, headerFilter: true, allowHeaderFiltering: true ,dataType: 'date',
                    },            
                    {
                        caption: "Acciones", type: "buttons",
                        buttons: [{
                            text: "Registrar",
                            icon: "check",
                            type: 'success',
                            hint: "Registrar",
                            onClick: function (e) {    
                                ModalConfirmarLiberacion(e.row.data);
                            }
                        },
                        {
                            text: "Detalle",
                            icon: "menu",
                            hint: "Detalle",
                            onClick: function (e) {
                                // Execute your command here
                                ModalConsultarPalletsIngresado(e.row.data);
                            }
                        }]
                    }

                ],
            });

        },
        error: function (msg) {
            $("#MensajeErrorGeneral").show('fade');
            setTimeout(function () {
                $("#MensajeErrorGeneral").fadeOut(1500);
            }, 3000);
        }
    })
    function getLocale() {
        const storageLocale = sessionStorage.getItem('locale');
        return storageLocale != null ? storageLocale : 'es';
    }
}


function generarInformePackingListPDF(modelo) {
        if (modelo.DetalleIngresado == "Incompleto" || modelo.Estado == "Incompleto") {
        $("#MensajePackingSinDetalle").show('fade');
        setTimeout(function () {
            $("#MensajePackingSinDetalle").fadeOut(1500);
        }, 3000);
    } else {
        idPacking = modelo.PackingId;
        $("#ModalAfirmacionLiberacion").modal("show");
    }
}

function ConfirmarRegistro() {
    $.ajax({
        url: '../Calidad/RegistrarPedidoLocal',
        type: 'POST',
        data: {
            cabeceraOrdenVenta: modelTemp
        },
        success: function (respuesta) {
            console.log("respuesta:" + respuesta)
            if (respuesta == "True") {
                mostrarIngresosPallet();
                $("#ModalAfirmacionRegistro").modal("hide");
                $("#MensajeGuardado").show('fade');
                setTimeout(function () {
                    $("#MensajeGuardado").fadeOut(1500);
                }, 3000);
            } else {
                $("#MensajeErrorGeneral").show('fade');
                setTimeout(function () {
                    $("#MensajeErrorGeneral").fadeOut(1500);
                }, 3000);
            }
        }
    });
}

function ModalConfirmarLiberacion(modelo) {
    modelTemp = modelo;
    $("#ModalAfirmacionRegistro").modal("show");
}


function ModalConsultarPalletsIngresado(modelo) {
    ModalConsultarDetalleIngresosChatarra(modelo);
    $("#ModalDetalleOrdenVenta").modal("show");
}


function ModalConsultarDetalleIngresosChatarra(modelo) {
    $.ajax({
        url: "../PackingList/ConsultaOrdenVentaDetalle?DocEntry=" + modelo.DocEntry,
        type: "GET",
        success: function (msg) {
            $("#tblDetalleOrdenesVentas").dxDataGrid({
                dataSource: msg,
                columnAutoWidth: true,
                showBorders : true,
                keyExpr: "DocEntry",
                headerFilter: false,
                filterPanel: false,
                filterRow: false,
                selection: {
                    mode: "single"
                },
                columns: [
                    {
                        dataField: "DocEntry", visible: false
                    },
                    {
                        dataField: "WhsCode", caption: "Almacen"
                    },
                    {
                        dataField: "ItemCode", caption: "Codigo Item"
                    },
                    {
                        dataField: "Descripcion", caption: "Modelo Dacar"
                    },
                    {
                        dataField: "Text", caption: "Descripcion Cliente"
                    },
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
                ],
                summary: {
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
                        }]
                }
            });
        },
        error: function (msg) {
            $("#MensajeErrorGeneral").show('fade');
            setTimeout(function () {
                $("#MensajeErrorGeneral").fadeOut(1500);
            }, 3000);
        },

    });
}
