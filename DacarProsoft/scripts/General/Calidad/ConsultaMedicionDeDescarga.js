var valor = null;
var temp = null;
var char;
var nominal = null;
var nominalReal = null;
var datos = null;
var idAutodes = null;

$(document).ready(function () {
    ConsultaRegistrosMedicionesDescarga();
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


function ConsultaRegistrosMedicionesDescarga() {
    $.ajax({
        url: "../Calidad/ConsultarRegistrosMedicionesDescargas",
        type: "GET"
        , success: function (msg) {
            const locale = getLocale();
            DevExpress.localization.locale(locale);

            $("#tblPruebasMedicionesDescarga").dxDataGrid({
                dataSource: msg,
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
                repaintChangesOnly: true,
                columns: [

                    { dataField: "PruebaLaboratorioCalidadId", visible: false },
                    {
                        dataField: "FechaIngreso", caption: "Ingreso", alignment: "left", dataType: "date", allowHeaderFiltering: true, allowSearch: false

                    },
                    {
                        dataField: "CodigoIngreso", caption: "Codigo", alignment: "left", allowHeaderFiltering: true, allowSearch: true

                    },
                    {
                        dataField: "Marca", caption: "Marca", alignment: "left", allowHeaderFiltering: true, allowSearch: true,

                    },
                    {
                        dataField: "PreAcondicionamiento", caption: "Pre-Acond.", alignment: "left", allowHeaderFiltering: true, allowSearch: true

                    },
                    {
                        dataField: "TipoBateria", caption: "Tipo Bateria", alignment: "left", allowHeaderFiltering: true, allowSearch: true

                    },
                    {
                        dataField: "Modelo", caption: "Modelo", alignment: "left", allowHeaderFiltering: true, allowSearch: true

                    },
                    {
                        dataField: "Separador", caption: "Separador", alignment: "left", allowHeaderFiltering: true, allowSearch: true

                    },
                    {
                        dataField: "LoteEnsamble", caption: "L. Ensamble", alignment: "right", allowHeaderFiltering: false, allowSearch: true

                    },
                    {
                        dataField: "LoteCarga", caption: "L. Carga", alignment: "right", allowHeaderFiltering: false, allowSearch: true

                    },     
                    {
                        dataField: "Peso", caption: "Peso", alignment: "right", allowHeaderFiltering: false, allowSearch: false,
                        format: {
                            type: "fixedPoint",
                            precision: 2,

                        },

                    },
                    {
                        dataField: "ContadorRegistros", caption: "Cantidad Mediciones", alignment: "right", allowHeaderFiltering: false, allowSearch: true

                    },
                    {
                        caption: "Ingresos",
                        cellTemplate: function (container, options) {
                            var btnAnexo = "<a style='box-shadow: 2px 2px 5px #999 inset' onclick='ConsultaDetalleRegistrosMedicionesDescarga(" + JSON.stringify(options.data) + ")'>Detalle</a>";
                            //var lblEspacio = "<a> </a>"
                            //var btnAnexo2 = "<a style='box-shadow: 2px 2px 5px #999 inset' onclick='ModalObtenerRutaAnexos(" + JSON.stringify(options.data) + ")'>Consulta</a>";
                            $("<div>")
                                .append($(btnAnexo)/*, $(lblEspacio), $(btnAnexo2)*/)
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

    function getLocale() {
        const storageLocale = sessionStorage.getItem('locale');
        return storageLocale != null ? storageLocale : 'en';
    }
}

function ModalRegistrarAnexo(valor) {
    valores = valor;
    $("#ModalAnexosIngreso").modal("show");

}

function GenerarPdf() {
    var canvas = document.getElementById('myChart');
    var dataURL = canvas.toDataURL();
    SetViewBag(dataURL);

    var url = "../Calidad/GenerarPdfReporteAutodescarga?idMeidicionDescarga=" + idAutodes;
    window.open(url);
}

function AbrirModalEnvio() {
    $("#ModalEnvioCorreoElectronico").modal("show");
    var canvas = document.getElementById('myChart');
    var dataURL = canvas.toDataURL();
    SetViewBag(dataURL);
    //var canvas = document.getElementById('myChart');
    //var dataURL = canvas.toDataURL();
    //SetViewBag(dataURL);

    //var url = "../Calidad/GenerarPdfReporte?Nominal=" + nominal;
    //window.open(url);
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
            url: '/Calidad/EnviarPdfReporteAutodescarga',
            type: 'POST',
            data: { idMeidicionDescarga: idAutodes, Correo: $("#txtCorreoDestino").val(), CorreoCopia: $("#txtCorreoCopia").val() },
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
        url: '/Calidad/GuardarViewBagDetalleAutodescarga',
        dataType: 'json',
        data: { chart: val},
        success: function () {
          
        },
    })

}

function ConsultaDetalleRegistrosMedicionesDescarga(modelo) {
    idAutodes = modelo.PruebaLaboratorioCalidadId;
    $.ajax({
        url: "../Calidad/ConsultarDetallesMedicionDescarga",

        data: {
            idMedicion: modelo.PruebaLaboratorioCalidadId
        },
        type: 'post',
        success: function (msg) {

            ChartDetalleAutodescarga(msg,modelo.Modelo);

            const locale = getLocale();
            DevExpress.localization.locale(locale);

            $("#tblDetalleMedicionesDescarga").dxDataGrid({
                dataSource: msg,
                showBorders: true,
                columnAutoWidth: true,
                showBorders: true,
                showColumnLines: true,
                showRowLines: true,
                rowAlternationEnabled: false,
                allowColumnReordering: true,
                allowColumnResizing: false,             
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
                repaintChangesOnly: true,
                columns: [
                    { dataField: "PruebaLaboratorioCalidadId", visible: false },
                    {
                        dataField: "FechaIngreso", caption: "Ingreso", alignment: "left", dataType: "date", allowHeaderFiltering: true, allowSearch: false
                    },
                    {
                        dataField: "Voltaje", caption: "Voltaje", alignment: "left", allowHeaderFiltering: true, allowSearch: true
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

    function getLocale() {
        const storageLocale = sessionStorage.getItem('locale');
        return storageLocale != null ? storageLocale : 'en';
    }
}


function ChartDetalleAutodescarga(datos,modelo) {
    $("#lblDetalleMedicionDescarga").text("Registros de Autodescargas " + modelo);

            if (char != null) {
                char.destroy();
            }
            var ctx = $("#myChart")

            var nombre = [];
            var voltaje = [];
            var color = ['rgba(255, 99, 132, 0.2)', 'rgba(54, 162, 235, 0.2)', 'rgba(255, 206, 86, 0.2)', 'rgba(75, 192, 192, 0.2)', 'rgba(153, 102, 255, 0.2)', 'rgba(255, 159, 64, 0.2)', 'rgba(224, 18, 248, 0.2)', 'rgba(248, 237, 18, 0.2)', 'rgba(18, 248, 237, 0.2)', 'rgba(179, 6, 22, 0.2)', 'rgba(0, 61, 252, 0.2) '];
            var bordercolor = ['rgba(255,99,132,1)', 'rgba(54, 162, 235, 1)', 'rgba(255, 206, 86, 1)', 'rgba(75, 192, 192, 1)', 'rgba(153, 102, 255, 1)', 'rgba(255, 159, 64, 1)'];
           
            for (var i in datos) {
                nombre.push(datos[i].FechaIngreso);
                voltaje.push(datos[i].Voltaje);
    }
    
            var chartdata = {
                labels: nombre,
                datasets: [{
                    label: 'Voltajes',
                    backgroundColor: color,
                    borderColor: color,
                    borderWidth: 2,
                    cubicInterpolationMode: 'monotone',
                    backgroundColor: 'rgba(7,59,251,0.5)',// Color de fondo
                    borderColor: 'rgba(7,59,251,0.5)',// Color del borde
                    data: voltaje,
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
                                labelString: "Voltajes",
                                fontColor: "black"
                            }
                        }],
                        xAxes: [{
                            scaleLabel: {
                                display: true,
                                labelString: "Fecha",
                                fontColor: "black"
                            },
                            display: false,
                        }],
                    },
                    interaction: {
                        intersect: false,
                    },
                    title: {
                        display: true,
                        text: 'Autodescarga',
                        fontSize: 18,
                    },                
                }  
            });
    $("#ModalMedicionesRegistradas").modal("show");
}