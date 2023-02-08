var referencia = null;
var usu = null;
var modelo = null;
var lookupDataSource = null;
var lookupDataSourceModelo = null;
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
        url: "../CrearUsuario/ConsultarModelo",
        type: "GET",
        async: false,
        success: function (msg) {
            lookupDataSourceModelo = msg
        }
    })
    $.ajax({
        url: "../CrearUsuario/ConsultarListaPreciosGenericaCompleta",
        type: "GET",
        async: false,
        success: function (msg) {
            ListaPrecioGenerica = msg
        }
    })
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
   
    IngresarMarca(modelo.ReferenciaUsuario);
   referencia= modelo.ReferenciaUsuario
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
                wordWrapEnabled: true,
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
                    mode: 'batch',
                    allowUpdating: true,
                    allowAdding: true,
                    allowDeleting: true,
                    useIcons: true,
                },
                repaintChangesOnly: true,
                columns: [
                    {
                        dataField: "ListaPrecioClienteId", visible: false
                    },
                    {
                        dataField: "NombreListaId", visible: false
                    },
                    {
                        dataField: "CustomerReference", caption: "Referencia Cliente", allowEditing: true, alignment: 'left'
                      
                    },
                    {
                        dataField: "DacarPartNumber", caption: "Numero Parte Dacar", allowEditing: true, alignment: 'left',
                        lookup: {

                            dataSource: lookupDataSourceModelo,
                            valueExpr: "ItemCode",
                            displayExpr: "Descripcion",
                        },
                        setCellValue: function (newData, value, currentRowData) {

                            var items = ListaPrecioGenerica.filter(function (item) {
                                return item.DacarPartNumber == value;
                            });
                            newData.DacarPartNumber = value;
                            newData.Marca = "";
                            newData.ModeloGenerico = items[0].ModeloGenerico.toString();
                            newData.CustomerReference = items[0].CustomerReference.toString();
                            newData.QuantityXLayer = items[0].QuantityXLayer;
                            newData.CantidadPorPallet = items[0].CantidadPorPallet;
                            newData.Pisos = items[0].Pisos;
                            newData.PrecioProducto = items[0].PrecioProducto;
                            newData.PrecioEnvio = items[0].PrecioEnvio;
                            newData.DimensionsHeight = items[0].DimensionsHeight;
                            newData.DimensionsLenght = items[0].DimensionsLenght;
                            newData.SpecificationsNominalCapacity = items[0].SpecificationsNominalCapacity;
                            newData.ReserveCap = items[0].ReserveCap;
                            newData.CCAMenos18 = items[0].CCAMenos18;
                            newData.CA0 = items[0].CA0;
                            newData.WeightKg = items[0].WeightKg;
                            newData.Categoria = items[0].Categoria;
                            newData.AssemblyBci = items[0].AssemblyBci;
                            newData.DimensionWidth = items[0].DimensionWidth;

                        },

                    },
                    {
                        dataField: "ModeloGenerico", caption: "Modelo Génerico", allowEditing: true, alignment: 'left'
                        
                    },
                    {
                        dataField: "DimensionsHeight", visible: false, allowEditing: false, alignment: 'left'
                    },
                    {
                        dataField: "DimensionsLenght", visible: false, allowEditing: false, alignment: 'left'
                    },
                    {
                        dataField: "DimensionWidth", visible: false, allowEditing: false, alignment: 'left'
                    },
                    {
                        dataField: "AssemblyBci", visible: false, allowEditing: false, alignment: 'left'
                    },
                    {
                        dataField: "SpecificationsNominalCapacity", visible: false, allowEditing: false, alignment: 'left'
                    },
                    {
                        dataField: "ReserveCap", visible: false, allowEditing: false, alignment: 'left'
                    },
                    {
                        dataField: "CCAMenos18", visible: false, allowEditing: false, alignment: 'left'
                    },
                    {
                        dataField: "CA0", visible: false, allowEditing: false, alignment: 'left'
                    },
                    {
                        dataField: "WeightKg", visible: false, allowEditing: false, alignment: 'left'
                    },
                    {
                        dataField: "Pisos", caption: "Pisos", allowEditing: true, width: 50, alignment: 'left'
                    },
                    {
                        dataField: "CantidadPorPallet", caption: "Cantidad por Pallet", allowEditing: true, alignment: 'left'
                    },
                    {
                        dataField: "QuantityXLayer", caption: "Cantidad por Piso", allowEditing: true, alignment: 'left'
                    },
                    {
                        dataField: "Categoria", visible: false, allowEditing: false, alignment: 'left',
                    },
                    {
                        dataField: "PrecioProducto", caption: "Precio Venta", allowEditing: true, format: 'currency', dataType: 'number', alignment: 'left',
                    },
                    {
                        dataField: "PrecioEnvio", caption: "Precio Envio", allowEditing: true, format: 'currency', dataType: 'number', alignment: 'left',
                    },
                    {
                        dataField: "Marca", caption: "Marca", allowEditing: true,
                        lookup: {
                            dataSource: lookupDataSource,
                            valueExpr: "ItemCode",
                            displayExpr: "Descripcion",
                        }, alignment: 'left',
                    },
                    {
                        type: 'buttons',
                        buttons: ['delete', 'save'], width : 40
                    }
                ],
                onToolbarPreparing(e) {
                     e.toolbarOptions.items[1].visible = false;  
                },

                onRowUpdating: function (options) {
                      //  this.oldData = Object.assign({}, options.oldData);
                       // ActualizarGenerico(options.newData, options.key);
                },/*
                onCellClick: function (e) {
                    if (e.rowType == "data") {

                        //Cambiar datasource del lookup dinamicamente al abrir
                        modelo1 = e.row.data.ModeloGenerico;
                        Extranjero = e.row.data.CustomerReference;
                        IngresarMarcaModelo(modelo1);
                        if (e.editorName == 'dxSelectBox') {
                            e.editorOptions.onOpened = function (arg) {
                                arg.component.option("dataSource", lookupDataSource);
                            }
                        }

                    }
                },*/

                onEditorPreparing: function (e) {
                    if (e.parentType == "dataRow") {

                      //  IngresarMarcaModelo();

                        //Cambiar datasource del lookup dinamicamente al abrir
                        modelo = e.row.data.ModeloGenerico;
                        Extranjero = e.row.data.Extranjero;
                    //    IngresarMarcaModelo();
                        if (e.editorName == 'dxSelectBox' && e.dataField == "Marca") {
                            e.editorOptions.onOpened = function (arg) {
                                arg.component.option("dataSource", lookupDataSource);
                                modelo = e.row.data.ModeloGenerico;
                                Extranjero = e.row.data.Extranjero;
                            }
                        }

                    }
                },

                /*
                onEditorPreparing: function (e) {
                    if (e.parentType == "dataRow" ) {
                        
                        IngresarMarcaModelo();

                        //Cambiar datasource del lookup dinamicamente al abrir
                        modelo = e.row.data.ModeloGenerico;
                        if (e.editorName == 'dxSelectBox') {
                            e.editorOptions.onOpened = function (arg) {
                                arg.component.option("dataSource", lookupDataSource);
                            }
                        }
                    
                    }
                },/*onCellClick: function (e) {
                    if (e.rowType == "data") {

                        //Cambiar datasource del lookup dinamicamente al abrir
                        modelo = e.row.data.ModeloGenerico;
                        IngresarMarcaModelo();
                        if (e.editorName == 'dxSelectBox') {
                            e.editorOptions.onOpened = function (arg) {
                                arg.component.option("dataSource", lookupDataSource);
                            }
                        }

                    }
                },*/
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
function IngresarMarcaModelo( modelo1) {
    lookupDataSource = null;
    /*lookupDataSource = {
        store: new DevExpress.data.CustomStore({
            key: "Descripcion",
            loadMode: "raw",
            load: function () {
                return $.getJSON("../CrearUsuario/ConsutarMarcaModeloGenerico?Modelo=" + modelo);
            }
        }),
        sort: "Descripcion"
    }*/

    $.ajax({
        url: "../CrearUsuario/ConsutarMarcaModeloGenerico?Modelo=" + modelo + "&Foraneo=" + Extranjero,
        type: "GET",
        async: false,
        success: function (msg) {
            lookupDataSource = msg
        }
    })
}


