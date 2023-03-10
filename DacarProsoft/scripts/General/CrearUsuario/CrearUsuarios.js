var usu;
var IdUsu = null;
var NvlDificultadContrasena = null;
$(document).ready(function(){
    ConsultarUsuarios();
});

function ConsultarUsuarios() {
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

    $.ajax({
        url: "../CrearUsuario/ConsultarUsuarios",
        type: "GET"
       , success: function (msg) {
           ConfigDev.dataSource = msg;
           ConfigDev.columns = [
                { dataField: "IdUsuario", visible:false },
                { dataField: "NombreCompleto", caption: "Nombres" },
                { dataField: "NombreUsuario", caption: "Usuario" },
                { dataField: "DescripcionTipoUsuario", caption: "Tipo" },
                {
                    caption: "Acciones",
                    cellTemplate: function (container, options) {
                        var btn = "<button class='btn-primary' onclick='ModalEditarUsuario(" + JSON.stringify(options.data) + ")'>Editar</button>";
                        var lblEspacio = "<a> </a>"
                        var btn2 = "<button class='btn-primary' onclick='ModalElminarUsuario(" + JSON.stringify(options.data) + ")'>Eliminar</button>";

                        $("<div class="+"form-group"+">")
                        .append($(btn), $(lblEspacio), $(btn2))          
                        .appendTo(container);
                    

                    }
              
                }
           ];
           $("#tblTiposUsuarios").dxDataGrid(ConfigDev);
        },
        error: function (msg) {

            $("#MensajeErrorInesperado").show('fade');
            console.log(msg);
        }

    })
}

function ModalEditarUsuario(modelo) {
    console.log(modelo);
    IdUsu = modelo.IdUsuario;
    $("#txtNombresAct").val(modelo.NombreCompleto);
    $("#txtUsuarioAct").val(modelo.NombreUsuario);
    var select = $("#selectTipoUsuarioAct option:selected").val(modelo.DescripcionTipoUsuario);
    $("#ModalActualizarUsuario").modal("show");
}

function ActualizarUsuario() {
    if ($("#txtNombresAct").val() == "") {
        alert("Ingrese Nombres Completos.");
        return;
    }
    if ($("#txtUsuarioAct").val() == "") {
        alert("Ingrese Usuario.");
        return;
    }
    if ($("#txtContraseniaAct").val() == "") {
        alert("Ingrese Contrasenia.");
        return;
    }
    if ($("#selectTipoUsuarioAct").val() == "") {
        alert("Ingrese un tipo de usuario.");
        return;
    }
    $.ajax({
        url: "../CrearUsuario/ActualizarUsuarios",
        type: "POST",
        data: {
            IdUsuario: IdUsu,
            NombreCompleto: $("#txtNombresAct").val(),
            NombreUsuario: $("#txtUsuarioAct").val(),
            contrasena: $("#txtContraseniaAct").val(),
            TipoUsuario: $("#selectTipoUsuarioAct option:selected").val()

        }, success: function (msg) {
            $("#txtNombresAct").val("");
            $("#txtUsuarioAct").val("");
            $("#txtContraseniaAct").val("");
            ConsultarUsuarios();
            $("#ModalActualizarUsuario").modal("hide");

            $("#ActualizacionRealizada").show('fade');
            setTimeout(function () {
                $("#ActualizacionRealizada").fadeOut(1500);
            }, 3000);
        },
        error: function (msg) {
            $("#MensajeErrorInesperado").show('fade');
            console.log(msg);
        }
    })
}

$('#LinkClose').on("click", function (e) {
    $("#MensajeErrorInesperado").hide('fade');
});
$('#LinkClose2').on("click", function (e) {
    $("#ActualizacionRealizada").hide('fade');
});
$('#LinkClose3').on("click", function (e) {
    $("#EliminacionCorrecta").hide('fade');
});
$('#LinkClose4').on("click", function (e) {
    $("#IngresoCorrecto").hide('fade');
});

function ModalElminarUsuario(modelo) {
    console.log(modelo);
    usu = modelo;
    $("#txtUsuario").val(modelo.IdUsuario);
    $("#txtNombres").val(modelo.NombreCompleto);
    var select = $("#txtUsuario").text();
    var select2 = $("#txtNombres").text();

    $("#EliminarUsuario").modal("show");

}


