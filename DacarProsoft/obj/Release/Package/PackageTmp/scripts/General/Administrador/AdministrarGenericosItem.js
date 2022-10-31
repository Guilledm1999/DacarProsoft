var temp = null;

$(document).ready(function () {
    ConsultaRegistrosPruebasLaboratorio();
});

$('#LinkClose').on("click", function (e) {
    $("#MensajeErrorGeneral").hide('fade');
});

function ConsultaRegistrosPruebasLaboratorio() {
    $.ajax({
        url: "../Administrador/ConsultarGenericosItem",
        type: "GET"
        , success: function (msg) {
            temp = msg;

            const locale = getLocale();
            DevExpress.localization.locale(locale);

            $("#tblGenericosItem").dxDataGrid({
                dataSource: temp,
                keyExpr: 'GenericoItemId',
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
              
                    { dataField: "GenericoItemId", visible: false },
                    {
                        dataField: "GrupoGenericoItem", caption: "Grupo Generico", allowEditing: true, allowHeaderFiltering: true, validationRules: [{ type: 'required' }]

                    },
                    {
                        dataField: "ModeloDacar", caption: "Modelo Dacar", allowEditing: true, allowHeaderFiltering: false, validationRules: [{ type: 'required' }]
                    },
                    {
                        dataField: "NumeroParteCliente", caption: "Numero Parte Cliente", allowEditing: true, allowHeaderFiltering: false, validationRules: [{ type: 'required' }]
                    },
                    {
                        dataField: "EtiquetaDatosTecnicos", caption: "Etiqueta Datos Tec.", allowEditing: true, allowHeaderFiltering: false, validationRules: [{ type: 'required' }]
                    },
                    {
                        dataField: "Polaridad", caption: "Polaridad", allowEditing: true, allowHeaderFiltering: true, validationRules: [{ type: 'required' }]
                    },
                    {
                        dataField: "TipoTerminal", caption: "Tipo Terminal", allowEditing: true, allowHeaderFiltering: true, validationRules: [{ type: 'required' }]
                    },
                    {
                        dataField: "CantidadPiso", caption: "Cantidad Piso", allowEditing: true, allowHeaderFiltering: false, validationRules: [{ type: 'required' }]
                    },
                    {
                        dataField: "PisoMaximo", caption: "Piso Máximo", allowEditing: true, allowHeaderFiltering: false, validationRules: [{ type: 'required' }]
                    },
                    {
                        dataField: "BateriasPallet", caption: "Baterias Pallet", allowEditing: true, allowHeaderFiltering: false, validationRules: [{ type: 'required' }]
                    },
                    {
                        dataField: "PesoTara", caption: "Peso Tara", allowEditing: true, allowHeaderFiltering: false, validationRules: [{ type: 'required' }]
                    }
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
        url: '../Administrador/ActualizarGenerico',
        type: 'POST',
        dataType: 'json',
        data: {
            generico: valor, Key:key
        },
        success: function (respuesta) {
            console.log("respuesta:" + respuesta);
            if (respuesta == "True") {
                
                ConsultaRegistrosPruebasLaboratorio();
            }
        }
    });
}

function InsertarGenerico(valor) {
    $.ajax({
        url: '../Administrador/InsertarGenerico',
        type: 'POST',
        dataType: 'json',
        data: {
            generico: valor
        },
        success: function (respuesta) {
            console.log("respuesta:" + respuesta);

            if (respuesta == "True") {
                ConsultaRegistrosPruebasLaboratorio();
            }
            
        }
    });
}

function EliminarGenerico(valor) {
    $.ajax({
        url: '../Administrador/EliminarGenerico',
        type: 'POST',
        dataType: 'json',
        data: {
            generico: valor
        },
        success: function (respuesta) {
            console.log("respuesta:" + respuesta);

            if (respuesta == "True") {
                ConsultaRegistrosPruebasLaboratorio();
            }
        }
    });
}