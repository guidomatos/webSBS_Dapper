//variable para la invocacion del javascript
var usuarioJS;

$(document).ready(function () {
    var main;

    main = function () {
        var me = this;

        me.Globales = {

            BtnExportarExcel: '#btnExportarExcel'

        };
        me.Init = {
            InicializarEventos: function () {

                //$(document).on('click', me.Globales.BtnExportarExcel, me.Eventos.ExportarExcel);
                $(document).on('click', '#btnExportarExcel', me.Eventos.ExportarExcel);

            },
            InicializarAcciones: function () {

                me.Funciones.BuscarUsuario();
                me.Funciones.GrabarUsuario();

            }
        };
        me.Eventos = {

            ExportarExcel: function (event) {


                const param = me.Funciones.ObtenerParametroBusqueda();
                console.log('param', param);
                me.Funciones.DescargarArchivo(param);

            }

        };
        me.Funciones = {

            BuscarUsuario: function () {

                const param = me.Funciones.ObtenerParametroBusqueda();
                const listaUsuario = me.Funciones.ajaxBuscarUsuario(param);
                console.log('listaUsuario', listaUsuario);
            },
            GrabarUsuario: function () {
                let param = {};

                param.RolId = 0;
                param.CodigoUsuario = 'gmatos11';
                param.ClaveSecreta = '123456';
                param.Email = 'gmatos11@pruebas.com';
                param.ApellidoPaterno = 'Matos';
                param.ApellidoMaterno = 'Camones';
                param.PrimerNombre = 'Guido';
                param.SegundoNombre = 'Alan';
                param.Alias = 'Guido Matos';

                const response = me.Funciones.ajaxGrabarUsuario(param);
                
                console.log('response', response);
            },
            // #region "Api"
            ajaxBuscarUsuario: function (param) {

                let lista = [];

                $.ajax({
                    type: 'POST',
                    url: urlBuscarUsuario,
                    dataType: 'json',
                    contentType: 'application/json; charset=utf-8',
                    data: JSON.stringify(param),
                    async: false,
                    beforeSend: function () {
                    },
                    success: function (response) {
                        if (response.success) lista = response.data;
                    },
                    error: function (data, error) {

                    },
                    complete: function () {
                    }
                });

                return lista;

            },
            ajaxGrabarUsuario: function (param) {

                let responseGrabar = {
                    success: false,
                    data: 0
                };

                $.ajax({
                    type: 'POST',
                    url: urlGrabarUsuario,
                    dataType: 'json',
                    contentType: 'application/json; charset=utf-8',
                    data: JSON.stringify(param),
                    async: false,
                    beforeSend: function () {
                    },
                    success: function (response) {
                        responseGrabar = response;
                    },
                    error: function (data, error) {

                    },
                    complete: function () {
                    }
                });

                return responseGrabar;

            }
            // #endregion

            , ObtenerParametroBusqueda: function () {
                let param = {};
                param.RolId = 0;
                param.CodigoUsuario = "gmatos";
                return param;
            }
            ,DescargarArchivo: function (param) {

                $.ajax({
                    url: urlExportarExcel,
                    method: 'POST',
                    contentType: 'application/json; charset=utf-8',
                    data: JSON.stringify(param),
                    xhrFields: {
                        responseType: 'blob'
                    },
                    success: function (data, status, response) {
                        let filename = "";
                        let disposition = response.getResponseHeader('Content-Disposition');
                        if (disposition && disposition.indexOf('attachment') !== -1) {
                            let filenameRegex = /filename[^;=\n]*=((['"]).*?\2|[^;\n]*)/;
                            let matches = filenameRegex.exec(disposition);
                            if (matches != null && matches[1]) filename = matches[1].replace(/['"]/g, '');
                        }
                        let a = document.createElement('a');
                        a.href = window.URL.createObjectURL(data);
                        a.download = filename;
                        document.body.append(a);
                        a.click();
                        window.URL.revokeObjectURL(urlExportarExcel);
                    },
                    complete: function (data) {


                    },
                });

            }

        };
        me.Inicializar = function () {
            //funciones a usar ni bien se cargue la pagina
            me.Init.InicializarEventos();
            me.Init.InicializarAcciones();
        };
    };

    usuarioJS = new main();
    usuarioJS.Inicializar();

});