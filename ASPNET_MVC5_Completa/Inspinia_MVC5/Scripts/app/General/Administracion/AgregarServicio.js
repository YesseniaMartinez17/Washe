
var servs = new Array();
var descs = new Array();
var fotos = new Array();
var precios = new Array();
var cats = new Array();
var ids = new Array();

$(document).ready(
    () => {
        var id = 0;

        listarCategorias();

        $('.money').click(
            () => {
                $('.money').val(0);
                $('.money').mask('0,000.A0', { translation: { 'A': { pattern: /[0-5]/, optional: true } } });
            }
        );
        $('#agregar').click(
            () => {
                let titulo = $('#titulo').val();
                let descripcion = $('#descripcion').val();
                let precio = $('#precio').val();
                let imagen = $('#fotografia').val();
                var seleccionId = $('#selectServicios').children("option:selected").val();
                let seleccionDesc = $("#selectServicios option:selected").text();

                let valida = true;

                descripcion == "" ? ($('#descripcion').addClass(" error"), valida = false) : ($('#descripcion').removeClass(" error"), (valida == true ? valida = true : valida = false));
                precio <= 0 ? ($('#precio').addClass(" error"), valida = false) : valida = ($('#precio').removeClass(" error"), (valida == true ? valida = true : valida = false));
                $('#fotografia').get(0).files.length == 0 ? ($('#mensajeFotografia').addClass(" text-danger"), valida = false) : ($('#mensajeFotografia').addClass(" text-danger"), (valida == true ? valida = true : valida = false));
                seleccionId == '-1' ? ($('#selectServicios').addClass(" error"), valida = false) : ($('#selectServicios').removeClass(" error"), (valida == true ? valida = true : valida = false));
                titulo == "" ? ($('#titulo').addClass(" error"), valida = false) : ($('#titulo').removeClass(" error"), (valida == true ? valida = true : valida = false));

                if (valida == true) {
                    id++;
                    //aqui los agregamos a los arreglos
                    ids.push(id);
                    servs.push(titulo);
                    descs.push(descripcion);
                    precios.push(precio)
                    fotos.push(($('#fotografia'))[0].files[0]);
                    cats.push(seleccionId);
                    var idImg = 'img-detalles-' + id;
                    $('#Insertar').css("display", "block");
                    $('div #descripcionesservicios .tab-pane').removeClass("active");
                    $('#listadoRegistros li').removeClass("active");
                    $('#listadoRegistros').append('<li class="active animated fadeInDown" id="li-' + id + '"><a data-toggle="tab" href="#tab-' + id + '" aria-expanded="true"><i id="' + id + '" onclick="remover(this)" class="fa fa-trash"></i>' + titulo + '</a></li>');
                    $('#descripcionesservicios').append(`<div id="tab-` + id + `" class="tab-pane active">
                        <div class="panel-body">
                            <strong>`+ titulo + `</strong>

                            <p>
                                `+ descripcion + `
                            </p>
                                <br>
                                <strong>Precio:</strong> `+ precio +` Lps.<br>
                                <strong>Categoría:</strong> `+ seleccionDesc +`<br>

                            <h3>
                                <img data-id="`+ idImg + `" id="` + idImg + `" class="img-thumbnail" src="" style="width:50%;height:"30%"/>
                            </h3>
                        </div>
                    </div>`);
                    $('#Insertar').css("display", "block");

                    $('#Insertar').removeClass(' fadeOutRight');
                    $('#Insertar').addClass(' fadeInLeft');
                    $('#img-detalles-' + id).attr("src", $('#ocultoimg').attr("src"));

                    $('#titulo').val('');
                    $('#descripcion').val('');
                    $('#precio').val(0);
                    $('#fotografia').val('');
                    $('#DivFotografia').removeClass();
                    $('#DivFotografia').addClass('fileinput input-group fileinput-new');
                    
                    $('#selectServicios').val('-1');
                }
            }
        );

        $("#fotografia").change(function () {
            readURL(this);
        });

        $('#btnConfirmar').click(
            () => {
                var datos = new FormData();
                
                let l = ids.length;
                for (var x = 0; x < l; x++) {
                    datos.append("cserv_Id", cats[x]);//id cat
                    datos.append("serv", servs[x]);//titulo
                    datos.append("serv_desc", descs[x]);
                    datos.append("precios", precios[x]);
                    datos.append("serv_Image", fotos[x]);
                }

                $.ajax({
                    url: "/Administracion/InsertarServicio"
                    , type: "POST"
                    , data: datos
                    , contentType: false
                    , processData: false
                    , async: false
                    , success: (data) => {
                        if (data.value == false) {
                            toastr.error("Ha ocurrido un error no identificado, favor reintentarlo mas tarde");
                        } else {
                            if (data.Message == "Exito") {
                                window.location.replace('/Administracion/AdministrarSubcategorias');
                            } else if (data.Message == "Duplicado") {
                                var c = data.Comentario;
                                toastr.warning(c,'Modifique el nombre de los siguientes servicios ya que son repetitivos');
                            }
                        }
                    }, error: (data) => {
                        toastr.error('Ocurrio un error no identificado favor contacte con el administrador');
                    }
                });

            }
        );
    }
);

function readURL(input) {
    if (input.files && input.files[0]) {
        var reader = new FileReader();

        reader.onload = function (e) {
            $('#ocultoimg').attr('src', e.target.result);
        }

        reader.readAsDataURL(input.files[0]); // convert to base64 string
    }
}

function remover(input) {
    let id = $(input).attr('id');
    swal({
        title: "Desea remover este registro?"
        , icon: "warning"
        , buttons: ["Cancelar", "Remover"]
        , dangermode: true
    }).then(
        (confirmado) => {
            if (confirmado) {
                let idList = '#listadoRegistros li #li-' + id;
                let iddesc = $('tab-' + id);
                let idr = ids.indexOf(parseInt(id));

                ids.splice(idr, 1);
                servs.splice(idr, 1);
                descs.splice(idr, 1);
                fotos.splice(idr, 1);
                precios.splice(idr, 1);
                cats.splice(idr, 1);

                var l = ids.length;
                if (l >= 1) {
                    $('li#li-' + ids[0]).addClass(" active");
                    $('div#tab-' + ids[0]).addClass(" active");
                } else {
                    $('#Insertar').addClass(' animated fadeOutRight');
                }


                $('li#li-' + id).removeClass(" fadeInDown");
                $('li#li-' + id).addClass(" fadeOutUp");


                $('div#descripcionesservicios div#tab-' + id).remove();
                setTimeout(
                    () => {
                        $('li#li-' + id).remove();
                    },0700
                );

            }
        }
    );
}




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
                    $('#selectServicios').append('<option value="-1">Todas</option>');
                    $('#selectServicios').append(listado);
                }
            )
}
        //$('li#1').remove();
        //$('div#tab-7').remove();