

$(document).ready(function () {
   RevisarTiempoContrasena();
});

function RevisarTiempoContrasena(){
    $.ajax({
        url: "../Principal/RevisarDiasContrasena",
        type: "POST",
        data: {
            
            
        }, success: function (msg) {
            if (msg == true) {
                document.querySelector('#btnCerrarModalFooter').disabled = true;
                document.querySelector('#btnModalClose').disabled = true;
                $('#ModalCambioContraseniaUser').modal({ backdrop: 'static', keyboard: false })
                $("#ModalCambioContraseniaUser").modal('show');
                
            } 

        },

        error: function (msg) {
           
        }
    })
}

