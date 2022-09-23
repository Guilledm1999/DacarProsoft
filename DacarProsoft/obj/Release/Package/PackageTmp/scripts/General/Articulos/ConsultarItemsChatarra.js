$(document).ready(function () {
    ConsultarItemChatarra();
});


$('#LinkClose').on("click", function (e) {
    $("#MensajeErrorInesperado").hide('fade');
});

function ConsultarItemChatarra() {
    $.ajax({
        url: "../Articulos/ConsultarPesosChatarra",
        type: "GET"
       , success: function (msg) {

         ConfigDev.dataSource = msg;
               ConfigDev.allowColumnReordering = false,
               ConfigDev.allowColumnResizing = true,
               ConfigDev.columnAutoWidth = false,
               ConfigDev.showBorders = true,
            ConfigDev.filterRow = { visible: false },
           ConfigDev.filterPanel = { visible: false },
           ConfigDev.headerFilter = { visible: true },
          ConfigDev.dataSource = msg;
           ConfigDev.columns = [
                { dataField: "CodigoItem", caption: "Codigo", alignment: "right", width: 160},
                { dataField: "ItemName", caption: "Descripcion", alignment: "right", width: 160 },
                {
                    dataField: "PesoArticulo", caption: "Peso Articulo(kg)", alignment: "right", width: 160, calculateCellValue: function (rowData) {
                        return (rowData.PesoArticulo).toFixed(2);
                    }
                },
               
           ];
           $("#TablaItemsChatarra").dxDataGrid(ConfigDev);
       },
        error: function (msg) {
            $("#MensajeErrorInesperado").show('fade');
        }

    })
}



