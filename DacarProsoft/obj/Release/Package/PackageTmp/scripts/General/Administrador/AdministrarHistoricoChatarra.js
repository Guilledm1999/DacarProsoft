var temp = null;

$(document).ready(function () {
    $(".loading-icon").css("display", "none");
    ConsultaConsultarHistorico();
});

$('#LinkClose2').on("click", function (e) {
    $("#MensajeErrorInesperado").hide('fade');
});
$('#LinkClose9').on("click", function (e) {
    $("#MensajeActulalizacionCorrecta").hide('fade');
});
$('#LinkClose8').on("click", function (e) {
    $("#MensajeActualizacionIncorrecta").hide('fade');
});

function ConsultaConsultarHistorico() {
    $.ajax({
        url: "../Administrador/ConsultarHistoricoChatarra",
        type: "GET"
        , success: function (msg) {
            temp = msg;
            const locale = getLocale();
            DevExpress.localization.locale(locale);

            $("#tblHistoricoChatarra").dxDataGrid({
                dataSource: temp,
                keyExpr: 'HistoricoChatarraId',
                showBorders: true,
                columnAutoWidth: true,
                showBorders: true,
                showColumnLines: true,
                showRowLines: true,
                rowAlternationEnabled: false,
                allowColumnReordering: true,
                allowColumnResizing: false,
                columns: [

                    { dataField: "HistoricoChatarraId", visible: false },
                    {
                        dataField: "Anio", caption: "Año"
                    },
                    {
                        dataField: "Mes", caption: "Mes"
                    },
                    {
                        dataField: "TipoIngreso", caption: "Tipo Ingreso"
                    },
                    {
                        dataField: "Cantidad", caption: "Cantidad"
                    },
                    {
                        dataField: "Precio", caption: "Precio"
                    },
                    {
                        dataField: "Peso", caption: "Peso"
                    },
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
    function getLocale() {
        const storageLocale = sessionStorage.getItem('locale');
        return storageLocale != null ? storageLocale : 'es';
    }
}

function ActualizarRegistros() {
    $(".result").text("");
    $(".loading-icon").removeClass("hide");
    $(".btn").attr("disabled", true);
    $(".btn-txt").text("Espere...");

    $.ajax({
        url: '../Administrador/ActualizarHistoricoChatarra',
        type: 'GET',
        success: function (respuesta) {
            console.log("respuesta:" + respuesta);
            if (respuesta == "True") {
                ConsultaConsultarHistorico();
                $("#MensajeActulalizacionCorrecta").show('fade');
                setTimeout(function () {
                    $("#MensajeActulalizacionCorrecta").fadeOut(1500);
                }, 3000); return;
            } else {
                $("#MensajeErrorInesperado").show('fade');
                setTimeout(function () {
                    $("#MensajeErrorInesperado").fadeOut(1500);
                }, 3000); return;

            }
        }
    });
    $(".btn").attr("disabled", false);
    $(".btn-txt").text("Actualizar tabla");
}