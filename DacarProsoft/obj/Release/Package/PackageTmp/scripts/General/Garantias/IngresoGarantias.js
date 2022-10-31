var progreso = 0;
var idIterval;
$(document).ready(function () {
    $("#cargaImg").hide();
    $(".loading-icon").css("display", "none");

    $("#txtMsjGarantia").hide();
    $('.js-example-basic-single').select2();
    ocultarDiv();
});

$('#myModal').modal({
    backdrop: 'static',
    keyboard: false,
    show: false
});

var extensionesValidas = ".png, .gif, .jpeg, .jpg";
var pesoPermitido = 1024;
var ValiExt = "False";
var ValiPes = "False";
var valorIdNumeroRevision = null;
var AplicaGarantia = false;
var IngresoManual = false;

function ocultarDiv() {
    $(".loading-icon").css("display", "none");
    //$("#ContenidoDiv").hide();
}

function ConsultaDeVentas() {
    $(".result").text("");
    $(".loading-icon").removeClass("hide");
    $(".btn").attr("disabled", true);
    $(".btn-txt").text("Espere...");
    ConsultarGarantia();
}

function cargarClientes() {

    var valor = $("#txtCliente option:selected").val();
    console.log(valor);
    $("#txtCedula").val(valor);
}

$('#btnprueba').on("click", function (e) {
});


$('#LinkClose4').on("click", function (e) {
    $("#MensajeGuardadoExitoso").hide('fade');
});
$('#LinkClose').on("click", function (e) {
    $("#MensajeErrorGeneral").hide('fade');
});
$('#LinkClose2').on("click", function (e) {
    $("#MensajeIngreseNumeroGarantia").hide('fade');
});
$('#LinkClose3').on("click", function (e) {
    $("#MensajeGarantiaNoEncontrada").hide('fade');
});

$('#PesoImgSobrepasado').on("click", function (e) {
    $("#LinkMjsPesoSobrepasadot").hide('fade');
});

$('#ArchivoImagenIncorrecto').on("click", function (e) {
    $("#LinkMjsArchivoIncorrecto").hide('fade');
});

$('#LinkClose5').on("click", function (e) {
    $("#MensajeCompleteCampos").hide('fade');
});

$('#LinkMjsNoCumpleGarantia').on("click", function (e) {
    $("#NoCumpleParaGarantia").hide('fade');
});

$('#LinkMjsCumpleGarantia').on("click", function (e) {
    $("#CumpleParaGarantia").hide('fade');
});

function CargarCantones() {
    if ($("#txtProvincia").val() != null && $("#txtProvincia").val() != "") {
        $("#txtDireccion").empty();
        $("#txtDireccion").append('<option value="">--Seleccione el cantón--</option>');
        $.ajax({
            type: 'POST',
            url: "../Garantias/CantonesEcuador",
            dataType: 'json',
            data: { id: $("#txtProvincia").val() },
            success: function (articulos) {
                $.each(articulos, function (i, articulo) {
                    $("#txtDireccion").append('<option value="' + articulo.Value + '">' +
                        articulo.Text + '</option>');
                });
            },
        })
    } else {
        $("#txtDireccion").empty();
        $("#txtDireccion").append('<option value="">--Seleccione el cantón--</option>');
    }
}

function ConsultarGarantia() {
    var numeroGarantia = document.getElementById('txtNumeroGarantia').value;
    if (numeroGarantia.length == 0) {
        $(".btn").attr("disabled", false);
        $(".btn-txt").text("Consultar");
        $("#MensajeIngreseNumeroGarantia").show('fade');
        setTimeout(function () {
            $("#MensajeIngreseNumeroGarantia").fadeOut(1500);
        }, 3000);
        return;
    }
    ConsultarNumeroGarantia();   
}

function CargarModelosBaterias() {
        $("#txtModelo").empty();
        $("#txtModelo").append('<option value="">--Escoja--</option>');
        $.ajax({
            type: 'POST',
            url: "../Garantias/ConsultarModelosBaterias",
            dataType: 'json',
            data: { id: $("#txtMarcasPropias option:selected").val() },
            success: function (articulos) {
                $.each(articulos, function (i, articulo) {
                    $("#txtModelo").append('<option value="' + articulo.Value + '">' +
                        articulo.Text + '</option>');
                });
            },
        }) 
}
function CargarProvincia() {
    $("#txtProvincia").empty();
    $("#txtProvincia").append('<option value="">--Escoja--</option>');
    $.ajax({
        type: 'POST',
        url: "../Garantias/ConsultarProvincias",
        dataType: 'json',
        data: { id: $("#txtMarcasPropias option:selected").val() },
        success: function (Meses) {
            $.each(Meses, function (i, mese) {
                $("#txtProvincia").append('<option value="' + mese.Value + '">' +
                    mese.Text + '</option>');
            });
        },
    })
}

