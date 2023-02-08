var PieLin = null;
var provincias = null;
var data = null;
var valor = null;
var chart1 = null;
var dataGrid = null;
$(document).ready(function () {
    Charts();
    PedidosRecibidos();

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

function Charts() {
    $(() => {
        $('#chart').dxChart({
            palette: 'Soft Pastel',
          //  dataSource,
            commonSeriesSettings: {
                argumentField: 'Mes',
                valueField: 'Total',
                type: 'bar',
                label: {
                    visible: true,
                    format: {
                        type: 'fixedPoint',
                        precision: 0,
                    },
                },
            },
          
            seriesTemplate: {
                nameField: 'Anio',
                customizeSeries(valueFromNameField) {
                    return valueFromNameField === 2009 ? { type: 'line', label: { visible: true }, color: '#ff3f7a' } : {};
                },
            },
            title: {
                text: 'Cantidad de garantias registradas por meses',

                font: {
                    color: "#007aff",
                    weight: 900,
                  
                },
                horizontalAlignment: "left",
               
            },
            export: {
                enabled: true,
            },
            legend: {
                verticalAlignment: 'bottom',
                horizontalAlignment: 'center',
            },
        });
    });


    $(() => {
        $('#pie').dxPieChart({
            palette: 'Soft Pastel',
           
            //PieLin,
           // title: 'Top internet languages',
            legend: {
                horizontalAlignment: 'center',
                verticalAlignment: 'bottom',
            },
            export: {
                enabled: true,
            },
            series: [{
                argumentField: 'Linea',
                valueField: 'Total',
                label: {
                    visible: true,
                    connector: {
                        visible: true,
                        width: 0.5,
                    },
                   // format: 'fixedPoint',
                    customizeText(point) {
                        return `${point.argumentText}: ${point.percentText} (${point.valueText})`;
                    },
                },
               
            }],
        });
    });
    //datasource del piechart
    $(function () {
        $("#pie").dxPieChart({
            dataSource: new DevExpress.data.DataSource({
                store: new DevExpress.data.CustomStore({
                    loadMode: "raw",
                    load: function () {
                        return $.getJSON('../Reportes/LineaMarca');
                    }
                }),
                paginate: false
            })
        });
    });
    $.ajax({
        url: "../Reportes/Provincias",
        type: "GET",
        asyc: false
        , success: function (msg) {
            provincias = msg;
        },
        error: function (msg) {
            $(".btn").attr("disabled", false);
            $(".btn-txt").text("Consultar");
            $("#MensajeErrorInesperado").show('fade');
            setTimeout(function () {
                $("#MensajeErrorInesperado").fadeOut(1500);
            }, 3000); return;
        }
    });
    //Mapa
    (async () => {

        const topology = await fetch(
            'https://code.highcharts.com/mapdata/countries/ec/ec-all.topo.json'
        ).then(response => response.json());

        // Prepare demo data. The data is joined to map using value of 'hc-key'
        // property by default. See API docs for 'joinBy' for more info on linking
        // data and map.
        
       
         data = [
            ['ec-gu', provincias.gu ?? 0], ['ec-es', provincias.es ?? 0], ['ec-cr', provincias.cr ?? 0], ['ec-im', provincias.im ?? 0],
            ['ec-su', provincias.su ?? 0], ['ec-se', provincias.se ?? 0], ['ec-sd', provincias.sd ?? 0], ['ec-az', provincias.az ?? 0],
            ['ec-eo', provincias.eo ?? 0], ['ec-lj', provincias.lj ?? 0], ['ec-zc', provincias.zc ?? 0], ['ec-cn', provincias.cn ?? 0],
            ['ec-bo', provincias.bo ?? 0], ['ec-ct', provincias.ct ?? 0], ['ec-lr', provincias.lr ?? 0], ['ec-mn', provincias.mn ?? 0],
            ['ec-cb', provincias.cb ?? 0], ['ec-ms', provincias.ms ?? 0], ['ec-pi', provincias.pi ?? 0], ['ec-pa', provincias.pa ?? 0],
            ['ec-1076', provincias.number ?? 0], ['ec-na', provincias.na ?? 0], ['ec-tu', provincias.tu ?? 0], ['ec-ga', provincias.ga ?? 0]
        ];

        // Create the chart
        chart1= Highcharts.mapChart('container', {
            chart: {
                map: topology
            },

            title: {
                text: 'Cantidad de garantias por provincias',
                align: 'left',
                style: {
                    color: '#007aff',
                    fontWeight: 'bold'
                }
            },

         
            mapNavigation: {
                enabled: true,
                buttonOptions: {
                    verticalAlign: 'bottom'
                }
            },

            colorAxis: {
                min: 0,
                max : 200,
                minColor: '#FFFFFF',
                maxColor: '#2c7be5',
               /* stops: [
                    [0, '#EFEFFF'],
                    [0.67, '#4444FF'],
                    [1, '#000022']
                ]*/
            },

            series: [{
                data: data,
                name: 'Garantias',
                states: {
                    hover: {
                        color: '#BADA55'
                    }
                },
                dataLabels: {
                    enabled: true,
                    format: '{point.name}'
                }
            }]
        });

    })();

}




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
        PedidosRecibidos();
    }
}

