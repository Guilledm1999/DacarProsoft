var valor = null;
var char;
var chartGen;
var chartGenPes;
var chartGenMes;
var chartGenProveedores;
var char2;
var char3;
var char4;
var char5;
var char6;
var char7;
var char8;
var char9;
var char10;
var char11;
var char12;
var pesoTotalIng = null;
var cantidadTotalIng = null;
var desviacionTotalCal = null;
var va1 = 0;
var pesoComprasUd = 0;
var pesoNc = 0;
var pesoGta = 0;
var va2 = 0;
var va3 = 0;
var va4 = 0;
var sumTotal = 0;
var pesProNc = 0;
var pesProCom = 0;
var pesProComKg = 0;
var pesProComGta = 0;
var kgproNcCo = 0;
var cantidadAproxCompKg = 0;
var desvNcGen = 0;
var desvGtaGen = 0;
var desvComGen = 0;
var desvComKgGen = 0;
var desvGeneralCha = 0;
var precioIngresoNc = 0;
var precioIngresoGta = 0;
var precioIngresoCo = 0;
var precioIngresoCoKg = 0;
var cantTotalDataGrid = 0;
var preTotalDataGrid = 0;
var pesTotalDataGrid = 0;
var resultJax = null;
var contadorEven = 0;
var valorTempCantProm = 0;
var resBusAnioAnt = null;
var precHisAnt = 0;
var pesoHisAnt = 0;
var cantHisAnt = 0;
var pesoPromHistAnt = 0;
var precioPromHistAnt = 0;
var tempValoresHist = null;

$(document).ready(function () {
    $(".loading-icon").css("display", "none");
    consultarResAnterior();
//    ConsultaDeIngresos();
});

function opDetalleGeneral(op) {
    ChartResumenesChatarras();
}

$('#LinkClose25').on("click", function (e) {
    $("#MensajeErrorDobleConsulta").hide('fade');
});
//datGridInstance

function ResetOpciones() {
    $("#selecTipoIngreso").data('dxTagBox').reset();
    $("#selectMes").data('dxTagBox').reset();
    var selectBox = $("#selecTipoCliente").dxSelectBox("instance");
    selectBox.option('value', 'Todos');
    var selectBox2 = $("#selecClienteLinea").dxSelectBox("instance");
    selectBox2.option('value', 'Todos');
    var selectBox3 = $("#selecClienteClase").dxSelectBox("instance");
    selectBox3.option('value', 'Todos');
    //$("#selecTipoCliente").data('dxselectbox').value === 'Todos';
    //.value === 'Todos'
}
function ConsultarIngresoAnterior() {
    var select = $("#anioClass option:selected").text();
    var res;
    $.ajax({
        url: "../Chatarra/ConsultaIngresosChatarraAnioAnterior?anio=" + select,
        type: "GET",
        async: false,
        success: function (msg) {
            res = msg;
        },
        error: function (msg) {
            $("#MensajeErrorGeneral").show('fade');
            setTimeout(function () {
                $("#MensajeErrorGeneral").fadeOut(1500);
            }, 3000);
        }
    })
    return res;
}

function ConsultaDeIngresos() {
    $(".result").text("");
    $(".loading-icon").removeClass("hide");
    $(".btn").attr("disabled", true);
    $(".btn-txt").text("Espere...");
     va1 = 0;
     pesoComprasUd = 0;
     pesoNc = 0;
     pesoGta = 0;
     va2 = 0;
     va3 = 0;
     va4 = 0;
     sumTotal = 0;
     pesProNc = 0;
     pesProCom = 0;
     pesProComKg = 0;
     pesProComGta = 0;
     kgproNcCo = 0;
     cantidadAproxCompKg = 0;
     desvNcGen = 0;
     desvGtaGen = 0;
     desvComGen = 0;
     desvComKgGen = 0;
     desvGeneralCha = 0;
     precioIngresoNc = 0;
     precioIngresoGta = 0;
     precioIngresoCo = 0;
    precioIngresoCoKg = 0;

    InformeIngresosDeChatarra();
}

function obtenerDesv() {
    var respVari;
    $.ajax({
        async: false,
        url: "../Chatarra/ConsultarDesviacionChatarra",
        type: "GET",
        success: function (response) {
            respVari = response;
        },
        error: function (error) {
        }
    });
    return respVari;
}

function chartGeneralPesos(valor, pesoTo) {
    const noTruncarDecimales = { maximumFractionDigits: 2, minimumFractionDigits: 2 };
    var color = ['rgba(7,59,251,0.5)', 'rgba(225, 17, 17,0.5)', 'rgba(229, 236, 14,0.5)', 'rgba(236, 112, 14,0.5)'];
    var descrip = [];
    var cantidad = [];

    if (chartGenPes != null) {
        chartGenPes.destroy();
    }
    for (var i in valor) {
        descrip.push(valor[i].descripcion);
        cantidad.push(((valor[i].peso * 100) / pesoTo).toFixed(0));
    }
    var ctx = $("#myChartGeneralPesos");
    ctx.attr('width', 40);

    var chartdata = {
        labels: descrip,
        datasets: [{
            label: "Cantidad:",
            borderWidth: 2,
            cubicInterpolationMode: 'monotone',
            backgroundColor: color,// Color de fondo
            borderColor: color,// Color del borde
            data: cantidad,
            pointRadius: 3,
            pointHoverRadius: 4,
            pointHitRadius: 10,
            fill: false,
            yAxisID: 'A',
        }
        ]
    };
    chartGenPes = new Chart(ctx, {
        type: "pie",
        data: chartdata,
        options: {
            responsive: true,
            title: {
                display: true,
                text: 'Kg Chatarra(%)',
                fontSize: 12,
            },
            legend: {
                position: 'left'
            },         
            tooltips: {
                callbacks: {
                    label: function (tooltipItem, chart) {
                        var datasetLabel = chart.datasets[tooltipItem.datasetIndex].label || '';
                        return datasetLabel + " " + (chart['datasets'][0]['data'][tooltipItem['index']])+"%";
                    }
                }
            },
           
        }
    });
}
function consultarResAnterior() {
    var select = $("#anioClass option:selected").text();
    const noTruncarDecimales = { maximumFractionDigits: 2, minimumFractionDigits: 2 };
    const noTruncarDecimales2 = { maximumFractionDigits: 0, minimumFractionDigits: 0 };

    var contAnioAnte = ConsultarIngresoAnterior();

    resBusAnioAnt = contAnioAnte;
    tempValoresHist = contAnioAnte;
    var cantidad = 0;
    var peso = 0;
    var precio = 0;
    //for (var i in contAnioAnte) {
    //    cantidad = cantidad + contAnioAnte[i].cantidad;
    //    peso = peso + contAnioAnte[i].peso;
    //    precio = precio + contAnioAnte[i].precio;
    //}
    for (var i in contAnioAnte) {
        cantidad = cantidad + contAnioAnte[i].Cantidad;
        peso = peso + contAnioAnte[i].Peso;
        precio = precio + contAnioAnte[i].Precio;
    }
    var can = cantidad.toLocaleString('en-US', noTruncarDecimales2);
    var pes = (peso / 1000).toLocaleString('en-US', noTruncarDecimales2);
    var prec = precio.toLocaleString('en-US', noTruncarDecimales);
    var pesoPro = (peso / cantidad).toFixed(2);
    var precPro = (precio / peso).toFixed(2);

     precHisAnt = precio;
     pesoHisAnt = peso;
    cantHisAnt = cantidad;
    pesoPromHistAnt = pesoPro;
    precioPromHistAnt = precPro;

    $("#cantAnioAnteriorCha").text((parseInt(select) - 1) + ": " + can);
    $("#pesAnioAnteriorCha").text((parseInt(select) - 1) + ": " + pes);
    $("#precAnioAnteriorCha").text((parseInt(select) - 1) + ": " + prec);
    $("#pesoAnioAnteriorCha").text((parseInt(select) - 1) + ": " + pesoPro);
    $("#precioPromAnioAnteriorCha").text((parseInt(select) - 1) + ": " + precPro);
}

function crearTablaDescriptivaAndChart(val) {
    if (contadorEven == 1) {       
        va1 = 0;
        pesoComprasUd = 0;
        pesoComprasKg = 0;
        pesoNc = 0;
        pesoGta = 0;
        va2 = 0;
        va3 = 0;
        va4 = 0;
        sumTotal = 0;
        pesProNc = 0;
        pesProCom = 0;
        pesProComKg = 0;
        pesProComGta = 0;
        kgproNcCo = 0;
        cantidadAproxCompKg = 0;
        desvNcGen = 0;
        desvGtaGen = 0;
        desvComGen = 0;
        desvComKgGen = 0;
        desvGeneralCha = 0;
        precioIngresoNc = 0;
        precioIngresoGta = 0;
        precioIngresoCo = 0;
        precioIngresoCoKg = 0;
        precioProIngresoNc = 0;
        precioProIngresoGta = 0;
        precioProIngresoCo = 0;
        precioProIngresoCoKg = 0;

        var valoresParaChart = [];
        const noTruncarDecimales = { maximumFractionDigits: 2, minimumFractionDigits: 2 };
        const noTruncarDecimales2 = { maximumFractionDigits: 0, minimumFractionDigits: 0 };
        for (var x of val) {
            if (x.Tipo_Ingreso == "Compras (Kg)") {
                va1 = x.Cantidad + va1;
                pesoComprasKg = x.Peso_Real + pesoComprasKg;
                desvComKgGen = desvComKgGen + x.Peso_Teorico;
                precioIngresoCoKg = precioIngresoCoKg + x.Precio

            }
            if (x.Tipo_Ingreso == "Compras (Ud)") {
                va2 = x.Cantidad + va2;
                pesoComprasUd = x.Peso_Real + pesoComprasUd;
                desvComGen = desvComGen + x.Peso_Teorico;
                precioIngresoCo = precioIngresoCo + x.Precio

            }
            if (x.Tipo_Ingreso == "Nota Credito") {
                va3 = x.Cantidad + va3;
                pesoNc = x.Peso_Real + pesoNc;
                desvNcGen = desvNcGen + x.Peso_Teorico;
                precioIngresoNc = precioIngresoNc + x.Precio

            }
            if (x.Tipo_Ingreso == "Garantia") {
                va4 = x.Cantidad + va4;
                pesoGta = x.Peso_Real + pesoGta;
                desvGtaGen = desvGtaGen + x.Peso_Teorico;
                precioIngresoGta = precioIngresoGta + x.Precio
            }
        }
        pesProNc = pesoNc / va3;
        pesProCom = pesoComprasUd / va2;
        pesProComKg = pesoComprasKg  / va1;
        pesProComGta = (pesoGta / va4);
        //cantidadAproxCompKg = va1 / pesProComKg;

        kgproNcCo = (pesProNc + pesProCom + pesProComKg) / 3;

        sumTotal = pesoComprasKg + pesoComprasUd + pesoNc + pesoGta;

        var cantTotalGene = va3 + va2 + cantidadAproxCompKg + va4;
        var pesoTotalGene = (pesoNc + pesoComprasUd + va1 + pesoGta) / 1000;
        var precioTotalGene = precioIngresoNc + precioIngresoCo + precioIngresoCoKg + precioIngresoGta;

        precioProIngresoNc = precioIngresoNc / va3;
        precioProIngresoGta = precioIngresoGta / va4;
        precioProIngresoCo = precioIngresoCo / va2;
        precioProIngresoCoKg = precioIngresoCoKg / va1;


        let chaNC = {
            "descripcion": "Nota Credito",
            "cantidad": va3,
            "peso": pesoNc.toFixed(2),
            "precio": precioIngresoNc.toFixed(2),
        }
        let chaCo = {
            "descripcion": "Compras (Ud)",
            "cantidad": va2,
            "peso": pesoComprasUd.toFixed(2),
            "precio": precioIngresoCo.toFixed(2),
        }
        let chaCoKg = {
            "descripcion": "Compras (Kg)",
            "cantidad": va1,
            "peso": pesoComprasKg.toFixed(2),
            "precio": precioIngresoCoKg.toFixed(2),
        }
        let chaGtaKg = {
            "descripcion": "Garantias",
            "cantidad": va4.toFixed(),
            "peso": pesoGta.toFixed(2),
            "precio": precioIngresoGta.toFixed(2),
        }
        valoresParaChart.push(chaNC);
        valoresParaChart.push(chaCoKg);
        valoresParaChart.push(chaCo);
        valoresParaChart.push(chaGtaKg);

        //chartGeneral(valoresParaChart);
        //chartGeneralPesos(valoresParaChart, sumTotal);
        graficoMesesGeneral(val);
        graficoProveedoresGeneral(val);


        if ($("#tblChaNc tr").length > 1) {
            document.getElementById("filaTemp").remove();
        }
        if ($("#tblChaGta tr").length > 1) {
            document.getElementById("filaTemp").remove();
        }
        if ($("#tblChaCom tr").length > 1) {
            document.getElementById("filaTemp").remove();
        }
        if ($("#tblChaComKg tr").length > 1) {
            document.getElementById("filaTemp").remove();
        }

        if (valorTempCantProm == 1) {
            cantidadAproxCompKg = $("#cardTextCanNc").text();
            var temVa = parseInt(cantidadAproxCompKg.replace(/,/g, ""));
            pesProComKg = $("#cardTextCanPesProm").text();
            precioProIngresoCoKg = precioIngresoCoKg / temVa;

        }
        var fila = '<tr id="filaTemp">\n\
                 <td width="22%">'+ va3.toLocaleString('en-US', noTruncarDecimales2) + '</td>' +
            '<td width="22%">' + precioIngresoNc.toLocaleString('en-US', noTruncarDecimales) + '</td>' +
            '<td width="22%">' + pesoNc.toLocaleString('en-US', noTruncarDecimales) + '</td>' +
            '<td width="20%">' + pesProNc.toLocaleString('en-US', noTruncarDecimales) + '</td>' +
            '<td width="20%">' + precioProIngresoNc.toLocaleString('en-US', noTruncarDecimales) + '</td>' +
            '</tr>';
        $('#tblChaNc').prepend(fila);
        var fila2 = '<tr id="filaTemp">\n\
                 <td width="22%">'+ va2.toLocaleString('en-US', noTruncarDecimales2) + '</td>' +
            '<td width="22%">' + precioIngresoCo.toLocaleString('en-US', noTruncarDecimales) + '</td>' +
            '<td width="22%">' + pesoComprasUd.toLocaleString('en-US', noTruncarDecimales) + '</td>' +
            '<td width="20%">' + pesProCom.toLocaleString('en-US', noTruncarDecimales) + '</td>' +
            '<td width="20%">' + precioProIngresoCo.toLocaleString('en-US', noTruncarDecimales) + '</td>' +
            '</tr>';
        $('#tblChaCom').prepend(fila2);
        var fila3 = '<tr id="filaTemp">\n\
                 <td width="22%">'+ va1.toLocaleString('en-US', noTruncarDecimales2) + '</td>' +
            '<td width="22%">' + precioIngresoCoKg.toLocaleString('en-US', noTruncarDecimales) + '</td>' +
            '<td width="22%">' + pesoComprasKg.toLocaleString('en-US', noTruncarDecimales) + '</td>' +
            '<td width="20%">' + pesProComKg.toLocaleString('en-US', noTruncarDecimales) + '</td>' +
            '<td width="20%">' + precioProIngresoCoKg.toLocaleString('en-US', noTruncarDecimales) + '</td>' +
            '</tr>';
        $('#tblChaComKg').prepend(fila3);
        var fila4 = '<tr id="filaTemp">\n\
                 <td width="22%">'+ va4.toLocaleString('en-US', noTruncarDecimales2) + '</td>' +
            '<td width="22%">' + precioIngresoGta.toLocaleString('en-US', noTruncarDecimales) + '</td>' +
            '<td width="22%">' + pesoGta.toLocaleString('en-US', noTruncarDecimales) + '</td>' +
            '<td width="20%">' + pesProComGta.toLocaleString('en-US', noTruncarDecimales) + '</td>' +
            '<td width="20%">' + precioProIngresoGta.toLocaleString('en-US', noTruncarDecimales) + '</td>' +
            '</tr>';
        $('#tblChaGta').prepend(fila4);
        calcularPorcentaje();
    }
    contadorEven = contadorEven + 1;
}

