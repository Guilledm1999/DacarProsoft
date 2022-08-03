
$(document).ready(function () {
    InicializarCalendario();
});

$('#LinkClose3').on("click", function (e) {
    $("#MensajeErrorGeneral").hide('fade');
});
$('#LinkClose2').on("click", function (e) {
    $("#MensajeSinFactura").hide('fade');
});



function InicializarCalendario() {
    var resConsulta = ConsultarEventos();
    var initialLocaleCode = 'es';
        var calendarEl = document.getElementById('calendar');
    var calendar = new FullCalendar.Calendar(calendarEl, {

        headerToolbar: {
            left: 'prev,next today',
            center: 'title',
            right: 'dayGridMonth,timeGridWeek'
        },
        locale: initialLocaleCode,
        buttonIcons: false, // show the prev/next text
        weekNumbers: true,
        navLinks: true, // can click day/week names to navigate views
        editable: false,
        selectable: true,

        events: resConsulta,
        eventColor: '#000000',

        eventClick: function (info) {
            var objTag = document.getElementById("pruebaPdf");

            objTag.removeAttribute('data');

            //console.log(objTag.getAttribute('data'));
            //objTag.removeAttribute('data');

            var valorConsul = ConsultarPdf(info.event.extendedProps.cardCode, info.event.extendedProps.descripcion);

            if (valorConsul != "") {
                var objTag = document.getElementById("pruebaPdf");

                objTag.setAttribute('data', "data:application/pdf;base64," + valorConsul);
                console.log("true"+objTag.getAttribute('data'));


            } else {
                var objTag = document.getElementById("pruebaPdf");

                objTag.setAttribute('data', "data:application/pdf;base64,");
                console.log("false: "+objTag.getAttribute('data'));

                $("#MensajeSinFactura").show('fade');
                setTimeout(function () {
                    $("#MensajeSinFactura").fadeOut(1500);
                }, 3000);
            }
            mostrarDatosFactura(info.event.extendedProps.cardCode, info.event.extendedProps.descripcion);

            $("#tituloModal").text(info.event.title);
            $("#txtDetalleEVento").text(info.event.extendedProps.descripcion);
            $("#txtDetalleEVentoContener").text(info.event.extendedProps.Booking);

            $("#txtDetalleEVentoFechaPedido").text(info.event.extendedProps.fechaPedido);
            $("#txtDetalleEVentoFechaDespacho").text(info.event.extendedProps.fechaDespacho);

            $("#txtDetalleEVentoDestino").text(info.event.extendedProps.destino);
            $("#txtDetalleEVentoFechaZarpe").text(info.event.extendedProps.fechaZarpe);
            $("#txtDetalleEVentoBooking").text(info.event.extendedProps.BookinText);         
          
            $("#modalDescripcionEvento").modal("show")
        },
    });
        calendar.render();   
}


function ConsultarEventos() {
    var res;
    $.ajax({
        url: "../Pedidos/EventosCalendarioPedidos",
        type: "GET",
        async: false,
        success: function (msg) {
            res = msg;
        },
        error: function (msg) {
            $("#MensajeErrorGeneral").show('fade');
            setTimeout(function () {
                $("#MensajeErrorGeneral").fadeOut(1500);
            }, 3000);
        }
    })
    return res;
}

function ConsultarPdf(var1, var2) {
    var res;
    $.ajax({
        url: "../Pedidos/ConvertirPdf?cardCode=" + var1 + "&numeroOrden=" + var2,
        type: "GET",
        async: false,
        success: function (msg) {
            res = msg;
        },
        error: function (msg) {
            $("#MensajeErrorGeneral").show('fade');
            setTimeout(function () {
                $("#MensajeErrorGeneral").fadeOut(1500);
            }, 3000);
        }
    })
    return res;
}


function mostrarDatosFactura(cardCode, orden) {
    $.ajax({
        url: "../Pedidos/ConsultarDsecripFact?cardCode=" + cardCode + "&numeroOrden=" + orden,
        type: "GET",
        async: false,
        success: function (msg) {
            $("#tblDescripFact").dxDataGrid({
                dataSource: msg,
                keyExpr: 'numeroItem',
                showBorders: true,
                columnAutoWidth: true,
                showBorders: true,
                showColumnLines: true,
                showRowLines: true,
                rowAlternationEnabled: false,
                allowColumnReordering: true,
                allowColumnResizing: false,
                          
                headerFilter: {
                    visible: true
                },
                //customizeColumns(columns) {
                //    columns[0].width = 70;
                //},
                loadPanel: {
                    enabled: false,
                },
                scrolling: {
                    mode: 'infinite',
                },             
                columns: [
                    { dataField: "numeroItem", caption: "Item", allowEditing: false, allowHeaderFiltering: false },
                    {
                        dataField: "Description", caption: "Descripcion", allowEditing: false, allowHeaderFiltering: false
                    },
                    {
                        dataField: "Quantity", caption: "Cantidad", allowEditing: false, headerFilter: false, allowHeaderFiltering: false
                    },
                    {
                        dataField: "Price", caption: "Precio Unitario", allowEditing: false, headerFilter: true, allowHeaderFiltering: true
                    },
                    {
                        dataField: "TotalPrice", caption: "Precio Total", allowEditing: false, headerFilter: true, allowHeaderFiltering: true
                    },
                ],
                summary: {
                    totalItems: [
                        {
                            name: "numeroItem",
                            column: "numeroItem",
                            summaryType: "count",
                            displayFormat: "Cantidad Total",
                            showInColumn: "numeroItem",
                            customizeText: function (e) {
                                if (e.value != 0 && e.value != "") {
                                    return "Totales: ";
                                }
                            }
                        }
                        ,
                        {
                            name: "Quantity",
                            column: "Quantity",
                            summaryType: "sum",
                            displayFormat: "Cantidad Total",
                            showInColumn: "Quantity",
                            customizeText: function (e) {
                                if (e.value != 0 && e.value != "") {
                                    const noTruncarDecimales = { maximumFractionDigits: 0, minimumFractionDigits: 0 };
                                    ValTotal = (e.value).toLocaleString('en-US', noTruncarDecimales);
                                    return ValTotal;
                                }
                            }
                        }
                        , {
                            column: "TotalPrice",
                            summaryType: "sum",
                            showInColumn: "TotalPrice",
                            displayFormat: "Total: {0}",
                            valueFormat: "currency",
                            customizeText: function (e) {
                                if (e.value != 0 && e.value != "") {
                                    const noTruncarDecimales = { maximumFractionDigits: 2, minimumFractionDigits: 2 };
                                    ValTotal = (e.value).toLocaleString('en-US', noTruncarDecimales);
                                    return ValTotal;
                                }
                            },

                        },
                    ],
                },
            });
        },
        error: function (msg) {
            $("#MensajeErrorGeneral").show('fade');
            setTimeout(function () {
                $("#MensajeErrorGeneral").fadeOut(1500);
            }, 3000);
        }
    })
}