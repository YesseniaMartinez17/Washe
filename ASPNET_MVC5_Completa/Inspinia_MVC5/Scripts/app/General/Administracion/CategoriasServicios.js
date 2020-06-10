var idG = 0;
$(document).ready(
    function () {
        //alert();
    }
);

$('.btnDetalles').click(
    function () {
        var id = $(this).data("id");
        $('#ContenidoDetalle').load("/Administracion/_Detalles/" + id,
            function () {
                $("#ModalDetalles").modal("show");
            });
    }
);
//.attr href

$('.btnInactivar').click(
    function () {
        var id = $(this).data("id");
        idG = id;
        swal({
            title: "Confirmar."
            , text: "Esta seguro que desea inactivar este registro?"
            , icon: "error"
            //, buttons: ["Cancelar","Proceder"]
            , buttons: ["cancelar","Proceder"]
            , dangerMode: true
        })
            .then((willDelete) => {
                if (willDelete) {
                    swal("Razón de inactivación:","(mínimo 10 caracteres)", {
                        content: "input"
                        , icon: "warning"
                    })
                        .then((value) => {
                            if (value.length > 10) {

                                //alert('el id es:' + id);
                                //post(value);
                                
                                const token = $("[name='__RequestVerificationToken']").val();
                                $.ajax(
                                    //
                                    {
                                        method: "POST"
                                        , url: "/Administracion/inactivar"
                                        , data: {
                                            __RequestVerificationToken: token
                                            ,id: id
                                            , razon: value
                                        }
                                    }
                                ).done(
                                    () =>   {
                                        toastr.success('El registro fue inhabilitado de forma exitosa', 'Éxito');
                                            }
                                ).fail(
                                        () => {
                                            toastr.error('Contacte con el administrador', 'Ha ocurrido un error');
                                               }
                                ).always(
                                        () =>   {
                                            //
                                                }
                        );



                            } else {
                                toastr.fail('Inactivación anulada', 'Razón inactivación invalida.');
                            }
                            //swal('You typed:' + value);
                        });
                } else {
                    ////swal("Your imaginary file is safe!");
                }
            });
    }
);


//toastr.success('Without any options', 'Simple notification!')

$(".btnREactivar").click(
    function () {
        var id = $(this).data("id");
        swal(
            {
                title: "Desea reactivar este registro?"
                , text: "(El registro estara nuevamente disponible)"
                , buttons: {
                    cancel: "Cancelar"
                    , confirmar: true
                }
                , icon: "warning"
            }
        ).then(
            (value) => {
                if (value) {
                    const token = $("[name='__RequestVerificationToken']").val();
                    $.ajax(
                        {
                            url: "/Administracion/REactivar"
                            , method: "POST"
                            , data: {
                                id: id
                                , __RequestVerificationToken: token
                            }
                        }
                    ).success(
                        //
                        () => {
                            toastr.success('El registro fue habilitado de forma exitosa', 'Éxito');
                        }
                    ).fail(
                        () => {
                            toastr.error('Favor intentarlo de nuevo mas tarde o contactar con el administrador', 'Ha ocurrido un error');
                        }
                    );
                }
            }    
        );
    }
);

$('.demo4').click(function () {
    swal("Write something here:", {
        content: "input",
    })
        .then((value) => {
            swal(`You typed: ${value}`);
        });
});



$('#btnConfirmarInact').click(
    function() {
        alert($('#inptInactivacion').val());
    }
);


$('#prueba').click(
    () => {
        alert(idG);
    }
);

function validarForanea() {
    var token = '@Html.AntiForgeryToken()';
    token = $(token).val();
    return token;
}




$('.btnModificar').click(
    function () {
        const id = $(this).data("id");
        const desc = $(this).data("descripcion");
        const inpt = $('#inptModifica');
        var footer = document.createElement('input');
        footer.setAttribute("type","button");
        footer.setAttribute("value", "Confirmar");
        footer.setAttribute("id", id);
        footer.setAttribute("data-id", id);
        footer.setAttribute("class", "btn btn-success modaleditarbtn");
        inpt.attr("placeholder",desc);
        //footer.setAttribute("","");
        //footer.setAttribute("", "");
        $('#contenidoModificar').append(footer);

        $('#ModalModifica').modal("show");
    }
);


$('#ModalModifica').on("hidden.bs.modal", function () {
        $('.modaleditarbtn').remove();
    }
);

$('.modaleditarbtn').click(
    function () {
        const id = $(this).data("id");
    }
);


$('.btn_modificar_deshabilitado').click(
    () => {
        toastr.warning('', 'No se puede modificar un registro deshabilitado');
        $(this).focusout();
    }
);