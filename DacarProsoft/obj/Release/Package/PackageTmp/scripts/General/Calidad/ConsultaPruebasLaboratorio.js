var valor = null;

$(document).ready(function () {
    ConsultaRegistrosPruebasLaboratorio();
});

$('#LinkClose').on("click", function (e) {
    $("#MensajeErrorInesperado").hide('fade');
});
$('#LinkClose2').on("click", function (e) {
    $("#MensajeIngreseTodosLosCampos").hide('fade');
});
$('#LinkClose3').on("click", function (e) {
    $("#MensajeGuardadoExitoso").hide('fade');
});
$('#LinkClose4').on("click", function (e) {
    $("#MensajeSinAnexos").hide('fade');
});

function ConsultaRegistrosPruebasLaboratorio() {
    $.ajax({
        url: "../Calidad/ConsultarRegistrosPruebasLaboratorio",
        type: "GET"
        , success: function (msg) {

            $("#tblPruebasLaboratorioRegistrados").dxDataGrid({
                dataSource: msg,
                keyExpr: 'PruebaLaboratorioCalidadId',
                showBorders: true,
                columnAutoWidth: true,              
                showBorders: true,
                showColumnLines: true,
                showRowLines: true,
                rowAlternationEnabled: false,
                allowColumnReordering: true,
                allowColumnResizing: false,
                //paging: {
                //    pageSize: 10
                //},

                //selection: {
                //    mode: 'multiple'
                //},
                searchPanel: {
                    visible: true,
                    width: 240,
                    placeholder: "Buscar..."
                },
                headerFilter: {
                    visible: true
                },
                //customizeColumns(columns) {
                //    columns[0].width = 70;
                //},
                loadPanel: {
                    enabled: false,
                },
                scrolling: {
                    mode: 'infinite',
                },
                sorting: {
                    mode: 'none',
                },
                //pager: {
                //    visible: true,
                //    allowedPageSizes: [5, 10, 50],
                //    showPageSizeSelector: true,
                //    showInfo: true,
                //    showNavigationButtons: true
                //
                //},
                export: {
                    enabled: true,
                    allowExportSelectedData: false
                },

                onExporting: function (e) {
                    var workbook = new ExcelJS.Workbook();
                    var worksheet = workbook.addWorksheet('Ingresos Pruebas');

                    DevExpress.excelExporter.exportDataGrid({
                        component: e.component,
                        worksheet: worksheet,
                        autoFilterEnabled: true
                    }).then(function () {
                        workbook.xlsx.writeBuffer().then(function (buffer) {
                            saveAs(new Blob([buffer], { type: 'application/octet-stream' }), 'IngresosPruebasLaboratorio.xlsx');
                        });
                    });
                    e.cancel = true;
                },
                columns: [
                    {
                        caption: "Anexos", allowExporting: false,
                        cellTemplate: function (container, options) {
                            //var btnAnexo = "<button class='btn-primary' onclick='ModalObtenerRutaAnexos(" + JSON.stringify(options.data) + ")'>Anexos</button>";
                            var btnAnexo = "<a style='box-shadow: 2px 2px 5px #999 inset' onclick='ModalRegistrarAnexo(" + JSON.stringify(options.data) + ")'>Ingreso</a>";
                            var lblEspacio = "<a> </a>"
                            var btnAnexo2 = "<a style='box-shadow: 2px 2px 5px #999 inset' onclick='ModalObtenerRutaAnexos(" + JSON.stringify(options.data) + ")'>Consulta</a>";

                           // var nav = "<nav class='menu'> <ul> <li><a href='#'>Ingreso</a></li>  <li><a href='#'>Consulta</a></li>  </ul> </nav>";

                            $("<div>")
                                .append($(btnAnexo), $(lblEspacio), $(btnAnexo2))
                                .appendTo(container);
                        }
                    },

                    { dataField: "PruebaLaboratorioCalidadId", visible: false },
                    {
                        dataField: "FechaIngreso", caption: "Fecha Ingreso", alignment: "right", dataType: "date", allowHeaderFiltering: true, allowSearch: false, width: 130
                    },
                    {
                        dataField: "CodigoIngreso", caption: "Cod. Ingreso", alignment: "right", allowHeaderFiltering: false, allowSearch: true, width: 100
                    },
                    {
                        dataField: "Marca", caption: "Marca", alignment: "right", allowHeaderFiltering: true, allowSearch: false
                    },
                    {
                        dataField: "TipoNorma", caption: "Tipo Norma", alignment: "right", allowHeaderFiltering: true, allowSearch: false, width: 110
                    },
                    {
                        dataField: "Normativa", caption: "Normativa", alignment: "right", allowHeaderFiltering: true, allowSearch: false, width: 110
                    },
                    {
                        dataField: "PreAcondicionamiento", caption: "Pre-Acond.", alignment: "right", allowHeaderFiltering: true, allowSearch: false, width: 100
                    },
                    {
                        dataField: "TipoBateria", caption: "Tipo Bateria", alignment: "right", allowHeaderFiltering: true, allowSearch: false, width: 110
                    },
                    {
                        dataField: "Modelo", caption: "Modelo", alignment: "right", allowHeaderFiltering: true, allowSearch: false, width: 95
                    },
                    {
                        dataField: "Separador", caption: "Separador", alignment: "right", allowHeaderFiltering: true, allowSearch: false, width: 105
                    },
                    {
                        dataField: "TipoEnsayo", caption: "Tipo Ensayo", alignment: "right", allowHeaderFiltering: true, allowSearch: false, width: 120
                    },
                    {
                        dataField: "LoteEnsamble", caption: "Lote Ensamble", alignment: "right", allowHeaderFiltering: false, allowSearch: false, width: 120
                    },
                    {
                        dataField: "LoteCarga", caption: "Lote Carga", alignment: "right", allowHeaderFiltering: false, allowSearch: false, width: 100
                    },
                    {
                        dataField: "CCA", caption: "CCA", alignment: "right", allowHeaderFiltering: false, allowSearch: false, width: 70
                    },
                    {
                        dataField: "Peso", caption: "Peso", alignment: "right", allowHeaderFiltering: false, allowSearch: false, width: 70
                    },
                    {
                        dataField: "Voltaje", caption: "Voltaje", alignment: "right", allowHeaderFiltering: false, allowSearch: false, width: 80
                    },
                    {
                        dataField: "DensidadIngreso", caption: "Dens. Ingreso", alignment: "right", allowHeaderFiltering: false, allowSearch: false, width: 110
                    },

                    {
                        dataField: "DensidadPreAcondicionamiento", caption: "Dens. Pre-Acond.", alignment: "right", allowHeaderFiltering: false, allowSearch: false, width: 120
                    },
                    {
                        dataField: "TemperaturaIngreso", caption: "Temp. Ingreso", alignment: "right", allowHeaderFiltering: false, allowSearch: false, width: 100
                    },
                    {
                        dataField: "TemperaturaPrueba", caption: "Temp. Prueba", alignment: "right", allowHeaderFiltering: false, allowSearch: false, width: 100
                    },
                    {
                        dataField: "DatoTeoricoPrueba", caption: "Dato Teorico Prueba", alignment: "right", allowHeaderFiltering: false, allowSearch: false, width: 135
                    },
                    {
                        dataField: "ValorObjetivo", caption: "Valor Objetivo", alignment: "right", allowHeaderFiltering: false, allowSearch: false, width: 100
                    },
                    {
                        dataField: "ResultadoFinal", caption: "Resultado Final", alignment: "right", allowHeaderFiltering: false, allowSearch: false, width: 110
                    },
                    {
                        dataField: "Observaciones", caption: "Observaciones", alignment: "right", allowHeaderFiltering: false, allowSearch: false, width: 115
                    },
                    {
                        dataField: "Calificacion", caption: "Calificacion", alignment: "right", allowHeaderFiltering: false, allowSearch: false, width: 90
                    },
                    {
                        dataField: "FechaRegistro", caption: "Fecha Registro", alignment: "right", dataType: "date", allowHeaderFiltering: false, allowSearch: false, width: 120
                    },
                   
                ],
            });
        },
        error: function (msg) {

            $("#MensajeErrorInesperado").show('fade');
            setTimeout(function () {
                $("#MensajeErrorInesperado").fadeOut(1500);
            }, 3000); return;
        }
    })


}