function IngresarMarca(CardCode) {
    /*
     lookupDataSource = {
         store: new DevExpress.data.CustomStore({
             key: "IdMarca",
             loadMode: "raw",
             load: function () {
                 return $.getJSON("../CrearUsuario/ConsultarMarcasItems");
             }
         }),
         sort: "Descripcion"
     }*/

    $.ajax({
        url: "../CrearUsuario/ConsultarMarcasItems?CardCode=" + CardCode,
        type: "GET",
        async: false,
        success: function (msg) {
            lookupDataSource = msg
        }
    })

    $.ajax({
        url: "../CrearUsuario/ConsultarVersion?Version=" + CardCode,
        type: "GET",
        async: false,
        success: function (msg) {
            $("#txtVersion").val(msg);
        }
    })

    
}

function ActualizarGenerico(valor) {
    $.ajax({
        url: '../CrearUsuario/ActualizarValores',
        type: 'POST',
        dataType: 'json',
        data: {
            generico: valor, Version: document.getElementById("txtVersion").value, CardCode: referencia
        },
        success: function (msg) {
            
            if (msg == "True") {
              
                $("#ListaPrecioUsuarioPortal").modal("hide");
                $("#ActualizacionRealizada").show('fade');
                setTimeout(function () {
                    $("#ActualizacionRealizada").fadeOut(1500);
                }, 3000);
            }
        },
        error: function (msg) {
         
            if (msg.responseText == "True") {
               
                $("#ListaPrecioUsuarioPortal").modal("hide");
                $("#ActualizacionRealizada").show('fade');
                setTimeout(function () {
                    $("#ActualizacionRealizada").fadeOut(1500);
                }, 3000);
            }
            else {
                $("#MensajeErrorInesperado").show('fade');
            }
        }

    });
}

