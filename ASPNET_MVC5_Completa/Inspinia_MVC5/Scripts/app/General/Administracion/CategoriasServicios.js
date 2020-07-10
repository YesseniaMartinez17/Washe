var idG = 0;
$(document).ready(
    function () {
        obtenerdata();
    }
);
//.attr href

function detalles(id) {
    //var id = $(btnDetalle).data("id");
    $('#ContenidoDetalle').load("/Administracion/_Detalles/" + id,
        function () {
            $("#ModalDetalles").modal("show");
        });
}







function inactivar(id) {
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
                                        obtenerdata();
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
                                toastr.error('Inactivación anulada', 'Razón inactivación invalida.');
                            }
                            //swal('You typed:' + value);
                        });
                } else {
                    ////swal("Your imaginary file is safe!");
                }
        });
    }


//toastr.success('Without any options', 'Simple notification!')

    function REactivar(id) {
        swal(
            {
                title: "Desea habilitar este registro?"
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
                            obtenerdata();
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

$('.demo4').click(function () {
    swal("Write something here:", {
        content: "input",
    })
        .then((value) => {
            swal(`You typed: ${value}`);
        });
});

function validarForanea() {
    var token = '@Html.AntiForgeryToken()';
    token = $(token).val();
    return token;
}



$('#ModalModifica').on("hidden.bs.modal", function ()
    {
        $('.modaleditarbtn').remove();
    }
);

function Modificar(id) {
        const desc = $(id).data("descripcion");
        const inpt = $('#inptModifica');
        //console.log(desc);
        //console.log(id);
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
        obtenerdata();
}


$('.modaleditarbtn').click(
    function () {
        const id = $(this).data("id");
    }
);




function obtenerdata() {
    fetch("/Administracion/obtenerdata")
    .then(
        function (data) {
            return data.json();
        }
    ).then(
        function (dataJson) {
            //
            let listado = "";
            let conteo = dataJson.length;
            var cols = "";
            for (let x = 0; x < conteo; x++) {
                cols += '<tr>';
                cols += '<td>' + dataJson[x].cserv_Descripcion + '</td>'

                if (dataJson[x].cserv_Estado == true) {
                    cols += `<td><div class="btn-success btn-circle">
                            <i class="fa fa-check"></i>
                            </div></td>`;
                }else{
                    cols += `<td><div class="btn-warning btn-circle">
                                    <i class="fa fa-times"></i>
                                    </div></td>`;
                }

                let id = dataJson[x].cserv_Id;
                cols += `
                        <td>
                            <button data-id="` + id + `" onclick="detalles(`+id+`)" class="btnDetalles btn btn-outline btn-primary btn-sm">Detalles</button>
                        `;
                if (dataJson[x].cserv_Estado == true) {
                    cols += `
                                <button data-id="` + id + `" onclick="Modificar(` + id +`)"  data-descripcion="` + dataJson[x].cserv_Descripcion +`" class="btn btn-outline btn-warning btn-sm btnModificar">Modificar</button>
                                <button data-id="` + id + `" onclick="inactivar(`+id+`)" class="btn btn-outline btn-danger btn-sm btnInactivar">Inactivar registro</button><td>`;
                }
                else
                {
                    cols += `
                    <button data-id="` + id + `" onclick="inptdeshabilitado()" data-descripcion="` + dataJson[x].cserv_Descripcion +`" class="btn btn-outline btn-warning btn-sm">Modificar</button>
                    <button data-id="` + id + `" onclick="REactivar(` + id +`)"  class="btn btn-outline btn-success btn-sm btnREactivar">Activar registro</button></td>`;
                }
                cols += '</tr>';
                //if (dataJson[x] !== undefined) {
                //    listado += "<p>" + dataJson[x].cserv_Descripcion + "</p>";
                //}                

            }
            console.log(cols);


            $('#tablainicial tr').remove();
            $('#tablainicial').append(cols);




            //let frase = "pato";
            //var cols = '<td>' + frase + '</td><td>more data</td >';
            //$('#tablainicial').append('<tr>' + cols + '</tr >');
        }
    )
}


$('#pruebarellena').click(
    () => {
        obtenerdata();
    }
);


//let frase = "pato";
//$('#tablainicial').append('<tr><td>' + frase + '</td><td>more data</td ></tr >');

//@if (item.cserv_Estado == true) {
//    <div class="btn-success btn-circle">
//        <i class="fa fa-check"></i>
//    </div>
//}
//else {
//    <div class="btn-warning btn-circle">
//        <i class="fa fa-times"></i>
//    </div>
//}




//$('.btn_modificar_deshabilitado').click(
//    () => {
//        toastr.warning('', 'No se puede modificar un registro deshabilitado');
//        $(this).focusout();
//    }
//);


function inptdeshabilitado() {
    toastr.warning('', 'No se puede modificar un registro deshabilitado');
    obtenerdata();
}