var anio1 = null;
var char;
var char2;
var char3;
var char4;
var anio2 = null;

$(document).ready(function () {
    $(".loading-icon").css("display", "none");
    $(document).on('click', '.fa', function (event) {
        event.preventDefault();
        $(this).closest('tr').remove();
    });
    $("#image").removeClass("hide");
    //$("#lbltabladescriptiva").hide();
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
    var txtFechaInicio = $("#txtFechaInicio option:selected").text();
    var txtFechaFin = $("#txtFechaFin option:selected").text();

    if (txtFechaInicio == "--Selecione el año--" || txtFechaFin == "--Selecione el año--") {
        $(".btn").attr("disabled", false);
        $(".btn-txt").text("Consultar");
        $("#MensajeIngreseTodosLosCampos").show('fade');
        setTimeout(function () {
            $("#MensajeIngreseTodosLosCampos").fadeOut(1500);
        }, 3000);
    }

    else {
        AnalisisGarantiasRegistrados();
    }
}

function AnalisisGarantiasRegistrados() {
    ConsultarPivot();
    // $("#lbltabladescriptiva").show();
   // ConsultarFechaUno();
    //ConsultarFechaDos();
    //ChartResumenesGarantias(x, y);

}


function ConsultarFechaUno() {
    var fechaInicio = $('#txtFechaInicio option:selected').text();
    $.ajax({
        url: "../Reportes/ReporteAnalisisDeGarantiasPorAnio1?Anio=" + fechaInicio ,
        type: "GET"
        , success: function (msg) {
            if (msg.length != 0) {
                $(".btn").attr("disabled", false);
                $(".btn-txt").text("Consultar");
                $("#tblGridResumenAnio1").dxDataGrid({
                    dataSource: msg,
                    showBorders: true,
                    columnAutoWidth: true,
                    showBorders: true,
                    columns: [{
                        caption: "Resultados Año " + fechaInicio, alignment: "center",
                        columns: [
                            {
                                dataField: "Descripcion", caption: "Descripcion"
                            }, {
                                dataField: "Valor", caption: "Cantidad"
                            },
                             {
                                dataField: "Porcentaje", caption: "Porcentaje(%)"
                            },
                            {
                                caption: "Acciones",

                                cellTemplate: function (container, options) {

                                    var btnDetalle = "<button class='btn-primary' onclick='ModalConsultarDetalles(" + JSON.stringify(options.data) + ")'>Detalle</button>";

                                    $("<div>")
                                        .append($(btnDetalle))
                                        .appendTo(container);
                                }
                            }
                        ]
                    }],
                    summary: {
                        totalItems: [
                            {
                                name: "Valor",
                                column: "Valor",
                                summaryType: "sum",
                                displayFormat: "Total: {0}",
                                showInColumn: "Valor",
                                customizeText: function (e) {
                                    if (e.value != 0 && e.value != "") {
                                        return "Total: " + (e.value)
                                    }
                                }
                            },
                            {
                                name: "Porcentaje",
                                column: "Porcentaje",
                                summaryType: "sum",
                                displayFormat: "Total: {0}",
                                showInColumn: "Porcentaje",
                                customizeText: function (e) {
                                    if (e.value != 0 && e.value != "") {
                                        return "100%" //+ (e.value)
                                    }
                                }
                            }],
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

function ConsultarFechaDos(valor) {
    var fechaFin = $('#txtFechaFin option:selected').text();
    $.ajax({
        url: "../Reportes/ReporteAnalisisDeGarantiasPorAnio2?Anio=" + fechaFin,
        type: "GET"
        , success: function (msg) {
            if (msg.length != 0) {
                retorno = msg;

                $(".btn").attr("disabled", false);
                $(".btn-txt").text("Consultar");
                $("#tblGridResumenAnio2").dxDataGrid({
                    dataSource: msg,
                    showBorders: true,
                    columnAutoWidth: true,
                    showBorders: true,
                    columns: [{
                        caption: "Resultados Año " + fechaFin, alignment: "center",
                        columns: [
                            {
                                dataField: "Descripcion", caption: "Descripcion"
                            }, {
                                dataField: "Valor", caption: "Cantidad"
                            }, {
                                dataField: "Porcentaje", caption: "Porcentaje(%)"
                            },
                            {
                                caption: "Acciones",

                                cellTemplate: function (container, options) {

                                    var btnDetalle = "<button class='btn-primary' onclick='ModalConsultarDetalles2(" + JSON.stringify(options.data) + ")'>Detalle</button>";

                                    $("<div>")
                                        .append($(btnDetalle))
                                        .appendTo(container);
                                }
                            }
                        ]
                    }],
                    summary: {
                        totalItems: [
                            {
                                name: "Valor",
                                column: "Valor",
                                summaryType: "sum",
                                displayFormat: "Total: {0}",
                                showInColumn: "Valor",
                                customizeText: function (e) {
                                    if (e.value != 0 && e.value != "") {
                                        return "Total: " + (e.value)
                                    }
                                }
                            },
                            {
                                name: "Porcentaje",
                                column: "Porcentaje",
                                summaryType: "sum",
                                displayFormat: "Total: {0}",
                                showInColumn: "Porcentaje",
                                customizeText: function (e) {
                                    if (e.value != 0 && e.value != "") {
                                        return "100%" //+ (e.value)
                                    }
                                }
                            }],
                    }
                });

                ChartResumenesGarantias(valor, msg)
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
    ConsultarPivot();
}


function ChartResumenesGarantias(Listado, Listado2) {
    var acumulado = 0;
    var acumulado2 = 0;

    var Enero = 0;
    var Febrero = 0;
    var Marzo = 0;
    var Abril = 0;
    var Mayo = 0;
    var Junio = 0;
    var Julio = 0;
    var Agosto = 0;
    var Septiembre = 0;
    var Octubre = 0;
    var Noviembre = 0;
    var Diciembre = 0;

    var Enero2 = 0;
    var Febrero2 = 0;
    var Marzo2 = 0;
    var Abril2 = 0;
    var Mayo2 = 0;
    var Junio2 = 0;
    var Julio2 = 0;
    var Agosto2 = 0;
    var Septiembre2 = 0;
    var Octubre2 = 0;
    var Noviembre2 = 0;
    var Diciembre2 = 0;


    if (char != null) {
        char.destroy();
    }
    var ctx = $("#myChart")

    var nombre = ["Enero", "Febrero", "Marzo", "Abril", "Mayo", "Junio", "Julio","Agosto","Septiembre","Octubre","Noviembre","Diciembre"];
    var stock = [];
    var color = ['rgba(255, 99, 132, 0.2)', 'rgba(54, 162, 235, 0.2)', 'rgba(255, 206, 86, 0.2)', 'rgba(75, 192, 192, 0.2)', 'rgba(153, 102, 255, 0.2)', 'rgba(255, 159, 64, 0.2)', 'rgba(224, 18, 248, 0.2)', 'rgba(248, 237, 18, 0.2)', 'rgba(18, 248, 237, 0.2)', 'rgba(179, 6, 22, 0.2)', 'rgba(0, 61, 252, 0.2) '];
    var bordercolor = ['rgba(255,99,132,1)', 'rgba(54, 162, 235, 1)', 'rgba(255, 206, 86, 1)', 'rgba(75, 192, 192, 1)', 'rgba(153, 102, 255, 1)', 'rgba(255, 159, 64, 1)'];

    for (var i in Listado) {
        acumulado = acumulado + Listado[i].Valor;
       // nombre.push(Listado[i].Descripcion);
        if (Listado[i].Descripcion == "Enero") {
            Enero = Listado[i].Valor;
        }
        if (Listado[i].Descripcion == "Febrero") {
            Febrero = Listado[i].Valor;
        }
        if (Listado[i].Descripcion == "Marzo") {
            Marzo = Listado[i].Valor;
        }
        if (Listado[i].Descripcion == "Abril") {
            Abril = Listado[i].Valor;
        }
        if (Listado[i].Descripcion == "Mayo") {
            Mayo = Listado[i].Valor;
        }
        if (Listado[i].Descripcion == "Junio") {
            Junio = Listado[i].Valor;
        }
        if (Listado[i].Descripcion == "Julio") {
            Julio = Listado[i].Valor;
           // msg[0]['Apellido']
            console.log("Julio:" + Listado[i].Descripcion);
        }
        if (Listado[i].Descripcion == "Agosto") {
            Agosto = Listado[i].Valor;
        }
        if (Listado[i].Descripcion == "Septiembre") {
            Septiembre = Listado[i].Valor;
        }
        if (Listado[i].Descripcion == "Octubre") {
            Octubre = Listado[i].Valor;
        }
        if (Listado[i].Descripcion == "Noviembre") {
            Noviembre = Listado[i].Valor;
        }
        if (Listado[i].Descripcion == "Diciembre") {
            Diciembre = Listado[i].Valor;
        }
        //stock.push(Listado[i].Valor);
    }

    for (var i in Listado2) {
        acumulado2 = acumulado2 + Listado2[i].Valor;
        // nombre.push(Listado[i].Descripcion);
        if (Listado2[i].Descripcion == "Enero") {
            Enero2 = Listado2[i].Valor;
        }
        if (Listado2[i].Descripcion == "Febrero") {
            Febrero2 = Listado2[i].Valor;
        }
        if (Listado2[i].Descripcion == "Marzo") {
            Marzo2 = Listado2[i].Valor;
        }
        if (Listado2[i].Descripcion == "Abril") {
            Abril2 = Listado2[i].Valor;
        }
        if (Listado2[i].Descripcion == "Mayo") {
            Mayo2 = Listado2[i].Valor;
        }
        if (Listado2[i].Descripcion == "Junio") {
            Junio2 = Listado2[i].Valor;
        }
        if (Listado2[i].Descripcion == "Julio") {
            Julio2 = Listado2[i].Valor;
        }
        if (Listado2[i].Descripcion == "Agosto") {
            Agosto2 = Listado2[i].Valor;
        }
        if (Listado2[i].Descripcion == "Septiembre") {
            Septiembre2 = Listado2[i].Valor;
        }
        if (Listado2[i].Descripcion == "Octubre") {
            Octubre2 = Listado2[i].Valor;
        }
        if (Listado2[i].Descripcion == "Noviembre") {
            Noviembre2 = Listado2[i].Valor;
        }
        if (Listado2[i].Descripcion == "Diciembre") {
            Diciembre2 = Listado2[i].Valor;
        }
        //stock.push(Listado[i].Valor);
    }


    var dataPrimero = {
        label: $("#txtFechaInicio option:selected").text(),
        data: [Enero, Febrero, Marzo, Abril, Mayo, Junio, Julio, Agosto, Septiembre, Octubre, Noviembre, Diciembre],
        lineTension: 0.3,
        backgroundColor: 'rgba(7, 229, 115,1)',
        borderColor: 'rgba(7, 229, 115,1)',  
        //borderWidth: 2,
        //hoverBackgroundColor: color,
        //hoverBorderColor: bordercolor,
        //fill: false
        // Set More Options
    };

    var dataSegundo = {
        label: $("#txtFechaFin option:selected").text(),
        data: [Enero2, Febrero2, Marzo2, Abril2, Mayo2, Junio2, Julio2, Agosto2, Septiembre2, Octubre2, Noviembre2, Diciembre2],
        //lineTension: 0.3,
        backgroundColor: 'rgba(255,99,132,1)',
        borderColor: 'rgba(255,99,132,1)',
        //borderWidth: 2,
        //hoverBackgroundColor: color,
        //hoverBorderColor: bordercolor,
        //fill: false
        // Set More Options
    };

    var DataTotales = {
        labels: nombre,
        datasets: [dataPrimero, dataSegundo]
    };

    //var chartdata = {
    //    labels: nombre,
    //    datasets: [{
    //        label: 'Cantidad',
    //        backgroundColor: color,
    //        borderColor: color,
    //        borderWidth: 2,
    //        hoverBackgroundColor: color,
    //        hoverBorderColor: bordercolor,
    //        data: [Enero, Febrero, Marzo, Abril, Mayo, Junio, Julio, Agosto, Septiembre, Octubre, Noviembre, Diciembre],
    //        fill: false
    //    }]
    //};

    char = new Chart(ctx, {
        type: "bar",
        data: DataTotales,
        options: {
            legend: { display: true },
            scales: {
                yAxes: [{
                    ticks: {
                        beginAtZero: true,
                        //max: 20,
                        min: 0

                    }
                }]
            },
            tooltip: {
                valueDecimals: 0
            },
            // responsive: true,
            //legend: {
            //    position: 'bottom',
            //},
            title: {
                display: true,
                text: 'Diagrama Bar'
            },
            //animation: {
            //    animateScale: true,
            //    animateRotate: true
            //}
            //    legend: { display: false }
        }
        //options: {
        //    legend: { display: false }
        //}
    });
    ChartResumenesGarantias2(acumulado, acumulado2);

   
}


function ChartResumenesGarantias2(acum1, acum2) {
    var porcentaje1 = 0;
    var porcentaje2 = 0;
    porcentaje1 = (acum1 * 100) / (acum1 + acum2);
    porcentaje2 = (acum2 * 100) / (acum1 + acum2);

    if (char2 != null) {
        char2.destroy();
    }

    var ctx = $("#myChart2");


    var nombre = [];
    var stock = [];
    var color = ['rgba(255, 99, 132, 0.2)', 'rgba(54, 162, 235, 0.2)', 'rgba(255, 206, 86, 0.2)', 'rgba(75, 192, 192, 0.2)', 'rgba(153, 102, 255, 0.2)', 'rgba(255, 159, 64, 0.2)', 'rgba(224, 18, 248, 0.2)', 'rgba(248, 237, 18, 0.2)', 'rgba(18, 248, 237, 0.2)', 'rgba(179, 6, 22, 0.2)', 'rgba(0, 61, 252, 0.2) ', 'rgba(6, 179, 32, 0.2) ', 'rgba(134, 129, 71, 0.2) ', 'rgba(99, 134, 71, 0.2) '];
    var bordercolor = ['rgba(255,99,132,1)', 'rgba(54, 162, 235, 1)', 'rgba(255, 206, 86, 1)', 'rgba(75, 192, 192, 1)', 'rgba(153, 102, 255, 1)', 'rgba(255, 159, 64, 1)'];
    //console.log(Listado);

    //for (var i in Listado) {
    //    nombre.push(Listado[i].Descripcion);
    //    stock.push(Listado[i].Valor);
    //}

    nombre.push($("#txtFechaInicio option:selected").text());
    stock.push(porcentaje1.toFixed(2));

    nombre.push($("#txtFechaFin option:selected").text());
    stock.push(porcentaje2.toFixed(2));

    

    var chartdata = {
        labels: nombre,
        datasets: [{
            label: 'Cantidad',
            backgroundColor: color,
            borderColor: color,
            borderWidth: 2,
            hoverBackgroundColor: color,
            hoverBorderColor: bordercolor,
            data: stock,
            fill: false
        }]
    };

    char2 = new Chart(ctx, {
        type: "pie",
        data: chartdata,
        options: {
            responsive: true,
            legend: {
                position: 'bottom',
            },
            title: {
                display: true,
                text: 'Diagrama Pie'
            },
            animation: {
                animateScale: true,
                animateRotate: true
            },

            //    legend: { display: false }
        }
    });

}


function ModalConsultarDetalles(data) {
    ModalConsultarInformacionMes(data);
    ModalConsultarInformacionMesPorModeloAñoInicio(data);
}

function ModalConsultarDetalles2(data) {
    ModalConsultarInformacionMes2(data);
    ModalConsultarInformacionMesPorModeloAñoFin(data);

}


function ModalConsultarInformacionMes(data) {
    $("#ModalDetalleGarantiaMes").modal("show");
    $.ajax({
        url: "../Reportes/ReporteDetalleGarantiaPorCausales?Anio=" + $("#txtFechaInicio option:selected").text() + " &Mes=" + data.Descripcion,
        type: "GET"
        , success: function (msg) {
            if (msg.length != 0) {
                $("#tblDetalleCausalesGarantia").dxDataGrid({
                    dataSource: msg,
                    showBorders: true,
                    columnAutoWidth: true,
                    showBorders: true,
                   
                        columns: [
                            {
                                dataField: "Descripcion", caption: "Descripcion"
                            }, {
                                dataField: "Valor", caption: "Cantidad"
                            }, {
                                dataField: "Porcentaje", caption: "Porcentaje(%)"
                            }
                         
                        ],
                    
                    summary: {
                        totalItems: [
                            {
                                name: "Valor",
                                column: "Valor",
                                summaryType: "sum",
                                displayFormat: "Total: {0}",
                                showInColumn: "Valor",
                                customizeText: function (e) {
                                    if (e.value != 0 && e.value != "") {
                                        return "Total: " + (e.value)
                                    }
                                }
                            },
                            {
                                name: "Porcentaje",
                                column: "Porcentaje",
                                summaryType: "sum",
                                displayFormat: "Total: {0}",
                                showInColumn: "Porcentaje",
                                customizeText: function (e) {
                                    if (e.value != 0 && e.value != "") {
                                        return "100%" //+ (e.value)
                                    }
                                }
                            }],
                    }
                });
                ChartResumenesDetalleGarantias(msg);
            } else {

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
function ModalConsultarInformacionMes2(data) {
    $("#ModalDetalleGarantiaMes").modal("show");
    $.ajax({
        url: "../Reportes/ReporteDetalleGarantiaPorCausales?Anio=" + $("#txtFechaFin option:selected").text() + " &Mes=" + data.Descripcion,
        type: "GET"
        , success: function (msg) {
            if (msg.length != 0) {
                $("#tblDetalleCausalesGarantia").dxDataGrid({
                    dataSource: msg,
                    showBorders: true,
                    columnAutoWidth: true,
                    showBorders: true,

                    columns: [
                        {
                            dataField: "Descripcion", caption: "Descripcion"
                        }, {
                            dataField: "Valor", caption: "Cantidad"
                        }, {
                            dataField: "Porcentaje", caption: "Porcentaje(%)"
                        }

                    ],

                    summary: {
                        totalItems: [
                            {
                                name: "Valor",
                                column: "Valor",
                                summaryType: "sum",
                                displayFormat: "Total: {0}",
                                showInColumn: "Valor",
                                customizeText: function (e) {
                                    if (e.value != 0 && e.value != "") {
                                        return "Total: " + (e.value)
                                    }
                                }
                            },
                            {
                                name: "Porcentaje",
                                column: "Porcentaje",
                                summaryType: "sum",
                                displayFormat: "Total: {0}",
                                showInColumn: "Porcentaje",
                                customizeText: function (e) {
                                    if (e.value != 0 && e.value != "") {
                                        return "100%" //+ (e.value)
                                    }
                                }
                            }],
                    }
                });
                ChartResumenesDetalleGarantias(msg);
            } else {

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

function ChartResumenesDetalleGarantias(Listado) {


    if (char3 != null) {
        char3.destroy();
    }

    var ctx = $("#myChart3")


    var nombre = [];
    var stock = [];
    var color = ['rgba(255, 99, 132, 0.2)', 'rgba(54, 162, 235, 0.2)', 'rgba(255, 206, 86, 0.2)', 'rgba(75, 192, 192, 0.2)', 'rgba(153, 102, 255, 0.2)', 'rgba(255, 159, 64, 0.2)', 'rgba(224, 18, 248, 0.2)', 'rgba(248, 237, 18, 0.2)', 'rgba(18, 248, 237, 0.2)', 'rgba(179, 6, 22, 0.2)', 'rgba(0, 61, 252, 0.2) ', 'rgba(6, 179, 32, 0.2) ', 'rgba(134, 129, 71, 0.2) ', 'rgba(99, 134, 71, 0.2) '];
    var bordercolor = ['rgba(255,99,132,1)', 'rgba(54, 162, 235, 1)', 'rgba(255, 206, 86, 1)', 'rgba(75, 192, 192, 1)', 'rgba(153, 102, 255, 1)', 'rgba(255, 159, 64, 1)'];
    console.log(Listado);

    for (var i in Listado) {
        nombre.push(Listado[i].Descripcion);
        stock.push(Listado[i].Valor);
    }

    var chartdata = {
        labels: nombre,
        datasets: [{
            label: 'Cantidad',
            backgroundColor: color,
            borderColor: color,
            borderWidth: 2,
            hoverBackgroundColor: color,
            hoverBorderColor: bordercolor,
            data: stock,
            fill: false
        }]
    };

    char3 = new Chart(ctx, {
        type: "pie",
        data: chartdata,
        options: {
            responsive: true,
            legend: {
                position: 'bottom',
            },
            title: {
                display: true,
                text: 'Diagrama Pie'
            },
            animation: {
                animateScale: true,
                animateRotate: true
            },

            //    legend: { display: false }
        }
    });
}

function ModalConsultarInformacionMesPorModeloAñoInicio(data) {
    $.ajax({
        url: "../Reportes/ReporteDetalleGarantiaPorModelo?Anio=" + $("#txtFechaInicio option:selected").text() + " &Mes=" + data.Descripcion,
        type: "GET"
        , success: function (msg) {
            if (msg.length != 0) {
                $("#tblDetalleReporteGarantiaPorModelo").dxDataGrid({
                    dataSource: msg,
                    showBorders: true,
                    columnAutoWidth: true,
                    showBorders: true,

                    columns: [
                        {
                            dataField: "Descripcion", caption: "Descripcion"
                        }, {
                            dataField: "Valor", caption: "Cantidad"
                        }, {
                            dataField: "Porcentaje", caption: "Porcentaje(%)"
                        }


                    ],
                    summary: {
                        totalItems: [
                            {
                                name: "Valor",
                                column: "Valor",
                                summaryType: "sum",
                                displayFormat: "Total: {0}",
                                showInColumn: "Valor",
                                customizeText: function (e) {
                                    if (e.value != 0 && e.value != "") {
                                        return "Total: " + (e.value)
                                    }
                                }
                            },
                            {
                                name: "Porcentaje",
                                column: "Porcentaje",
                                summaryType: "sum",
                                displayFormat: "Total: {0}",
                                showInColumn: "Porcentaje",
                                customizeText: function (e) {
                                    if (e.value != 0 && e.value != "") {
                                        return "100%" //+ (e.value)
                                    }
                                }
                            }],
                    }
                });
                ChartResumenesDetalleGarantiasPorModeloAñoInicio(msg);
            } else {

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

function ChartResumenesDetalleGarantiasPorModeloAñoInicio(Listado) {
    var acum = 0;

    if (char4 != null) {
        char4.destroy();
    }

    var ctx = $("#myChart4")


    var nombre = [];
    var stock = [];
    var color = ['rgba(255, 99, 132, 0.2)', 'rgba(54, 162, 235, 0.2)', 'rgba(255, 206, 86, 0.2)', 'rgba(75, 192, 192, 0.2)', 'rgba(153, 102, 255, 0.2)', 'rgba(255, 159, 64, 0.2)', 'rgba(224, 18, 248, 0.2)', 'rgba(248, 237, 18, 0.2)', 'rgba(18, 248, 237, 0.2)', 'rgba(179, 6, 22, 0.2)', 'rgba(0, 61, 252, 0.2) ', 'rgba(6, 179, 32, 0.2) ', 'rgba(134, 129, 71, 0.2) ', 'rgba(99, 134, 71, 0.2) '];
    var bordercolor = ['rgba(255,99,132,1)', 'rgba(54, 162, 235, 1)', 'rgba(255, 206, 86, 1)', 'rgba(75, 192, 192, 1)', 'rgba(153, 102, 255, 1)', 'rgba(255, 159, 64, 1)'];
    console.log(Listado);

    for (var i in Listado) {
        acum = acum + Listado[i].Valor;
    }
    console.log("el acumulado es:" + acum);


    for (var i in Listado) {
        nombre.push(Listado[i].Descripcion);

        stock.push(((Listado[i].Valor * 100) / acum).toFixed(2));
    }
    var chartdata = {
        labels: nombre,
        datasets: [{
            label: 'Cantidad',
            backgroundColor: color,
            borderColor: color,
            borderWidth: 2,
            hoverBackgroundColor: color,
            hoverBorderColor: bordercolor,
            data: stock,
            fill: false
        }]
    };

    char4 = new Chart(ctx, {
        type: "pie",
        data: chartdata,
        options: {
            responsive: true,
            legend: {
                position: 'bottom',
            },
            title: {
                display: true,
                text: 'Diagrama Pie(%)'
            },
            animation: {
                animateScale: true,
                animateRotate: true
            },

            //    legend: { display: false }
        }
    });
}

function ModalConsultarInformacionMesPorModeloAñoFin(data) {
    $.ajax({
        url: "../Reportes/ReporteDetalleGarantiaPorModelo?Anio=" + $("#txtFechaFin option:selected").text() + " &Mes=" + data.Descripcion,
        type: "GET"
        , success: function (msg) {
            if (msg.length != 0) {
                $("#tblDetalleReporteGarantiaPorModelo").dxDataGrid({
                    dataSource: msg,
                    showBorders: true,
                    columnAutoWidth: true,
                    showBorders: true,

                    columns: [
                        {
                            dataField: "Descripcion", caption: "Descripcion"
                        }, {
                            dataField: "Valor", caption: "Cantidad"
                        }, {
                            dataField: "Porcentaje", caption: "Porcentaje(%)"
                        }

                    ],
                    summary: {
                        totalItems: [
                            {
                                name: "Valor",
                                column: "Valor",
                                summaryType: "sum",
                                displayFormat: "Total: {0}",
                                showInColumn: "Valor",
                                customizeText: function (e) {
                                    if (e.value != 0 && e.value != "") {
                                        return "Total: " + (e.value)
                                    }
                                }
                            },
                            {
                                name: "Porcentaje",
                                column: "Porcentaje",
                                summaryType: "sum",
                                displayFormat: "Total: {0}",
                                showInColumn: "Porcentaje",
                                customizeText: function (e) {
                                    if (e.value != 0 && e.value != "") {
                                        return "100%" //+ (e.value)
                                    }
                                }
                            }],
                    }
                });
                ChartResumenesDetalleGarantiasPorModeloAñoFin(msg);
            } else {

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

function ChartResumenesDetalleGarantiasPorModeloAñoFin(Listado) {
    var acum = 0;

    if (char4 != null) {
        char4.destroy();
    }

    var ctx = $("#myChart4")


    var nombre = [];
    var stock = [];
    var color = ['rgba(255, 99, 132, 0.2)', 'rgba(54, 162, 235, 0.2)', 'rgba(255, 206, 86, 0.2)', 'rgba(75, 192, 192, 0.2)', 'rgba(153, 102, 255, 0.2)', 'rgba(255, 159, 64, 0.2)', 'rgba(224, 18, 248, 0.2)', 'rgba(248, 237, 18, 0.2)', 'rgba(18, 248, 237, 0.2)', 'rgba(179, 6, 22, 0.2)', 'rgba(0, 61, 252, 0.2) ', 'rgba(6, 179, 32, 0.2) ', 'rgba(134, 129, 71, 0.2) ', 'rgba(99, 134, 71, 0.2) '];
    var bordercolor = ['rgba(255,99,132,1)', 'rgba(54, 162, 235, 1)', 'rgba(255, 206, 86, 1)', 'rgba(75, 192, 192, 1)', 'rgba(153, 102, 255, 1)', 'rgba(255, 159, 64, 1)'];
    console.log(Listado);

    for (var i in Listado) {
        acum = acum + Listado[i].Valor;
    }
    console.log("el acumulado es:" + acum);


    for (var i in Listado) {
        nombre.push(Listado[i].Descripcion);

        stock.push(((Listado[i].Valor * 100) / acum).toFixed(2));
    }
    var chartdata = {
        labels: nombre,
        datasets: [{
            label: 'Cantidad',
            backgroundColor: color,
            borderColor: color,
            borderWidth: 2,
            hoverBackgroundColor: color,
            hoverBorderColor: bordercolor,
            data: stock,
            fill: false
        }]
    };

    char4 = new Chart(ctx, {
        type: "pie",
        data: chartdata,
        options: {
            responsive: true,
            legend: {
                position: 'bottom',
            },
            title: {
                display: true,
                text: 'Diagrama Pie(%)'
            },
            animation: {
                animateScale: true,
                animateRotate: true
            },

            //    legend: { display: false }
        }
    });
}



function ConsultarPivot() {
    var fechaUno = $('#txtFechaInicio option:selected').text();
    var fechaDos = $('#txtFechaFin option:selected').text();

    $.ajax({
        url: "../Reportes/PivotDeAnalisisGarantiasAnios?anio1=" + fechaUno + " &anio2=" + fechaDos,
        type: "GET"
        , success: function (msg) {
            if (msg.length != 0) {
                $(".btn").attr("disabled", false);
                $(".btn-txt").text("Consultar");

               
                const locale = getLocale();
                DevExpress.localization.locale(locale);                

                const pivotGridChart = $('#pivotgrid-chart').dxChart({
                    commonSeriesSettings: {
                        type: 'bar',
                    },
                    tooltip: {
                        enabled: true,
                        //format: 'currency',
                        customizeTooltip(args) {
                            var lastword = (args.seriesName).split(" ").pop();
                            console.log("ultima palabra " + lastword);
                            if (lastword == "Porcentaje") {
                                console.log("entro x verdadero");
                                console.log(args.seriesName);
                                return {
                                    html: `${args.seriesName} | Total<div class='currency'>${(args.valueText * 100).toFixed(2)}</div>`,
                                };
                            }
                            else {
                                console.log("entro x falso");
                                console.log(args.seriesName);

                                return {
                                    html: `${args.seriesName} | Total<div class='currency'>${args.valueText}</div>`,
                                };
                            }
                           
                        },
                    },
                    //size: {
                    //    height: 200,
                    //},
                    //adaptiveLayout: {
                    //    width: 450,
                    //},
                }).dxChart('instance');

                
                const pivotGrid =  $('#PivotGridAnalisis').dxPivotGrid({
                    dataFieldArea: 'column',
                    rowHeaderLayout: 'tree',
                    wordWrapEnabled: false,
                    fieldChooser: {
                        enabled: false,
                    },
                    'texts.grandTotal': 'Totales',
                    
                    //onCellPrepared: function (e) {
                    //    if (e.cell.columnType === "GT" || e.cell.rowType === "GT")
                    //        e.cellElement.css("backgroundColor", "lightGreen")
                    //},

                    //onContentReady: function (e) {
                    //    e.element.find(".dx-pivotgrid-horizontal-headers .dx-grandtotal").first().text("Totales");

                    //},
                    dataSource: {
                        fields: [{
                            caption: 'Area Responsable',
                            dataField: 'AreaResponsable',
                            expanded: false,
                            sortBySummaryField: "Total",

                            area: 'row',
                        }, {
                            caption: 'Resumen Analisis',
                            dataField: 'ResumenAnalisis',
                            expanded: false,
                            area: 'row',
                        }, {
                            caption: 'Grupo Bateria',
                            dataField: 'GrupoBateria',
                            area: 'row',
                        }, {
                            caption: 'Modelo Bateria',
                            dataField: 'ModeloBateria',
                            area: 'row',
                         },{
                            dataField: 'FechaRegistro',
                            dataType: 'date',
                            area: 'column',
                        }, {
                            caption: 'Cantidad',
                            dataField: 'Cantidad',
                            dataType: 'number',
                            summaryType: 'sum',
                            //format: 'currency',
                            area: 'data',
                        }, {
                            caption: 'Porcentaje',
                            dataField: 'Cantidad',
                            dataType: 'number',
                            summaryType: 'sum',
                            summaryDisplayMode: 'percentOfRowGrandTotal',
                            area: 'data',
                            //isMeasure: false // allows the end-user to place this field to the data area only

                        }],
                        store: msg,
                    },
                    //fieldChooser: {
                    //    height: 500,
                    //},
                    allowExpandAll: false,

                    showBorders: true,
                    /*height: 540,*/

                }).dxPivotGrid('instance');

                function getLocale() {
                    const storageLocale = sessionStorage.getItem('locale');
                    return storageLocale != null ? storageLocale : 'es';
                }

                pivotGrid.bindChart(pivotGridChart, {
                    //putDataFieldsInto: "series", // "args"

                    dataFieldsDisplayMode: 'splitPanes',
                    //  processCell: function (args) {
                    //    console.log(args);
                    //    args.visible =
                    //        args.rowPath.length == 1 &&
                    //        args.columnPath.length === args.maxColumnLevel;
                    //},

                    alternateDataFields: false,

                    //customizeChart: function (chartOptions) {
                    //    if (chartOptions && chartOptions.valueAxis && chartOptions.valueAxis.length) {
                    //        //chartOptions.valueAxis[0].title = 'Total';
                    //        //chartOptions.valueAxis[0].position = 'right';

                    //        if (chartOptions.valueAxis[1]) {
                    //            //chartOptions.valueAxis[1]
                    //        }
                    //        //chartOptions.valueAxis[1].visible = false;
                    //        //chartOptions.valueAxis[1].label.visible = false;
                           
                    //    }

                    //    return chartOptions;
                    //},             
                 
                  
                });       
                
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