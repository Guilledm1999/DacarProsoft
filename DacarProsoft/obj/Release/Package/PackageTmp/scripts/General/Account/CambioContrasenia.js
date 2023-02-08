var contrasenaAntigua = null;
var contrasenaNueva = null;
let timeout;

var NvlDificultadContrasena = null;

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
    $.ajax({
        url: "../Account/ConsultarNivelDificultadContrasena",
        type: "POST",
        async: false,
        success: function (res) {
            NvlDificultadContrasena = res;
        },
        error: function (msg) {
            NvlDificultadContrasena = 3;
        }
    })

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

/*
function checkPasswordStrengthAct() {
    var number = /([0-9])/;
    var alphabets = /([a-zA-Z])/;
    var special_characters = /([~,!,@,#,$,%,^,&,*,-,_,+,=,?,>,<])/;
    var password = $('#txtContraseniaAct').val().trim();
    if (password.length < 6) {
        $('#passwordbarAct').removeClass();
        $('#passwordbarAct').addClass('progress-bar bg-danger');
        document.getElementById("passwordbarAct").style.width = "25%";
        $('#password-strength-statusAct').removeClass();
        $('#password-strength-statusAct').addClass('weak-password');
        $('#password-strength-statusAct').removeClass();
        $('#password-strength-statusAct').addClass('weak-password');
        $('#password-strength-statusAct').html("Débil (Debe tener al menos 6 caracteres.)");
        document.getElementById("btnConfirmarCambio").disabled = true;
    } else {
        if (password.match(number) && password.match(alphabets) && password.match(special_characters)) {
            $('#passwordbarAct').removeClass();
            $('#passwordbarAct').addClass('progress-bar bg-success');
            document.getElementById("passwordbarAct").style.width = "100%";
            $('#password-strength-statusAct').removeClass();
            $('#password-strength-statusAct').addClass('strong-password');
            $('#password-strength-statusAct').html("Fuerte");
            document.getElementById("btnConfirmarCambio").disabled = false;
            $('#btnConfirmarCambio').removeAttr('disabled');
        }
        else {
            $('#passwordbarAct').removeClass();
            $('#passwordbarAct').addClass('progress-bar bg-warning');
            document.getElementById("passwordbarAct").style.width = "50%";
            $('#password-strength-statusAct').removeClass();
            $('#password-strength-statusAct').addClass('medium-password');
            $('#password-strength-statusAct').html("Medio (debe incluir letras, numeros y caracteres especiales.)");
            document.getElementById("btnConfirmarCambio").disabled = true;
        }
    }

    if (password.length == 0) {
        $('#passwordbarAct').removeClass();
        ('#passwordbarAct').addClass('progress-bar bg-warning');
        document.getElementById("passwordbarAct").style.width = "0%";
    }
}
*/

function checkPasswordStrength() {
    var number = /([0-9])/;
    var alphabets = /([a-zA-Z])/;
    var special_characters = /([~,!,@,#,$,%,^,&,*,-,_,+,=,?,>,<])/;
    var password = $('#txtPassNuevaUser').val().trim();
    if (password.length < 6) {
        $('#passwordbar').removeClass();
        $('#passwordbar').addClass('progress-bar bg-danger');
        document.getElementById("passwordbar").style.width = "25%";
        $('#password-strength-status').removeClass();
        $('#password-strength-status').addClass('weak-password');
        $('#password-strength-status').removeClass();
        $('#password-strength-status').addClass('weak-password');
        $('#password-strength-status').html("Débil (Debe tener al menos 6 caracteres.)");
       
        if (NvlDificultadContrasena==1) {
            document.getElementById("btnConfirmarCambio").disabled = false;
        } else {
            document.getElementById("btnConfirmarCambio").disabled = true;
        }
    } else {
        if (password.match(number) && password.match(alphabets) && password.match(special_characters)) {
            $('#passwordbar').removeClass();
            $('#passwordbar').addClass('progress-bar bg-success');
            document.getElementById("passwordbar").style.width = "100%";
            $('#password-strength-status').removeClass();
            $('#password-strength-status').addClass('strong-password');
            $('#password-strength-status').html("Fuerte");
            document.getElementById("btnConfirmarCambio").disabled = false;
           
        }
        else {
            $('#passwordbar').removeClass();
            $('#passwordbar').addClass('progress-bar bg-warning');
            document.getElementById("passwordbar").style.width = "50%";
            $('#password-strength-status').removeClass();
            $('#password-strength-status').addClass('medium-password');
            $('#password-strength-status').html("Medio (debe incluir letras, numeros y caracteres especiales.)");
            if (NvlDificultadContrasena <= 2) {
                document.getElementById("btnConfirmarCambio").disabled = false;
            } else {
                document.getElementById("btnConfirmarCambio").disabled = true;
            }
        }
    }

    if (password.length ==0) {
        $('#passwordbar').removeClass();
        ('#passwordbar').addClass('progress-bar bg-warning');
        document.getElementById("passwordbar").style.width = "0%";
    }
}
function myFunction() {
    


    grecaptcha.ready(function () {
        grecaptcha.execute('reCAPTCHA_site_key', { action: 'submit' }).then(function (token) {
            // Add your logic to submit to your backend server here.
        });
    });
}