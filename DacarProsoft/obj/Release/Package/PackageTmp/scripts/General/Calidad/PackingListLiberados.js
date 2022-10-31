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
        url: "../Calidad/ObtenerPackingLiberados",
        type: "GET"
        , success: function (msg) {
            const locale = getLocale();
            DevExpress.localization.locale(locale);

            $("#tblLiberadosPacking").dxDataGrid({
                dataSource: msg,
                keyExpr: 'PackingId',
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
                    { dataField: "PackingId", visible: false },
                    {
                        dataField: "NumeroDocumento", caption: "# Secuencial Sap", allowEditing: false, allowHeaderFiltering: false
                    },
                    {
                        dataField: "NumeroOrden", caption: "Numero Orden", allowEditing: false, headerFilter: false, allowHeaderFiltering: false
                    },
                    {
                        dataField: "NombreCliente", caption: "Cliente", allowEditing: false, headerFilter: true, allowHeaderFiltering: true
                    },
                    {
                        dataField: "Destino", caption: "Destino", allowEditing: false, headerFilter: true, allowHeaderFiltering: true
                    },
                    {
                        dataField: "cantidadMediciones", caption: "Cantidad Mediciones", allowEditing: false, headerFilter: false, allowHeaderFiltering: false
                    },
                    {
                        dataField: "CantidadPallet", caption: "Cantidad Pallet", allowEditing: false, headerFilter: false, allowHeaderFiltering: false
                    },
                    {
                        dataField: "Estado", caption: "Estado Packing List", allowEditing: false
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

function generarPDFLiberacion(modelo) {
    var url = "../Calidad/ImprimirLiberacionProducto?packingId=" + modelo.PackingId;
    window.open(url);
    //$("#ModalListadoDePallets").modal("hide");
}

function ModalConsultarPalletsIngresado(modelo) {
    ConsultarPalletsIngresado(modelo);
    $("#ModalListadoDePallets").modal("show");
}
function ConsultarPalletsIngresado(modelo) {
    $.ajax({
        url: "../Calidad/ObtenerPalletList?PackingId=" + modelo.PackingId,
        type: "GET"
        , success: function (msg) {
            $("#tblListadoPalletsIngresados").dxDataGrid({
                dataSource: msg,
                keyExpr: "PalletPacking1",
                allowColumnReordering: false,
                allowColumnResizing: true,
                columnAutoWidth: true,
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
                        dataField: "PalletPacking1", visible: false
                    },
                    {
                        dataField: "PackingId", visible: false
                    },
                    {
                        dataField: "PalletNumber", caption: "Número Pallet", allowEditing: false, allowHeaderFiltering: false
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
                        dataField: "Cantidad", caption: "Cantidad Items", allowEditing: false, allowHeaderFiltering: false
                    },
                    {
                        dataField: "CantidadMediciones", caption: "Cantidad Mediciones", allowEditing: false, allowHeaderFiltering: false
                    },
                    {
                        caption: "Mediciones", type: "buttons", width: 100,
                        buttons: [{
                            text: "Detalle",
                            icon: "menu",
                            hint: "Detalle",
                            onClick: function (e) {
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
}
function ModalConsultarMedicionesPallets(modelo) {
    ConsultarMedicionPallet(modelo);
    console.log("Pallet # " + modelo.PalletNumber);
    $("#lblPalletDetalle").text("Pallet # " + modelo.PalletNumber);
    //$("#ModalListadoDePallets").modal("hide");
    $("#ModalAgregarMedicionPallets").modal("show");
}

function ConsultarMedicionPallet(modelo) {
    packingId = modelo.PackingId;
    palletId = modelo.PalletPacking1;
    $.ajax({
        url: "../Calidad/ObtenerPalletListMediciones?PackinkId=" + modelo.PackingId + "&PalletId=" + modelo.PalletPacking1,
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
                onRowUpdating: function (options) {
                    this.oldData = Object.assign({}, options.oldData);
                    ActualizarMedicion(options.newData, options.key);

                },
                onRowInserting: function (options) {
                    InsertarMedicion((options.data));
                }
                ,
                onRowRemoving: function (options) {
                    EliminarMedicion(options.data);
                }
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
