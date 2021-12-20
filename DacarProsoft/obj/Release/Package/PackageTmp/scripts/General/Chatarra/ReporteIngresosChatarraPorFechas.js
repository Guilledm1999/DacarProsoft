$(document).ready(function () {
    $(".loading-icon").css("display", "none");
    $(document).on('click', '.fa', function (event) {
        event.preventDefault();
        $(this).closest('tr').remove();
    });
});

$('#LinkClose').on("click", function (e) {
    $("#MensajeErrorInesperado").hide('fade');
});

$('#LinkClose2').on("click", function (e) {
    $("#MensajeIngreseTodosLosCampos").hide('fade');
});

$('#LinkClose3').on("click", function (e) {
    $("#MensajeSinInformacion").hide('fade');
});


function ConsultarReporte() {
    var txtFechaInicio = $("#txtFechaInicio").val();
    var txtFechaFin = $("#txtFechaFin").val();

    if (txtFechaInicio == null || txtFechaFin == null) {
        $(".btn").attr("disabled", false);
        $(".btn-txt").text("Consultar");
        $("#MensajeIngreseTodosLosCampos").show('fade');
        setTimeout(function () {
            $("#MensajeIngreseTodosLosCampos").fadeOut(1500);
        }, 3000);
    }

    else {
        ConsultarDatosChatarra();
    }
}


function ConsultarDatosChatarra() {
    var txtFechaInicio = $("#txtFechaInicio").val();
    var txtFechaFin = $("#txtFechaFin").val();
    var txtOpcionFiltro = $("#OpcionFiltrado option:selected").val();

    $.ajax({
        url: "../Chatarra/ConsultaDatosIngresosChatarra?anioInicial=" + txtFechaInicio + " &anioFinal=" + txtFechaFin + " &OpcionFiltrado=" + txtOpcionFiltro,
        type: "GET"
        , success: function (msg) {
            if (msg.length != 0) {
                console.log(msg);
                $(".btn").attr("disabled", false);
                $(".btn-txt").text("Consultar");
                $("#IngresosdeChatarras").dxDataGrid({
                    dataSource: msg,
                    showBorders: true,
                    columnAutoWidth: true,
                    showBorders: true,
                  
                    export: {
                        enabled: true,
                    },
                    
                    searchPanel: {
                        visible: true,
                        width: 240,
                        placeholder: "Buscar..."
                    },
                    headerFilter: {
                        visible: true
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
                            {
                            dataField: "NumeroDocumento", caption: "Numero Documento", allowHeaderFiltering: true, allowSearch: false
                            }, 
                            {
                                dataField: "Identificacion", caption: "Identificacion", visible:false
                            },
                            {
                            dataField: "Cliente", caption: "Cliente", allowHeaderFiltering: false, allowSearch: true
                            }, {
                                dataField: "GroupName", caption: "Grupo Cliente", allowHeaderFiltering: true, allowSearch: false
                            },
                            {
                                dataField: "ClienteLinea", caption: "Cliente Linea", allowHeaderFiltering: true, allowSearch: false
                            },
                            {
                                dataField: "ClienteClase", caption: "Cliente Clase", allowHeaderFiltering: true, allowSearch: false
                            }, {
                                dataField: "TipoIngreso", caption: "Tipo Ingreso", allowHeaderFiltering: false, allowSearch: false
                            },
                           
                            {
                                dataField: "BodegaId", caption: "Bodega", allowHeaderFiltering: true, allowSearch: false
                            },
                            {
                                dataField: "Comentarios", caption: "Comentarios", allowHeaderFiltering: false, allowSearch: false
                            },
                            {
                                dataField: "Descripcion", caption: "Descripcion", allowHeaderFiltering: false, allowSearch: false
                            },
                            {
                                dataField: "Cantidad", caption: "Cantidad", allowHeaderFiltering: false, allowSearch: false
                        },
                        {
                            dataField: "PesoTeoricoTotalAjustado", caption: "Peso Teorico Total", allowHeaderFiltering: false, allowSearch: false
                        },
                            {
                                dataField: "PesoTotalAjustado", caption: "Peso Total", allowHeaderFiltering: false, allowSearch: false
                        },
                        {
                            dataField: "DiferenciaPesos", caption: "Diferencia Kg", allowHeaderFiltering: false, allowSearch: false
                        },
                            {
                                dataField: "Fecha", caption: "Fecha Documento", allowHeaderFiltering: false, allowSearch: false
                        },
                        {
                            dataField: "MesRegistro", caption: "Mes Registro", allowHeaderFiltering: true, allowSearch: false
                        },
                            {
                                dataField: "FechaRegistro", caption: "Fecha Registro", allowHeaderFiltering: false, allowSearch: false
                            },
                        ],
                    summary: {
                        totalItems: [
                            {
                                name: "Descripcion",
                                column: "Descripcion",
                                summaryType: "count",
                                showInColumn: "Descripcion",
                                customizeText: function (e) {
                                    if (e.value != 0 && e.value != "") {
                                        return "Totales: "
                                    }
                                }
                            },
                            {
                                name: "Cantidad",
                                column: "Cantidad",
                                summaryType: "sum",
                                showInColumn: "Cantidad",
                                customizeText: function (e) {
                                    if (e.value != 0 && e.value != "") {
                                        return  (e.value)
                                    }
                                }
                            },
                            {
                                name: "PesoTeoricoTotalAjustado",
                                column: "PesoTeoricoTotalAjustado",
                                summaryType: "sum",
                                showInColumn: "PesoTeoricoTotalAjustado",
                                customizeText: function (e) {
                                    if (e.value != 0 && e.value != "") {
                                        return (e.value).toFixed(2)+" kg";
                                    }
                                }
                            },
                            {
                                name: "PesoTotalAjustado",
                                column: "PesoTotalAjustado",
                                summaryType: "sum",
                                showInColumn: "PesoTotalAjustado",
                                customizeText: function (e) {
                                    if (e.value != 0 && e.value != "") {
                                        return (e.value).toFixed(2) + " kg";
                                    }
                                }
                            }                      ],
                    }
                });
                ConsultarFechaDos(msg);
            } else {

                $(".btn").attr("disabled", false);
                $(".btn-txt").text("Consultar");

                $("#MensajeSinInformacion").show('fade');
                setTimeout(function () {
                    $("#MensajeSinInformacion").fadeOut(1500);
                }, 3000); return;


            }

        },
        error: function (msg) {
            $(".btn").attr("disabled", false);
            $(".btn-txt").text("Consultar");
            $("#MensajeErrorInesperado").show('fade');
            setTimeout(function () {
                $("#MensajeErrorInesperado").fadeOut(1500);
            }, 3000); return;
        }
    })
}