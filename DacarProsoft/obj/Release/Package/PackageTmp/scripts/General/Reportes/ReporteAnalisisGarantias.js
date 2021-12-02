var char;
var char2;
var type = null;
var type2 = null;


$(document).ready(function () {
    $(".loading-icon").css("display", "none");
    $(document).on('click', '.fa', function (event) {
        event.preventDefault();
        $(this).closest('tr').remove();
    });
    $("#image").removeClass("hide");
    $("#lbltabladescriptiva").hide();
    
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
    $(".result").text("");
    $(".loading-icon").removeClass("hide");
    $(".btn").attr("disabled", true);
    $(".btn-txt").text("Espere...");


    var txtFechaInicio = $("#txtFechaInicio").val();
    var txtFechaFin = $("#txtFechaFin").val();
    var txtSelect = $("#SelectEstado option:selected").val();

    if (txtFechaInicio.length == 0 || txtFechaFin.length == 0 || txtSelect.length == 0) {
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
    $("#lbltabladescriptiva").show();

    var tipo = $("#SelectEstado option:selected").val();
    var fechaInicio = $('#txtFechaInicio').val();
    var fechaFin = $('#txtFechaFin').val();
    $.ajax({
        url: "../Reportes/ReporteAnalisisDeGarantias?Filtro=" + tipo + "&FechaInicio=" + fechaInicio + "&FechaFin=" + fechaFin,
        type: "GET"
        , success: function (msg) {
            if (msg.length != 0) {
                $(".btn").attr("disabled", false);
                $(".btn-txt").text("Consultar");

                $("#tblGridResumen").dxDataGrid({
                    dataSource: msg,
                    showBorders: true,
                    columnAutoWidth: true,
                    showBorders: true,
                    columns: [
                        {
                            dataField: "Descripcion", caption: "Descripcion"
                        }, {
                            dataField: "Valor", caption: "Cantidad"
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
                            }],
                    }
                });
                //chart.destroy();
                // chart2.destroy();

                if (tipo == 1) {
                    type = "bar";
                    type2 = "pie";
                }
                if (tipo == 2) {
                    type = "line";
                    type2 = "pie";
                }
                if (tipo == 3) {
                    type = "bar";
                    type2 = "pie";
                }
                if (tipo == 4) {
                    type = "bar";
                    type2 = "pie";
                }
                if (tipo == 5) {
                    type = "bar";
                    type2 = "pie";
                }

                ChartResumenesGarantias(msg, type);
                ChartResumenesGarantias2(msg, type2);


              
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

function ChartResumenesGarantias(Listado, tipo) {

    if (char!=null) {
        char.destroy();
    }
    var ctx = $("#myChart")

    var nombre = [];
    var stock = [];
    var color = ['rgba(255, 99, 132, 0.2)', 'rgba(54, 162, 235, 0.2)', 'rgba(255, 206, 86, 0.2)', 'rgba(75, 192, 192, 0.2)', 'rgba(153, 102, 255, 0.2)', 'rgba(255, 159, 64, 0.2)', 'rgba(224, 18, 248, 0.2)', 'rgba(248, 237, 18, 0.2)', 'rgba(18, 248, 237, 0.2)', 'rgba(179, 6, 22, 0.2)','rgba(0, 61, 252, 0.2) '];
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

    char= new Chart(ctx, {
        type: tipo,
        data: chartdata,
        options: {
            legend: { display: false },
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
}

function ChartResumenesGarantias2(Listado, tipo) {


    if (char2!=null) {
        char2.destroy();
    }

    var ctx = $("#myChart2")


    var nombre = [];
    var stock = [];
    var color = ['rgba(255, 99, 132, 0.2)', 'rgba(54, 162, 235, 0.2)', 'rgba(255, 206, 86, 0.2)', 'rgba(75, 192, 192, 0.2)', 'rgba(153, 102, 255, 0.2)', 'rgba(255, 159, 64, 0.2)', 'rgba(224, 18, 248, 0.2)', 'rgba(248, 237, 18, 0.2)', 'rgba(18, 248, 237, 0.2)', 'rgba(179, 6, 22, 0.2)', 'rgba(0, 61, 252, 0.2) '];
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

    char2 = new Chart(ctx, {
        type: tipo,
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
