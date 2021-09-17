var modeloAcceso = null;
var modeloActAcceso = null;

$(document).ready(function () {
    ConsultaDeAccesos();
});


function ConsultaDeAccesos() {
    $.ajax({
        url: "../Accesos/ConsultaAccesos",
        type: "GET"
    , success: function (msg) {
        ConfigDev.dataSource = msg;
        ConfigDev.columnAutoWidth = true,
        ConfigDev.showBorders = true,
           ConfigDev.filterRow = { visible: false },
            ConfigDev.filterPanel = { visible: false },
            ConfigDev.headerFilter = { visible: false },
          ConfigDev.grouping = {
              autoExpandAll: false,
          },
           ConfigDev.groupPanel = {
               visible: false,

           },

        ConfigDev.columns = [
             { dataField: "idAcceso", visible: false },
              { dataField: "TipoUsuario", caption: "Usuario", groupIndex: 0 },
              { dataField: "MenuDescr", caption: "Descripcion Menu" },
              { dataField: "EstadoMenu", caption: "Estado Menu" },
                { dataField: "DocEntry", visible: false },
                  {
                      caption: "Acciones",
                      cellTemplate: function (container, options) {
                          var lblDetalle = "<button class='btn-primary' onclick='permiso(" + JSON.stringify(options.data) + ")'>Eliminar</button>";
                          var lblEspacio = "<a> </a>"
                          var lblDetalle2 = "<button class='btn-primary' onclick='Modificar(" + JSON.stringify(options.data) + ")'>Modificar</button>";
                          $("<div class=" + "form-group" + ">")
                       .append($(lblDetalle), $(lblEspacio), $(lblDetalle2))
                       .appendTo(container);

                      }
                  }
        ];
        $("#TablaAccesos").dxDataGrid(ConfigDev);
    }, error: function (msg) {
        $("#MensajeErrorInesperado").show('fade');
        setTimeout(function () {
            $("#MensajeErrorInesperado").fadeOut(1500);
        }, 3000);
    }
    })
}

$('#LinkClose').on("click", function (e) {
    $("#MensajeErrorInesperado").hide('fade');
});
$('#LinkClose2').on("click", function (e) {
    $("#MensajeEliminacion").hide('fade');
});
$('#LinkClose3').on("click", function (e) {
    $("#MensajeErrorEliminacion").hide('fade');
});
$('#LinkClose4').on("click", function (e) {
    $("#MensajeIngresoExitoso").hide('fade');
});
$('#LinkClose5').on("click", function (e) {
    $("#MensajeIngresoNoExitoso").hide('fade');
});
$('#LinkClose6').on("click", function (e) {
    $("#MensajeActualizacionExitoso").hide('fade');
});
$('#LinkClose7').on("click", function (e) {
    $("#MensajeActualizacionNoExitoso").hide('fade');
});

function permiso(modelo) {
    $("#ModalConfirmacion").modal("show");
    modeloAcceso = modelo;
}


function Modificar(modelo) {
    $("#txtTipoUsuario").val(modelo.TipoUsuario);
    $("#txtModulo").val(modelo.MenuDescr);
    $("#ModalActualizarAcceso").modal("show");
    modeloActAcceso = modelo;
}

$('#AfirmacionEliminado').on("click", function (e) {
    ModalEliminar();
});

$('#RegistrarAcceso').on("click", function (e) {
    AgregarAcceso();
});

$('#ActualizarAcceso').on("click", function (e) {
    ActualizarAcceso();
});

function ModalEliminar() {
    $.ajax({
        url: "../Accesos/EliminarAcceso",
        type: "POST",
        data: {
            idAcceso: modeloAcceso.idAcceso
        }, success: function (msg) {
            console.log("el valor del bool es"+msg);

            if (msg == "True") {

                ConsultaDeAccesos();
                $("#ModalConfirmacion").modal("hide");
                $("#MensajeEliminacion").show('fade');
                setTimeout(function () {
                    $("#MensajeEliminacion").fadeOut(1500);
                }, 3000);
            } else {
                $("#ModalConfirmacion").modal("hide");
                $("#MensajeErrorEliminacion").show('fade');
                setTimeout(function () {
                    $("#MensajeErrorEliminacion").fadeOut(1500);
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

function AgregarAcceso() {
    $.ajax({
        url: "../Accesos/AgregarAcceso",
        type: "POST",
        data: {
            tipoUsuario: $("#TipoUsuario option:selected").val(), tipoMenu: $("#TipoMenu option:selected").val(), estado: $("#selectEstado option:selected").val()

        }, success: function (msg) {
            console.log("el valor del bool es" + msg);

            if (msg == "True") {
                ConsultaDeAccesos();
                $("#ModalCrearAcceso").modal("hide");
                $("#MensajeIngresoExitoso").show('fade');
                setTimeout(function () {
                    $("#MensajeIngresoExitoso").fadeOut(1500);
                }, 3000);
            } else {
                ConsultaDeAccesos();
                $("#ModalCrearAcceso").modal("hide");
                $("#MensajeIngresoNoExitoso").show('fade');
                setTimeout(function () {
                    $("#MensajeIngresoNoExitoso").fadeOut(1500);
                }, 3000);
            }
        },
        error: function (msg) {
            $("#ModalCrearAcceso").modal("hide");
            $("#MensajeErrorInesperado").show('fade');
            setTimeout(function () {
                $("#MensajeErrorInesperado").fadeOut(1500);
            }, 3000);
        }
    })
}
function ActualizarAcceso() {
    $.ajax({
        url: "../Accesos/ActualizarAcceso",
        type: "POST",
        data: {
            idAcceso: modeloActAcceso.idAcceso, estado: $("#selectEstadoAct option:selected").val()

        }, success: function (msg) {
            console.log("el valor del bool es" + msg);

            if (msg == "True") {
                ConsultaDeAccesos();
                $("#ModalActualizarAcceso").modal("hide");
                $("#MensajeActualizacionExitoso").show('fade');
                setTimeout(function () {
                    $("#MensajeActualizacionExitoso").fadeOut(1500);
                }, 3000);
            } else {
                ConsultaDeAccesos();
                $("#ModalActualizarAcceso").modal("hide");
                $("#MensajeActualizacionNoExitoso").show('fade');
                setTimeout(function () {
                    $("#MensajeActualizacionNoExitoso").fadeOut(1500);
                }, 3000);
            }
        },
        error: function (msg) {
            $("#ModalActualizarAcceso").modal("hide");
            $("#MensajeErrorInesperado").show('fade');
            setTimeout(function () {
                $("#MensajeErrorInesperado").fadeOut(1500);
            }, 3000);
        }
    })
}

$('#BtnNuevoAcceso').on("click", function (e) {
    $("#ModalCrearAcceso").modal("show");
});