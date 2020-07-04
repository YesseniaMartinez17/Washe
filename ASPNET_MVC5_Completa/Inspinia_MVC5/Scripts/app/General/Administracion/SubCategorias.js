

$(document).ready(
    () => {
        listarCategorias();
        obtenerdata();

        $('.money').click(
            () => {
                $('.money').val(0);
                $('.money').mask('0,000.A0', { translation: { 'A': { pattern: /[0-5]/, optional: true } } });
            }
        );

        $('#submitModalMod').click(
            function () {
                var id = $('#submitModalMod').attr("data-Ide");
                var titulo = $('#tituloModalMod').val();
                var descripcion = $('#descripcionModalMod').val();
                var precio = $('#precioModalMod').val();
                var fotografia = ($('#fotografia'))[0].files[0];
                var datastring = new FormData();
                //datastring.append("Nombre de lo que se quiere en el controlador", objeto);
                datastring.append("fotografia", fotografia);
                datastring.append("id", id);
                datastring.append("titulo", titulo);
                datastring.append("descripcion", descripcion);
                datastring.append("precio", precio);

                $.ajax({
                    url: '/Administracion/Modificar'
                    , type: "POST"
                    , data: datastring
                    , contentType: false
                    , processData: false
                    , async: false
                    , success: function (data) {
                        $("#ModalModificar").modal("hide");
                        toastr.success("Modificación realizada de forma exitosa");
                        obtenerdata();
                    }, error: function (data) {
                        toastr.error('Contacte con el administrador', 'Ha ocurrido un error');
                        obtenerdata();
                    }
                }
                );

            }
        );
        //$("input[type='button']").removeClass();
    }
);

function listarCategorias() {
    fetch('/Administracion/obtenerdata')
        .then(
            function (data) {
                return data.json();
        }).then(
        function (dataJson) {
            let conteo = dataJson.length;
            let listado = "";
            for (let x = 0; x < conteo; x++) {
                if (dataJson[x].cserv_Estado == true) {
                    listado += '<option value="' + dataJson[x].cserv_Id + '">' + dataJson[x].cserv_Descripcion + '</option>';
                }
            }
            $('#selectServicios option').remove();
            $('#selectServicios').append('<option value="-1">Todas</option>');
            $('#selectServicios').append(listado);
            }
        )
}

function obtenerdata(id) {
    var cols = "";
    let enviar = null;
    if (parseInt(id) > 0) {
        enviar = id;
    }
    //fetch('/Administracion/ListarSubcategorias' + enviar)
    //    .then(
    //        function (data) {
    //            return data.json();
    //        }
    //    ).then(
    //        function (dataJson) {
    //        }
    //    );\

    var url = '/Administracion/ListarSubcategorias';
    var data = { id: enviar };

    fetch(url, {
        method: 'POST', // or 'PUT'
        body: JSON.stringify(data), // data can be `string` or {object}!
        headers: {
            'Content-Type': 'application/json'
        }
    }).then(
        function (data) {
            return data.json();
        }
    ).then(
        function (dataJson) {
            for (var x = 0; x < dataJson.length; x++) {
                cols += '<tr>';
                cols += '<td>' + dataJson[x].serv_Titulo + '</td><td>' + dataJson[x].serv_Precio + ' Lps</td><td>' + dataJson[x].serv_Descripcion + `</td>
                <td><input class="btn btn-outline btn-info" type="button" onclick="Detalles(` + dataJson[x].serv_Id + `)" value="Detalles"/>`

                if (dataJson[x].serv_Estado == true) {
                    cols += `<input class="btn btn-outline btn-warning" data-id="` + dataJson[x].serv_Id
                        + `" data-descripcion="` + dataJson[x].serv_Descripcion
                        + `" data-precios="` + dataJson[x].serv_Precio
                        + `" data-titulo="` + dataJson[x].serv_Titulo
                        + `"  type="button" onclick="subCategoriaModificar(this)" value="Modificar"/>`;
                    cols += '<input class="btn btn-danger btn-outline" type="button" onclick="inhabilitar(' + dataJson[x].serv_Id + ')" value="Inhabilitar"/>';
                } else {
                    cols += `<input class="btn btn-outline btn-warning" type="button" onclick="toastr.warning('Registros inhabilitados no pueden ser modificados.');obtenerdata();" value="Modificar"/>`;
                    cols += '<input class="btn btn-success btn-outline" type="button" onclick="habilitar(' + dataJson[x].serv_Id + ')" value="Habilitar"/>';
                }
                cols += '</td>';
                cols += '</tr>';
            }
            $('#tbSubCategorias tr').remove();
            $('#tbSubCategorias').append(cols);


        }
        )
        //.catch(error => console.error('Error:', error))
        //.then(response => console.log('Success:', response))
        ;
}
function inhabilitar(id) {
    var url = "/Administracion/inhabilitarServicio";
    Generalinactivar(url, id, obtenerdata);
}

function habilitar(id) {
    var url = "/Administracion/habilitarServicio";
    Generalhabilitar(url, id, obtenerdata);
}

$('select#selectServicios').change(
    function () {
        var seleccionado = $(this).children("option:selected").val();
        obtenerdata(seleccionado);
    }
);



function Detalles(id) {
    //var id = $(btnDetalle).data("id");
    $('#ContenidoDetalle').load("/Administracion/_DetalleServicios/" + id,
        function () {
            $("#ModalDetalles").modal("show");
        });
}
function subCategoriaModificar(inpt) {
    let id = $(inpt).data("id");
    $('#tituloModalMod').val($(inpt).data("titulo"));
    $('#descripcionModalMod').val($(inpt).data("descripcion"));
    $('#precioModalMod').val($(inpt).data("precios"));
    $('#submitModalMod').attr("data-Ide", id);

    $('#ModalModificar').modal("show");

}
