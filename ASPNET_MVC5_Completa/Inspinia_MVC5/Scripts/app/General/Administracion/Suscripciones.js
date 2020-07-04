$(document).ready(
    () => {
        listarSuscripciones();
        $('.money').val(0);
        $('.money').mask('0,000.A0', { translation: { 'A': { pattern: /[0-5]/, optional: true } } });
        $('.nmbrentero').val(0);
        $('.nmbrentero').mask('A0', { translation: { 'A': { pattern: /[1-9]/, optional: false } } });
        

        $('#AgregarSuscripción').click(
            () => {
                listarServiciosAgregar();
                $('#ModalAgregar').modal("show");
            }
        );



        $('#agregarSuscripcion').click(
            () => {

                let desc = $('#DescripcionAgregar').val();
                let precio = $('#precioAgregar').val();
                let vigencia = $('#vigenciaAgregar').val();
                let serv = $('#SelectServicioAgregar').val();
                let maxsolicitudes = $('#solicitudesAgregar').val();


                let valida = true;

                desc == "" ? ($('#DescripcionAgregar').addClass(" error"), valida = false) : ($('#DescripcionAgregar').removeClass(" error"), (valida == true ? valida = true : valida = false));
                precio <= 0 ? ($('#precioAgregar').addClass(" error"), valida = false) : valida = ($('#precioAgregar').removeClass(" error"), (valida == true ? valida = true : valida = false));
                serv == '-1' ? ($('#SelectServicioAgregar').addClass(" error"), valida = false) : ($('#SelectServicioAgregar').removeClass(" error"), (valida == true ? valida = true : valida = false));
                maxsolicitudes < 1 ? ($('#solicitudesAgregar').addClass(" error"), valida = false) : ($('#solicitudesAgregar').removeClass(" error"), (valida == true ? valida = true : valida = false));
                vigencia < 1 ? ($('#vigenciaAgregar').addClass(" error"), valida = false) : ($('#vigenciaAgregar').removeClass(" error"), (valida == true ? valida = true : valida = false));

                if (valida) {
                    const token = $("[name='__RequestVerificationToken']").val();
                $.ajax(
                    //
                    {
                        method: "POST" 
                        , url: "/Administracion/InsertarSuscripcion"
                        , data: {
                            serv_Precio: precio,
                            serv_Id: serv,
                            meses: vigencia,
                            cantidad: maxsolicitudes,
                            serv_Descripcion: desc,
                            __RequestVerificationToken: token
                       }
                    }
                ).done(
                    (data) => {
                        if (data.Message == "Duplicado") {
                            //Suscripción normal motocicleta
                            toastr.warning('Ya existe una suscripción con este nombre.');
                        } else if (data.Message == "Exito") {
                            toastr.success('El registro agrego de forma exitosa a la base de datos', 'Éxito');
                            $("#ModalAgregar").modal('hide');
                            listarSuscripciones();
                        }
                    }
                ).fail(
                    () => {
                        toastr.error('Contacte con el administrador', 'Ha ocurrido un error no identificado');
                    }
                ).always(
                    () => {
                    });
                }

            }
        );


        $("#ModalAgregar").on("hidden.bs.modal", function () {
            limpiezaSuscripcion();
        });

        $('#DescripcionModificar').click(
            function () {
                $(this).val('');
            }
        );
        $('#modificarSuscripcion').click(
            function () {
                let desc = $('#DescripcionModificar').val();
                let precio = $('#precioModificar').val();
                let maxsolicitudes = $('#solicitudesModificar').val();
                let vigencia = $('#vigenciaModificar').val();
                let id = $('#modificarSuscripcion').attr('data-id');

                let valida = true;
                desc == "" ? ($('#DescripcionModificar').addClass(" error"), valida = false) : ($('#DescripcionModificar').removeClass(" error"), (valida == true ? valida = true : valida = false));
                precio <= 0 ? ($('#precioModificar').addClass(" error"), valida = false) : valida = ($('#precioModificar').removeClass(" error"), (valida == true ? valida = true : valida = false));
                maxsolicitudes < 1 ? ($('#solicitudesModificar').addClass(" error"), valida = false) : ($('#solicitudesModificar').removeClass(" error"), (valida == true ? valida = true : valida = false));
                vigencia < 1 ? ($('#vigenciaModificar').addClass(" error"), valida = false) : ($('#vigenciaModificar').removeClass(" error"), (valida == true ? valida = true : valida = false));

                if (valida) {
                    //ajax
                    const token = $("[name='__RequestVerificationToken']").val();
                    $.ajax({
                        url: "/Administracion/UpdateSuscripcion",
                        method: "POST",
                        data: {
                            __RequestVerificationToken: token,
                            sus_Id: id,
                            precio: precio,
                            descripcion: desc,
                            cantidadM: maxsolicitudes,
                            vigenciaM: vigencia
                        }
                    }).done(
                        () => {
                            toastr.success('Modificación realizada con exitosa', 'Exito');
                        }
                    ).fail(
                        () => {
                            toastr.error('Favor intentarlo de nuevo mas tarde o contacte con el administrador', 'Ha ocurrido un error');
                        }
                    ).always(
                        () => {
                            $("#ModalModificar").modal('hide');
                            listarSuscripciones();
                        });
                }
            }
        ); 

            $("#ModalModificar").on("hidden.bs.modal", function () {
            $('#ModalModificar input[type=text]').val('');
            $('#ModalModificar input[type=number]').val(1);
                
            limpiezaSuscripcion();
            });


    }// funcion document ready
);



