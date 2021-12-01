$(document).ready(function () {

    RevisionesTecnicas();

    $(".loading-icon").css("display", "none");
    $(document).on('click', '.fa', function (event) {
        event.preventDefault();
        $(this).closest('tr').remove();
    });
//    $("#image").removeClass("hide");
});

$('#LinkClose').on("click", function (e) {
    $("#MensajeErrorInesperado").hide('fade');
});
$('#LinkClose2').on("click", function (e) {
    $("#MensajeIngreseTodosLosCampos").hide('fade');
});


function ConsultarReporte() {
    $(".result").text("");
    $(".loading-icon").removeClass("hide");
    $(".btn").attr("disabled", true);
    $(".btn-txt").text("Espere...");
    //var txtFechaInicio = document.getElementById('txtFechaInicio').value;
    //var txtFechaFin = document.getElementById('txtFechaFin').value;

    var txtFechaInicio = $("#txtFechaInicio").val();
    var txtFechaFin = $("#txtFechaFin").val();

    if (txtFechaInicio.length == 0 && txtFechaFin.length == 0) {
        $(".btn").attr("disabled", false);
        $(".btn-txt").text("Consultar");
        $("#MensajeIngreseTodosLosCampos").show('fade');
        setTimeout(function () {
            $("#MensajeIngreseTodosLosCampos").fadeOut(1500);
        }, 3000); return;
    }
    else {
        RevisionesTecnicas();
    }
}

