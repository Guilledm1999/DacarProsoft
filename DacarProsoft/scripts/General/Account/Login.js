$(document).ready(function () {
    console.log("Usted entro al javascript");
});

function InicioSesion() {
    $.ajax({
        url: "../Account/Verify",
        type: "POST",
        data: {
            NombreUsuario: $("#NombreUsuario").val(), Contrasena: $("#contrasena").val()
        }, success: function () {
            alert("Inicio de Sesion");
        },
        error: function (msg) {
            alert("Error Al iniciar Sesion");
        }
    })
}

   



    function onSubmit(token) {
        document.getElementById("Login").submit();
}