function CargarNumeroComprobante() {
  
    $.ajax({
        type: 'POST',
        url: "../Garantias/ObtenerNumeroComprobante",
        success: function (msg) {
            document.getElementById("txtNumeroComprobante").readOnly = true;
            $("#txtNumeroComprobante").val(msg);
        },
    })
}

function ConsultarNumeroGarantia() {
    IngresoManual = false;
    $.ajax({
        url: "../Garantias/ConsultarNumeroGarantia",
        type: "POST",
        data: {
            numero: $("#txtNumeroGarantia").val()
        }, success: function (msg) {
            
            if (Object.keys(msg).length === 0) {

                IngresoManual = true;
                document.getElementById("txtModelo").disabled = false;
                document.getElementById("txtProvincia").disabled = false;
                document.getElementById("txtDireccion").disabled = false;
                document.getElementById("txtCliente").disabled = false;


                $("#txtCedula").val("");
                $("#txtProvincia").val("");

                CargarCantones();
                $("#txtNumeroGarantiaObtenido").val("");
                $("#txtNumeroFactura").val("");
                $("#txtPorcentajeVentas").val("");


                CargarProvincia();
                CargarModelosBaterias();
                $("#MensajeGarantiaNoEncontrada").show('fade');
                setTimeout(function () {
                    $("#MensajeGarantiaNoEncontrada").fadeOut(1500);
                }, 3000);

                document.getElementById("txtCliente").readOnly = false;
                document.getElementById("txtCedula").readOnly = true;
                document.getElementById("txtProvincia").readOnly = false;
                document.getElementById("txtNumeroGarantiaObtenido").readOnly = false;
                document.getElementById("txtNumeroFactura").readOnly = false;
                document.getElementById("txtPorcentajeVentas").readOnly = true;

                document.getElementById("OcultarContenidoDiv").style.display = ""; 	   //show
                //$("#ContenidoDiv").show();

            } else {
                IngresoManual = false;

                $("#txtModelo").empty();
                $("#txtProvincia").empty();
                $("#txtDireccion").empty();
                $("#txtCliente").empty();

                document.getElementById("txtCliente").readOnly = true;
                document.getElementById("txtCedula").readOnly = true;
                document.getElementById("txtProvincia").readOnly = true;
                document.getElementById("txtNumeroGarantiaObtenido").readOnly = true;
                document.getElementById("txtNumeroFactura").readOnly = true;
                document.getElementById("txtPorcentajeVentas").readOnly = true;

                $("#txtModelo").append('<option value="' + 1 + '">' +
                    msg[0]['ModeloBateria'] + '</option>');
                document.getElementById("txtModelo").disabled = true;

                $("#txtProvincia").append('<option value="' + 1 + '">' +
                    msg[0]['Provincia'] + '</option>');
                document.getElementById("txtProvincia").disabled = true;

                $("#txtDireccion").append('<option value="' + 1 + '">' +
                    msg[0]['Ciudad'] + '</option>');
                document.getElementById("txtDireccion").disabled = true;

                $("#txtCliente").append('<option value="' + 1 + '">' +
                    msg[0]['Nombre'] + " " + msg[0]['Apellido'] + '</option>');
                document.getElementById("txtCliente").disabled = true;


                document.getElementById("OcultarContenidoDiv").style.display = ""; 	   //show
               // $("#ContenidoDiv").show();
                
                $("#txtCedula").val(msg[0]['Cedula']);
                $("#txtNumeroGarantiaObtenido").val(msg[0]['NumeroGarantia']);
                $("#txtNumeroFactura").val(msg[0]['NumeroFactura']);
                $("#txtPorcentajeVentas").val(msg[0]['ValorBateria'].toFixed(2));

                if (msg[0]['ValorBateria']==0) {
                    document.getElementById("txtPorcentajeVentas").readOnly = false;
                }

            }
        },
        error: function (msg) {
            $("#MensajeErrorGeneral").show('fade');
            setTimeout(function () {
                $("#MensajeErrorGeneral").fadeOut(1500);
            }, 3000);
        }
    })
    $(".btn").attr("disabled", false);
    $(".btn-txt").text("Consultar");
}

