
$(document).ready(function () {
    InicializarCalendario();
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
            console.log("info:" + JSON.stringify(info));

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
            console.log(msg);
        }
    })
    return res;
}