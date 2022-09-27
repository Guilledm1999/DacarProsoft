var packingId = null;
var palletId = null;
var resTempo = null;
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
        url: "../Calidad/ObtenerPackingLocalesLiberados",
        type: "GET"
        , success: function (msg) {
            const locale = getLocale();
            DevExpress.localization.locale(locale);

            $("#tblLiberadosPacking").dxDataGrid({
                dataSource: msg,
                keyExpr: 'EncabezadoPedidoLocal',
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
                    { dataField: "EncabezadoPedidoLocal", visible: false },
                    {
                        dataField: "NumeroDocumento", caption: "# Secuencial Sap", allowEditing: false, allowHeaderFiltering: false
                    },
                    {
                        dataField: "NumeroOrden", caption: "Numero Orden", allowEditing: false, headerFilter: false, allowHeaderFiltering: false
                    },
                    {
                        dataField: "PackingId", caption: "Código", allowEditing: false, headerFilter: false, allowHeaderFiltering: false
                    },
                    {
                        dataField: "NombreCliente", caption: "Cliente", allowEditing: false, headerFilter: true, allowHeaderFiltering: true
                    },
                    {
                        dataField: "Destino", caption: "Destino", allowEditing: false, headerFilter: true, allowHeaderFiltering: true, visible: false
                    },
                    {
                        dataField: "cantidadMediciones", caption: "Cantidad Mediciones", allowEditing: false, headerFilter: false, allowHeaderFiltering: false
                    },
                    {
                        dataField: "CantidadPallet", caption: "Cantidad Pallet", allowEditing: false, headerFilter: false, allowHeaderFiltering: false
                    },
                    {
                        dataField: "Estado", caption: "Estado Packing List", allowEditing: false, visible: false
                    },
                    {
                        dataField: "FechaRegistro", caption: "Fecha Actualizacion", allowEditing: false, allowHeaderFiltering: true, dataType: 'date',
                    },
                    {
                        caption: "Acciones", type: "buttons",
                        buttons: [{
                            text: "Imprimir",
                            icon: "print",
                            hint: "Imprimir",
                            onClick: function (e) {
                                generarPDFLiberacion(e.row.data);
                            }
                        },
                        {
                            text: "Detalle",
                            icon: "menu",
                            hint: "Detalle",
                            onClick: function (e) {
                                // Execute your command here
                                ModalConsultarMedicionesPallets(e.row.data);
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

function generarPDFLiberacion(modelo) {
    var url = "../Calidad/ImprimirLiberacionProductoLocal?identificador=" + modelo.EncabezadoPedidoLocal;
    window.open(url);
    //$("#ModalListadoDePallets").modal("hide");
}

function ModalConsultarMedicionesPallets(modelo) {
    ConsultarMedicionPallet(modelo);
    //$("#lblPalletDetalle").text("Pallet # " + modelo.PalletNumber);
    //$("#ModalListadoDePallets").modal("hide");
    $("#ModalAgregarMedicionPallets").modal("show");
}

function ConsultarMedicionPallet(modelo) {
    packingId = modelo.PackingId;
    palletId = modelo.PalletPacking1;
    $.ajax({
        url: "../Calidad/ObtenerPalletListMedicionesLocales?identificador=" + modelo.EncabezadoPedidoLocal,
        type: "GET"
        , success: function (msg) {
            $("#tblListadoMedicionesPallets").dxDataGrid({
                dataSource: msg,
                keyExpr: "MedicionPalletPackingId",
                allowColumnReordering: false,
                allowColumnResizing: true,
                columnAutoWidth: false,
                showBorders: true,

                paging: {
                    pageSize: 10
                },
                pager: {
                    visible: true,
                    allowedPageSizes: [5, 10, 100],
                    showPageSizeSelector: true,
                    showNavigationButtons: true
                },
               
                columns: [
                    {
                        dataField: "MedicionPalletPackingId", visible: false
                    },
                    {
                        dataField: "PackingId", visible: false
                    },
                    {
                        dataField: "PalletId", visible: false, width: 70
                    },
                 
                    {
                        dataField: "Modelo", caption: "Modelo",allowHeaderFiltering: false, width: 200, alignment: "center"                      
                    },
                    {
                        dataField: "NumeroLote", caption: "# Lote", allowHeaderFiltering: false, width: 150, alignment: "center"
                    },
                    {
                        dataField: "Voltaje", caption: "Voltaje",allowHeaderFiltering: false, dataType: "number", alignment: "center", width: 130
                    },
                    {
                        dataField: "nivel", caption: "nivel",  allowHeaderFiltering: false, dataType: "boolean", width: 130
                    },
                    {
                        dataField: "Acabado", caption: "Acabado", allowHeaderFiltering: false, dataType: "boolean", width: 130
                    },
                    {
                        dataField: "Limpieza", caption: "Limpieza",  allowHeaderFiltering: false, dataType: "boolean", width: 130
                    },
                    {
                        dataField: "CCA", caption: "CCA",  allowHeaderFiltering: false,  dataType: "number", alignment: "center", width: 130
                    },
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
}
