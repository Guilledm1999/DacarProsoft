var IdentificadorRevisionGarantia = null;

$(document).ready(function () {
    ConsultaRegistrosGarantias();
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

function ConsultaRegistrosGarantias() {
        $.ajax({
            url: "../Produccion/ConsultarRegistrosGarantias",
            type: "GET"
            , success: function (msg) {

                $("#tblGarantiasRegistradas").dxDataGrid({
                    dataSource: msg,
                    keyExpr: 'IngresoRevisionGarantiaId',
                    showBorders: true,
                    columnAutoWidth: true,
                    showBorders: true,
                    showColumnLines: true,
                    rowAlternationEnabled: false,
                    showRowLines: true,
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
                    //pager: {
                    //    visible: true,
                    //    allowedPageSizes: [5, 10, 50],
                    //    showPageSizeSelector: true,
                    //    showInfo: true,
                    //    showNavigationButtons: true
                    //},
                    export: {
                        enabled: true,
                        allowExportSelectedData: false
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
                                saveAs(new Blob([buffer], { type: 'application/octet-stream' }), 'AnalisisGarantias.xlsx');
                            });
                        });
                        e.cancel = true;
                    },
                    columns: [

                        { dataField: "IngresoRevisionGarantiaId", visible: false },
                        {
                            dataField: "Cliente", caption: "Cliente", alignment: "left", allowHeaderFiltering: true, allowSearch: false
                        },                 
                        {
                            dataField: "NumeroComprobante", caption: "Comprobante", alignment: "left", allowHeaderFiltering: false, allowSearch: true
                        },                      
                        {
                            dataField: "Provincia", caption: "Provincia", alignment: "left", allowHeaderFiltering: true, allowSearch: false
                        },
                        {
                            dataField: "Direccion", caption: "Direccion", alignment: "left", allowHeaderFiltering: false, allowSearch: false
                        },
                        {
                            dataField: "Vendedor", caption: "Vendedor", alignment: "left", allowHeaderFiltering: true, allowSearch: false
                        },                      
                        {
                            dataField: "Modelo", caption: "Modelo", alignment: "left", allowHeaderFiltering: true, allowSearch: false
                        },
                        {
                            dataField: "Lote", caption: "Lote", alignment: "left", visible: false, allowHeaderFiltering: false, allowSearch: false
                        },
                        {
                            dataField: "LoteEnsamble", caption: "Lote Ensamble", alignment: "left", visible: false, allowHeaderFiltering: false, allowSearch: false
                        },
                        
                        {
                            dataField: "Prorrateo", caption: "Prorrateo", alignment: "left", visible: false, allowHeaderFiltering: false, allowSearch: false
                        },
                        {
                            dataField: "Meses", caption: "Meses", alignment: "left", visible: false, allowHeaderFiltering: false, allowSearch: false
                        },
                        {
                            dataField: "PorcentajeVenta", caption: "Porcentaje Venta", alignment: "left", visible: false, allowHeaderFiltering: false, allowSearch: false
                        },
                        {
                            dataField: "Voltaje", caption: "Voltaje", alignment: "left", visible: false, allowHeaderFiltering: false, allowSearch: false
                        },
                        {
                            dataField: "ModoIngreso", caption: "Modo Ingreso", alignment: "left", visible: false, allowHeaderFiltering: true, allowSearch: false
                        },
                        {
                            dataField: "FechaVenta", caption: "Fecha Venta", alignment: "left", dataType: "date", allowHeaderFiltering: true, allowSearch: false
                        },
                        {
                            dataField: "FechaIngreso", caption: "Fecha Ingreso", alignment: "left", dataType: "date", allowHeaderFiltering: true, allowSearch: false
                        },
                        {
                            dataField: "AplicaGarantia", caption: "Aplica Garantia", alignment: "left", allowHeaderFiltering: true, allowSearch: false, visible:false
                        },
                        {
                            caption: "Acciones",
                            cellTemplate: function (container, options) {
                                var btnDiagnostico = "<button class='btn-primary' onclick='ModalObtenerDiagnostico(" + JSON.stringify(options.data) + ")'>Diagnostico</button>";
                              

                                $("<div>")
                                    .append($(btnDiagnostico))
                                    .appendTo(container);
                            }
                        }
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

function ModalObtenerDiagnostico(datos) {
    IdentificadorRevisionGarantia = datos.IngresoRevisionGarantiaId;
    $("#txtModeloBateria").val(datos.Modelo);
    $("#txtNumeroComprobante").val(datos.NumeroComprobante);
    $("#txtLoteEnsamble").val(datos.LoteEnsamble);
    $("#txtLoteCarga").val(datos.Lote);
    $("#txtVoltajeBateria").val(datos.Voltaje);

 

    //console.log("entro al metodo");
    //console.log(datos);
    ObtenerInspeccionInicial(datos.IngresoRevisionGarantiaId);
    ObtenerDiagnostico(datos.IngresoRevisionGarantiaId);
    ObtenerTrabajoRealizado(datos.IngresoRevisionGarantiaId);

    $("#ModalDiagnosticoRevision").modal("show");
}


function ObtenerInspeccionInicial(IdCabeceraRevisionGarantia) {
    $.ajax({
        url: "../Garantias/ConsultarInspeccionInicial",
        type: "POST",
        data: {
            IdCabeceraInspeccion: IdCabeceraRevisionGarantia
        }, success: function (msg) {
            //ItemsPedidoCliente = msg;  
            $("#tblInspeccionInicial").dxDataGrid({
                dataSource: msg,
                keyExpr: 'IngresoRevisionGarantiaInspeccionInicialId',
                showBorders: true,
                columnAutoWidth: true,
                showBorders: true,
                showColumnLines: true,
                rowAlternationEnabled: false,
                showRowLines: true,
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
                            dataField: "CCA", caption: "CCA", alignment: 'center', visible: false
                        }
                    ]
                }
                ]
                ,
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
                showColumnLines: true,
                rowAlternationEnabled: false,
                showRowLines: true,
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
                showColumnLines: true,
                rowAlternationEnabled: false,
                showRowLines: true,
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

function SeleccionarAreaResponsable(){
    if ($("#txtDiagnostico option:selected").val() != "") {
        $.ajax({
            type: 'POST',
            url: "../Produccion/ConsultarAreaResponsable",
            data: { CodidoArea: $("#txtDiagnostico option:selected").val() },
            success: function (msg) {
                console.log(msg);
                $("#txtAreaResponsable").val(msg);
            },
        })
    } else {
        $("#txtAreaResponsable").val("");
    }
}

function validarIngresos() {
    var valor1 = $("#txtNumeroComprobante").val();
    var valor2 = $("#txtLoteEnsamble").val();
    var valor3 = $("#txtLoteCarga").val();
    var valor4 = $("#txtModeloBateria").val();
    var valor5 = $("#txtVoltajeBateria").val();
    var valor6 = $("#txtAreaResponsable").val();
  
    if ($("#txtDiagnostico option:selected").val() == "") {
        $("#MensajeIngreseTodosLosCampos").show('fade');
        setTimeout(function () {
            $("#MensajeIngreseTodosLosCampos").fadeOut(1500);
        }, 3000);
        return;
    }
    if (valor1.length == 0) {
        $("#MensajeIngreseTodosLosCampos").show('fade');
        setTimeout(function () {
            $("#MensajeIngreseTodosLosCampos").fadeOut(1500);
        }, 3000);
        return;
    }
    if (valor2.length == 0) {
        $("#MensajeIngreseTodosLosCampos").show('fade');
        setTimeout(function () {
            $("#MensajeIngreseTodosLosCampos").fadeOut(1500);
        }, 3000);
        return;
    } if (valor3.length == 0) {
        $("#MensajeIngreseTodosLosCampos").show('fade');
        setTimeout(function () {
            $("#MensajeIngreseTodosLosCampos").fadeOut(1500);
        }, 3000);
        return;
    } if (valor4.length == 0) {
        $("#MensajeIngreseTodosLosCampos").show('fade');
        setTimeout(function () {
            $("#MensajeIngreseTodosLosCampos").fadeOut(1500);
        }, 3000);
        return;
    } if (valor5.length == 0) {
        $("#MensajeIngreseTodosLosCampos").show('fade');
        setTimeout(function () {
            $("#MensajeIngreseTodosLosCampos").fadeOut(1500);
        }, 3000);
        return;
    } 
    if (valor6.length == 0) {
        $("#MensajeIngreseTodosLosCampos").show('fade');
        setTimeout(function () {
            $("#MensajeIngreseTodosLosCampos").fadeOut(1500);
        }, 3000);
        return;
    } 

    RegistrarAnalisisRegistroGarantia();
}

function RegistrarAnalisisRegistroGarantia() {
    var valor = "";
    $.ajax({
        url: '../Produccion/RegistrarAnalisisDeGarantias',
        data: {
            IngresoRevisionGarantiaId: IdentificadorRevisionGarantia, LoteFabricacion: $("#txtNumeroComprobante").val(), LoteEnsamble: $("#txtLoteEnsamble").val(), LoteCarga: $("#txtLoteCarga").val(), ModeloBateria: $("#txtModeloBateria").val(),
            Voltaje: $("#txtVoltajeBateria").val(), CCA: $("#txtCCABateria").val(), DencidadCelda1: $("#txtCelda1").val(), DencidadCelda2: $("#txtCelda2").val(), DencidadCelda3: $("#txtCelda3").val(), DencidadCelda4: $("#txtCelda4").val(),
            DencidadCelda5: $("#txtCelda5").val(), DencidadCelda6: $("#txtCelda6").val(), ResumenAnalisis: $("#txtDiagnostico option:selected").text(), AreaResponsable: $("#txtAreaResponsable").val(), Observaciones: $("#txtObservaciones").val()
        },
        type: 'post',
        success: function (respuesta) {
            if (respuesta == "True") {
                ConsultaRegistrosGarantias();
                $("#txtNumeroComprobante").val("");
                $("#txtLoteEnsamble").val("");
                $("#txtLoteCarga").val("");
                $("#txtModeloBateria").val("");
                $("#txtVoltajeBateria").val("");
                $("#txtCCABateria").val("");
                $("#txtCelda1").val("");
                $("#txtCelda2").val("");
                $("#txtCelda3").val("");
                $("#txtCelda4").val("");
                $("#txtCelda5").val("");
                $("#txtCelda6").val("");
                $("#txtAreaResponsable").val("");
                $("#txtObservaciones").val("");
                $("#txtDiagnostico option[value='']").attr("selected", true);

                $("#ModalDiagnosticoRevision").modal("hide");

                $("#MensajeGuardadoExitoso").show('fade');
                setTimeout(function () {
                    $("#MensajeGuardadoExitoso").fadeOut(1500);
                }, 3000); return;
              
            } else {
                console.log("entre x falso");
                $("#MensajeErrorInesperado").show('fade');
                setTimeout(function () {
                    $("#MensajeErrorInesperado").fadeOut(1500);
                }, 3000); return;
            }
        },
        error: function () {
            $("#MensajeErrorInesperado").show('fade');
            setTimeout(function () {
                $("#MensajeErrorInesperado").fadeOut(1500);
            }, 3000); return;
        }
    });
}