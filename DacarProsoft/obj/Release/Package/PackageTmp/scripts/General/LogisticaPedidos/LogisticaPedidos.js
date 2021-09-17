
$(document).ready(function () {

    var cboCategoriaClass = document.getElementById("SelectSubCategoria");
    cboCategoriaClass.addEventListener("change", ConsultarArticulos);

   
    //ConsultarPedidos();
  });

function ConsultarPedidos() {
    $.ajax({
        url: "../LogisticaPedido/ConsultarPedido",
        type: "GET"
       , success: function (msg) {

           ConfigDev.dataSource = msg;
           ConfigDev.columns = [
                { dataField: "NombreCompleto", caption: "Nombres" },
                { dataField: "NombreUsuario", caption: "Usuario" },
                { dataField: "DescripcionTipoUsuario", caption: "Tipo" },
                {
                    caption: "Acciones",
                    cellTemplate: function (container, options) {
                        var btn = "<button class='btn btn-light' onclick='ModalEditarUsuario(" + JSON.stringify(options.data) + ")'>Editar</button>";

                        $("<div>")
                            .append($(btn))
                            .appendTo(container);
                    }
                }
           ];
           $("#tblPedidos").dxDataGrid(ConfigDev);
       },
        error: function (msg) {

            alert(msg);
        }

    })
}


function ModalEditarPedido(modelo) {
    console.log(modelo);
    $("#txtIdTipoUsuario").val(modelo.IdUsuario);
    $("#txtNombres").val(modelo.NombreCompleto);
    $("#ModalPedido").modal("show");

}

function ModalCrearPedido() {

    $("#txtIdTipoUsuario").val(0);
    $("#ModalPedido").modal("show");
}


function ConsultarArticulos() {
 
    var select = $("#CategoriaClass option:selected").text();
    var valor = $("#CategoriaClass").val();
    console.log(select);

    var select2 = $("#SelectSubCategoria option:selected").text();
    var valor2 = $("#SelectSubCategoria").val();
    console.log(select);
  
    $.ajax({
        url: "../LogisticaPedidos/ConsultaDeArticulosPorCategorias?Categoria=" + valor + "&&Subcategoria=" + valor2,
        type: "GET"

       , success: function (msg) {
           ConfigDev.dataSource = msg;
           ConfigDev.showBorders = true;
           ConfigDev.paging = {
                   pageSize: 5
           },
           ConfigDev.selection = {
               mode: "multiple"
           };
           ConfigDev.filterRow = {
               visible: true
           },
           ConfigDev.editing = {
               mode: "cell",
               allowUpdating: true
           },
           ConfigDev.columns = [
                { dataField: "Codigo", caption: "Codigo"  ,allowEditing: false},
                { dataField: "Descripcion", caption: "Descripcion", allowEditing: false },
                 { dataField: "Categoria", caption: "Categoria", allowEditing: false },
                { dataField: "SubCategoria", caption: "Subcategoria", allowEditing: false,type:Number },
                { dataField:"0" ,caption: "Total", allowEditing: true }
           ];
           ConfigDev.onSelectionChanged= function(selectedItems) {
               var data = selectedItems.selectedRowsData;
               if(data.length > 0)
                   $("#selected-items-container").text(
                   $.map(data, function(value) {
                       return value.FirstName + " " + value.LastName;
                   }).join(", "));
               else 
                   $("#selected-items-container").text("Nobody has been selected");
               if(!changedBySelectBox)
                   $("#select-prefix").dxSelectBox("instance").option("value", null);
    
               changedBySelectBox = false;
               clearButton.option("disabled", !data.length);
           }
           $("#tblListaProductos").dxDataGrid(ConfigDev);
       },
        error: function (msg) {

            alert(msg);
        }

    })
    $("#select-all-mode").dxSelectBox({
                dataSource: ["allPages", "page"],
                value: "allPages",
                onValueChanged: function (data) {
                    dataGrid.option("selection.selectAllMode", data.value);
                }
            });
            $("#show-checkboxes-mode").dxSelectBox({
                dataSource: ["none", "onClick", "onLongTap", "always"],
                value: "onClick",
                onValueChanged: function (data) {
                    dataGrid.option("selection.showCheckBoxesMode", data.value);
                    $("#select-all-mode").dxSelectBox("instance").option("disabled", data.value === "none");
                }
            });
}


function CrearPedido() {
    if ($("#selectCliente").val() == "") {
        alert("Ingrese Cliente.");
        return;
    }
    if ($("#txtFechaEmision").val() == "") {
        alert("Ingrese fecha de Emision.");
        return;
    }
    if ($("#txtOrdenCompra").val() == "") {
        alert("Ingrese Orden de compra.");
        return;
    }
    if ($("#txtLugarEntrega").val() == "") {
        alert("Ingrese un lugar de entrega .");
        return;
    }

    $.ajax({
        url: "../LogisticaPedidos/ConsultaInternaDeClientes",
        type: "GET",
        success: function (msg) {

            GuardarArticulo(msg);
            //ConsultarUsuarios();
            //$("#ModalPedido").modal("hide");
            //alert(msg);
        },
        error: function (msg) {
            GuardarCliente();
        }
    })


    //$.ajax({
    //    url: "../LogisticaPedidos/GuardarPedido",
    //    type: "POST",
    //    data: {
    //        IdUsuario: $("#txtIdTipoUsuario").val(),
    //        NombreCompleto: $("#txtNombres").val(),
    //        NombreUsuario: $("#txtUsuario").val(),
    //        contrasena: $("#txtContrasenia").val(),
    //        TipoUsuario: $("#selectTipoUsuario").val()

    //    }, success: function (msg) {
    //        ConsultarUsuarios();
    //        $("#ModalPedido").modal("hide");
    //        alert(msg);
    //    },
    //    error: function (msg) {
    //        alert(msg);
    //    }
    //})
}



function GuardarCliente() {
    var queryResult = Enumerable.From(objeto).Where(function (x) {
        return x.Cedula == $("#select").val
    }).Select(function (x) { return x }).toArray();

    $.ajax({
        url: "../LogisticaPedidos/GuardarCliente",
        type: "POST",
        data: {      
            queryResult,
        }, success: function (msg) {
            CrearPedido();
            alert(msg);
        },
        error: function (msg) {
            alert(msg);
        }
    })
}


function GuardarArticulo(cliente,articulos) {

    /* Tratamos los datos */
    var points = JSON.parse(cliente);
    /* Los convertimos en un array añadiendo el "id" a los campos */
    var points_array = Object.keys(points).map(
      function (clave) {
          var elemento = points[clave];
          elemento.id = clave;
          return elemento;
      }
    );
    /* Los ordenamos comparando los puntos (points) */
    points_array.sort(
      function(a, b) {
          return b.points - a.points;
      }
    );


    $.ajax({
        url: "../CrearUsuario/GuardarArticulosPedidos",
        type: "POST",
        data: {
            ClienteId:points_array[0].id,
            articulos   
        }, success: function (msg) {
            ConsultarUsuarios();
            $("#ModalTipoUsuario").modal("hide");
            alert(msg);
        },
        error: function (msg) {
            alert(msg);
        }
    })
}

//IdUsuario: $("#txtIdTipoUsuario").val(),
//NombreCompleto: $("#txtNombres").val(),
//NombreUsuario: $("#txtUsuario").val(),
//contrasena: $("#txtContrasenia").val(),
//TipoUsuario: $("#selectTipoUsuario").val()





