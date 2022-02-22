﻿var valor = null;
var temp = null;
var valor = null;
var char;

$(document).ready(function () {
    ConsultaRegistrosPruebasLaboratorio();
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
$('#LinkClose4').on("click", function (e) {
    $("#MensajeSinAnexos").hide('fade');
});

function ConsultaRegistrosPruebasLaboratorio() {
    $.ajax({
        url: "../Calidad/ConsultarRegistrosPruebasLaboratorio",
        type: "GET"
        , success: function (msg) {
            temp = msg;
            const locale = getLocale();
            DevExpress.localization.locale(locale);
            $("#tblPruebasLaboratorioRegistrados").dxDataGrid({
                dataSource: temp,
                keyExpr: 'PruebaLaboratorioCalidadId',
                showBorders: true,
                columnAutoWidth: true,              
                showBorders: true,
                showColumnLines: true,
                showRowLines: true,
                rowAlternationEnabled: false,
                allowColumnReordering: true,
                allowColumnResizing: false,       

                searchPanel: {
                    visible: true,
                    width: 240,
                    placeholder: "Buscar..."
                },
                headerFilter: {
                    visible: true
                },
                filterPanel: { visible: true },

               
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
                repaintChangesOnly: true,

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
                    {
                        caption: "Anexos", allowExporting: false,
                        headerCellTemplate: function (header, info) {
                            $('<div>')
                                .html(info.column.caption)
                                .css('white-space', 'normal')
                                .css('text-align', 'center')
                                .css('margin', '7px 0 0 0')
                                .appendTo(header);
                        },
                        cellTemplate: function (container, options) {
                            //var btnAnexo = "<button class='btn-primary' onclick='ModalObtenerRutaAnexos(" + JSON.stringify(options.data) + ")'>Anexos</button>";
                            var btnAnexo = "<a style='box-shadow: 2px 2px 5px #999 inset' onclick='ModalRegistrarAnexo(" + JSON.stringify(options.data) + ")'>Ingreso</a>";
                            var lblEspacio = "<a> </a>"
                            var btnAnexo2 = "<a style='box-shadow: 2px 2px 5px #999 inset' onclick='ModalObtenerRutaAnexos(" + JSON.stringify(options.data) + ")'>Consulta</a>";

                           // var nav = "<nav class='menu'> <ul> <li><a href='#'>Ingreso</a></li>  <li><a href='#'>Consulta</a></li>  </ul> </nav>";

                            $("<div>")
                                .append($(btnAnexo), $(lblEspacio), $(btnAnexo2))
                                .appendTo(container);
                        }
                    },

                    { dataField: "PruebaLaboratorioCalidadId", visible: false },
                    {
                        dataField: "FechaIngreso", caption: "Fecha Ingreso", alignment: "left", dataType: "date", allowHeaderFiltering: true, allowSearch: false, width: 100,
                        headerCellTemplate: function (header, info) {
                            $('<div>')
                                .html(info.column.caption)
                                .css('white-space', 'normal')
                                .css('text-align', 'center')
                                .appendTo(header);
                        }
                    },
                    {
                        dataField: "CodigoIngreso", caption: "Codigo Ingreso", alignment: "left", allowHeaderFiltering: true, allowSearch: true, width: 100,
                        headerCellTemplate: function (header, info) {
                            $('<div>')
                                .html(info.column.caption)
                                .css('white-space', 'normal')
                                .css('text-align', 'center')
                                .appendTo(header);
                        }
                    },
                    {
                        dataField: "Marca", caption: "Marca", alignment: "left", allowHeaderFiltering: true, allowSearch: true,
                        headerCellTemplate: function (header, info) {
                            $('<div>')
                                .html(info.column.caption)
                                .css('white-space', 'normal')
                                .css('text-align', 'center')
                                .css('margin', '7px 0 0 0')
                                .appendTo(header);
                        }

                    },
                    {
                        dataField: "TipoNorma", caption: "Tipo Norma", alignment: "left", allowHeaderFiltering: true, allowSearch: true, width: 90,
                        headerCellTemplate: function (header, info) {
                            $('<div>')
                                .html(info.column.caption)
                                .css('white-space', 'normal')
                                .css('text-align', 'center')
                                .appendTo(header);
                        }
                    },
                    {
                        dataField: "Normativa", caption: "Normativa", alignment: "left", allowHeaderFiltering: true, allowSearch: true, width: 100,
                        headerCellTemplate: function (header, info) {
                            $('<div>')
                                .html(info.column.caption)
                                .css('white-space', 'normal')
                                .css('text-align', 'center')
                                .css('margin', '7px 0 0 0')
                                .appendTo(header);
                        }
                    },
                    {
                        dataField: "PreAcondicionamiento", caption: "Pre-Acond.", alignment: "left", allowHeaderFiltering: true, allowSearch: true, width: 100,
                        headerCellTemplate: function (header, info) {
                            $('<div>')
                                .html(info.column.caption)
                                .css('white-space', 'normal')
                                .css('text-align', 'center')
                                .css('margin', '7px 0 0 0')
                                .appendTo(header);
                        }
                    },
                    {
                        dataField: "TipoBateria", caption: "Tipo Bateria", alignment: "left", allowHeaderFiltering: true, allowSearch: true, width: 110,
                        headerCellTemplate: function (header, info) {
                            $('<div>')
                                .html(info.column.caption)
                                .css('white-space', 'normal')
                                .css('text-align', 'center')
                                .css('margin', '7px 0 0 0')
                                .appendTo(header);
                        }
                    },
                    {
                        dataField: "Modelo", caption: "Modelo", alignment: "left", allowHeaderFiltering: true, allowSearch: true, width: 95,
                        headerCellTemplate: function (header, info) {
                            $('<div>')
                                .html(info.column.caption)
                                .css('white-space', 'normal')
                                .css('text-align', 'center')
                                .css('margin', '7px 0 0 0')
                                .appendTo(header);
                        }
                    },
                    {
                        dataField: "Separador", caption: "Separador", alignment: "left", allowHeaderFiltering: true, allowSearch: true, width: 100,
                        headerCellTemplate: function (header, info) {
                            $('<div>')
                                .html(info.column.caption)
                                .css('white-space', 'normal')
                                .css('text-align', 'center')
                                .css('margin', '7px 0 0 0')
                                .appendTo(header);
                        }
                    },
                    {
                        dataField: "TipoEnsayo", caption: "Tipo Ensayo", alignment: "left", allowHeaderFiltering: true, allowSearch: true, width: 90,
                        headerCellTemplate: function (header, info) {
                            $('<div>')
                                .html(info.column.caption)
                                .css('white-space', 'normal')
                                .css('text-align', 'center')
                                .appendTo(header);
                        }
                    },
                    {
                        dataField: "LoteEnsamble", caption: "Lote de Ensamble", alignment: "right", allowHeaderFiltering: false, allowSearch: true, width: 90,
                        headerCellTemplate: function (header, info) {
                            $('<div>')
                                .html(info.column.caption)
                                .css('white-space', 'normal')
                                .css('text-align', 'center')
                                .appendTo(header);
                        }
                    },
                    {
                        dataField: "LoteCarga", caption: "Lote de Carga", alignment: "right", allowHeaderFiltering: false, allowSearch: true, width: 90,
                        headerCellTemplate: function (header, info) {
                            $('<div>')
                                .html(info.column.caption)
                                .css('white-space', 'normal')
                                .css('text-align', 'center')
                                .appendTo(header);
                        }
                    },
                    {
                        dataField: "CCA", caption: "CCA", alignment: "right", allowHeaderFiltering: false, allowSearch: false, width: 70,
                        headerCellTemplate: function (header, info) {
                            $('<div>')
                                .html(info.column.caption)
                                .css('white-space', 'normal')
                                .css('text-align', 'center')
                                .css('margin', '7px 12px 0 0')
                                .appendTo(header);
                        }
                    },
                    {
                        dataField: "Peso", caption: "Peso", alignment: "right", allowHeaderFiltering: false, allowSearch: false, width: 70,
                        headerCellTemplate: function (header, info) {
                            $('<div>')
                                .html(info.column.caption)
                                .css('white-space', 'normal')
                                .css('text-align', 'center')
                                .css('margin', '7px 12px 0 0')
                                .appendTo(header);
                        }
                    },
                    {
                        dataField: "Voltaje", caption: "Voltaje", alignment: "right", allowHeaderFiltering: false, allowSearch: false, width: 80,
                        headerCellTemplate: function (header, info) {
                            $('<div>')
                                .html(info.column.caption)
                                .css('white-space', 'normal')
                                .css('text-align', 'center')
                                .css('margin', '7px 12px 0 0')
                                .appendTo(header);
                        }
                    },
                    {
                        dataField: "DensidadIngreso", caption: "Dens. Ingreso", alignment: "right", allowHeaderFiltering: false, allowSearch: false, width: 80,
                        headerCellTemplate: function (header, info) {
                            $('<div>')
                                .html(info.column.caption)
                                .css('white-space', 'normal')
                                .css('text-align', 'center')
                                .appendTo(header);
                        }
                    },

                    {
                        dataField: "DensidadPreAcondicionamiento", caption: "Dens. Pre-Acond.", alignment: "right", allowHeaderFiltering: false, allowSearch: false, width: 85,
                        headerCellTemplate: function (header, info) {
                            $('<div>')
                                .html(info.column.caption)
                                .css('white-space', 'normal')
                                .css('text-align', 'center')
                                .appendTo(header);
                        }
                    },
                    {
                        dataField: "TemperaturaIngreso", caption: "Temp. Ingreso", alignment: "right", allowHeaderFiltering: false, allowSearch: false, width: 80,
                        headerCellTemplate: function (header, info) {
                            $('<div>')
                                .html(info.column.caption)
                                .css('white-space', 'normal')
                                .css('text-align', 'center')
                                .appendTo(header);
                        }
                    },
                    {
                        dataField: "TemperaturaPrueba", caption: "Temp. Prueba", alignment: "right", allowHeaderFiltering: false, allowSearch: false, width: 80,
                        headerCellTemplate: function (header, info) {
                            $('<div>')
                                .html(info.column.caption)
                                .css('white-space', 'normal')
                                .css('text-align', 'center')
                                .appendTo(header);
                        }
                    },
                    {
                        dataField: "DatoTeoricoPrueba", caption: "Dato Teorico Prueba", alignment: "right", allowHeaderFiltering: false, allowSearch: false, width: 90,
                        headerCellTemplate: function (header, info) {
                            $('<div>')
                                .html(info.column.caption)
                                .css('white-space', 'normal')
                                .css('text-align', 'center')
                                .appendTo(header);
                        }
                    },
                    {
                        dataField: "ValorObjetivo", caption: "Valor Objetivo", alignment: "right", allowHeaderFiltering: false, allowSearch: false, width: 75,
                        headerCellTemplate: function (header, info) {
                            $('<div>')
                                .html(info.column.caption)
                                .css('white-space', 'normal')
                                .css('text-align', 'center')
                                .appendTo(header);
                        }
                    },
                    {
                        dataField: "ResultadoFinal", caption: "Resultado Final", alignment: "right", allowHeaderFiltering: false, allowSearch: false, width: 80,
                        headerCellTemplate: function (header, info) {
                            $('<div>')
                                .html(info.column.caption)
                                .css('white-space', 'normal')
                                .css('text-align', 'center')
                                .appendTo(header);
                        }
                    },
                    {
                        dataField: "Calificacion", caption: "Calificacion", alignment: "right", allowHeaderFiltering: false, allowSearch: false, width: 90,
                        headerCellTemplate: function (header, info) {
                            $('<div>')
                                .html(info.column.caption)
                                .css('vertical-align', 'middle')
                                .css('text-align', 'center')
                                .css('margin', '7px 4px 0 0')
                                .appendTo(header);
                        }
                    },
                    {
                        dataField: "Observaciones", caption: "Observaciones", alignment: "right", allowHeaderFiltering: false, allowSearch: false, width: 115,
                        headerCellTemplate: function (header, info) {
                            $('<div>')
                                .html(info.column.caption)
                                .css('white-space', 'normal')
                                .css('text-align', 'center')
                                .css('margin', '7px 7px 0 0')
                                .appendTo(header);
                        }
                    },            
                    {
                        dataField: "FechaRegistro", caption: "Fecha Registro", alignment: "right", dataType: "date", allowHeaderFiltering: false, allowSearch: false, width: 80,
                        headerCellTemplate: function (header, info) {
                            $('<div>')
                                .html(info.column.caption)
                                .css('white-space', 'normal')
                                .css('text-align', 'center')
                                .appendTo(header);
                        }
                    },       
                ],
                summary : {
                    totalItems: [
                        {
                            name: "LoteCarga",
                            column: "LoteCarga",
                            displayFormat: "Cantidad Total",
                            showInColumn: "LoteCarga",
                            customizeText: function (e) {
                                    return "Promedios: ";
                            }
                        }

                        , {
                            column: "CCA",
                            summaryType: "avg",
                            showInColumn: "CCA",
                            customizeText: function (e) {
                                if (e.value != 0 && e.value != "") {
                                    const noTruncarDecimales = { maximumFractionDigits: 0 };
                                    ValTotal = (e.value).toLocaleString('en-US', noTruncarDecimales);
                                    $("#txtPromedioCCA").val(ValTotal);
                                    return  ValTotal;
                                }
                            }

                        }, {
                            column: "DensidadPreAcondicionamiento",
                            summaryType: "avg",
                            showInColumn: "DensidadPreAcondicionamiento",
                            customizeText: function (e) {
                                if (e.value != 0 && e.value != "") {
                                    const noTruncarDecimales = { maximumFractionDigits: 0 };
                                    ValTotal = (e.value).toLocaleString('en-US', noTruncarDecimales);
                                    return ValTotal;
                                }
                            }
                        }, {
                            column: "TemperaturaPrueba",
                            summaryType: "avg",
                            showInColumn: "TemperaturaPrueba",
                            customizeText: function (e) {
                                if (e.value != 0 && e.value != "") {
                                    const noTruncarDecimales = { maximumFractionDigits: 0 };
                                    ValTotal = (e.value).toLocaleString('en-US', noTruncarDecimales);
                                    return ValTotal;
                                }
                            }
                        },{
                            column: "Peso",
                            summaryType: "avg",
                            showInColumn: "Peso",
                            customizeText: function (e) {
                                if (e.value != 0 && e.value != "") {
                                    const noTruncarDecimales = { maximumFractionDigits: 2 };
                                    ValTotal = (e.value).toLocaleString('en-US', noTruncarDecimales);
                                    $("#txtPromedioPeso").val(ValTotal);

                                    return ValTotal;
                                }
                            }

                        }, {
                            column: "Voltaje",
                            summaryType: "avg",
                            showInColumn: "Voltaje",
                            customizeText: function (e) {
                                if (e.value != 0 && e.value != "") {
                                    const noTruncarDecimales = { maximumFractionDigits: 2 };
                                    ValTotal = (e.value).toLocaleString('en-US', noTruncarDecimales);
                                    $("#txtPromedioVoltaje").val(ValTotal);
                                    return ValTotal;
                                }
                            }

                        }, {
                            column: "DensidadIngreso",
                            summaryType: "avg",
                            showInColumn: "DensidadIngreso",
                            customizeText: function (e) {
                                if (e.value != 0 && e.value != "") {
                                    const noTruncarDecimales = { maximumFractionDigits: 0 };
                                    ValTotal = (e.value).toLocaleString('en-US', noTruncarDecimales);
                                    return ValTotal;
                                }
                            }

                        }, {
                            column: "TemperaturaIngreso",
                            summaryType: "avg",
                            showInColumn: "TemperaturaIngreso",
                            customizeText: function (e) {
                                if (e.value != 0 && e.value != "") {
                                    const noTruncarDecimales = { maximumFractionDigits: 0 };
                                    ValTotal = (e.value).toLocaleString('en-US', noTruncarDecimales);
                                    return ValTotal;
                                }
                            }

                        }, {
                            column: "DatoTeoricoPrueba",
                            summaryType: "avg",
                            showInColumn: "DatoTeoricoPrueba",
                            customizeText: function (e) {
                                if (e.value != 0 && e.value != "") {
                                    const noTruncarDecimales = { maximumFractionDigits: 0 };
                                    ValTotal = (e.value).toLocaleString('en-US', noTruncarDecimales);
                                    return ValTotal;
                                }
                            }

                        }, {
                            column: "ValorObjetivo",
                            summaryType: "avg",
                            showInColumn: "ValorObjetivo",
                            customizeText: function (e) {
                                if (e.value != 0 && e.value != "") {
                                    const noTruncarDecimales = { maximumFractionDigits: 0 };
                                    ValTotal = (e.value).toLocaleString('en-US', noTruncarDecimales);
                                    $("#txtValorObjetivo").val(ValTotal);
                                    return ValTotal;
                                }
                            }

                        }, {
                            column: "Calificacion",
                            summaryType: "avg",
                            showInColumn: "Calificacion",
                            customizeText: function (e) {
                                if (e.value != 0 && e.value != "") {
                                    const noTruncarDecimales = { maximumFractionDigits: 0 };
                                    ValTotal = (e.value).toLocaleString('en-US', noTruncarDecimales);
                                    $("#txtPromedioCalificacion").val(ValTotal);
                                    return ValTotal;
                                }
                            }

                        },
                        {
                            column: "ResultadoFinal",
                            summaryType: "avg",
                            showInColumn: "ResultadoFinal",
                            customizeText: function (e) {
                                if (e.value != 0 && e.value != "") {
                                    const noTruncarDecimales = { maximumFractionDigits: 2 };
                                    ValTotal = (e.value).toLocaleString('en-US', noTruncarDecimales);
                                    $("#txtPromedioResultadoFinal").val(ValTotal);

                                    return ValTotal;
                                }
                            }
                        },
                    ],
                }, 
                onContentReady: function (e) {

                    BotonPrueba();             
                }, 
            });
        },
        error: function (msg) {

            $("#MensajeErrorInesperado").show('fade');
            setTimeout(function () {
                $("#MensajeErrorInesperado").fadeOut(1500);
            }, 3000); return;
        }
    })

    function getLocale() {
        const storageLocale = sessionStorage.getItem('locale');
        return storageLocale != null ? storageLocale : 'es';
    }
}

//href = "../pdfs/project-brief.pdf"
function ModalRegistrarAnexo(valor) {
    valores = valor;
    $("#ModalAnexosIngreso").modal("show");

}

function RegistrarNuevoAnexo() {
    var formdata = new FormData();
    formdata.append("PruebaLaboratorioCalidadId", valores.PruebaLaboratorioCalidadId);
    formdata.append("FechaRegistro", valores.FechaRegistro);
    formdata.append("modelo", valores.Modelo);

    var files = $("#txtRegistrarAnexos").get(0).files;

    for (var i = 0; i < files.length; i++) {
        formdata.append("archivos", files[i]);
    }
    $.ajax({
        type: 'POST',
        url: "../Calidad/RegistrarNuevosAnexos",
        processData: false,
        contentType: false,
        data:
            formdata,
        success: function (msg) {
            if (msg == "True") {
                $("#ModalAnexosIngreso").modal("hide");
                $("#txtRegistrarAnexos").val("");
                $("#MensajeGuardadoExitoso").show('fade');
                setTimeout(function () {
                    $("#MensajeGuardadoExitoso").fadeOut(1500);
                }, 3000);

            } else {
                $("#MensajeErrorInesperado").show('fade');
                setTimeout(function () {
                    $("#MensajeErrorInesperado").fadeOut(1500);
                }, 3000);
            }
        },
        error: function (msg) {
            $("#MensajeErrorInesperado").show('fade');
            setTimeout(function () {
                $("#MensajeErrorInesperado").fadeOut(1500);
            }, 3000);
        }
    })
}


function ModalObtenerRutaAnexos(valor) {
    $.ajax({
        url: '../Calidad/ConsultarAnexosRegistrados',
        data: {
            IdRegistro: valor.PruebaLaboratorioCalidadId, FechaRegistro: valor.FechaRegistro, modelo: valor.Modelo
        },
        type: 'post',
        success: function (respuestas) {
            if (respuestas.length === 0) {
                $("#MensajeSinAnexos").show('fade');
                setTimeout(function () {
                    $("#MensajeSinAnexos").fadeOut(1500);
                }, 3000); return;
                
            } else {
                $("#txtAnexoDesc").empty();
                $("#txtAnexoDesc").append('<option value="">--Seleccione--</option>');
                $.each(respuestas, function (i, respuesta) {
                    $("#txtAnexoDesc").append('<option value="' + respuesta.Value + '">' +
                        respuesta.Text + '</option>');
                });
            }
        },
        error: function () {
            $("#MensajeErrorGeneral").show('fade');
            setTimeout(function () {
                $("#MensajeErrorGeneral").fadeOut(1500);
            }, 3000); return;
        }
    });
    $("#ModalAnexos").modal("show");
}


function DescargarAnexo() {
    $("#ModalAnexos").modal("hide");
    var valor = $("#txtAnexoDesc option:selected").val();
    window.open(valor);
}

function BotonPrueba() {
    const filterExpr = $("#tblPruebasLaboratorioRegistrados").dxDataGrid("instance").getCombinedFilter(true);
    $("#tblPruebasLaboratorioRegistrados").dxDataGrid("instance").getDataSource()
        .store()
        .load({ filter: filterExpr })
        .then((result) => {
            valor = result;
            console.log(result);
        }); 
}

function ChartResumenesGarantias() {
    // encontrar solucion para que solo se pueda sacar detalles de una sola bateria
    var comprobador = null;
    for (var i in valor.sort()) {
        if (valor[0].Modelo == valor[i].Modelo) {
            comprobador = 1;
        } else {
            comprobador == 2;
        }
    }
    console.log("Comprobador:" + comprobador);

    if (comprobador == 2) {
        $("#MensajeErrorGeneral").show('fade');
        setTimeout(function () {
            $("#MensajeErrorGeneral").fadeOut(1500);
        }, 3000); return;
    }
    if (comprobador==1) {
        $("#lblDetallePackingList").text("Analisis Pruebas Laboratorio - Modelo " + valor[0].Modelo);

        $("#ModalInformeGrafica").modal("show");

        if (char != null) {
            char.destroy();
        }
        var ctx = $("#myChart")

        var nombre = [];
        var stock = [];
        var stock2 = [];

        var color = ['rgba(255, 99, 132, 0.2)', 'rgba(54, 162, 235, 0.2)', 'rgba(255, 206, 86, 0.2)', 'rgba(75, 192, 192, 0.2)', 'rgba(153, 102, 255, 0.2)', 'rgba(255, 159, 64, 0.2)', 'rgba(224, 18, 248, 0.2)', 'rgba(248, 237, 18, 0.2)', 'rgba(18, 248, 237, 0.2)', 'rgba(179, 6, 22, 0.2)', 'rgba(0, 61, 252, 0.2) '];
        var bordercolor = ['rgba(255,99,132,1)', 'rgba(54, 162, 235, 1)', 'rgba(255, 206, 86, 1)', 'rgba(75, 192, 192, 1)', 'rgba(153, 102, 255, 1)', 'rgba(255, 159, 64, 1)'];

        if (valor[0].TipoEnsayo == "CAP") {
            console.log("CAP");
            $.ajax({
                type: 'POST',
                url: "../Calidad/ConsultarValorTipoDePrueba",
                dataType: 'json',
                data: { modelo: valor[0].Modelo, valor: 1 },
                success: function (result) {
                    for (var i in valor) {
                        nombre.push(valor[i].CodigoIngreso);
                        stock.push(valor[i].ResultadoFinal);
                        //stock2.push(valor[i].DatoTeoricoPrueba);
                        stock2.push(result);

                    }
                },
            })

        }
        if (valor[0].TipoEnsayo == "RC") {
            console.log("RC");

            $.ajax({
                type: 'POST',
                url: "../Calidad/ConsultarValorTipoDePrueba",
                dataType: 'json',
                data: { modelo: valor[0].Modelo, valor: 3 },
                success: function (result) {
                    for (var i in valor) {
                        nombre.push(valor[i].CodigoIngreso);
                        stock.push(valor[i].ResultadoFinal);
                        stock2.push(result);
                    }
                },
            })

        }
        if (valor[0].TipoEnsayo == "CCA") {
            console.log("CCA");

            $.ajax({
                type: 'POST',
                url: "../Calidad/ConsultarValorTipoDePrueba",
                dataType: 'json',
                data: { modelo: valor[0].Modelo, valor: 3 },
                success: function (result) {
                    for (var i in valor) {
                        nombre.push(valor[i].CodigoIngreso);
                        stock.push(valor[i].CCA);
                        stock2.push(result);
                    }
                },
            })

        }
        if (valor[0].TipoEnsayo == "CICLOS") {
            console.log("CICLOS");
            for (var i in valor) {
                nombre.push(valor[i].CodigoIngreso);
                stock.push(valor[i].ResultadoFinal);
                stock2.push(valor[i].ValorObjetivo);
            }

        }


        var chartdata = {
            labels: nombre,
            datasets: [{
                label: 'Resultado',
                backgroundColor: color,
                borderColor: color,
                borderWidth: 2,
                cubicInterpolationMode: 'monotone',
                backgroundColor: 'rgba(7,59,251,0.5)',// Color de fondo
                borderColor: 'rgba(7,59,251,0.5)',// Color del borde
                data: stock,
                fill: false
            },
            {
                label: 'Nominal',
                backgroundColor: color,
                borderColor: color,
                borderWidth: 2,
                cubicInterpolationMode: 'monotone',
                backgroundColor: 'rgba(251, 7, 7, 0.5)',// Color de fondo
                borderColor: 'rgba(251, 7, 7, 0.5)',// Color del borde
                data: stock2,
                fill: false
            }]
        };

        char = new Chart(ctx, {
            type: "line",
            data: chartdata,
            options: {
                responsive: true,
                scales: {
                    yAxes: [{
                        ticks: {
                            stepSize: 5,
                            //beginAtZero: true,
                            //max: 20,
                            //    min: 0
                        },
                        scaleLabel: {
                            display: true,
                            labelString: "Resultados",
                            fontColor: "black"
                        }
                    }],
                    xAxes: [{
                        scaleLabel: {
                            display: true,
                            labelString: "Codigo Ingresos",
                            fontColor: "black"
                        }
                    }],


                },
                interaction: {
                    intersect: false,
                },
                //scales: {
                //    yAxes: [{
                //        ticks: {
                //            beginAtZero: true,
                //            //max: 20,
                //            min: 0
                //        }
                //    }]
                //},
                //tooltip: {
                //    valueDecimals: 0
                //},
                // responsive: true,
                //legend: {
                //    position: 'bottom',
                //},
                title: {
                    display: true,
                    text: 'Tipo de ensayo ' + valor[0].TipoEnsayo
                },
                //animation: {
                //    animateScale: true,
                //    animateRotate: true
                //}
            }
            //options: {
            //    legend: { display: false }
            //}
        });
    }

}