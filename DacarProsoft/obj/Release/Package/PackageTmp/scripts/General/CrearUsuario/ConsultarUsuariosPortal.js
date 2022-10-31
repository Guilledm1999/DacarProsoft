var referencia = null;
var usu = null;

$(document).ready(function () {
    ConsultarUsuariosPortal();
});

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
$('#LinkClose5').on("click", function (e) {
    $("#UsuarioYaRegistrado").hide('fade');
});
$('#LinkClose6').on("click", function (e) {
    $("#IngreseTodosCampos").hide('fade');
});



function ConsultarUsuariosPortal() {
    $.ajax({
        url: "../CrearUsuario/ConsultarUsuariosRegistradosPortal",
        type: "GET"
        , success: function (msg) {
            //ItemsPedidoCliente = msg;  
            $("#tblUsuariosPortal").dxDataGrid({
                dataSource: msg,
                keyExpr: 'UsuarioPortalId',
                showBorders: true,
                columnAutoWidth: true,
                showBorders: true,
                paging: {
                    pageSize: 10
                },
                pager: {
                    visible: true,
                    allowedPageSizes: [5, 10, 100],
                    showPageSizeSelector: true,
                    showInfo: true,
                    showNavigationButtons: true
                },
                searchPanel: {
                    visible: true,
                    width: 240,
                    placeholder: "Search...",
                    alignment: "left"
                },
                columns: [

                    {
                        dataField: "UsuarioPortalId", visible: false
                    },
                    {
                        dataField: "NombreCliente", caption: "Nombre Cliente"
                    }, {
                        dataField: "UsuarioPortal", caption: "Usuario Portal"
                    }, {
                        dataField: "ReferenciaUsuario", caption: "Identificacion"
                    },
                    {
                        dataField: "Validaciones", caption: "Validaciones"
                    },{
                        caption: "Actions",
                        cellTemplate: function (container, options) {
                            var btn = "<button class='btn-primary' onclick='ModalEditarUsuario(" + JSON.stringify(options.data) + ")'>Editar</button>";
                            var lblEspacio = "<a> </a>"
                            var btn2 = "<button type='button' class='btn-primary' onclick='ModalElminarUsuario(" + JSON.stringify(options.data) + ")'>Eliminar</button>";
                            var btn3 = "<button type='button' class='btn-success' onclick='ModalVizualizarListaPrecio(" + JSON.stringify(options.data) + ")'>Lista de Precio</button>";

                            $("<div class=" + "form-group" + ">")
                                .append($(btn3), $(lblEspacio),$(btn), $(lblEspacio), $(btn2))
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
}

function ModalElminarUsuario(modelo) {
    usu = modelo;
    $("#EliminarUsuarioPortal").modal("show");

}

function ModalEditarUsuario(modelo) {
    usu = modelo;
    $("#txtContrasenia").val("");
    $("#txtUsuario").val("");


    $("#txtNombres").val(modelo.NombreCliente);
    $("#txtUsuario").val(modelo.UsuarioPortal);
    //var select = $("#txtUsuario").text();
    //var select2 = $("#txtNombres").text();

    $("#ModalActualizarUsuario").modal("show");

}

function ElminarUsuarioPortal() {
    $.ajax({
        url: "../CrearUsuario/EliminarUsuariosPortal",
        type: "POST",
        data: {
            UserId: usu.UsuarioPortalId
        }, success: function (msg) {
            ConsultarUsuariosPortal();
            $("#EliminarUsuarioPortal").modal("hide");
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


function ActualizarUsuarioPortal() {
    var txtUsuario = $("#txtUsuario").val();
    var txtContrasenia = $("#txtContrasenia").val();
    console.log("Esto tiene de longitud usuario" + txtUsuario.length);
    console.log("Esto tiene de longitud contrasena" + txtContrasenia.length);

    if (txtUsuario.length == 0 ) {
        $("#IngreseTodosCampos").show('fade');
        setTimeout(function () {
            $("#IngreseTodosCampos").fadeOut(1500);
        }, 3000); return;
    }
    else if ( txtContrasenia.length == 0) {
        $("#IngreseTodosCampos").show('fade');
        setTimeout(function () {
            $("#IngreseTodosCampos").fadeOut(1500);
        }, 3000); return;
    } 
    else {
        var valVali = null;
        if ($("#SelectValidaciones option:selected").val() == 0) {
            valVali = true;

        } else {
            valVali = false;

        }

        $.ajax({
            url: "../CrearUsuario/ActualizarUsuariosPortal",
            type: "POST",
            data: {
                IdUsuarioPortal: usu.UsuarioPortalId, Usuario: $("#txtUsuario").val(), Clave: $("#txtContrasenia").val(), Tipo: $("#SelectEstadoUsuario option:selected").val(), Validacion: valVali
            }, success: function (msg) {

                if (msg == "True") {

                    ConsultarUsuariosPortal();
                    $("#ModalActualizarUsuario").modal("hide");
                    $("#ActualizacionRealizada").show('fade');
                    setTimeout(function () {
                        $("#ActualizacionRealizada").fadeOut(1500);
                    }, 3000);
                }

                else {
                    console.log("ingreso x falso");

                    $("#ModalActualizarUsuario").modal("hide");
                    $("#UsuarioYaRegistrado").show('fade');
                    setTimeout(function () {
                        $("#UsuarioYaRegistrado").fadeOut(1500);
                    }, 3000);
                }
               
            },
            error: function (msg) {
                $("#MensajeErrorInesperado").show('fade');
            }
        })
    }
}
function ModalVizualizarListaPrecio(modelo) {
    comprobar = false;
    $.ajax({
        url: "../CrearUsuario/ConsultarListaPreciosCliente",
        type: "POST",
        data: {
            CardCode: modelo.ReferenciaUsuario,
        }
        , success: function (msg) {
            //ItemsPedidoCliente = msg;  
            $("#tblListaPrecioUsuariosPortal").dxDataGrid({
                dataSource: msg,
                keyExpr: 'ListaPrecioClienteId',
                showBorders: true,
                columnAutoWidth: true,
                showBorders: true,
                paging: {
                    pageSize: 10
                },
                pager: {
                    visible: true,
                    allowedPageSizes: [5, 10, 100],
                    showPageSizeSelector: true,
                    showInfo: true,
                    showNavigationButtons: true
                },
                searchPanel: {
                    visible: true,
                    width: 240,
                    placeholder: "Search...",
                    alignment: "left"
                },
                editing: {
                    mode: 'cell',
                    allowUpdating: true,
                },
                columns: [
                    {
                        dataField: "ListaPrecioClienteId", visible: false
                    },
                    {
                        dataField: "CustomerReference", caption: "Referencia Cliente", allowEditing: true
                    },
                    {
                        dataField: "DacarPartNumber", caption: "Numero Parte Dacar", allowEditing: true
                    },
                    {
                        dataField: "ModeloGenerico", caption: "Modelo Génerico", allowEditing: true
                    },
                    {
                        dataField: "DimensionsHeight", visible: false, allowEditing: false
                    },
                    {
                        dataField: "DimensionsLenght", visible: false, allowEditing: false
                    },
                    {
                        dataField: "DimensionWidth", visible: false, allowEditing: false
                    },
                    {
                        dataField: "AssemblyBci", visible: false, allowEditing: false
                    },
                    {
                        dataField: "SpecificationsNominalCapacity", visible: false, allowEditing: false
                    },
                    {
                        dataField: "ReserveCap", visible: false, allowEditing: false
                    },
                    {
                        dataField: "CCAMenos18", visible: false, allowEditing: false
                    },
                    {
                        dataField: "CA0", visible: false, allowEditing: false
                    },
                    {
                        dataField: "WeightKg", visible: false, allowEditing: false
                    },
                    {
                        dataField: "QuantityXLayer",caption: "Cantidad por Piso", allowEditing: true
                    },
                    {
                        dataField: "Categoria", visible: false, allowEditing: false
                    },
                    {
                        dataField: "PrecioProducto", caption: "Precio Venta", allowEditing: true
                    },
                    {
                        dataField: "PrecioEnvio", caption: "Precio Envio", allowEditing: true
                    },
                ],
                onRowUpdating: function (options) {
                        this.oldData = Object.assign({}, options.oldData);
                        ActualizarGenerico(options.newData, options.key);
                },         
            });
        },
        error: function (msg) {
            $("#MensajeErrorInesperado").show('fade');
            setTimeout(function () {
                $("#MensajeErrorInesperado").fadeOut(1500);
            }, 3000); return;
        }
    })
    $("#ListaPrecioUsuarioPortal").modal("show");
}

function ActualizarGenerico(valor, key) {
    $.ajax({
        url: '../CrearUsuario/ActualizarValores',
        type: 'POST',
        dataType: 'json',
        data: {
            generico: valor, Key: key
        },
        success: function (respuesta) {
            console.log("respuesta:" + respuesta);
            if (respuesta == "True") {
                ConsultaRegistrosPruebasLaboratorio();
            }
        }
    });
}