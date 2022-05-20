$(document).ready(function () {
    $(".loading-icon").css("display", "none");
});

function ConsultaDeIngresos() {
    $(".result").text("");
    $(".loading-icon").removeClass("hide");
    $(".btn").attr("disabled", true);
    $(".btn-txt").text("Espere...");
    InformeIngresosDeChatarra();
}

function InformeIngresosDeChatarra() {
    var select = $("#anioClass option:selected").text();
    document.getElementById("OcultarBoton").style.display = "";
    $.ajax({
        url: "../Chatarra/ConsultaIngresosChatarraConDesviacionSap?anio=" + select,
        type: "GET"
        , success: function (msg) {
            //$("#cargaImg").hide();
            const locale = getLocale();
            DevExpress.localization.locale(locale);

            $("#IngresosdeChatarras").dxDataGrid({
                dataSource: msg,
                columnAutoWidth: true,
                keyExpr: "Id",
                showBorders: true,
                allowColumnReordering: false,
                filterRow: { visible: false },
                filterPanel: { visible: false },
                headerFilter: { visible: true },
                columnFixing: {
                    enabled: true
                },
                export: {
                    enabled: true,
                },
                paging: {
                    pageSize: 10
                },
                pager: {
                    showPageSizeSelector: true,
                    allowedPageSizes: [5, 10, 100],
                    showInfo: true
                },
                onExporting(e) {
                    const workbook = new ExcelJS.Workbook();
                    const worksheet = workbook.addWorksheet('Chatarra');

                    DevExpress.excelExporter.exportDataGrid({
                        component: e.component,
                        worksheet,
                        autoFilterEnabled: true,
                    }).then(() => {
                        workbook.xlsx.writeBuffer().then((buffer) => {
                            saveAs(new Blob([buffer], { type: 'application/octet-stream' }), 'IngresoChatarra.xlsx');
                        });
                    });
                    e.cancel = true;
                },
                columns: [
                    { dataField: "Id", visible: false },
                    //{ dataField: "CardCode", visible: false },
                    {
                        caption: "Acciones", fixed: true,

                        cellTemplate: function (container, options) {
                            var btn = "<button class='btn-primary' onclick='DetalleChatarra(" + JSON.stringify(options.data) + ")'>Detalle</button>";
                            $("<div>")
                                .append($(btn))
                                .appendTo(container);
                        }
                    },
                    {
                        dataField: "N_Documento", caption: "# Documento", allowEditing: false, fixed: false, allowFiltering: false
                    },
                    {
                        dataField: "Pedido", caption: "# Pedido", allowEditing: false, fixed: false, allowFiltering: false
                    },
                    {
                        dataField: "Identificador", caption: "Identificacion", allowEditing: false, width: 130, allowFiltering: false
                    },
                    {
                        dataField: "Cliente", caption: "Cliente", allowEditing: false, fixed: false, width: 250
                    },
                    {
                        dataField: "Tipo_Cliente", caption: "Tipo Cliente", allowEditing: false
                    },
                    {
                        dataField: "Cliente_Linea", caption: "Cliente Linea", allowEditing: false
                    },
                    {
                        dataField: "Cliente_Clase", caption: "Cliente Clase", allowEditing: false
                    },
                    {
                        dataField: "Tipo_Ingreso", caption: "Tipo Ingreso", allowEditing: false
                    },
                    {
                        dataField: "FechaRegistro", caption: "Mes Ingreso", allowEditing: false, dataType: 'date', format: 'month'
                    },
                    {
                        dataField: "Cantidad", caption: "Cantidad(Uds.)", allowFiltering: false, allowEditing: false,
                    },
                    ,
                    {
                        dataField: "Peso_Teorico", caption: "Peso Teorico(kg)", alignment: "right", allowFiltering: false, width: 130, allowEditing: false,
                        format: {
                            type: "fixedPoint",
                            precision: 2,

                        },
                        customizeText: function (cellInfo) {
                        const noTruncarDecimales = { maximumFractionDigits: 2, minimumFractionDigits: 2 };
                        return (cellInfo.value).toLocaleString('en-US', noTruncarDecimales);
                    }
                   
                    },
                    {
                        dataField: "Peso_Real", caption: "Peso Ingresado(kg)", alignment: "right", allowFiltering: false, width: 135, allowEditing: false,
                        format: {
                            type: "fixedPoint",
                            precision: 2,
                        }, customizeText: function (cellInfo) {
                            const noTruncarDecimales = { maximumFractionDigits: 2, minimumFractionDigits: 2 };
                            return (cellInfo.value).toLocaleString('en-US', noTruncarDecimales);
                        },
                        
                    },              
                    {
                        dataField: "Desviacion", caption: "Desviacion", alignment: "right", allowFiltering: false, allowEditing: false,
                        format: {
                            type: "fixedPoint",
                            precision: 2,
                        }, customizeText: function (cellInfo) {
                            return cellInfo.value + "%";
                        },                        
                    cellTemplate(container, options) {
                        container.addClass((options.data.Desviacion > -4) ? 'inc' : 'dec');
                        container.html(options.text);

                    }
                       
                    },
                    {
                        dataField: "Bodega", caption: "Bodega", allowEditing: false
                    },
                    {
                        dataField: "Vendedor", caption: "Comentarios", allowFiltering: false, allowEditing: false
                    },
                    {
                        dataField: "Vendedor", caption: "Comentarios", allowFiltering: false, allowEditing: false
                    },
                    {
                        dataField: "Comentarios", caption: "Fecha Ingreso", allowEditing: false,
                    },
                    {
                        dataField: "FechaRegistro", caption: "Fecha Ingreso", allowEditing: false, dataType: 'date'
                    }
                ],
                summary: {
                    totalItems: [
                        {
                            name: "Tipo_Ingreso",
                            column: "Tipo_Ingreso",
                            summaryType: "count",
                            displayFormat: "Cantidad Total",
                            showInColumn: "Tipo_Ingreso",
                            customizeText: function (e) {
                                if (e.value != 0 && e.value != "") {
                                    return "Totales: ";
                                }
                            }
                        }
                        ,
                        {
                            name: "Cantidad",
                            column: "Cantidad",
                            summaryType: "sum",
                            displayFormat: "Cantidad Total",
                            showInColumn: "Cantidad",
                            customizeText: function (e) {
                                if (e.value != 0 && e.value != "") {                                 
                                    const noTruncarDecimales = { maximumFractionDigits: 0, minimumFractionDigits: 0 };
                                    ValTotal = (e.value).toLocaleString('en-US', noTruncarDecimales);
                                    return ValTotal;                                }
                            }
                        }
                        , {
                            column: "Peso_Teorico",
                            summaryType: "sum",
                            showInColumn: "Peso_Teorico",
                            displayFormat: "Total: {0}",
                            valueFormat: "currency",
                            customizeText: function (e) {
                                if (e.value != 0 && e.value != "") {
                                    const noTruncarDecimales = { maximumFractionDigits: 2, minimumFractionDigits: 2 };
                                    ValTotal = (e.value).toLocaleString('en-US', noTruncarDecimales);
                                    return ValTotal + "kg";
                                }
                            },
                          
                        },
                        {
                            column: "Peso_Real",
                            summaryType: "sum",
                            showInColumn: "Peso_Real",
                            displayFormat: "Total: {0}",
                            valueFormat: "currency",
                            customizeText: function (e) {
                                if (e.value != 0 && e.value != "") {
                                    const noTruncarDecimales = { maximumFractionDigits: 2, minimumFractionDigits: 2 };
                                    ValTotal = (e.value).toLocaleString('en-US', noTruncarDecimales);
                                    return ValTotal + "kg";
                                }
                            }
                        },
                        {
                            column: "Desviacion",
                            summaryType: "avg",
                            showInColumn: "Desviacion",
                            displayFormat: "Total: {0}",
                            valueFormat: "currency",
                            customizeText: function (e) {
                                    if (e.value != 0 && e.value != "") {
                                        const noTruncarDecimales = { maximumFractionDigits: 2, minimumFractionDigits: 2 };
                                        ValTotal = (e.value).toLocaleString('en-US', noTruncarDecimales);
                                        return ValTotal + "%";
                                    }                               
                            }

                        },
                    ],
                },
                onContentReady: function (e) {
                    //DatosFiltradosTabla();
                },
                onCellPrepared: function (e) {
                    if (e.rowType === "data" && e.column.dataField === "Peso_Real") {
                        e.cellElement.css("color", e.data >= 20 ? "inc" : "dec");
                        // Tracks the `Amount` data field
                            return e.data;                      
                    }
                }
            });
            $(".btn").attr("disabled", false);
            $(".btn-txt").text("Consultar");
        },
        error: function (msg) {
            $(".btn").attr("disabled", false);
            $(".btn-txt").text("Consultar");
            $("#MensajeErrorInesperado").show('fade');
            $("#cargaImg").hide();
            setTimeout(function () {
                $("#MensajeErrorInesperado").fadeOut(1500);
            }, 3000);
        }
    })
    function getLocale() {
        const storageLocale = sessionStorage.getItem('locale');
        return storageLocale != null ? storageLocale : 'es';
    }
}

