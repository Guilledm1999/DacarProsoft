$(document).ready(function () {
    $("#txtMsjGarantia").hide();
    $('.js-example-basic-single').select2();

    $(".loading-icon").css("display", "none");
    $(document).on('click', '.fa', function (event) {
        event.preventDefault();
        $(this).closest('tr').remove();
    });
    //var x = document.getElementById("ContenidoDiv");
    //x.style.display === "none";
});

$('#LinkClose').on("click", function (e) {
    $("#MensajeErrorInesperado").hide('fade');
});

$('#LinkClose2').on("click", function (e) {
    $("#MensajeIngreseTodosLosCampos").hide('fade');
});

$('#LinkClose3').on("click", function (e) {
    $("#MensajeSinInformacion").hide('fade');
});

function ConsultarReporte() {
    $(".result").text("");
    $(".loading-icon").removeClass("hide");
    $(".btn").attr("disabled", true);
    $(".btn-txt").text("Espere...");


    var txtFecha = $("#txtFecha option:selected").text();
    var txtTipoCliente = $("#txtTipoCliente option:selected").val();
    var txtClienteClase = $("#txtClienteClase option:selected").val();
    var txtClienteLinea = $("#txtClienteLinea option:selected").val();

    if (txtFecha == "--Selecione el año--" || txtTipoCliente.length == 0 || txtClienteClase.length == 0 || txtClienteLinea.length == 0) {
        $(".btn").attr("disabled", false);
        $(".btn-txt").text("Consultar");
        $("#MensajeIngreseTodosLosCampos").show('fade');
        setTimeout(function () {
            $("#MensajeIngreseTodosLosCampos").fadeOut(1500);
        }, 3000);
    }

    else {
        AnalisisGarantiasRegistrados();
    }


}


function AnalisisGarantiasRegistrados() {
    //$("#lbltabladescriptiva").show();
    var txtFecha = $("#txtFecha option:selected").text();
    var txtTipoCliente = $("#txtTipoCliente option:selected").val();
    var txtClienteClase = $("#txtClienteClase option:selected").val();
    var txtClienteLinea = $("#txtClienteLinea option:selected").val();

   
    $.ajax({
        url: "../Reportes/ReporteDetalleGarantiaPorTipoDeCliente?tipoCliente=" + txtTipoCliente + "&ClienteClase=" + txtClienteClase + "&ClienteLinea=" + txtClienteLinea + " &Anio=" + txtFecha,
        type: "GET"
        , success: function (msg) {
            if (msg.length != 0) {
                $(".btn").attr("disabled", false);
                $(".btn-txt").text("Consultar");

                $("#tblGridResumenTipoCliente").dxDataGrid({
                    dataSource: msg,
                    showBorders: true,
                    columnAutoWidth: true,
                    showBorders: true,
                    columns: [
                        {
                            dataField: "Descripcion", caption: "Descripcion"
                        }, {
                            dataField: "Valor", caption: "Cantidad"
                        }, {
                            dataField: "Porcentaje", caption: "Porcentaje(%)"
                        }

                        
                    ],
                    summary: {
                        totalItems: [
                            {
                                name: "Valor",
                                column: "Valor",
                                summaryType: "sum",
                                displayFormat: "Total: {0}",
                                showInColumn: "Valor",
                                customizeText: function (e) {
                                    if (e.value != 0 && e.value != "") {
                                        return "Total: " + (e.value)
                                    }
                                }
                            },
                            {
                                name: "Porcentaje",
                                column: "Porcentaje",
                                summaryType: "sum",
                                displayFormat: "Total: {0}",
                                showInColumn: "Porcentaje",
                                customizeText: function (e) {
                                    if (e.value != 0 && e.value != "") {
                                        return "100%" //+ (e.value)
                                    }
                                }
                            }

                        ],
                    }
                });
              

                //ChartResumenesGarantias(msg, type);



            } else {

                $(".btn").attr("disabled", false);
                $(".btn-txt").text("Consultar");

                $("#MensajeSinInformacion").show('fade');
                setTimeout(function () {
                    $("#MensajeSinInformacion").fadeOut(1500);
                }, 3000); return;


            }

        },
        error: function (msg) {
            $(".btn").attr("disabled", false);
            $(".btn-txt").text("Consultar");
            $("#MensajeErrorInesperado").show('fade');
            setTimeout(function () {
                $("#MensajeErrorInesperado").fadeOut(1500);
            }, 3000); return;
        }
    })

}