function validarIngresos() {
    var valor2 =$("#txtCedula").val();
    var valor3 =$("#txtNumeroGarantiaObtenido").val();
    var valor4 =$("#txtNumeroComprobante").val();
    var valor5 = $("#txtNumeroFactura").val();
    var valor10 =$("#txtLote").val();
    var valor11 =$("#txtProrrateo").val();
    var valor12 = $("#txtMeses").val();
    var valor13 =$("#txtFechaVenta").val();
    var valor14 =$("#txtPorcentajeVentas").val();
    var valor15 =$("#txtVoltaje").val();
    var valor16 =$("#txtCelda1").val();
    var valor17 =$("#txtCelda2").val();
    var valor18 =$("#txtCelda3").val();
    var valor19 =$("#txtCelda4").val();
    var valor20 =$("#txtCelda5").val();
    var valor21 =$("#txtCelda6").val();
    var valor22 = $("#txtCca").val();
    var valor23 = $("#txtNumeroGarantia").val();
    var valor24 = $("#txtLoteEnsamble").val();

    var inputFileImage1 = $("#ImgFacturaIngresada")[0].files[0];
    var inputFileImage2 = $("#ImgTestIngresada")[0].files[0];

    if ($("#txtModelo option:selected").text() == "--Escoja--") {
        $("#MensajeCompleteCampos").show('fade');
        setTimeout(function () {
            $("#MensajeCompleteCampos").fadeOut(1500);
        }, 3000);
        return;
    }
    if ($("#txtCliente option:selected").text() == "--Seleccione el cliente--") {
        $("#MensajeCompleteCampos").show('fade');
        setTimeout(function () {
            $("#MensajeCompleteCampos").fadeOut(1500);
        }, 3000);
        return;
    }
    if ($("#txtProvincia option:selected").text() == "--Escoja--") {
        $("#MensajeCompleteCampos").show('fade');
        setTimeout(function () {
            $("#MensajeCompleteCampos").fadeOut(1500);
        }, 3000);
        return;
    }

    if ($("#txtDireccion option:selected").text() == "--Seleccione el cantón--") {
        $("#MensajeCompleteCampos").show('fade');
        setTimeout(function () {
            $("#MensajeCompleteCampos").fadeOut(1500);
        }, 3000);
        return;
    }
    if (valor2.length == 0) {
        $("#MensajeCompleteCampos").show('fade');
        setTimeout(function () {
            $("#MensajeCompleteCampos").fadeOut(1500);
        }, 3000);
        return;
    } if (valor3.length == 0) {
        $("#MensajeCompleteCampos").show('fade');
        setTimeout(function () {
            $("#MensajeCompleteCampos").fadeOut(1500);
        }, 3000);
        return;
    } if (valor4.length == 0) {
        $("#MensajeCompleteCampos").show('fade');
        setTimeout(function () {
            $("#MensajeCompleteCampos").fadeOut(1500);
        }, 3000);
        return;
    } if (valor5.length == 0) {
        $("#MensajeCompleteCampos").show('fade');
        setTimeout(function () {
            $("#MensajeCompleteCampos").fadeOut(1500);
        }, 3000);
        return;
       
    } 
    if (valor10.length == 0) {
        $("#MensajeCompleteCampos").show('fade');
        setTimeout(function () {
            $("#MensajeCompleteCampos").fadeOut(1500);
        }, 3000);
        return;
    } if (valor11.length == 0) {
        $("#MensajeCompleteCampos").show('fade');
        setTimeout(function () {
            $("#MensajeCompleteCampos").fadeOut(1500);
        }, 3000);
        return;
    } if (valor12.length == 0) {
        $("#MensajeCompleteCampos").show('fade');
        setTimeout(function () {
            $("#MensajeCompleteCampos").fadeOut(1500);
        }, 3000);
        return;
    } if (valor13.length == 0) {
        $("#MensajeCompleteCampos").show('fade');
        setTimeout(function () {
            $("#MensajeCompleteCampos").fadeOut(1500);
        }, 3000);
        return;
    } if (valor14.length == 0) {
        $("#MensajeCompleteCampos").show('fade');
        setTimeout(function () {
            $("#MensajeCompleteCampos").fadeOut(1500);
        }, 3000);
        return;
    } if (valor15.length == 0) {
        $("#MensajeCompleteCampos").show('fade');
        setTimeout(function () {
            $("#MensajeCompleteCampos").fadeOut(1500);
        }, 3000);
        return;
    }
    //if (valor23.length == 0) {
    //    $("#MensajeCompleteCampos").show('fade');
    //    setTimeout(function () {
    //        $("#MensajeCompleteCampos").fadeOut(1500);
    //    }, 3000);
    //    return;
    //}
    if (valor24.length == 0) {
        $("#MensajeCompleteCampos").show('fade');
        setTimeout(function () {
            $("#MensajeCompleteCampos").fadeOut(1500);
        }, 3000);
        return;
    }
    //if (inputFileImage1.length == 0) {
    //        $("#MensajeCompleteCampos").show('fade');
    //        setTimeout(function () {
    //            $("#MensajeCompleteCampos").fadeOut(1500);
    //        }, 3000);
    //        return;
    //}
    if ($('input[name=inlineRadioOptions1]:checked').length == 0) {
        $("#MensajeCompleteCampos").show('fade');
        setTimeout(function () {
            $("#MensajeCompleteCampos").fadeOut(1500);
        }, 3000);
        return;
    }
    if ($('input[name=inlineRadioOptions2]:checked').length == 0) {
        $("#MensajeCompleteCampos").show('fade');
        setTimeout(function () {
            $("#MensajeCompleteCampos").fadeOut(1500);
        }, 3000);
        return;
    } if ($('input[name=inlineRadioOptions3]:checked').length == 0) {
        $("#MensajeCompleteCampos").show('fade');
        setTimeout(function () {
            $("#MensajeCompleteCampos").fadeOut(1500);
        }, 3000);
        return;
    } if ($('input[name=inlineRadioOptions4]:checked').length == 0) {
        $("#MensajeCompleteCampos").show('fade');
        setTimeout(function () {
            $("#MensajeCompleteCampos").fadeOut(1500);
        }, 3000);
        return;
    } if ($('input[name=inlineRadioOptions5]:checked').length == 0) {
        $("#MensajeCompleteCampos").show('fade');
        setTimeout(function () {
            $("#MensajeCompleteCampos").fadeOut(1500);
        }, 3000);
        return;
    } if ($('input[name=inlineRadioOptions6]:checked').length == 0) {
        $("#MensajeCompleteCampos").show('fade');
        setTimeout(function () {
            $("#MensajeCompleteCampos").fadeOut(1500);
        }, 3000);
        return;
    } if ($('input[name=inlineRadioOptions7]:checked').length == 0) {
        $("#MensajeCompleteCampos").show('fade');
        setTimeout(function () {
            $("#MensajeCompleteCampos").fadeOut(1500);
        }, 3000);
        return;
    } if ($('input[name=inlineRadioOptions8]:checked').length == 0) {
        $("#MensajeCompleteCampos").show('fade');
        setTimeout(function () {
            $("#MensajeCompleteCampos").fadeOut(1500);
        }, 3000);
        return;
    }
    if ($('input[name=inlineRadioOptions10]:checked').length == 0) {
        $("#MensajeCompleteCampos").show('fade');
        setTimeout(function () {
            $("#MensajeCompleteCampos").fadeOut(1500);
        }, 3000);
        return;
    } if ($('input[name=inlineRadioOptions11]:checked').length == 0) {
        $("#MensajeCompleteCampos").show('fade');
        setTimeout(function () {
            $("#MensajeCompleteCampos").fadeOut(1500);
        }, 3000);
        return;
    } if ($('input[name=inlineRadioOptions12]:checked').length == 0) {
        $("#MensajeCompleteCampos").show('fade');
        setTimeout(function () {
            $("#MensajeCompleteCampos").fadeOut(1500);
        }, 3000);
        return;
    } if ($('input[name=inlineRadioOptions13]:checked').length == 0) {
        $("#MensajeCompleteCampos").show('fade');
        setTimeout(function () {
            $("#MensajeCompleteCampos").fadeOut(1500);
        }, 3000);
        return;
    } if ($('input[name=inlineRadioOptions14]:checked').length == 0) {
        $("#MensajeCompleteCampos").show('fade');
        setTimeout(function () {
            $("#MensajeCompleteCampos").fadeOut(1500);
        }, 3000);
        return;
    } if ($('input[name=inlineRadioOptions15]:checked').length == 0) {
        $("#MensajeCompleteCampos").show('fade');
        setTimeout(function () {
            $("#MensajeCompleteCampos").fadeOut(1500);
        }, 3000);
        return;
    } if ($('input[name=inlineRadioOptions16]:checked').length == 0) {
        $("#MensajeCompleteCampos").show('fade');
        setTimeout(function () {
            $("#MensajeCompleteCampos").fadeOut(1500);
        }, 3000);
        return;
    } if ($('input[name=inlineRadioOptions17]:checked').length == 0) {
        $("#MensajeCompleteCampos").show('fade');
        setTimeout(function () {
            $("#MensajeCompleteCampos").fadeOut(1500);
        }, 3000);
        return;
    }
    RegistrarRevisionGarantiaCabecera();
}

