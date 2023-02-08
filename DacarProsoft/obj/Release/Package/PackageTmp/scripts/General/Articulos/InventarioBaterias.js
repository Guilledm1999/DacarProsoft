

$(document).ready(function () {
    ConsultarArticulos();
});
function ConsultarArticulos() {
    $.ajax({
        url: "../Articulos/ConsultaBateriasPlanta",
        type: "GET"
        , success: function (msg) {


            $("#tblTiposArticulos").dxDataGrid({
                dataSource: msg,
                keyExpr: 'Modelo',
                showRowLines: false,
                columnAutoWidth: true,
                allowColumnReordering: true,
                showBorders: true,
                height: 700,
                wordWrapEnabled: true,
                allowColumnResizing: true,
                loadPanel: {
                    enabled: true,
                },
                columnChooser: {
                    enabled: true,
                },
                
                export: {
                    enabled: true,

                },
                
                sorting: {
                    mode: 'none',
                },
                scrolling: {
                    mode: 'virtual',
                },
                selection: {
                    mode: 'single',
                },
                stateStoring: {
                    enabled: true,
                    type: 'localStorage',
                    storageKey: 'storage',
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
                sortByGroupSummaryInfo: [{
                    summaryItem: 'count',
                }],

                headerFilter: { visible: true, allowSearch: true },
                onCellPrepared(options) {
                    const fieldData = options.value;
                    // let fieldHtml = '';
                    if (fieldData) {
                        if (options.rowType === "data") {
                            if (fieldData > 0 && options.column.caption != "Modelo" && options.column.caption != "Referencia") {
                                options.cellElement.css("font-size", "14px");
                                options.cellElement.css("font-weight", "bolder");

                            } else {
                                fieldHtml = fieldData.value;
                            }
                            options.cellElement.html(fieldHtml);
                        }}
                        

                    if (options.rowType === "data") {
                        if (options.column.caption === "TOTAL BS" || options.column.caption === "TOTAL BC" || options.column.caption === "TOTAL BT" )  { //condition where the column requires the coloring
                            options.cellElement.css("background-color", "#D7F1FF"); //set the background color based on the data
                        }
                    }


                },
                onRowPrepared: function (e) {
                    if (e.key == "N40-57 I BG" || e.key == "U1-32" || e.key == "88-100" || e.key == "42-65 I" || e.key == "55-65 I" || e.key == "66-75 I" || e.key == "48-77 I" || e.key == "25-70 I" || e.key == "24-MDP-1000"
                        || e.key == "34-85 I" || e.key == "27-MDP-1125" || e.key == "65-100 I" || e.key == "94-90" || e.key == "31-MDP-1250" || e.key == "36-50" || e.key == "NS40-50 I BF"   ) {
                       // e.rowElement.css("background-color", "#F1FBFF");
                       // e.rowElement.css("border-bottom-color", "#e3e5e6");
                        e.rowElement.css("border-bottom", "thick double #e3e5e6");
                        
                    }
                },

                onExporting(e) {
                    const workbook = new ExcelJS.Workbook();
                    const worksheet = workbook.addWorksheet('Inventario de Baterias en Planta');

                    DevExpress.excelExporter.exportDataGrid({
                        component: e.component,
                        worksheet,
                        autoFilterEnabled: true,
                        customizeCell(options) {
                            const { gridCell } = options;
                            const { excelCell } = options;

                            if (gridCell.rowType === 'data') {
                                if (gridCell.column.caption == 'TOTAL BS' || gridCell.column.caption == 'TOTAL BC' || gridCell.column.caption == 'TOTAL BT') {
                                    excelCell.fill = { type: 'pattern', pattern: 'solid', fgColor: { argb: 'D7F1FF' } };
                                }
                                if (gridCell.rowType == "header" )  {
                                    excelCell.fill = { type: 'pattern', pattern: 'solid', fgColor: { argb: 'D7F1FF' } };
                                }
                                if (gridCell.rowType === 'totalFooter' && excelCell.value) {
                                    excelCell.font.italic = true;
                                }
                                
                            }
                           
                        },


                    }).then(() => {
                        workbook.xlsx.writeBuffer().then((buffer) => {
                            saveAs(new Blob([buffer], { type: 'application/octet-stream' }), 'Inventario_Baterias_Planta.xlsx');
                        });
                    });
                    e.cancel = true;
                },

                

              


                columns: [
                    {
                        caption: 'Linea',
                        dataField: 'LineaReferencia',
                        
                        groupIndex: 0,
                    },
                    {
                        caption: 'Referencia',
                        dataField: 'Referencia',
                        alignment: "center",
                        allowHeaderFiltering: true,
                     //   width: 125,
                    },{

                    dataField: 'Modelo',
                    caption: 'Modelo',
                    allowHeaderFiltering: true,
                    alignment: "center",
                 //   width: 110,

                },

                {
                    caption: 'Baterias Selladas',
                    columns: [{
                        caption: 'ST-TEKNO',
                        dataField: 'DacarStSelladas',
                        format: 'fixedPoint',
                        alignment: "center",
                        allowHeaderFiltering: false,
                        width: '4%',
                        calculateDisplayValue: function (rowData) {
                            if (rowData["DacarStSelladas"] == 0) {
                                return "";
                            }
                            return rowData["DacarStSelladas"];
                        }
                    }, {
                        caption: 'BP',
                        dataField: 'DacarBpSelladas',
                        format: 'fixedPoint',
                        alignment: "center",
                        allowHeaderFiltering: false,
                        width: '4%',
                        calculateDisplayValue: function (rowData) {
                            if (rowData["DacarBpSelladas"] == 0) {
                                return "";
                            }
                            return rowData["DacarBpSelladas"];
                        }
                    },
                    {
                        caption: 'TX-ECO',
                        dataField: 'DacarTxSelladas',
                        format: 'fixedPoint',
                        alignment: "center",
                        allowHeaderFiltering: false,
                        width: '4%',
                        calculateDisplayValue: function (rowData) {
                            if (rowData["DacarTxSelladas"] == 0) {
                                return "";
                            }
                            return rowData["DacarTxSelladas"];
                        }
                        },/*
                        {
                            caption: 'TEKNO',
                            dataField: 'TeknoSelladas',
                            format: 'fixedPoint',
                            alignment: "center",
                            allowHeaderFiltering: false,
                        },*/
                        {
                            caption: 'Kaiser Ambato',
                            dataField: 'KaiserSelladas',
                            format: 'fixedPoint',
                            alignment: "center",
                            allowHeaderFiltering: false,
                            width: '4%',
                            calculateDisplayValue: function (rowData) {
                                if (rowData["KaiserSelladas"] == 0) {
                                    return "";
                                }
                                return rowData["KaiserSelladas"];
                            }
                        },
                       
                    {
                        caption: 'TOTAL BS',
                        dataField: 'TotalSelladas',
                        format: 'fixedPoint',
                        alignment: "center",
                        allowHeaderFiltering: false,
                        width: '4%',
                        calculateDisplayValue: function (rowData) {
                            if (rowData["TotalSelladas"] == 0) {
                                return "";
                            }
                            return rowData["TotalSelladas"];
                        }
                        },
                       
                    ],
                }, {
                    caption: 'Baterias en Carga',
                    columns: [{
                        caption: 'ST-TEKNO',
                        dataField: 'DacarStCarga',
                        format: 'fixedPoint',
                        alignment: "center",
                        allowHeaderFiltering: false,
                        width: '4%',
                        calculateDisplayValue: function (rowData) {
                            if (rowData["DacarStCarga"] == 0) {
                                return "";
                            }
                            return rowData["DacarStCarga"];
                        }
                    }, {
                        caption: 'BP',
                        dataField: 'DacarBpCarga',
                        format: 'fixedPoint',
                        alignment: "center",
                        allowHeaderFiltering: false,
                        width: '4%',
                        calculateDisplayValue: function (rowData) {
                            if (rowData["DacarBpCarga"] == 0) {
                                return "";
                            }
                            return rowData["DacarBpCarga"];
                        }
                    },
                    {
                        caption: 'TX-ECO',
                        dataField: 'DacarTxCarga',
                        format: 'fixedPoint',
                        alignment: "center",
                        allowHeaderFiltering: false,
                        width: '4%',
                        calculateDisplayValue: function (rowData) {
                            if (rowData["DacarTxCarga"] == 0) {
                                return "";
                            }
                            return rowData["DacarTxCarga"];
                        }
                        },/*
                        {
                            caption: 'TEKNO',
                            dataField: 'TeknoCarga',
                            format: 'fixedPoint',
                            alignment: "center",
                            allowHeaderFiltering: false,
                        },*/
                        {
                            caption: 'Kaiser Ambato',
                            dataField: 'KaiserCarga',
                            format: 'fixedPoint',
                            alignment: "center",
                            allowHeaderFiltering: false,
                            width: '4%',
                            calculateDisplayValue: function (rowData) {
                                if (rowData["KaiserCarga"] == 0) {
                                    return "";
                                }
                                return rowData["KaiserCarga"];
                            }
                        },
                    {
                        caption: 'TOTAL BC',
                        dataField: 'TotalCarga',
                        format: 'fixedPoint',
                        alignment: "center",
                        allowHeaderFiltering: false,
                        width: '4%',
                        calculateDisplayValue: function (rowData) {
                            if (rowData["TotalCarga"] == 0) {
                                return "";
                            }
                            return rowData["TotalCarga"];
                        }
                    },
                    ],
                },
                {
                    caption: 'Baterias terminadas en CD',
                    columns: [{
                        caption: 'ST',
                        dataField: 'DacarStCd',
                        format: 'fixedPoint',
                        alignment: "center",
                        allowHeaderFiltering: false,
                        width: '4%',
                        calculateDisplayValue: function (rowData) {
                            if (rowData["DacarStCd"] == 0) {
                                return "";
                            }
                            return rowData["DacarStCd"];
                        }
                    }, {
                        caption: 'BP',
                        dataField: 'DacarBpCd',
                        format: 'fixedPoint',
                        alignment: "center",
                        allowHeaderFiltering: false,
                        width: '4%',
                        calculateDisplayValue: function (rowData) {
                            if (rowData["DacarBpCd"] == 0) {
                                return "";
                            }
                            return rowData["DacarBpCd"];
                        }
                    },
                    {
                        caption: 'TX-ECO',
                        dataField: 'DacarTxCd',
                        format: 'fixedPoint',
                        alignment: "center",
                        allowHeaderFiltering: false,
                        width: '4%',
                        calculateDisplayValue: function (rowData) {
                            if (rowData["DacarTxCd"] == 0) {
                                return "";
                            }
                            return rowData["DacarTxCd"];
                        }
                        },
                        {
                            caption: 'TEKNO',
                            dataField: 'TeknoCd',
                            format: 'fixedPoint',
                            alignment: "center",
                            allowHeaderFiltering: false,
                            width: '4%',
                            calculateDisplayValue: function (rowData) {
                                if (rowData["TeknoCd"] == 0) {
                                    return "";
                                }
                                return rowData["TeknoCd"];
                            }
                        },
                        {
                            caption: 'Kaiser Ambato',
                            dataField: 'KaiserCd',
                            format: 'fixedPoint',
                            alignment: "center",
                            allowHeaderFiltering: false,
                            width: '4%',
                            calculateDisplayValue: function (rowData) {
                                if (rowData["KaiserCd"] == 0) {
                                    return "";
                                }
                                return rowData["KaiserCd"];
                            }
                        },
                    {
                        caption: 'TOTAL BT',
                        dataField: 'TotalCd',
                        format: 'fixedPoint',
                        alignment: "center",
                        allowHeaderFiltering: false,
                        width: '4%',
                        calculateDisplayValue: function (rowData) {
                            if (rowData["TotalCd"] == 0) {
                                return "";
                            }
                            return rowData["TotalCd"];
                        }
                    },
                    ],
                    },
                   
                    {

                        dataField: 'Devolucion',
                        caption: 'Devolución',
                        alignment: "center",
                        width: '6%',
                        allowHeaderFiltering: false,
                        calculateDisplayValue: function (rowData) {
                            if (rowData["Devolucion"] == 0) {
                                return "";
                            }
                            return rowData["Devolucion"];
                        }

                    },   
                    {

                        dataField: 'SinMarca',
                        caption: 'Sin Marca',
                        alignment: "center",
                        width: '6%',
                        allowHeaderFiltering: false,
                        calculateDisplayValue: function (rowData) {
                            if (rowData["SinMarca"] == 0) {
                                return "";
                            }
                            return rowData["SinMarca"];
                        }

                    },
                    {

                        dataField: 'Magnum',
                        caption: 'Magnum',
                        alignment: "center",
                        width: '6%',
                        allowHeaderFiltering: false,
                        calculateDisplayValue: function (rowData) {
                            if (rowData["Magnum"] == 0) {
                                return "";
                            }
                            return rowData["Magnum"];
                        }

                    },

              
                ],
                summary: {
                    groupItems: [ 
                        
                        {
                            column: 'DacarStSelladas',
                            summaryType: 'sum',
                            displayFormat: "{0}",
                            showInGroupFooter: true,
                            valueFormat: {
                                type: ",##0.###",

                            }
                        },
                        {
                            column: 'DacarBpSelladas',
                            summaryType: 'sum',
                            displayFormat: "{0}",
                            showInGroupFooter: true,
                            valueFormat: {
                                type: ",##0.###",

                            }
                        },
                        {
                            column: 'DacarTxSelladas',
                            summaryType: 'sum',
                            displayFormat: "{0}",
                            showInGroupFooter: true,
                            valueFormat: {
                                type: ",##0.###",

                            }
                        },
                       
                        {
                            column: 'KaiserSelladas',
                            summaryType: 'sum',
                            displayFormat: "{0}",
                            showInGroupFooter: true,
                            valueFormat: {
                                type: ",##0.###",

                            }
                        },
                        {
                            column: 'TotalSelladas',
                            summaryType: 'sum',
                            displayFormat: "{0}",
                            showInGroupFooter: true,
                            valueFormat: {
                                type: ",##0.###",

                            }
                        },
                        {
                            column: 'Magnum',
                            summaryType: 'sum',
                            displayFormat: "{0}",
                            showInGroupFooter: true,
                            valueFormat: {
                                type: ",##0.###",

                            }
                        },
                        {
                            column: 'Devolucion',
                            summaryType: 'sum',
                            displayFormat: "{0}",
                            showInGroupFooter: true,
                            valueFormat: {
                                type: ",##0.###",

                            }
                        },
                        {
                            column: 'TotalCarga',
                            summaryType: 'sum',
                            displayFormat: "{0}",
                            showInGroupFooter: true,
                            valueFormat: {
                                type: ",##0.###",

                            }
                        },
                       
                        {
                            column: 'DacarStCarga',
                            summaryType: 'sum',
                            displayFormat: "{0}",
                            showInGroupFooter: true,
                            valueFormat: {
                                type: ",##0.###",

                            }
                        },
                        {
                            column: 'DacarBpCarga',
                            summaryType: 'sum',
                            displayFormat: "{0}",
                            showInGroupFooter: true,
                            valueFormat: {
                                type: ",##0.###",

                            }
                        },
                        {
                            column: 'DacarTxCarga',
                            summaryType: 'sum',
                            displayFormat: "{0}",
                            showInGroupFooter: true,
                            valueFormat: {
                                type: ",##0.###",

                            }
                        },
                        {
                            column: 'KaiserCarga',
                            summaryType: 'sum',
                            displayFormat: "{0}",
                            showInGroupFooter: true,
                            valueFormat: {
                                type: ",##0.###",

                            }
                        },
                        {
                            column: 'KaiserCd',
                            summaryType: 'sum',
                            displayFormat: "{0}",
                            showInGroupFooter: true,
                            valueFormat: {
                                type: ",##0.###",

                            }
                        },
                        {
                            column: 'DacarBpCd',
                            summaryType: 'sum',
                            displayFormat: "{0}",
                            showInGroupFooter: true,
                            valueFormat: {
                                type: ",##0.###",

                            }
                        },
                        {
                            column: 'DacarTxCd',
                            summaryType: 'sum',
                            displayFormat: "{0}",
                            showInGroupFooter: true,
                            valueFormat: {
                                type: ",##0.###",

                            }
                        },
                        {
                            column: 'DacarStCd',
                            summaryType: 'sum',
                            displayFormat: "{0}",
                            showInGroupFooter: true,
                            valueFormat: {
                                type: ",##0.###",

                            }
                        },
                        {
                            column: 'TeknoCd',
                            summaryType: 'sum',
                            displayFormat: "{0}",
                            showInGroupFooter: true,
                            valueFormat: {
                                type: ",##0.###",

                            }
                        },
                        {
                            column: 'TotalCd',
                            summaryType: 'sum',
                            displayFormat: "{0}",
                            showInGroupFooter: true,
                            valueFormat: {
                                type: ",##0.###",

                            }
                        },
                        {
                            column: 'DacarStSelladas',
                            summaryType: 'sum',
                            displayFormat: "{0}",
                            alignByColumn: true,
                            valueFormat: {
                                type: ",##0.###",

                            }
                        },
                        {
                            column: 'DacarBpSelladas',
                            summaryType: 'sum',
                            displayFormat: "{0}",
                            alignByColumn: true,
                            valueFormat: {
                                type: ",##0.###",

                            }
                        },
                        {
                            column: 'DacarTxSelladas',
                            summaryType: 'sum',
                            displayFormat: "{0}",
                            alignByColumn: true,
                            valueFormat: {
                                type: ",##0.###",

                            }
                        },

                        {
                            column: 'KaiserSelladas',
                            summaryType: 'sum',
                            displayFormat: "{0}",
                            alignByColumn: true,
                            valueFormat: {
                                type: ",##0.###",

                            }
                        },
                        {
                            column: 'TotalSelladas',
                            summaryType: 'sum',
                            displayFormat: "{0}",
                            alignByColumn: true,
                            valueFormat: {
                                type: ",##0.###",

                            }
                        },
                        {
                            column: 'Magnum',
                            summaryType: 'sum',
                            displayFormat: "{0}",
                            alignByColumn: true,
                            valueFormat: {
                                type: ",##0.###",

                            }
                        },
                        {
                            column: 'SinMarca',
                            summaryType: 'sum',
                            displayFormat: "{0}",
                            alignByColumn: true,
                            valueFormat: {
                                type: ",##0.###",

                            }
                        },
                        {
                            column: 'Devolucion',
                            summaryType: 'sum',
                            displayFormat: "{0}",
                            alignByColumn: true,
                            valueFormat: {
                                type: ",##0.###",

                            }
                        },
                        {
                            column: 'TotalCarga',
                            summaryType: 'sum',
                            displayFormat: "{0}",
                            alignByColumn: true,
                            valueFormat: {
                                type: ",##0.###",

                            }
                           
                        },

                        {
                            column: 'DacarStCarga',
                            summaryType: 'sum',
                            displayFormat: "{0}",
                            alignByColumn: true,
                            valueFormat: {
                                type: ",##0.###",

                            }
                        },
                        {
                            column: 'DacarBpCarga',
                            summaryType: 'sum',
                            displayFormat: "{0}",
                            alignByColumn: true,
                            valueFormat: {
                                type: ",##0.###",

                            }
                        },
                        {
                            column: 'DacarTxCarga',
                            summaryType: 'sum',
                            displayFormat: "{0}",
                            alignByColumn: true,
                            valueFormat: {
                                type: ",##0.###",

                            }
                        },
                        {
                            column: 'KaiserCarga',
                            summaryType: 'sum',
                            displayFormat: "{0}",
                            alignByColumn: true,
                            valueFormat: {
                                type: ",##0.###",

                            }
                        },
                        {
                            column: 'KaiserCd',
                            summaryType: 'sum',
                            displayFormat: "{0}",
                            alignByColumn: true,
                            valueFormat: {
                                type: ",##0.###",

                            }
                        },
                        {
                            column: 'DacarBpCd',
                            summaryType: 'sum',
                            displayFormat: "{0}",
                            alignByColumn: true,
                            valueFormat: {
                                type: ",##0.###",

                            }
                        },
                        {
                            column: 'DacarTxCd',
                            summaryType: 'sum',
                            displayFormat: "{0}",
                            alignByColumn: true,
                            valueFormat: {
                                type: ",##0.###",

                            }
                        },
                        {
                            column: 'DacarStCd',
                            summaryType: 'sum',
                            displayFormat: "{0}",
                            alignByColumn: true,
                            valueFormat: {
                                type: ",##0.###",

                            }
                        },
                        {
                            column: 'TeknoCd',
                            summaryType: 'sum',
                            displayFormat: "{0}",
                            alignByColumn: true,
                            valueFormat: {
                                type: ",##0.###",

                            }
                        },
                        {
                            column: 'TotalCd',
                            summaryType: 'sum',
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
                        column: 'TOTAL BS',
                        summaryType: 'sum',
                            displayFormat: "{0}",
                            valueFormat: {
                                type: ",##0.###",

                            }
                    },
                    {
                        column: 'DacarStSelladas',
                        summaryType: 'sum',
                        displayFormat: "{0}",
                        valueFormat: {
                            type: ",##0.###",

                        }
                        },
                        {
                            column: 'DacarBpSelladas',
                            summaryType: 'sum',
                            displayFormat: "{0}",
                            valueFormat: {
                                type: ",##0.###",

                            }
                        },
                        {
                            column: 'DacarTxSelladas',
                            summaryType: 'sum',
                            displayFormat: "{0}",
                            valueFormat: {
                                type: ",##0.###",

                            }
                        },
                        {
                            column: 'TEKNO',
                            summaryType: 'sum',
                            displayFormat: "{0}",
                            valueFormat: {
                                type: ",##0.###",

                            }
                        },
                        {
                            column: 'Kaiser Ambato',
                            summaryType: 'sum',
                            displayFormat: "{0}",
                            valueFormat: {
                                type: ",##0.###",

                            }
                        },
                        {
                            column: 'DacarStCd',
                            summaryType: 'sum',
                            displayFormat: "{0}",
                            valueFormat: {
                                type: ",##0.###",

                            }
                        },
                        {
                            column: 'Magnum',
                            summaryType: 'sum',
                            displayFormat: "{0}",
                            valueFormat: {
                                type: ",##0.###",

                            }
                        },
                        {
                            column: 'SinMarca',
                            summaryType: 'sum',
                            displayFormat: "{0}",
                            valueFormat: {
                                type: ",##0.###",

                            }
                        },
                        {
                            column: 'Devolucion',
                            summaryType: 'sum',
                            displayFormat: "{0}",
                            valueFormat: {
                                type: ",##0.###",

                            }
                        },
                        {
                            column: 'TOTAL BT',
                            summaryType: 'sum',
                            displayFormat: "{0}",
                            valueFormat: {
                                type: ",##0.###",

                            }
                        },
                        {
                            column: 'TOTAL BC',
                            summaryType: 'sum',
                            displayFormat: "{0}",
                            valueFormat: {
                                type: ",##0.###",

                            }
                        },
                        {
                            column: 'DacarStCarga',
                            summaryType: 'sum',
                            displayFormat: "{0}",
                            valueFormat: {
                                type: ",##0.###",

                            }
                        },
                        {
                            column: 'DacarBpCarga',
                            summaryType: 'sum',
                            displayFormat: "{0}",
                            valueFormat: {
                                type: ",##0.###",

                            }
                        },
                        {
                            column: 'DacarTxCarga',
                            summaryType: 'sum',
                            displayFormat: "{0}",
                            valueFormat: {
                                type: ",##0.###",

                            }
                        },
                        {
                            column: 'KaiserCarga',
                            summaryType: 'sum',
                            displayFormat: "{0}",
                            valueFormat: {
                                type: ",##0.###",

                            }
                        },
                        {
                            column: 'KaiserCd',
                            summaryType: 'sum',
                            displayFormat: "{0}",
                            valueFormat: {
                                type: ",##0.###",

                            }
                        },
                        {
                            column: 'DacarBpCd',
                            summaryType: 'sum',
                            displayFormat: "{0}",
                            valueFormat: {
                                type: ",##0.###",

                            }
                        },
                        {
                            column: 'DacarTxCd',
                            summaryType: 'sum',
                            displayFormat: "{0}",
                            valueFormat: {
                                type: ",##0.###",

                            }
                        },

                    ],

                },
               

            });
       
        },
        error: function (msg) {
            $("#MensajeErrorInesperado").show('fade');
        }

    })


}