function getNumericMonth(monthAbbr) {
    return (String(['enero',
        'febrero',
        'marzo',
        'abril',
        'mayo',
        'junio',
        'julio',
        'agosto',
        'septiembre',
        'octubre',
        'noviembre',
        'diciembre'].indexOf(monthAbbr) + 1))
}

function calcularPorcentaje() {
    var cantT = $("#cardTextCanNc").text();
    var precioT = $("#cardTextCanCompKg").text();
    var pesoT = $("#cardTextCanComp").text();
    var pesoPromT = $("#cardTextCanPesProm").text();
    var precioPromT = $("#cardTextCanPrecProm").text();

    cantT = cantT.replace(/,/g, "");
    precioT = precioT.replace(/,/g, "");
    pesoT = pesoT.replace(/,/g, "");


    var cantHis = cantHisAnt;
    var precioHis = precHisAnt.toFixed(2);
    var pesoHis = pesoHisAnt/1000;
    var pesoProHis = pesoPromHistAnt;
    var precioProHis = precioPromHistAnt;

    cantT = cantT * 100;
    pesoT = pesoT * 100;
    precioT = precioT * 100;
    pesoPromT = pesoPromT * 100;
    precioPromT = precioPromT * 100;

    var porcCan = cantT/cantHis;
    var porcKg = pesoT/pesoHis;
    var porcPrec = precioT/precioHis;
    var porcKgPro = pesoPromT / pesoProHis;
    var porcPreciPro = precioPromT / precioProHis;



    if (porcCan >= 100) {
        porcCan = porcCan - 100;
        $('#cantAnioAnteriorChaPor')
            .removeClass('badge badge-soft-danger rounded-capsule ml-2')
            .addClass('badge badge-soft-success rounded-capsule ml-2');
    } else {
        porcCan = (100 - porcCan) * -1;
        $('#cantAnioAnteriorChaPor')
            .removeClass('badge badge-soft-success rounded-capsule ml-2')
            .addClass('badge badge-soft-danger rounded-capsule ml-2');
    }
    if (porcKg >= 100) {
        porcKg = porcKg - 100;
        $('#pesAnioAnteriorChaPor')
            .removeClass('badge badge-soft-danger rounded-capsule ml-2')
            .addClass('badge badge-soft-success rounded-capsule ml-2');
    } else {
        porcKg = (100 - porcKg) * -1;
        $('#pesAnioAnteriorChaPor')
            .removeClass('badge badge-soft-success rounded-capsule ml-2')
            .addClass('badge badge-soft-danger rounded-capsule ml-2');
    }
    if (porcPrec >= 100) {
        porcPrec = porcPrec - 100;
        $('#precAnioAnteriorChaPor')
            .removeClass('badge badge-soft-danger rounded-capsule ml-2')
            .addClass('badge badge-soft-success rounded-capsule ml-2');
    } else {
        porcPrec = (100 - porcPrec) * -1;
        $('#precAnioAnteriorChaPor')
            .removeClass('badge badge-soft-success rounded-capsule ml-2')
            .addClass('badge badge-soft-danger rounded-capsule ml-2');
    }

    if (porcKgPro >= 100) {
        porcKgPro = porcKgPro - 100;
        $('#pesoAnioAnteriorChaPor')
            .removeClass('badge badge-soft-danger rounded-capsule ml-2')
            .addClass('badge badge-soft-success rounded-capsule ml-2');
    } else {
        porcKgPro = (100 - porcKgPro) * -1;
        $('#pesoAnioAnteriorChaPor')
            .removeClass('badge badge-soft-success rounded-capsule ml-2')
            .addClass('badge badge-soft-danger rounded-capsule ml-2');
    }
    if (porcPreciPro >= 100) {
        porcPreciPro = porcPreciPro - 100;
        $('#PrecioAnioAnteriorChaPor')
            .removeClass('badge badge-soft-success rounded-capsule ml-2')
            .addClass('badge badge-soft-danger rounded-capsule ml-2');
    } else {
        porcPreciPro = (100 - porcPreciPro) * -1;
        $('#PrecioAnioAnteriorChaPor')
            .removeClass('badge badge-soft-danger rounded-capsule ml-2')
            .addClass('badge badge-soft-success rounded-capsule ml-2');
      
    }
   
    $("#cantAnioAnteriorChaPor").text(porcCan.toFixed(2) + "%");
    $("#pesAnioAnteriorChaPor").text(porcKg.toFixed(2) + "%");
    $("#precAnioAnteriorChaPor").text(porcPrec.toFixed(2) + "%");
    $("#pesoAnioAnteriorChaPor").text(porcKgPro.toFixed(2) + "%");
    $("#PrecioAnioAnteriorChaPor").text(porcPreciPro.toFixed(2) + "%");

}

function consultaInfoAnterior(val, tipo) {
    //tempValoresHist
    //const filteredLibraries = jsLibraries.filter((item) => item !== 'react')
    if (tipo == 1) {
        //resBusAnioAnt
        //console.log("el val de la func es:" + JSON.stringify(val))
        //console.log("el valor historico es:" + JSON.stringify(resBusAnioAnt))
        var comprSelect = $("#selecTipoIngreso").dxTagBox('instance').option('value');

        const noTruncarDecimales = { maximumFractionDigits: 2, minimumFractionDigits: 2 };
        const noTruncarDecimales2 = { maximumFractionDigits: 0, minimumFractionDigits: 0 };
        var select = $("#anioClass option:selected").text();

        var cantidad = 0;
        var peso = 0;
        var precio = 0;
        var valMes = 0;

        let arrayFilter = [];


        if (val.length == 0) {
            if (comprSelect.length == 0) {
                //tempValoresHist = resBusAnioAnt;
            }
            for (var y of tempValoresHist) {
                arrayFilter.push(y);
                cantidad = cantidad + y.Cantidad;
                peso = peso + y.Peso;
                precio = precio + y.Precio;
            }
            //tempValoresHist = arrayFilter;
        } else {
            for (var z of val) {
                valMes = getNumericMonth(z);
                for (var y of tempValoresHist) {
                    if (y.Mes == valMes) {
                        arrayFilter.push(y);
                        cantidad = cantidad + y.Cantidad;
                        peso = peso + y.Peso;
                        precio = precio + y.Precio;
                    }
                }
            }
            //tempValoresHist = arrayFilter;
        }

        var can = cantidad.toLocaleString('en-US', noTruncarDecimales2);
        var pes = (peso / 1000).toLocaleString('en-US', noTruncarDecimales2);
        var prec = precio.toLocaleString('en-US', noTruncarDecimales);
        var pesoPro = (peso / cantidad).toFixed(2);
        var precPro = (precio / peso).toFixed(2);
        precHisAnt = precio;
        pesoHisAnt = peso;
        cantHisAnt = cantidad;
        pesoPromHistAnt = pesoPro;
        precioPromHistAnt = precPro;
        $("#cantAnioAnteriorCha").text((parseInt(select) - 1) + ": " + can);
        $("#pesAnioAnteriorCha").text((parseInt(select) - 1) + ": " + pes);
        $("#precAnioAnteriorCha").text((parseInt(select) - 1) + ": " + prec);
        $("#pesoAnioAnteriorCha").text((parseInt(select) - 1) + ": " + pesoPro);
        $("#precioPromAnioAnteriorCha").text((parseInt(select) - 1) + ": " + precPro);
    }
    if (tipo == 2) {
        const noTruncarDecimales = { maximumFractionDigits: 2, minimumFractionDigits: 2 };
        const noTruncarDecimales2 = { maximumFractionDigits: 0, minimumFractionDigits: 0 };
        var select = $("#anioClass option:selected").text();
        var comprSelect = $("#selectMes").dxTagBox('instance').option('value');

        var cantidad = 0;
        var peso = 0;
        var precio = 0;
        var valMes = 0;
        let arrayFilter = [];

        if (val.length == 0) {
            if (comprSelect.length == 0) {
                //tempValoresHist = resBusAnioAnt;
            }
            for (var y of tempValoresHist) {
                arrayFilter.push(y);
                cantidad = cantidad + y.Cantidad;
                peso = peso + y.Peso;
                precio = precio + y.Precio;
            }
            //tempValoresHist = arrayFilter;
        }
      
        else {
            for (var z of val) {
                for (var y of tempValoresHist) {
                    if (y.TipoIngreso == z) {
                        arrayFilter.push(y);
                        cantidad = cantidad + y.Cantidad;
                        peso = peso + y.Peso;
                        precio = precio + y.Precio;
                    }
                }
            }
            //tempValoresHist = arrayFilter;
        }
        var can = cantidad.toLocaleString('en-US', noTruncarDecimales2);
        var pes = (peso / 1000).toLocaleString('en-US', noTruncarDecimales2);
        var prec = precio.toLocaleString('en-US', noTruncarDecimales);
        var pesoPro = (peso / cantidad).toFixed(2);
        var precPro = (precio / peso).toFixed(2);
        precHisAnt = precio;
        pesoHisAnt = peso;
        cantHisAnt = cantidad;
        pesoPromHistAnt = pesoPro;
        precioPromHistAnt = precPro;
        $("#cantAnioAnteriorCha").text((parseInt(select) - 1) + ": " + can);
        $("#pesAnioAnteriorCha").text((parseInt(select) - 1) + ": " + pes);
        $("#precAnioAnteriorCha").text((parseInt(select) - 1) + ": " + prec);
        $("#pesoAnioAnteriorCha").text((parseInt(select) - 1) + ": " + pesoPro);
        $("#precioPromAnioAnteriorCha").text((parseInt(select) - 1) + ": " + precPro);
    }
}