$('#btnRegistrarRevision').on("click", function (e) {
        if (document.getElementById("ImgFacturaIngresada").files.length == 0 || document.getElementById("ImgTestIngresada").files.length == 0) {
            validarIngresos();

            //$("#MensajeCompleteCampos").show('fade');
            //setTimeout(function () {
            //    $("#MensajeCompleteCampos").fadeOut(1500);
            //}, 3000);
            //return;
        }
        else {
            if (ValiExt == "True") {
                if (ValiPes == "True") {
                    validarIngresos();
                }
                else {
                    $("#PesoImgSobrepasado").show('fade');
                    setTimeout(function () {
                        $("#PesoImgSobrepasado").fadeOut(1500);
                    }, 3000); return;
                }
            } else {
                $("#ArchivoImagenIncorrecto").show('fade');
                setTimeout(function () {
                    $("#ArchivoImagenIncorrecto").fadeOut(1500);
                }, 3000); return;
            }
        }
});

function ValidarTipoImagen(obj) {
    validarExtension(obj);
    validarPeso(obj);
}

function validarExtension(datos) {
    ValiExt = "False";

    var ruta = datos.value;
    var extension = ruta.substring(ruta.lastIndexOf('.') + 1).toLowerCase();
    var extensionValida = extensionesValidas.indexOf(extension);

    if (extensionValida < 0) {
        ValiExt = "False";
    } else {
        ValiExt = "True";
    }
}

