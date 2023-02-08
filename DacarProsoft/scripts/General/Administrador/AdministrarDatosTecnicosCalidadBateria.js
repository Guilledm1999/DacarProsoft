var temp = null;

$(document).ready(function () {
    ConsultaDatosTecnicosCalidadBateria();
});

$('#LinkClose').on("click", function (e) {
    $("#MensajeErrorGeneral").hide('fade');
});

function ConsultaDatosTecnicosCalidadBateria() {
    $.ajax({
        url: "../Administrador/ConsultarDatosTecnicosCalidadBateria",
        type: "GET"
        , success: function (msg) {
            temp = msg;

            const locale = getLocale();
            DevExpress.localization.locale(locale);

            $("#tblGenericosItem").dxDataGrid({
                dataSource: temp,
                keyExpr: 'DatosTecnicosCalidadBateriasId',
                showBorders: true,
                columnAutoWidth: true,
                showBorders: true,
                showColumnLines: true,
                showRowLines: true,
                rowAlternationEnabled: false,
                allowColumnReordering: true,
                allowColumnResizing: false,

                searchPanel: {
                    visible: true,
                    width: 240,
                    placeholder: "Buscar..."
                },
               
                headerFilter: {
                    visible: true
                },
                editing: {
                    mode: 'row',
                    allowUpdating: true,
                    allowDeleting: true,
                    allowAdding: true,
                    useIcons: true,
                    texts: {
                        confirmDeleteMessage: 'Esta seguro de eliminar este item?'
                    }
                },
                columns: [

                    { dataField: "DatosTecnicosCalidadBateriasId", visible: false },
                    {
                        dataField: "Modelo", caption: "Modelo", allowEditing: true, allowHeaderFiltering: true, validationRules: [{ type: 'required' }]

                    },
                    {
                        dataField: "CAP", caption: "C20", allowEditing: true, allowHeaderFiltering: false
                    },
                    {
                        dataField: "C5", caption: "C5", allowEditing: true, allowHeaderFiltering: false,
                    },
                    {
                        dataField: "C10", caption: "C10", allowEditing: true, allowHeaderFiltering: false,
                    },
                    {
                        dataField: "C100", caption: "C100", allowEditing: true, allowHeaderFiltering: true,
                    },
                    {
                        dataField: "RC", caption: "RC", allowEditing: true, allowHeaderFiltering: true,
                    },
                    {
                        dataField: "CCA", caption: "CCA", allowEditing: true, allowHeaderFiltering: false,
                    },
                    {
                        dataField: "CACeroGrados", caption: "CACeroGrados", allowEditing: true, allowHeaderFiltering: false,
                    },
                    {
                        dataField: "CCAMenosDiescochoExpo", caption: "CCAMenosDiescochoExpo", allowEditing: true, allowHeaderFiltering: false,
                    },
                    {
                        dataField: "C20xDiseno", caption: "C20xDiseno", allowEditing: true, allowHeaderFiltering: false,
                    },
                    {
                        dataField: "CapResxDiseno", caption: "CapResxDisen", allowEditing: true, allowHeaderFiltering: false,
                    },
                    {
                        dataField: "CCAxDisenoSeparadorFibra", caption: "CCAxDisenoSeparadorFibra", allowEditing: true, allowHeaderFiltering: false,
                    },
                    {
                        dataField: "CAxDisenoSeparadorFibra", caption: "CAxDisenoSeparadorFibra", allowEditing: true, allowHeaderFiltering: false,
                    },
                    {
                        dataField: "HCAxDisenoSeparadorFibra", caption: "HCAxDisenoSeparadorFibra", allowEditing: true, allowHeaderFiltering: false,
                    },
                    {
                        dataField: "CCAxDisenoSeparadorPE", caption: "CCAxDisenoSeparadorPE", allowEditing: true, allowHeaderFiltering: false,
                    },
                    {
                        dataField: "CAxDisenoSeparadorPE", caption: "CAxDisenoSeparadorPE", allowEditing: true, allowHeaderFiltering: false,
                    },
                    {
                        dataField: "HCAxDisenoSeparadorPE", caption: "HCAxDisenoSeparadorPE", allowEditing: true, allowHeaderFiltering: false,
                    },
                    {
                        dataField: "CantPlacas", caption: "CantPlacas", allowEditing: true, allowHeaderFiltering: false,
                    },
                    {
                        dataField: "PesoSellada", caption: "PesoSellada", allowEditing: true, allowHeaderFiltering: false,
                    },
                    {
                        dataField: "PesoHumedaKg", caption: "PesoHumedaKg", allowEditing: true, allowHeaderFiltering: false,
                    },
                    {
                        dataField: "Linea", caption: "Linea", allowEditing: true, allowHeaderFiltering: false,
                    },
                ],
                onRowUpdating: function (options) {
                    this.oldData = Object.assign({}, options.oldData);
                    ActualizarGenerico(options.newData, options.key);

                },
                onRowInserting: function (options) {
                    InsertarGenerico((options.data));
                }
                ,
                onRowRemoving: function (options) {
                    EliminarGenerico(options.data);
                }
            });
        },
        error: function (msg) {
            $("#MensajeErrorGeneral").show('fade');
            setTimeout(function () {
                $("#MensajeErrorGeneral").fadeOut(1500);
            }, 3000); return;
        }
    })
    function getLocale() {
        const storageLocale = sessionStorage.getItem('locale');
        return storageLocale != null ? storageLocale : 'es';
    }
}

function ActualizarGenerico(valor, key) {
    $.ajax({
        url: '../Administrador/ActualizarDatosTecnicosCalidadBateria',
        type: 'POST',
        dataType: 'json',
        data: {
            generico: valor, Key: key
        },
        success: function (respuesta) {
            console.log("respuesta:" + respuesta);
            if (respuesta == "True") {

                ConsultaDatosTecnicosCalidadBateria();
            }
        }
    });
}

function InsertarGenerico(valor) {
    $.ajax({
        url: '../Administrador/InsertarDatosTecnicosCalidadBateria',
        type: 'POST',
        dataType: 'json',
        data: {
            generico: valor
        },
        success: function (respuesta) {
            console.log("respuesta:" + respuesta);

            if (respuesta == "True") {
                ConsultaDatosTecnicosCalidadBateria();
            }

        }
    });
}

function EliminarGenerico(valor) {
    $.ajax({
        url: '../Administrador/EliminarDatosTecnioscCalidadBateria',
        type: 'POST',
        dataType: 'json',
        data: {
            generico: valor
        },
        success: function (respuesta) {
            console.log("respuesta:" + respuesta);

            if (respuesta == "True") {
                ConsultaDatosTecnicosCalidadBateria();
            }
        }
    });
}