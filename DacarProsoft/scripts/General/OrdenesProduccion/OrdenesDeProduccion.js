
$(document).ready(function () {
    $("#Cargar").css("display", "none");
    $("#cargaImg").hide();
});


$('#LinkClose').on("click", function (e) {
    $("#MensajeErrorInesperado").hide('fade');
});
function ConsultarOrdenPorEstado() {
    $("#tblOrdenesProduccion").hide();

    $(".result").text("");
    $(".loading-icon").removeClass("hide");
    $("#Cargar").css("display");
    $(".btn").attr("disabled", true);
    $(".btn-txt").text("Espere...");
    ConsultarPedidos();
}

function ConsultarPedidos() {
    var select = $("#EstadoOrden option:selected").text();
    var valor = $("#EstadoOrden").val();
    var metodo1="../OrdenesProduccion/ConsultaDeOrdenesDeFabricacionPorEstado?estado=" + valor;
    var metodo2="../OrdenesProduccion/ConsultaDeOrdenesDeFabricacion";
    $("#cargaImg").show();
    $.ajax({
        url: metodo1,
        type: "GET"
       , success: function (msg) {
           $("#cargaImg").hide();
           ConfigDev.dataSource = msg;
           ConfigDev.allowColumnReordering = true,
           ConfigDev.allowColumnResizing = true,
           ConfigDev.columnAutoWidth = true,
           ConfigDev.showBorders = true,       
              ConfigDev.filterRow = { visible: true },
               ConfigDev.filterPanel = { visible: true },
               ConfigDev.headerFilter = { visible: true },
           ConfigDev.grouping = {
               autoExpandAll: false,
           },
           ConfigDev.groupPanel = {
               visible: false,
               
           },
           ConfigDev.columns = [          
                {dataField: "NumInterno", caption: "Numero de Orden",sortOrder: 'desc', groupIndex: 0},
               {dataField: "SeriesName", caption: "Tipo de Orden" },
                { dataField: "ItemName", caption: "Item" },
                 { dataField: "TipoProduccion", caption: "Tipo de Produccion" },
                 { dataField: "CreacionOrden", caption: "Fecha de Orden", dataType: "date", headerFilter:false},
                //{ dataField: "Status", caption: "Estado" },
                 { dataField: "ItemCode", caption: "Codigo de Item",visible:false},
                { dataField: "PlannedQty", caption: "Cantidad Planificada", visible: false },
                 { dataField: "CmpltQty", caption: "Cantidad Completada", visible: false },
                 { dataField: ("PlannedQty" - "Cmpltqty"), caption: "Cantidad Restante", visible: false },
                { dataField: "Linea", caption: "Linea", visible: false },
                  { dataField: "Expr1", caption: "Valor no identificado", visible: false },
                 { dataField: "BaseQty", caption: "Fecha de Orden", visible: false },
                { dataField: "Expr2", caption: "Valor no Identificado", visible: false },
                 { dataField: "IssuedQty", caption: "Cantidad Emitida", visible: false },
                { dataField: "WhsName", caption: "Tipo producto", visible: false },
                { dataField: "WhsCode", caption: "Fecha de Orden", visible: false },
                { dataField: "Comments", caption: "Comentarios", visible: false },
                {    caption: "Acciones",
                    cellTemplate: function (container, options) {
                        var lblDetalle="<a class='prev' onclick='ModaldetalleOrden(" + JSON.stringify(options.data) + ")'>Detalle</a>";
                        $("<div>")
                            .append($(lblDetalle))
                            .appendTo(container);
                    }
                }
           ];
           $("#cargaImg").hide();

           $(".btn").attr("disabled", false);
           $(".btn-txt").text("Consultar");
           $("#tblOrdenesProduccion").dxDataGrid(ConfigDev);
           $("#tblOrdenesProduccion").show();


       },
       error: function (msg) {
           $(".btn").attr("disabled", false);
           $(".btn-txt").text("Consultar");
           $("#MensajeErrorInesperado").show('fade');
           $("#cargaImg").hide();
        }

    })
}

function ModaldetalleOrden(modelo) {
    console.log(modelo);
    console.log(modelo.CmpltQty);

    $('.modal-title').text('Orden de fabricacion  # ' + modelo.NumInterno);
    $("#txtSeriesName").val(modelo.SeriesName);
    $("#txtItemName").val(modelo.ItemName);
    $("#txtTipoProduccion").val(modelo.TipoProduccion);
    $("#txtCreacionOrden").val(modelo.CreacionOrden);
    $("#txtStatus").val(modelo.Status);
    $("#txtItemCode").val(modelo.ItemCode);
    $("#txtPlannedQty").val(modelo.PlannedQty);
    $("#txtCmpltqty").val(modelo.CmpltQty);
    $("#txtRestante").val(modelo.PlannedQty - modelo.CmpltQty);
    $("#txtLinea").val(modelo.Linea);
    $("#txtBaseQty").val(modelo.BaseQty);
    $("#txtIssuedQty").val(modelo.IssuedQty);
    $("#txtWhsName").val(modelo.WhsName);
    $("#txtWhsCode").val(modelo.WhsCode);
    $("#txtComments").val(modelo.Comments);
    $("#ModalDetalleOrden").modal("show");
}