function inhabilitar(id) {
    var url = "/Administracion/InhabilitarSuscripcion";
    Generalinactivar(url, id, listarSuscripciones);
}

function habilitar(id) {
    var url = "/Administracion/habilitarSuscripcion";
    Generalhabilitar(url, id, listarSuscripciones);
}

function listarSuscripciones() {
    let url = "/Administracion/ListarSuscripciones";
    fetch("/Administracion/ListarSuscripciones")
    .then(
            (data) => {
                 return data.json();  
            }
    ).then(
        (dataJson) => {
            let conteo = dataJson.length;
            var cols = "";
            var id = undefined;
            for (var x = 0; x < conteo; x++) {
                cols += '<tr class="animated fadeInDown">';


                if (dataJson[x].sus_Estado == true) {
                    cols += `<td><div class="btn-success btn-circle">
                            <i class="fa fa-check"></i>
                            </div></td>`;
                } else {
                    cols += `<td><div class="btn-warning btn-circle">
                                    <i class="fa fa-times"></i>
                                    </div></td>`;
                }
                id = dataJson[x].sus_Id; 


                cols += '<td>' + dataJson[x].sus_Descripcion + '</td>';
                cols += '<td>' + dataJson[x].serv_Descripcion + '</td>';
                cols += '<td>' + dataJson[x].sus_Precio + '</td>';
                //cols += '<td>' + dataJson[x].sus_MesesVigencia + '</td>';
                //cols += '<td>' + dataJson[x].sus_CantidadMaximaSolicitudes + ' mensual </td>';


                cols += `
                        <td>
                            <button data-id="` + id + `" onclick="detallesSuscripcion(` + id + `)" class="btnDetalles btn btn-outline btn-primary btn-sm">Detalles</button>
                        `;
                if (dataJson[x].sus_Estado == true) {
                    cols += `
                                <button data-id="` + id + `" onclick="Modificar(this)"  data-descripcion="` + dataJson[x].sus_Descripcion + `" class="btn btn-outline btn-warning btn-sm btnModificar">Modificar</button>
                                <button data-id="` + id + `" onclick="inhabilitar(` + id + `)" class="btn btn-outline btn-danger btn-sm btnInactivar">Inactivar registro</button><td>`;
                }
                else {
                    cols += `
                    <button data-id="` + id + `" onclick="inptdeshabilitadomodificar()" data-descripcion="` + dataJson[x].sus_Descripcion + `" class="btn btn-outline btn-warning btn-sm">Modificar</button>
                    <button data-id="` + id + `" onclick="habilitar(` + id + `)"  class="btn btn-outline btn-success btn-sm btnREactivar">Activar registro</button></td>`;
                }

                cols += '</tr>';
            }
            $('#tbSuscripciones tr').remove();
            $('#tbSuscripciones').append(cols);

        }
    )
}

function Modificar(inpt) {
    let id = $(inpt).attr('data-id');
    let descAnterior = $(inpt).attr('data-descripcion');
    $('#ModalModificar').modal('show');
    $('#DescripcionModificar').val(descAnterior);
    $('#modificarSuscripcion').attr("data-id", null);
    $('#modificarSuscripcion').attr("data-id", id);
}

function inptdeshabilitadomodificar() {
    toastr.warning('', 'No se puede modificar un registro deshabilitado');
    listarSuscripciones();
}

function listarServiciosAgregar() {
    fetch('/Administracion/listarServicios')
        .then(
            (data) => {
                return data.json();
            }
        ).then(
        (datajson) => {
            let l = datajson.length;
            var cols = "";
            cols += '<option value="-1">Seleccione</option>';
            $('#selectServicios').append('<option value="-1">Todas</option>');
            for (var x = 0; x < l; x++) {
                cols += '<option value="' + datajson[x].serv_Id + '">' + datajson[x].serv_Descripcion + '</option>'
            }
            $('#SelectServicioAgregar option').remove();
            $('#SelectServicioAgregar').append(cols);
        });

}



function detallesSuscripcion(id) {
    //var id = $(btnDetalle).data("id");
    $('#ContenidoDetalles').load("/Administracion/DetallesSuscripcion/" + id,
        function () {
            $("#ModalDetalles").modal("show");
        });
}




function limpiezaSuscripcion() {
    $('#DescripcionAgregar').val('');
    $('#precioAgregar').val(0);
    $('#vigenciaAgregar').val(1);
    $('#SelectServicioAgregar').val(0);
    $('#solicitudesAgregar').val(1);
};
