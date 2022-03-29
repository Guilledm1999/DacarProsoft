var IdMedicionCarga = null;

$(document).ready(function () {
    ObtenerCodigoIngreso();
    ConsultaRegistrosPruebasLaboratorio();
});

$('#LinkClose').on("click", function (e) {
    $("#MensajeGuardado").hide('fade');
});

$('#LinkClose2').on("click", function (e) {
    $("#MensajeErrorInesperado").hide('fade');
});

$('#LinkClose5').on("click", function (e) {
    $("#MensajeCompleteCampos").hide('fade');
});

function RegistrarPrueba() {
    var formdata = new FormData();
    formdata.append("FechaIngreso", $("#txtFechaIngreso").val());
    formdata.append("CodigoIngreso", $("#txtCodigoIngreso").val());
    formdata.append("Marca", $("#txtMarca option:selected").text()); 
    formdata.append("PreAcondicionamiento", $("#txtPreAcondicionamiento option:selected").text());
    formdata.append("TipoBateria", $("#txtTipoBateria option:selected").text());
    formdata.append("Modelo", $("#txtModelo option:selected").text());
    formdata.append("Separador", $("#txtSeparador option:selected").text());
    formdata.append("LoteEnsamble", $("#txtLoteEnsamble").val());
    formdata.append("LoteCarga", $("#txtLoteCarga").val());
    formdata.append("Peso", $("#txtPeso").val());
    formdata.append("Voltaje", $("#txtVoltaje").val());

    $.ajax({
        type: 'POST',
        url: "../Calidad/RegistrarPruebasMedicionDescarga",
        processData: false,
        contentType: false,
        data:
            formdata,
        success: function (msg) 
        {
            console.log("retorna:"+msg);
            if (msg == "True") {
                LimpiarResultados();
                ObtenerCodigoIngreso();
                ConsultaRegistrosPruebasLaboratorio();

                $("#MensajeGuardado").show('fade');
                setTimeout(function () {
                    $("#MensajeGuardado").fadeOut(1500);
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

function LimpiarResultados() {
    $("#txtFechaIngreso").val("");
    $("#txtCodigoIngreso").val("");
    $("#txtMarca option[value=''").attr("selected", true);
    $("#txtPreAcondicionamiento").val("");
    $("#txtTipoBateria option[value=''").attr("selected", true);
    $("#txtModelo option[value=''").attr("selected", true);
    $("#txtSeparador option[value=''").attr("selected", true);
    $("#txtLoteEnsamble").val("");
    $("#txtLoteCarga").val("");
    $("#txtPeso").val("");
    $("#txtVoltaje").val("");

}

$('#btnRegistrarPrueba').on("click", function (e) {

    var val1= $("#txtFechaIngreso").val();
    var val2 = $("#txtCodigoIngreso").val();
    var val3 = $("#txtMarca option:selected").val();
    var val6 = $("#txtPreAcondicionamiento").val();
    var val8 = $("#txtModelo option:selected").val();
    var val9 = $("#txtSeparador option:selected").val();
    var val11 = $("#txtLoteEnsamble").val();
    var val12 = $("#txtLoteCarga").val();
    var val14 = $("#txtPeso").val();
    var val15 = $("#txtVoltaje").val();


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
    }  if (val6.length == 0) {
        $("#MensajeCompleteCampos").show('fade');
        setTimeout(function () {
            $("#MensajeCompleteCampos").fadeOut(1500);
        }, 3000);
        return;
    }

    if (val8.length == 0) {
        $("#MensajeCompleteCampos").show('fade');
        setTimeout(function () {
            $("#MensajeCompleteCampos").fadeOut(1500);
        }, 3000);
        return;
    } if (val9.length == 0) {
        $("#MensajeCompleteCampos").show('fade');
        setTimeout(function () {
            $("#MensajeCompleteCampos").fadeOut(1500);
        }, 3000);
        return;
    }  if (val11.length == 0) {
        $("#MensajeCompleteCampos").show('fade');
        setTimeout(function () {
            $("#MensajeCompleteCampos").fadeOut(1500);
        }, 3000);
        return;
    }
    if (val12.length == 0) {
        $("#MensajeCompleteCampos").show('fade');
        setTimeout(function () {
            $("#MensajeCompleteCampos").fadeOut(1500);
        }, 3000);
        return;
    }
    if (val14.length == 0) {
        $("#MensajeCompleteCampos").show('fade');
        setTimeout(function () {
            $("#MensajeCompleteCampos").fadeOut(1500);
        }, 3000);
        return;
    } if (val15.length == 0) {
        $("#MensajeCompleteCampos").show('fade');
        setTimeout(function () {
            $("#MensajeCompleteCampos").fadeOut(1500);
        }, 3000);
        return;
    } 
    RegistrarPrueba();

});

function CargarModelosBateriasVehiculos() {
    if ($("#txtTipoBateria").val() != null && $("#txtTipoBateria").val() != "") {
        $("#txtModelo").empty();
        $("#txtModelo").append('<option value="">--Seleccione el modelo--</option>');
        $.ajax({
            type: 'POST',
            url: "../Calidad/ConsultarModelosBateriasPorTipoVehiculo",
            dataType: 'json',
            data: { id: $("#txtTipoBateria option:selected").val() },
            success: function (articulos) {
                $.each(articulos, function (i, articulo) {
                    $("#txtModelo").append('<option value="' + articulo.Value + '">' +
                        articulo.Text + '</option>');
                });
            },
        })
    } else {
        $("#txtModelo").empty();
        $("#txtModelo").append('<option value="">--Seleccione el modelo--</option>');
    }
}


function ObtenerCodigoIngreso() {
    $.ajax({
        url: '../Calidad/ConsultarCodigoRegistroMedicionDeDescarga',

        type: 'get',
        success: function (respuestas) {
            $("#txtCodigoIngreso").val(respuestas);
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


function ConsultaRegistrosPruebasLaboratorio() {
    $.ajax({
        url: "../Calidad/ConsultarRegistrosMedicionesDescarga",
        type: "GET"
        , success: function (msg) {

            const locale = getLocale();
            DevExpress.localization.locale(locale);

            $("#tblPruebasLaboratorioRegistrados").dxDataGrid({
                dataSource: msg,
                keyExpr: "PruebaLaboratorioCalidadId",
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
                        dataField: "FechaIngreso", caption: "Ingreso", alignment: "left", dataType: "date", allowHeaderFiltering: true, allowSearch: false, width: 100,
                       
                    },
                    {
                        dataField: "CodigoIngreso", caption: "Codigo", alignment: "left", allowHeaderFiltering: true, allowSearch: true, width: 100,
                       
                    },
                    {
                        dataField: "Marca", caption: "Marca", alignment: "left", allowHeaderFiltering: true, allowSearch: true,
                      

                    },                  
                    {
                        dataField: "PreAcondicionamiento", caption: "Pre-Acond.", alignment: "left", allowHeaderFiltering: true, allowSearch: true, width: 100,
                        
                    },
                    {
                        dataField: "TipoBateria", caption: "Tipo Bateria", alignment: "left", allowHeaderFiltering: true, allowSearch: true, width: 110,
                       
                    },
                    {
                        dataField: "Modelo", caption: "Modelo", alignment: "left", allowHeaderFiltering: true, allowSearch: true, width: 95,
                       
                    },
                    {
                        dataField: "Separador", caption: "Separador", alignment: "left", allowHeaderFiltering: true, allowSearch: true, width: 100,
                    },                 
                    {
                        dataField: "LoteEnsamble", caption: "L. Ensamble", alignment: "right", allowHeaderFiltering: false, allowSearch: true, width: 90,
                       
                    },
                    {
                        dataField: "LoteCarga", caption: "L. Carga", alignment: "right", allowHeaderFiltering: false, allowSearch: true, width: 90,
                       
                    },
                   
                    {
                        dataField: "Peso", caption: "Peso", alignment: "right", allowHeaderFiltering: false, allowSearch: false, width: 70,
                        format: {
                            type: "fixedPoint",
                            precision: 2,

                        },
                      
                    },
                    {
                        caption: "Medicion V.",
                        cellTemplate: function (container, options) {
                            var btnAnexo = "<a style='box-shadow: 2px 2px 5px #999 inset' onclick='ModalRegistrarMedicion(" + JSON.stringify(options.data) + ")'>Nuevo</a>";
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

function ModalRegistrarMedicion(modelo) {
    IdMedicionCarga = modelo.PruebaLaboratorioCalidadId;
    $("#ModalRegistrarNuevoRegistroCarga").modal("show");
}

function RegistrarNuevaMedicion() {
    var fechaMedicion = $("#txtFechaMedicion").val();
    var voltajeMedicion = $("#txtNuevoVoltajeMedicion").val();

    if (fechaMedicion.length == 0) {
        $("#MensajeCompleteCampos").show('fade');
        setTimeout(function () {
            $("#MensajeCompleteCampos").fadeOut(1500);
        }, 3000);
        return;
    }
    if (voltajeMedicion.length == 0) {
        $("#MensajeCompleteCampos").show('fade');
        setTimeout(function () {
            $("#MensajeCompleteCampos").fadeOut(1500);
        }, 3000);
        return;
    }
    RegistrarNuevaMedicionVoltaje(IdMedicionCarga);
}


function RegistrarNuevaMedicionVoltaje(idIngreso) {
    console.log("id:" + idIngreso);
    console.log("fecha:" + $("#txtFechaMedicion").val());
    console.log("voltaje:" + $("#txtNuevoVoltajeMedicion").val());

    $.ajax({
        url: '../Calidad/RegistrarNuevaMedicionDescarga',
        data: {
            IdPruebaMedicionDescarga: idIngreso, FechaMedicion: $("#txtFechaMedicion").val(), Voltaje: $("#txtNuevoVoltajeMedicion").val()
        },
        type: 'post',
        success: function (msg) {
            if (msg == "True") {
                $("#ModalRegistrarNuevoRegistroCarga").modal("hide");
                $("#txtFechaMedicion").val("");
                $("#txtNuevoVoltajeMedicion").val("")
                $("#MensajeGuardado").show('fade');
                setTimeout(function () {
                    $("#MensajeGuardado").fadeOut(1500);
                }, 3000);
            } else {
                $("#MensajeErrorInesperado").show('fade');
                setTimeout(function () {
                    $("#MensajeErrorInesperado").fadeOut(1500);
                }, 3000);
            }
        },
        error: function () {
            $("#MensajeErrorInesperado").show('fade');
            setTimeout(function () {
                $("#MensajeErrorInesperado").fadeOut(1500);
            }, 3000);
        }
    });


}