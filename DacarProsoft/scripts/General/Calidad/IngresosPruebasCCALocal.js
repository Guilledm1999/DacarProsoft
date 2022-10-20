$(document).ready(function () {
    ObtenerCodigoIngreso();
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


function CargarModelosBateriasVehiculos() {
    if ($("#txtTipoBateria").val() != null && $("#txtTipoBateria").val() != "") {
        $("#txtModelo").empty();
        $("#txtModelo").append('<option value="">--Seleccione--</option>');
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
        $("#txtModelo").append('<option value="">--Seleccione--</option>');
    }
}

function ObtenerCodigoIngreso() {
    $.ajax({
        url: '../Calidad/ConsultarCodigoRegistroPruebaCCALocal',
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
 //   $("#ModalAnexos").modal("show");
}
function CargarValorTeorico() {
    $.ajax({
        url: '../Calidad/ConsultarCCATeorico?modelo=' + $("#txtModelo option:selected").text(),
        type: 'get',
        success: function (respuesta) {
            $("#txtDatoTeoricoPrueba").val(respuesta);
        },
        error: function () {
            $("#MensajeErrorGeneral").show('fade');
            setTimeout(function () {
                $("#MensajeErrorGeneral").fadeOut(1500);
            }, 3000); return;
        }
    });
}

function CalcularResultado() {
    var val1 = $("#txtResultadoFinal").val();
    var val2 = $("#txtDatoTeoricoPrueba").val();
    var result = (val1 * 100) / val2;
    $("#txtCalificacion").val(result.toFixed(2));
}

function RegistrarMedicionCCA() {
    var formdata = new FormData();
    formdata.append("FechaIngreso", $("#txtFechaIngreso").val());
    formdata.append("CodigoIngreso", $("#txtCodigoIngreso").val());
    formdata.append("TipoBateria", $("#txtTipoBateria option:selected").text());
    formdata.append("ModeloBateria", $("#txtModelo option:selected").text());
    formdata.append("Separador", $("#txtSeparador option:selected").text());
    formdata.append("LoteEnsamble", $("#txtLoteEnsamble").val());
    formdata.append("LoteCarga", $("#txtLoteCarga").val());
    formdata.append("Temperatura", $("#txtTemperaturaPrueba").val());
    formdata.append("Peso", $("#txtPeso").val());
    formdata.append("Voltaje", $("#txtVoltaje").val());
    formdata.append("DatoTeorico", $("#txtDatoTeoricoPrueba").val());
    formdata.append("Resultado", $("#txtResultadoFinal").val());
    formdata.append("Rendimiento", $("#txtCalificacion").val());
    formdata.append("Observaciones", $("#txtObservaciones").val());
    formdata.append("CodigoBateria", $("#txtCodigoBateria").val());

    var files = $("#txtAnexos").get(0).files;

    for (var i = 0; i < files.length; i++) {
        formdata.append("archivos", files[i]);
    }

    $.ajax({
        url: '../Calidad/RegistrarPruebasLaboratorioCCA',
        type: 'post',
        processData: false,
        contentType: false,
        data:
            formdata,
        success: function (msg) {
            if (msg == "True") {
                LimpiarResultados();
                ObtenerCodigoIngreso();
                $("#MensajeGuardado").show('fade');
                setTimeout(function () {
                    $("#MensajeGuardado").fadeOut(1500);
                }, 3000);

            } else {
                $("#MensajeErrorGeneral").show('fade');
                setTimeout(function () {
                    $("#MensajeErrorGeneral").fadeOut(1500);
                }, 3000); return;
            }
        },
        error: function () {
            $("#MensajeErrorGeneral").show('fade');
            setTimeout(function () {
                $("#MensajeErrorGeneral").fadeOut(1500);
            }, 3000); return;
        }
    });
}
function LimpiarResultados() {
    $("#txtFechaIngreso").val("");
    $("#txtCodigoIngreso").val("");
    $("#txtTipoBateria option[value=''").attr("selected", true);
    $("#txtModelo option[value=''").attr("selected", true);
    $("#txtSeparador option[value=''").attr("selected", true);
    $("#txtLoteEnsamble").val("");
    $("#txtLoteCarga").val("");
    $("#txtTemperaturaPrueba").val("");
    $("#txtPeso").val("");
    $("#txtVoltaje").val(""); 
    $("#txtDatoTeoricoPrueba").val("");
    $("#txtResultadoFinal").val("");
    $("#txtObservaciones").val("");
    $("#txtCalificacion").val("");
    $("#txtAnexos").val("");
    $("#txtCodigoBateria").val("")
}

function comprobarCampos() {
    var val1= $("#txtFechaIngreso").val();
    var val2= $("#txtCodigoIngreso").val();
    var val3 = $("#txtTipoBateria option:selected").text();
    var val4 = $("#txtModelo option:selected").text();
    var val5 = $("#txtSeparador option:selected").text();
    var val6 = $("#txtLoteEnsamble").val();
    var val7 = $("#txtLoteCarga").val();
    var val8 = $("#txtTemperaturaPrueba").val();
    var val9 = $("#txtPeso").val();
    var val10 = $("#txtVoltaje").val();
    var val11= $("#txtDatoTeoricoPrueba").val();
    var val12 = $("#txtResultadoFinal").val();
    //var val13 = $("#txtObservaciones").val();
    var val14 = $("#txtCalificacion").val();
    var val15 = $("#txtCodigoBateria").val();
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
    }
    if (val3.length == "--Seleccione--") {
        $("#MensajeCompleteCampos").show('fade');
        setTimeout(function () {
            $("#MensajeCompleteCampos").fadeOut(1500);
        }, 3000);
        return;
    }
    if (val4.length == "--Seleccione--") {
        $("#MensajeCompleteCampos").show('fade');
        setTimeout(function () {
            $("#MensajeCompleteCampos").fadeOut(1500);
        }, 3000);
        return;
    }
    if (val5.length == "--Seleccione--") {
        $("#MensajeCompleteCampos").show('fade');
        setTimeout(function () {
            $("#MensajeCompleteCampos").fadeOut(1500);
        }, 3000);
        return;
    }
    if (val6.length == 0) {
        $("#MensajeCompleteCampos").show('fade');
        setTimeout(function () {
            $("#MensajeCompleteCampos").fadeOut(1500);
        }, 3000);
        return;
    }
    if (val7.length == 0) {
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
    }
    if (val9.length == 0) {
        $("#MensajeCompleteCampos").show('fade');
        setTimeout(function () {
            $("#MensajeCompleteCampos").fadeOut(1500);
        }, 3000);
        return;
    }
    if (val10.length == 0) {
        $("#MensajeCompleteCampos").show('fade');
        setTimeout(function () {
            $("#MensajeCompleteCampos").fadeOut(1500);
        }, 3000);
        return;
    }
    if (val11.length == 0) {
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
    //if (val13.length == 0) {
    //    $("#MensajeCompleteCampos").show('fade');
    //    setTimeout(function () {
    //        $("#MensajeCompleteCampos").fadeOut(1500);
    //    }, 3000);
    //    return;
    //}
    if (val14.length == 0) {
        $("#MensajeCompleteCampos").show('fade');
        setTimeout(function () {
            $("#MensajeCompleteCampos").fadeOut(1500);
        }, 3000);
        return;
    }
    if (val15.length == 0) {
        $("#MensajeCompleteCampos").show('fade');
        setTimeout(function () {
            $("#MensajeCompleteCampos").fadeOut(1500);
        }, 3000);
        return;
    }
    RegistrarMedicionCCA();
}