var idPacking = null;
$(document).ready(function () {
    mostrarIngresosPallet();
    $(".loading-icon").css("display", "none");
    $(document).on('click', '.fa', function (event) {
        event.preventDefault();
        $(this).closest('tr').remove();
    });
    $("#image").removeClass("hide");
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

function ConsultarIngresosPacking() {
    $(".result").text("");
    $(".loading-icon").removeClass("hide");
    $(".btn").attr("disabled", true);
    $(".btn-txt").text("Espere...");
    mostrarIngresosPallet();
}

function mostrarIngresosPallet() {
    var valor = $("#TipoBusqueda").val();
    $.ajax({
        url: "../PackingList/ObtenerPalletIngresadosComext",
        type: "GET"
        , success: function (msg) {
            $("#tblIngresosdePacking").dxDataGrid({
                dataSource: msg,
                keyExpr: 'PackingId',
                showBorders: true,
                columnAutoWidth: true,
                showBorders: true,
                showColumnLines: true,
                showRowLines: true,
                rowAlternationEnabled: false,
                allowColumnReordering: true,
                allowColumnResizing: false,
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
                //customizeColumns(columns) {
                //    columns[0].width = 70;
                //},
                loadPanel: {
                    enabled: false,
                },
                scrolling: {
                    mode: 'infinite',
                },
                export: {
                    enabled: true,
                    allowExportSelectedData: false
                },
                onExporting: function (e) {
                    var workbook = new ExcelJS.Workbook();
                    var worksheet = workbook.addWorksheet('Ingresos Pruebas');

                    DevExpress.excelExporter.exportDataGrid({
                        component: e.component,
                        worksheet: worksheet,
                        autoFilterEnabled: true
                    }).then(function () {
                        workbook.xlsx.writeBuffer().then(function (buffer) {
                            saveAs(new Blob([buffer], { type: 'application/octet-stream' }), 'IngresosPruebasLaboratorio.xlsx');
                        });
                    });
                    e.cancel = true;
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
                        dataField: "Mes", caption: "Mes", allowEditing: false, headerFilter: true, allowHeaderFiltering: true
                    },
                    {
                        dataField: "Origen", caption: "Origen", allowEditing: false, headerFilter: true, allowHeaderFiltering: true
                    },
                    {
                        dataField: "Destino", caption: "Destino", allowEditing: false, headerFilter: true, allowHeaderFiltering: true
                    },
                    {
                        dataField: "CantidadPallet", visible: false
                    }, {
                        dataField: "PalletFaltantes", visible: false
                    },
                    {
                        dataField: "Estado", caption: "Estado Packing List",  allowEditing: false
                    },
                   
                    {
                        dataField: "DetalleIngresado", caption: "Estado Orden",  allowEditing: false
                    },
                    {
                        dataField: "FechaRegistro", caption: "Fecha Registro", allowEditing: false, allowHeaderFiltering: true ,dataType: 'date',
                    },
                    {
                        caption: "Acciones",
                        cellTemplate: function (container, options) {
                            var btnDetalle = "<button class='btn-primary' onclick='generarInformePackingListPDF(" + JSON.stringify(options.data) + ")'>Imprimir</button>";

                            $("<div>")
                                .append($(btnDetalle))
                                .appendTo(container);
                        }
                    }

                ],
            });

            $(".btn").attr("disabled", false);
            $(".btn-txt").text("Consultar");

        },
        error: function (msg) {
            $("#MensajeErrorGeneral").show('fade');
            setTimeout(function () {
                $("#MensajeErrorGeneral").fadeOut(1500);
            }, 3000);
        }
    })
}


function generarInformePackingListPDF(modelo) {
    if (modelo.DetalleIngresado == "Incompleto" || modelo.Estado == "Incompleto") {
        $("#MensajePackingSinDetalle").show('fade');
        setTimeout(function () {
            $("#MensajePackingSinDetalle").fadeOut(1500);
        }, 3000);
    } else {
        idPacking = modelo.PackingId;
        $("#ModalAfirmacionFondoPacking").modal("show");
    }
}

$('#AfirmacionEticketaPacking').on("click", function (e) {
    etiqueta = "NO";
    generarPDFPackingList(etiqueta);
    $("#ModalAfirmacionFondoPacking").modal("hide");
});

$('#NgacionEticketaPacking').on("click", function (e) {
    etiqueta = "SI";
    generarPDFPackingList(etiqueta);
    $("#ModalAfirmacionFondoPacking").modal("hide");
});

function generarPDFPackingList(variable) {
    var url = "../PackingList/InformePackingList?PackingId=" + idPacking + "&Fondo=" + variable;
    window.open(url);
    $("#ModalListadoDePallets").modal("hide");
}