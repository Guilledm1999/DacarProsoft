var referencia = null;
var comprobar = false;
var temporalListPrecio = null;
var tempIdLis = null;
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
$('#LinkClose6').on("click", function (e) {
    $("#NoRegistroProducto").hide('fade');
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
   
    document.getElementById("RegistrarUsuarioPortal").disabled = true;
    
    $("#txtNombreUsuario").val(modelo.NombreCliente);
    $("#txtUsuarioPortal").val(modelo.CardCode);

    referencia = modelo.CardCode;
    $("#ModalCrearUsuarioPortal").modal("show");
}

function TempRegisListaPrecios() {
    if (comprobar == true) {
        document.getElementById("RegistrarUsuarioPortal").disabled = false;
        $("#ListaPrecioUsuarioPortal").modal("hide");
    }
    else {
        $("#ListaPrecioUsuarioPortal").modal("hide");
        $("#NoRegistroProducto").show('fade');
        setTimeout(function () {
            $("#NoRegistroProducto").fadeOut(1500);
        }, 3000); return;
    }

}
function SeleccionarPreciosBaterias() {
    comprobar = false;
    $.ajax({
        url: "../CrearUsuario/ConsultarListaPreciosGenerica",
        type: "GET"
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
                        dataField: "ListaPrecioClienteId", visible:false
                    },
                    {
                        dataField: "CustomerReference", caption: "Referencia Cliente", allowEditing: false
                    },
                    {
                        dataField: "DacarPartNumber", caption: "Numero Parte Dacar", allowEditing: false
                    },
                    {
                        dataField: "ModeloGenerico", caption: "Modelo Génerico", allowEditing: false
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
                        dataField: "QuantityXLayer", visible: false, allowEditing: false
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
                onRowUpdating: function (e) {
                    comprobar = true;
                    temporalListPrecio = e.newData;
                    tempIdLis = e.key;
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

$('#RegistrarUsuarioPortal').on("click", function (e) {
    var valVali = null;
    const datosTabla = $("#tblListaPrecioUsuariosPortal").dxDataGrid("getDataSource");
    console.log(datosTabla);
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
            validacion: valVali,
            listaProductos: datosTabla._store._array

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

