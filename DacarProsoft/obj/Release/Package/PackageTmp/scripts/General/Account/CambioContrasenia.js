var contrasenaAntigua = null;
var contrasenaNueva = null;

$(document).ready(function () {
});


$('#LinkCloseUno').on("click", function (e) {
    $("#MsjCambioExitoso").hide('fade');
});
$('#LinkCloseDos').on("click", function (e) {
    $("#MsjCambioNoExitoso").hide('fade');
});
$('#LinkCloseTres').on("click", function (e) {
    $("#MsjCompleteTodosCamposForm").hide('fade');
});
$('#LinkCloseCuatro').on("click", function (e) {
    $("#MsjPassNoConcuerda").hide('fade');
});

function AbrirModalCambioPass() {
    $("#ModalCambioContraseniaUser").modal("show");
}

function ConfirmarCambioPassUser() {
     contrasenaAntigua = $("#txtPassAntiguaUser").val();
     contrasenaNueva = $("#txtPassNuevaUser").val();

    if (contrasenaAntigua.length == 0) {
        $("#MsjCompleteTodosCamposForm").show('fade');
        setTimeout(function () {
            $("#MsjCompleteTodosCamposForm").fadeOut(1500);
        }, 3000);
        return;
    }
    if (contrasenaNueva.length == 0) {
        $("#MsjCompleteTodosCamposForm").show('fade');
        setTimeout(function () {
            $("#MsjCompleteTodosCamposForm").fadeOut(1500);
        }, 3000);
        return;
    }
    validarContraseniaUsuario(contrasenaAntigua);
}

function validarContraseniaUsuario(val1) {
    $.ajax({
        url: "../Account/ConsultarPass",
        type: "POST",
        async: false,
        data: {
            contrasena: val1
        }, success: function (res) {
            if (res == "True") {
                cambiarClaveUsuario();
            } else {
                $("#MsjPassNoConcuerda").show('fade');
                setTimeout(function () {
                    $("#MsjPassNoConcuerda").fadeOut(1500);
                }, 3000);
            }
        },
        error: function (msg) {
            $("#MsjCambioNoExitoso").show('fade');
            setTimeout(function () {
                $("#MsjCambioNoExitoso").fadeOut(1500);
            }, 3000);
        }
    })
}

function cambiarClaveUsuario() {
    $.ajax({
        url: "../Account/CambiarPassUser",
        type: "POST",
        async: false,
        data: {
            contrasena: $("#txtPassNuevaUser").val()
        }, success: function (res) {
            if (res == "True") {

                location.href = "../Account/LogOut";

            }
        },
        error: function (msg) {
            $("#MsjCambioNoExitoso").show('fade');
            setTimeout(function () {
                $("#MsjCambioNoExitoso").fadeOut(1500);
            }, 3000);        }
    })
}