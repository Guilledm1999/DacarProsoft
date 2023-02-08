

$(document).ready(function () {
    ConsultarArticulos();
});


function Menu() {
 
    $(document).on('click', '.nav-item', function () {
        //Dentro de este selector existe la etiqueta <a> la buscamos
        let a_tag = $(this).find('h6');
        let a_text = a_tag.text();
        console.log('Opcion de menu ' + a_text);

        $.ajax({
            url: "../Principal/RedirigirPagina?OpcionPrincipal=" + a_text,
            type: "GET"
      , success: function (msg) {

      },
            error: function (msg) { 
                alert("Error en la consulta", +msg);
            }

        })


    });



}