//href = "../pdfs/project-brief.pdf"
function ModalRegistrarAnexo(valor) {
    valores = valor;
    $("#ModalAnexosIngreso").modal("show");

}

function RegistrarNuevoAnexo() {
    var formdata = new FormData();
    formdata.append("PruebaLaboratorioCalidadId", valores.PruebaLaboratorioCalidadId);
    formdata.append("FechaRegistro", valores.FechaRegistro);
    formdata.append("modelo", valores.Modelo);

    var files = $("#txtRegistrarAnexos").get(0).files;

    for (var i = 0; i < files.length; i++) {
        formdata.append("archivos", files[i]);
    }
    $.ajax({
        type: 'POST',
        url: "../Calidad/RegistrarNuevosAnexos",
        processData: false,
        contentType: false,
        data:
            formdata,
        success: function (msg) {
            console.log("retorna:" + msg);
            if (msg == "True") {
                $("#ModalAnexosIngreso").modal("hide");
                $("#txtRegistrarAnexos").val("");
                $("#MensajeGuardadoExitoso").show('fade');
                setTimeout(function () {
                    $("#MensajeGuardadoExitoso").fadeOut(1500);
                }, 3000);

            } else {
                $("#MensajeErrorInesperado").show('fade');
                setTimeout(function () {
                    $("#MensajeErrorInesperado").fadeOut(1500);
                }, 3000);
            }
        },
        error: function (msg) {
            $("#MensajeErrorInesperado").show('fade');
            setTimeout(function () {
                $("#MensajeErrorInesperado").fadeOut(1500);
            }, 3000);
        }
    })
}


function ModalObtenerRutaAnexos(valor) {
    $.ajax({
        url: '../Calidad/ConsultarAnexosRegistrados',
        data: {
            IdRegistro: valor.PruebaLaboratorioCalidadId, FechaRegistro: valor.FechaRegistro, modelo: valor.Modelo
        },
        type: 'post',
        success: function (respuestas) {
            if (respuestas.length === 0) {
                $("#MensajeSinAnexos").show('fade');
                setTimeout(function () {
                    $("#MensajeSinAnexos").fadeOut(1500);
                }, 3000); return;
                
            } else {
                $("#txtAnexoDesc").empty();
                $("#txtAnexoDesc").append('<option value="">--Seleccione--</option>');
                $.each(respuestas, function (i, respuesta) {
                    $("#txtAnexoDesc").append('<option value="' + respuesta.Value + '">' +
                        respuesta.Text + '</option>');
                });
            }
        },
        error: function () {
            $("#MensajeErrorGeneral").show('fade');
            setTimeout(function () {
                $("#MensajeErrorGeneral").fadeOut(1500);
            }, 3000); return;
        }
    });
    $("#ModalAnexos").modal("show");
}


function DescargarAnexo() {
    $("#ModalAnexos").modal("hide");
    var valor = $("#txtAnexoDesc option:selected").val();
    console.log(valor);
    window.open(valor);
}

