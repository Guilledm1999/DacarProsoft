var referencia = null;
var comprobar = false;
var temporalListPrecio = null;
var tempIdLis = null;
var lookupDataSource = null;
var lookupDataSourceModelo = null;
var modelo = null;
var Extranjero = null;
var ListaPrecioGenerica = null;
var ItemCodeId = null;
var excel = null;

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

    if (document.getElementById("txtVersion").value == null || document.getElementById("txtVersion").value == "") {
      //  $("#ListaPrecioUsuarioPortal").modal("hide");
        $("#NoRegistroProducto1").show('fade');
        setTimeout(function () {
            $("#NoRegistroProducto1").fadeOut(1500);
        }, 3000); return;
    }
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
    IngresarMarca();
    if (comprobar != true) {

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
                        mode: 'row',
                        allowUpdating: true,
                        allowAdding: true,
                        allowDeleting: true,
                        useIcons: true,
                    },
                    columns: [
                        {
                            dataField: "ItemCode", visible: false
                        },
                        {
                            dataField: "ListaPrecioClienteId", visible: false
                        },
                        {
                            dataField: "CustomerReference", caption: "Referencia Cliente", alignment: 'left'

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
                            dataField: "ModeloGenerico", caption: "Modelo Génerico", allowEditing: false, alignment: 'left'
                            /*lookup: {

                                dataSource: lookupDataSourceModelo,
                                valueExpr: "ItemCode",
                                displayExpr: "Descripcion",
                            },*/

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
                            dataField: "QuantityXLayer", caption: "Cant. x Piso", visible: true, allowEditing: true, alignment: 'left'
                        },
                        {
                            dataField: "CantidadPorPallet", caption: "Cant. x Pallet", visible: true, allowEditing: true, dataType: 'number', alignment: 'left'
                        },
                        {
                            dataField: "Pisos", caption: "Pisos", visible: true, allowEditing: true, width: 50, alignment: 'center'
                        },
                        {
                            dataField: "Categoria", visible: false, allowEditing: false, alignment: 'center'
                        },
                        {
                            dataField: "PrecioProducto", caption: "Precio Venta", allowEditing: true, format: 'currency', dataType: 'number', alignment: 'left'
                        },
                        {
                            dataField: "PrecioEnvio", caption: "Precio Envio", allowEditing: true, format: 'currency', dataType: 'number', alignment: 'left'
                        },
                        {
                            //ConsutarMarcaModeloGenerico()
                            dataField: "Marca", caption: "Marca", allowEditing: true,
                            lookup: {

                                dataSource: lookupDataSource,
                                valueExpr: "ItemCode",
                                displayExpr: "Descripcion",
                            }, alignment: 'center',

                            setCellValue: function (newData, value, currentRowData, display) {


                                EncontrarItemCode(currentRowData.CustomerReference, currentRowData.ModeloGenerico, value);
                                newData.ItemCode = ItemCodeId;
                                newData.Marca = value;

                            },

                        },
                        {
                            type: 'buttons',
                            buttons: ['delete', 'save', 'edit'], width: 60
                        }
                    ],
                    /*
                    onCellClick: function (e) {
                        if (e.rowType == "data") {
                            if (e.column.caption == "Marca") {
                                //Cambiar datasource del lookup dinamicamente al abrir
                                //  modelo = e.row.data.ModeloGenerico;
                                //  Extranjero = e.row.data.CustomerReference;
                                modelo1 = e.row.data.ModeloGenerico;
                                Extranjero = e.row.data.CustomerReference;
                                IngresarMarcaModelo();

                                }

                           
                        }
                    },

                    onEditorPreparing: function (e) {
                        if (e.parentType == "dataRow") {


                            //Cambiar datasource del lookup dinamicamente al abrir
                            modelo = e.row.data.ModeloGenerico;
                            Extranjero = e.row.data.Extranjero;

                        //  IngresarMarcaModelo();
                            if (e.editorName == 'dxSelectBox' && e.dataField == "Marca") {
                                e.editorOptions.onOpened = function (arg) {
                                    modelo = e.row.data.ModeloGenerico;
                                    Extranjero = e.row.data.Extranjero;

                                   // IngresarMarcaModelo();
                                    arg.component.option("dataSource", lookupDataSource);
                                }
                            }

                        }
                    },

*/
                  
                    onRowInserted: function (e) {
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

    }
       
        $("#ListaPrecioUsuarioPortal").modal("show");
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
            $("#tblListaPrecioUsuariosPortal").dxDataGrid("instance").option("editing.mode", "batch")
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
            $("#tblListaPrecioUsuariosPortal").dxDataGrid("saveEditData");
            $("#tblListaPrecioUsuariosPortal").dxDataGrid("instance").option("editing.mode", "row")

        }
    } catch (e) {
        console.error(e);
    }
}
function IngresarMarcaModelo() {
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
        url: "../CrearUsuario/ConsutarMarcaModeloGenerico?CardCode=" + CardCode + "&Foraneo=" + Extranjero,
        type: "GET",
        async: false,
        success: function (msg) {
            lookupDataSource = msg
        }
    })
}