function ElminarUsuario() {
    $.ajax({
        url: "../CrearUsuario/EliminarUsuarios",
        type: "POST",
        data: {
            UserId:usu.IdUsuario
        }, success: function (msg) {
            $("#txtNombres").val("");
            $("#txtUsuario").val("");
            $("#txtContrasenia").val("");
            $("#selectTipoUsuario").val("");
            ConsultarUsuarios();
            $("#EliminarUsuario").modal("hide");
            $("#EliminacionCorrecta").show('fade');
            setTimeout(function () {
                $("#EliminacionCorrecta").fadeOut(1500);
            }, 3000);
        },
        error: function (msg) {
            $("#MensajeErrorInesperado").show('fade');
            console.log(msg);
        }
    })
}



function ModalCrearUsuario() {
    $("#txtIdTipoUsuario").val(0);
    $("#ModalTipoUsuario").modal("show");
}


function CrearUsuario() {
    if ($("#txtNombres").val() == "") {
        alert("Ingrese Nombres Completos.");
        return;
    }
    if ($("#txtUsuario").val() == "") {
        alert("Ingrese Usuario.");
        return;
    }
    if ($("#txtContrasenia").val() == "") {
        alert("Ingrese Contrasenia.");
        return;
    }
    if ($("#selectTipoUsuario").val() == "") {
        alert("Ingrese un tipo de usuario.");
        return;
    }
    $.ajax({
        url:"../CrearUsuario/GuardarModificarUsuarios",
        type:"POST",
        data: {
            IdUsuario: $("#txtIdTipoUsuario").val(),
            NombreCompleto:$("#txtNombres").val(),
            NombreUsuario: $("#txtUsuario").val(),
            contrasena: $("#txtContrasenia").val(),
            TipoUsuario: $("#selectTipoUsuario").val()

        }, success: function (msg) {
            $("#txtNombres").val("");
            $("#txtUsuario").val("");
            $("#txtContrasenia").val("");
            $("#selectTipoUsuario").val("");
            ConsultarUsuarios();
            $("#ModalTipoUsuario").modal("hide");
          
            $("#IngresoCorrecto").show('fade');
            setTimeout(function () {
                $("#IngresoCorrecto").fadeOut(1500);
            }, 3000);
        },

        error: function (msg) {      
            $("#MensajeErrorInesperado").show('fade');
            console.log(msg);
        }
    })
}


function checkPasswordStrength() {
    var number = /([0-9])/;
    var alphabets = /([a-zA-Z])/;
    var special_characters = /([~,!,@,#,$,%,^,&,*,-,_,+,=,?,>,<])/;
    var password = $('#txtContrasenia').val().trim();
    if (password.length < 6) {
        $('#passwordbar').removeClass();
        $('#passwordbar').addClass('progress-bar bg-danger');
        document.getElementById("passwordbar").style.width = "25%";
        $('#password-strength-status').removeClass();
        $('#password-strength-status').addClass('weak-password');
        $('#password-strength-status').removeClass();
        $('#password-strength-status').addClass('weak-password');
        $('#password-strength-status').html("Débil (Debe tener al menos 6 caracteres.)");
        if (NvlDificultadContrasena == 1) {
            document.getElementById("btnRegistrar").disabled = false;
        } else {
            document.getElementById("btnRegistrar").disabled = true;
        }
    } else {
        if (password.match(number) && password.match(alphabets) && password.match(special_characters)) {
            $('#passwordbar').removeClass();
            $('#passwordbar').addClass('progress-bar bg-success');
            document.getElementById("passwordbar").style.width = "100%";
            $('#password-strength-status').removeClass();
            $('#password-strength-status').addClass('strong-password');
            $('#password-strength-status').html("Fuerte");
            document.getElementById("btnRegistrar").disabled = false;
        }
        else {
            $('#passwordbar').removeClass();
            $('#passwordbar').addClass('progress-bar bg-warning');
            document.getElementById("passwordbar").style.width = "50%";
            $('#password-strength-status').removeClass();
            $('#password-strength-status').addClass('medium-password');
            $('#password-strength-status').html("Medio (debe incluir letras, numeros y caracteres especiales.)");
            if (NvlDificultadContrasena <= 2) {
                document.getElementById("btnRegistrar").disabled = false;
            } else {
                document.getElementById("btnRegistrar").disabled = true;
            }
        }
    }

    if (password.length == 0) {
        $('#passwordbar').removeClass();
        ('#passwordbar').addClass('progress-bar bg-warning');
        document.getElementById("passwordbar").style.width = "0%";
    }
}
