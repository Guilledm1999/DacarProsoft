$(document).ready(function () {
    $('.js-example-basic-single').select2();

    $(".loading-icon").css("display", "none");
    $(document).on('click', '.fa', function (event) {
        event.preventDefault();
        $(this).closest('tr').remove();
    });
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



function cargarNombresRegistros() {
    if ($("#txtNombreBase option:selected").val() != null && $("#txtNombreBase option:selected").val() != "") {
        $("#txtNombreRegistro").empty();
        $("#txtNombreRegistro").append('<option value="">--Seleccione el registro--</option>');
        $.ajax({
            type: 'POST',
            url: "../Calidad/ConsultarRegistrosLcn",
            dataType: 'json',
            data: { nombre: $("#txtNombreBase option:selected").val() },
            success: function (articulos) {
                $.each(articulos, function (i, articulo) {
                    $("#txtNombreRegistro").append('<option value="' + articulo.Value + '">' +
                        articulo.Text + '</option>');
                });
            },
        })
    } else {
        $("#txtNombreRegistro").empty();
        $("#txtNombreRegistro").append('<option value="">--Seleccione el registro--</option>');
    }
}



function ConsultarReporte() {
    var txtNombreBase = $("#txtNombreBase option:selected").text();
    var txtNombreRegistro = $("#txtNombreRegistro option:selected").text();

    if (txtNombreBase == "--Seleccione la base a consultar--" || txtNombreRegistro == "--Seleccione el registro--") {
        $(".btn").attr("disabled", false);
        $(".btn-txt").text("Consultar");
        $("#MensajeIngreseTodosLosCampos").show('fade');
        setTimeout(function () {
            $("#MensajeIngreseTodosLosCampos").fadeOut(1500);
        }, 3000);
    }
    else {
        ConsultarReporteAccess();
    }
}

function ConsultarReporteAccess() {
    var txtNombreBase = $("#txtNombreBase option:selected").val();
    var txtNombreRegistro = $("#txtNombreRegistro option:selected").val();
    $.ajax({
        type: 'POST',
        url: "../Calidad/ConsultarDetalleRegistrosLcn",
        dataType: 'json',
        data: { nombreBase: txtNombreBase, testUnique: txtNombreRegistro }

            , success: function (msg) {
                //if (msg.length != 0) {
                //    $(".btn").attr("disabled", false);
                //    $(".btn-txt").text("Consultar");
                //    $("#tblGridResumenCliente").dxDataGrid({
                //        dataSource: msg,
                //        showBorders: true,
                //        columnAutoWidth: true,
                //        showBorders: true,
                //        columns: [{
                //            caption: "Resultados Año " + fecha + " del cliente:" + cliente, alignment: "center",
                //            columns: [
                //                {
                //                    dataField: "Descripcion", caption: "Descripcion"
                //                }, {
                //                    dataField: "Valor", caption: "Cantidad"
                //                }, {
                //                    dataField: "Porcentaje", caption: "Porcentaje(%)"
                //                },
                //                {
                //                    caption: "Acciones",
                //                    cellTemplate: function (container, options) {
                //                        var btnDetalle = "<button class='btn-primary' onclick='ModalConsultarDetalles(" + JSON.stringify(options.data) + ")'>Detalle</button>";
                //                        $("<div>")
                //                            .append($(btnDetalle))
                //                            .appendTo(container);
                //                    }
                //                }
                //            ]
                //        }],
                //        summary: {
                //            totalItems: [
                //                {
                //                    name: "Valor",
                //                    column: "Valor",
                //                    summaryType: "sum",
                //                    displayFormat: "Total: {0}",
                //                    showInColumn: "Valor",
                //                    customizeText: function (e) {
                //                        if (e.value != 0 && e.value != "") {
                //                            return "Total: " + (e.value)
                //                        }
                //                    }
                //                },
                //                {
                //                    name: "Porcentaje",
                //                    column: "Porcentaje",
                //                    summaryType: "sum",
                //                    displayFormat: "Total: {0}",
                //                    showInColumn: "Porcentaje",
                //                    customizeText: function (e) {
                //                        if (e.value != 0 && e.value != "") {
                //                            return "100%" //+ (e.value)
                //                        }
                //                    }
                //                }],
                //        }
                //    });
                //    ChartResumenesGarantias(msg);
                //} else {

                //    $(".btn").attr("disabled", false);
                //    $(".btn-txt").text("Consultar");

                //    $("#MensajeSinInformacion").show('fade');
                //    setTimeout(function () {
                //        $("#MensajeSinInformacion").fadeOut(1500);
                //    }, 3000); return;
                //}
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