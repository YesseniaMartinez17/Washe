
var id = [];
var desc = [];
var precio = [];


$('.Idetalles').click(
    function () {
        const id = $(this).attr('data-Id');
        console.log(id);
        console.log($(this).attr('data-imgsrc'));
        console.log($(this).attr('data-desc'));


         
        $('#ContenidoDetalles').load("/Servicios/externodetalles/" + id, () => {
            $('#fotografiaDetalleSuscripcion').attr('src', $(this).attr('data-imgsrc'));
            $('#descripcionDetalles').text($(this).attr('data-desc'));
            $('#ModalDetalles').modal('show')
        });
    });

$('.objetosServicios').click(
    function (eventt) {
        var clicked = eventt.target.nodeName;
        if (clicked == 'DIV') {
            var idObj = $(this).data("value");
            var descObj = $(this).data("descripcion");
            var precioObj = $(this).data("precio");

            if ($(this).hasClass("active")) {
                $(this).removeClass(" active");
                const indexR = id.indexOf(idObj);
                if (indexR > -1) {

                    id.splice(indexR, 1);
                    desc.splice(indexR, 1);
                    precio.splice(indexR, 1);
                }
            } else {
                $(this).addClass(" active ");
                id.push(idObj);
                desc.push(descObj);
                precio.push(precioObj);
            }
            //$(location).attr('href', '/Servicios/');
            if (id.length < 1) {
                $('#confirmar').addClass("animated fadeOut");
                setTimeout(
                    function () {
                        $('#confirmar').css("visibility", "hidden");
                    }, 300);
                //ocultar($('#confirmar'));

            } else if (id.length == 1) {
                $('#confirmar').css("visibility", "visible");
                $('#confirmar').addClass("animated fadeIn");
                setTimeout(
                    function () {
                        $('#confirmar').removeClass();
                    }, 300);
            }
        } else {
            //alert('patoFunciona');
        }
    }

);

$('#Continuar').click(
    function () {
        $(location).attr('href', '/Servicios/solicitar?id=' + id);//+ '&desc=' + desc + '&precio=' + precio);
    });   