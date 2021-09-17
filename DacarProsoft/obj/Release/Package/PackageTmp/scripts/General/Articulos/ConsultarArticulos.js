$(document).ready(function () {
    ConsultarArticulos();
});


$('#LinkClose').on("click", function (e) {
    $("#MensajeErrorInesperado").hide('fade');
});

function ConsultarArticulos() {
    $.ajax({
        url: "../Articulos/ConsultaDeArticulos",
        type: "GET"
       , success: function (msg) {

         ConfigDev.dataSource = msg;
           ConfigDev.allowColumnReordering = true,
               ConfigDev.allowColumnResizing = true,
               ConfigDev.columnAutoWidth = true,
               ConfigDev.showBorders = true,
            ConfigDev.filterRow = { visible: true },
           ConfigDev.filterPanel = { visible: true },
           ConfigDev.headerFilter = { visible: true },
          ConfigDev.dataSource = msg;
           ConfigDev.columns = [
                { dataField: "Codigo", caption: "Codigo" },
                { dataField: "Descripcion", caption: "Descripcion" },
                { dataField: "ModeloBc", caption: "ModeloBc" },
                 { dataField: "Marca", caption: "Marca" },
                { dataField: "Grupo", caption: "Grupo" },
                 { dataField: "Categoria", caption: "Categoria" },
                { dataField: "SubCategoria", caption: "Subcategoria" }
           ];
           $("#tblTiposArticulos").dxDataGrid(ConfigDev);
       },
        error: function (msg) {
            $("#MensajeErrorInesperado").show('fade');
        }

    })
}



