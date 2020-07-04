

function Generalinactivar(url, id, funcionCallbackListar) {
    idG = id;
    swal({
        title: "Confirmar."
        , text: "Esta seguro que desea inactivar este registro?"
        , icon: "error"
        //, buttons: ["Cancelar","Proceder"]
        , buttons: ["cancelar", "Proceder"]
        , dangerMode: true
    })
        .then((willDelete) => {
            if (willDelete) {
                swal("Razón de inactivación:", "(mínimo 10 caracteres)", {
                    content: "input"
                    , icon: "warning"
                })
                    .then((value) => {
                        if (value.length > 10) {

                            //post(value);

                            const token = $("[name='__RequestVerificationToken']").val();
                            $.ajax(
                                //
                                {
                                     
                                    method: "POST"
                                    , url: url
                                    , data: {
                                        Id: id
                                        , RazonInactivo: value
                                        , __RequestVerificationToken: token
                                    }
                                }
                            ).done(
                                function (data) {
                                    toastr.success('El registro fue inhabilitado de forma exitosa', 'Éxito');
                                }
                            ).fail(
                                () => {
                                    toastr.error('Contacte con el administrador', 'Ha ocurrido un error');
                                }
                            ).always(
                                () => {
                                    funcionCallbackListar();
                                }
                            );



                        } else {
                            toastr.error('Inactivación anulada', 'Razón inactivación invalida.');
                        }
                        //swal('You typed:' + value);
                    });
            } else {
                ////swal("Your imaginary file is safe!");
            }
        });
}



function Generalhabilitar(url, id, funcionExitoCallback) {
    swal(
    {
        title: "Desea habilitar este registro?"
        , text: "(El registro estara nuevamente disponible)"
        , buttons: {
            cancel: "Concelar"
            , Confirmar: true
        }
        , icon: "warning"
    }
    ).then(
        (value) => {
            if (value) {
                const token = $("[name='__RequestVerificationToken']").val();
                $.ajax({
                    url: url
                    , method: "POST"
                    , data: {
                        id: id
                        , __RequestVerificationToken: token
                    }
                }).success(
                    () => {
                        toastr.success("El registro se ha sido habilitado de forma exitosa. ", "Éxito");
                    }).fail(
                        () => {
                            toastr.error('Favor intentarlo de nuevo mas tarde o contactar con el administrador', 'Ha ocurrido un error');
                        }).always(
                            () => {
                                funcionExitoCallback();
                            });
            }
        }
    );
};