// Validacion de peso del fichero en kbs
function validarPeso(datos) {
    ValiPes = "False";
    if (datos.files && datos.files[0]) {
        var pesoFichero = datos.files[0].size / 1024;
        if (pesoFichero > pesoPermitido) {
            ValiPes = "False";

        } else {
            ValiPes = "True";
        }
    }
}

function RegistrarRevisionGarantiaCabecera() {
    //$("#pleaseWaitDialog").modal("show");
    //progreso = 0;
    //idIterval = setInterval(function () {
    //    // Aumento en 5 el progeso
    //    progreso += 6;
    //    $('#BarraProceso').css('width', progreso + '%');
    //}, 1000);
 
    var inputFileImage1 = $("#ImgFacturaIngresada")[0].files[0];
    var inputFileImage2 = $("#ImgTestIngresada")[0].files[0];

    var formdata = new FormData();
    formdata.append("cliente", $("#txtCliente option:selected").text());
    formdata.append("cedula", $("#txtCedula").val());
    formdata.append("numeroGarantia", $("#txtNumeroGarantiaObtenido").val());
    formdata.append("numeroComprobante", $("#txtNumeroComprobante").val());
    formdata.append("numeroFactura", $("#txtNumeroFactura").val());
    formdata.append("provincia", $("#txtProvincia option:selected").text());
    formdata.append("direccion", $("#txtDireccion option:selected").text());
    formdata.append("vendedor", $("#txtVendedor option:selected").text());
    formdata.append("ImgFac", inputFileImage1);
    formdata.append("marca", $("#txtMarca").val());
    formdata.append("modelo", $("#txtModelo option:selected").text());
    formdata.append("lote", $("#txtLote").val());
    formdata.append("prorrateo", $("#txtProrrateo").val());
    formdata.append("meses", $("#txtMeses").val());
    formdata.append("fechaVenta", $("#txtFechaVenta").val());
    formdata.append("fechaIngreso", $("#txtFechaIngreso").val());
    formdata.append("porcentajeVenta", $("#txtPorcentajeVentas").val());
    formdata.append("voltaje", $("#txtVoltaje").val());
    formdata.append("loteEnsamble", $("#txtLoteEnsamble").val());
    formdata.append("ImgTest", inputFileImage2);

    $.ajax({
        url: '../Garantias/RegistrarRevisionDeGarantiaCabecera',
        data: formdata,
        type: 'post',
        processData: false,
        contentType: false,
        success: function (respuesta) {
            valorIdNumeroRevision = respuesta;         
            if (valorIdNumeroRevision == "0") {
                $("#MensajeErrorGeneral").show('fade');
                setTimeout(function () {
                    $("#MensajeErrorGeneral").fadeOut(1500);
                }, 3000); return;
            } else {
                RegistrarRevisionGarantiaDetalle(valorIdNumeroRevision);
            }
        },
        error: function () {
            $("#MensajeErrorGeneral").show('fade');
            setTimeout(function () {
                $("#MensajeErrorGeneral").fadeOut(1500);
            }, 3000); return;        }
    });
}

