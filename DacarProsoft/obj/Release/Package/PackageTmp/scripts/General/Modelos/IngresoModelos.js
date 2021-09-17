$(document).ready(function () {
    ListadoModelos();
});

function ListadoModelos() {
    $.ajax({
        url: "../Modelos/ConsultaDetalleChatarra",
        type: "GET"
   , success: function (msg) {
       ConfigDev.dataSource = msg;
       ConfigDev.columnAutoWidth = true,
       ConfigDev.showBorders = true,
          ConfigDev.filterRow = { visible: true },
           ConfigDev.filterPanel = { visible: true },
           ConfigDev.headerFilter = { visible: true },
       ConfigDev.columns = [
           { dataField: "ModeloID", caption: "# Modelo" },
           { dataField: "Nombre", caption: "Nombre" },
           { dataField: "PesoTeorico", caption: "Peso Teorico" },   
       ];
       $("#tblModelosChatarras").dxDataGrid(ConfigDev);

   },
     
    })
}


function IngresarModeloChatarra() {
    $("#ModalIngresoModeloChatarra").modal("show");
}


function CrearModeloChatarra() {
    if ($("#txtNombreChatarra").val() == "") {
        alert("Ingrese Descripcion");
        return;
    }
    if ($("#txtPesoTeorico").val() == "") {
        alert("Ingrese Peso Teorico");
        return;
    }

    $.ajax({
        url: "../Modelos/GuardarModeloChatarra",
        type: "POST",
        data: {
            Descripcion: $("#txtNombreChatarra").val(),
            PesoTeorico: $("#txtPesoTeorico").val(),

        }, success: function (msg) {
            ListadoModelos();
            $("#ModalIngresoModeloChatarra").modal("hide");
            $("#txtNombreChatarra").val("");
            $("#txtPesoTeorico").val("");
            alert("Registro Ingresado");

        },
        error: function (msg) {
            alert(msg);
        }
    })
}