function RevisionesTecnicas() {
    //var fechaInicio = $('#txtFechaInicio').val();
    //var fechaFin = $('#txtFechaFin').val();
    $.ajax({
        url: "../Garantias/ConsultarRevisionesTecnica"/*?FechaInicio=" + fechaInicio + "&FechaFin=" + fechaFin*/,
        type: "GET"
        , success: function (msg) {
            //ItemsPedidoCliente = msg;  
            $("#tblReporteRevisionesTecnicas").dxDataGrid({
                dataSource: msg,
                keyExpr: 'IngresoRevisionGarantiaId',
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
                //searchPanel: {
                //    visible: true,
                //    width: 240,
                //    placeholder: "Buscar..."
                //},
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
                headerFilter: {
                    visible: true
                },
                //pager: {
                //    visible: true,
                //    allowedPageSizes: [5, 10, 50],
                //    showPageSizeSelector: true,
                //    showInfo: true,
                //    showNavigationButtons: true
                //},
                export: {
                    enabled: true,
                    allowExportSelectedData: true
                },

                onExporting: function (e) {
                    var workbook = new ExcelJS.Workbook();
                    var worksheet = workbook.addWorksheet('Reporte Control');

                    DevExpress.excelExporter.exportDataGrid({
                        component: e.component,
                        worksheet: worksheet,
                        autoFilterEnabled: true
                    }).then(function () {
                        workbook.xlsx.writeBuffer().then(function (buffer) {
                            saveAs(new Blob([buffer], { type: 'application/octet-stream' }), 'RevisionesTecnicas.xlsx');
                        });
                    });
                    e.cancel = true;
                },
                columns: [

                    { dataField: "IngresoRevisionGarantiaId", visible: false },
                    {
                        dataField: "Cedula", caption: "Cedula", alignment: "left", headerFilter: {
                            allowSearch: true
                        }
                    },
                    {
                        dataField: "Cliente", caption: "Cliente", alignment: "left"
                    },
                    {
                        dataField: "NumeroGarantia", caption: "Numero Garantia", alignment: "left"
                    },
                    {
                        dataField: "NumeroComprobante", caption: "Numero Comprobante", alignment: "left"
                    },
                    {
                        dataField: "NumeroRevision", caption: "Numero Factura", alignment: "left"
                    },
                    {
                        dataField: "Provincia", caption: "Provincia", alignment: "left", allowFiltering: false
                    },
                    {
                        dataField: "Direccion", caption: "Direccion", alignment: "left"
                    },
                    {
                        dataField: "Vendedor", caption: "Vendedor", alignment: "left"
                    },
                    {
                        dataField: "FacturaCliente", caption: "Numero Garantia", alignment: "left", allowFiltering: false, visible: false
                    },
                    {
                        dataField: "TestBateria", caption: "Numero Garantia", alignment: "left", allowFiltering: false, visible: false
                    },
                    {
                        dataField: "Marca", caption: "Marca", alignment: "left", allowFiltering: false, visible: false
                    },
                    {
                        dataField: "Modelo", caption: "Modelo", alignment: "left", allowFiltering: false
                    },
                    {
                        dataField: "Lote", caption: "Lote", alignment: "left", allowFiltering: false, visible: false
                    },
                    {
                        dataField: "Prorrateo", caption: "Prorrateo", alignment: "left", allowFiltering: false, visible: false
                    },
                    {
                        dataField: "Meses", caption: "Meses", alignment: "left", allowFiltering: false, visible: false
                    },
                    {
                        dataField: "PorcentajeVenta", caption: "Porcentaje Venta", alignment: "left", allowFiltering: false, visible: false
                    },
                    {
                        dataField: "Voltaje", caption: "Voltaje", alignment: "left", allowFiltering: false, visible: false
                    },
                    {
                        dataField: "ModoIngreso", caption: "Modo Ingreso", alignment: "left", allowFiltering: false, visible: false
                    },
                    {
                        dataField: "AplicaGarantia", caption: "Aplica Garantia", alignment: "left"
                    },
                    {
                        dataField: "FechaVenta", caption: "Fecha Venta", alignment: "left", dataType: "date"
                    },
                    {
                        dataField: "FechaIngreso", caption: "Fecha Ingreso", alignment: "left", dataType: "date"
                    },
                    {
                        caption: "Acciones",

                        cellTemplate: function (container, options) {

                            var btnFactura = "<button class='btn-primary' onclick='ModalObtenerFactura(" + JSON.stringify(options.data) + ")'>Factura</button>";
                            var lblEspacio = "<a> </a>"
                            var btnTest = "<button class='btn-primary' onclick='ModalObtenerTest(" + JSON.stringify(options.data) + ")'>Test</button>";
                            var btnDetalle = "<button class='btn-success' onclick='ModalObtenerDetalle(" + JSON.stringify(options.data) + ")'>Detalle</button>";



                            $("<div>")
                                .append($(btnFactura), $(lblEspacio), $(btnTest), $(lblEspacio), $(btnDetalle))
                                .appendTo(container);
                        }
                    }
                ],
            });
            $(".btn").attr("disabled", false);
            $(".btn-txt").text("Consultar");
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

function ModalObtenerFactura(modelo) {
    document.getElementById("ImagenFactura").src = "../Images/ImagenesGarantias/Facturas/"+ modelo.FacturaCliente;
    $("#ModalFactura").modal("show");
}

function ModalObtenerTest(modelo) {
    document.getElementById("ImagenTest").src = "../Images/ImagenesGarantias/Test/" + modelo.TestBateria;
    $("#ModalTest").modal("show");
}

function ModalObtenerDetalle(modelo) {
    ObtenerInspeccionInicial(modelo.IngresoRevisionGarantiaId);
    ObtenerDiagnostico(modelo.IngresoRevisionGarantiaId);
    ObtenerTrabajoRealizado(modelo.IngresoRevisionGarantiaId);

    $("#ModalDetalleRevision").modal("show");
}


function ObtenerInspeccionInicial(IdCabeceraRevisionGarantia) {
    $.ajax({
        url: "../Garantias/ConsultarInspeccionInicial",
        type: "POST",
        data: {
            IdCabeceraInspeccion: IdCabeceraRevisionGarantia
        },success: function (msg) {
            //ItemsPedidoCliente = msg;  
            $("#tblInspeccionInicial").dxDataGrid({
                dataSource: msg,
                keyExpr: 'IngresoRevisionGarantiaInspeccionInicialId',
                showBorders: true,
                columnAutoWidth: true,
                showBorders: true,
                paging: {
                    pageSize: 10
                },
                columns: [{
                    caption: 'Inspeccion Inicial', alignment: 'center',
                    columns: [
                        {
                            dataField: "IngresoRevisionGarantiaInspeccionInicialId", visible: false
                        },
                        {
                            dataField: "GolpeadaORota", caption: "Golpeada o Rota"
                        }, {
                            dataField: "Hinchada", caption: "Hinchada"
                        }, {
                            dataField: "BornesFlojosOHundidos", caption: "Bornes Flojos o Hundidos"
                        },
                        {
                            dataField: "BornesFundidos", caption: "Bornes Fundidos"
                        },

                        {
                            dataField: "ElectrolitoErroneo", caption: "Electrolito Erroneo"
                        }, {
                            dataField: "FugaEnCubierta", caption: "Fuga en cubierta"
                        }, {
                            dataField: "FugaEnBornes", caption: "Fuga en bornes"
                        },
                        {
                            dataField: "CCA", caption: "CCA", alignment: 'center',
                        }
                    ]
                }
                ]
                ,
            });
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

function ObtenerDiagnostico(IdCabeceraRevisionGarantia) {
    $.ajax({
        url: "../Garantias/ConsultarDiagnostico",
        type: "POST",
        data: {
            IdCabeceraInspeccion: IdCabeceraRevisionGarantia
        }, success: function (msg) {
            //ItemsPedidoCliente = msg;  
            $("#tblDiagnostico").dxDataGrid({
                dataSource: msg,
                keyExpr: 'IngresoRevisionGarantiaDiagnosticoId',
                showBorders: true,
                columnAutoWidth: true,
                showBorders: true,
                paging: {
                    pageSize: 10
                },
                columns: [{
                    caption: 'Diagnostico', alignment: 'center',
                columns: [

                    {
                        dataField: "IngresoRevisionGarantiaDiagnosticoId", visible: false
                    },
                    {
                        dataField: "BateriaEnBuenEstado", caption: "Bateria en Buen Estado"
                    }, {
                        dataField: "PresentaFalloFabricacion", caption: "Presenta Fallo Fabricacion"
                    }, {
                        dataField: "DentroPeriodoGarantia", caption: "Dentro Periodo Garantia"
                    },
                    {
                        dataField: "AplicacionUsoAdecuado", caption: "Aplicacion Uso Adecuado"
                    },

                    {
                        dataField: "AplicaGarantia", caption: "Aplica Garantia"
                    }
                    ]
                }
                ]                    ,
            });
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

function ObtenerTrabajoRealizado(IdCabeceraRevisionGarantia) {
    $.ajax({
        url: "../Garantias/ConsultarTrabajoRealizado",
        type: "POST",
        data: {
            IdCabeceraInspeccion: IdCabeceraRevisionGarantia
        }, success: function (msg) {
            //ItemsPedidoCliente = msg;  
            $("#tblTrabajoRealizado").dxDataGrid({
                dataSource: msg,
                keyExpr: 'IngresoRevisionGarantiaTrabajoRealizadoId',
                showBorders: true,
                columnAutoWidth: true,
                showBorders: true,
                paging: {
                    pageSize: 10
                },
                columns: [{
                    caption: 'Trabajo Realizado', alignment: 'center',
                columns: [

                    {
                        dataField: "IngresoRevisionGarantiaTrabajoRealizadoId", visible: false
                    },
                    {
                        dataField: "PruebaAltaResistencia", caption: "Prueba Alta Resistencia"
                    }, {
                        dataField: "CambioAcido", caption: "Cambio Acido"
                    }, {
                        dataField: "RecargaBateria", caption: "Recarga Bateria"
                    },
                    {
                        dataField: "InspeccionEstructuraExterna", caption: "Inspeccion Estructura Externa"
                    },

                    ]
                }
                ]                    ,
            });
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