var packingId = null;
var palletId = null;
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
                    { dataField: "PackingId", visible: false },
                    {
                        dataField: "NumeroDocumento", caption: "# Secuencial Sap", allowEditing: false, allowHeaderFiltering: false
                    },
                    {
                        dataField: "NumeroOrden", caption: "Numero Orden", allowEditing: false, headerFilter: false, allowHeaderFiltering: false
                    },
                    {
                        dataField: "NombreCliente", caption: "Cliente", allowEditing: false, headerFilter: true, allowHeaderFiltering: true
                    },
                    {
                        dataField: "NumeroContenedor", caption: "Cantidad Pallets", allowEditing: false, headerFilter: true, allowHeaderFiltering: true
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
                                // Execute your command here
                                ModalConsultarPalletsIngresado(e.row.data);
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
    var cantMaxMed = ConsultarIngresoMaximoMedicion();
    if (resTempo.Estado == "Incompleto" || resTempo.cantidadMediciones > cantMaxMed || resTempo.cantidadMediciones <= 0) {
        $("#ModalAfirmacionLiberacion").modal("hide");
        $("#MensajePackingSinDetalle").show('fade');
        setTimeout(function () {
            $("#MensajePackingSinDetalle").fadeOut(1500);
        }, 3000);
    } else {
        ActualizarEstadoPacking();
        $("#ModalAfirmacionLiberacion").modal("hide");
        $("#MensajeGuardado").show('fade');
        setTimeout(function () {
            $("#MensajeGuardado").fadeOut(1500);
        }, 3000);
    }
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

function ModalConsultarPalletsIngresado(modelo) {
    ConsultarPalletsIngresado(modelo);
    $("#ModalListadoDePallets").modal("show");
}
function ConsultarPalletsIngresado(modelo) {
    $.ajax({
        url: "../Calidad/ObtenerPalletList?PackingId=" + modelo.PackingId,
        type: "GET"
        , success: function (msg) {
            $("#tblListadoPalletsIngresados").dxDataGrid({
                dataSource: msg,
                keyExpr: "PalletPacking1",
                allowColumnReordering: false,
                allowColumnResizing: true,
                columnAutoWidth: true,
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
                        dataField: "PalletPacking1", visible: false
                    },
                    {
                        dataField: "PackingId", visible: false
                    },
                    {
                        dataField: "PalletNumber", caption: "Número Pallet", allowEditing: false, allowHeaderFiltering: false
                    },
                    {
                        dataField: "AnchoPallet", visible: false
                    },
                    {
                        dataField: "LargoPallet", visible: false
                    },
                    {
                        dataField: "AltoPallet", visible: false
                    },
                    {
                        dataField: "Cantidad", caption: "Cantidad Items", allowEditing: false, allowHeaderFiltering: false
                    },
                    {
                        dataField: "CantidadMediciones", caption: "Cantidad Mediciones", allowEditing: false, allowHeaderFiltering: false
                    },
                    {
                        caption: "Mediciones", type: "buttons", width: 100,
                        buttons: [{
                            text: "Agregar Medicion",
                            icon: "add",
                            hint: "Agregar Medicion",
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
}
function ModalConsultarMedicionesPallets(modelo) {
    ConsultarMedicionPallet(modelo);
    $("#ModalListadoDePallets").modal("hide");
    $("#ModalAgregarMedicionPallets").modal("show");
}

function ConsultarMedicionPallet(modelo) {
    modelTemp = modelo;
    $("#lblPalletDetalle").text("Pallet #" + modelo.PalletNumber);
    packingId = modelo.PackingId;
    palletId = modelo.PalletPacking1;
    var lookupDataSource = {
        store: new DevExpress.data.CustomStore({
            key: "ItemCode",
            loadMode: "raw",
            load: function () {
                // Returns an array of objects that have the following structure:
                // { id: 1, name: "John Doe" }
                return $.getJSON("../Calidad/ObtenerModelosBatPallet?PackinkId=" + modelo.PackingId + "&PalletId=" + modelo.PalletPacking1);
            }
        }),
        sort: "Modelo"
    }
    $.ajax({
        url: "../Calidad/ObtenerPalletListMediciones?PackinkId=" + modelo.PackingId + "&PalletId=" + modelo.PalletPacking1,
        type: "GET",
        async: false
        , success: function (msg) {
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
                editing: {
                    mode: 'row',
                    allowUpdating: false,
                    allowDeleting: true,
                    allowAdding: true,
                    useIcons: true,
                    texts: {
                        confirmDeleteMessage: 'Esta seguro de eliminar este item?'
                    }
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
                    //{
                    //    dataField: "NumeroMedicion", caption: "# Medicion", allowEditing: true, allowHeaderFiltering: false, validationRules: [{ type: 'required' }], width: 90, alignment: "center"
                    //},
                   
                    {
                        dataField: "Modelo", caption: "Modelo", allowEditing: true, allowHeaderFiltering: false, validationRules: [{ type: 'required' }], width: 200, alignment: "center"
                        , lookup: {
                        //dataSource: valorView,
                        dataSource: lookupDataSource,
                            valueExpr: "DescriptionCode",
                            displayExpr: "DescriptionCode",
                    }

                    },
                    {
                        dataField: "NumeroLote", caption: "# Lote", allowEditing: true, allowHeaderFiltering: false, validationRules: [{ type: 'required' }], width: 150, alignment: "center"
                    },
                    {
                        dataField: "Voltaje", caption: "Voltaje", allowEditing: true, allowHeaderFiltering: false, validationRules: [{ type: 'required' }], dataType: "number", alignment: "center", width: 130
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
         
                ],
                onRowUpdating: function (options) {
                    this.oldData = Object.assign({}, options.oldData);
                    ActualizarMedicion(options.newData, options.key);

                },
                onRowInserting: function (options) {
                    InsertarMedicion((options.data));
                }
                ,
                onRowRemoving: function (options) {
                    EliminarMedicion(options.data);
                }
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
            console.log("respuesta:" + respuesta);
            ConsultarMedicionPallet(modelTemp);

        }
    });
    mostrarIngresosPallet();
}

function InsertarMedicion(valor) {
    $.ajax({
        url: '../Calidad/InsertarMedicionPallet',
        type: 'POST',
        dataType: 'json',
        async: false,
        data: {
            packingId: packingId, palletId: palletId,medicionModal: valor
        },
        success: function (respuesta) {
            console.log("respuesta:" + respuesta);
            ConsultarMedicionPallet(modelTemp);

        }
    });
    mostrarIngresosPallet();
}

function EliminarMedicion(valor) {
    $.ajax({
        url: '../Calidad/EliminarMedicionPallet',
        type: 'POST',
        dataType: 'json',
        async: false,
        data: {
            medicionModal: valor
        },
        success: function (respuesta) {
            console.log("respuesta:" + respuesta);
            ConsultarMedicionPallet(modelTemp);

        }
    });
    mostrarIngresosPallet();
}

function ActualizarEstadoPacking() {
    console.log("ingreso a actualizar");
    $.ajax({
        url: '../Calidad/ActualizarEstadoPacking',
        type: 'post',
        async: false,

        data: {
            packingId: packingId
        },
        success: function (respuesta) {
          
        }
    });
    mostrarIngresosPallet();
}