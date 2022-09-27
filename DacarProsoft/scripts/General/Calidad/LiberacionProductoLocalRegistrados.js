var packingId = null;
var palletId = null;
var encabezadoIdentificador = null;
var resTempo = null;
var modelTemp = null;
$(document).ready(function () {
    mostrarIngresosPallet();
});

$('#LinkClose5').on("click", function (e) {
    $("#MensajeGuardado").hide('fade');
});
$('#LinkClose6').on("click", function (e) {
    $("#MensajeErrorGuardado").hide('fade');
});
$('#LinkClose7').on("click", function (e) {
    $("#MensajeErrorGeneral").hide('fade');
});
$('#LinkClose10').on("click", function (e) {
    $("#MensajePackingSinDetalle").hide('fade');
});
$('#LinkClose11').on("click", function (e) {
    $("#MensajeEliminacionCorecta").hide('fade');
});
$('#LinkClose12').on("click", function (e) {
    $("#MensajeEliminacionIncorecta").hide('fade');
});
$('#LinkClose10').on("click", function (e) {
    $("#MensajeCompleteCampos").hide('fade');
});
$('#LinkClose11').on("click", function (e) {
    $("#MensajeGuardadoMedicion").hide('fade');
});
$('#LinkClose12').on("click", function (e) {
    $("#MensajeErrorGuardadoMedicion").hide('fade');
});
$('#LinkClose13').on("click", function (e) {
    $("#MensajeEliminacionCorectaMedicion").hide('fade');
});
$('#LinkClose14').on("click", function (e) {
    $("#MensajeErrorELiminacion").hide('fade');
});

