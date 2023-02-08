

$(document).ready(function () {
    ConsultarArticulos();
});
function ConsultarArticulos() {
    $.ajax({
        url: "../Articulos/ConsultaBateriasAlmacenes",
        type: "GET"
        , success: function (msg) {

           
            $("#tblTiposArticulos").dxDataGrid({
                dataSource: msg,
                keyExpr: 'Modelo',
                columnAutoWidth: true,
                showRowLines: false,
                allowColumnReordering: true,
                showBorders: true,
                height: 700,
                allowColumnResizing: true,
                sorting: {
                    mode: 'none',
                },
                wordWrapEnabled: true,
                scrolling: {
                    mode: 'virtual',
                },
                columnChooser: {
                    enabled: true,
                },
                export: {
                    enabled: true,
                   
                },
                onToolbarPreparing(e) {
                    let dataGrid = e.component;
                    e.toolbarOptions.items.unshift({
                        location: "after",
                        widget: "dxButton",
                        options: {
                            icon: "refresh",
                            onClick: function () {
                                dataGrid.refresh();
                            }
                        }
                    })
                },
                //al clickear 
                selection: {
                    mode: 'single',
                },
                // No resetear el grid
                stateStoring: {
                    enabled: true,
                    type: 'localStorage',
                    storageKey: 'storage',
                },


                headerFilter: { visible: true, allowSearch: true },
                onCellPrepared(options) {
                    const fieldData = options.value;
                   // let fieldHtml = '';
                    if (fieldData) {
                        if (options.rowType === "data") {

                            if (fieldData > 0 && options.column.caption != "Modelo" && options.column.caption != "Referencia") {
                                options.cellElement.css("font-weight", "bolder");
                                options.cellElement.css("font-size", "14px");
                            }
                            else {
                                fieldHtml = fieldData.value;
                            }

                            
                            options.cellElement.html(fieldHtml);
                        }}
                        

                    if (options.rowType === "data") {
                        if (options.column.caption === "TOTAL") { //condition where the column requires the coloring
                            options.cellElement.css("background-color", "#D7F1FF"); //set the background color based on the data
                        }
                    }
                },
                onRowPrepared: function (e) {
                    if (e.key == "N40-57 I BG" || e.key == "U1-32" || e.key == "88-100" || e.key == "42-65 I" || e.key == "55-65 I" || e.key == "66-75 I" || e.key == "48-77 I" || e.key == "25-70 I" || e.key == "24-MDP-1000"
                        || e.key == "34-85 I" || e.key == "27-MDP-1125" || e.key == "65-100 I" || e.key == "94-90" || e.key == "31-MDP-1250" || e.key == "36-50" || e.key == "NS40-50 I BF"   ) {
                        // e.rowElement.css("background-color", "#F1FBFF");
                        e.rowElement.css("border-bottom-color", "#e3e5e6");
                        e.rowElement.css("border-bottom", "thick double #e3e5e6");

                    }
                },
                onExporting(e) {
                    const workbook = new ExcelJS.Workbook();
                    const worksheet = workbook.addWorksheet('Inventario de Baterias de Almacenes');
                   
                    DevExpress.excelExporter.exportDataGrid({
                        component: e.component,
                        worksheet,
                        autoFilterEnabled: true,
                        customizeCell(options) {
                            const { gridCell } = options;
                            const { excelCell } = options;

                            if (gridCell.rowType === 'data') {
                                if (gridCell.column.caption == 'TOTAL') {
                                    excelCell.fill = { type: 'pattern', pattern: 'solid', fgColor: { argb: 'D7F1FF' } };
                                }
                                if (gridCell.rowType == "header") {
                                    excelCell.fill = { type: 'pattern', pattern: 'solid', fgColor: { argb: 'D7F1FF' } };
                                }
                                if (gridCell.rowType === 'totalFooter' && excelCell.value) {
                                    excelCell.font.italic = true;
                                }
                            }
                        },

                    }).then(() => {
                        workbook.xlsx.writeBuffer().then((buffer) => {
                            saveAs(new Blob([buffer], { type: 'application/octet-stream' }), 'Inventario_Baterias_Almacenes.xlsx');
                        });
                    });
                    e.cancel = true;
                },
                columns: [
                    {
                        caption: 'Línea',
                        dataField: 'LineaReferencia',
                        groupIndex: 0,
                       
                    },

                    {
                        caption: 'Referencia',
                        dataField: 'Referencia',
                        allowHeaderFiltering: true,
                        alignment: "center",
                        //width: 125,
                    },
                    {

                        dataField: 'Modelo',
                        caption: 'Modelo',
                        allowHeaderFiltering: true,
                        alignment: "center",
                      //  width: 110,

                    },

                    {
                        caption: 'Plaza Dañin',
                        columns: [{
                            caption: 'ST',
                            dataField: 'DacarStDanin',
                            format: 'fixedPoint',
                            alignment: "center",
                            allowHeaderFiltering: false,
                            width: '4%',
                            calculateDisplayValue: function (rowData) {
                                if (rowData["DacarStDanin"] == 0) {
                                    return "";
                                }
                                return rowData["DacarStDanin"];
                            }
                

                        }, {
                            caption: 'BP',
                            dataField: 'DacarBpDanin',
                            format: 'fixedPoint',
                            alignment: "center",
                            allowHeaderFiltering: false,
                            width: '4%',
                            calculateDisplayValue: function (rowData) {
                                if (rowData["DacarBpDanin"] == 0) {
                                    return "";
                                }
                                return rowData["DacarBpDanin"];
                            }
                            },
                            {
                                caption: 'TX-ECO',
                                dataField: 'DacarTxDanin',
                                format: 'fixedPoint',
                                alignment: "center",
                                allowHeaderFiltering: false,
                                width: '4%',
                                calculateDisplayValue: function (rowData) {
                                    if (rowData["DacarTxDanin"] == 0) {
                                        return "";
                                    }
                                    return rowData["DacarTxDanin"];
                                }
                            },
                            {
                                caption: 'TOTAL',
                                dataField: 'TotalDanin',
                                format: 'fixedPoint',
                                alignment: "center",
                                allowHeaderFiltering: false,
                                width: '4%',
                                calculateDisplayValue: function (rowData) {
                                    if (rowData["TotalDanin"] == 0) {
                                        return "";
                                    }
                                    return rowData["TotalDanin"];
                                }

                            },
                        ],
                    }, {
                        caption: 'Juan Tanca Marengo',
                        columns: [{
                            caption: 'ST',
                            dataField: 'DacarStTanca',
                            format: 'fixedPoint',
                            alignment: "center",
                            allowHeaderFiltering: false
                            , width: '4%',
                            calculateDisplayValue: function (rowData) {
                                if (rowData["DacarStTanca"] == 0) {
                                    return "";
                                }
                                return rowData["DacarStTanca"];
                            }
                        }, {
                            caption: 'BP',
                            dataField: 'DacarBpTanca',
                            format: 'fixedPoint',
                            alignment: "center",
                            allowHeaderFiltering: false,
                            width: '4%',
                            calculateDisplayValue: function (rowData) {
                                if (rowData["DacarBpTanca"] == 0) {
                                    return "";
                                }
                                return rowData["DacarBpTanca"];
                            }
                        },
                        {
                            caption: 'TX-ECO',
                            dataField: 'DacarTxTanca',
                            format: 'fixedPoint',
                            alignment: "center",
                            allowHeaderFiltering: false,
                            width: '4%',

                            calculateDisplayValue: function (rowData) {
                                if (rowData["DacarTxTanca"] == 0) {
                                    return "";
                                }
                                return rowData["DacarTxTanca"];
                            }
                        },  
                        {
                            caption: 'TOTAL',
                            dataField: 'TotalTanca',
                            format: 'fixedPoint',
                            alignment: "center",
                            allowHeaderFiltering: false,
                            width: '4%',
                            calculateDisplayValue: function (rowData) {
                                if (rowData["TotalTanca"] == 0) {
                                    return "";
                                }
                                return rowData["TotalTanca"];
                            }
                        },
                        ],
                    },
                    {
                        caption: 'Gomez Rendon',
                        columns: [{
                            caption: 'ST',
                            dataField: 'DacarStRendon',
                            format: 'fixedPoint',
                            alignment: "center",
                            allowHeaderFiltering: false,
                            width: '4%',
                            calculateDisplayValue: function (rowData) {
                                if (rowData["DacarStRendon"] == 0) {
                                    return "";
                                }
                                return rowData["DacarStRendon"];
                            }
                        }, {
                            caption: 'BP',
                            dataField: 'DacarBpRendon',
                            format: 'fixedPoint',
                            alignment: "center",
                            allowHeaderFiltering: false,
                            width: '4%',
                            calculateDisplayValue: function (rowData) {
                                if (rowData["DacarBpRendon"] == 0) {
                                    return "";
                                }
                                return rowData["DacarBpRendon"];
                            }
                        },
                        {
                            caption: 'TX-ECO',
                            dataField: 'DacarTxRendon',
                            format: 'fixedPoint',
                            alignment: "center",
                            allowHeaderFiltering: false,
                            width: '4%',
                            calculateDisplayValue: function (rowData) {
                                if (rowData["DacarTxRendon"] == 0) {
                                    return "";
                                }
                                return rowData["DacarTxRendon"];
                            }
                        },
                        {
                            caption: 'TOTAL',
                            dataField: 'TotalRendon',
                            format: 'fixedPoint',
                            alignment: "center",
                            allowHeaderFiltering: false,
                            width: '4%',
                            calculateDisplayValue: function (rowData) {
                                if (rowData["TotalRendon"] == 0) {
                                    return "";
                                }
                                return rowData["TotalRendon"];
                            }
                        },
                        ],
                    },
                    {
                        caption: 'Piazza Samborondon',
                        columns: [{
                            caption: 'ST',
                            dataField: 'DacarStSambo',
                            format: 'fixedPoint',
                            alignment: "center",
                            allowHeaderFiltering: false,
                            width: '4%',
                            calculateDisplayValue: function (rowData) {
                                if (rowData["DacarStSambo"] == 0) {
                                    return "";
                                }
                                return rowData["DacarStSambo"];
                            }
                        }, {
                            caption: 'BP',
                            dataField: 'DacarBpSambo',
                            format: 'fixedPoint',
                            alignment: "center",
                            allowHeaderFiltering: false,
                            width: '4%',
                            calculateDisplayValue: function (rowData) {
                                if (rowData["DacarBpSambo"] == 0) {
                                    return "";
                                }
                                return rowData["DacarBpSambo"];
                            }
                        },
                        {
                            caption: 'TX-ECO',
                            dataField: 'DacarTxSambo',
                            format: 'fixedPoint',
                            alignment: "center",
                            allowHeaderFiltering: false,
                            width: '4%',
                            calculateDisplayValue: function (rowData) {
                                if (rowData["DacarTxSambo"] == 0) {
                                    return "";
                                }
                                return rowData["DacarTxSambo"];
                            }
                        },
                        {
                            caption: 'TOTAL',
                            dataField: 'TotalSambo',
                            format: 'fixedPoint',
                            alignment: "center",
                            allowHeaderFiltering: false,
                            width: '4%',
                            calculateDisplayValue: function (rowData) {
                                if (rowData["TotalSambo"] == 0) {
                                    return "";
                                }
                                return rowData["TotalSambo"];
                            }

                        },
                        ],
                    },
                    {
                        caption: 'Quito',
                        columns: [{
                            caption: 'ST',
                            dataField: 'DacarStQuito',
                            format: 'fixedPoint',
                            alignment: "center",
                            allowHeaderFiltering: false,
                            width: '4%',
                            calculateDisplayValue: function (rowData) {
                                if (rowData["DacarStQuito"] == 0) {
                                    return "";
                                }
                                return rowData["DacarStQuito"];
                            }
                        }, {
                            caption: 'BP',
                            dataField: 'DacarBpQuito',
                            format: 'fixedPoint',
                            alignment: "center",
                            allowHeaderFiltering: false,
                            width: '4%',
                            calculateDisplayValue: function (rowData) {
                                if (rowData["DacarBpQuito"] == 0) {
                                    return "";
                                }
                                return rowData["DacarBpQuito"];
                            }
                        },
                        {
                            caption: 'TX-ECO',
                            dataField: 'DacarTxQuito',
                            format: 'fixedPoint',
                            alignment: "center",
                            allowHeaderFiltering: false,
                            width: '4%',
                            calculateDisplayValue: function (rowData) {
                                if (rowData["DacarTxQuito"] == 0) {
                                    return "";
                                }
                                return rowData["DacarTxQuito"];
                            }
                        },
                        {
                            caption: 'TOTAL',
                            dataField: 'TotalQuito',
                            format: 'fixedPoint',
                            alignment: "center",
                            allowHeaderFiltering: false,
                            width: '4%',
                            calculateDisplayValue: function (rowData) {
                                if (rowData["TotalQuito"] == 0) {
                                    return "";
                                }
                                return rowData["TotalQuito"];
                            }
                        },
                        ],
                       
                        
                    },

                ], sortByGroupSummaryInfo: [{ summaryItem: "count", sortOrder: "desc" }],

                summary: {
                    groupItems: [{
                        column: 'TOTAL',
                        summaryType: 'sum',

                        displayFormat: '{0}',
                        showInGroupFooter: true,
                    },
                        {
                            column: 'DacarStDanin',
                            summaryType: 'sum',
                            displayFormat: "{0}",
                            showInGroupFooter: true,
                            valueFormat: {
                                type: ",##0.###",

                            }
                        },
                        {
                            column: 'DacarBpDanin',
                            summaryType: 'sum',
                            displayFormat: "{0}",
                            showInGroupFooter: true,
                            valueFormat: {
                                type: ",##0.###",

                            }
                        },
                        {
                            column: 'DacarTxDanin',
                            summaryType: 'sum',
                            displayFormat: "{0}",
                            showInGroupFooter: true,
                            valueFormat: {
                                type: ",##0.###",

                            }
                        },
                        {
                            column: 'DacarStTanca',
                            summaryType: 'sum',
                            displayFormat: "{0}",
                            showInGroupFooter: true,
                            valueFormat: {
                                type: ",##0.###",

                            }
                        },
                        {
                            column: 'DacarBpTanca',
                            summaryType: 'sum',
                            displayFormat: "{0}",
                            showInGroupFooter: true,
                            valueFormat: {
                                type: ",##0.###",

                            }
                        },
                        {
                            column: 'DacarTxTanca',
                            summaryType: 'sum',
                            displayFormat: "{0}",
                            showInGroupFooter: true,
                            valueFormat: {
                                type: ",##0.###",

                            }
                        },
                        {
                            column: 'TotalTanca',
                            summaryType: 'sum',
                            displayFormat: "{0}",
                            showInGroupFooter: true,
                            valueFormat: {
                                type: ",##0.###",

                            }
                        },
                        {
                            column: 'DacarStSambo',
                            summaryType: 'sum',
                            displayFormat: "{0}",
                            showInGroupFooter: true,
                            valueFormat: {
                                type: ",##0.###",

                            }
                        },
                        {
                            column: 'DacarBpSambo',
                            summaryType: 'sum',
                            displayFormat: "{0}",
                            showInGroupFooter: true,
                            valueFormat: {
                                type: ",##0.###",

                            }
                        },
                        {
                            column: 'DacarTxSambo',
                            summaryType: 'sum',
                            displayFormat: "{0}",
                            showInGroupFooter: true,
                            valueFormat: {
                                type: ",##0.###",

                            }
                        },
                        {
                            column: 'TotalSambo',
                            summaryType: 'sum',
                            displayFormat: "{0}",
                            showInGroupFooter: true,
                            valueFormat: {
                                type: ",##0.###",

                            }
                        },
                        {
                            column: 'DacarStQuito',
                            summaryType: 'sum',
                            displayFormat: "{0}",
                            showInGroupFooter: true,
                            valueFormat: {
                                type: ",##0.###",

                            }
                        },
                        {
                            column: 'DacarBpQuito',
                            summaryType: 'sum',
                            displayFormat: "{0}",
                            showInGroupFooter: true,
                            valueFormat: {
                                type: ",##0.###",

                            }
                        },
                        {
                            column: 'DacarTxQuito',
                            summaryType: 'sum',
                            displayFormat: "{0}",
                            showInGroupFooter: true,
                            valueFormat: {
                                type: ",##0.###",

                            }
                        },
                        {
                            column: 'TotalQuito',
                            summaryType: 'sum',
                            displayFormat: "{0}",
                            showInGroupFooter: true,
                            valueFormat: {
                                type: ",##0.###",

                            }
                        },
                        {
                            column: 'DacarStRendon',
                            summaryType: 'sum',
                            displayFormat: "{0}",
                            showInGroupFooter: true,
                            valueFormat: {
                                type: ",##0.###",

                            }
                        },
                        {
                            column: 'DacarBpRendon',
                            summaryType: 'sum',
                            displayFormat: "{0}",
                            showInGroupFooter: true,
                            valueFormat: {
                                type: ",##0.###",

                            }
                        },
                        {
                            column: 'DacarTxRendon',
                            summaryType: 'sum',
                            displayFormat: "{0}",
                            showInGroupFooter: true,
                              valueFormat: {
                                type: ",##0.###",

                            }
                        },
                        {
                            column: 'TotalRendon',
                            summaryType: 'sum',
                            displayFormat: "{0}",
                            showInGroupFooter: true,
                            valueFormat: {
                                type: ",##0.###",

                            }
                        },
                        //Arriba
                        {
                            column: 'TOTAL',
                            summaryType: 'sum',

                            displayFormat: '{0}',
                            alignByColumn: true,
                            valueFormat: {
                                type: ",##0.###",

                            }
                        },
                        {
                            column: 'DacarStDanin',
                            summaryType: 'sum',
                            displayFormat: "{0}",
                            alignByColumn: true,
                            valueFormat: {
                                type: ",##0.###",

                            }
                        },
                        {
                            column: 'DacarBpDanin',
                            summaryType: 'sum',
                            displayFormat: "{0}",
                            alignByColumn: true,
                            valueFormat: {
                                type: ",##0.###",

                            }
                        },
                        {
                            column: 'DacarTxDanin',
                            summaryType: 'sum',
                            displayFormat: "{0}",
                            alignByColumn: true,
                            valueFormat: {
                                type: ",##0.###",

                            }
                        },
                        {
                            column: 'DacarStTanca',
                            summaryType: 'sum',
                            displayFormat: "{0}",
                            alignByColumn: true,
                            valueFormat: {
                                type: ",##0.###",

                            }
                        },
                        {
                            column: 'DacarBpTanca',
                            summaryType: 'sum',
                            displayFormat: "{0}",
                            alignByColumn: true,
                            valueFormat: {
                                type: ",##0.###",

                            }
                        },
                        {
                            column: 'DacarTxTanca',
                            summaryType: 'sum',
                            displayFormat: "{0}",
                            alignByColumn: true,
                            valueFormat: {
                                type: ",##0.###",

                            }
                        },
                        {
                            column: 'TotalTanca',
                            summaryType: 'sum',
                            displayFormat: "{0}",
                            alignByColumn: true,
                            valueFormat: {
                                type: ",##0.###",

                            }
                        },
                        {
                            column: 'DacarStSambo',
                            summaryType: 'sum',
                            displayFormat: "{0}",
                            alignByColumn: true,
                            valueFormat: {
                                type: ",##0.###",

                            }
                        },
                        {
                            column: 'DacarBpSambo',
                            summaryType: 'sum',
                            displayFormat: "{0}",
                            alignByColumn: true,
                            valueFormat: {
                                type: ",##0.###",

                            }
                        },
                        {
                            column: 'DacarTxSambo',
                            summaryType: 'sum',
                            displayFormat: "{0}",
                            alignByColumn: true,
                            valueFormat: {
                                type: ",##0.###",

                            }
                        },
                        {
                            column: 'TotalSambo',
                            summaryType: 'sum',
                            displayFormat: "{0}",
                            alignByColumn: true,
                            valueFormat: {
                                type: ",##0.###",

                            }
                        },
                        {
                            column: 'DacarStQuito',
                            summaryType: 'sum',
                            displayFormat: "{0}",
                            alignByColumn: true,
                            valueFormat: {
                                type: ",##0.###",

                            }
                        },
                        {
                            column: 'DacarBpQuito',
                            summaryType: 'sum',
                            displayFormat: "{0}",
                            alignByColumn: true,
                            valueFormat: {
                                type: ",##0.###",

                            }
                        },
                        {
                            column: 'DacarTxQuito',
                            summaryType: 'sum',
                            displayFormat: "{0}",
                            alignByColumn: true,
                            valueFormat: {
                                type: ",##0.###",

                            }
                            
                        },
                        {
                            column: 'TotalQuito',
                            summaryType: 'sum',
                            displayFormat: "{0}",
                            alignByColumn: true,
                            valueFormat: {
                                type: ",##0.###",

                            }
                            
                        },
                        {
                            column: 'DacarStRendon',
                            summaryType: 'sum',
                            displayFormat: "{0}",
                            alignByColumn: true,
                            valueFormat: {
                                type: ",##0.###",

                            }
                        },
                        {
                            column: 'DacarBpRendon',
                            summaryType: 'sum',
                            displayFormat: "{0}",
                            alignByColumn: true,
                            valueFormat: {
                                type: ",##0.###",

                            }
                        },
                        {
                            column: 'DacarTxRendon',
                            summaryType: 'sum',
                            displayFormat: "{0}",
                            alignByColumn: true,
                            valueFormat: {
                                type: ",##0.###",

                            }
                        },
                        {
                            column: 'TotalRendon',
                            summaryType: 'sum',
                            displayFormat: "{0}",
                            alignByColumn: true,
                            valueFormat: {
                                type: ",##0.###",

                            }
                        },
                        {
                            column: 'Referencia',
                            summaryType: 'count',
                            displayFormat: "{0}",
                            alignByColumn: true,
                            valueFormat: {
                                type: ",##0.###",

                            }
                        },


                    ],

                    totalItems: [
                        {
                            column: 'Modelo',
                            
                            displayFormat: "Totales:"
                        },
                        {
                        column: 'TOTAL',
                        summaryType: 'sum',
                        displayFormat: "{0}"
                    },
                        {
                            column: 'DacarStDanin',
                            summaryType: 'sum',
                            displayFormat: "{0}",
                            valueFormat: {
                                type: ",##0.###",

                            }
                        },
                        {
                            column: 'DacarBpDanin',
                            summaryType: 'sum',
                            displayFormat: "{0}",
                            valueFormat: {
                                type: ",##0.###",

                            }
                        },
                        {
                            column: 'DacarTxDanin',
                            summaryType: 'sum',
                            displayFormat: "{0}",
                            valueFormat: {
                                type: ",##0.###",

                            }
                        },
                        {
                            column: 'DacarStTanca',
                            summaryType: 'sum',
                            displayFormat: "{0}",
                            valueFormat: {
                                type: ",##0.###",

                            }
                        },
                        {
                            column: 'DacarBpTanca',
                            summaryType: 'sum',
                            displayFormat: "{0}",
                            valueFormat: {
                                type: ",##0.###",

                            }
                        },
                        {
                            column: 'DacarTxTanca',
                            summaryType: 'sum',
                            displayFormat: "{0}",
                            valueFormat: {
                                type: ",##0.###",

                            }
                        },
                        {
                            column: 'TotalTanca',
                            summaryType: 'sum',
                            displayFormat: "{0}"
                        },
                        {
                            column: 'DacarStSambo',
                            summaryType: 'sum',
                            displayFormat: "{0}",
                            valueFormat: {
                                type: ",##0.###",

                            }
                        },
                        {
                            column: 'DacarBpSambo',
                            summaryType: 'sum',
                            displayFormat: "{0}",
                            valueFormat: {
                                type: ",##0.###",

                            }
                        },
                        {
                            column: 'DacarTxSambo',
                            summaryType: 'sum',
                            displayFormat: "{0}",
                            valueFormat: {
                                type: ",##0.###",

                            }
                        },
                        {
                            column: 'TotalSambo',
                            summaryType: 'sum',
                            displayFormat: "{0}",
                            valueFormat: {
                                type: ",##0.###",

                            }
                        },
                        {
                            column: 'DacarStQuito',
                            summaryType: 'sum',
                            displayFormat: "{0}",
                            valueFormat: {
                                type: ",##0.###",

                            }
                        },
                        {
                            column: 'DacarBpQuito',
                            summaryType: 'sum',
                            displayFormat: "{0}",
                            valueFormat: {
                                type: ",##0.###",

                            }
                        },
                        {
                            column: 'DacarTxQuito',
                            summaryType: 'sum',
                            displayFormat: "{0}",
                             valueFormat: {
                                type: ",##0.###",

                            }
                        },
                        {
                            column: 'TotalQuito',
                            summaryType: 'sum',
                            displayFormat: "{0}",
                            valueFormat: {
                                type: ",##0.###",
                                
                            }
                        },
                        {
                            column: 'DacarStRendon',
                            summaryType: 'sum',
                            displayFormat: "{0}",
                            valueFormat: {
                                type: ",##0.###",

                            }
                        },
                        {
                            column: 'DacarBpRendon',
                            summaryType: 'sum',
                            displayFormat: "{0}",
                            valueFormat: {
                                type: ",##0.###",

                            }
                        },
                        {
                            column: 'DacarTxRendon',
                            summaryType: 'sum',
                            displayFormat: "{0}",
                            valueFormat: {
                                type: ",##0.###",

                            }
                        },
                        {
                            column: 'TotalRendon',
                            summaryType: 'sum',
                            displayFormat: "{0}",
                            valueFormat: {
                                type: ",##0.###",

                            }
                        }

                    ],
                },
            });
        },
        error: function (msg) {
            $("#MensajeErrorInesperado").show('fade');
        }

    })
}