function IngresarMarca() {
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
        url: "../CrearUsuario/ConsultarMarcasItems?CardCode=" + referencia,
        type: "GET",
        async: false,
        success: function (msg) {
            lookupDataSource = msg
        }
    })

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
}

function EncontrarItemCode(Foran, model, marc) {
  


    $.ajax({
        url: "../CrearUsuario/EncontrarItemCode?Foraneo=" + Foran + "&Modelo=" + model + "&Marca=" + marc,
        async: false,
        success: function (msg) {
            var regr = msg;

            ItemCodeId = regr;
          // var ret= myArray[1];
        }
    })

 

}

function SeleccionarModelosGenericos() {
    comprobar2 = false;
    $.ajax({
        url: "../CrearUsuario/ConsultarDatosGenericosBateria",
        type: "GET"
        , success: function (msg) {
            //ItemsPedidoCliente = msg;  
            $("#tblDatosGenericosBateria").dxDataGrid({
                dataSource: msg,
                keyExpr: 'DatosTecnicosBatExporId',
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
                        dataField: "DatosTecnicosBatExporId", visible: false
                    },
                    {
                        dataField: "Modelo", caption: "Modelo", visible: true, allowEditing: false
                    },
                    {
                        dataField: "C20", caption: "Referencia Cliente", allowEditing: false, visible: false
                    },
                    {
                        dataField: "Pisos", caption: "Pisos", allowEditing: false
                    },
                    {
                        dataField: "PesoSellada", caption: "Modelo Génerico", allowEditing: false, visible: false
                    },
                    {
                        dataField: "PesoHumedaKg", visible: false, allowEditing: false, visible: false
                    },
                    {
                        dataField: "CCAMenos18CLocal", visible: false, allowEditing: false, visible: false
                    },
                    {
                        dataField: "CCAMenos18CExpo", visible: false, allowEditing: false, visible: false
                    },
                    {
                        dataField: "CAP", visible: false, allowEditing: false, visible: false
                    },
                    {
                        dataField: "CantidadPorPallet", visible: true, allowEditing: true, caption: "Cantidad por Pallet"
                    },
                    {
                        dataField: "CantidadPiso", visible: true, allowEditing: true, caption: "Cantidad piso"
                    },
                    {
                        dataField: "CantidadPiso", visible: true, allowEditing: true, caption: "Cantidad piso"
                    },


                                
                ],
                onRowUpdating: function (e) {
                    comprobar2 = true;
                    temporalListPrecio2 = e.newData;
                    tempIdLis2 = e.key;
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
        valVali = false;
    } else {
        valVali = true;
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
            listaProductos: datosTabla._store._array,
            Version: $("#txtVersion").val(),

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

$('#CerrarUsuarioPortal').on("click", function (e) {
    $("#ModalCrearUsuarioPortal").modal("hide");
    comprobar = false
    document.getElementById("txtContrasenaPortal").value = "";
    document.getElementById("file_upload").value = "";
    
});

$('#ModalCrearUsuarioPortal').on('hidden.bs.modal', function () {
    $("#ModalCrearUsuarioPortal").modal("hide");
    comprobar = false
    document.getElementById("txtContrasenaPortal").value = "";
    document.getElementById("txtVersion").value = "";
    document.getElementById("file_upload").value = "";
})


function checkPasswordStrength() {
    var number = /([0-9])/;
    var alphabets = /([a-zA-Z])/;
    var special_characters = /([~,!,@,#,$,%,^,&,*,-,_,+,=,?,>,<])/;
    var password = $('#txtContrasenaPortal').val().trim();
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