function PedidosRecibidos() {
    //var fechaInicio = $('#txtFechaInicio').val();
    //var fechaFin = $('#txtFechaFin').val();
    $.ajax({
        url: "../Reportes/ReporteGeneralDeGarantias"/*?FechaInicio=" + fechaInicio + "&FechaFin=" + fechaFin*/,
        type: "GET"
        , success: function (msg) {
            const locale = getLocale();
            DevExpress.localization.locale(locale);
            //ItemsPedidoCliente = msg;  
             dataGrid= $("#tblReporteGarantias").dxDataGrid({
                dataSource: msg,
                keyExpr: 'IngresoGarantiaId',
                showBorders: true,
                columnAutoWidth: false,
                showBorders: true,
                showColumnLines: true,
                rowAlternationEnabled: false,
                showRowLines: true,
                paging: {
                    pageSize: 10
                },
                selection: {
                    mode: 'multiple'
                },
                searchPanel: {
                    visible: true,
                    width: 240,
                    placeholder: "Buscar..."
                },
                headerFilter: {
                    visible: true
                },
                pager: {
                    visible: true,
                    allowedPageSizes: [5, 10, 50],
                    showPageSizeSelector: true,
                    showInfo: true,
                    showNavigationButtons: true
                },
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
                            saveAs(new Blob([buffer], { type: 'application/octet-stream' }), 'GarantiasIngresadas.xlsx');
                        });
                    });
                    e.cancel = true;
                },
                columns: [

                    { dataField: "IngresoGarantiaId", visible: false },
                    {
                        dataField: "Cedula", caption: "Cedula", alignment: "left", headerFilter: {
                            allowSearch: true
                        }
                    },
                    {
                        dataField: "RegistroGarantia", caption: "Fecha", alignment: "left", dataType: 'date', format: 'month'
                    },
                    {
                        dataField: "Nombre", caption: "Nombre", alignment: "left"
                    },
                    {
                        dataField: "Apellido", caption: "Apellido", alignment: "left"
                    },
                    {
                        dataField: "Email", caption: "Email", alignment: "left"
                    },
                    {
                        dataField: "Distribuidor", caption: "Distribuidor", alignment: "left"
                    },
                    {
                        dataField: "Ciudad", caption: "Ciudad", alignment: "left", allowFiltering: false
                    },
                    {
                        dataField: "Provincia", caption: "Provincia", alignment: "left", allowFiltering: false, 
                    },
                    {
                        dataField: "ModeloBateria", caption: "Modelo Bateria", alignment: "left"
                    },
                    //{
                    //    dataField: "NumeroBateria", caption: "Numero Bateria", alignment: "left"
                    //},
                    {
                        dataField: "NumeroGarantia", caption: "Numero Garantia", alignment: "left", allowFiltering: false
                    },
                    {
                        dataField: "RegistroGarantia", caption: "Registro Garantia", alignment: "left", dataType: "date"
                    }
                ],
                onRowDblClick: function (e) {
                    var FilaId = e.key;
                    $("#DetalleModal").modal("show");
                    document.getElementById('txtNombre').text   = 'Guille';
                    ConsultarDetalleDeItem(FilaId);
                   
                },
                 onContentReady: function (e) {
                    if (e.name === "changedProperty") {
                        // handle the property change here
                    }

                    Generarcharts();
                },


             });
            function getLocale() {
                const storageLocale = sessionStorage.getItem('locale');
                return storageLocale != null ? storageLocale : 'es';
            }
            Generarcharts();
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