function InformeIngresosDeChatarra() {
    var select = $("#anioClass option:selected").text();
    const meses = ['enero','febrero', 'marzo', 'abril', 'mayo', 'junio', 'julio', 'agosto', 'septiembre', 'octubre', 'noviembre', 'diciembre']; 
    const tipoCliente = ['Todos','MARCAS PRIVADAS', 'MARCAS PROPIAS', 'Prov. Locales'];
    const tipoClienteLinea = ['Todos', 'COBERTURA', 'INSTITUCIONES', 'MARCAS PRIVADAS', 'RECICLAJE'];
    const tipoClienteClase = ['Todos', 'AUTOSERVICIO', 'COORPORATIVO', 'DETALLISTA', 'DISTRIBUIDOR ZONAL', 'MARCAS PRIVADAS', 'PUNTO DE FABRICA','RECICLAJE'];
    const tipoIngreso = ['Compras (Kg)', 'Compras (Ud)', 'Garantia', 'Nota Credito'];

    $("tbody").children().remove()
    var valorDesv = obtenerDesv();
   
    $.ajax({
        url: "../Chatarra/ConsultaIngresosChatarraConDesviacionSap?anio=" + select,
        type: "GET"
        //async: false
        , success: function (msg) {
            resultJax = msg;
            document.getElementById("MostrarCardsInf").style.display = "";
            document.getElementById("OcultarTablaGeneral").style.display = "";
            document.getElementById("MostrarTblDetalles").style.display = "";
            document.getElementById("MostrarPrefiltros").style.display = "";
            document.getElementById("MostrarPrefiltros2").style.display = "";
            document.getElementById("MostrarPrefiltros3").style.display = "";
            document.getElementById("MostrarPrefiltros4").style.display = "";
            document.getElementById("MostrarPrefiltros5").style.display = "";
            document.getElementById("MostrarPrefiltros6").style.display = "";

            crearTablaDescriptivaAndChart(resultJax);       
            const locale = getLocale();
            DevExpress.localization.locale(locale);

            $("#selectMes").dxTagBox({
                dataSource: meses,
                searchEnabled: true,
                onValueChanged: function (e) {
                    var tbl = $("#IngresosdeChatarras").dxDataGrid("instance");
                    console.log(dataGrid);
                    if (e.value.length != 0) {
                        var filter = [];
                        for (var i = 0; i < e.value.length; i++) {
                            filter.push(["NombreMes", "=", e.value[i]]);
                            filter.push("or");
                        }
                        filter.pop();
                        //dataGrid.getCombinedFilter(true);
                        dataGrid.filter(filter);
                        //dataGrid.filter(filter);
                        consultaInfoAnterior(e.value,1);
                    }
                    else {
                        consultaInfoAnterior(e.value,1);
                        dataGrid.clearFilter();
                    }
                    //let filterValues = dataGrid.columnOption("Tipo_Cliente", "filterValues");

                    //console.log("fil:" + filterValues);
                },  
            });

            $('#selecTipoIngreso').dxTagBox({
                dataSource: tipoIngreso,
                searchEnabled: true,
                onValueChanged: function (e) {
                    console.log(dataGrid);
                    if (e.value.length != 0) {
                        valorTempCantProm = 1;
                        var filter = [];
                        for (var i = 0; i < e.value.length; i++) {
                            filter.push(["Tipo_Ingreso", "=", e.value[i]]);
                            filter.push("or");
                        }
                        filter.pop();
                        dataGrid.filter(filter);
                        consultaInfoAnterior(e.value, 2);
                    }
                    else {
                        valorTempCantProm = 0;
                        consultaInfoAnterior(e.value, 2);
                        dataGrid.clearFilter();
                    }
                    //let filterValues = dataGrid.columnOption("Tipo_Cliente", "filterValues");
                    //console.log("fil:" + filterValues);
                },
            });
            $('#selecTipoCliente').dxSelectBox({
                dataSource: tipoCliente,
                value: tipoCliente[0],
                onValueChanged(data) {                
                    if (data.value === 'Todos') { dataGrid.clearFilter(); } else { dataGrid.filter(['Tipo_Cliente', '=',data.value]); }
                },
            });
            $('#selecClienteLinea').dxSelectBox({
                dataSource: tipoClienteLinea,
                value: tipoClienteLinea[0],
                onValueChanged(data) {                   
                    if (data.value === 'Todos') { dataGrid.clearFilter(); } else { dataGrid.filter(['Cliente_Linea', '=', data.value]); }
                },
            });
            $('#selecClienteClase').dxSelectBox({
                dataSource: tipoClienteClase,
                value: tipoClienteClase[0],
                onValueChanged(data) {                 
                    if (data.value === 'Todos') { dataGrid.clearFilter(); } else { dataGrid.filter(['Cliente_Clase', '=', data.value]); }
                },
            });
            const dataGrid =$("#IngresosdeChatarras").dxDataGrid({
                dataSource: resultJax,
                columnAutoWidth: true,
                keyExpr: "Id",
                showBorders: true,
                allowColumnReordering: true,
                allowColumnResizing: true,
                filterRow: { visible: false },
                filterPanel: { visible: false },
                headerFilter: {
                    visible: true
                },
                columnFixing: {
                    enabled: true
                },
                export: {
                    enabled: true,
                },
                paging: {
                    pageSize: 10
                },
                searchPanel: {
                    visible: true,
                    width: 240,
                    placeholder: "Buscar..."
                },
                pager: {
                    showPageSizeSelector: true,
                    allowedPageSizes: [5, 10, 100],
                    showInfo: true
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
                    { dataField: "Id", visible: false },
                    { dataField: "DocEntry", visible: false },
                    { dataField: "FechaRegistro2", visible: false },
                    { dataField: "NombreMes", visible: false },

                    {
                        dataField: "FechaRegistro", caption: "Fecha Ingreso", allowEditing: false, dataType: 'date', sortOrder: "desc"
                    },
                    {
                        dataField: "FechaRegistro", caption: "Mes Ingreso", allowEditing: false, dataType: 'date', format: 'month'
                    },
                    {
                        dataField: "N_Documento", caption: "# Documento", allowEditing: false, fixed: false, allowFiltering: false, visible: false
                    },

                    {
                        dataField: "Pedido", caption: "# Pedido", allowEditing: false, fixed: false, allowFiltering: false, visible: false
                    },
                    {
                        dataField: "Identificador", caption: "Identificacion", allowEditing: false, width: 130, allowFiltering: false, visible: false
                    },
                    {
                        dataField: "Cliente", caption: "Cliente", allowEditing: false, fixed: false, width: 250, headerFilter: {
                            allowSearch: true,
                        },
                    },
                    {
                        dataField: "Tipo_Cliente", caption: "Tipo Cliente", allowEditing: false
                    },
                    {
                        dataField: "Cliente_Linea", caption: "Cliente Linea", allowEditing: false
                    },
                    {
                        dataField: "Cliente_Clase", caption: "Cliente Clase", allowEditing: false
                    },
                    {
                        dataField: "Tipo_Ingreso", caption: "Tipo Ingreso", allowEditing: false
                    },
                    {
                        dataField: "Cantidad", caption: "Cantidad(Uds.)", allowFiltering: false, allowEditing: false,
                    },
                    ,
                    {
                        dataField: "Peso_Teorico", caption: "Peso Teorico(kg)", alignment: "right", allowFiltering: false, width: 130, allowEditing: false,
                        format: {
                            type: "fixedPoint",
                            precision: 2,

                        },
                        customizeText: function (cellInfo) {
                            const noTruncarDecimales = { maximumFractionDigits: 2, minimumFractionDigits: 2 };
                            return (cellInfo.value).toLocaleString('en-US', noTruncarDecimales);
                        }

                    },
                    {
                        dataField: "Peso_Real", caption: "Peso Ingresado(kg)", alignment: "right", allowFiltering: false, width: 135, allowEditing: false,
                        format: {
                            type: "fixedPoint",
                            precision: 2,
                        }, customizeText: function (cellInfo) {
                            const noTruncarDecimales = { maximumFractionDigits: 2, minimumFractionDigits: 2 };
                            return (cellInfo.value).toLocaleString('en-US', noTruncarDecimales);
                        },

                    },
                    {
                        dataField: "Precio", caption: "USD($)", alignment: "right", allowFiltering: false, width: 135, allowEditing: false,
                        format: {
                            type: "fixedPoint",
                            precision: 2,
                        }, customizeText: function (cellInfo) {
                            const noTruncarDecimales = { maximumFractionDigits: 2, minimumFractionDigits: 2 };
                            
                            return (cellInfo.value).toLocaleString('en-US', noTruncarDecimales);
                        },

                    },
                    
                    {
                        dataField: "Desviacion", caption: "Desviacion", alignment: "right", allowFiltering: false, allowEditing: false,
                        format: {
                            type: "fixedPoint",
                            precision: 2,
                        }, customizeText: function (cellInfo) {
                            return cellInfo.value + "%";
                        },
                        cellTemplate(container, options) {

                            container.addClass((options.data.Desviacion > valorDesv) ? 'inc' : 'dec');
                            container.html(options.text);
                        }

                    },
                    {
                        dataField: "Bodega", caption: "Bodega", allowEditing: false
                    },
                    {
                        dataField: "Vendedor", caption: "Vendedor", allowEditing: false
                    },
                    {
                        dataField: "Comentarios", caption: "Comentarios", allowEditing: false, visible: false
                    },
                    {
                        caption: "Acciones",

                        cellTemplate: function (container, options) {
                            var btn = "<button class='btn-primary' onclick='DetalleChatarra(" + JSON.stringify(options.data) + ")'>Detalle</button>";
                            $("<div>")
                                .append($(btn))
                                .appendTo(container);
                        }
                    }
                ],
                summary: {
                    totalItems: [
                        {
                            name: "Tipo_Ingreso",
                            column: "Tipo_Ingreso",
                            summaryType: "count",
                            displayFormat: "Cantidad Total",
                            showInColumn: "Tipo_Ingreso",
                            customizeText: function (e) {
                                if (e.value != 0 && e.value != "") {
                                    return "Totales: ";
                                }
                            }
                        }                     
                        ,
                        {
                            name: "Cantidad",
                            column: "Cantidad",
                            summaryType: "sum",
                            displayFormat: "Cantidad Total",
                            showInColumn: "Cantidad",
                            customizeText: function (e) {
                                if (e.value != 0 && e.value != "") {
                                    const noTruncarDecimales = { maximumFractionDigits: 0, minimumFractionDigits: 0 };
                                    ValTotal = (e.value).toLocaleString('en-US', noTruncarDecimales);
                                    cantidadTotalIng = e.value;
                                    $("#cardTextCanNc").text(ValTotal);
                                    cantTotalDataGrid = e.value;
                                    return ValTotal;
                                }
                            }
                        }
                        , {
                            column: "Peso_Teorico",
                            summaryType: "sum",
                            showInColumn: "Peso_Teorico",
                            displayFormat: "Total: {0}",
                            valueFormat: "currency",
                            customizeText: function (e) {
                                if (e.value != 0 && e.value != "") {
                                    const noTruncarDecimales = { maximumFractionDigits: 2, minimumFractionDigits: 2 };
                                    ValTotal = (e.value).toLocaleString('en-US', noTruncarDecimales);
                                    return ValTotal + "kg";
                                }
                            },

                        },
                        {
                            column: "Precio",
                            summaryType: "sum",
                            showInColumn: "Precio",
                            displayFormat: "Total: {0}",
                            valueFormat: "currency",
                            customizeText: function (e) {
                                if (e.value != 0 && e.value != "") {
                                    const noTruncarDecimales = { maximumFractionDigits: 2, minimumFractionDigits: 2 };
                                    ValTotal = (e.value).toLocaleString('en-US', noTruncarDecimales);
                                    //preTotalDataGrid
                                    preTotalDataGrid = e.value;
                                    $("#cardTextCanCompKg").text(ValTotal);
                                    return "$"+ValTotal;
                                }
                            },

                        },
                        {
                            column: "Peso_Real",
                            summaryType: "sum",
                            showInColumn: "Peso_Real",
                            displayFormat: "Total: {0}",
                            valueFormat: "currency",
                            customizeText: function (e) {
                                if (e.value != 0 && e.value != "") {
                                    const noTruncarDecimales = { maximumFractionDigits: 2, minimumFractionDigits: 2 };
                                    const noTruncarDecimales2 = { maximumFractionDigits: 0, minimumFractionDigits: 0 };

                                    ValTotal = (e.value).toLocaleString('en-US', noTruncarDecimales);
                                    pesoTotalIng = e.value;
                                    var res = e.value / 1000;
                                    $("#cardTextCanComp").text(res.toLocaleString('en-US', noTruncarDecimales2));
                                    pesTotalDataGrid = e.value;
                                    return ValTotal + "kg";
                                }
                            }
                        },
                        {
                            column: "Desviacion",
                            summaryType: "avg",
                            showInColumn: "Desviacion",
                            displayFormat: "Total: {0}",
                            valueFormat: "currency",
                            customizeText: function (e) {
                                if (e.value != 0 && e.value != "") {
                                    const noTruncarDecimales = { maximumFractionDigits: 2, minimumFractionDigits: 2 };

                                    ValTotal = (e.value).toLocaleString('en-US', noTruncarDecimales);
                                    desviacionTotalCal = ValTotal;

                                    var tot = pesTotalDataGrid / cantTotalDataGrid;
                                    var temPesTot = pesTotalDataGrid / 1000;
                                    var temPesTot2 = (temPesTot.toFixed(0)) * 1000;
                                    var totPrec = preTotalDataGrid / temPesTot2;

                                    $("#cardTextCanPesProm").text(tot.toLocaleString('en-US', noTruncarDecimales));
                                    $("#cardTextCanPrecProm").text(totPrec.toLocaleString('en-US', noTruncarDecimales));


                                    return ValTotal + "%";
                                }
                            }

                        },
                    ],
                },
                onContentReady: function (e) {
                    DatosFiltradosTabla();
                },
                onCellPrepared: function (e) {
                    if (e.rowType === "data" && e.column.dataField === "Peso_Real") {
                        e.cellElement.css("color", e.data >= 20 ? "inc" : "dec");
                        // Tracks the `Amount` data field
                        return e.data;
                    }
                }
            }).dxDataGrid('instance');
            $(".btn").attr("disabled", false);
            $(".btn-txt").text("Consultar");
        },
        error: function (msg) {
            $(".btn").attr("disabled", false);
            $(".btn-txt").text("Consultar");
            $("#MensajeErrorInesperado").show('fade');
            $("#cargaImg").hide();
            setTimeout(function () {
                $("#MensajeErrorInesperado").fadeOut(1500);
            }, 3000);
        }
    })
    function getLocale() {
        const storageLocale = sessionStorage.getItem('locale');
        return storageLocale != null ? storageLocale : 'es';
    }
//    consultarResAnterior();
}

function graficoMesesGeneral(valgeneral) {
    //console.log("valor:" + JSON.stringify(valgeneral));
    const noTruncarDecimales = { maximumFractionDigits: 2, minimumFractionDigits: 2 };
    var groupByVendedor = function (miarray, prop) {
        return miarray.reduce(function (groups, item) {
            var val = item[prop];

            if (val == 1) {
                val = "Enero";
            } if (val == 2) {
                val = "Febrero";
            } if (val == 3) {
                val = "Marzo";
            } if (val == 4) {
                val = "Abril";
            } if (val == 5) {
                val = "Mayo";
            } if (val == 6) {
                val = "Junio";
            } if (val == 7) {
                val = "Julio";
            } if (val == 8) {
                val = "Agosto";
            } if (val == 9) {
                val = "Septiembre";
            } if (val == 10) {
                val = "Octubre";
            } if (val == 11) {
                val = "Noviembre";
            } if (val == 12) {
                val = "Diciembre";
            }
            //la creamos e inicializamos el arreglo de profesionales.         
            groups[val] = groups[val] || [];
            groups[val].push(item);
            return groups;
        }, {});
    }
    var resultMes = groupByVendedor(valgeneral, 'FechaRegistro2');
    //console.log("resultMes:" + JSON.stringify(resultMes));

    var kilosNc = 0;
    var cantidadNc = 0;
    var kilosCo = 0;
    var cantidadCo = 0;
    var kilosCoKg = 0;
    var cantidadCoKg = 0;
    var mes = [];
    var cantNc = [];
    var cantCo = [];
    var cantCoKg = [];
    var cantGta = [];

    var pesNc = [];
    var pesCo = [];
    var pesCoKg = [];

    var pesoPromCoNc = 0;
    var contador = 0;
    //var valorKgVendedor = [];

    for (var i in resultMes) {
        kilosNc = 0;
        cantidadNc = 0;
        kilosCo = 0;
        cantidadCo = 0;
        cantidadGta = 0;
        cantidadCoKg = 0;
        kilosCoKg = 0;
        pesPromNc = 0;
        pesPromCo = 0;
        pesoPromCoNc = 0;
        nuevoObjeto = {};
        for (var j in resultMes[i]) {
            if (resultMes[i][j].Tipo_Ingreso == "Nota Credito") {
                cantidadNc = cantidadNc + resultMes[i][j].Cantidad;
            }
            if (resultMes[i][j].Tipo_Ingreso == "Compras (Ud)") {
                //kilosCo = kilosCo + resultMes[i][j].Peso_Real;
                cantidadCo = cantidadCo + resultMes[i][j].Cantidad;
            }
            if (resultMes[i][j].Tipo_Ingreso == "Compras (Kg)") {
                //kilosCoKg = kilosCoKg + resultMes[i][j].Peso_Real;

                cantidadCoKg = cantidadCoKg + resultMes[i][j].Cantidad;
            }
            if (resultMes[i][j].Tipo_Ingreso == "Garantia") {
                cantidadGta = cantidadGta + resultMes[i][j].Cantidad;
                //    cantidadCoKg = cantidadCoKg + resultMes[i][j].Peso_Real;
            }
            kilosNc = (kilosNc + resultMes[i][j].Peso_Real);
        }
        pesPromNc = (kilosNc / cantidadNc);
        pesPromCo = (kilosCo / cantidadCo);

        pesoPromCoNc = (pesPromNc + pesPromCo) / 2;
        //cantidadCoKg = (kilosCoKg / pesoPromCoNc);

        cantNc.push(cantidadNc.toFixed(0));
        cantCo.push(cantidadCo.toFixed(0));
        cantCoKg.push(cantidadCoKg.toFixed(0));
        cantGta.push(cantidadGta.toFixed(0));

        pesNc.push(kilosNc.toFixed(2));
        //pesCo.push(kilosCo.toFixed(2));
        //pesCoKg.push(kilosCoKg.toFixed(2));

        mes.push(Object.keys(resultMes)[contador]);
        contador = contador + 1;
    }
    if (chartGenMes != null) {
        chartGenMes.destroy();
    }
    var ctx = $("#myChartGeneralMeses");
    var nombreVendedor = [];
    var valorNombreVendedor = [];
    var valorKgVendedor = [];

    var datasetNc = {
        label: "NC Baterías chat.",
        borderWidth: 2,
        cubicInterpolationMode: 'monotone',
        backgroundColor: 'rgba(7,59,251,0.5)',// Color de fondo
        borderColor: 'rgba(7,59,251,0.5)',// Color del borde
        data: cantNc,
        pointRadius: 3,
        pointHoverRadius: 4,
        pointHitRadius: 10,
        fill: false,
        yAxisID: 'A',
    };
    var datasetGta = {
        label: "Garantia",
        borderWidth: 2,
        cubicInterpolationMode: 'monotone',
        backgroundColor: 'rgba(225, 17, 17,0.5)',// Color de fondo
        borderColor: 'rgba(225, 17, 17,0.5)',// Color del borde
        data: cantGta,
        pointRadius: 3,
        pointHoverRadius: 4,
        pointHitRadius: 10,
        fill: false,
        yAxisID: 'A',
    };
    var datasetCo = {
        label: "Compras por Bat.",
        borderWidth: 2,
        cubicInterpolationMode: 'monotone',
        backgroundColor: 'rgba(229, 236, 14,0.5)',// Color de fondo
        borderColor: 'rgba(229, 236, 14,0.5)',// Color del borde
        data: cantCo,
        pointRadius: 3,
        pointHoverRadius: 4,
        pointHitRadius: 10,
        fill: false,
        yAxisID: 'A',

    };
    var datasetCokg = {
        label: "Compras por Peso",
        borderWidth: 2,
        cubicInterpolationMode: 'monotone',
        backgroundColor: 'rgba(236, 112, 14,0.5)',// Color de fondo
        borderColor: 'rgba(236, 112, 14,0.5)',// Color del borde
        data: cantCoKg,
        pointRadius: 3,
        pointHoverRadius: 4,
        pointHitRadius: 10,
        fill: false,
        yAxisID: 'A',
    };
    var datasetPesoNc = {
        label: "Kilogramos:",
        borderWidth: 2,
        cubicInterpolationMode: 'monotone',
        backgroundColor: 'rgba(36, 200, 0,0.5)',// Color de fondo
        borderColor: 'rgba(36, 200, 0,0.5)',// Color del borde
        data: pesNc,
        pointRadius: 3,
        pointHoverRadius: 4,
        pointHitRadius: 10,
        fill: false,
        yAxisID: 'B',
        type: 'line',
        lineTension: 0,
    };
    var chartdata = {
        labels: mes,
        datasets: [datasetNc, datasetCokg, datasetCo, datasetGta, datasetPesoNc
        ]
    };

    chartGenMes = new Chart(ctx, {
        type: "bar",
        data: chartdata,
        options: {

            responsive: true,
            scales: {
                yAxes: [
                    {
                        id: 'A',
                        type: 'linear',
                        position: 'left',

                        ticks: {
                            callback: function (valor, index, valores) {
                                return Number(valor).toFixed(0).replace(/\B(?=(\d{3})+(?!\d))/g, ',');
                            }
                        },
                        scaleLabel: {
                            display: true,
                            labelString: "Baterias Chatarra",
                            fontColor: "black"
                        }
                    },
                    {
                        id: 'B',
                        type: 'linear',
                        position: 'right',
                        ticks: {
                            callback: function (valor, index, valores) {
                                return Number(valor).toFixed(0).replace(/\B(?=(\d{3})+(?!\d))/g, ',');
                            }
                        },
                        scaleLabel: {
                            display: true,
                            labelString: "Kilogramos",
                            fontColor: "black"
                        }

                    },

                ],
            },
            tooltips: {
                callbacks: {
                    label: function (tooltipItem, chart) {
                        var datasetLabel = chart.datasets[tooltipItem.datasetIndex].label || '';
                        return datasetLabel + " " + (tooltipItem.yLabel).toLocaleString('en-US', noTruncarDecimales);
                    }
                }
            },
            interaction: {
                intersect: false,
            },
            title: {
                display: true,
                text: 'Cantidades y Pesos chatarra',
                fontSize: 18,
            },
        }

    });
}

