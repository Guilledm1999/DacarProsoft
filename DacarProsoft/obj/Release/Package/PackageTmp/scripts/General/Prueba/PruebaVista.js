var temp = null;
const Gen = [
    {   Descri: "Masculino" },
    {  Descri: "Femenino" },
];

var provincias = null;
 
$(document).ready(function () {
    ConsultaRegistros();
    GenerarGraficoPie()();

});
window.jsPDF = window.jspdf.jsPDF;

$('#LinkClose').on("click", function (e) {
    $("#MensajeErrorGeneral").hide('fade');
});
$('#LinkClose').on("click", function (e) {
    $("#MensajeGuardado").hide('fade');
});

$('#LinkClose5').on("click", function (e) {
    $("#MensajeCompleteCampos").hide('fade');
});

const countries = [{
    name: 'Land',
    area: 0.29,
}, {
    name: 'Water',
    area: 0.71,
}];

function GenerarGraficoPie()
{
    var Genero = null;
    $.ajax({
        url: "../Prueba/Genero",
        type: "GET",
        async: false,
        success: function (msg) {
            Genero = msg
        }
    })
    var Provincias = null;
    $.ajax({
        url: "../Prueba/ProvinciaPie",
        type: "GET",
        async: false,
        success: function (msg) {
            Provincias = msg
        }
    })

    const legendSettings = {
        verticalAlignment: 'bottom',
        horizontalAlignment: 'center',
        itemTextPosition: 'right',
        rowCount: 2,
    };
    const sizeGroupName = 'piesGroup';
    const seriesOptions = [{
        argumentField: 'Descripcion',
        valueField: 'Cantidad',
        label: {
            visible: true,
            format: 'percent',
        },
    }];

    $('#PieGenero').dxPieChart({
        sizeGroup: sizeGroupName,
        palette: 'Soft Pastel',
        title: 'Genero',
        legend: legendSettings,
        dataSource: Genero,
        series: seriesOptions,
    });

    $('#PieProvincia').dxPieChart({
        sizeGroup: sizeGroupName,
        palette: 'Soft Pastel',
        title: 'Provincia',
        legend: legendSettings,
        dataSource: Provincias,
        series: seriesOptions,
    });
}