function RegistrarRevisionGarantiaDetalle(valor) {
    AplicaGarantia = false;
    var celda1 = null;
    var celda2 = null;
    var celda3 = null;
    var celda4 = null;
    var celda5 = null;
    var celda6 = null;
    var cca = null;

    if ($('input:radio[name=inlineRadioOptions17]:checked').val() == "false") {
        AplicaGarantia = true;
    }
    if ($('input:radio[name=inlineRadioOptions10]:checked').val() == "true") {
        AplicaGarantia = true;
    }
    if ($('input:radio[name=inlineRadioOptions11]:checked').val() == "true") {
        AplicaGarantia = true;
    }
    if ($('input:radio[name=inlineRadioOptions12]:checked').val() == "true") {
        AplicaGarantia = true;
    }
    if ($('input:radio[name=inlineRadioOptions13]:checked').val() == "true") {
        AplicaGarantia = true;
    }
    if ($('input:radio[name=inlineRadioOptions14]:checked').val() == "true") {
        AplicaGarantia = true;
    }

    if ($("#txtCelda1").val() == "") {
        celda1 = "0";
    }
    else {
        celda1 = $("#txtCelda1").val();
    }
    if ($("#txtCelda2").val() == "") {
        celda2 = "0";
    }
    else {
        celda2 = $("#txtCelda2").val();
    }
    if ($("#txtCelda3").val() == "") {
        celda3 = "0";
    }
    else {
        celda3 = $("#txtCelda3").val();
    }
    if ($("#txtCelda4").val() == "") {
        celda4 = "0";
    }
    else {
        celda4 = $("#txtCelda4").val();
    }
    if ($("#txtCelda5").val() == "") {
        celda5 = "0";
    }
    else {
        celda5 = $("#txtCelda5").val();
    }
    if ($("#txtCelda6").val() == "") {
        celda6 = "0";
    }
    else {
        celda6 = $("#txtCelda6").val();
    }
    if ($("#txtCca").val() == "") {
        cca = "0";
    }
    else {
        cca = $("#txtCca").val();
    }

    $.ajax({
        url: '../Garantias/RegistrarRevisionDeGarantiaDetalle',
        data: {
            RevisionId: valor, InGolpeadaoRota: $('input:radio[name=inlineRadioOptions10]:checked').val(), InHinchada: $('input:radio[name=inlineRadioOptions11]:checked').val(), InBornesFlojos: $('input:radio[name=inlineRadioOptions12]:checked').val(), InBornesFundidos: $('input:radio[name=inlineRadioOptions13]:checked').val(), IngElectrolito: $('input:radio[name=inlineRadioOptions14]:checked').val(), InFugaEnCubierta: $('input:radio[name=inlineRadioOptions15]:checked').val(),
            InFugaEnBornes: $('input:radio[name=inlineRadioOptions16]:checked').val(), InRevisionesPeriodicas: $('input:radio[name=inlineRadioOptions17]:checked').val(), InDcC1: celda1, InDcC2: celda2, InDcC3: celda3, InDcC4: celda4, InDcC5: celda5, InDcC6: celda6,
            InCCA: cca, TrPruebaAltaResistencia: $('input:radio[name=inlineRadioOptions1]:checked').val(), TrCambioAcido: $('input:radio[name=inlineRadioOptions2]:checked').val(), TrRecargaBateria: $('input:radio[name=inlineRadioOptions3]:checked').val(), TrInspeccionEstructuraExt: $('input:radio[name=inlineRadioOptions4]:checked').val(), DBateriaBuenEstado: $('input:radio[name=inlineRadioOptions5]:checked').val(), DPresentaFallosFabricacion: $('input:radio[name=inlineRadioOptions6]:checked').val(),
            DDentroPeriodo: $('input:radio[name=inlineRadioOptions7]:checked').val(), DUsoAdecuado: $('input:radio[name=inlineRadioOptions8]:checked').val(), /*DAplicaGarantia: $('input:radio[name=inlineRadioOptions9]:checked').val(),*/ AplicaGarantia: AplicaGarantia, IngresoManual: IngresoManual, Cliente: $("#txtCliente option:selected").text()
        },
        type: 'post',
        success: function (respuesta) {
            //clearInterval(idIterval);
            //$("#pleaseWaitDialog").modal("hide");
            //$("#ModalDetallePedido").modal("hide");
            document.getElementById("OcultarContenidoDiv").style.display = "none"; 	   //show

            if (respuesta == "True") {
                $("#txtMsjGarantia").hide();

                if (AplicaGarantia==true) {
                    $("#NoCumpleParaGarantia").show('fade');
                    setTimeout(function () {
                        $("#NoCumpleParaGarantia").fadeOut(1500);
                    }, 3000);
                }
                else {
                    $("#txtMsjGarantia").hide();

                    $("#CumpleParaGarantia").show('fade');
                    setTimeout(function () {
                        $("#CumpleParaGarantia").fadeOut(1500);
                    }, 3000);
                }

                $("#MensajeGuardadoExitoso").show('fade');
                setTimeout(function () {
                    $("#MensajeGuardadoExitoso").fadeOut(1500);
                }, 3000);    
                if (IngresoManual == false) {
                    location.reload();
                    ocultarDiv();
                    vaciarInputs();
                }
                else {
                    ocultarDiv();
                    vaciarInputs();

                }

            } else {
                $("#MensajeErrorGeneral").show('fade');
                setTimeout(function () {
                    $("#MensajeErrorGeneral").fadeOut(1500);
                }, 3000); return;
            }
        },
        error: function () {
            $("#MensajeErrorGeneral").show('fade');
            setTimeout(function () {
                $("#MensajeErrorGeneral").fadeOut(1500);
            }, 3000); return;        }
    });
}

