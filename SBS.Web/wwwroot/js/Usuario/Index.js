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

                const listaUsuario = me.Funciones.ApiBuscarUsuario();
                console.log('listaUsuario', listaUsuario);

            }
        };
        me.Eventos = {

        };
        me.Funciones = {

            // #region "Api"
            ApiBuscarUsuario: function () {

                let lista = [];

                $.ajax({
                    type: 'POST',
                    url: urlBuscarUsuario,
                    dataType: 'json',
                    contentType: 'application/json; charset=utf-8',
                    data: null,
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