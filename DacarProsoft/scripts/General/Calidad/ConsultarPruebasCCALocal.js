var valor = null;
var temp = null;
var char;
var nominal = null;
var nominalReal = null;
var valorteoricoPr = null;
var resultadofinalPr = null;
var calificacionPr = null;

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
$('#LinkClose5').on("click", function (e) {
    $("#MensajeDobleModelo").hide('fade');
});
$('#LinkClose6').on("click", function (e) {
    $("#MensajeDobleTipoEnsayo").hide('fade');
});
$('#LinkClose7').on("click", function (e) {
    $("#MensajeCompleteCorreo").hide('fade');
});
$('#LinkClose8').on("click", function (e) {
    $("#MensajeRespuestaEnvio").hide('fade');
});
$('#LinkClose9').on("click", function (e) {
    $("#MensajeSinImagenRegistrada").hide('fade');
});

function ConsultaRegistrosPruebasLaboratorio() {
    $.ajax({
        url: "../Calidad/ConsultarRegistrosPruebasCCALaboratorio",
        type: "GET"
        , success: function (msg) {
            temp = msg;
            const locale = getLocale();
            DevExpress.localization.locale(locale);
            $("#tblPruebasLaboratorioRegistrados").dxDataGrid({
                dataSource: temp,
                keyExpr: 'PruebasLaboratorioCCAId',
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
                    //{
                    //    caption: "Anexos", allowExporting: false,
                    //    headerCellTemplate: function (header, info) {
                    //        $('<div>')
                    //            .html(info.column.caption)
                    //            .css('white-space', 'normal')
                    //            .css('text-align', 'center')
                    //            .css('margin', '7px 0 0 0')
                    //            .appendTo(header);
                    //    },
                    //    cellTemplate: function (container, options) {
                    //        var btnAnexo = "<a style='box-shadow: 2px 2px 5px #999 inset' onclick='ModalRegistrarAnexo(" + JSON.stringify(options.data) + ")'>Ingreso</a>";
                    //        var lblEspacio = "<a> </a>"
                    //        var btnAnexo2 = "<a style='box-shadow: 2px 2px 5px #999 inset' onclick='ModalObtenerRutaAnexos(" + JSON.stringify(options.data) + ")'>Consulta</a>";

                    //        $("<div>")
                    //            .append($(btnAnexo), $(lblEspacio), $(btnAnexo2))
                    //            .appendTo(container);
                    //    }
                    //},
                    { dataField: "PruebasLaboratorioCCAId", visible: false },
                    { dataField: "RutaAnexo", visible: false },

                    
                    {
                        caption: "Vizualizar", type: "buttons",
                        buttons: [
                        {
                            text: "Vizualizar",
                                icon: "image",
                                hint: "Vizualizar",
                            onClick: function (e) {
                                // Execute your command here
                                ComprobarImagen(e.row.data);
                            }
                        }]
                    },
                    {
                        dataField: "FechaPrueba", caption: "Fecha Ingreso", alignment: "left", dataType: "date", allowHeaderFiltering: true, allowSearch: false,
                       
                    },
                    {
                        dataField: "CodigoIngreso", caption: "Codigo Ingreso", alignment: "left", allowHeaderFiltering: true, allowSearch: true,
                       
                    },
                    {
                        dataField: "CodigoBateria", caption: "Codigo Bateria", alignment: "left", allowHeaderFiltering: true, allowSearch: true, 
                      
                    },          
                    {
                        dataField: "TipoBateria", caption: "Tipo Bateria", alignment: "left", allowHeaderFiltering: true, allowSearch: true,
                      
                    },
                    {
                        dataField: "Modelo", caption: "Modelo", alignment: "left", allowHeaderFiltering: true, allowSearch: true, headerFilter: {
                            allowSearch: true,
                        },
                      
                    },
                    {
                        dataField: "Separador", caption: "Separador", alignment: "left", allowHeaderFiltering: true, allowSearch: true,
                       
                    },
                    {
                        dataField: "LoteEnsamble", caption: "Lote de Ensamble", alignment: "right", allowHeaderFiltering: false, allowSearch: true,
                        
                    },
                    {
                        dataField: "LoteCarga", caption: "Lote de Carga", alignment: "right", allowHeaderFiltering: false, allowSearch: true,
                       
                    },
                    {
                        dataField: "Peso", caption: "Peso", alignment: "right", allowHeaderFiltering: false, allowSearch: false,
                        format: {
                            type: "fixedPoint",
                            precision: 2,

                        },
                       
                    },
                    {
                        dataField: "Voltaje", caption: "Voltaje", alignment: "right", allowHeaderFiltering: false, allowSearch: false,
                        format: {
                            type: "fixedPoint",
                            precision: 2,

                        },
                       
                    },
                    {
                        dataField: "Temperatura", caption: "Temperatura", alignment: "right", allowHeaderFiltering: false, allowSearch: false,
                        format: {
                            type: "fixedPoint",
                            precision: 1,

                        },
                       
                    },
                    {
                        dataField: "DatoTeoricoPrueba", caption: "Dato Teorico Prueba", alignment: "right", allowHeaderFiltering: false, allowSearch: false,
                        format: {
                            type: "fixedPoint",
                            precision: 2,
                        },
                       
                    },
                    {
                        dataField: "ResultadoFinal", caption: "Resultado Final", alignment: "right", allowHeaderFiltering: false, allowSearch: false,
                        
                    },
                    {
                        dataField: "Rendimiento", caption: "Rendimiento", alignment: "right", allowHeaderFiltering: false, allowSearch: false,
                        
                    },
                    {
                        dataField: "Observaciones", caption: "Observaciones", alignment: "right", allowHeaderFiltering: false, allowSearch: true,
                        
                    },
                    {
                        dataField: "FechaRegistro", caption: "Fecha Registro", alignment: "right", dataType: "date", allowHeaderFiltering: false, allowSearch: false, sortOrder: "desc",
                        
                    },
                ],
                summary: {
                    totalItems: [
                        {
                            name: "LoteCarga",
                            column: "LoteCarga",
                            displayFormat: "Cantidad Total",
                            showInColumn: "LoteCarga",
                            customizeText: function (e) {
                                return "Promedios: ";
                            }
                        },
                        {
                            column: "Temperatura",
                            summaryType: "avg",
                            showInColumn: "Temperatura",
                            customizeText: function (e) {
                                if (e.value != 0 && e.value != "") {
                                    const noTruncarDecimales = { maximumFractionDigits: 0 };
                                    ValTotal = (e.value).toLocaleString('en-US', noTruncarDecimales);
                                    $("#txtTemperatura").val(ValTotal);                        
                                    return ValTotal;
                                }
                            }
                        }, {
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
                        },
                        {
                            column: "DatoTeoricoPrueba",
                            summaryType: "avg",
                            showInColumn: "DatoTeoricoPrueba",
                            customizeText: function (e) {
                                if (e.value != 0 && e.value != "") {
                                    var valorNom = (e.value).toFixed();
                                    const noTruncarDecimales = { maximumFractionDigits: 1 };
                                    ValTotal = (e.value).toLocaleString('en-US', noTruncarDecimales);
                                    valorteoricoPr = ValTotal;
                                    $("#txtValorNominal").val(valorNom);
                                    return ValTotal;
                                }
                            }
                        },  {
                            column: "Rendimiento",
                            summaryType: "avg",
                            showInColumn: "Rendimiento",
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

                                    resultadofinalPr = ValTotal;
                                    return ValTotal;
                                }
                            }
                        },
                    ],
                },
                onContentReady: function (e) {
                    DatosFiltradosTabla();
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
        return storageLocale != null ? storageLocale : 'en';
    }
}
function ComprobarImagen(model) {
    console.log("la ruta tiene: " + model.RutaAnexo);

    if (model.RutaAnexo == null || model.RutaAnexo == "") {
        $("#MensajeSinImagenRegistrada").show('fade');
        setTimeout(function () {
            $("#MensajeSinImagenRegistrada").fadeOut(1500);
        }, 3000); return;
    } else {
        console.log("Posee Imagen");
        MostrarImagen(model.RutaAnexo);
    }
    
}
function MostrarImagen(valor) {
    document.getElementById("ImagenPruebaLaboratorio").src = "../Images/AnexosCCA/" + valor;
    $("#ModalImagenReferencia").modal("show");
}

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

function DatosFiltradosTabla() {
    const filterExpr = $("#tblPruebasLaboratorioRegistrados").dxDataGrid("instance").getCombinedFilter(true);
    $("#tblPruebasLaboratorioRegistrados").dxDataGrid("instance").getDataSource()
        .store()
        .load({ filter: filterExpr })
        .then((result) => {
            valor = result;
        });
}

function ChartResumenesGarantias() {
    const valorModel = valor.find(element => element.Modelo != valor[0].Modelo);
    const valorEnsayo = valor.find(element => element.TipoEnsayo != valor[0].TipoEnsayo);
    var valorTemp = null;

    if (valorModel != null) {
        $("#MensajeDobleModelo").show('fade');
        setTimeout(function () {
            $("#MensajeDobleModelo").fadeOut(1500);
        }, 3000); return;
    }
    else {
        if (valorEnsayo != null) {
            $("#MensajeDobleTipoEnsayo").show('fade');
            setTimeout(function () {
                $("#MensajeDobleTipoEnsayo").fadeOut(1500);
            }, 3000); return;
        } else {

            $("#lblDetallePackingList").text("Analisis Pruebas Laboratorio - Modelo " + valor[0].Modelo);

            calificacionPr = (resultadofinalPr / valorteoricoPr) * 100;

            $("#txtPromedioCalificacion").val(calificacionPr.toFixed());
            $("#ModalInformeGrafica").modal("show");

            if (char != null) {
                char.destroy();
            }
            var ctx = $("#myChart")

            var nombre = [];
            var stock = [];
            var stock2 = [];
            var minimo = [];
            let pruebaValores = [];

            valorTemp = valor;
            var color = ['rgba(255, 99, 132, 0.2)', 'rgba(54, 162, 235, 0.2)', 'rgba(255, 206, 86, 0.2)', 'rgba(75, 192, 192, 0.2)', 'rgba(153, 102, 255, 0.2)', 'rgba(255, 159, 64, 0.2)', 'rgba(224, 18, 248, 0.2)', 'rgba(248, 237, 18, 0.2)', 'rgba(18, 248, 237, 0.2)', 'rgba(179, 6, 22, 0.2)', 'rgba(0, 61, 252, 0.2) '];

            for (var i in valorTemp) {
                pruebaValores.push({ Resultado: valorTemp[i].ResultadoFinal, Fecha: valorTemp[i].FechaRegistro });
                nombre.push(valorTemp[i].CodigoIngreso);
                stock.push(valorTemp[i].ResultadoFinal);
                minimo.push((parseInt(valorTemp[i].DatoTeoricoPrueba) * 0.9).toFixed(0));
                stock2.push(valorTemp[i].DatoTeoricoPrueba);
                //stock2.push(result);
                var valNom = $("#txtValorNominal").val();

                $("#txtValorNominal").val(parseInt(valNom));
                $("#txtValorObjetivo").val((parseInt(valNom) * 0.9).toFixed(0));
                nominal = (parseInt(valNom) * 0.9).toFixed(0);
                nominalReal = (parseInt(valNom)).toFixed(0);
            }

            var chartdata = {
                labels: nombre,
                datasets: [{
                    label: "Resultado:",
                    backgroundColor: color,
                    borderColor: color,
                    borderWidth: 2,
                    cubicInterpolationMode: 'monotone',
                    backgroundColor: 'rgba(7,59,251,0.5)',// Color de fondo
                    borderColor: 'rgba(7,59,251,0.5)',// Color del borde
                    data: stock,
                    pointRadius: 3,
                    pointHoverRadius: 4,
                    pointHitRadius: 10,
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
                    pointRadius: 0,
                    pointHoverRadius: 1,
                    pointHitRadius: 1,
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
                    title: {
                        display: true,
                        text: 'Tipo de ensayo ' + valorTemp[0].TipoEnsayo,
                        fontSize: 18,
                    },

                }

            });
        }
    }

}

function GenerarPdf() {
    var canvas = document.getElementById('myChart');
    var dataURL = canvas.toDataURL();
    SetViewBag(dataURL);

    console.log("valor de nominal " + nominal + " y el real " + nominalReal);
    var url = "../Calidad/GenerarPdfReporteCCaLocales?Nominal=" + nominal;
    window.open(url);
}

function AbrirModalEnvio() {
    $("#ModalEnvioCorreoElectronico").modal("show");
    var canvas = document.getElementById('myChart');
    var dataURL = canvas.toDataURL();
    SetViewBag(dataURL);
}
function EnviarPdf() {
    $('#BtnEnvio').prop('disabled', true);
    $("#BtnEnvio").text("Enviando...");

    if ($("#txtCorreoDestino").val() == "" || $("#txtCorreoCopia").val() == "") {
        $('#BtnEnvio').prop('disabled', false);
        $("#BtnEnvio").text("Enviar");

        $("#MensajeCompleteCorreo").show('fade');
        setTimeout(function () {
            $("#MensajeCompleteCorreo").fadeOut(1500);
        }, 3000);
    } else {
        $.ajax({
            url: '/Calidad/EnviarPdfReporteCCALocal',
            type: 'POST',
            data: { Nominal: nominal, Correo: $("#txtCorreoDestino").val(), CorreoCopia: $("#txtCorreoCopia").val() },
            success: function (msg) {
                $('#BtnEnvio').prop('disabled', false);
                $("#BtnEnvio").text("Enviar");

                $("#txtCorreoDestino").val("");
                $("#txtCorreoCopia").val("");

                $("#ModalEnvioCorreoElectronico").modal("hide");
                $("#ModalInformeGrafica").modal("hide");

                $("#MensajeRespuestaEnvio").text(msg);
                $("#MensajeRespuestaEnvio").show('fade');
                setTimeout(function () {
                    $("#MensajeRespuestaEnvio").fadeOut(1500);
                }, 3000); return;
            },
            error: function (msg) {
                console.log("error");
            }
        })
    }
}

function SetViewBag(val) {
    $.ajax({
        type: 'POST',
        url: '/Calidad/GuardarViewBagTempPruebasCCA',
        dataType: 'json',
        data: { chart: val, registros: valor },
        success: function () {

        },
    })
}