function vaciarInputs() {
    var inputImage = document.getElementById("ImgFacturaIngresada");
    inputImage.value = '';
    var inputImage2 = document.getElementById("ImgTestIngresada");
    inputImage2.value = '';
    $("#txtCedula").val("");
    $("#txtNumeroGarantiaObtenido").val("");
    $("#txtNumeroComprobante").val("");
    $("#txtNumeroFactura").val("");
    $("#txtLote").val("");
    $("#txtLoteEnsamble").val("");
    $("#txtProrrateo").val("");
    $("#txtMeses").val("");
    $("#txtFechaVenta").val("");
    $("#txtPorcentajeVentas").val("");
    $("#txtVoltaje").val("");
    $("#txtCelda1").val("");
    $("#txtCelda2").val("");
    $("#txtCelda3").val("");
    $("#txtCelda4").val("");
    $("#txtCelda5").val("");
    $("#txtCelda6").val("");
    $("#txtCca").val("");
    $("#txtNumeroGarantia").val("");

    document.querySelectorAll('[name=inlineRadioOptions1]').forEach((x) => x.checked = false);
    document.querySelectorAll('[name=inlineRadioOptions2]').forEach((x) => x.checked = false);
    document.querySelectorAll('[name=inlineRadioOptions3]').forEach((x) => x.checked = false);
    document.querySelectorAll('[name=inlineRadioOptions4]').forEach((x) => x.checked = false);
    document.querySelectorAll('[name=inlineRadioOptions5]').forEach((x) => x.checked = false);
    document.querySelectorAll('[name=inlineRadioOptions6]').forEach((x) => x.checked = false);
    document.querySelectorAll('[name=inlineRadioOptions7]').forEach((x) => x.checked = false);
    document.querySelectorAll('[name=inlineRadioOptions8]').forEach((x) => x.checked = false);
    document.querySelectorAll('[name=inlineRadioOptions10]').forEach((x) => x.checked = false);
    document.querySelectorAll('[name=inlineRadioOptions11]').forEach((x) => x.checked = false);
    document.querySelectorAll('[name=inlineRadioOptions12]').forEach((x) => x.checked = false);
    document.querySelectorAll('[name=inlineRadioOptions13]').forEach((x) => x.checked = false);
    document.querySelectorAll('[name=inlineRadioOptions14]').forEach((x) => x.checked = false);
    document.querySelectorAll('[name=inlineRadioOptions15]').forEach((x) => x.checked = false);
    document.querySelectorAll('[name=inlineRadioOptions16]').forEach((x) => x.checked = false);
    document.querySelectorAll('[name=inlineRadioOptions17]').forEach((x) => x.checked = false);
}

