var temp = null;

$(document).ready(function () {
    ConsultaRegistrosCronograma();
});

$('#LinkClose').on("click", function (e) {
    $("#MensajeErrorGeneral").hide('fade');
});

function ConsultaRegistrosCronograma() {
    $.ajax({
        url: "../Pedidos/ConsultarEventosMes",
        type: "GET"
        , success: function (msg) {
            temp = msg;

            const locale = getLocale();
            DevExpress.localization.locale(locale);

            $("#tblEventosCronogramaExp").dxDataGrid({
                dataSource: temp,
                keyExpr: 'CronogramaExportacionId',
                showBorders: true,
                columnAutoWidth: true,
                showBorders: true,
                showColumnLines: true,
                showRowLines: true,
                rowAlternationEnabled: false,
                allowColumnReordering: false,
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

                    { dataField: "CronogramaExportacionId", visible: false },
                    {
                        dataField: "Orden", caption: "Orden", allowEditing: true, allowHeaderFiltering: true, allowSearch: true, headerFilter: {
                            allowSearch: true,
                        }, validationRules: [{ type: 'required' }],

                    },
                    {
                        dataField: "Cliente", caption: "Cliente", allowEditing: true, allowHeaderFiltering: true, headerFilter: {
                            allowSearch: true,
                        }, validationRules: [{ type: 'required' }]
                    },
                    {
                        dataField: "CardCode", caption: "Identificacion", allowEditing: true, allowHeaderFiltering: false, validationRules: [{ type: 'required' }]
                    },
                    {
                        dataField: "Destino", caption: "Destino", allowEditing: true, allowHeaderFiltering: false, validationRules: [{ type: 'required' }]
                    },
                    {
                        dataField: "FechaDespacho", caption: "Fecha despacho", allowEditing: true, dataType: 'date', allowHeaderFiltering: true, validationRules: [{ type: 'required' }]
                    },
                    {
                        dataField: "FechaPedido", caption: "Fecha pedido", allowEditing: true, dataType: 'date',allowHeaderFiltering: true, validationRules: [{ type: 'required' }]
                    },
                    {
                        dataField: "FechaZarpe", caption: "Fecha zarpe", dataType: 'date', allowEditing: true, allowHeaderFiltering: false
                    },
                    {
                        dataField: "Booking", caption: "Booking", allowEditing: true, allowHeaderFiltering: false, validationRules: [{ type: 'required' }]
                    },
                    {
                        dataField: "TotalContenedores", caption: "Total Contenedores", allowEditing: true, allowHeaderFiltering: false, validationRules: [{ type: 'required' }]
                    },
                   
                ],
                onRowUpdating: function (options) {
                    this.oldData = Object.assign({}, options.oldData);
                    ActualizarEventoMes(options.newData, options.key);

                },
                onRowInserting: function (options) {
                    InsertarEventoMes((options.data));
                }
                ,
                onRowRemoving: function (options) {
                    EliminarEventoMes(options.data);
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

function ActualizarEventoMes(valor, key) {
    $.ajax({
        url: '../Pedidos/ActualizarEventoMes',
        type: 'POST',
        dataType: 'json',
        data: {
            crono: valor, Key: key
        },
        success: function (respuesta) {
            if (respuesta == "True") {

                ConsultaRegistrosCronograma();
            }
        }
    });
}

function InsertarEventoMes(valor) {
    $.ajax({
        url: '../Pedidos/InsertarEventoMes',
        type: 'POST',
        dataType: 'json',
        data: {
            crono: valor
        },
        success: function (respuesta) {

            if (respuesta == "True") {
                ConsultaRegistrosCronograma();
            }

        }
    });
}

function EliminarEventoMes(valor) {
    $.ajax({
        url: '../Pedidos/EliminarEventoMes',
        type: 'POST',
        dataType: 'json',
        data: {
            crono: valor
        },
        success: function (respuesta) {

            if (respuesta == "True") {
                ConsultaRegistrosCronograma();
            }
        }
    });
}