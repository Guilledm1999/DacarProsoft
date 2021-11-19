﻿
$(document).ready(function () {
    ocultarDiv();
    //var x = document.getElementById("ContenidoDiv");
    //x.style.display === "none";
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
    $("#ContenidoDiv").hide();
}

function ConsultaDeVentas() {
    $(".result").text("");
    $(".loading-icon").removeClass("hide");
    $(".btn").attr("disabled", true);
    $(".btn-txt").text("Espere...");
    ConsultarGarantia();
}


$('#btnprueba').on("click", function (e) {
    console.log($('input:radio[name=inlineRadioOptions10]:checked').val());
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
                CargarNumeroComprobante();
                document.getElementById("txtModelo").disabled = false;
                document.getElementById("txtProvincia").disabled = false;
                $("#txtCliente").val("");
                $("#txtCedula").val("");
                $("#txtProvincia").val("");
                $("#txtNumeroGarantiaObtenido").val("");
                //$("#txtModelo").val("");
                $("#txtNumeroFactura").val("");

                CargarProvincia();
                CargarModelosBaterias();
                $("#MensajeGarantiaNoEncontrada").show('fade');
                setTimeout(function () {
                    $("#MensajeGarantiaNoEncontrada").fadeOut(1500);
                }, 3000);

                document.getElementById("txtCliente").readOnly = false;
                document.getElementById("txtCedula").readOnly = false;
                document.getElementById("txtProvincia").readOnly = false;
                document.getElementById("txtNumeroGarantiaObtenido").readOnly = false;
                //document.getElementById("txtModelo").readOnly = false;
                document.getElementById("txtNumeroFactura").readOnly = false;
                $("#ContenidoDiv").show();

            } else {
                IngresoManual = false;

                $("#txtModelo").empty();
                $("#txtProvincia").empty();


                document.getElementById("txtCliente").readOnly = true;
                document.getElementById("txtCedula").readOnly = true;
                document.getElementById("txtProvincia").readOnly = true;
                document.getElementById("txtNumeroGarantiaObtenido").readOnly = true;
                //document.getElementById("txtModelo").readOnly = true;
                document.getElementById("txtNumeroFactura").readOnly = true;
                document.getElementById("txtNumeroComprobante").readOnly = true;

                $("#txtModelo").append('<option value="' + 1 + '">' +
                    msg[0]['ModeloBateria'] + '</option>');
                document.getElementById("txtModelo").disabled = true;

                $("#txtProvincia").append('<option value="' + 1 + '">' +
                    msg[0]['Provincia'] + '</option>');
                document.getElementById("txtProvincia").disabled = true;

                $("#ContenidoDiv").show();
                $("#txtCliente").val(msg[0]['Nombre'] + " " + msg[0]['Apellido']);
                $("#txtNumeroComprobante").val(msg[0]['NumeroCombrobante']);

                //$("#txtCliente").prop("disabled", true);
                $("#txtCedula").val(msg[0]['Cedula']);
                //$("#txtProvincia").val(msg[0]['Provincia']);
                $("#txtNumeroGarantiaObtenido").val(msg[0]['NumeroGarantia']);
                //$("#txtModelo").val(msg[0]['ModeloBateria']);
                $("#txtNumeroFactura").val(msg[0]['NumeroFactura']);
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
    var valor1=$("#txtCliente").val();
    var valor2 =$("#txtCedula").val();
    var valor3 =$("#txtNumeroGarantiaObtenido").val();
    var valor4 =$("#txtNumeroComprobante").val();
    var valor5 = $("#txtNumeroFactura").val();
    //var valor6 =$("#txtProvincia").val();
    var valor7 =$("#txtDireccion").val();
    //var valor8 =$("#txtVendedor").val();
    //var valor9 =$("#txtModelo").val();
    var valor10 =$("#txtLote").val();
    var valor11 =$("#txtProrrateo").val();
    var valor12 =$("#txtMeses").val();
    var valor13 =$("#txtFechaVenta").val();
    var valor14 =$("#txtPorcentajeVentas").val();
    var valor15 =$("#txtVoltaje").val();
    var valor16 =$("#txtCelda1").val();
    var valor17 =$("#txtCelda2").val();
    var valor18 =$("#txtCelda3").val();
    var valor19 =$("#txtCelda4").val();
    var valor20 =$("#txtCelda5").val();
    var valor21 =$("#txtCelda6").val();
    var valor22 =$("#txtCca").val();
    var valor23 =$("#txtNumeroGarantia").val();
    var inputFileImage1 = $("#ImgFacturaIngresada")[0].files[0];
    var inputFileImage2 = $("#ImgTestIngresada")[0].files[0];

    if ($("#txtModelo option:selected").text() == "--Escoja--") {
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
    
    if (valor1.length == 0) {
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
       
    } //if (valor6.length == 0) {
    //    $("#MensajeCompleteCampos").show('fade');
    //    setTimeout(function () {
    //        $("#MensajeCompleteCampos").fadeOut(1500);
    //    }, 3000);
    //    return;
    //}
    if (valor7.length == 0) {
        $("#MensajeCompleteCampos").show('fade');
        setTimeout(function () {
            $("#MensajeCompleteCampos").fadeOut(1500);
        }, 3000);
        return;
    }
    //if (valor8.length == 0) {
    //    $("#MensajeCompleteCampos").show('fade');
    //    setTimeout(function () {
    //        $("#MensajeCompleteCampos").fadeOut(1500);
    //    }, 3000);
    //    return;
    ////} if (valor9.length == 0) {
    ////    $("#MensajeCompleteCampos").show('fade');
    ////    setTimeout(function () {
    ////        $("#MensajeCompleteCampos").fadeOut(1500);
    ////    }, 3000);
    ////    return;
    //    //
    //}
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
    } if (valor16.length == 0) {
        $("#MensajeCompleteCampos").show('fade');
        setTimeout(function () {
            $("#MensajeCompleteCampos").fadeOut(1500);
        }, 3000);
        return;
    } if (valor17.length == 0) {
        $("#MensajeCompleteCampos").show('fade');
        setTimeout(function () {
            $("#MensajeCompleteCampos").fadeOut(1500);
        }, 3000);
        return;
    } if (valor18.length == 0) {
        $("#MensajeCompleteCampos").show('fade');
        setTimeout(function () {
            $("#MensajeCompleteCampos").fadeOut(1500);
        }, 3000);
        return;
    } if (valor19.length == 0) {
        $("#MensajeCompleteCampos").show('fade');
        setTimeout(function () {
            $("#MensajeCompleteCampos").fadeOut(1500);
        }, 3000);
        return;
    } if (valor20.length == 0) {
        $("#MensajeCompleteCampos").show('fade');
        setTimeout(function () {
            $("#MensajeCompleteCampos").fadeOut(1500);
        }, 3000);
        return;
    } if (valor21.length == 0) {
        $("#MensajeCompleteCampos").show('fade');
        setTimeout(function () {
            $("#MensajeCompleteCampos").fadeOut(1500);
        }, 3000);
        return;
    } if (valor22.length == 0) {
        $("#MensajeCompleteCampos").show('fade');
        setTimeout(function () {
            $("#MensajeCompleteCampos").fadeOut(1500);
        }, 3000);
        return;
    } if (valor23.length == 0) {
        $("#MensajeCompleteCampos").show('fade');
        setTimeout(function () {
            $("#MensajeCompleteCampos").fadeOut(1500);
        }, 3000);
        return;
    }
    if (inputFileImage1.length == 0) {
            $("#MensajeCompleteCampos").show('fade');
            setTimeout(function () {
                $("#MensajeCompleteCampos").fadeOut(1500);
            }, 3000);
            return;

    } if (inputFileImage2.length == 0) {
            $("#MensajeCompleteCampos").show('fade');
            setTimeout(function () {
                $("#MensajeCompleteCampos").fadeOut(1500);
            }, 3000);
            return;
    }
    
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
    //if ($('input[name=inlineRadioOptions9]:checked').length == 0) {
    //    $("#MensajeCompleteCampos").show('fade');
    //    setTimeout(function () {
    //        $("#MensajeCompleteCampos").fadeOut(1500);
    //    }, 3000);
    //    return;
    //}
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
        if (document.getElementById("ImgFacturaIngresada").files.length == 0 && document.getElementById("ImgTestIngresada").files.length == 0) {
            $("#MensajeCompleteCampos").show('fade');
            setTimeout(function () {
                $("#MensajeCompleteCampos").fadeOut(1500);
            }, 3000);
            return;
        }
        else {
            if (ValiExt == "True") {
                if (ValiPes == "True") {
                    validarIngresos();
                    //    RegistrarRevisionGarantiaCabecera();
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
    var inputFileImage1 = $("#ImgFacturaIngresada")[0].files[0];
    var inputFileImage2 = $("#ImgTestIngresada")[0].files[0];

    var formdata = new FormData();
    formdata.append("cliente", $("#txtCliente").val());
    formdata.append("cedula", $("#txtCedula").val());
    formdata.append("numeroGarantia", $("#txtNumeroGarantiaObtenido").val());
    formdata.append("numeroComprobante", $("#txtNumeroComprobante").val());
    formdata.append("numeroFactura", $("#txtNumeroFactura").val());
    formdata.append("provincia", $("#txtProvincia option:selected").text());
    formdata.append("direccion", $("#txtDireccion").val());
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
            alert("Ha ocurrido un error");
        }
    });
}

function RegistrarRevisionGarantiaDetalle(valor) {
    AplicaGarantia = false;

    if ($('input:radio[name=inlineRadioOptions17]:checked').val() == "true") {
        console.log("Entro check 1");
        AplicaGarantia = true;
    }
    if ($('input:radio[name=inlineRadioOptions10]:checked').val() == "true") {
        console.log("Entro check 2");
        AplicaGarantia = true;
    }
    if ($('input:radio[name=inlineRadioOptions11]:checked').val() == "true") {
        console.log("Entro check 3");
        AplicaGarantia = true;
    }
    if ($('input:radio[name=inlineRadioOptions12]:checked').val() == "true") {
        console.log("Entro check 4");
        AplicaGarantia = true;
    }
    if ($('input:radio[name=inlineRadioOptions13]:checked').val() == "true") {
        console.log("Entro check 5");
        AplicaGarantia = true;
    }
    if ($('input:radio[name=inlineRadioOptions14]:checked').val() == "true") {
        console.log("Entro check 6");
        AplicaGarantia = true;
    }

    $.ajax({
        url: '../Garantias/RegistrarRevisionDeGarantiaDetalle',
        data: {
            RevisionId: valor, InGolpeadaoRota: $('input:radio[name=inlineRadioOptions10]:checked').val(), InHinchada: $('input:radio[name=inlineRadioOptions11]:checked').val(), InBornesFlojos: $('input:radio[name=inlineRadioOptions12]:checked').val(), InBornesFundidos: $('input:radio[name=inlineRadioOptions13]:checked').val(), IngElectrolito: $('input:radio[name=inlineRadioOptions14]:checked').val(), InFugaEnCubierta: $('input:radio[name=inlineRadioOptions15]:checked').val(),
            InFugaEnBornes: $('input:radio[name=inlineRadioOptions16]:checked').val(), InRevisionesPeriodicas: $('input:radio[name=inlineRadioOptions17]:checked').val(), InDcC1: $("#txtCelda1").val(), InDcC2: $("#txtCelda2").val(), InDcC3: $("#txtCelda3").val(), InDcC4: $("#txtCelda4").val(), InDcC5: $("#txtCelda5").val(), InDcC6: $("#txtCelda6").val(),
            InCCA: $("#txtCca").val(), TrPruebaAltaResistencia: $('input:radio[name=inlineRadioOptions1]:checked').val(), TrCambioAcido: $('input:radio[name=inlineRadioOptions2]:checked').val(), TrRecargaBateria: $('input:radio[name=inlineRadioOptions3]:checked').val(), TrInspeccionEstructuraExt: $('input:radio[name=inlineRadioOptions4]:checked').val(), DBateriaBuenEstado: $('input:radio[name=inlineRadioOptions5]:checked').val(), DPresentaFallosFabricacion: $('input:radio[name=inlineRadioOptions6]:checked').val(),
            DDentroPeriodo: $('input:radio[name=inlineRadioOptions7]:checked').val(), DUsoAdecuado: $('input:radio[name=inlineRadioOptions8]:checked').val(), /*DAplicaGarantia: $('input:radio[name=inlineRadioOptions9]:checked').val(),*/ AplicaGarantia: AplicaGarantia, IngresoManual: IngresoManual
        },
        type: 'post',
        success: function (respuesta) {
            if (respuesta == "True") {

                if (AplicaGarantia==true) {
                    $("#NoCumpleParaGarantia").show('fade');
                    setTimeout(function () {
                        $("#NoCumpleParaGarantia").fadeOut(1500);
                    }, 3000);
                }
                else {
                    $("#CumpleParaGarantia").show('fade');
                    setTimeout(function () {
                        $("#CumpleParaGarantia").fadeOut(1500);
                    }, 3000);
                }

                $("#MensajeGuardadoExitoso").show('fade');
                setTimeout(function () {
                    $("#MensajeGuardadoExitoso").fadeOut(1500);
                }, 3000); 

                ocultarDiv();
                vaciarInputs();


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
    $("#txtCliente").val("");
    $("#txtCedula").val("");
    $("#txtNumeroGarantiaObtenido").val("");
    $("#txtNumeroComprobante").val("");
    $("#txtNumeroFactura").val("");
    //$("#txtProvincia").val("");
    $("#txtDireccion").val("");
    //$("#txtVendedor").val("");
    //$("#txtModelo").val("");
    $("#txtLote").val("");
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
    //document.querySelectorAll('[name=inlineRadioOptions9]').forEach((x) => x.checked = false);
    document.querySelectorAll('[name=inlineRadioOptions10]').forEach((x) => x.checked = false);
    document.querySelectorAll('[name=inlineRadioOptions11]').forEach((x) => x.checked = false);
    document.querySelectorAll('[name=inlineRadioOptions12]').forEach((x) => x.checked = false);
    document.querySelectorAll('[name=inlineRadioOptions13]').forEach((x) => x.checked = false);
    document.querySelectorAll('[name=inlineRadioOptions14]').forEach((x) => x.checked = false);
    document.querySelectorAll('[name=inlineRadioOptions15]').forEach((x) => x.checked = false);
    document.querySelectorAll('[name=inlineRadioOptions16]').forEach((x) => x.checked = false);
    document.querySelectorAll('[name=inlineRadioOptions17]').forEach((x) => x.checked = false);

}