function DetalleChatarra(modelo) {
    var url = null;
    if (modelo.Tipo_Ingreso == "Compras") {
        url = "../Chatarra/ConsultaDetalleIngresoMercanciasSapSinFu?DocEntry=" + modelo.DocEntry;
    } else {
        url = "../Chatarra/ConsultaCompraDetalleIngresoMercanciasSapSinFu?DocEntry=" + modelo.DocEntry;
    }

    $.ajax({
        url: url,
        type: "GET"
        , success: function (msg) {
            //$("#cargaImg").hide();
            const locale = getLocale();
            DevExpress.localization.locale(locale);

            $("#ChatarraDetalle").dxDataGrid({
                dataSource: msg,
                columnAutoWidth: true,
                keyExpr: "Id",
                showBorders: true,
                allowColumnReordering: false,
                filterRow: { visible: false },
                filterPanel: { visible: false },
                headerFilter: { visible: true },
                columnFixing: {
                    enabled: true
                },
                export: {
                    enabled: true,
                },
                paging: {
                    pageSize: 10
                },
                pager: {
                    showPageSizeSelector: true,
                    allowedPageSizes: [5, 10, 100],
                    showInfo: true
                },
                onExporting(e) {
                    const workbook = new ExcelJS.Workbook();
                    const worksheet = workbook.addWorksheet('Chatarra');

                    DevExpress.excelExporter.exportDataGrid({
                        component: e.component,
                        worksheet,
                        autoFilterEnabled: true,
                    }).then(() => {
                        workbook.xlsx.writeBuffer().then((buffer) => {
                            saveAs(new Blob([buffer], { type: 'application/octet-stream' }), 'IngresoChatarra.xlsx');
                        });
                    });
                    e.cancel = true;
                },
                columns: [
                    { dataField: "Id", visible: false },
                    //{ dataField: "CardCode", visible: false },
                    {
                        caption: "Acciones", fixed: true,

                        cellTemplate: function (container, options) {
                            var btn = "<button class='btn-primary' onclick='DetalleChatarra(" + JSON.stringify(options.data) + ")'>Detalle</button>";
                            $("<div>")
                                .append($(btn))
                                .appendTo(container);
                        }
                    },
                    {
                        dataField: "N_Documento", caption: "# Documento", allowEditing: false, fixed: false, allowFiltering: false
                    },
                    {
                        dataField: "Pedido", caption: "# Pedido", allowEditing: false, fixed: false, allowFiltering: false
                    },
                    {
                        dataField: "Identificador", caption: "Identificacion", allowEditing: false, width: 130, allowFiltering: false
                    },
                    {
                        dataField: "Cliente", caption: "Cliente", allowEditing: false, fixed: false, width: 250
                    },
                    {
                        dataField: "Tipo_Cliente", caption: "Tipo Cliente", allowEditing: false
                    },
                    {
                        dataField: "Cliente_Linea", caption: "Cliente Linea", allowEditing: false
                    },
                    {
                        dataField: "Cliente_Clase", caption: "Cliente Clase", allowEditing: false
                    },
                    {
                        dataField: "Tipo_Ingreso", caption: "Tipo Ingreso", allowEditing: false
                    },
                    {
                        dataField: "FechaRegistro", caption: "Mes Ingreso", allowEditing: false, dataType: 'date', format: 'month'
                    },
                    {
                        dataField: "Cantidad", caption: "Cantidad(Uds.)", allowFiltering: false, allowEditing: false,
                    },
                    ,
                    {
                        dataField: "Peso_Teorico", caption: "Peso Teorico(kg)", alignment: "right", allowFiltering: false, width: 130, allowEditing: false,
                        format: {
                            type: "fixedPoint",
                            precision: 2,

                        },
                        customizeText: function (cellInfo) {
                            const noTruncarDecimales = { maximumFractionDigits: 2, minimumFractionDigits: 2 };
                            return (cellInfo.value).toLocaleString('en-US', noTruncarDecimales);
                        }

                    },
                    {
                        dataField: "Peso_Real", caption: "Peso Ingresado(kg)", alignment: "right", allowFiltering: false, width: 135, allowEditing: false,
                        format: {
                            type: "fixedPoint",
                            precision: 2,
                        }, customizeText: function (cellInfo) {
                            const noTruncarDecimales = { maximumFractionDigits: 2, minimumFractionDigits: 2 };
                            return (cellInfo.value).toLocaleString('en-US', noTruncarDecimales);
                        },

                    },
                    {
                        dataField: "Desviacion", caption: "Desviacion", alignment: "right", allowFiltering: false, allowEditing: false,
                        format: {
                            type: "fixedPoint",
                            precision: 2,
                        }, customizeText: function (cellInfo) {
                            return cellInfo.value + "%";
                        },
                        cellTemplate(container, options) {
                            container.addClass((options.data.Desviacion > -4) ? 'inc' : 'dec');
                            container.html(options.text);

                        }

                    },
                    {
                        dataField: "Bodega", caption: "Bodega", allowEditing: false
                    },
                    {
                        dataField: "Vendedor", caption: "Comentarios", allowFiltering: false, allowEditing: false
                    },
                    {
                        dataField: "Vendedor", caption: "Comentarios", allowFiltering: false, allowEditing: false
                    },
                    {
                        dataField: "Comentarios", caption: "Fecha Ingreso", allowEditing: false,
                    },
                    {
                        dataField: "FechaRegistro", caption: "Fecha Ingreso", allowEditing: false, dataType: 'date'
                    }
                ],
                summary: {
                    totalItems: [
                        {
                            name: "Tipo_Ingreso",
                            column: "Tipo_Ingreso",
                            summaryType: "count",
                            displayFormat: "Cantidad Total",
                            showInColumn: "Tipo_Ingreso",
                            customizeText: function (e) {
                                if (e.value != 0 && e.value != "") {
                                    return "Totales: ";
                                }
                            }
                        }
                        ,
                        {
                            name: "Cantidad",
                            column: "Cantidad",
                            summaryType: "sum",
                            displayFormat: "Cantidad Total",
                            showInColumn: "Cantidad",
                            customizeText: function (e) {
                                if (e.value != 0 && e.value != "") {
                                    const noTruncarDecimales = { maximumFractionDigits: 0, minimumFractionDigits: 0 };
                                    ValTotal = (e.value).toLocaleString('en-US', noTruncarDecimales);
                                    return ValTotal;
                                }
                            }
                        }
                        , {
                            column: "Peso_Teorico",
                            summaryType: "sum",
                            showInColumn: "Peso_Teorico",
                            displayFormat: "Total: {0}",
                            valueFormat: "currency",
                            customizeText: function (e) {
                                if (e.value != 0 && e.value != "") {
                                    const noTruncarDecimales = { maximumFractionDigits: 2, minimumFractionDigits: 2 };
                                    ValTotal = (e.value).toLocaleString('en-US', noTruncarDecimales);
                                    return ValTotal + "kg";
                                }
                            },

                        },
                        {
                            column: "Peso_Real",
                            summaryType: "sum",
                            showInColumn: "Peso_Real",
                            displayFormat: "Total: {0}",
                            valueFormat: "currency",
                            customizeText: function (e) {
                                if (e.value != 0 && e.value != "") {
                                    const noTruncarDecimales = { maximumFractionDigits: 2, minimumFractionDigits: 2 };
                                    ValTotal = (e.value).toLocaleString('en-US', noTruncarDecimales);
                                    return ValTotal + "kg";
                                }
                            }
                        },
                        {
                            column: "Desviacion",
                            summaryType: "avg",
                            showInColumn: "Desviacion",
                            displayFormat: "Total: {0}",
                            valueFormat: "currency",
                            customizeText: function (e) {
                                if (e.value != 0 && e.value != "") {
                                    const noTruncarDecimales = { maximumFractionDigits: 2, minimumFractionDigits: 2 };
                                    ValTotal = (e.value).toLocaleString('en-US', noTruncarDecimales);
                                    return ValTotal + "%";
                                }
                            }

                        },
                    ],
                },
                onContentReady: function (e) {
                    //DatosFiltradosTabla();
                },
                onCellPrepared: function (e) {
                    if (e.rowType === "data" && e.column.dataField === "Peso_Real") {
                        e.cellElement.css("color", e.data >= 20 ? "inc" : "dec");
                        // Tracks the `Amount` data field
                        return e.data;
                    }
                }
            });
            $(".btn").attr("disabled", false);
            $(".btn-txt").text("Consultar");
        },
        error: function (msg) {
            $(".btn").attr("disabled", false);
            $(".btn-txt").text("Consultar");
            $("#MensajeErrorInesperado").show('fade');
            $("#cargaImg").hide();
            setTimeout(function () {
                $("#MensajeErrorInesperado").fadeOut(1500);
            }, 3000);
        }
    })
}