function ConsultaRegistros() {
    $.ajax({
        url: "../Prueba/Provincias",
        type: "GET",
        async: false,
        success: function (msg) {
            provincias = msg
        }
    })
    console.log("si");
    $.ajax({
        url: "../Prueba/Datos",
        type: "GET",
        success: function (msg) {
        temp = msg;

            const locale = getLocale();
            DevExpress.localization.locale(locale);

            $("#tblMostrarDatos").dxDataGrid({
                dataSource: temp,
                keyExpr: 'IdPersona',
                showBorders: true,
                columnAutoWidth: true,
                allowFixing: false,
                showBorders: true,
                showColumnLines: true,
                showRowLines: true,
                rowAlternationEnabled: false,
                allowColumnReordering: false,
                allowColumnResizing: false,

                searchPanel: {
                    visible: true,
                    width: 240,
                    placeholder: "Buscar..."
                },
                headerFilter: {
                    visible: true
                },
                editing: {
                    mode: 'row',
                    allowUpdating: true,
                    allowDeleting: true,
                    allowAdding: true,
                    useIcons: true,
                    texts: {
                        confirmDeleteMessage: 'Esta seguro de eliminar este item?'
                    }
                },
                columns: [

                   
                    {
                        dataField: "Nombre", caption: "Nombre", alignment: "left",  allowHeaderFiltering: true, allowSearch: false, /*width: 250,*/ allowSearch: true, headerFilter: {
                            allowSearch: true,
                        }, validationRules: [{ type: 'required' }],

                    },
                    {
                        dataField: "Cedula", caption: "Cedula", alignment: "left", allowHeaderFiltering: true, allowSearch: false, /*width: 250,*/ allowHeaderFiltering: true, headerFilter: {
                            allowSearch: true,
                        }, validationRules: [{
                            type: 'required',
                            type: 'pattern', pattern: '^\\d+$' }] //Validar entero
                        , editorOptions: { max: 10 }
                            
                    },
                    {
                        dataField: "Correo", caption: "Correo", alignment: "left", allowHeaderFiltering: true, allowSearch: false, /*width: 250,*/ allowHeaderFiltering: false, validationRules: [{ type: 'required' }]
                    },
                    {
                        dataField: "Genero", lookup: {
                            dataSource: Gen,
                            valueExpr: "Descri",
                            displayExpr: "Descri"
                        }
                    , alignment: "left", allowHeaderFiltering: true, allowSearch: false, /*width: 250,*/ allowHeaderFiltering: false, validationRules: [{ type: 'required' }]
                    },
                    {
                        dataField: "Provincia", lookup: {
                            dataSource: provincias,
                            valueExpr: "idProvincia",
                            displayExpr: "Descripcion"
                        }, caption: "Provincia", alignment: "left", allowHeaderFiltering: true, allowSearch: false, /*width: 250,*/ allowHeaderFiltering: false, validationRules: [{ type: 'required' }
                                                   ]
                    },
                   

                ],

                export: {
                    enabled: true,
                    formats: ['pdf'],
                    allowExportSelectedData: true,
                },
                onRowUpdating: function (options) {
                        this.oldData = Object.assign({}, options.oldData);
                    ActualizarEvento(options.newData, options.key);
                    GenerarGraficoPie();

                },
                onRowInserting: function (options) {
                    InsertarEvento((options.data));
                    GenerarGraficoPie();
                }
                ,
                onRowRemoving: function (options) {
                    EliminarEvento(options.data);
                    GenerarGraficoPie();
                }
            });
        },
        error: function (msg) {
            $("#MensajeErrorGeneral").show('fade');
            setTimeout(function () {
                $("#MensajeErrorGeneral").fadeOut(1500);
            }, 3000); return;
        }
    })
    function getLocale() {
        const storageLocale = sessionStorage.getItem('locale');
        return storageLocale != null ? storageLocale : 'es';
    }
}



function ActualizarEvento(valor, key) {
    $.ajax({
        url: '../Prueba/ActualizarEvento',
        type: 'POST',
        dataType: 'json',
        data: {
            crono: valor, Key: key
        },
        success: function (respuesta) {
            if (respuesta == "True") {

                ConsultaRegistros();
                GenerarGraficoPie();
            }
        }
    });
    
}


function InsertarEvento(valor) {
    console.log(valor);
    $.ajax({
        url: '../Prueba/InsertarEventoMes',
        type: 'POST',
        dataType: 'json',
        data: {
            crono: valor
        },
        success: function (respuesta) {

            if (respuesta == "True") {
                ConsultaRegistros();
                GenerarGraficoPie();
            }

        }
    });
   
}

function EliminarEvento(valor) {
    $.ajax({
        url: '../Prueba/EliminarEvento',
        type: 'POST',
        dataType: 'json',
        data: {
            crono: valor
        },
        success: function (respuesta) {

            if (respuesta == "True") {
               
                GenerarGraficoPie();
            }
        }
    });
}






   


