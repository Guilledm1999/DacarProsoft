var referencia = null;
$(document).ready(function () {
    ConsultarClientesDeSap();
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


function ConsultarClientesDeSap(){
    $.ajax({
        url: "../CrearUsuario/ConsultarClientesSap",
        type: "GET"
        , success: function (msg) {
            //ItemsPedidoCliente = msg;  
            $("#tblClientesSap").dxDataGrid({
                dataSource: msg,
                keyExpr: 'CardCode',
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
                        dataField: "CardCode", caption: "Documento Cliente", alignment: "left"
                    },
                    {
                        dataField: "NombreCliente", caption: "NombreCliente"
                    }, {
                        caption: "Actions",
                        cellTemplate: function (container, options) {

                            var btnDetalle = "<button type='button' class='btn-primary' onclick='RegistrarUsuario(" + JSON.stringify(options.data) + ")'>Crear Usuario</button>";

                            $("<div>")
                                .append($(btnDetalle))
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

function RegistrarUsuario(modelo) {
    $("#txtNombreUsuario").val(modelo.NombreCliente);
    $("#txtUsuarioPortal").val(modelo.CardCode);

    referencia = modelo.CardCode;
    $("#ModalCrearUsuarioPortal").modal("show");
}


$('#RegistrarUsuarioPortal').on("click", function (e) {
    var valVali = null;
    if ($("#SelectValidaciones option:selected").val() == 0) {
        valVali = true;

    } else {
        valVali = false;

    }
    $.ajax({
        url: "../CrearUsuario/IngresarUsuarioClientesSap",
        type: "POST",
        data: {
            NombreCliente: $("#txtNombreUsuario").val(),
            Usuario: $("#txtUsuarioPortal").val(),
            Clave: $("#txtContrasenaPortal").val(),
            Referencia: referencia,
            validacion: valVali

        }, success: function (msg) {
            if (msg == "True") {
                $("#txtNombreUsuario").val("");
                $("#txtUsuarioPortal").val("");
                $("#txtContrasenaPortal").val("");
                ConsultarClientesDeSap();
                $("#ModalCrearUsuarioPortal").modal("hide");

                $("#IngresoCorrecto").show('fade');
                setTimeout(function () {
                    $("#IngresoCorrecto").fadeOut(1500);
                }, 3000);
            } else {
                $("#txtContrasenaPortal").val("");

                $("#ModalCrearUsuarioPortal").modal("hide");

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

});