function graficoProveedoresGeneral(valgeneral) {
    var compr = $("#selecTipoIngreso").dxTagBox('instance').option('value');
    if (compr == "Compras (Ud)") {
        document.getElementById("MostrarChartProvee").style.display = "";
        document.getElementById("OcultarChartProvee").style.display = "none";

        const noTruncarDecimales = { maximumFractionDigits: 2, minimumFractionDigits: 2 };
        var pesoPromGeneral = $("#cardTextCanPesProm").text();
        var mes = [];
        var helper = {};
        var result = valgeneral.reduce(function (r, o) {
            var key = o.FechaRegistro2 + '-' + o.Cliente;
            if (!helper[key]) {
                helper[key] = Object.assign({}, o); // create a copy of o
                r.push(helper[key]);
            } else {
                if (helper[key].Tipo_Ingreso == "Compras (Kg)") {
                    helper[key].Cantidad += (o.Cantidad / pesoPromGeneral);
                    helper[key].Peso_Real += o.Peso_Real;
                } else {
                    helper[key].Cantidad += o.Cantidad;
                    helper[key].Peso_Real += o.Peso_Real;
                }
            }
            return r;
        }, []);

        var groupByAcum = function (miarray, prop) {
            return miarray.reduce(function (groups, item) {
                var val = item[prop];
                groups[val] = groups[val] || [];
                groups[val].push(item);
                return groups;
            }, {});
        }
        var resultMes = groupByAcum(result, 'NombreMes');
        var cantidadFreno = 0;
        var cantidadImpAn = 0;
        var kilogramosGene = 0;

        var cantFreno = [];
        var cantImpAn = [];

        var kgGene = [];
        var mes = [];

        for (var i in resultMes) {
            cantidadFreno = 0;
            kilogramosGene = 0;
            cantidadImpAn=0;
            kilosCo = 0;
            cantidadCo = 0;
            cantidadGta = 0;
            kilosCoKg = 0;
            cantidadCoKg = 0;
            pesPromNc = 0;
            pesPromCo = 0;
            pesoPromCoNc = 0;
            nuevoObjeto = {};
            for (var j in resultMes[i]) {
                if (resultMes[i][j].Cliente == "S.A. IMPORTADORA ANDINA S.A.I.A.") {
                    cantidadImpAn = cantidadImpAn + resultMes[i][j].Cantidad;
                    kilogramosGene = kilogramosGene + resultMes[i][j].Peso_Real;
                }
                if (resultMes[i][j].Cliente == "FRENOSEGURO CIA. LTDA.") {
                    cantidadFreno = cantidadFreno + resultMes[i][j].Cantidad;
                    kilogramosGene = kilogramosGene + resultMes[i][j].Peso_Real;
                }
            }
            cantFreno.push(cantidadFreno.toFixed(0));
            cantImpAn.push(cantidadImpAn.toFixed(0));
            kgGene.push(kilogramosGene.toFixed(0));

            mes.push(i);
        }

        if (chartGenProveedores != null) {
            chartGenProveedores.destroy();
        }
        var ctx = $("#myChartProveedores");
        var datasetImpAnd = {
            label: "IMPORTADORA ANDINA",
            borderWidth: 2,
            cubicInterpolationMode: 'monotone',
            backgroundColor: 'rgba(7,59,251,0.5)',// Color de fondo
            borderColor: 'rgba(7,59,251,0.5)',// Color del borde
            data: cantImpAn,
            pointRadius: 3,
            pointHoverRadius: 4,
            pointHitRadius: 10,
            fill: false,
            yAxisID: 'A',
        };
        var datasetFreno = {
            label: "FRENOSEGURO",
            borderWidth: 2,
            cubicInterpolationMode: 'monotone',
            backgroundColor: 'rgba(225, 17, 17,0.5)',// Color de fondo
            borderColor: 'rgba(225, 17, 17,0.5)',// Color del borde
            data: cantFreno,
            pointRadius: 3,
            pointHoverRadius: 4,
            pointHitRadius: 10,
            fill: false,
            yAxisID: 'A',
        };
        var datasetKgGene = {
            label: "Kilogramos:",
            borderWidth: 2,
            cubicInterpolationMode: 'monotone',
            backgroundColor: 'rgba(36, 200, 0,0.5)',// Color de fondo
            borderColor: 'rgba(36, 200, 0,0.5)',// Color del borde
            data: kgGene,
            pointRadius: 3,
            pointHoverRadius: 4,
            pointHitRadius: 10,
            fill: false,
            yAxisID: 'B',
            type: 'line',
            lineTension: 0,
        };
      
        var chartdata = {
            labels: mes,
            datasets: [datasetImpAnd, datasetFreno, datasetKgGene
            ]
        };

        chartGenProveedores = new Chart(ctx, {
            type: "bar",
            data: chartdata,
            options: {

                responsive: true,
                scales: {
                    yAxes: [
                        {
                            id: 'A',
                            type: 'linear',
                            position: 'left',

                            ticks: {
                                callback: function (valor, index, valores) {
                                    return Number(valor).toFixed(0).replace(/\B(?=(\d{3})+(?!\d))/g, ',');
                                }
                            },
                            scaleLabel: {
                                display: true,
                                labelString: "Baterias Chatarra",
                                fontColor: "black"
                            }
                        },
                        {
                            id: 'B',
                            type: 'linear',
                            position: 'right',
                            ticks: {
                                callback: function (valor, index, valores) {
                                    return Number(valor).toFixed(0).replace(/\B(?=(\d{3})+(?!\d))/g, ',');
                                }
                            },
                            scaleLabel: {
                                display: true,
                                labelString: "Kilogramos",
                                fontColor: "black"
                            }

                        },

                    ],
                },
                tooltips: {
                    callbacks: {
                        label: function (tooltipItem, chart) {
                            var datasetLabel = chart.datasets[tooltipItem.datasetIndex].label || '';
                            return datasetLabel + " " + (tooltipItem.yLabel).toLocaleString('en-US', noTruncarDecimales);
                        }
                    }
                },
                interaction: {
                    intersect: false,
                },
                title: {
                    display: true,
                    text: 'Cantidades y Pesos chatarra',
                    fontSize: 18,
                },
            }

        });
    }
    if (compr == "Compras (Kg)") {
        document.getElementById("MostrarChartProvee").style.display = "";
        document.getElementById("OcultarChartProvee").style.display = "none";

        const noTruncarDecimales = { maximumFractionDigits: 2, minimumFractionDigits: 2 };
        var pesoPromGeneral = $("#cardTextCanPesProm").text();
        var mes = [];
        var helper = {};
        var result = valgeneral.reduce(function (r, o) {
            var key = o.FechaRegistro2 + '-' + o.Cliente;

            if (!helper[key]) {
                helper[key] = Object.assign({}, o); // create a copy of o
                r.push(helper[key]);
            } else { 
                    helper[key].Cantidad += o.Cantidad;
                    helper[key].Peso_Real += o.Peso_Real;       
            }
            return r;
        }, []);

        var groupByAcum = function (miarray, prop) {
            return miarray.reduce(function (groups, item) {
                var val = item[prop];
                groups[val] = groups[val] || [];
                groups[val].push(item);
                return groups;
            }, {});
        }
        var resultMes = groupByAcum(result, 'NombreMes');
        var cantidadPrac = 0;
        var cantidadEcor = 0;
        var cantidadRecPac = 0;

        var kilogramosGene = 0;

        var cantPrac = [];
        var cantEcor = [];
        var cantRecPac = [];
        var kgGene = [];
       
        var mes = [];

        for (var i in resultMes) {
            cantidadEcor = 0;
            kilogramosGene = 0;
            cantidadRecPac = 0;
            cantidadPrac = 0;
            kilosCo = 0;
            cantidadCo = 0;
            cantidadGta = 0;
            kilosCoKg = 0;
            cantidadCoKg = 0;
            pesPromNc = 0;
            pesPromCo = 0;
            pesoPromCoNc = 0;
            nuevoObjeto = {};
            for (var j in resultMes[i]) {
                if (resultMes[i][j].Cliente == "ECORESA ECOLOGIA & RECICLAJE S.A.") {
                    cantidadEcor = cantidadEcor + resultMes[i][j].Peso_Real;
                    kilogramosGene = kilogramosGene + resultMes[i][j].Cantidad;
                }
                if (resultMes[i][j].Cliente == "PRACTIPOWER S.A.") {
                    cantidadPrac = cantidadPrac + resultMes[i][j].Peso_Real;
                    kilogramosGene = kilogramosGene + resultMes[i][j].Cantidad;
                }
                if (resultMes[i][j].Cliente == "RECICLAJES DEL PACIFICO RECYCLINGPACIFIC S.A.") {
                    cantidadRecPac = cantidadRecPac + resultMes[i][j].Peso_Real;
                    kilogramosGene = kilogramosGene + resultMes[i][j].Cantidad;
                }
            }
            cantPrac.push(cantidadPrac.toFixed(0));
            cantEcor.push(cantidadEcor.toFixed(0));
            cantRecPac.push(cantidadRecPac.toFixed(0));

            kgGene.push(kilogramosGene.toFixed(0));
          
            mes.push(i);
        }

        if (chartGenProveedores != null) {
            chartGenProveedores.destroy();
        }
        var ctx = $("#myChartProveedores");
        var datasetEco = {
            label: "Ecoresa (kg)",
            borderWidth: 2,
            cubicInterpolationMode: 'monotone',
            backgroundColor: 'rgba(7,59,251,0.5)',// Color de fondo
            borderColor: 'rgba(7,59,251,0.5)',// Color del borde
            data: cantEcor,
            pointRadius: 3,
            pointHoverRadius: 4,
            pointHitRadius: 10,
            fill: false,
            yAxisID: 'A',
        };
        var datasetPra = {
            label: "Practipower (kg)",
            borderWidth: 2,
            cubicInterpolationMode: 'monotone',
            backgroundColor: 'rgba(225, 17, 17,0.5)',// Color de fondo
            borderColor: 'rgba(225, 17, 17,0.5)',// Color del borde
            data: cantPrac,
            pointRadius: 3,
            pointHoverRadius: 4,
            pointHitRadius: 10,
            fill: false,
            yAxisID: 'A',
        };
        var datasetRec = {
            label: "Reciclajes del Pacifico (kg)",
            borderWidth: 2,
            cubicInterpolationMode: 'monotone',
            backgroundColor: 'rgba(255, 206, 86, 0.5)',// Color de fondo
            borderColor: 'rgba(255, 206, 86, 0.5)',// Color del borde
            data: cantRecPac,
            pointRadius: 3,
            pointHoverRadius: 4,
            pointHitRadius: 10,
            fill: false,
            yAxisID: 'A',
        };
        var datasetKgGene = {
            label: "Baterias Aproximadas:",
            borderWidth: 2,
            cubicInterpolationMode: 'monotone',
            backgroundColor: 'rgba(36, 200, 0,0.5)',// Color de fondo
            borderColor: 'rgba(36, 200, 0,0.5)',// Color del borde
            data: kgGene,
            pointRadius: 3,
            pointHoverRadius: 4,
            pointHitRadius: 10,
            fill: false,
            yAxisID: 'B',
            type: 'line',
            lineTension: 0,
        };
       
        var chartdata = {
            labels: mes,
            datasets: [datasetEco, datasetPra, datasetRec, datasetKgGene
            ]
        };

        chartGenProveedores = new Chart(ctx, {
            type: "bar",
            data: chartdata,
            options: {

                responsive: true,
                scales: {
                    yAxes: [
                        {
                            id: 'A',
                            type: 'linear',
                            position: 'left',

                            ticks: {
                                callback: function (valor, index, valores) {
                                    return Number(valor).toFixed(0).replace(/\B(?=(\d{3})+(?!\d))/g, ',');
                                }
                            },
                            scaleLabel: {
                                display: true,
                                labelString: "Kilogramos",
                                fontColor: "black"
                            }
                        },
                        {
                            id: 'B',
                            type: 'linear',
                            position: 'right',
                            ticks: {
                                callback: function (valor, index, valores) {
                                    return Number(valor).toFixed(0).replace(/\B(?=(\d{3})+(?!\d))/g, ',');
                                }
                            },
                            scaleLabel: {
                                display: true,
                                labelString: "Baterias Chatarra",
                                fontColor: "black"
                            }

                        },

                    ],
                },
                tooltips: {
                    callbacks: {
                        label: function (tooltipItem, chart) {
                            var datasetLabel = chart.datasets[tooltipItem.datasetIndex].label || '';
                            return datasetLabel + " " + (tooltipItem.yLabel).toLocaleString('en-US', noTruncarDecimales);
                        }
                    }
                },
                interaction: {
                    intersect: false,
                },
                title: {
                    display: true,
                    text: 'Cantidades y Pesos chatarra',
                    fontSize: 18,
                },
            }

        });
    } 
    if (compr == "Todos" || compr == "Nota Credito" || compr == "Garantia" || compr == "") {
        document.getElementById("OcultarChartProvee").style.display = "";
        document.getElementById("MostrarChartProvee").style.display = "none";
        $("#MensajeErrorDobleConsulta").show('fade');
        setTimeout(function () {
            $("#MensajeErrorDobleConsulta").fadeOut(1500);
        }, 3000);
    }
}