function ConsultarIngresoMaximoMedicion() {
    var res;
    $.ajax({
        url: "../Calidad/NumeroMaximoMedicion",
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

function mostrarIngresosPallet() {
    $.ajax({
        url: "../Calidad/ObtenerPalletIngresadosLiberacionLocalRegistrados",
        type: "GET"
        , success: function (msg) {
            const locale = getLocale();
            DevExpress.localization.locale(locale);
            $("#tblIngresosdePacking").dxDataGrid({
                dataSource: msg,
                keyExpr: 'PackingId',
                allowColumnReordering: true,
                allowColumnResizing: true,
                columnAutoWidth: true,
                showBorders: true,
                paging: {
                    pageSize: 10
                },
                headerFilter: {
                    visible: true
                },
                pager: {
                    visible: true,
                    allowedPageSizes: [5, 10, 100],
                    showPageSizeSelector: true,
                    showInfo: true,
                    showNavigationButtons: true
                },
                searchPanel: {
                    visible: true,
                    placeholder: "Buscar..."
                },
                columns: [
                    { dataField: "EncabezadoPedidoLocal", visible: false },
                    {
                        dataField: "NumeroDocumento", caption: "# Secuencial Sap", allowEditing: false, allowHeaderFiltering: false
                    },
                    {
                        dataField: "NumeroOrden", caption: "Numero Orden", allowEditing: false, headerFilter: false, allowHeaderFiltering: false
                    },
                    {
                        dataField: "PackingId", caption: "Codigo", allowEditing: false, headerFilter: false, allowHeaderFiltering: false
                    },  
                    {
                        dataField: "NombreCliente", caption: "Cliente", allowEditing: false, headerFilter: true, allowHeaderFiltering: true
                    },
                    {
                        dataField: "NumeroContenedor", caption: "Cantidad Pallets", allowEditing: false, headerFilter: false, allowHeaderFiltering: false
                    },            
                    {
                        dataField: "cantidadMediciones", caption: "Cantidad Mediciones", allowEditing: false, headerFilter: false, allowHeaderFiltering: false
                    },
                    {
                        dataField: "FechaRegistro", caption: "Fecha Registro", allowEditing: false, allowHeaderFiltering: true, dataType: 'date',
                    },
                    {
                        caption: "Acciones", type: "buttons",
                        buttons: [{
                            text: "Liberar",
                            icon: "check",
                            type: 'success',
                            hint: "Liberar",
                            onClick: function (e) {    
                                ModalConfirmarLiberacion(e.row.data);
                            }
                        },
                        {
                            text: "Detalle",
                            icon: "menu",
                            hint: "Detalle",
                            onClick: function (e) {
                                ModalConsultarMedicionesPallets(e.row.data);
                            }
                        }]
                    }
                ],
            });
        },
        error: function (msg) {
            $("#MensajeErrorGeneral").show('fade');
            setTimeout(function () {
                $("#MensajeErrorGeneral").fadeOut(1500);
            }, 3000);
        }
    })
    function getLocale() {
        const storageLocale = sessionStorage.getItem('locale');
        return storageLocale != null ? storageLocale : 'es';
    }
}

function generarInformePackingListPDF(modelo) {    
    if (modelo.DetalleIngresado == "Incompleto" || modelo.Estado == "Incompleto") {
        $("#MensajePackingSinDetalle").show('fade');
        setTimeout(function () {
            $("#MensajePackingSinDetalle").fadeOut(1500);
        }, 3000);
    } else {
        idPacking = modelo.PackingId;
        $("#ModalAfirmacionLiberacion").modal("show");
    }
}

function ConfirmarLiberacion() {
    ActualizarEstadoPacking();
    $("#ModalAfirmacionLiberacion").modal("hide");
    $("#MensajeGuardado").show('fade');
    setTimeout(function () {
        $("#MensajeGuardado").fadeOut(1500);
    }, 3000);
}

function ModalConfirmarLiberacion(modelo) {
    $("#ModalAfirmacionLiberacion").modal("show");
    packingId = modelo.PackingId;
    resTempo = modelo;
}

function generarPDFPackingList(variable) {
    var url = "../Calidad/ImprimirLiberacionProducto?packingId=" + packingId;
    window.open(url);
    $("#ModalListadoDePallets").modal("hide");
}

function ModalConsultarMedicionesPallets(modelo) {
    encabezadoIdentificador = modelo.EncabezadoPedidoLocal;
    ConsultarMedicionPallet(modelo.EncabezadoPedidoLocal);
    $("#ModalListadoDePallets").modal("hide");
    $("#ModalAgregarMedicionPallets").modal("show");
}

function CargarModelosBaterias(identificador) {
    $("#txtModelo").empty();
        $("#txtModelo").append('<option value="">--Seleccione el modelo--</option>');
        $.ajax({
            type: 'POST',
            url: "../Calidad/ObtenerModelosBatPalletLocal?identificador=" + identificador,
            dataType: 'json',
            data: { id: $("#txtTipoBateria option:selected").val() },
            success: function (articulos) {
                for (var x in articulos) {
                    $("#txtModelo").append('<option value="' + articulos[x].ItemCode + '">' +
                        articulos[x].DescriptionCode + '</option>');
                }             
            },
        }) 
}

function ConsultarMedicionPallet(ident) {
    CargarModelosBaterias(ident);
    $.ajax({
        url: "../Calidad/ObtenerListMedicionesPalletLocal?identificador=" + ident,
        type: "GET",
        async: false
        , success: function (msg) {
            const locale = getLocale();
            DevExpress.localization.locale(locale);
            $("#tblListadoMedicionesPallets").dxDataGrid({
                dataSource: msg,
                keyExpr: "MedicionPalletPackingId",
                allowColumnReordering: false,
                allowColumnResizing: true,
                columnAutoWidth: false,
                showBorders: true,
                paging: {
                    pageSize: 10
                },
                pager: {
                    visible: true,
                    allowedPageSizes: [5, 10, 100],
                    showPageSizeSelector: true,
                    showNavigationButtons: true
                },              
                columns: [
                    {
                        dataField: "MedicionPalletPackingId", visible: false
                    },
                    {
                        dataField: "PackingId", visible: false
                    },
                    {
                        dataField: "PalletId", visible: false, width: 70
                    },                   
                    {
                        dataField: "Modelo", caption: "Modelo", allowEditing: true, allowHeaderFiltering: false, validationRules: [{ type: 'required' }], width: 200, alignment: "center"
                    },
                    {
                        dataField: "NumeroLote", caption: "# Lote", allowEditing: true, allowHeaderFiltering: false, validationRules: [{ type: 'required' }], width: 150, alignment: "center"
                    },
                    {
                        dataField: "Voltaje", caption: "Voltaje", allowEditing: true, allowHeaderFiltering: false, validationRules: [{ type: 'required' }], alignment: "center", width: 130,
                       format: {
                            type: "fixedPoint",
                            precision: 2,

                        },
                       customizeText: function (cellInfo) {
                        const noTruncarDecimales = { maximumFractionDigits: 2, minimumFractionDigits: 2 };

                        return (cellInfo.value).toLocaleString('en-US', noTruncarDecimales);
                    },
                    },
                    {
                        dataField: "nivel", caption: "nivel", allowEditing: true, allowHeaderFiltering: false, dataType: "boolean", width: 130
                    },
                    {
                        dataField: "Acabado", caption: "Acabado", allowEditing: true, allowHeaderFiltering: false, dataType: "boolean", width: 130
                    },
                    {
                        dataField: "Limpieza", caption: "Limpieza", allowEditing: true, allowHeaderFiltering: false, dataType: "boolean", width: 130
                    },
                    {
                        dataField: "CCA", caption: "CCA", allowEditing: true, allowHeaderFiltering: false, validationRules: [{ type: 'required' }], dataType: "number", alignment: "center", width: 130
                    },
                    {
                        caption: "Acciones", type: "buttons", width: 70,
                        buttons: [{
                            text: "Eliminar",
                            icon: "remove",
                            hint: "Eliminar",
                            onClick: function (e) {
                                EliminarMedicion(e.row.data);
                            }
                        },
                      ]
                    }
                ],
            });
        },

        error: function (msg) {
            $("#MensajeErrorGeneral").show('fade');
            setTimeout(function () {
                $("#MensajeErrorGeneral").fadeOut(1500);
            }, 3000);
        }
    })
    function getLocale() {
        const storageLocale = sessionStorage.getItem('locale');
        return storageLocale != null ? storageLocale : 'es';
    }
}


function ActualizarMedicion(valor, key) {
    $.ajax({
        url: '../Calidad/ActualizarMedicionPallet',
        type: 'POST',
        dataType: 'json',
        async: false,
        data: {
            medicionModal: valor, Key: key
        },
        success: function (respuesta) {
            ConsultarMedicionPallet(modelTemp);

        }
    });
    mostrarIngresosPallet();
}

function RegistrarMedicion() {
    var val1 = $("#txtNumeroLote").val();
    var val2 = $("#txtVoltaje").val();
    var val3 = $("#txtModelo option:selected").text();
    var val4 = $("#txtNivel option:selected").text();
    var val5 = $("#txtEtiquetado option:selected").text();
    var val6 = $("#txtCCA").val();
    var val7 = $("#txtLimpieza option:selected").text();

    if (val1.length == 0) {
        $("#MensajeCompleteCampos").show('fade');
        setTimeout(function () {
            $("#MensajeCompleteCampos").fadeOut(1500);
        }, 3000);
        return;
    }
    if (val2.length == 0) {
        $("#MensajeCompleteCampos").show('fade');
        setTimeout(function () {
            $("#MensajeCompleteCampos").fadeOut(1500);
        }, 3000);
        return;
    }
    if (val6.length == 0) {
        $("#MensajeCompleteCampos").show('fade');
        setTimeout(function () {
            $("#MensajeCompleteCampos").fadeOut(1500);
        }, 3000);
        return;
    }
    if (val3 == "----Seleccione el modelo----") {
        $("#MensajeCompleteCampos").show('fade');
        setTimeout(function () {
            $("#MensajeCompleteCampos").fadeOut(1500);
        }, 3000);
        return;
    }
    if (val4 == "--Seleccione--") {
        $("#MensajeCompleteCampos").show('fade');
        setTimeout(function () {
            $("#MensajeCompleteCampos").fadeOut(1500);
        }, 3000);
        return;
    }
    if (val5 == "--Seleccione--") {
        $("#MensajeCompleteCampos").show('fade');
        setTimeout(function () {
            $("#MensajeCompleteCampos").fadeOut(1500);
        }, 3000);
        return;
    }
    if (val7 == "--Seleccione--") {
        $("#MensajeCompleteCampos").show('fade');
        setTimeout(function () {
            $("#MensajeCompleteCampos").fadeOut(1500);
        }, 3000);
        return;
    }
    InsertarMedicion();
}

function InsertarMedicion() {
    var formdata = new FormData();
    formdata.append("packingId", encabezadoIdentificador);
    formdata.append("numeroLote", $("#txtNumeroLote").val());
    formdata.append("Modelo", $("#txtModelo option:selected").text());
    formdata.append("Voltaje", $("#txtVoltaje").val());
    formdata.append("nivel", $("#txtNivel option:selected").text());
    formdata.append("acabado", $("#txtEtiquetado option:selected").text());
    formdata.append("limpieza", $("#txtLimpieza  option:selected").text());
    formdata.append("cca", $("#txtCCA").val());
    var files = $("#txtAnexos").get(0).files;
    for (var i = 0; i < files.length; i++) {
        formdata.append("archivos", files[i]);
    }
    $.ajax({
        url: '../Calidad/InsertarMedicionPalletLocal',
        type: 'POST',
        processData: false,
        contentType: false,
        data:
            formdata,
        success: function (respuesta) {
            if (respuesta == "True") {
                $("#txtNumeroLote").val("");
                $("#txtVoltaje").val("");
                $("#txtCCA").val("");
                ConsultarMedicionPallet(encabezadoIdentificador);
                $("#MensajeGuardadoMedicion").show('fade');
                setTimeout(function () {
                    $("#MensajeGuardadoMedicion").fadeOut(1500);
                }, 3000);
            
                mostrarIngresosPallet();
            } else {
                $("#MensajeErrorGuardadoMedicion").show('fade');
                setTimeout(function () {
                    $("#MensajeErrorGuardadoMedicion").fadeOut(1500);
                }, 3000);
            }     
        }
    });
    //mostrarIngresosPallet();
}

function EliminarMedicion(valor) {
    $.ajax({
        url: '../Calidad/EliminarMedicionPalletLocal',
        type: 'POST',
        data: {
            medicionModal: valor
        },
        success: function (respuesta) {
            console.log("respuesta:" + respuesta);
            if (respuesta == "True") {
                ConsultarMedicionPallet(encabezadoIdentificador);
                $("#MensajeEliminacionCorectaMedicion").show('fade');
                setTimeout(function () {
                    $("#MensajeEliminacionCorectaMedicion").fadeOut(1500);
                }, 3000);
                mostrarIngresosPallet();
            } else {
                $("#MensajeErrorGuardadoMedicion").show('fade');
                setTimeout(function () {
                    $("#MensajeErrorGuardadoMedicion").fadeOut(1500);
                }, 3000);
            }           
        }
    });
}

function ActualizarEstadoPacking() {
    $.ajax({
        url: '../Calidad/ActualizarEstadoPackingLocal',
        type: 'post',
        async: false,

        data: {
            identificador: packingId
        },
        success: function (respuesta) {
          
        }
    });
    mostrarIngresosPallet();
}