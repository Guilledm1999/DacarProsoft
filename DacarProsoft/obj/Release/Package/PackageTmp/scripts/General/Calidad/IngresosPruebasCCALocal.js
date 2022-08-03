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
    $("#ModalAnexos").modal("show");
}