function graficoProveedoresGeneralback(valgeneral) {
    const noTruncarDecimales = { maximumFractionDigits: 2, minimumFractionDigits: 2 };
    var pesoPromGeneral = $("#cardTextCanPesProm").text();
    var mes = [];
    var helper = {};

    var result = valgeneral.reduce(function (r, o) {
        var key = o.FechaRegistro2 + '-' + o.Cliente;

        if (!helper[key]) {
            helper[key] = Object.assign({}, o); // create a copy of o
            r.push(helper[key]);
        } else {
            if (helper[key].Tipo_Ingreso == "Compras (Kg)") {
                helper[key].Cantidad += (o.Cantidad / pesoPromGeneral);
                helper[key].Peso_Real += o.Peso_Real;
            } else {
                helper[key].Cantidad += o.Cantidad;
                helper[key].Peso_Real += o.Peso_Real;
            }   
        }
        return r;
    }, []);

    var groupByAcum = function (miarray, prop) {
        return miarray.reduce(function (groups, item) {
            var val = item[prop];
            groups[val] = groups[val] || [];
            groups[val].push(item);
            return groups;
        }, {});
    }

    var resultMes = groupByAcum(result, 'NombreMes');

    if (chartGenProveedores != null) {
        chartGenProveedores.destroy();
    }
    var ctx = $("#myChartProveedores");

    chartGenProveedores = new Chart(ctx, {
        type: "bar",
        data: {
            labels: [],
            datasets: []
        },
        options: {
            responsive: true,
            scales: {
                yAxes: [
                    {
                        id: 'A',
                        type: 'linear',
                        position: 'left',

                        ticks: {
                            callback: function (valor, index, valores) {
                                return Number(valor).toFixed(0).replace(/\B(?=(\d{3})+(?!\d))/g, ',');
                            }
                        },
                        scaleLabel: {
                            display: true,
                            labelString: "Baterias Chatarra",
                            fontColor: "black"
                        }
                    },
                    {
                        id: 'B',
                        type: 'linear',
                        position: 'right',
                        ticks: {
                            callback: function (valor, index, valores) {
                                return Number(valor).toFixed(0).replace(/\B(?=(\d{3})+(?!\d))/g, ',');
                            }
                        },
                        scaleLabel: {
                            display: true,
                            labelString: "Kilogramos",
                            fontColor: "black"
                        }
                    },
                ],
            },
            tooltips: {
                callbacks: {
                    label: function (tooltipItem, chart) {
                        var datasetLabel = chart.datasets[tooltipItem.datasetIndex].label || '';
                        return datasetLabel + " " + (tooltipItem.yLabel).toLocaleString('en-US', noTruncarDecimales);
                    }
                }
            },
            interaction: {
                intersect: false,
            },
            title: {
                display: true,
                text: 'Cantidades y Pesos chatarra',
                fontSize: 18,
            },
        }
    });
    var borderColors = [
        'rgba(255,99,132,1)',
        'rgba(54, 162, 235, 1)',
        'rgba(255, 206, 86, 1)',
        'rgba(75, 192, 192, 1)',
        'rgba(153, 102, 255, 1)',
        'rgba(255, 159, 64, 1)'
    ];
    var backgroundColors = [
        'rgba(255, 99, 132, 0.2)',
        'rgba(54, 162, 235, 0.2)',
        'rgba(255, 206, 86, 0.2)',
        'rgba(75, 192, 192, 0.2)',
        'rgba(153, 102, 255, 0.2)',
        'rgba(255, 159, 64, 0.2)'
    ];
    var colorInt = 0; //Used to set a variable background and border color
    for (var i in resultMes) {
        var newDataset = {
            label: [],
            data: [],
            backgroundColor: backgroundColors[colorInt],
            borderColor: borderColors[colorInt]
        };
        colorInt += 1;
        for (var j in resultMes[i]) {
            newDataset.label.push(resultMes[i][j].Cliente);
            newDataset.data.push(resultMes[i][j].Cantidad);
        }
        addData(chartGenProveedores, i, newDataset);
    }
}

function addData(chart, label, data) {
    //console.log("esto trae label:" + JSON.stringify(label));
    //console.log("esto trae data:" + JSON.stringify(data));
    chart.data.labels.push(label);
    chart.data.datasets.push(data);
    chart.update();
}

function DatosFiltradosTabla() {
    const filterExpr = $("#IngresosdeChatarras").dxDataGrid("instance").getCombinedFilter(true);
    $("#IngresosdeChatarras").dxDataGrid("instance").getDataSource()
        .store()
        .load({ filter: filterExpr })
        .then((result) => {
            valor = result;
            contadorEven = 1;
           cambiaValorTabla(result);
        });
}

function cambiaValorTabla(val) {
    crearTablaDescriptivaAndChart(val);
}

function DetalleChatarra(modelo) {
    $("#ModalDetalleGeneralCha").modal("hide");
    var url = "../Chatarra/ConsultaDetalleIngresoChatarraSap?docEntry=" + modelo.DocEntry + "&tipo=" + modelo.Tipo_Ingreso;
    $.ajax({
        url: url,
        type: "GET"
        , success: function (msg) {
            //$("#cargaImg").hide();
            const locale = getLocale();
            DevExpress.localization.locale(locale);

            $("#ChatarraDetalle").dxDataGrid({
                dataSource: msg,
                columnAutoWidth: true,
                keyExpr: "DocEntry",
                showBorders: true,
                allowColumnReordering: false,
                filterRow: { visible: false },
                filterPanel: { visible: false },
                headerFilter: { visible: true },
                columnFixing: {
                    enabled: true
                },

                paging: {
                    pageSize: 10
                },
                pager: {
                    showPageSizeSelector: true,
                    allowedPageSizes: [5, 10, 100],
                    showInfo: true
                },
                columns: [
                    { dataField: "DocEntry", visible: false },
                    //{ dataField: "CardCode", visible: false },                  
                    {
                        dataField: "ItemCode", caption: "# Item", allowEditing: false, fixed: false, allowFiltering: false
                    },
                    {
                        dataField: "Description", caption: "Descripcion", allowEditing: false, fixed: false, allowFiltering: false
                    },
                    {
                        dataField: "Cantidad", caption: "Cantidad", allowEditing: false, allowFiltering: false
                    },
                    {
                        dataField: "PesoTeoricoUnitario", caption: "Peso Teorico Unitario", allowEditing: false, fixed: false, allowFiltering: false,
                        format: {
                            type: "fixedPoint",
                            precision: 2,

                        },
                        customizeText: function (cellInfo) {
                            const noTruncarDecimales = { maximumFractionDigits: 2, minimumFractionDigits: 2 };
                            return (cellInfo.value).toLocaleString('en-US', noTruncarDecimales);
                        }
                    },
                    {
                        dataField: "PesoTeoricoSubtotal", caption: "Peso Teorico Total", allowEditing: false, fixed: false, allowFiltering: false,
                        format: {
                            type: "fixedPoint",
                            precision: 2,

                        },
                        customizeText: function (cellInfo) {
                            const noTruncarDecimales = { maximumFractionDigits: 2, minimumFractionDigits: 2 };
                            return (cellInfo.value).toLocaleString('en-US', noTruncarDecimales);
                        }
                    },
                    {
                        dataField: "PesoIngresado", caption: "Peso Ingresado", allowEditing: false, fixed: false, allowFiltering: false,
                        format: {
                            type: "fixedPoint",
                            precision: 2,

                        },
                        customizeText: function (cellInfo) {
                            const noTruncarDecimales = { maximumFractionDigits: 2, minimumFractionDigits: 2 };
                            return (cellInfo.value).toLocaleString('en-US', noTruncarDecimales);
                        }
                    },
                    {
                        dataField: "Desviacion", caption: "Desviacion", allowEditing: false, fixed: false, allowFiltering: false,
                        format: {
                            type: "fixedPoint",
                            precision: 2,

                        },
                        customizeText: function (cellInfo) {
                            const noTruncarDecimales = { maximumFractionDigits: 2, minimumFractionDigits: 2 };
                            return (cellInfo.value).toLocaleString('en-US', noTruncarDecimales);
                        }
                    },


                ],
                summary: {
                    totalItems: [
                        {
                            name: "Cantidad",
                            column: "Cantidad",
                            summaryType: "sum",
                            displayFormat: "Cantidad Total",
                            showInColumn: "Cantidad",
                            customizeText: function (e) {
                                if (e.value != 0 && e.value != "") {
                                    $("#txtCantidadTotal").val(e.value);
                                    const noTruncarDecimales = { maximumFractionDigits: 0, minimumFractionDigits: 0 };
                                    ValTotal = (e.value).toLocaleString('en-US', noTruncarDecimales);
                                    return ValTotal;
                                }
                            }
                        }
                        , {
                            column: "PesoTeoricoSubtotal",
                            summaryType: "sum",
                            showInColumn: "PesoTeoricoSubtotal",
                            displayFormat: "Total: {0}",
                            valueFormat: "currency",
                            customizeText: function (e) {
                                if (e.value != 0 && e.value != "") {
                                    $("#txtPesoTeorico").val(e.value);

                                    const noTruncarDecimales = { maximumFractionDigits: 2, minimumFractionDigits: 2 };
                                    ValTotal = (e.value).toLocaleString('en-US', noTruncarDecimales);
                                    return ValTotal + "kg";
                                }
                            },
                        }
                        , {
                            column: "PesoIngresado",
                            summaryType: "sum",
                            showInColumn: "PesoIngresado",
                            displayFormat: "Total: {0}",
                            valueFormat: "currency",
                            customizeText: function (e) {
                                if (e.value != 0 && e.value != "") {
                                    $("#txtPesoIngresado").val(e.value);
                                    const noTruncarDecimales = { maximumFractionDigits: 2, minimumFractionDigits: 2 };
                                    ValTotal = (e.value).toLocaleString('en-US', noTruncarDecimales);
                                    return ValTotal + "kg";
                                }
                            },
                        },
                        {
                            column: "Desviacion",
                            summaryType: "sum",
                            showInColumn: "Desviacion",
                            displayFormat: "Total: {0}",
                            valueFormat: "currency",
                            customizeText: function (e) {
                                if (e.value != 0 && e.value != "") {
                                    var pesoTeorico = $("#txtPesoTeorico").val();
                                    var pesoIngresado = $("#txtPesoIngresado").val();
                                    var desviacion = null;
                                    var subtotal = (pesoIngresado / pesoTeorico) * 100;
                                    if (subtotal > 100) {
                                        desviacion = subtotal - 100;
                                    } else {
                                        desviacion = (100 - subtotal) * -1;
                                    }
                                    $("#txtDesviacionPromedioReporte").val(desviacion.toFixed(2));
                                    $("#txtDesviacionTotal").val(desviacion.toFixed(2));

                                    return "Prom: " + desviacion.toFixed(2) + "%";
                                }
                            }
                        },
                    ],
                },
                onCellPrepared: function (e) {
                    if (e.rowType === "data" && e.column.dataField === "Peso_Real") {
                        e.cellElement.css("color", e.data >= 20 ? "inc" : "dec");
                        // Tracks the `Amount` data field
                        return e.data;
                    }
                }
            });
            $(".btn").attr("disabled", false);
            $(".btn-txt").text("Consultar");
        },
        error: function (msg) {
            $(".btn").attr("disabled", false);
            $(".btn-txt").text("Consultar");
            $("#MensajeErrorInesperado").show('fade');
            $("#cargaImg").hide();
            setTimeout(function () {
                $("#MensajeErrorInesperado").fadeOut(1500);
            }, 3000);
        }
    })
    function getLocale() {
        const storageLocale = sessionStorage.getItem('locale');
        return storageLocale != null ? storageLocale : 'es';
    }
    $("#ModalDetalleChatarra").modal("show");
}

const monthNames = ["January", "February", "March", "April", "May", "June",
    "July", "August", "September", "October", "November", "December"
];

getLongMonthName = function (date) {
    return monthNames[date.getMonth()];
}

getStringMes = function (mes) {
    if (mes == 1) {
        return "Enero"
    }
    if (mes == 2) {
        return "Febrero"
    } if (mes == 3) {
        return "Marzo"
    } if (mes == 4) {
        return "Abril"
    }
}

function ChartResumenesChatarras() {
    var tempPeso = pesoTotalIng.toFixed(2);
    var tempCant = cantidadTotalIng.toFixed(2);

    const decimalEc = { maximumFractionDigits: 2, minimumFractionDigits: 2 };
    pesoTotValIng = (pesoTotalIng).toLocaleString('en-US', decimalEc);
    cantTotValIng = (cantidadTotalIng).toLocaleString('en-US', decimalEc);

    var valorRes = tempPeso / tempCant;

    //console.log("el valor valor res es: " + valorRes);
    $("#txtCantidadTotalVen").val(cantTotValIng);
    $("#txtPesoTotalVen").val(pesoTotValIng);
    $("#txtDesviacionVen").val(desviacionTotalCal);
    $("#txtPesoPromVen").val(valorRes.toFixed(2));

    $("#txtCantidadTotalTipCien").val(cantTotValIng);
    $("#txtPesoTotalTipCien").val(pesoTotValIng);
    $("#txtDesviacionTipCien").val(desviacionTotalCal);
    $("#txtPesoPromTipCien").val(valorRes.toFixed(2));

    $("#txtCantidadTotalClientLin").val(cantTotValIng);
    $("#txtPesoTotalClientLin").val(pesoTotValIng);
    $("#txtDesviacionClientLin").val(desviacionTotalCal);
    $("#txtPesoPromClientLin").val(valorRes.toFixed(2));

    $("#txtCantidadTotalClientClas").val(cantTotValIng);
    $("#txtPesoTotalClientClas").val(pesoTotValIng);
    $("#txtDesviacionClientClas").val(desviacionTotalCal);
    $("#txtPesoPromClientClas").val(valorRes.toFixed(2));

    $("#txtCantidadTotalTipoIng").val(cantTotValIng);
    $("#txtPesoTotalTipoIng").val(pesoTotValIng);
    $("#txtDesviacionTipoIng").val(desviacionTotalCal);
    $("#txtPesoPromTipIng").val(valorRes.toFixed(2));

    $("#txtCantidadTotalMes").val(cantTotValIng);
    $("#txtPesoTotalMes").val(pesoTotValIng);
    $("#txtDesviacionMes").val(desviacionTotalCal);
    $("#txtPesoPromMes").val(valorRes.toFixed(2));

    graficoVendedores();
    graficoTipoClientes();
    graficoClienteLinea();
    graficoClienteClase();
    graficoTipoIngreso();
    graficoMeses();
    $("#ModalReporteChatarra").modal("show");
}

