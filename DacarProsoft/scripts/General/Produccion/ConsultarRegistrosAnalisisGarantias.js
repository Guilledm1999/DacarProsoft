$(document).ready(function () {
    ConsultaRegistrosAnalisisGarantias();
});

$('#LinkClose').on("click", function (e) {
    $("#MensajeErrorInesperado").hide('fade');
});
$('#LinkClose2').on("click", function (e) {
    $("#MensajeIngreseTodosLosCampos").hide('fade');
});
$('#LinkClose3').on("click", function (e) {
    $("#MensajeGuardadoExitoso").hide('fade');
});

function ConsultaRegistrosAnalisisGarantias() {
    $.ajax({
        url: "../Produccion/ConsultarAnalisisGarantiaRegistrados",
        type: "GET"
        , success: function (msg) {

            $("#tblAnalisisRegistrados").dxDataGrid({
                dataSource: msg,
                keyExpr: 'AnalisisRegistrosGarantiasId',
                showBorders: true,
                columnAutoWidth: true,
                showBorders: true,
                showColumnLines: true,
                rowAlternationEnabled: false,
                showRowLines: true,
                //paging: {
                //    pageSize: 10
                //},

                //selection: {
                //    mode: 'multiple'
                //},
                searchPanel: {
                    visible: true,
                    width: 240,
                    placeholder: "Buscar..."
                },
                headerFilter: {
                    visible: true
                },
                customizeColumns(columns) {
                    columns[0].width = 70;
                },
                loadPanel: {
                    enabled: false,
                },
                scrolling: {
                    mode: 'infinite',
                },
                sorting: {
                    mode: 'none',
                },
                //pager: {
                //    visible: true,
                //    allowedPageSizes: [5, 10, 50],
                //    showPageSizeSelector: true,
                //    showInfo: true,
                //    showNavigationButtons: true
                //
                //},
                export: {
                    enabled: true,
                    allowExportSelectedData: false
                },

                onExporting: function (e) {
                    var workbook = new ExcelJS.Workbook();
                    var worksheet = workbook.addWorksheet('Ingresos Analisis');

                    DevExpress.excelExporter.exportDataGrid({
                        component: e.component,
                        worksheet: worksheet,
                        autoFilterEnabled: true
                    }).then(function () {
                        workbook.xlsx.writeBuffer().then(function (buffer) {
                            saveAs(new Blob([buffer], { type: 'application/octet-stream' }), 'IngresosAnalisisGarantias.xlsx');
                        });
                    });
                    e.cancel = true;
                },
                columns: [

                    { dataField: "AnalisisRegistrosGarantiasId", visible: false },
                    {
                        dataField: "IngresoRevisionGarantiaId", visible: false
                    },
                    {
                        dataField: "FechaRegistroAnalisis", caption: "Fecha Analisis", alignment: "left", dataType: "date", allowHeaderFiltering: true, allowSearch: false
                    },
                    {
                        dataField: "NumeroComprobante", caption: "# Comprobante", alignment: "left", allowHeaderFiltering: false, allowSearch: true
                    },
                    {
                        dataField: "LoteCarga", caption: "Lote Carga", alignment: "left", allowHeaderFiltering: false, allowSearch: false, visible: false
                    },
                    {
                        dataField: "LoteEnsamble", caption: "Lote Ensamble", alignment: "left", allowHeaderFiltering: false, allowSearch: false, visible: false
                    },
                    {
                        dataField: "ModeloBateria", caption: "Modelo", alignment: "left", allowHeaderFiltering: true, allowSearch: false
                    },
                    {
                        dataField: "Voltaje", caption: "Voltaje", alignment: "left", allowHeaderFiltering: false, allowSearch: false
                    },
                    {
                        dataField: "CCA", caption: "CCA", alignment: "left", visible: false, allowHeaderFiltering: false, allowSearch: false
                    },
                    {
                        dataField: "DencidadCelda1", caption: "D. Celda 1", alignment: "left", visible: false, allowHeaderFiltering: false, allowSearch: false
                    },

                    {
                        dataField: "DencidadCelda2", caption: "D. Celda 2", alignment: "left", visible: false, allowHeaderFiltering: false, allowSearch: false
                    },
                    {
                        dataField: "DencidadCelda3", caption: "D. Celda 3", alignment: "left", visible: false, allowHeaderFiltering: false, allowSearch: false
                    },
                    {
                        dataField: "DencidadCelda4", caption: "D. Celda 4", alignment: "left", visible: false, allowHeaderFiltering: false, allowSearch: false
                    },
                    {
                        dataField: "DencidadCelda5", caption: "D. Celda 5", alignment: "left", visible: false, allowHeaderFiltering: false, allowSearch: false
                    },
                    {
                        dataField: "DencidadCelda6", caption: "D. Celda 6", alignment: "left", visible: false, allowHeaderFiltering: false, allowSearch: false
                    },
                    {
                        dataField: "ResumenAnalisis", caption: "Analisis", alignment: "left", allowHeaderFiltering: true, allowSearch: false
                    },
                    {
                        dataField: "AreaResponsable", caption: "Area Responsable", alignment: "left", allowHeaderFiltering: true, allowSearch: false
                    },
                    {
                        dataField: "Observaciones", caption: "Observaciones", alignment: "left", allowHeaderFiltering: false, allowSearch: false
                    },
                   
                    //{
                    //    caption: "Acciones",
                    //    cellTemplate: function (container, options) {
                    //        var btnDiagnostico = "<button class='btn-primary' onclick='ModalObtenerDiagnostico(" + JSON.stringify(options.data) + ")'>Diagnostico</button>";


                    //        $("<div>")
                    //            .append($(btnDiagnostico))
                    //            .appendTo(container);
                    //    }
                    //}
                ],
            });


        },
        error: function (msg) {

            $("#MensajeErrorInesperado").show('fade');
            setTimeout(function () {
                $("#MensajeErrorInesperado").fadeOut(1500);
            }, 3000); return;
        }
    })


}