$('#btnGuardar').on("click", function (e) {

    var val1 = $("#txtNombre").val();
    var val2 = $("#txtCedula").val();
    var val3 = $("#txtCorreo").val();
    var val4 = $("#txtGenero option:selected").val();
    var val5 = $("#txtProvincia option:selected").val();
 


    if (val1.length == 0) {
        $("#MensajeCompleteCampos").show('fade');
        setTimeout(function () {
            $("#MensajeCompleteCampos").fadeOut(1500);
        }, 3000);
        return;
    }
    if (val2.length == 0) {
        $("#MensajeCompleteCampos").show('fade');
        setTimeout(function () {
            $("#MensajeCompleteCampos").fadeOut(1500);
        }, 3000);
        return;
    } if (val3.length == 0) {
        $("#MensajeCompleteCampos").show('fade');
        setTimeout(function () {
            $("#MensajeCompleteCampos").fadeOut(1500);
        }, 3000);
        return;
    } if (val4.length == 0) {
        $("#MensajeCompleteCampos").show('fade');
        setTimeout(function () {
            $("#MensajeCompleteCampos").fadeOut(1500);
        }, 3000);
        return;
    } if (val5.length == 0) {
        $("#MensajeCompleteCampos").show('fade');
        setTimeout(function () {
            $("#MensajeCompleteCampos").fadeOut(1500);
        }, 3000);
        return;
    }
   
    
    RegistrarPrueba();
   
})



function RegistrarPrueba() {
    var formdata = new FormData();
    formdata.append("Nombre", $("#txtNombre").val());
    formdata.append("Cedula", $("#txtCedula").val());
    formdata.append("Correo", $("#txtCorreo").val());
    formdata.append("Genero", $("#txtGenero option:selected").text());
    formdata.append("Provincia", $("#txtProvincia option:selected").val());
    //formdata.append("Separador", $("#txtSeparador option:selected").text());

    $.ajax({
        type: 'POST',
        url: "..//Prueba/InsertarEventoMes",
        processData: false,
        contentType: false,
        data:
            formdata,
        success: function (msg) {
            console.log("retorna:" + msg);
            if (msg == "True") {
                LimpiarResultados();
                ConsultaRegistros();
                GenerarGraficoPie();

                $("#MensajeGuardado").show('fade');
                setTimeout(function () {
                    $("#MensajeGuardado").fadeOut(1500);
                }, 3000);

            } else {
                $("#MensajeErrorGeneral").show('fade');
                setTimeout(function () {
                    $("#MensajeErrorGeneral").fadeOut(1500);
                }, 3000);
            }
        },
        error: function (msg) {
            $("#MensajeErrorGeneral").show('fade');
            setTimeout(function () {
                $("#MensajeErrorGeneral").fadeOut(1500);
            }, 3000);
        }
    })
    
}
function ImprimirPdf() {
    var url = "../Prueba/Pdf";
    window.open(url);
    // $("#ModalListadoDePallets").modal("hide");
}

function Imprimir() {
    var formdata = new FormData();
    formdata.append("Nombre", $("#txtNombre").val());
    formdata.append("Cedula", $("#txtCedula").val());
    formdata.append("Correo", $("#txtCorreo").val());
    formdata.append("Genero", $("#txtGenero option:selected").text());
    formdata.append("Provincia", $("#txtProvincia option:selected").val());
    //formdata.append("Separador", $("#txtSeparador option:selected").text());

    $.ajax({
        type: 'POST',
        url: "..//Prueba/InsertarEventoMes",
        processData: false,
        contentType: false,
        data:
            formdata,
        success: function (msg) {
            console.log("retorna:" + msg);
            if (msg == "True") {
                LimpiarResultados();
                ConsultaRegistros();
                GenerarGraficoPie();

                $("#MensajeGuardado").show('fade');
                setTimeout(function () {
                    $("#MensajeGuardado").fadeOut(1500);
                }, 3000);

            } else {
                $("#MensajeErrorGeneral").show('fade');
                setTimeout(function () {
                    $("#MensajeErrorGeneral").fadeOut(1500);
                }, 3000);
            }
        },
        error: function (msg) {
            $("#MensajeErrorGeneral").show('fade');
            setTimeout(function () {
                $("#MensajeErrorGeneral").fadeOut(1500);
            }, 3000);
        }
    })

}



function LimpiarResultados() {
    $("#txtNombre").val("");
    $("#txtCedula").val("");
    $("#txtCorreo").val("");
    $("#txtGenero option[value=''").attr("selected", true);
    $("#txtProvincia option[value=''").attr("selected", true);
}