function tablaResumenVendedor(valor) {
    const locale = getLocale();
    DevExpress.localization.locale(locale);
    $("#DetalleFinalChatarrasVendedor").dxDataGrid({
        dataSource: Object.values(valor),
        columnAutoWidth: true,
        showBorders: true,
        allowColumnReordering: false,
        filterRow: { visible: false },
        filterPanel: { visible: false },
        headerFilter: { visible: true },
        columnFixing: {
            enabled: true
        },
        paging: {
            pageSize: 5
        },
        pager: {
            showPageSizeSelector: true,
            allowedPageSizes: [5, 10, 100],
            showInfo: true
        },
        columns: [
            {
                dataField: "Vendedor", caption: "Vendedor", allowEditing: false, fixed: false, allowFiltering: false
            },
            {
                dataField: "Cantidad", caption: "Cantidad", allowEditing: false, fixed: false, allowFiltering: false,
                format: {
                    type: "fixedPoint",
                    precision: 2,
                },
                customizeText: function (cellInfo) {
                    const noTruncarDecimales = { maximumFractionDigits: 2, minimumFractionDigits: 2 };
                    return (cellInfo.value).toLocaleString('en-US', noTruncarDecimales);
                }
            },
            {
                dataField: "Peso_Real", caption: "Peso Real", allowEditing: false, fixed: false, allowFiltering: false,
                format: {
                    type: "fixedPoint",
                    precision: 2,
                },
                customizeText: function (cellInfo) {
                    const noTruncarDecimales = { maximumFractionDigits: 2, minimumFractionDigits: 2 };
                    return (cellInfo.value).toLocaleString('en-US', noTruncarDecimales);
                }
            },
            {
                dataField: "Peso_Promedio", caption: "Peso Promedio", allowEditing: false, fixed: false, allowFiltering: false,
                format: {
                    type: "fixedPoint",
                    precision: 2,
                },
                customizeText: function (cellInfo) {
                    const noTruncarDecimales = { maximumFractionDigits: 2, minimumFractionDigits: 2 };
                    return (cellInfo.value).toLocaleString('en-US', noTruncarDecimales);
                }
            },

        ],
    });
    function getLocale() {
        const storageLocale = sessionStorage.getItem('locale');
        return storageLocale != null ? storageLocale : 'es';
    }
}
function tablaResumenTipoCliente(valor) {
    const locale = getLocale();
    DevExpress.localization.locale(locale);
    $("#DetalleFinalChatarrasTipoCliente").dxDataGrid({
        dataSource: Object.values(valor),
        columnAutoWidth: true,
        showBorders: true,
        allowColumnReordering: false,
        filterRow: { visible: false },
        filterPanel: { visible: false },
        headerFilter: { visible: true },
        columnFixing: {
            enabled: true
        },
        paging: {
            pageSize: 5
        },
        pager: {
            showPageSizeSelector: true,
            allowedPageSizes: [5, 10, 100],
            showInfo: true
        },
        columns: [
            {
                dataField: "Vendedor", caption: "Tipo Cliente", allowEditing: false, fixed: false, allowFiltering: false
            },
            {
                dataField: "Cantidad", caption: "Cantidad", allowEditing: false, fixed: false, allowFiltering: false,
                format: {
                    type: "fixedPoint",
                    precision: 2,

                },
                customizeText: function (cellInfo) {
                    const noTruncarDecimales = { maximumFractionDigits: 2, minimumFractionDigits: 2 };
                    return (cellInfo.value).toLocaleString('en-US', noTruncarDecimales);
                }
            },
            {
                dataField: "Peso_Real", caption: "Peso Real", allowEditing: false, fixed: false, allowFiltering: false,
                format: {
                    type: "fixedPoint",
                    precision: 2,

                },
                customizeText: function (cellInfo) {
                    const noTruncarDecimales = { maximumFractionDigits: 2, minimumFractionDigits: 2 };
                    return (cellInfo.value).toLocaleString('en-US', noTruncarDecimales);
                }
            }, {
                dataField: "Peso_Promedio", caption: "Peso Promedio", allowEditing: false, fixed: false, allowFiltering: false,
                format: {
                    type: "fixedPoint",
                    precision: 2,

                },
                customizeText: function (cellInfo) {
                    const noTruncarDecimales = { maximumFractionDigits: 2, minimumFractionDigits: 2 };
                    return (cellInfo.value).toLocaleString('en-US', noTruncarDecimales);
                }
            },
        ],
    });
    function getLocale() {
        const storageLocale = sessionStorage.getItem('locale');
        return storageLocale != null ? storageLocale : 'es';
    }
}
function tablaResumenClienteLinea(valor) {
    const locale = getLocale();
    DevExpress.localization.locale(locale);
    $("#DetalleFinalChatarrasClienteLinea").dxDataGrid({
        dataSource: Object.values(valor),
        columnAutoWidth: true,
        showBorders: true,
        allowColumnReordering: false,
        filterRow: { visible: false },
        filterPanel: { visible: false },
        headerFilter: { visible: true },
        columnFixing: {
            enabled: true
        },
        paging: {
            pageSize: 5
        },
        pager: {
            showPageSizeSelector: true,
            allowedPageSizes: [5, 10, 100],
            showInfo: true
        },
        columns: [
            {
                dataField: "Vendedor", caption: "Cliente Linea", allowEditing: false, fixed: false, allowFiltering: false
            },
            {
                dataField: "Cantidad", caption: "Cantidad", allowEditing: false, fixed: false, allowFiltering: false,
                format: {
                    type: "fixedPoint",
                    precision: 2,

                },
                customizeText: function (cellInfo) {
                    const noTruncarDecimales = { maximumFractionDigits: 2, minimumFractionDigits: 2 };
                    return (cellInfo.value).toLocaleString('en-US', noTruncarDecimales);
                }
            },
            {
                dataField: "Peso_Real", caption: "Peso Real", allowEditing: false, fixed: false, allowFiltering: false,
                format: {
                    type: "fixedPoint",
                    precision: 2,

                },
                customizeText: function (cellInfo) {
                    const noTruncarDecimales = { maximumFractionDigits: 2, minimumFractionDigits: 2 };
                    return (cellInfo.value).toLocaleString('en-US', noTruncarDecimales);
                }
            }, {
                dataField: "Peso_Promedio", caption: "Peso Promedio", allowEditing: false, fixed: false, allowFiltering: false,
                format: {
                    type: "fixedPoint",
                    precision: 2,

                },
                customizeText: function (cellInfo) {
                    const noTruncarDecimales = { maximumFractionDigits: 2, minimumFractionDigits: 2 };
                    return (cellInfo.value).toLocaleString('en-US', noTruncarDecimales);
                }
            },
        ],
    });
    function getLocale() {
        const storageLocale = sessionStorage.getItem('locale');
        return storageLocale != null ? storageLocale : 'es';
    }
}
function tablaResumenClienteClase(valor) {
    const locale = getLocale();
    DevExpress.localization.locale(locale);
    $("#DetalleFinalChatarrasClienteClase").dxDataGrid({
        dataSource: Object.values(valor),
        columnAutoWidth: true,
        showBorders: true,
        allowColumnReordering: false,
        filterRow: { visible: false },
        filterPanel: { visible: false },
        headerFilter: { visible: true },
        columnFixing: {
            enabled: true
        },
        paging: {
            pageSize: 5
        },
        pager: {
            showPageSizeSelector: true,
            allowedPageSizes: [5, 10, 100],
            showInfo: true
        },
        columns: [
            {
                dataField: "Vendedor", caption: "Cliente Clase", allowEditing: false, fixed: false, allowFiltering: false
            },
            {
                dataField: "Cantidad", caption: "Cantidad", allowEditing: false, fixed: false, allowFiltering: false,
                format: {
                    type: "fixedPoint",
                    precision: 2,

                },
                customizeText: function (cellInfo) {
                    const noTruncarDecimales = { maximumFractionDigits: 2, minimumFractionDigits: 2 };
                    return (cellInfo.value).toLocaleString('en-US', noTruncarDecimales);
                }
            },
            {
                dataField: "Peso_Real", caption: "Peso Real", allowEditing: false, fixed: false, allowFiltering: false,
                format: {
                    type: "fixedPoint",
                    precision: 2,

                },
                customizeText: function (cellInfo) {
                    const noTruncarDecimales = { maximumFractionDigits: 2, minimumFractionDigits: 2 };
                    return (cellInfo.value).toLocaleString('en-US', noTruncarDecimales);
                }
            }, {
                dataField: "Peso_Promedio", caption: "Peso Promedio", allowEditing: false, fixed: false, allowFiltering: false,
                format: {
                    type: "fixedPoint",
                    precision: 2,

                },
                customizeText: function (cellInfo) {
                    const noTruncarDecimales = { maximumFractionDigits: 2, minimumFractionDigits: 2 };
                    return (cellInfo.value).toLocaleString('en-US', noTruncarDecimales);
                }
            },
        ],
    });
    function getLocale() {
        const storageLocale = sessionStorage.getItem('locale');
        return storageLocale != null ? storageLocale : 'es';
    }
}
function tablaResumenTipoIngreso(valor) {
    const locale = getLocale();
    DevExpress.localization.locale(locale);
    $("#DetalleFinalChatarrasTipoIngreso").dxDataGrid({
        dataSource: Object.values(valor),
        columnAutoWidth: true,
        showBorders: true,
        allowColumnReordering: false,
        filterRow: { visible: false },
        filterPanel: { visible: false },
        headerFilter: { visible: true },
        columnFixing: {
            enabled: true
        },
        paging: {
            pageSize: 5
        },
        pager: {
            showPageSizeSelector: true,
            allowedPageSizes: [5, 10, 100],
            showInfo: true
        },
        columns: [
            {
                dataField: "Vendedor", caption: "Tipo Ingreso", allowEditing: false, fixed: false, allowFiltering: false
            },
            {
                dataField: "Cantidad", caption: "Cantidad", allowEditing: false, fixed: false, allowFiltering: false,
                format: {
                    type: "fixedPoint",
                    precision: 2,

                },
                customizeText: function (cellInfo) {
                    const noTruncarDecimales = { maximumFractionDigits: 2, minimumFractionDigits: 2 };
                    return (cellInfo.value).toLocaleString('en-US', noTruncarDecimales);
                }
            },
            {
                dataField: "Peso_Real", caption: "Peso Real", allowEditing: false, fixed: false, allowFiltering: false,
                format: {
                    type: "fixedPoint",
                    precision: 2,

                },
                customizeText: function (cellInfo) {
                    const noTruncarDecimales = { maximumFractionDigits: 2, minimumFractionDigits: 2 };
                    return (cellInfo.value).toLocaleString('en-US', noTruncarDecimales);
                }
            }, {
                dataField: "Peso_Promedio", caption: "Peso Promedio", allowEditing: false, fixed: false, allowFiltering: false,
                format: {
                    type: "fixedPoint",
                    precision: 2,

                },
                customizeText: function (cellInfo) {
                    const noTruncarDecimales = { maximumFractionDigits: 2, minimumFractionDigits: 2 };
                    return (cellInfo.value).toLocaleString('en-US', noTruncarDecimales);
                }
            },
        ],
    });
    function getLocale() {
        const storageLocale = sessionStorage.getItem('locale');
        return storageLocale != null ? storageLocale : 'es';
    }
}
function tablaResumenMeses(valor) {
    const locale = getLocale();
    DevExpress.localization.locale(locale);
    $("#DetalleFinalChatarrasMeses").dxDataGrid({
        dataSource: Object.values(valor),
        columnAutoWidth: true,
        showBorders: true,
        allowColumnReordering: false,
        filterRow: { visible: false },
        filterPanel: { visible: false },
        headerFilter: { visible: true },
        columnFixing: {
            enabled: true
        },
        columns: [
            {
                dataField: "Vendedor", caption: "Mes", allowEditing: false, fixed: false, allowFiltering: false
            },
            {
                dataField: "Cantidad", caption: "Cantidad", allowEditing: false, fixed: false, allowFiltering: false,
                format: {
                    type: "fixedPoint",
                    precision: 2,

                },
                customizeText: function (cellInfo) {
                    const noTruncarDecimales = { maximumFractionDigits: 2, minimumFractionDigits: 2 };
                    return (cellInfo.value).toLocaleString('en-US', noTruncarDecimales);
                }
            },
            {
                dataField: "Peso_Real", caption: "Peso Real", allowEditing: false, fixed: false, allowFiltering: false,
                format: {
                    type: "fixedPoint",
                    precision: 2,

                },
                customizeText: function (cellInfo) {
                    const noTruncarDecimales = { maximumFractionDigits: 2, minimumFractionDigits: 2 };
                    return (cellInfo.value).toLocaleString('en-US', noTruncarDecimales);
                }
            }, {
                dataField: "Peso_Promedio", caption: "Peso Promedio", allowEditing: false, fixed: false, allowFiltering: false,
                format: {
                    type: "fixedPoint",
                    precision: 2,

                },
                customizeText: function (cellInfo) {
                    const noTruncarDecimales = { maximumFractionDigits: 2, minimumFractionDigits: 2 };
                    return (cellInfo.value).toLocaleString('en-US', noTruncarDecimales);
                }
            },
        ],
    });
    function getLocale() {
        const storageLocale = sessionStorage.getItem('locale');
        return storageLocale != null ? storageLocale : 'es';
    }
}
function graficoVendedores() {
    const noTruncarDecimales = { maximumFractionDigits: 2, minimumFractionDigits: 2 };

    if (char != null) {
        char.destroy();
    }
    //if (char2 != null) {
    //    char2.destroy();
    //}
    var groupByVendedor = function (miarray, prop) {
        return miarray.reduce(function (groups, item) {
            var val = item[prop];
            groups[val] = groups[val] || { Cantidad: 0, Peso_Real: 0, Peso_Promedio: 0, Vendedor: val };
            groups[val].Cantidad += item.Cantidad;
            var val1 = groups[val].Cantidad += item.Cantidad;
            groups[val].Peso_Real += item.Peso_Real;
            var val2 = groups[val].Peso_Real += item.Peso_Real;
            groups[val].Peso_Promedio = (val2 / val1);
            return groups;
        }, {});
    }
    var ctx = $("#myChart1");
    //var ctx2 = $("#myChart2");
    var nombreVendedor = [];
    var valorNombreVendedor = [];
    var valorKgVendedor = [];

    tablaResumenVendedor(groupByVendedor(valor, 'Vendedor'));

    for (var i in groupByVendedor(valor, 'Vendedor')) {
        nombreVendedor.push(groupByVendedor(valor, 'Vendedor')[i].Vendedor);
        valorNombreVendedor.push(groupByVendedor(valor, 'Vendedor')[i].Cantidad);
        valorKgVendedor.push(groupByVendedor(valor, 'Vendedor')[i].Peso_Real.toFixed(2));
    }
    var chartdata = {
        labels: nombreVendedor,
        datasets: [{
            label: "Cantidad:",
            //backgroundColor: color,
            //borderColor: color,
            borderWidth: 2,
            cubicInterpolationMode: 'monotone',
            backgroundColor: 'rgba(7,59,251,0.5)',// Color de fondo
            borderColor: 'rgba(7,59,251,0.5)',// Color del borde
            data: valorNombreVendedor,
            pointRadius: 3,
            pointHoverRadius: 4,
            pointHitRadius: 10,
            fill: false,
            yAxisID: 'A',
            //yAxisID: "left"
        },
        {
            label: "Peso:",
            //backgroundColor: color,
            //borderColor: color,
            borderWidth: 2,
            cubicInterpolationMode: 'monotone',
            backgroundColor: 'rgba(2,162,0,0.5)',// Color de fondo
            borderColor: 'rgba(2,162,0,0.5)',// Color del borde
            data: valorKgVendedor,
            pointRadius: 3,
            pointHoverRadius: 4,
            pointHitRadius: 10,
            fill: false,
            yAxisID: 'B',
            type: 'line',
            lineTension: 0,
            // yAxisID: "right",
        }
        ]
    };
    //var chartdata2 = {
    //    labels: nombreVendedor,
    //    datasets: [{
    //        label: "Peso:",
    //        //backgroundColor: color,
    //        //borderColor: color,
    //        borderWidth: 2,
    //        cubicInterpolationMode: 'monotone',
    //        backgroundColor: 'rgba(74,178,51,0.5)',// Color de fondo
    //        borderColor: 'rgba(74,178,51,0.5)',// Color del borde
    //        data: valorKgVendedor,
    //        pointRadius: 3,
    //        pointHoverRadius: 4,
    //        pointHitRadius: 10,
    //        fill: false
    //    }
    //    ]
    //};
    char = new Chart(ctx, {
        type: "bar",
        data: chartdata,
        options: {
            responsive: true,
            scales: {
                yAxes: [
                    {
                        id: 'A',
                        //type: 'linear',
                        position: 'left',
                        ticks: {
                            callback: function (valor, index, valores) {
                                return Number(valor).toFixed(0).replace(/\B(?=(\d{3})+(?!\d))/g, ',');
                            }
                        },
                        scaleLabel: {
                            display: true,
                            labelString: "Cantidades",
                            fontColor: "black"
                        }
                    }, {
                        id: 'B',
                        //type: 'linear',
                        position: 'right',
                        ticks: {
                            callback: function (valor, index, valores) {
                                return Number(valor).toFixed(0).replace(/\B(?=(\d{3})+(?!\d))/g, ',');
                            }
                        },
                        scaleLabel: {
                            display: true,
                            labelString: "Kilos",
                            fontColor: "black"
                        }
                    },
                ],
                xAxes: [{
                    scaleLabel: {
                        display: true,
                        labelString: "Detalle",
                        fontColor: "black"
                    }
                }],
            },
            interaction: {
                intersect: false,
            },
            tooltips: {
                callbacks: {
                    label: function (tooltipItem, chart) {
                        var datasetLabel = chart.datasets[tooltipItem.datasetIndex].label || '';
                        return datasetLabel + " " + (tooltipItem.yLabel).toLocaleString('en-US', noTruncarDecimales);
                    }
                }
            },
            title: {
                display: true,
                text: 'Cantidades chatarra',
                fontSize: 18,
            },
        }
    });
    //char2 = new Chart(ctx2, {
    //    type: "bar",
    //    data: chartdata2,
    //    options: {
    //        responsive: true,
    //        scales: {
    //            yAxes: [{
    //                ticks: {
    //                    callback: function (valor, index, valores) {
    //                        return Number(valor).toFixed(2).replace(/\B(?=(\d{3})+(?!\d))/g, ',');
    //                    }
    //                    //    stepSize: 5,                
    //                },
    //                scaleLabel: {
    //                    display: true,
    //                    labelString: "Resultados",
    //                    fontColor: "black"
    //                }
    //            }],
    //            xAxes: [{
    //                scaleLabel: {
    //                    display: true,
    //                    labelString: "Detalle",
    //                    fontColor: "black"
    //                }
    //            }],
    //        },
    //        tooltips: {
    //            callbacks: {
    //                label: function (tooltipItem, chart) {
    //                    var datasetLabel = chart.datasets[tooltipItem.datasetIndex].label || '';
    //                    return datasetLabel + " " + (tooltipItem.yLabel).toLocaleString('en-US', noTruncarDecimales) + " kg";
    //                }
    //            }
    //        },
    //        interaction: {
    //            intersect: false,
    //        },
    //        title: {
    //            display: true,
    //            text: 'Pesos Chatarra(kg)',
    //            fontSize: 18,
    //        },
    //    }
    //});
}