function ValidarGarantia() {
    if ($('input:radio[name=inlineRadioOptions17]:checked').val() != null && $('input:radio[name=inlineRadioOptions10]:checked').val() != null && $('input:radio[name=inlineRadioOptions11]:checked').val() != null
        && $('input:radio[name=inlineRadioOptions12]:checked').val() != null && $('input:radio[name=inlineRadioOptions13]:checked').val() != null && $('input:radio[name=inlineRadioOptions14]:checked').val() != null) {
        $("#txtMsjGarantia").show();
        if ($('input:radio[name=inlineRadioOptions17]:checked').val() == "true" && $('input:radio[name=inlineRadioOptions10]:checked').val() == "false" && $('input:radio[name=inlineRadioOptions11]:checked').val() == "false" &&
            $('input:radio[name=inlineRadioOptions12]:checked').val() == "false" && $('input:radio[name=inlineRadioOptions13]:checked').val() == "false" && $('input:radio[name=inlineRadioOptions14]:checked').val() == "false") {
            console.log("entro x verdadero");
            var elem1 = document.getElementById("txtMsjGarantia");
            elem1.style.backgroundColor = "MediumSeaGreen";
        $("#txtMsjGarantia").val("El producto cumple con los requisitos minimos para aplicar garantia.");    
        }
        else {
            var elem1 = document.getElementById("txtMsjGarantia");
            elem1.style.backgroundColor = "LemonChiffon";

            $("#txtMsjGarantia").val("El producto no cumple con los requisitos minimos para aplicar garantia.");
         }  
    }
    else {
        $("#txtMsjGarantia").hide();
    }
}
function CalcularProrrateo() {
    ValorBateria();

    if ($("#txtPorcentajeVentas").val() != "" && $("#txtFechaVenta").val() != "" && $("#txtModelo option:selected").val() != "") {
        $.ajax({
            type: 'POST',
            url: "../Garantias/ConsultarProrrateo",
            dataType: 'json',
            data: {
                MarcaPropiasId: $("#txtModelo option:selected").val(), MarcaPropiasTexto: $("#txtModelo option:selected").text(), PvpVentas: $("#txtPorcentajeVentas").val(), FechaIngreso: $("#txtFechaIngreso").val()
                , FechaVenta: $("#txtFechaVenta").val()},
            success: function (msg) {
                $("#txtProrrateo").val(msg[0]['PorcentajeProrrateo']);
                $("#txtMeses").val(msg[0]['MesesGarantia']);
            },
        })
    }
    else {
        console.log("entro x falso");
    }
}

function ValorBateria() {
    if ($("#txtModelo option:selected").val() != "") {
        $.ajax({
            type: 'POST',
            url: "../Garantias/ConsultarValorBateria",
            dataType: 'json',
            data: {
                MarcaPropiasTexto: $("#txtModelo option:selected").text()
            },
            success: function (msg) {

                if (msg == 0) {
                    document.getElementById("txtPorcentajeVentas").readOnly = false;
                } else {
                    document.getElementById("txtPorcentajeVentas").readOnly = true;
                }

                $("#txtPorcentajeVentas").val(msg.toFixed(2));
            },
        })
    }
}

