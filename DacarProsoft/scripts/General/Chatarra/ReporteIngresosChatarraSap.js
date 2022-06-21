var valor = null;
var char;
var char2;
var char3;
var char4;
var char5;
var char6;
var char7;
var char8;
var char9;
var char10;
var char11;
var char12;
var pesoTotalIng = null;
var cantidadTotalIng = null;
var desviacionTotalCal = null;

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
                allowColumnReordering: true,
                allowColumnResizing: true,
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
                searchPanel: {
                    visible: true,
                    width: 240,
                    placeholder: "Buscar..."
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
                    { dataField: "DocEntry", visible: false },
                    { dataField: "FechaRegistro2", visible: false },
                    {
                        dataField: "FechaRegistro", caption: "Fecha Ingreso", allowEditing: false, dataType: 'date', sortOrder: "desc"
                    },
                    {
                        dataField: "FechaRegistro", caption: "Mes Ingreso", allowEditing: false, dataType: 'date', format: 'month'
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
                        dataField: "Vendedor", caption: "Vendedor", allowEditing: false
                    },
                    {
                        dataField: "Comentarios", caption: "Comentarios", allowEditing: false,
                    },                  
                    {
                        caption: "Acciones",

                        cellTemplate: function (container, options) {
                            var btn = "<button class='btn-primary' onclick='DetalleChatarra(" + JSON.stringify(options.data) + ")'>Detalle</button>";
                            $("<div>")
                                .append($(btn))
                                .appendTo(container);
                        }
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
                                    cantidadTotalIng = ValTotal;
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
                                    pesoTotalIng = ValTotal;
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
                                        desviacionTotalCal = ValTotal;
                                        return ValTotal + "%";
                                    }                               
                            }

                        },
                    ],
                },
                onContentReady: function (e) {
                    DatosFiltradosTabla();
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

function DatosFiltradosTabla() {
    const filterExpr = $("#IngresosdeChatarras").dxDataGrid("instance").getCombinedFilter(true);
    $("#IngresosdeChatarras").dxDataGrid("instance").getDataSource()
        .store()
        .load({ filter: filterExpr })
        .then((result) => {
            valor = result;
        });
}

function DetalleChatarra(modelo) {
    var url = "../Chatarra/ConsultaDetalleIngresoChatarraSap?docEntry=" + modelo.DocEntry + "&tipo=" + modelo.Tipo_Ingreso;
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
                keyExpr: "DocEntry",
                showBorders: true,
                allowColumnReordering: false,
                filterRow: { visible: false },
                filterPanel: { visible: false },
                headerFilter: { visible: true },
                columnFixing: {
                    enabled: true
                },
               
                paging: {
                    pageSize: 10
                },
                pager: {
                    showPageSizeSelector: true,
                    allowedPageSizes: [5, 10, 100],
                    showInfo: true
                },             
                columns: [
                    { dataField: "DocEntry", visible: false },
                    //{ dataField: "CardCode", visible: false },                  
                    {
                        dataField: "ItemCode", caption: "# Item", allowEditing: false, fixed: false, allowFiltering: false
                    },
                    {
                        dataField: "Description", caption: "Descripcion", allowEditing: false, fixed: false, allowFiltering: false
                    },
                    {
                        dataField: "Cantidad", caption: "Cantidad", allowEditing: false, allowFiltering: false
                    },
                    {
                        dataField: "PesoTeoricoUnitario", caption: "Peso Teorico Unitario", allowEditing: false, fixed: false, allowFiltering: false,
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
                        dataField: "PesoTeoricoSubtotal", caption: "Peso Teorico Total", allowEditing: false, fixed: false, allowFiltering: false,
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
                        dataField: "PesoIngresado", caption: "Peso Ingresado", allowEditing: false, fixed: false, allowFiltering: false,
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
                        dataField: "Desviacion", caption: "Desviacion", allowEditing: false, fixed: false, allowFiltering: false,
                        format: {
                            type: "fixedPoint",
                            precision: 2,

                        },
                        customizeText: function (cellInfo) {
                            const noTruncarDecimales = { maximumFractionDigits: 2, minimumFractionDigits: 2 };
                            return (cellInfo.value).toLocaleString('en-US', noTruncarDecimales);
                        }
                    },
                    
                    
                ],
                summary: {
                    totalItems: [                      
                        {
                            name: "Cantidad",
                            column: "Cantidad",
                            summaryType: "sum",
                            displayFormat: "Cantidad Total",
                            showInColumn: "Cantidad",
                            customizeText: function (e) {
                                if (e.value != 0 && e.value != "") {
                                    $("#txtCantidadTotal").val(e.value);
                                    const noTruncarDecimales = { maximumFractionDigits: 0, minimumFractionDigits: 0 };
                                    ValTotal = (e.value).toLocaleString('en-US', noTruncarDecimales);
                                    return ValTotal;
                                }
                            }
                        }
                        , {
                            column: "PesoTeoricoSubtotal",
                            summaryType: "sum",
                            showInColumn: "PesoTeoricoSubtotal",
                            displayFormat: "Total: {0}",
                            valueFormat: "currency",
                            customizeText: function (e) {
                                if (e.value != 0 && e.value != "") {
                                    $("#txtPesoTeorico").val(e.value);

                                    const noTruncarDecimales = { maximumFractionDigits: 2, minimumFractionDigits: 2 };
                                    ValTotal = (e.value).toLocaleString('en-US', noTruncarDecimales);
                                    return ValTotal + "kg";
                                }
                            },

                        }
                        , {
                            column: "PesoIngresado",
                            summaryType: "sum",
                            showInColumn: "PesoIngresado",
                            displayFormat: "Total: {0}",
                            valueFormat: "currency",
                            customizeText: function (e) {
                                if (e.value != 0 && e.value != "") {
                                    $("#txtPesoIngresado").val(e.value);
                                    const noTruncarDecimales = { maximumFractionDigits: 2, minimumFractionDigits: 2 };
                                    ValTotal = (e.value).toLocaleString('en-US', noTruncarDecimales);
                                    return ValTotal + "kg";
                                }
                            },

                        },
                            {
                                column: "Desviacion",
                            summaryType: "sum",
                                showInColumn: "Desviacion",
                            displayFormat: "Total: {0}",
                            valueFormat: "currency",
                            customizeText: function (e) {
                                if (e.value != 0 && e.value != "") {
                                    var pesoTeorico = $("#txtPesoTeorico").val();
                                    var pesoIngresado = $("#txtPesoIngresado").val();
                                    var desviacion = null;
                                    var subtotal = (pesoIngresado / pesoTeorico) * 100;
                                    if (subtotal > 100) {
                                        desviacion = subtotal - 100;
                                    } else {
                                        desviacion = (100 - subtotal) * -1;
                                    }
                                    $("#txtDesviacionPromedioReporte").val(desviacion.toFixed(2));
                                    $("#txtDesviacionTotal").val(desviacion.toFixed(2));

                                    return "Prom: " + desviacion.toFixed(2) + "%";
                                }
                            }

                        },
                    ],
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
    $("#ModalDetalleChatarra").modal("show");

}

const monthNames = ["January", "February", "March", "April", "May", "June",
    "July", "August", "September", "October", "November", "December"
];

getLongMonthName = function (date) {
    return monthNames[date.getMonth()];
}

getStringMes = function (mes) {
    if (mes == 1) {
        return "Enero"
    }
    if (mes == 2) {
        return "Febrero"
    } if (mes == 3) {
        return "Marzo"
    } if (mes == 4) {
        return "Abril"
    } 
}

function ChartResumenesChatarras() {
    $("#txtCantidadTotalVen").val(cantidadTotalIng);
    $("#txtPesoTotalVen").val(pesoTotalIng);
    $("#txtDesviacionVen").val(desviacionTotalCal);

    $("#txtCantidadTotalTipCien").val(cantidadTotalIng);
    $("#txtPesoTotalTipCien").val(pesoTotalIng);
    $("#txtDesviacionTipCien").val(desviacionTotalCal);

    $("#txtCantidadTotalClientLin").val(cantidadTotalIng);
    $("#txtPesoTotalClientLin").val(pesoTotalIng);
    $("#txtDesviacionClientLin").val(desviacionTotalCal);

    $("#txtCantidadTotalClientClas").val(cantidadTotalIng);
    $("#txtPesoTotalClientClas").val(pesoTotalIng);
    $("#txtDesviacionClientClas").val(desviacionTotalCal);

    $("#txtCantidadTotalTipoIng").val(cantidadTotalIng);
    $("#txtPesoTotalTipoIng").val(pesoTotalIng);
    $("#txtDesviacionTipoIng").val(desviacionTotalCal);

    $("#txtCantidadTotalMes").val(cantidadTotalIng);
    $("#txtPesoTotalMes").val(pesoTotalIng);
    $("#txtDesviacionMes").val(desviacionTotalCal);

    graficoVendedores();
    graficoTipoClientes();
    graficoClienteLinea();
    graficoClienteClase();
    graficoTipoIngreso();
    graficoMeses();
    $("#ModalReporteChatarra").modal("show");
}

function tablaResumenVendedor(valor) {    
    const locale = getLocale();
    DevExpress.localization.locale(locale);
    $("#DetalleFinalChatarrasVendedor").dxDataGrid({
        dataSource: Object.values(valor),
        columnAutoWidth: true,
        showBorders: true,
        allowColumnReordering: false,
        filterRow: { visible: false },
        filterPanel: { visible: false },
        headerFilter: { visible: true },
        columnFixing: {
            enabled: true
        },
        paging: {
            pageSize: 5
        },
        pager: {
            showPageSizeSelector: true,
            allowedPageSizes: [5, 10, 100],
            showInfo: true
        },
        columns: [               
            {
                dataField: "Vendedor", caption: "Vendedor", allowEditing: false, fixed: false, allowFiltering: false
            },
            {
                dataField: "Cantidad", caption: "Cantidad", allowEditing: false, fixed: false, allowFiltering: false,
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
                dataField: "Peso_Real", caption: "Peso Real", allowEditing: false, fixed: false, allowFiltering: false,
                format: {
                    type: "fixedPoint",
                    precision: 2,

                },
                customizeText: function (cellInfo) {
                    const noTruncarDecimales = { maximumFractionDigits: 2, minimumFractionDigits: 2 };
                    return (cellInfo.value).toLocaleString('en-US', noTruncarDecimales);
                }
            },          
        ],
    });
    function getLocale() {
        const storageLocale = sessionStorage.getItem('locale');
        return storageLocale != null ? storageLocale : 'es';
    }
}
function tablaResumenTipoCliente(valor) {
    const locale = getLocale();
    DevExpress.localization.locale(locale);
    $("#DetalleFinalChatarrasTipoCliente").dxDataGrid({
        dataSource: Object.values(valor),
        columnAutoWidth: true,
        showBorders: true,
        allowColumnReordering: false,
        filterRow: { visible: false },
        filterPanel: { visible: false },
        headerFilter: { visible: true },
        columnFixing: {
            enabled: true
        },
        paging: {
            pageSize: 5
        },
        pager: {
            showPageSizeSelector: true,
            allowedPageSizes: [5, 10, 100],
            showInfo: true
        },
        columns: [
            {
                dataField: "Vendedor", caption: "Tipo Cliente", allowEditing: false, fixed: false, allowFiltering: false
            },
            {
                dataField: "Cantidad", caption: "Cantidad", allowEditing: false, fixed: false, allowFiltering: false,
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
                dataField: "Peso_Real", caption: "Peso Real", allowEditing: false, fixed: false, allowFiltering: false,
                format: {
                    type: "fixedPoint",
                    precision: 2,

                },
                customizeText: function (cellInfo) {
                    const noTruncarDecimales = { maximumFractionDigits: 2, minimumFractionDigits: 2 };
                    return (cellInfo.value).toLocaleString('en-US', noTruncarDecimales);
                }
            },
        ],
    });
    function getLocale() {
        const storageLocale = sessionStorage.getItem('locale');
        return storageLocale != null ? storageLocale : 'es';
    }
}
function tablaResumenClienteLinea(valor) {
    const locale = getLocale();
    DevExpress.localization.locale(locale);
    $("#DetalleFinalChatarrasClienteLinea").dxDataGrid({
        dataSource: Object.values(valor),
        columnAutoWidth: true,
        showBorders: true,
        allowColumnReordering: false,
        filterRow: { visible: false },
        filterPanel: { visible: false },
        headerFilter: { visible: true },
        columnFixing: {
            enabled: true
        },
        paging: {
            pageSize: 5
        },
        pager: {
            showPageSizeSelector: true,
            allowedPageSizes: [5, 10, 100],
            showInfo: true
        },
        columns: [
            {
                dataField: "Vendedor", caption: "Cliente Linea", allowEditing: false, fixed: false, allowFiltering: false
            },
            {
                dataField: "Cantidad", caption: "Cantidad", allowEditing: false, fixed: false, allowFiltering: false,
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
                dataField: "Peso_Real", caption: "Peso Real", allowEditing: false, fixed: false, allowFiltering: false,
                format: {
                    type: "fixedPoint",
                    precision: 2,

                },
                customizeText: function (cellInfo) {
                    const noTruncarDecimales = { maximumFractionDigits: 2, minimumFractionDigits: 2 };
                    return (cellInfo.value).toLocaleString('en-US', noTruncarDecimales);
                }
            },
        ],
    });
    function getLocale() {
        const storageLocale = sessionStorage.getItem('locale');
        return storageLocale != null ? storageLocale : 'es';
    }
}
function tablaResumenClienteClase(valor) {
    const locale = getLocale();
    DevExpress.localization.locale(locale);
    $("#DetalleFinalChatarrasClienteClase").dxDataGrid({
        dataSource: Object.values(valor),
        columnAutoWidth: true,
        showBorders: true,
        allowColumnReordering: false,
        filterRow: { visible: false },
        filterPanel: { visible: false },
        headerFilter: { visible: true },
        columnFixing: {
            enabled: true
        },
        paging: {
            pageSize: 5
        },
        pager: {
            showPageSizeSelector: true,
            allowedPageSizes: [5, 10, 100],
            showInfo: true
        },
        columns: [
            {
                dataField: "Vendedor", caption: "Cliente Clase", allowEditing: false, fixed: false, allowFiltering: false
            },
            {
                dataField: "Cantidad", caption: "Cantidad", allowEditing: false, fixed: false, allowFiltering: false,
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
                dataField: "Peso_Real", caption: "Peso Real", allowEditing: false, fixed: false, allowFiltering: false,
                format: {
                    type: "fixedPoint",
                    precision: 2,

                },
                customizeText: function (cellInfo) {
                    const noTruncarDecimales = { maximumFractionDigits: 2, minimumFractionDigits: 2 };
                    return (cellInfo.value).toLocaleString('en-US', noTruncarDecimales);
                }
            },
        ],
    });
    function getLocale() {
        const storageLocale = sessionStorage.getItem('locale');
        return storageLocale != null ? storageLocale : 'es';
    }
}
function tablaResumenTipoIngreso(valor) {
    const locale = getLocale();
    DevExpress.localization.locale(locale);
    $("#DetalleFinalChatarrasTipoIngreso").dxDataGrid({
        dataSource: Object.values(valor),
        columnAutoWidth: true,
        showBorders: true,
        allowColumnReordering: false,
        filterRow: { visible: false },
        filterPanel: { visible: false },
        headerFilter: { visible: true },
        columnFixing: {
            enabled: true
        },
        paging: {
            pageSize: 5
        },
        pager: {
            showPageSizeSelector: true,
            allowedPageSizes: [5, 10, 100],
            showInfo: true
        },
        columns: [
            {
                dataField: "Vendedor", caption: "Tipo Ingreso", allowEditing: false, fixed: false, allowFiltering: false
            },
            {
                dataField: "Cantidad", caption: "Cantidad", allowEditing: false, fixed: false, allowFiltering: false,
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
                dataField: "Peso_Real", caption: "Peso Real", allowEditing: false, fixed: false, allowFiltering: false,
                format: {
                    type: "fixedPoint",
                    precision: 2,

                },
                customizeText: function (cellInfo) {
                    const noTruncarDecimales = { maximumFractionDigits: 2, minimumFractionDigits: 2 };
                    return (cellInfo.value).toLocaleString('en-US', noTruncarDecimales);
                }
            },
        ],
    });
    function getLocale() {
        const storageLocale = sessionStorage.getItem('locale');
        return storageLocale != null ? storageLocale : 'es';
    }
}
function tablaResumenMeses(valor) {
    const locale = getLocale();
    DevExpress.localization.locale(locale);
    $("#DetalleFinalChatarrasMeses").dxDataGrid({
        dataSource: Object.values(valor),
        columnAutoWidth: true,
        showBorders: true,
        allowColumnReordering: false,
        filterRow: { visible: false },
        filterPanel: { visible: false },
        headerFilter: { visible: true },
        columnFixing: {
            enabled: true
        },
        paging: {
            pageSize: 5
        },
        pager: {
            showPageSizeSelector: true,
            allowedPageSizes: [5, 10, 100],
            showInfo: true
        },
        columns: [
            {
                dataField: "Vendedor", caption: "Mes", allowEditing: false, fixed: false, allowFiltering: false
            },
            {
                dataField: "Cantidad", caption: "Cantidad", allowEditing: false, fixed: false, allowFiltering: false,
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
                dataField: "Peso_Real", caption: "Peso Real", allowEditing: false, fixed: false, allowFiltering: false,
                format: {
                    type: "fixedPoint",
                    precision: 2,

                },
                customizeText: function (cellInfo) {
                    const noTruncarDecimales = { maximumFractionDigits: 2, minimumFractionDigits: 2 };
                    return (cellInfo.value).toLocaleString('en-US', noTruncarDecimales);
                }
            },
        ],
    });
    function getLocale() {
        const storageLocale = sessionStorage.getItem('locale');
        return storageLocale != null ? storageLocale : 'es';
    }
}
function graficoVendedores() {
    const noTruncarDecimales = { maximumFractionDigits: 2, minimumFractionDigits: 2 };

    if (char != null) {
        char.destroy();
    }
    //if (char2 != null) {
    //    char2.destroy();
    //}
    var groupByVendedor = function (miarray, prop) {
        return miarray.reduce(function (groups, item) {
            var val = item[prop];
            groups[val] = groups[val] || { Cantidad: 0, Peso_Real: 0, Vendedor: val };
            groups[val].Cantidad += item.Cantidad;
            groups[val].Peso_Real += item.Peso_Real;
            return groups;
        }, {});
    }
    var ctx = $("#myChart1");
    //var ctx2 = $("#myChart2");
    var nombreVendedor = [];
    var valorNombreVendedor = [];
    var valorKgVendedor = [];

    tablaResumenVendedor(groupByVendedor(valor, 'Vendedor'));

    for (var i in groupByVendedor(valor, 'Vendedor')) {
        nombreVendedor.push(groupByVendedor(valor, 'Vendedor')[i].Vendedor);
        valorNombreVendedor.push(groupByVendedor(valor, 'Vendedor')[i].Cantidad);
        valorKgVendedor.push(groupByVendedor(valor, 'Vendedor')[i].Peso_Real.toFixed(2));
    }
    var chartdata = {
        labels: nombreVendedor,
        datasets: [{
            label: "Cantidad:",
            //backgroundColor: color,
            //borderColor: color,
            borderWidth: 2,
            cubicInterpolationMode: 'monotone',
            backgroundColor: 'rgba(7,59,251,0.5)',// Color de fondo
            borderColor: 'rgba(7,59,251,0.5)',// Color del borde
            data: valorNombreVendedor,
            pointRadius: 3,
            pointHoverRadius: 4,
            pointHitRadius: 10,
            fill: false,
            yAxisID: 'A',

            //yAxisID: "left"

        },
            {
                label: "Peso:",
                //backgroundColor: color,
                //borderColor: color,
                borderWidth: 2,
                cubicInterpolationMode: 'monotone',
                backgroundColor: 'rgba(2,162,0,0.5)',// Color de fondo
                borderColor: 'rgba(2,162,0,0.5)',// Color del borde
                data: valorKgVendedor,
                pointRadius: 3,
                pointHoverRadius: 4,
                pointHitRadius: 10,
                fill: false,
                yAxisID: 'B',
                type: 'line',

               // yAxisID: "right",

            }
        ]
    };
    //var chartdata2 = {
    //    labels: nombreVendedor,
    //    datasets: [{
    //        label: "Peso:",
    //        //backgroundColor: color,
    //        //borderColor: color,
    //        borderWidth: 2,
    //        cubicInterpolationMode: 'monotone',
    //        backgroundColor: 'rgba(74,178,51,0.5)',// Color de fondo
    //        borderColor: 'rgba(74,178,51,0.5)',// Color del borde
    //        data: valorKgVendedor,
    //        pointRadius: 3,
    //        pointHoverRadius: 4,
    //        pointHitRadius: 10,
    //        fill: false
    //    }
    //    ]
    //};
    char = new Chart(ctx, {
        type: "bar",
        data: chartdata,
        options: {
            responsive: true,
            scales: {
                yAxes: [
                    {
                        id: 'A',
                        //type: 'linear',
                        position: 'left',
                        ticks: {
                            callback: function (valor, index, valores) {
                                return Number(valor).toFixed(0).replace(/\B(?=(\d{3})+(?!\d))/g, ',');
                            }
                        },
                        scaleLabel: {
                            display: true,
                            labelString: "Cantidades",
                            fontColor: "black"
                        }
                    }, {
                        id: 'B',
                        //type: 'linear',
                        position: 'right',
                        ticks: {
                            callback: function (valor, index, valores) {
                                return Number(valor).toFixed(0).replace(/\B(?=(\d{3})+(?!\d))/g, ',');
                            }
                        },
                        scaleLabel: {
                            display: true,
                            labelString: "Kilos",
                            fontColor: "black"
                        }
                    },
                ],
                xAxes: [{
                    scaleLabel: {
                        display: true,
                        labelString: "Detalle",
                        fontColor: "black"
                    }
                }],
            },
            interaction: {
                intersect: false,
            },
            tooltips: {
                callbacks: {
                    label: function (tooltipItem, chart) {
                        var datasetLabel = chart.datasets[tooltipItem.datasetIndex].label || '';
                        return datasetLabel + " " + (tooltipItem.yLabel).toLocaleString('en-US', noTruncarDecimales);
                    }
                }
            },
            title: {
                display: true,
                text: 'Cantidades chatarra',
                fontSize: 18,
            },
        }

    });
    //char2 = new Chart(ctx2, {
    //    type: "bar",
    //    data: chartdata2,
    //    options: {
    //        responsive: true,
    //        scales: {
    //            yAxes: [{
    //                ticks: {
    //                    callback: function (valor, index, valores) {
    //                        return Number(valor).toFixed(2).replace(/\B(?=(\d{3})+(?!\d))/g, ',');
    //                    }
    //                    //    stepSize: 5,                
    //                },
    //                scaleLabel: {
    //                    display: true,
    //                    labelString: "Resultados",
    //                    fontColor: "black"
    //                }
    //            }],
    //            xAxes: [{
    //                scaleLabel: {
    //                    display: true,
    //                    labelString: "Detalle",
    //                    fontColor: "black"
    //                }
    //            }],
    //        },
    //        tooltips: {
    //            callbacks: {
    //                label: function (tooltipItem, chart) {
    //                    var datasetLabel = chart.datasets[tooltipItem.datasetIndex].label || '';
    //                    return datasetLabel + " " + (tooltipItem.yLabel).toLocaleString('en-US', noTruncarDecimales) + " kg";
    //                }
    //            }
    //        },
    //        interaction: {
    //            intersect: false,
    //        },
    //        title: {
    //            display: true,
    //            text: 'Pesos Chatarra(kg)',
    //            fontSize: 18,
    //        },
    //    }
    //});
}

function graficoTipoClientes() {
    const noTruncarDecimales = { maximumFractionDigits: 2, minimumFractionDigits: 2 };

    var groupByVendedor = function (miarray, prop) {
        return miarray.reduce(function (groups, item) {
            var val = item[prop];
            groups[val] = groups[val] || { Cantidad: 0, Peso_Real: 0, Vendedor: val };
            groups[val].Cantidad += item.Cantidad;
            groups[val].Peso_Real += item.Peso_Real;
            return groups;
        }, {});
    }

    if (char3 != null) {
        char3.destroy();
    }
    //if (char4 != null) {
    //    char4.destroy();
    //}
    var ctx = $("#myChart3");
    //var ctx2 = $("#myChart4");
    var nombreVendedor = [];
    var valorNombreVendedor = [];
    var valorKgVendedor = [];

    tablaResumenTipoCliente(groupByVendedor(valor, 'Tipo_Cliente'));

    for (var i in groupByVendedor(valor, 'Tipo_Cliente')) {
        nombreVendedor.push(groupByVendedor(valor, 'Tipo_Cliente')[i].Vendedor);
        valorNombreVendedor.push(groupByVendedor(valor, 'Tipo_Cliente')[i].Cantidad);
        valorKgVendedor.push(groupByVendedor(valor, 'Tipo_Cliente')[i].Peso_Real.toFixed(2));
    }
    var chartdata = {
        labels: nombreVendedor,
        datasets: [{
            label: "Cantidad:",
            //backgroundColor: color,
            //borderColor: color,
            borderWidth: 2,
            cubicInterpolationMode: 'monotone',
            backgroundColor: 'rgba(7,59,251,0.5)',// Color de fondo
            borderColor: 'rgba(7,59,251,0.5)',// Color del borde
            data: valorNombreVendedor,
            pointRadius: 3,
            pointHoverRadius: 4,
            pointHitRadius: 10,
            fill: false,
            yAxisID: 'A',

            //yAxisID: "left"
        },
            {
                label: "Peso:",
                //backgroundColor: color,
                //borderColor: color,
                borderWidth: 2,
                cubicInterpolationMode: 'monotone',
                backgroundColor: 'rgba(2,162,0,0.5)',// Color de fondo
                borderColor: 'rgba(2,162,0,0.5)',// Color del borde
                data: valorKgVendedor,
                pointRadius: 3,
                pointHoverRadius: 4,
                pointHitRadius: 10,
                fill: false,
                yAxisID: 'B',
                type: 'line',

                // yAxisID: "right",

            }
        ]
    };
    //var chartdata2 = {
    //    labels: nombreVendedor,
    //    datasets: [{
    //        label: "Peso:",
    //        //backgroundColor: color,
    //        //borderColor: color,
    //        borderWidth: 2,
    //        cubicInterpolationMode: 'monotone',
    //        backgroundColor: 'rgba(74,178,51,0.5)',// Color de fondo
    //        borderColor: 'rgba(74,178,51,0.5)',// Color del borde
    //        data: valorKgVendedor,
    //        pointRadius: 3,
    //        pointHoverRadius: 4,
    //        pointHitRadius: 10,
    //        fill: false
    //    }
    //    ]
    //};
    char3 = new Chart(ctx, {
        type: "bar",
        data: chartdata,
        options: {
            responsive: true,
            scales: {
                yAxes: [
                    {
                        id: 'A',
                        //type: 'linear',
                        position: 'left',
                        ticks: {
                            callback: function (valor, index, valores) {
                                return Number(valor).toFixed(0).replace(/\B(?=(\d{3})+(?!\d))/g, ',');
                            }
                        },
                        scaleLabel: {
                            display: true,
                            labelString: "Cantidades",
                            fontColor: "black"
                        }
                    }, {
                        id: 'B',
                        //type: 'linear',
                        position: 'right',
                        ticks: {
                            callback: function (valor, index, valores) {
                                return Number(valor).toFixed(0).replace(/\B(?=(\d{3})+(?!\d))/g, ',');
                            }
                        },
                        scaleLabel: {
                            display: true,
                            labelString: "Kilos",
                            fontColor: "black"
                        }
                    },
                ],
                xAxes: [{
                    scaleLabel: {
                        display: true,
                        labelString: "Detalle",
                        fontColor: "black"
                    }
                }],
            },
            tooltips: {
                callbacks: {
                    label: function (tooltipItem, chart) {
                        var datasetLabel = chart.datasets[tooltipItem.datasetIndex].label || '';
                        return datasetLabel + " " + (tooltipItem.yLabel).toLocaleString('en-US', noTruncarDecimales);
                    }
                }
            },
            interaction: {
                intersect: false,
            },
            title: {
                display: true,
                text: 'Cantidades chatarra',
                fontSize: 18,
            },
        }

    });
    //char4 = new Chart(ctx2, {
    //    type: "bar",
    //    data: chartdata2,
    //    options: {
    //        responsive: true,
    //        scales: {
    //            yAxes: [{
    //                ticks: {
    //                    callback: function (valor, index, valores) {
    //                        return Number(valor).toFixed(2).replace(/\B(?=(\d{3})+(?!\d))/g, ',');
    //                    }
    //                    //    stepSize: 5,                
    //                },
    //                scaleLabel: {
    //                    display: true,
    //                    labelString: "Resultados",
    //                    fontColor: "black"
    //                }
    //            }],
    //            xAxes: [{
    //                scaleLabel: {
    //                    display: true,
    //                    labelString: "Detalle",
    //                    fontColor: "black"
    //                }
    //            }],
    //        },
    //        tooltips: {
    //            callbacks: {
    //                label: function (tooltipItem, chart) {
    //                    var datasetLabel = chart.datasets[tooltipItem.datasetIndex].label || '';
    //                    return datasetLabel + " " + (tooltipItem.yLabel).toLocaleString('en-US', noTruncarDecimales) + " kg";
    //                }
    //            }
    //        },
    //        interaction: {
    //            intersect: false,
    //        },
    //        title: {
    //            display: true,
    //            text: 'Pesos Chatarra(kg)',
    //            fontSize: 18,
    //        },
    //    }
    //});
}
function graficoClienteLinea() {
    const noTruncarDecimales = { maximumFractionDigits: 2, minimumFractionDigits: 2 };

    var groupByVendedor = function (miarray, prop) {
        return miarray.reduce(function (groups, item) {
            var val = item[prop];
            groups[val] = groups[val] || { Cantidad: 0, Peso_Real: 0, Vendedor: val };
            groups[val].Cantidad += item.Cantidad;
            groups[val].Peso_Real += item.Peso_Real;
            return groups;
        }, {});
    }

    if (char5 != null) {
        char5.destroy();
    }
    //if (char6 != null) {
    //    char6.destroy();
    //}
    var ctx = $("#myChart5");
    //var ctx2 = $("#myChart6");
    var nombreVendedor = [];
    var valorNombreVendedor = [];
    var valorKgVendedor = [];

    tablaResumenClienteLinea(groupByVendedor(valor, 'Cliente_Linea'));

    for (var i in groupByVendedor(valor, 'Cliente_Linea')) {
        nombreVendedor.push(groupByVendedor(valor, 'Cliente_Linea')[i].Vendedor);
        valorNombreVendedor.push(groupByVendedor(valor, 'Cliente_Linea')[i].Cantidad);
        valorKgVendedor.push(groupByVendedor(valor, 'Cliente_Linea')[i].Peso_Real.toFixed(2));
    }
    var chartdata = {
        labels: nombreVendedor,
        datasets: [{
            label: "Cantidad:",
            //backgroundColor: color,
            //borderColor: color,
            borderWidth: 2,
            cubicInterpolationMode: 'monotone',
            backgroundColor: 'rgba(7,59,251,0.5)',// Color de fondo
            borderColor: 'rgba(7,59,251,0.5)',// Color del borde
            data: valorNombreVendedor,
            pointRadius: 3,
            pointHoverRadius: 4,
            pointHitRadius: 10,
            fill: false,
            yAxisID: 'A',

            //yAxisID: "left"

        },
            {
                label: "Peso:",
                //backgroundColor: color,
                //borderColor: color,
                borderWidth: 2,
                cubicInterpolationMode: 'monotone',
                backgroundColor: 'rgba(2,162,0,0.5)',// Color de fondo
                borderColor: 'rgba(2,162,0,0.5)',// Color del borde
                data: valorKgVendedor,
                pointRadius: 3,
                pointHoverRadius: 4,
                pointHitRadius: 10,
                fill: false,
                yAxisID: 'B',
                type: 'line',

                // yAxisID: "right",

            }
        ]
    };
    //var chartdata2 = {
    //    labels: nombreVendedor,
    //    datasets: [{
    //        label: "Peso:",
    //        //backgroundColor: color,
    //        //borderColor: color,
    //        borderWidth: 2,
    //        cubicInterpolationMode: 'monotone',
    //        backgroundColor: 'rgba(74,178,51,0.5)',// Color de fondo
    //        borderColor: 'rgba(74,178,51,0.5)',// Color del borde
    //        data: valorKgVendedor,
    //        pointRadius: 3,
    //        pointHoverRadius: 4,
    //        pointHitRadius: 10,
    //        fill: false
    //    }
    //    ]
    //};
    char5 = new Chart(ctx, {
        type: "bar",
        data: chartdata,
        options: {
            responsive: true,
            scales: {
                yAxes: [
                    {
                        id: 'A',
                        //type: 'linear',
                        position: 'left',
                        ticks: {
                            callback: function (valor, index, valores) {
                                return Number(valor).toFixed(0).replace(/\B(?=(\d{3})+(?!\d))/g, ',');
                            }
                        },
                        scaleLabel: {
                            display: true,
                            labelString: "Cantidades",
                            fontColor: "black"
                        }
                    }, {
                        id: 'B',
                        //type: 'linear',
                        position: 'right',
                        ticks: {
                            callback: function (valor, index, valores) {
                                return Number(valor).toFixed(0).replace(/\B(?=(\d{3})+(?!\d))/g, ',');
                            }
                        },
                        scaleLabel: {
                            display: true,
                            labelString: "Kilos",
                            fontColor: "black"
                        }
                    },
                ],
                xAxes: [{
                    scaleLabel: {
                        display: true,
                        labelString: "Detalle",
                        fontColor: "black"
                    }
                }],
            },
            tooltips: {
                callbacks: {
                    label: function (tooltipItem, chart) {
                        var datasetLabel = chart.datasets[tooltipItem.datasetIndex].label || '';
                        return datasetLabel + " " + (tooltipItem.yLabel).toLocaleString('en-US', noTruncarDecimales);
                    }
                }
            },
            interaction: {
                intersect: false,
            },
            title: {
                display: true,
                text: 'Cantidades chatarra',
                fontSize: 18,
            },
        }

    });
//    char6 = new Chart(ctx2, {
//        type: "bar",
//        data: chartdata2,
//        options: {
//            responsive: true,
//            scales: {
//                yAxes: [{
//                    ticks: {
//                        callback: function (valor, index, valores) {
//                            return Number(valor).toFixed(2).replace(/\B(?=(\d{3})+(?!\d))/g, ',');
//                        }
//                        //    stepSize: 5,                
//                    },
//                    scaleLabel: {
//                        display: true,
//                        labelString: "Resultados",
//                        fontColor: "black"
//                    }
//                }],
//                xAxes: [{
//                    scaleLabel: {
//                        display: true,
//                        labelString: "Detalle",
//                        fontColor: "black"
//                    }
//                }],
//            },
//            tooltips: {
//                callbacks: {
//                    label: function (tooltipItem, chart) {
//                        var datasetLabel = chart.datasets[tooltipItem.datasetIndex].label || '';
//                        return datasetLabel + " " + (tooltipItem.yLabel).toLocaleString('en-US', noTruncarDecimales) + " kg";
//                    }
//                }
//            },
//            interaction: {
//                intersect: false,
//            },
//            title: {
//                display: true,
//                text: 'Pesos Chatarra(kg)',
//                fontSize: 18,
//            },
//        }
//    });
}
function graficoClienteClase() {
    const noTruncarDecimales = { maximumFractionDigits: 2, minimumFractionDigits: 2 };

    var groupByVendedor = function (miarray, prop) {
        return miarray.reduce(function (groups, item) {
            var val = item[prop];
            groups[val] = groups[val] || { Cantidad: 0, Peso_Real: 0, Vendedor: val };
            groups[val].Cantidad += item.Cantidad;
            groups[val].Peso_Real += item.Peso_Real;
            return groups;
        }, {});
    }

    if (char7 != null) {
        char7.destroy();
    }
    //if (char8 != null) {
    //    char8.destroy();
    //}
    var ctx = $("#myChart7");
    //var ctx2 = $("#myChart8");
    var nombreVendedor = [];
    var valorNombreVendedor = [];
    var valorKgVendedor = [];

    tablaResumenClienteClase(groupByVendedor(valor, 'Cliente_Clase'));

    for (var i in groupByVendedor(valor, 'Cliente_Clase')) {
        nombreVendedor.push(groupByVendedor(valor, 'Cliente_Clase')[i].Vendedor);
        valorNombreVendedor.push(groupByVendedor(valor, 'Cliente_Clase')[i].Cantidad);
        valorKgVendedor.push(groupByVendedor(valor, 'Cliente_Clase')[i].Peso_Real.toFixed(2));
    }
    var chartdata = {
        labels: nombreVendedor,
        datasets: [{
            label: "Cantidad:",
            //backgroundColor: color,
            //borderColor: color,
            borderWidth: 2,
            cubicInterpolationMode: 'monotone',
            backgroundColor: 'rgba(7,59,251,0.5)',// Color de fondo
            borderColor: 'rgba(7,59,251,0.5)',// Color del borde
            data: valorNombreVendedor,
            pointRadius: 3,
            pointHoverRadius: 4,
            pointHitRadius: 10,
            fill: false,
            yAxisID: 'A',

            //yAxisID: "left"

        },
            {
                label: "Peso:",
                //backgroundColor: color,
                //borderColor: color,
                borderWidth: 2,
                cubicInterpolationMode: 'monotone',
                backgroundColor: 'rgba(2,162,0,0.5)',// Color de fondo
                borderColor: 'rgba(2,162,0,0.5)',// Color del borde
                data: valorKgVendedor,
                pointRadius: 3,
                pointHoverRadius: 4,
                pointHitRadius: 10,
                fill: false,
                yAxisID: 'B',
                type: 'line',

                // yAxisID: "right",

            }
        ]
    };
    //var chartdata2 = {
    //    labels: nombreVendedor,
    //    datasets: [{
    //        label: "Peso:",
    //        //backgroundColor: color,
    //        //borderColor: color,
    //        borderWidth: 2,
    //        cubicInterpolationMode: 'monotone',
    //        backgroundColor: 'rgba(74,178,51,0.5)',// Color de fondo
    //        borderColor: 'rgba(74,178,51,0.5)',// Color del borde
    //        data: valorKgVendedor,
    //        pointRadius: 3,
    //        pointHoverRadius: 4,
    //        pointHitRadius: 10,
    //        fill: false
    //    }
    //    ]
    //};
    char7 = new Chart(ctx, {
        type: "bar",
        data: chartdata,
        options: {
            responsive: true,
            scales: {
                yAxes: [
                    {
                        id: 'A',
                        //type: 'linear',
                        position: 'left',
                        ticks: {
                            callback: function (valor, index, valores) {
                                return Number(valor).toFixed(0).replace(/\B(?=(\d{3})+(?!\d))/g, ',');
                            }
                        },
                        scaleLabel: {
                            display: true,
                            labelString: "Cantidades",
                            fontColor: "black"
                        }
                    }, {
                        id: 'B',
                        //type: 'linear',
                        position: 'right',
                        ticks: {
                            callback: function (valor, index, valores) {
                                return Number(valor).toFixed(0).replace(/\B(?=(\d{3})+(?!\d))/g, ',');
                            }
                        },
                        scaleLabel: {
                            display: true,
                            labelString: "Kilos",
                            fontColor: "black"
                        }
                    },
                ],
                xAxes: [{
                    scaleLabel: {
                        display: true,
                        labelString: "Detalle",
                        fontColor: "black"
                    }
                }],
            },
            tooltips: {
                callbacks: {
                    label: function (tooltipItem, chart) {
                        var datasetLabel = chart.datasets[tooltipItem.datasetIndex].label || '';
                        return datasetLabel + " " + (tooltipItem.yLabel).toLocaleString('en-US', noTruncarDecimales);
                    }
                }
            },
            interaction: {
                intersect: false,
            },
            title: {
                display: true,
                text: 'Cantidades chatarra',
                fontSize: 18,
            },
        }

    });
    //char8 = new Chart(ctx2, {
    //    type: "bar",
    //    data: chartdata2,
    //    options: {
    //        responsive: true,
    //        scales: {
    //            yAxes: [{
    //                ticks: {
    //                    callback: function (valor, index, valores) {
    //                        return Number(valor).toFixed(2).replace(/\B(?=(\d{3})+(?!\d))/g, ',');
    //                    }
    //                    //    stepSize: 5,                
    //                },
    //                scaleLabel: {
    //                    display: true,
    //                    labelString: "Resultados",
    //                    fontColor: "black"
    //                }
    //            }],
    //            xAxes: [{
    //                scaleLabel: {
    //                    display: true,
    //                    labelString: "Detalle",
    //                    fontColor: "black"
    //                }
    //            }],
    //        },
    //        tooltips: {
    //            callbacks: {
    //                label: function (tooltipItem, chart) {
    //                    var datasetLabel = chart.datasets[tooltipItem.datasetIndex].label || '';
    //                    return datasetLabel + " " + (tooltipItem.yLabel).toLocaleString('en-US', noTruncarDecimales) + " kg";
    //                }
    //            }
    //        },
    //        interaction: {
    //            intersect: false,
    //        },
    //        title: {
    //            display: true,
    //            text: 'Pesos Chatarra(kg)',
    //            fontSize: 18,
    //        },
    //    }
    //});
}
function graficoTipoIngreso() {
    const noTruncarDecimales = { maximumFractionDigits: 2, minimumFractionDigits: 2 };
    var groupByVendedor = function (miarray, prop) {
        return miarray.reduce(function (groups, item) {
            var val = item[prop];
            groups[val] = groups[val] || { Cantidad: 0, Peso_Real: 0, Vendedor: val };
            groups[val].Cantidad += item.Cantidad;
            groups[val].Peso_Real += item.Peso_Real;
            return groups;
        }, {});
    }

    if (char9 != null) {
        char9.destroy();
    }
    //if (char10 != null) {
    //    char10.destroy();
    //}
    var ctx = $("#myChart9");
    //var ctx2 = $("#myChart10");
    var nombreVendedor = [];
    var valorNombreVendedor = [];
    var valorKgVendedor = [];

    tablaResumenTipoIngreso(groupByVendedor(valor, 'Tipo_Ingreso'));

    for (var i in groupByVendedor(valor, 'Tipo_Ingreso')) {
        nombreVendedor.push(groupByVendedor(valor, 'Tipo_Ingreso')[i].Vendedor);
        valorNombreVendedor.push(groupByVendedor(valor, 'Tipo_Ingreso')[i].Cantidad);
        valorKgVendedor.push(groupByVendedor(valor, 'Tipo_Ingreso')[i].Peso_Real.toFixed(2));
    }
    var chartdata = {
        labels: nombreVendedor,
        datasets: [{
            label: "Cantidad:",
            //backgroundColor: color,
            //borderColor: color,
            borderWidth: 2,
            cubicInterpolationMode: 'monotone',
            backgroundColor: 'rgba(7,59,251,0.5)',// Color de fondo
            borderColor: 'rgba(7,59,251,0.5)',// Color del borde
            data: valorNombreVendedor,
            pointRadius: 3,
            pointHoverRadius: 4,
            pointHitRadius: 10,
            fill: false,
            yAxisID: 'A',

            //yAxisID: "left"

        },
            {
                label: "Peso:",
                //backgroundColor: color,
                //borderColor: color,
                borderWidth: 2,
                cubicInterpolationMode: 'monotone',
                backgroundColor: 'rgba(2,162,0,0.5)',// Color de fondo
                borderColor: 'rgba(2,162,0,0.5)',// Color del borde
                data: valorKgVendedor,
                pointRadius: 3,
                pointHoverRadius: 4,
                pointHitRadius: 10,
                fill: false,
                yAxisID: 'B',
                type: 'line',

                // yAxisID: "right",

            }
        ]
    };
    //var chartdata2 = {
    //    labels: nombreVendedor,
    //    datasets: [{
    //        label: "Peso:",
    //        //backgroundColor: color,
    //        //borderColor: color,
    //        borderWidth: 2,
    //        cubicInterpolationMode: 'monotone',
    //        backgroundColor: 'rgba(74,178,51,0.5)',// Color de fondo
    //        borderColor: 'rgba(74,178,51,0.5)',// Color del borde
    //        data: valorKgVendedor,
    //        pointRadius: 3,
    //        pointHoverRadius: 4,
    //        pointHitRadius: 10,
    //        fill: false
    //    }
    //    ]
    //};
    char9 = new Chart(ctx, {
        type: "bar",
        data: chartdata,
        options: {
            responsive: true,
            scales: {
                yAxes: [
                    {
                        id: 'A',
                        //type: 'linear',
                        position: 'left',
                        ticks: {
                            callback: function (valor, index, valores) {
                                return Number(valor).toFixed(0).replace(/\B(?=(\d{3})+(?!\d))/g, ',');
                            }
                        },
                        scaleLabel: {
                            display: true,
                            labelString: "Cantidades",
                            fontColor: "black"
                        }
                    }, {
                        id: 'B',
                        //type: 'linear',
                        position: 'right',
                        ticks: {
                            callback: function (valor, index, valores) {
                                return Number(valor).toFixed(0).replace(/\B(?=(\d{3})+(?!\d))/g, ',');
                            }
                        },
                        scaleLabel: {
                            display: true,
                            labelString: "Kilos",
                            fontColor: "black"
                        }
                    },
                ],
                xAxes: [{
                    scaleLabel: {
                        display: true,
                        labelString: "Detalle",
                        fontColor: "black"
                    }
                }],
            },
            tooltips: {
                callbacks: {
                    label: function (tooltipItem, chart) {
                        var datasetLabel = chart.datasets[tooltipItem.datasetIndex].label || '';
                        return datasetLabel + " " + (tooltipItem.yLabel).toLocaleString('en-US', noTruncarDecimales);
                    }
                }
            },
            interaction: {
                intersect: false,
            },
            title: {
                display: true,
                text: 'Cantidades chatarra',
                fontSize: 18,
            },
        }

    });
    //char10 = new Chart(ctx2, {
    //    type: "bar",
    //    data: chartdata2,
    //    options: {
    //        responsive: true,
    //        scales: {
    //            yAxes: [{
    //                ticks: {
    //                    callback: function (valor, index, valores) {
    //                        return Number(valor).toFixed(2).replace(/\B(?=(\d{3})+(?!\d))/g, ',');
    //                    }
    //                    //    stepSize: 5,                
    //                },
    //                scaleLabel: {
    //                    display: true,
    //                    labelString: "Resultados",
    //                    fontColor: "black"
    //                }
    //            }],
    //            xAxes: [{
    //                scaleLabel: {
    //                    display: true,
    //                    labelString: "Detalle",
    //                    fontColor: "black"
    //                }
    //            }],
    //        },
    //        tooltips: {
    //            callbacks: {
    //                label: function (tooltipItem, chart) {
    //                    var datasetLabel = chart.datasets[tooltipItem.datasetIndex].label || '';
    //                    return datasetLabel + " " + (tooltipItem.yLabel).toLocaleString('en-US', noTruncarDecimales) + " kg";
    //                }
    //            }
    //        },
    //        interaction: {
    //            intersect: false,
    //        },
    //        title: {
    //            display: true,
    //            text: 'Pesos Chatarra(kg)',
    //            fontSize: 18,
    //        },
    //    }
    //});
}
function graficoMeses() {
    const noTruncarDecimales = { maximumFractionDigits: 2, minimumFractionDigits: 2 };

    var groupByVendedor = function (miarray, prop) {
        return miarray.reduce(function (groups, item) {
            
            var val = item[prop];
            if (val == 1) {
                val = "Enero";
            } if (val == 2) {
                val = "Febrero";
            } if (val == 3) {
                val = "Marzo";
            } if (val == 4) {
                val = "Abril";
            } if (val == 5) {
                val = "Mayo";
            } if (val == 6) {
                val = "Junio";
            } if (val == 7) {
                val = "Julio";
            } if (val == 8) {
                val = "Agosto";
            } if (val == 9) {
                val = "Septiembre";
            } if (val == 10) {
                val = "Octubre";
            } if (val == 11) {
                val = "Noviembre";
            } if (val == 12) {
                val = "Diciembre";
            }
            groups[val] = groups[val] || { Cantidad: 0, Peso_Real: 0, Vendedor: val };
            groups[val].Cantidad += item.Cantidad;
            groups[val].Peso_Real += item.Peso_Real;
            return groups;
        }, {});
    }

    if (char11 != null) {
        char11.destroy();
    }
    //if (char12 != null) {
    //    char12.destroy();
    //}
    var ctx = $("#myChart11");
    //var ctx2 = $("#myChart12");
    var nombreVendedor = [];
    var valorNombreVendedor = [];
    var valorKgVendedor = [];

    tablaResumenMeses(groupByVendedor(valor, 'FechaRegistro2'));

    for (var i in groupByVendedor(valor, 'FechaRegistro2')) {
        nombreVendedor.push(groupByVendedor(valor, 'FechaRegistro2')[i].Vendedor);
        valorNombreVendedor.push(groupByVendedor(valor, 'FechaRegistro2')[i].Cantidad);
        valorKgVendedor.push(groupByVendedor(valor, 'FechaRegistro2')[i].Peso_Real.toFixed(2));
    }
    var chartdata = {
        labels: nombreVendedor,
        datasets: [{
            label: "Cantidad:",
            //backgroundColor: color,
            //borderColor: color,
            borderWidth: 2,
            cubicInterpolationMode: 'monotone',
            backgroundColor: 'rgba(7,59,251,0.5)',// Color de fondo
            borderColor: 'rgba(7,59,251,0.5)',// Color del borde
            data: valorNombreVendedor,
            pointRadius: 3,
            pointHoverRadius: 4,
            pointHitRadius: 10,
            fill: false,
            yAxisID: 'A',

            //yAxisID: "left"

        },
            {
                label: "Peso:",
                //backgroundColor: color,
                //borderColor: color,
                borderWidth: 2,
                cubicInterpolationMode: 'monotone',
                backgroundColor: 'rgba(2,162,0,0.5)',// Color de fondo
                borderColor: 'rgba(2,162,0,0.5)',// Color del borde
                data: valorKgVendedor,
                pointRadius: 3,
                pointHoverRadius: 4,
                pointHitRadius: 10,
                fill: false,
                yAxisID: 'B',
                type: 'line',

                // yAxisID: "right",

            }
        
        ]
    };
    //var chartdata2 = {
    //    labels: nombreVendedor,
    //    datasets: [{
    //        label: "Peso:",
    //        //backgroundColor: color,
    //        //borderColor: color,
    //        borderWidth: 2,
    //        cubicInterpolationMode: 'monotone',
    //        backgroundColor: 'rgba(74,178,51,0.5)',// Color de fondo
    //        borderColor: 'rgba(74,178,51,0.5)',// Color del borde
    //        data: valorKgVendedor,
    //        pointRadius: 3,
    //        pointHoverRadius: 4,
    //        pointHitRadius: 10,
    //        fill: false
    //    }
    //        ,
    //        {
    //            label: "Cantidad:",
    //            //backgroundColor: color,
    //            //borderColor: color,
    //            borderWidth: 2,
    //            cubicInterpolationMode: 'monotone',
    //            backgroundColor: 'rgba(241,11,36,0.5)',// Color de fondo
    //            borderColor: 'rgba(214,11,36,0.5)',// Color del borde
    //            data: valorKgVendedor,
    //            pointRadius: 3,
    //            pointHoverRadius: 4,
    //            pointHitRadius: 10,
    //            fill: false,
    //            type: 'line',

    //        }
    //    ]
    //};
    char11 = new Chart(ctx, {
        type: "bar",
        data: chartdata,
        options: {
            responsive: true,
            scales: {
                yAxes: [
                    {
                        id: 'A',
                        //type: 'linear',
                        position: 'left',
                        ticks: {
                            callback: function (valor, index, valores) {
                                return Number(valor).toFixed(0).replace(/\B(?=(\d{3})+(?!\d))/g, ',');
                            }
                        },
                        scaleLabel: {
                            display: true,
                            labelString: "Cantidades",
                            fontColor: "black"
                        }
                    }, {
                        id: 'B',
                        //type: 'linear',
                        position: 'right',
                        ticks: {
                            callback: function (valor, index, valores) {
                                return Number(valor).toFixed(0).replace(/\B(?=(\d{3})+(?!\d))/g, ',');
                            }
                        },
                        scaleLabel: {
                            display: true,
                            labelString: "Kilos",
                            fontColor: "black"
                        }
                    },
                ],
                xAxes: [{
                    scaleLabel: {
                        display: true,
                        labelString: "Detalle",
                        fontColor: "black"
                    }
                }],
            },
            tooltips: {
                callbacks: {
                    label: function (tooltipItem, chart) {
                        var datasetLabel = chart.datasets[tooltipItem.datasetIndex].label || '';
                        return datasetLabel + " " + (tooltipItem.yLabel).toLocaleString('en-US', noTruncarDecimales);
                    }
                }
            },
            interaction: {
                intersect: false,
            },
            title: {
                display: true,
                text: 'Cantidades chatarra',
                fontSize: 18,
            },
        }

    });

    //char12 = new Chart(ctx2, {
    //    type: "bar",
    //    data: chartdata2,
    //    options: {
    //        responsive: true,          
    //        scales: {
    //            yAxes: [{
    //                ticks: {
    //                    callback: function (valor, index, valores) {
    //                        return Number(valor).toFixed(2).replace(/\B(?=(\d{3})+(?!\d))/g, ',');
    //                    }
    //                    //    stepSize: 5,                
    //                },
    //                scaleLabel: {
    //                    display: true,
    //                    labelString: "Resultados",
    //                    fontColor: "black"
    //                }
    //            }],
    //            xAxes: [{
    //                scaleLabel: {
    //                    display: true,
    //                    labelString: "Detalle",
    //                    fontColor: "black"
    //                }
    //            }],
    //        },
    //        tooltips: {
    //            callbacks: {
    //                label: function (tooltipItem, chart) {
    //                    var datasetLabel = chart.datasets[tooltipItem.datasetIndex].label || '';
    //                    return datasetLabel + " " + (tooltipItem.yLabel).toLocaleString('en-US', noTruncarDecimales)+" kg";
    //                }
    //            }
    //        },
    //        interaction: {
    //            intersect: false,
    //        },
    //        title: {
    //            display: true,
    //            text: 'Pesos Chatarra(kg)',
    //            fontSize: 18,
    //        },
    //    }
    //});
}