// Method to upload a valid excel file
function upload() {



    var files = document.getElementById('file_upload').files;
    if (files.length == 0) {
        alert("Please choose any file...");
        return;
    }
    var filename = files[0].name;
    var extension = filename.substring(filename.lastIndexOf(".")).toUpperCase();
    if (extension == '.XLS' || extension == '.XLSX') {
        excelFileToJSON(files[0]);
    } else {
        alert("Please select a valid excel file.");
    }
}
//Method to read excel file and convert it into JSON 
function excelFileToJSON(file) {
    try {
        var reader = new FileReader();
        reader.readAsBinaryString(file);
        reader.onload = function (e) {

            var data = e.target.result;
            var workbook = XLSX.read(data, {
                type: 'binary'
            });
            var result = {};
            var NombreHoja = null;
            workbook.SheetNames.forEach(function (sheetName) {
                var roa = XLSX.utils.sheet_to_row_object_array(workbook.Sheets[sheetName]);
                if (roa.length > 0) {
                    result = roa;

                }
            });
            //displaying the json result
            /*
            var resultEle = document.getElementById("json-result");
            resultEle.value = JSON.stringify(result, null, 4);
            resultEle.style.display = 'block';*/
           // $("#tblListaPrecioUsuariosPortal").dxDataGrid("instance").option("editing.mode", "batch")
            for (let i = 0; i < result.length; i++) {
                var DPN = result[i].NumeroParteDacar;
                var Precio = result[i].PrecioVenta;
                var envio = result[i].PrecioEnvio;
                var NombreExtranjero = result[i].ReferenciaCliente;
                var Marca = result[i].Marca;
                $("#tblListaPrecioUsuariosPortal").dxDataGrid("instance").addRow();
                $("#tblListaPrecioUsuariosPortal").dxDataGrid("cellValue", 0, "DacarPartNumber", DPN);
                $("#tblListaPrecioUsuariosPortal").dxDataGrid("cellValue", 0, "PrecioProducto", Precio);
                $("#tblListaPrecioUsuariosPortal").dxDataGrid("cellValue", 0, "PrecioEnvio", envio);
                $("#tblListaPrecioUsuariosPortal").dxDataGrid("cellValue", 0, "CustomerReference", NombreExtranjero);
                $("#tblListaPrecioUsuariosPortal").dxDataGrid("cellValue", 0, "Marca", Marca);

            }
          //  $("#tblListaPrecioUsuariosPortal").dxDataGrid("saveEditData");
           // $("#tblListaPrecioUsuariosPortal").dxDataGrid("instance").option("editing.mode", "row")

        }
    } catch (e) {
        console.error(e);
    }
}

$('#GuardarNuevaLista').on("click", function (e) {
    if (document.getElementById("txtVersion").value == null || document.getElementById("txtVersion").value == "") {
        
        $("#NoRegistroProducto1").show('fade');
        setTimeout(function () {
            $("#NoRegistroProducto1").fadeOut(1500);
        }, 3000); return;
    }
    else {
        $("#tblListaPrecioUsuariosPortal").dxDataGrid("saveEditData");
        const datosTabla = $("#tblListaPrecioUsuariosPortal").dxDataGrid("getDataSource");
        ActualizarGenerico(datosTabla._store._array);
    }
    
  
});

$('#ListaPrecioUsuarioPortal').on('hidden.bs.modal', function () {
    
    
    document.getElementById("txtVersion").value = "";
   
})


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
    } else {
        if (password.match(number) && password.match(alphabets) && password.match(special_characters)) {
            $('#passwordbar').removeClass();
            $('#passwordbar').addClass('progress-bar bg-success');
            document.getElementById("passwordbar").style.width = "100%";
            $('#password-strength-status').removeClass();
            $('#password-strength-status').addClass('strong-password');
            $('#password-strength-status').html("Fuerte");
        }
        else {
            $('#passwordbar').removeClass();
            $('#passwordbar').addClass('progress-bar bg-warning');
            document.getElementById("passwordbar").style.width = "50%";
            $('#password-strength-status').removeClass();
            $('#password-strength-status').addClass('medium-password');
            $('#password-strength-status').html("Medio (debe incluir letras, numeros y caracteres especiales.)");
        }
    }

    if (password.length == 0) {
        $('#passwordbar').removeClass();
        ('#passwordbar').addClass('progress-bar bg-warning');
        document.getElementById("passwordbar").style.width = "0%";
    }
}
