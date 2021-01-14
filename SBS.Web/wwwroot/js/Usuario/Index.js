//variable para la invocacion del javascript
var usuarioJS;

$(document).ready(function () {
    var main;

    main = function () {
        var me = this;

        me.Globales = {

        };
        me.Init = {
            InicializarEventos: function () {


            },
            InicializarAcciones: function () {

                me.Funciones.BuscarUsuario();
                me.Funciones.GrabarUsuario();

            }
        };
        me.Eventos = {

        };
        me.Funciones = {

            BuscarUsuario: function () {
                let param = {};
                param.RolId = 0;
                param.CodigoUsuario = "gmatos";
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