function graficoTipoClientes() {
    const noTruncarDecimales = { maximumFractionDigits: 2, minimumFractionDigits: 2 };

    var groupByVendedor = function (miarray, prop) {
        return miarray.reduce(function (groups, item) {
            var val = item[prop];
            groups[val] = groups[val] || { Cantidad: 0, Peso_Real: 0, Peso_Promedio: 0, Vendedor: val };
            groups[val].Cantidad += item.Cantidad;
            var val1 = groups[val].Cantidad += item.Cantidad;
            groups[val].Peso_Real += item.Peso_Real;
            var val2 = groups[val].Peso_Real += item.Peso_Real;
            groups[val].Peso_Promedio = (val2 / val1);
            return groups;
        }, {});
    }

    if (char3 != null) {
        char3.destroy();
    }
    //if (char4 != null) {
    //    char4.destroy();
    //}
    var ctx = $("#myChart3");
    //var ctx2 = $("#myChart4");
    var nombreVendedor = [];
    var valorNombreVendedor = [];
    var valorKgVendedor = [];

    tablaResumenTipoCliente(groupByVendedor(valor, 'Tipo_Cliente'));

    for (var i in groupByVendedor(valor, 'Tipo_Cliente')) {
        nombreVendedor.push(groupByVendedor(valor, 'Tipo_Cliente')[i].Vendedor);
        valorNombreVendedor.push(groupByVendedor(valor, 'Tipo_Cliente')[i].Cantidad);
        valorKgVendedor.push(groupByVendedor(valor, 'Tipo_Cliente')[i].Peso_Real.toFixed(2));
    }
    var chartdata = {
        labels: nombreVendedor,
        datasets: [{
            label: "Cantidad:",
            //backgroundColor: color,
            //borderColor: color,
            borderWidth: 2,
            cubicInterpolationMode: 'monotone',
            backgroundColor: 'rgba(7,59,251,0.5)',// Color de fondo
            borderColor: 'rgba(7,59,251,0.5)',// Color del borde
            data: valorNombreVendedor,
            pointRadius: 3,
            pointHoverRadius: 4,
            pointHitRadius: 10,
            fill: false,
            yAxisID: 'A',

            //yAxisID: "left"
        },
        {
            label: "Peso:",
            //backgroundColor: color,
            //borderColor: color,
            borderWidth: 2,
            cubicInterpolationMode: 'monotone',
            backgroundColor: 'rgba(2,162,0,0.5)',// Color de fondo
            borderColor: 'rgba(2,162,0,0.5)',// Color del borde
            data: valorKgVendedor,
            pointRadius: 3,
            pointHoverRadius: 4,
            pointHitRadius: 10,
            fill: false,
            yAxisID: 'B',
            type: 'line',
            lineTension: 0,

            // yAxisID: "right",

        }
        ]
    };
    //var chartdata2 = {
    //    labels: nombreVendedor,
    //    datasets: [{
    //        label: "Peso:",
    //        //backgroundColor: color,
    //        //borderColor: color,
    //        borderWidth: 2,
    //        cubicInterpolationMode: 'monotone',
    //        backgroundColor: 'rgba(74,178,51,0.5)',// Color de fondo
    //        borderColor: 'rgba(74,178,51,0.5)',// Color del borde
    //        data: valorKgVendedor,
    //        pointRadius: 3,
    //        pointHoverRadius: 4,
    //        pointHitRadius: 10,
    //        fill: false
    //    }
    //    ]
    //};
    char3 = new Chart(ctx, {
        type: "bar",
        data: chartdata,
        options: {
            responsive: true,
            scales: {
                yAxes: [
                    {
                        id: 'A',
                        //type: 'linear',
                        position: 'left',
                        ticks: {
                            callback: function (valor, index, valores) {
                                return Number(valor).toFixed(0).replace(/\B(?=(\d{3})+(?!\d))/g, ',');
                            }
                        },
                        scaleLabel: {
                            display: true,
                            labelString: "Cantidades",
                            fontColor: "black"
                        }
                    }, {
                        id: 'B',
                        //type: 'linear',
                        position: 'right',
                        ticks: {
                            callback: function (valor, index, valores) {
                                return Number(valor).toFixed(0).replace(/\B(?=(\d{3})+(?!\d))/g, ',');
                            }
                        },
                        scaleLabel: {
                            display: true,
                            labelString: "Kilos",
                            fontColor: "black"
                        }
                    },
                ],
                xAxes: [{
                    scaleLabel: {
                        display: true,
                        labelString: "Detalle",
                        fontColor: "black"
                    }
                }],
            },
            tooltips: {
                callbacks: {
                    label: function (tooltipItem, chart) {
                        var datasetLabel = chart.datasets[tooltipItem.datasetIndex].label || '';
                        return datasetLabel + " " + (tooltipItem.yLabel).toLocaleString('en-US', noTruncarDecimales);
                    }
                }
            },
            interaction: {
                intersect: false,
            },
            title: {
                display: true,
                text: 'Cantidades chatarra',
                fontSize: 18,
            },
        }

    });
    //char4 = new Chart(ctx2, {
    //    type: "bar",
    //    data: chartdata2,
    //    options: {
    //        responsive: true,
    //        scales: {
    //            yAxes: [{
    //                ticks: {
    //                    callback: function (valor, index, valores) {
    //                        return Number(valor).toFixed(2).replace(/\B(?=(\d{3})+(?!\d))/g, ',');
    //                    }
    //                    //    stepSize: 5,                
    //                },
    //                scaleLabel: {
    //                    display: true,
    //                    labelString: "Resultados",
    //                    fontColor: "black"
    //                }
    //            }],
    //            xAxes: [{
    //                scaleLabel: {
    //                    display: true,
    //                    labelString: "Detalle",
    //                    fontColor: "black"
    //                }
    //            }],
    //        },
    //        tooltips: {
    //            callbacks: {
    //                label: function (tooltipItem, chart) {
    //                    var datasetLabel = chart.datasets[tooltipItem.datasetIndex].label || '';
    //                    return datasetLabel + " " + (tooltipItem.yLabel).toLocaleString('en-US', noTruncarDecimales) + " kg";
    //                }
    //            }
    //        },
    //        interaction: {
    //            intersect: false,
    //        },
    //        title: {
    //            display: true,
    //            text: 'Pesos Chatarra(kg)',
    //            fontSize: 18,
    //        },
    //    }
    //});
}
function graficoClienteLinea() {
    const noTruncarDecimales = { maximumFractionDigits: 2, minimumFractionDigits: 2 };

    var groupByVendedor = function (miarray, prop) {
        return miarray.reduce(function (groups, item) {
            var val = item[prop];
            groups[val] = groups[val] || { Cantidad: 0, Peso_Real: 0, Peso_Promedio: 0, Vendedor: val };
            groups[val].Cantidad += item.Cantidad;
            var val1 = groups[val].Cantidad += item.Cantidad;
            groups[val].Peso_Real += item.Peso_Real;
            var val2 = groups[val].Peso_Real += item.Peso_Real;
            groups[val].Peso_Promedio = (val2 / val1);
            return groups;
        }, {});
    }

    if (char5 != null) {
        char5.destroy();
    }
    //if (char6 != null) {
    //    char6.destroy();
    //}
    var ctx = $("#myChart5");
    //var ctx2 = $("#myChart6");
    var nombreVendedor = [];
    var valorNombreVendedor = [];
    var valorKgVendedor = [];

    tablaResumenClienteLinea(groupByVendedor(valor, 'Cliente_Linea'));

    for (var i in groupByVendedor(valor, 'Cliente_Linea')) {
        nombreVendedor.push(groupByVendedor(valor, 'Cliente_Linea')[i].Vendedor);
        valorNombreVendedor.push(groupByVendedor(valor, 'Cliente_Linea')[i].Cantidad);
        valorKgVendedor.push(groupByVendedor(valor, 'Cliente_Linea')[i].Peso_Real.toFixed(2));
    }
    var chartdata = {
        labels: nombreVendedor,
        datasets: [{
            label: "Cantidad:",
            //backgroundColor: color,
            //borderColor: color,
            borderWidth: 2,
            cubicInterpolationMode: 'monotone',
            backgroundColor: 'rgba(7,59,251,0.5)',// Color de fondo
            borderColor: 'rgba(7,59,251,0.5)',// Color del borde
            data: valorNombreVendedor,
            pointRadius: 3,
            pointHoverRadius: 4,
            pointHitRadius: 10,
            fill: false,
            yAxisID: 'A',

            //yAxisID: "left"

        },
        {
            label: "Peso:",
            //backgroundColor: color,
            //borderColor: color,
            borderWidth: 2,
            cubicInterpolationMode: 'monotone',
            backgroundColor: 'rgba(2,162,0,0.5)',// Color de fondo
            borderColor: 'rgba(2,162,0,0.5)',// Color del borde
            data: valorKgVendedor,
            pointRadius: 3,
            pointHoverRadius: 4,
            pointHitRadius: 10,
            fill: false,
            yAxisID: 'B',
            type: 'line',
            lineTension: 0,

            // yAxisID: "right",

        }
        ]
    };
    //var chartdata2 = {
    //    labels: nombreVendedor,
    //    datasets: [{
    //        label: "Peso:",
    //        //backgroundColor: color,
    //        //borderColor: color,
    //        borderWidth: 2,
    //        cubicInterpolationMode: 'monotone',
    //        backgroundColor: 'rgba(74,178,51,0.5)',// Color de fondo
    //        borderColor: 'rgba(74,178,51,0.5)',// Color del borde
    //        data: valorKgVendedor,
    //        pointRadius: 3,
    //        pointHoverRadius: 4,
    //        pointHitRadius: 10,
    //        fill: false
    //    }
    //    ]
    //};
    char5 = new Chart(ctx, {
        type: "bar",
        data: chartdata,
        options: {
            responsive: true,
            scales: {
                yAxes: [
                    {
                        id: 'A',
                        //type: 'linear',
                        position: 'left',
                        ticks: {
                            callback: function (valor, index, valores) {
                                return Number(valor).toFixed(0).replace(/\B(?=(\d{3})+(?!\d))/g, ',');
                            }
                        },
                        scaleLabel: {
                            display: true,
                            labelString: "Cantidades",
                            fontColor: "black"
                        }
                    }, {
                        id: 'B',
                        //type: 'linear',
                        position: 'right',
                        ticks: {
                            callback: function (valor, index, valores) {
                                return Number(valor).toFixed(0).replace(/\B(?=(\d{3})+(?!\d))/g, ',');
                            }
                        },
                        scaleLabel: {
                            display: true,
                            labelString: "Kilos",
                            fontColor: "black"
                        }
                    },
                ],
                xAxes: [{
                    scaleLabel: {
                        display: true,
                        labelString: "Detalle",
                        fontColor: "black"
                    }
                }],
            },
            tooltips: {
                callbacks: {
                    label: function (tooltipItem, chart) {
                        var datasetLabel = chart.datasets[tooltipItem.datasetIndex].label || '';
                        return datasetLabel + " " + (tooltipItem.yLabel).toLocaleString('en-US', noTruncarDecimales);
                    }
                }
            },
            interaction: {
                intersect: false,
            },
            title: {
                display: true,
                text: 'Cantidades chatarra',
                fontSize: 18,
            },
        }

    });
    //    char6 = new Chart(ctx2, {
    //        type: "bar",
    //        data: chartdata2,
    //        options: {
    //            responsive: true,
    //            scales: {
    //                yAxes: [{
    //                    ticks: {
    //                        callback: function (valor, index, valores) {
    //                            return Number(valor).toFixed(2).replace(/\B(?=(\d{3})+(?!\d))/g, ',');
    //                        }
    //                        //    stepSize: 5,                
    //                    },
    //                    scaleLabel: {
    //                        display: true,
    //                        labelString: "Resultados",
    //                        fontColor: "black"
    //                    }
    //                }],
    //                xAxes: [{
    //                    scaleLabel: {
    //                        display: true,
    //                        labelString: "Detalle",
    //                        fontColor: "black"
    //                    }
    //                }],
    //            },
    //            tooltips: {
    //                callbacks: {
    //                    label: function (tooltipItem, chart) {
    //                        var datasetLabel = chart.datasets[tooltipItem.datasetIndex].label || '';
    //                        return datasetLabel + " " + (tooltipItem.yLabel).toLocaleString('en-US', noTruncarDecimales) + " kg";
    //                    }
    //                }
    //            },
    //            interaction: {
    //                intersect: false,
    //            },
    //            title: {
    //                display: true,
    //                text: 'Pesos Chatarra(kg)',
    //                fontSize: 18,
    //            },
    //        }
    //    });
}
function graficoClienteClase() {
    const noTruncarDecimales = { maximumFractionDigits: 2, minimumFractionDigits: 2 };

    var groupByVendedor = function (miarray, prop) {
        return miarray.reduce(function (groups, item) {
            var val = item[prop];
            groups[val] = groups[val] || { Cantidad: 0, Peso_Real: 0, Peso_Promedio: 0, Vendedor: val };
            groups[val].Cantidad += item.Cantidad;
            var val1 = groups[val].Cantidad += item.Cantidad;
            groups[val].Peso_Real += item.Peso_Real;
            var val2 = groups[val].Peso_Real += item.Peso_Real;
            groups[val].Peso_Promedio = (val2 / val1);
            return groups;
        }, {});
    }

    if (char7 != null) {
        char7.destroy();
    }
    //if (char8 != null) {
    //    char8.destroy();
    //}
    var ctx = $("#myChart7");
    //var ctx2 = $("#myChart8");
    var nombreVendedor = [];
    var valorNombreVendedor = [];
    var valorKgVendedor = [];

    tablaResumenClienteClase(groupByVendedor(valor, 'Cliente_Clase'));

    for (var i in groupByVendedor(valor, 'Cliente_Clase')) {
        nombreVendedor.push(groupByVendedor(valor, 'Cliente_Clase')[i].Vendedor);
        valorNombreVendedor.push(groupByVendedor(valor, 'Cliente_Clase')[i].Cantidad);
        valorKgVendedor.push(groupByVendedor(valor, 'Cliente_Clase')[i].Peso_Real.toFixed(2));
    }
    var chartdata = {
        labels: nombreVendedor,
        datasets: [{
            label: "Cantidad:",
            //backgroundColor: color,
            //borderColor: color,
            borderWidth: 2,
            cubicInterpolationMode: 'monotone',
            backgroundColor: 'rgba(7,59,251,0.5)',// Color de fondo
            borderColor: 'rgba(7,59,251,0.5)',// Color del borde
            data: valorNombreVendedor,
            pointRadius: 3,
            pointHoverRadius: 4,
            pointHitRadius: 10,
            fill: false,
            yAxisID: 'A',

            //yAxisID: "left"

        },
        {
            label: "Peso:",
            //backgroundColor: color,
            //borderColor: color,
            borderWidth: 2,
            cubicInterpolationMode: 'monotone',
            backgroundColor: 'rgba(2,162,0,0.5)',// Color de fondo
            borderColor: 'rgba(2,162,0,0.5)',// Color del borde
            data: valorKgVendedor,
            pointRadius: 3,
            pointHoverRadius: 4,
            pointHitRadius: 10,
            fill: false,
            yAxisID: 'B',
            type: 'line',
            lineTension: 0,

            // yAxisID: "right",

        }
        ]
    };
    //var chartdata2 = {
    //    labels: nombreVendedor,
    //    datasets: [{
    //        label: "Peso:",
    //        //backgroundColor: color,
    //        //borderColor: color,
    //        borderWidth: 2,
    //        cubicInterpolationMode: 'monotone',
    //        backgroundColor: 'rgba(74,178,51,0.5)',// Color de fondo
    //        borderColor: 'rgba(74,178,51,0.5)',// Color del borde
    //        data: valorKgVendedor,
    //        pointRadius: 3,
    //        pointHoverRadius: 4,
    //        pointHitRadius: 10,
    //        fill: false
    //    }
    //    ]
    //};
    char7 = new Chart(ctx, {
        type: "bar",
        data: chartdata,
        options: {
            responsive: true,
            scales: {
                yAxes: [
                    {
                        id: 'A',
                        //type: 'linear',
                        position: 'left',
                        ticks: {
                            callback: function (valor, index, valores) {
                                return Number(valor).toFixed(0).replace(/\B(?=(\d{3})+(?!\d))/g, ',');
                            }
                        },
                        scaleLabel: {
                            display: true,
                            labelString: "Cantidades",
                            fontColor: "black"
                        }
                    }, {
                        id: 'B',
                        //type: 'linear',
                        position: 'right',
                        ticks: {
                            callback: function (valor, index, valores) {
                                return Number(valor).toFixed(0).replace(/\B(?=(\d{3})+(?!\d))/g, ',');
                            }
                        },
                        scaleLabel: {
                            display: true,
                            labelString: "Kilos",
                            fontColor: "black"
                        }
                    },
                ],
                xAxes: [{
                    scaleLabel: {
                        display: true,
                        labelString: "Detalle",
                        fontColor: "black"
                    }
                }],
            },
            tooltips: {
                callbacks: {
                    label: function (tooltipItem, chart) {
                        var datasetLabel = chart.datasets[tooltipItem.datasetIndex].label || '';
                        return datasetLabel + " " + (tooltipItem.yLabel).toLocaleString('en-US', noTruncarDecimales);
                    }
                }
            },
            interaction: {
                intersect: false,
            },
            title: {
                display: true,
                text: 'Cantidades chatarra',
                fontSize: 18,
            },
        }

    });
    //char8 = new Chart(ctx2, {
    //    type: "bar",
    //    data: chartdata2,
    //    options: {
    //        responsive: true,
    //        scales: {
    //            yAxes: [{
    //                ticks: {
    //                    callback: function (valor, index, valores) {
    //                        return Number(valor).toFixed(2).replace(/\B(?=(\d{3})+(?!\d))/g, ',');
    //                    }
    //                    //    stepSize: 5,                
    //                },
    //                scaleLabel: {
    //                    display: true,
    //                    labelString: "Resultados",
    //                    fontColor: "black"
    //                }
    //            }],
    //            xAxes: [{
    //                scaleLabel: {
    //                    display: true,
    //                    labelString: "Detalle",
    //                    fontColor: "black"
    //                }
    //            }],
    //        },
    //        tooltips: {
    //            callbacks: {
    //                label: function (tooltipItem, chart) {
    //                    var datasetLabel = chart.datasets[tooltipItem.datasetIndex].label || '';
    //                    return datasetLabel + " " + (tooltipItem.yLabel).toLocaleString('en-US', noTruncarDecimales) + " kg";
    //                }
    //            }
    //        },
    //        interaction: {
    //            intersect: false,
    //        },
    //        title: {
    //            display: true,
    //            text: 'Pesos Chatarra(kg)',
    //            fontSize: 18,
    //        },
    //    }
    //});
}
function graficoTipoIngreso() {
    const noTruncarDecimales = { maximumFractionDigits: 2, minimumFractionDigits: 2 };
    var groupByVendedor = function (miarray, prop) {
        return miarray.reduce(function (groups, item) {
            var val = item[prop];
            groups[val] = groups[val] || { Cantidad: 0, Peso_Real: 0, Peso_Promedio: 0, Vendedor: val };
            groups[val].Cantidad += item.Cantidad;
            var val1 = groups[val].Cantidad += item.Cantidad;
            groups[val].Peso_Real += item.Peso_Real;
            var val2 = groups[val].Peso_Real += item.Peso_Real;
            groups[val].Peso_Promedio = (val2 / val1);
            return groups;
        }, {});
    }

    if (char9 != null) {
        char9.destroy();
    }
    //if (char10 != null) {
    //    char10.destroy();
    //}
    var ctx = $("#myChart9");
    //var ctx2 = $("#myChart10");
    var nombreVendedor = [];
    var valorNombreVendedor = [];
    var valorKgVendedor = [];

    tablaResumenTipoIngreso(groupByVendedor(valor, 'Tipo_Ingreso'));

    for (var i in groupByVendedor(valor, 'Tipo_Ingreso')) {
        nombreVendedor.push(groupByVendedor(valor, 'Tipo_Ingreso')[i].Vendedor);
        valorNombreVendedor.push(groupByVendedor(valor, 'Tipo_Ingreso')[i].Cantidad);
        valorKgVendedor.push(groupByVendedor(valor, 'Tipo_Ingreso')[i].Peso_Real.toFixed(2));
    }
    var chartdata = {
        labels: nombreVendedor,
        datasets: [{
            label: "Cantidad:",
            //backgroundColor: color,
            //borderColor: color,
            borderWidth: 2,
            cubicInterpolationMode: 'monotone',
            backgroundColor: 'rgba(7,59,251,0.5)',// Color de fondo
            borderColor: 'rgba(7,59,251,0.5)',// Color del borde
            data: valorNombreVendedor,
            pointRadius: 3,
            pointHoverRadius: 4,
            pointHitRadius: 10,
            fill: false,
            yAxisID: 'A',

            //yAxisID: "left"

        },
        {
            label: "Peso:",
            //backgroundColor: color,
            //borderColor: color,
            borderWidth: 2,
            cubicInterpolationMode: 'monotone',
            backgroundColor: 'rgba(2,162,0,0.5)',// Color de fondo
            borderColor: 'rgba(2,162,0,0.5)',// Color del borde
            data: valorKgVendedor,
            pointRadius: 3,
            pointHoverRadius: 4,
            pointHitRadius: 10,
            fill: false,
            yAxisID: 'B',
            type: 'line',
            lineTension: 0,

            // yAxisID: "right",

        }
        ]
    };
    //var chartdata2 = {
    //    labels: nombreVendedor,
    //    datasets: [{
    //        label: "Peso:",
    //        //backgroundColor: color,
    //        //borderColor: color,
    //        borderWidth: 2,
    //        cubicInterpolationMode: 'monotone',
    //        backgroundColor: 'rgba(74,178,51,0.5)',// Color de fondo
    //        borderColor: 'rgba(74,178,51,0.5)',// Color del borde
    //        data: valorKgVendedor,
    //        pointRadius: 3,
    //        pointHoverRadius: 4,
    //        pointHitRadius: 10,
    //        fill: false
    //    }
    //    ]
    //};
    char9 = new Chart(ctx, {
        type: "bar",
        data: chartdata,
        options: {
            responsive: true,
            scales: {
                yAxes: [
                    {
                        id: 'A',
                        //type: 'linear',
                        position: 'left',
                        ticks: {
                            callback: function (valor, index, valores) {
                                return Number(valor).toFixed(0).replace(/\B(?=(\d{3})+(?!\d))/g, ',');
                            }
                        },
                        scaleLabel: {
                            display: true,
                            labelString: "Cantidades",
                            fontColor: "black"
                        }
                    }, {
                        id: 'B',
                        //type: 'linear',
                        position: 'right',
                        ticks: {
                            callback: function (valor, index, valores) {
                                return Number(valor).toFixed(0).replace(/\B(?=(\d{3})+(?!\d))/g, ',');
                            }
                        },
                        scaleLabel: {
                            display: true,
                            labelString: "Kilos",
                            fontColor: "black"
                        }
                    },
                ],
                xAxes: [{
                    scaleLabel: {
                        display: true,
                        labelString: "Detalle",
                        fontColor: "black"
                    }
                }],
            },
            tooltips: {
                callbacks: {
                    label: function (tooltipItem, chart) {
                        var datasetLabel = chart.datasets[tooltipItem.datasetIndex].label || '';
                        return datasetLabel + " " + (tooltipItem.yLabel).toLocaleString('en-US', noTruncarDecimales);
                    }
                }
            },
            interaction: {
                intersect: false,
            },
            title: {
                display: true,
                text: 'Cantidades chatarra',
                fontSize: 18,
            },
        }
    });
}
function graficoMeses() {
    const noTruncarDecimales = { maximumFractionDigits: 2, minimumFractionDigits: 2 };

    var groupByVendedor = function (miarray, prop) {
        return miarray.reduce(function (groups, item) {

            var val = item[prop];
            if (val == 1) {
                val = "Enero";
            } if (val == 2) {
                val = "Febrero";
            } if (val == 3) {
                val = "Marzo";
            } if (val == 4) {
                val = "Abril";
            } if (val == 5) {
                val = "Mayo";
            } if (val == 6) {
                val = "Junio";
            } if (val == 7) {
                val = "Julio";
            } if (val == 8) {
                val = "Agosto";
            } if (val == 9) {
                val = "Septiembre";
            } if (val == 10) {
                val = "Octubre";
            } if (val == 11) {
                val = "Noviembre";
            } if (val == 12) {
                val = "Diciembre";
            }
            groups[val] = groups[val] || { Cantidad: 0, Peso_Real: 0, Peso_Promedio: 0, Vendedor: val };
            groups[val].Cantidad += item.Cantidad;
            var val1 = groups[val].Cantidad += item.Cantidad;
            groups[val].Peso_Real += item.Peso_Real;
            var val2 = groups[val].Peso_Real += item.Peso_Real;
            groups[val].Peso_Promedio = (val2 / val1);
            return groups;
        }, {});
    }

    if (char11 != null) {
        char11.destroy();
    }
    //if (char12 != null) {
    //    char12.destroy();
    //}
    var ctx = $("#myChart11");
    //var ctx2 = $("#myChart12");
    var nombreVendedor = [];
    var valorNombreVendedor = [];
    var valorKgVendedor = [];

    tablaResumenMeses(groupByVendedor(valor, 'FechaRegistro2'));

    for (var i in groupByVendedor(valor, 'FechaRegistro2')) {
        nombreVendedor.push(groupByVendedor(valor, 'FechaRegistro2')[i].Vendedor);
        valorNombreVendedor.push(groupByVendedor(valor, 'FechaRegistro2')[i].Cantidad);
        valorKgVendedor.push(groupByVendedor(valor, 'FechaRegistro2')[i].Peso_Real.toFixed(2));
    }
    var chartdata = {
        labels: nombreVendedor,
        datasets: [{
            label: "Cantidad:",
            //backgroundColor: color,
            //borderColor: color,
            borderWidth: 2,
            cubicInterpolationMode: 'monotone',
            backgroundColor: 'rgba(7,59,251,0.5)',// Color de fondo
            borderColor: 'rgba(7,59,251,0.5)',// Color del borde
            data: valorNombreVendedor,
            pointRadius: 3,
            pointHoverRadius: 4,
            pointHitRadius: 10,
            fill: false,
            yAxisID: 'A',

            //yAxisID: "left"

        },
        {
            label: "Peso:",
            //backgroundColor: color,
            //borderColor: color,
            borderWidth: 2,
            cubicInterpolationMode: 'monotone',
            backgroundColor: 'rgba(2,162,0,0.5)',// Color de fondo
            borderColor: 'rgba(2,162,0,0.5)',// Color del borde
            data: valorKgVendedor,
            pointRadius: 3,
            pointHoverRadius: 4,
            pointHitRadius: 10,
            fill: false,
            yAxisID: 'B',
            type: 'line',
            lineTension: 0,

            // yAxisID: "right",

        }

        ]
    };
    //var chartdata2 = {
    //    labels: nombreVendedor,
    //    datasets: [{
    //        label: "Peso:",
    //        //backgroundColor: color,
    //        //borderColor: color,
    //        borderWidth: 2,
    //        cubicInterpolationMode: 'monotone',
    //        backgroundColor: 'rgba(74,178,51,0.5)',// Color de fondo
    //        borderColor: 'rgba(74,178,51,0.5)',// Color del borde
    //        data: valorKgVendedor,
    //        pointRadius: 3,
    //        pointHoverRadius: 4,
    //        pointHitRadius: 10,
    //        fill: false
    //    }
    //        ,
    //        {
    //            label: "Cantidad:",
    //            //backgroundColor: color,
    //            //borderColor: color,
    //            borderWidth: 2,
    //            cubicInterpolationMode: 'monotone',
    //            backgroundColor: 'rgba(241,11,36,0.5)',// Color de fondo
    //            borderColor: 'rgba(214,11,36,0.5)',// Color del borde
    //            data: valorKgVendedor,
    //            pointRadius: 3,
    //            pointHoverRadius: 4,
    //            pointHitRadius: 10,
    //            fill: false,
    //            type: 'line',

    //        }
    //    ]
    //};
    char11 = new Chart(ctx, {
        type: "bar",
        data: chartdata,
        options: {
            responsive: true,
            scales: {
                yAxes: [
                    {
                        id: 'A',
                        //type: 'linear',
                        position: 'left',
                        ticks: {
                            callback: function (valor, index, valores) {
                                return Number(valor).toFixed(0).replace(/\B(?=(\d{3})+(?!\d))/g, ',');
                            }
                        },
                        scaleLabel: {
                            display: true,
                            labelString: "Cantidades",
                            fontColor: "black"
                        }
                    }, {
                        id: 'B',
                        //type: 'linear',
                        position: 'right',
                        ticks: {
                            callback: function (valor, index, valores) {
                                return Number(valor).toFixed(0).replace(/\B(?=(\d{3})+(?!\d))/g, ',');
                            }
                        },
                        scaleLabel: {
                            display: true,
                            labelString: "Kilos",
                            fontColor: "black"
                        }
                    },
                ],
                xAxes: [{
                    scaleLabel: {
                        display: true,
                        labelString: "Detalle",
                        fontColor: "black"
                    }
                }],
            },
            tooltips: {
                callbacks: {
                    label: function (tooltipItem, chart) {
                        var datasetLabel = chart.datasets[tooltipItem.datasetIndex].label || '';
                        return datasetLabel + " " + (tooltipItem.yLabel).toLocaleString('en-US', noTruncarDecimales);
                    }
                }
            },
            interaction: {
                intersect: false,
            },
            title: {
                display: true,
                text: 'Cantidades chatarra',
                fontSize: 18,
            },
        }

    });
}