function Generarcharts() {
    const filterExpr = $("#tblReporteGarantias").dxDataGrid("instance").getCombinedFilter(true);
  // $("#tblReporteGarantias").dxDataGrid("instance").refresh();
  var Prueba = $("#tblReporteGarantias").dxDataGrid("instance").getDataSource()
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
    CrearMapa(val);
    CrearPie(val);
}
function CrearPie(val) {
    $.ajax({
        url: "../Reportes/LineaMarcaActualizacion",
        type: "POST",
        data: { Linea: val },
        success: function (msg) {
            $("#pie").dxPieChart("option", "dataSource", msg);
        },
        error: function (msg) {
            $(".btn").attr("disabled", false);
            $(".btn-txt").text("Consultar");
            $("#MensajeErrorInesperado").show('fade');
            setTimeout(function () {
                $("#MensajeErrorInesperado").fadeOut(1500);
            }, 3000); return;
        }
    });
}

function CrearMapa(val) {

    $.ajax({
        url: "../Reportes/ProvinciasActualizacion",
        type: "POST",
        data: { Provincias: val },
        success: function (msg) {
            provincias = msg;
            data = [
                ['ec-gu', provincias.gu ?? 0], ['ec-es', provincias.es ?? 0], ['ec-cr', provincias.cr ?? 0], ['ec-im', provincias.im ?? 0],
                ['ec-su', provincias.su ?? 0], ['ec-se', provincias.se ?? 0], ['ec-sd', provincias.sd ?? 0], ['ec-az', provincias.az ?? 0],
                ['ec-eo', provincias.eo ?? 0], ['ec-lj', provincias.lj ?? 0], ['ec-zc', provincias.zc ?? 0], ['ec-cn', provincias.cn ?? 0],
                ['ec-bo', provincias.bo ?? 0], ['ec-ct', provincias.ct ?? 0], ['ec-lr', provincias.lr ?? 0], ['ec-mn', provincias.mn ?? 0],
                ['ec-cb', provincias.cb ?? 0], ['ec-ms', provincias.ms ?? 0], ['ec-pi', provincias.pi ?? 0], ['ec-pa', provincias.pa ?? 0],
                ['ec-1076', provincias.number ?? 0], ['ec-na', provincias.na ?? 0], ['ec-tu', provincias.tu ?? 0], ['ec-ga', provincias.ga ?? 0]
            ];
            var NumeroAlto = 0;
            for (var i = 0; i < data.length; i++) {
                var y = data[i][1].toString();
                if (data[i][1] > NumeroAlto) {
                    NumeroAlto = data[i][1];
                }
            }
           
            chart1.series[0].setData(data);
            chart1.colorAxis[0].update({
                max: NumeroAlto
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
    });
}

function crearTablaDescriptivaAndChart(val) {
    var meses = [];
    for (const color of val) {
        meses.push(color);
    }
    $.ajax({
        url: "../Reportes/MesesFiltro" ,
        type: "POST",
        data: { Meses: val },
         success: function (msg) {
             $("#chart").dxChart("option", "dataSource", msg);

        },
        error: function (msg) {
            $("#MjsInesperado").show('fade');
            setTimeout(function () {
                $("#MjsInesperado").fadeOut(1500);
            }, 3000); return;
        }
    })
   
}
   



function ConsultarDetalleDeItem(Id) {
    $.ajax({
        url: "../Reportes/ReporteIdGarantia?Id=" + Id,
        type: "GET"
        , success: function (msg) {
            document.getElementById('txtNombre').value = msg.Nombre + " " + msg.Apellido;
            document.getElementById('txtCorreo').value = msg.Email;
            document.getElementById('txtDistribuidor').value = msg.Distribuidor;
            document.getElementById('txtCiudad').value = msg.Ciudad;
            document.getElementById('txtModeloBateria').value = msg.ModeloBateria;
            document.getElementById('txtGarantia').value = msg.NumeroGarantia;
            document.getElementById('txtRegistro').value = msg.RegistroGarantia;
            document.getElementById('txtCelular').value = msg.Celular;
            document.getElementById('txtProvincia').value = msg.Provincia
            document.getElementById('txtMarca').value = msg.MarcaVehiculo;
            document.getElementById('txtAño').value = msg.AnioFabricacion;
            document.getElementById('txtKm').value = msg.Kilometraje+ " KM";
            document.getElementById('txtCedula').value = msg.Cedula;
            document.getElementById('txtFactura').value = msg.NumeroFactura;
           // document.getElementById('txtFechaRegistro').value = msg.RegistroGarantia;
            document.getElementById('txtModeloVehiculo').value = msg.ModeloVehiculo;


        },
        error: function (msg) {
            $("#MjsInesperado").show('fade');
            setTimeout(function () {
                $("#MjsInesperado").fadeOut(1500);
            }, 3000); return;
        }
    })
    $("#ModalDetalleItem").modal("show");
}

