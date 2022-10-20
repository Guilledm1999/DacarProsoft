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
        url: "../Calidad/ObtenerReporteLiberadosLocales",
        type: "GET"
        , success: function (msg) {
            const locale = getLocale();
            DevExpress.localization.locale(locale);
            const pivotGridChart = $('#pivotgrid-chart').dxChart({
                commonSeriesSettings: {
                    type: 'bar',
                },
                tooltip: {
                    enabled: true,
                    customizeTooltip(args) {
                        const valueText = (args.seriesName.indexOf('Total') !== -1)
                            ? new Intl.NumberFormat('en-EN', { style: 'currency', currency: 'USD' }).format(args.originalValue)
                            : args.originalValue;

                        return {
                            html: `${args.seriesName}<div class='currency'>${valueText}</div>`,
                        };
                    },
                },
                size: {
                    height: 320,
                },
                adaptiveLayout: {
                    width: 450,
                },
            }).dxChart('instance');

            const pivotGrid = $("#tblLiberadosPacking").dxPivotGrid({
                allowSortingBySummary: true,
                allowFiltering: true,
                showBorders: true,
                showColumnGrandTotals: false,
                showRowGrandTotals: false,
                showRowTotals: false,
                showColumnTotals: false,
                fieldChooser: {
                    enabled: false,
                },
                //allowSortingBySummary: true,
                //allowSorting: true,
                //allowFiltering: true,
                //allowExpandAll: true,
                //showColumnTotals: false,
                //showTotalsPrior: 'rows',
                //showBorders: true,
                //fieldChooser: {
                //    enabled: false,
                //},
                dataSource: {
                    fields: [{
                        dataField: "PackingId", visible: false
                    },
                        {
                            dataField: "MedicionId", visible: false
                        },
                        {
                            dataField: "NumeroDocumento", caption: "# Secuencial Sap", visible: false
                        },                 
                        {
                            dataField: "DocEntry", caption: "Código", visible: false
                        },
                        {
                            dataField: "NumeroMedicion", caption: "Numero Medicion", visible: false
                        },
                        {
                            dataField: "Modelo", caption: "Modelo", area: 'row',
                            sortBySummaryField: 'Total', width: 85,
                        },
                        {
                            dataField: "NombreCliente", caption: "Nombre Cliente", area: 'row',
                        },
                        {
                            dataField: "NumeroOrden", caption: "Numero Orden", area: 'row',
                        },
                        {
                            dataField: "NumeroLote", caption: "Numero Lote", area: 'row',
                        },
                        {
                            caption: 'Mediciones',
                            summaryType: 'count',
                            area: 'data',
                        },
                        {
                            dataField: "Voltaje", caption: "Voltaje Promedio", 
                            customizeText: function (cellInfo) {
                                const noTruncarDecimales = { maximumFractionDigits: 2, minimumFractionDigits: 2 };
                                return (cellInfo.value).toLocaleString('en-US', noTruncarDecimales);
                            },
                            summaryType: 'avg',
                            area: 'data',
                        },
                        {
                            caption: 'CCA Promedio',
                            dataField: 'CCA',
                            customizeText: function (cellInfo) {
                                const noTruncarDecimales = { maximumFractionDigits: 2, minimumFractionDigits: 2 };
                                return (cellInfo.value).toLocaleString('en-US', noTruncarDecimales);
                            },
                            summaryType: 'avg',
                            area: 'data',
                        },
                        {
                            dataField: "FechaMedicion", caption: "Fecha Actualizacion", dataType: 'date', area: 'column',
                        },
                        {
                            dataField: "Limpieza", caption: "Limpieza", area: 'column', visible: false
                        },
                        {
                            dataField: "Acabado", caption: "Acabado", area: 'column', visible: false
                        },
                        {
                            dataField: "Nivel", caption: "Nivel", area: 'column', visible: false
                        },
                  ],
                    store: msg,
                },
            }).dxPivotGrid('instance');
            pivotGrid.bindChart(pivotGridChart, {
                dataFieldsDisplayMode: 'splitPanes',
                alternateDataFields: false,
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
function mostrarIngresosPalletBack() {
    $.ajax({
        url: "../Calidad/ObtenerReporteLiberadosLocales",
        type: "GET"
        , success: function (msg) {
            const locale = getLocale();
            DevExpress.localization.locale(locale);

            $("#tblLiberadosPacking").dxDataGrid({
                dataSource: msg,
                keyExpr: 'MedicionId',
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
                        dataField: "PackingId", visible: false
                    },
                    {
                        dataField: "MedicionId", visible: false
                    },
                    {
                        dataField: "NumeroDocumento", caption: "# Secuencial Sap", allowEditing: false, allowHeaderFiltering: false, visible: false
                    },
                    {
                        dataField: "NumeroOrden", caption: "Numero Orden", allowEditing: false, headerFilter: {
                            allowSearch: true,
                        }, allowHeaderFiltering: true
                    },
                    {
                        dataField: "DocEntry", caption: "Código", allowEditing: false, allowHeaderFiltering: true, alignment: "left", headerFilter: {
                            allowSearch: true,
                        },
                    },
                    {
                        dataField: "NombreCliente", caption: "Nombre Cliente", allowEditing: false, allowHeaderFiltering: true, headerFilter: {
                            allowSearch: true,
                        },
                    },

                    {
                        dataField: "NumeroMedicion", caption: "Numero Medicion", allowEditing: false, headerFilter: false, allowHeaderFiltering: false
                    },
                    {
                        dataField: "NumeroLote", caption: "Numero Lote", allowEditing: false, headerFilter: false, allowHeaderFiltering: false
                    },
                    {
                        dataField: "Modelo", caption: "Modelo", allowEditing: false, headerFilter: {
                            allowSearch: true,
                        },
                    },
                    {
                        dataField: "Limpieza", caption: "Limpieza", allowEditing: false
                    },
                    {
                        dataField: "Acabado", caption: "Acabado", allowEditing: false
                    },
                    {
                        dataField: "Nivel", caption: "Nivel", allowEditing: false
                    },
                    {
                        dataField: "Voltaje", caption: "Voltaje", allowHeaderFiltering: false, alignment: "center", width: 130,
                        format: {
                            type: "fixedPoint",
                            precision: 2,
                        },
                        customizeText: function (cellInfo) {
                            const noTruncarDecimales = { maximumFractionDigits: 2, minimumFractionDigits: 2 };

                            return (cellInfo.value).toLocaleString('en-US', noTruncarDecimales);
                        },
                    },
                    {
                        dataField: "CCA", caption: "CCA", allowEditing: false, alignment: "left",
                    },
                    {
                        dataField: "FechaMedicion", caption: "Fecha Actualizacion", allowEditing: false, allowHeaderFiltering: true, dataType: 'date',
                    },
                    //{
                    //    caption: "Acciones", type: "buttons",
                    //    buttons: [{
                    //        text: "Imprimir",
                    //        icon: "print",
                    //        hint: "Imprimir",
                    //        onClick: function (e) {
                    //            generarPDFLiberacion(e.row.data);
                    //        }
                    //    },
                    //    {
                    //        text: "Detalle",
                    //        icon: "menu",
                    //        hint: "Detalle",
                    //        onClick: function (e) {
                    //            // Execute your command here
                    //            ModalConsultarMedicionesPallets(e.row.data);
                    //        }
                    //    }]
                    //}

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
            const locale = getLocale();
            DevExpress.localization.locale(locale);
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
                        dataField: "Voltaje", caption: "Voltaje", allowHeaderFiltering: false, alignment: "center", width: 130,
                        format: {
                            type: "fixedPoint",
                            precision: 2,
                        },
                        customizeText: function (cellInfo) {
                            const noTruncarDecimales = { maximumFractionDigits: 2, minimumFractionDigits: 2 };

                            return (cellInfo.value).toLocaleString('en-US', noTruncarDecimales);
                        },
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
    function getLocale() {
        const storageLocale = sessionStorage.getItem('locale');
        return storageLocale != null ? storageLocale : 'es';
    }
}
