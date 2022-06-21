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

function RegistrarPrueba() {
    var formdata = new FormData();
    formdata.append("FechaIngreso", $("#txtFechaIngreso").val());
    formdata.append("CodigoIngreso", $("#txtCodigoIngreso").val());
    formdata.append("Marca", $("#txtMarca option:selected").text());
    formdata.append("TipoNorma", $("#txtTipoNorma option:selected").text());
    formdata.append("Normativa", $("#txtNormativa option:selected").text());
    formdata.append("PreAcondicionamiento", $("#txtPreAcondicionamiento option:selected").text());
    formdata.append("TipoBateria", $("#txtTipoBateria option:selected").text());
    formdata.append("Modelo", $("#txtModelo option:selected").text());
    formdata.append("Separador", $("#txtSeparador option:selected").text());
    formdata.append("TipoEnsayo", $("#txtTipoEnsayo option:selected").text());
    formdata.append("LoteEnsamble", $("#txtLoteEnsamble").val());
    formdata.append("LoteCarga", $("#txtLoteCarga").val());
    formdata.append("CCA", $("#txtCCA").val());
    formdata.append("Peso", $("#txtPeso").val());
    formdata.append("Voltaje", $("#txtVoltaje").val());
    formdata.append("DensidadIngreso", $("#txtDensidadIngreso").val());
    //formdata.append("DensidadPreAcondicionamiento", $("#txtDensidadPreAcondiciamiento").val());
    formdata.append("TemperaturaIngreso", $("#txtTemperaturaIngreso").val());
    formdata.append("TemperaturaPrueba", $("#txtTemperaturaPrueba").val());
    formdata.append("DatoTeoricoPrueba", $("#txtDatoTeoricoPrueba").val());
    //formdata.append("ValorObjetivo", $("#txtValorObjetivo").val());
    formdata.append("ResultadoFinal", $("#txtResultadoFinal").val());
    formdata.append("Observaciones", $("#txtObservaciones").val());
    formdata.append("Calificacion", $("#txtCalificacion").val());
    formdata.append("CodigoBateria", $("#txtCodigoBateria").val());

    var files = $("#txtAnexos").get(0).files;

    for (var i = 0; i < files.length; i++) {
        formdata.append("archivos", files[i]);
    }
    $.ajax({
        type: 'POST',
        url: "../Calidad/RegistrarPruebasLaboratorio",
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
    $("#txtTipoNorma option[value=''").attr("selected", true);
    $("#txtMarca option[value=''").attr("selected", true);
    $("#txtNormativa option[value=''").attr("selected", true);
    $("#txtPreAcondicionamiento").val("");
    $("#txtTipoBateria option[value=''").attr("selected", true);
    $("#txtModelo option[value=''").attr("selected", true);
    $("#txtSeparador option[value=''").attr("selected", true);
    $("#txtTipoEnsayo option[value=''").attr("selected", true);
    $("#txtLoteEnsamble").val("");
    $("#txtLoteCarga").val("");
    $("#txtCCA").val("");
    $("#txtPeso").val("");
    $("#txtVoltaje").val("");
    $("#txtDensidadIngreso").val("");
    //$("#txtDensidadPreAcondiciamiento").val("");
    $("#txtTemperaturaIngreso").val("");
    $("#txtTemperaturaPrueba").val("");
    $("#txtDatoTeoricoPrueba").val("");
    //$("#txtValorObjetivo").val("");
    $("#txtResultadoFinal").val("");
    $("#txtObservaciones").val("");
    $("#txtCalificacion").val("");
    $("#txtCodigoBateria").val("");

    $("#txtAnexos").val("");
}


$('#btnRegistrarPrueba').on("click", function (e) {

    var val1= $("#txtFechaIngreso").val();
    var val2 = $("#txtCodigoIngreso").val();
    var val3 = $("#txtMarca option:selected").val();
    var val4 = $("#txtTipoNorma option:selected").val();
    var val5 = $("#txtNormativa option:selected").val();
    var val6 = $("#txtPreAcondicionamiento").val();
    var val7 = $("#txtTipoBateria option:selected").val();
    var val8 = $("#txtModelo option:selected").val();
    var val9 = $("#txtSeparador option:selected").val();
    var val10 = $("#txtTipoEnsayo option:selected").val();
    var val11 = $("#txtLoteEnsamble").val();
    var val12 = $("#txtLoteCarga").val();
    var val13 = $("#txtCCA").val();
    var val14 = $("#txtPeso").val();
    var val15 = $("#txtVoltaje").val();
    var val16 = $("#txtDensidadIngreso").val();
    var val17 = $("#txtDensidadPreAcondiciamiento").val();
    var val18 = $("#txtTemperaturaIngreso").val();
    var val19 = $("#txtTemperaturaPrueba").val();
    var val20 = $("#txtDatoTeoricoPrueba").val();
    var val21 = $("#txtValorObjetivo").val();
    var val22 = $("#txtResultadoFinal").val();
    var val23 = $("#txtObservaciones").val();
    var val24 = $("#txtCalificacion").val();
    var val25 = $("#txtCodigoBateria").val();

    //var Image = $("#txtAnexos").files;

    if (val1.length == 0) {
        $("#MensajeCompleteCampos").show('fade');
        setTimeout(function () {
            $("#MensajeCompleteCampos").fadeOut(1500);
        }, 3000);
        return;
    }
    if (val25.length == 0) {
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
    } if (val6.length == 0) {
        $("#MensajeCompleteCampos").show('fade');
        setTimeout(function () {
            $("#MensajeCompleteCampos").fadeOut(1500);
        }, 3000);
        return;
    }
    //if (val7.length == 0) {
    //    $("#MensajeCompleteCampos").show('fade');
    //    setTimeout(function () {
    //        $("#MensajeCompleteCampos").fadeOut(1500);
    //    }, 3000);
    //    return;
    //}
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
    } if (val10.length == 0) {
        $("#MensajeCompleteCampos").show('fade');
        setTimeout(function () {
            $("#MensajeCompleteCampos").fadeOut(1500);
        }, 3000);
        return;
    } if (val11.length == 0) {
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
    if (val13.length == 0) {
        $("#MensajeCompleteCampos").show('fade');
        setTimeout(function () {
            $("#MensajeCompleteCampos").fadeOut(1500);
        }, 3000);
        return;
    } if (val14.length == 0) {
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
    } if (val16.length == 0) {
        $("#MensajeCompleteCampos").show('fade');
        setTimeout(function () {
            $("#MensajeCompleteCampos").fadeOut(1500);
        }, 3000);
        return;
    }
    //if (val17.length == 0) {
    //    $("#MensajeCompleteCampos").show('fade');
    //    setTimeout(function () {
    //        $("#MensajeCompleteCampos").fadeOut(1500);
    //    }, 3000);
    //    return;
    //}
    if (val18.length == 0) {
        $("#MensajeCompleteCampos").show('fade');
        setTimeout(function () {
            $("#MensajeCompleteCampos").fadeOut(1500);
        }, 3000);
        return;
    }
    if (val19.length == 0) {
        $("#MensajeCompleteCampos").show('fade');
        setTimeout(function () {
            $("#MensajeCompleteCampos").fadeOut(1500);
        }, 3000);
        return;
    } if (val20.length == 0) {
        $("#MensajeCompleteCampos").show('fade');
        setTimeout(function () {
            $("#MensajeCompleteCampos").fadeOut(1500);
        }, 3000);
        return;
    }
    //if (val21.length == 0) {
    //    $("#MensajeCompleteCampos").show('fade');
    //    setTimeout(function () {
    //        $("#MensajeCompleteCampos").fadeOut(1500);
    //    }, 3000);
    //    return;
    //}
    if (val22.length == 0) {
        $("#MensajeCompleteCampos").show('fade');
        setTimeout(function () {
            $("#MensajeCompleteCampos").fadeOut(1500);
        }, 3000);
        return;
    }
    //if (val23.length == 0) {
    //    $("#MensajeCompleteCampos").show('fade');
    //    setTimeout(function () {
    //        $("#MensajeCompleteCampos").fadeOut(1500);
    //    }, 3000);
    //    return;
    //}
    if (val24.length == 0) {
        $("#MensajeCompleteCampos").show('fade');
        setTimeout(function () {
            $("#MensajeCompleteCampos").fadeOut(1500);
        }, 3000);
        return;
    }
    if ($("#txtModelo option:selected").text() == "--Escoja--") {
        $("#MensajeCompleteCampos").show('fade');
        setTimeout(function () {
            $("#MensajeCompleteCampos").fadeOut(1500);
        }, 3000);
        return;
    }
    //if (document.getElementById("txtAnexos").files.length==0) {
    //    $("#MensajeCompleteCampos").show('fade');
    //    setTimeout(function () {
    //        $("#MensajeCompleteCampos").fadeOut(1500);
    //    }, 3000);
    //    return;
    //}

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
        url: '../Calidad/ConsultarCodigoRegistroPrueba',

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


function CalcularResultado() {
    var total = (parseFloat($('#txtResultadoFinal').val()) / parseFloat($('#txtDatoTeoricoPrueba').val()))*100;
    $('#txtCalificacion').val(total.toFixed(2));
}
