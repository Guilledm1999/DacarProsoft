var temp = null;

$(document).ready(function () {
    ConsultaMaestrosGenerales();
});

$('#LinkClose').on("click", function (e) {
    $("#MensajeErrorGeneral").hide('fade');
});

function ConsultaMaestrosGenerales() {
    $.ajax({
        url: "../Administrador/ConsultarMaestrosGenerales",
        type: "GET"
        , success: function (msg) {
            temp = msg;

            const locale = getLocale();
            DevExpress.localization.locale(locale);

            $("#tblMaestrosGenerales").dxDataGrid({
                dataSource: temp,
                keyExpr: 'MaestrosUtilitariosId',
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

                    { dataField: "MaestrosUtilitariosId", visible: false },
                    {
                        dataField: "Descripcion", caption: "Descripcion", allowEditing: true, allowHeaderFiltering: true, validationRules: [{ type: 'required' }]

                    },
                    {
                        dataField: "Valor", caption: "Valor", allowEditing: true, allowHeaderFiltering: false, validationRules: [{ type: 'required' }]
                    },
                    {
                        dataField: "fechaCreacion", caption: "Fecha Creacion", allowEditing: false, allowHeaderFiltering: false
                    },
                    {
                        dataField: "fechaActualizacion", caption: "Fecha modificacion", allowEditing: false, allowHeaderFiltering: false
                    },
                    {
                        dataField: "estado", caption: "Estado", allowEditing: true, allowHeaderFiltering: true
                    }
                ],
                onRowUpdating: function (options) {
                    this.oldData = Object.assign({}, options.oldData);
                    ActualizarMaestroGeneral(options.newData, options.key);
                    ConsultaMaestrosGenerales();


                },
                onRowInserting: function (options) {
                    InsertarMaestroGeneral((options.data));
                    ConsultaMaestrosGenerales();

                }
                ,
                onRowRemoving: function (options) {
                    EliminarMaestroGeneral(options.data);
                    ConsultaMaestrosGenerales();

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

function ActualizarMaestroGeneral(valor, key) {
    $.ajax({
        url: '../Administrador/ActualizarMaestroGeneral',
        type: 'POST',
        dataType: 'json',
        async: false,
        data: {
            generico: valor, Key: key
        },
        success: function (respuesta) {
            console.log("respuesta:" + respuesta);
            if (respuesta == "True") {
            //    ConsultaMaestrosGenerales();
            }
        }
    });
}

function InsertarMaestroGeneral(valor) {
    $.ajax({
        url: '../Administrador/InsertarMaestroGeneral',
        type: 'POST',
        dataType: 'json',
        async: false,
        data: {
            generico: valor
        },
        success: function (respuesta) {
            console.log("respuesta:" + respuesta);
            if (respuesta == "True") {
            //    ConsultaMaestrosGenerales();
            }
        }
    });
}

function EliminarMaestroGeneral(valor) {
    $.ajax({
        url: '../Administrador/EliminarMaestroGeneral',
        type: 'POST',
        dataType: 'json',
        async: false,
        data: {
            generico: valor
        },
        success: function (respuesta) {
            console.log("respuesta:" + respuesta);

            if (respuesta == "True") {
            //    ConsultaMaestrosGenerales();
            }
        }
    });
}