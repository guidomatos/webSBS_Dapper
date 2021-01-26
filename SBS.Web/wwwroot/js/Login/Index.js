//variable para la invocacion del javascript
var login;

$(document).ready(function () {
    "use strict";
    var main;

    main = function () {
        var me = this;

        me.Globales = {

        };
        me.Init = {
            //funciones a usar dentro de la pagina
            InicializarEventos: function () {
                
            },
            InicializarAcciones: function () {
                if (errorLogin != "") {
                    me.Funciones.ShowMessage(errorLogin);
                }
            }
        };
        me.Eventos = {
            
        };
        me.Funciones = {
            ShowMessage: function (message) {
                $("#modal-error-message").html(message);
                $('#modal-error-id').modal('show');
            }
        };
        me.Inicializar = function () {
            //funciones a usar ni bien se cargue la pagina
            me.Init.InicializarEventos();
            me.Init.InicializarAcciones();
        };
    };

    login = new main();
    login.Inicializar();
});