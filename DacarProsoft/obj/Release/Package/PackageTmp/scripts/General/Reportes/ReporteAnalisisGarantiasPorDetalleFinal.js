$(document).ready(function () {
    //$("#txtMsjGarantia").hide();
    //$('.js-example-basic-single').select2();
    //prueba();
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


function ConsultarReporte() {
    var txtFechaAnalisis = $("#txtFechaAnalisis option:selected").text();
    var txtMesAnalisis = $("#txtMesAnalisis option:selected").text();
    var txtAreaResponsable = $("#txtAreaResponsable option:selected").text();
    var txtTipoBateria = $("#txtTipoBateria option:selected").text();
    var txtCausales = $("#txtCausales option:selected").text();
    var txtGrupoBateria = $("#txtGrupoBateria option:selected").text();

    if (txtFechaAnalisis == "--Selecione el año--" || txtMesAnalisis == "--Seleccione el mes--" || txtAreaResponsable == "--Seleccione el area--"
        || txtTipoBateria == "--Seleccione el tipo--" || txtCausales == "--Seleccione un causal--" || txtGrupoBateria == "--Seleccione el grupo--"    ) {
        $(".btn").attr("disabled", false);
        $(".btn-txt").text("Consultar");
        $("#MensajeIngreseTodosLosCampos").show('fade');
        setTimeout(function () {
            $("#MensajeIngreseTodosLosCampos").fadeOut(1500);
        }, 3000);
    }
    else {
        ConsultarReporteGeneral();
    }
}

function ConsultarReporteGeneral() {
    var txtFechaAnalisis = $("#txtFechaAnalisis option:selected").text();
    var txtMesAnalisis = $("#txtMesAnalisis option:selected").text();
    var txtAreaResponsable = $("#txtAreaResponsable option:selected").text();
    var txtTipoBateria = $("#txtTipoBateria option:selected").text();
    var txtCausales = $("#txtCausales option:selected").text();
    var txtGrupoBateria = $("#txtGrupoBateria option:selected").text();

    $.ajax({
        url: "../Reportes/ReporteDetalleGeneralGarantias",
        type: "POST",
        data: {
            FechaAnalisis: txtFechaAnalisis, MesAnalisis: txtMesAnalisis, AreaResponsable: txtAreaResponsable, TipoBateria: txtTipoBateria, Causales: txtCausales, GrupoBateria: txtGrupoBateria
        }
        , success: function (msg) {
            if (msg.length != 0) {
                $(".btn").attr("disabled", false);
                $(".btn-txt").text("Consultar");
                $("#tblGridResumenGeneral").dxDataGrid({
                    dataSource: msg,
                    showBorders: true,
                    columnAutoWidth: true,
                    showBorders: true,
                    columns: [{
                        caption: "Resultados Año " , alignment: "center",
                        columns: [
                            {
                                dataField: "Descripcion", caption: "Descripcion"
                            }, {
                                dataField: "anio1", caption: txtFechaAnalisis
                            }, {
                                dataField: "anio2", caption: parseInt(txtFechaAnalisis)-1
                            },
                            {
                                caption: "Acciones",
                                cellTemplate: function (container, options) {
                                    var btnDetalle = "<button class='btn-primary' onclick='ModalConsultarDetalles(" + JSON.stringify(options.data) + ")'>Detalle</button>";
                                    $("<div>")
                                        .append($(btnDetalle))
                                        .appendTo(container);
                                }
                            }
                        ]
                    }],
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
                            }],
                    }
                });
                ChartResumenesGarantias(msg);
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
