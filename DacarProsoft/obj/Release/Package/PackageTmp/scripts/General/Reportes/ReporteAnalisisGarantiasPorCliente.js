var anio1 = null;
var char;
var char2;
var char3;
var char4;
var anio2 = null;

$(document).ready(function () {
    $("#txtMsjGarantia").hide();
    $('.js-example-basic-single').select2();

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
    var txtCliente = $("#txtCliente option:selected").text();
    var txtFecha = $("#txtFecha option:selected").text();

    if (txtCliente == "--Seleccione el cliente--" || txtFecha == "--Selecione el año--") {
        $(".btn").attr("disabled", false);
        $(".btn-txt").text("Consultar");
        $("#MensajeIngreseTodosLosCampos").show('fade');
        setTimeout(function () {
            $("#MensajeIngreseTodosLosCampos").fadeOut(1500);
        }, 3000);
    }
    else {
        ConsultarReporteClienteMes();
    }
}

function ConsultarReporteClienteMes() {
    var cliente = $("#txtCliente option:selected").text();
    var fecha = $("#txtFecha option:selected").text();
    $.ajax({
        url: "../Reportes/ReporteDetalleGarantiaPorNombreClienteMeses?NombreCliente=" + cliente + " &AnioConsulta=" + fecha,
        type: "GET"
        , success: function (msg) {
            if (msg.length != 0) {
                $(".btn").attr("disabled", false);
                $(".btn-txt").text("Consultar");
                $("#tblGridResumenCliente").dxDataGrid({
                    dataSource: msg,
                    showBorders: true,
                    columnAutoWidth: true,
                    showBorders: true,
                    columns: [{
                        caption: "Resultados Año " + fecha + " del cliente:" + cliente, alignment: "center",
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
                ChartResumenesGarantias(msg);
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

function ChartResumenesGarantias(Listado) {
    var acumulado = 0;

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

    if (char != null) {
        char.destroy();
    }
    var ctx = $("#myChart")

    var nombre = ["Enero", "Febrero", "Marzo", "Abril", "Mayo", "Junio", "Julio", "Agosto", "Septiembre", "Octubre", "Noviembre", "Diciembre"];
    var stock = [];
    var color = ['rgba(255, 99, 132, 0.2)', 'rgba(54, 162, 235, 0.2)', 'rgba(255, 206, 86, 0.2)', 'rgba(75, 192, 192, 0.2)', 'rgba(153, 102, 255, 0.2)', 'rgba(255, 159, 64, 0.2)', 'rgba(224, 18, 248, 0.2)', 'rgba(248, 237, 18, 0.2)', 'rgba(18, 248, 237, 0.2)', 'rgba(179, 6, 22, 0.2)', 'rgba(0, 61, 252, 0.2) '];
    var bordercolor = ['rgba(255,99,132,1)', 'rgba(54, 162, 235, 1)', 'rgba(255, 206, 86, 1)', 'rgba(75, 192, 192, 1)', 'rgba(153, 102, 255, 1)', 'rgba(255, 159, 64, 1)'];

    for (var i in Listado) {
        acumulado = acumulado + Listado[i].Valor;
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

    var DataTotales = {
        labels: nombre,
        datasets: [dataPrimero]
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
    // ChartResumenesGarantias2(acumulado);
    ChartResumenesGarantias2(Listado);
}


function ChartResumenesGarantias2(Listado) {
    var acum = 0;
    if (char2 != null) {
        char2.destroy();
    }
    var ctx = $("#myChart2")
    var nombre = [];
    var stock = [];
    var color = ['rgba(255, 99, 132, 0.2)', 'rgba(54, 162, 235, 0.2)', 'rgba(255, 206, 86, 0.2)', 'rgba(75, 192, 192, 0.2)', 'rgba(153, 102, 255, 0.2)', 'rgba(255, 159, 64, 0.2)', 'rgba(224, 18, 248, 0.2)', 'rgba(248, 237, 18, 0.2)', 'rgba(18, 248, 237, 0.2)', 'rgba(179, 6, 22, 0.2)', 'rgba(0, 61, 252, 0.2) ', 'rgba(6, 179, 32, 0.2) ', 'rgba(134, 129, 71, 0.2) ', 'rgba(99, 134, 71, 0.2) '];
    var bordercolor = ['rgba(255,99,132,1)', 'rgba(54, 162, 235, 1)', 'rgba(255, 206, 86, 1)', 'rgba(75, 192, 192, 1)', 'rgba(153, 102, 255, 1)', 'rgba(255, 159, 64, 1)'];

    for (var i in Listado) {
        acum = acum + Listado[i].Valor;
    }

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

function ModalConsultarDetalles(data) {
    ModalConsultarInformacionMes(data);
    ModalConsultarInformacionMesPorModelo(data);
}

function ModalConsultarInformacionMes(data) {
    $("#ModalDetalleGarantiaMes").modal("show");
    $.ajax({
        url: "../Reportes/ReporteDetalleGarantiaPorMesesPorCliente?Anio=" + $("#txtFecha option:selected").text() + " &Mes=" + data.Descripcion + " &Cliente=" + $("#txtCliente option:selected").text(),
        type: "GET"
        , success: function (msg) {
            if (msg.length != 0) {
                $("#tblDetalleReporteGarantia").dxDataGrid({
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
    var acum = 0;

    if (char3 != null) {
        char3.destroy();
    }

    var ctx = $("#myChart3");

    var nombre = [];
    var stock = [];
    var color = ['rgba(255, 99, 132, 0.2)', 'rgba(54, 162, 235, 0.2)', 'rgba(255, 206, 86, 0.2)', 'rgba(75, 192, 192, 0.2)', 'rgba(153, 102, 255, 0.2)', 'rgba(255, 159, 64, 0.2)', 'rgba(224, 18, 248, 0.2)', 'rgba(248, 237, 18, 0.2)', 'rgba(18, 248, 237, 0.2)', 'rgba(179, 6, 22, 0.2)', 'rgba(0, 61, 252, 0.2) ', 'rgba(6, 179, 32, 0.2) ', 'rgba(134, 129, 71, 0.2) ', 'rgba(99, 134, 71, 0.2) '];
    var bordercolor = ['rgba(255,99,132,1)', 'rgba(54, 162, 235, 1)', 'rgba(255, 206, 86, 1)', 'rgba(75, 192, 192, 1)', 'rgba(153, 102, 255, 1)', 'rgba(255, 159, 64, 1)'];

    for (var i in Listado) {
        acum = acum + Listado[i].Valor;
    }

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


function ModalConsultarInformacionMesPorModelo(data) {
    $.ajax({
        url: "../Reportes/ReporteDetalleGarantiaPorMesesPorClientePorModelo?Anio=" + $("#txtFecha option:selected").text() + " &Mes=" + data.Descripcion + " &Cliente=" + $("#txtCliente option:selected").text(),
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
                ChartResumenesDetalleGarantiasPorModelo(msg);
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

function ChartResumenesDetalleGarantiasPorModelo(Listado) {
    var acum = 0;

    if (char4 != null) {
        char4.destroy();
    }

    var ctx = $("#myChart4");

    var nombre = [];
    var stock = [];
    var color = ['rgba(255, 99, 132, 0.2)', 'rgba(54, 162, 235, 0.2)', 'rgba(255, 206, 86, 0.2)', 'rgba(75, 192, 192, 0.2)', 'rgba(153, 102, 255, 0.2)', 'rgba(255, 159, 64, 0.2)', 'rgba(224, 18, 248, 0.2)', 'rgba(248, 237, 18, 0.2)', 'rgba(18, 248, 237, 0.2)', 'rgba(179, 6, 22, 0.2)', 'rgba(0, 61, 252, 0.2) ', 'rgba(6, 179, 32, 0.2) ', 'rgba(134, 129, 71, 0.2) ', 'rgba(99, 134, 71, 0.2) '];
    var bordercolor = ['rgba(255,99,132,1)', 'rgba(54, 162, 235, 1)', 'rgba(255, 206, 86, 1)', 'rgba(75, 192, 192, 1)', 'rgba(153, 102, 255, 1)', 'rgba(255, 159, 64, 1)'];

    for (var i in Listado) {
        acum = acum + Listado[i].Valor;
    }

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