
var arreglo = [];
var arregloCantidad = [];

var actual = 0;
var anterior = 0;
var patron = new Array(2);

var coordenadaUsuarioLat;
var coordenadaUsuarioLng;
function codigo(event) {
    var codigo = event.key; //which || event.keyCode;
    //if (codigo === 13) {
    //    console.log("Tecla ENTER");
    //}
    return codigo;
}

var lat1 = 0;
var long = 0;
var coordenadaUsuario;


$(document).ready(function () {

    var l;
    arreglo = $('#viewl').val().split(',');
    l = arreglo.length;
    for (var x = 0; x < l; x++) {
        arregloCantidad[x] = 1;
    }
    console.log('La cantidad es : ' + l);
    console.log('El arreglo de cantidad es : ' + arregloCantidad + arregloCantidad[0]);


    if ("geolocation" in navigator) {
        navigator.geolocation.getCurrentPosition(function (position) {
            lat1 = position.coords.latitude;
            long = position.coords.longitude;
            //console.log("Found your location nLat : " + lat1 + " nLang :" + long);
            coordenadaUsuario = new google.maps.LatLng(lat1, long);
            //console.log('Aqui es: ' + coordenadaUsuario);
            init();
        });


    } else {
        console.log("Browser doesn't support geolocation!");
    }

});



//google.maps.event.addDomListener(window, 'load', init);


function init() {

    var mapElement1 = document.getElementById('map1');

    //console.log('finalmente var coordenadaUsuario es: ' + coordenadaUsuario);
    if (typeof coordenadaUsuario === "undefined") {

        coordenadaUsuario = { lat: 15.5038827, lng: -88.01386190000001 };
        //console.log(coordenadaUsuario);
    } else {
        coordenadaUsuario = { lat: lat1, lng: long }
        //console.log(coordenadaUsuario);
    }

    var mapOptions1 = {
        zoom: 10,
        center: coordenadaUsuario,
        streetViewControl: false,
        // Style for Google Maps
        //styles: [{ "featureType": "water", "stylers": [{ "saturation": 43 }, { "lightness": -11 }, { "hue": "#0088ff" }] }, { "featureType": "road", "elementType": "geometry.fill", "stylers": [{ "hue": "#ff0000" }, { "saturation": -100 }, { "lightness": 99 }] }, { "featureType": "road", "elementType": "geometry.stroke", "stylers": [{ "color": "#808080" }, { "lightness": 54 }] }, { "featureType": "landscape.man_made", "elementType": "geometry.fill", "stylers": [{ "color": "#ece2d9" }] }, { "featureType": "poi.park", "elementType": "geometry.fill", "stylers": [{ "color": "#ccdca1" }] }, { "featureType": "road", "elementType": "labels.text.fill", "stylers": [{ "color": "#767676" }] }, { "featureType": "road", "elementType": "labels.text.stroke", "stylers": [{ "color": "#ffffff" }] }, { "featureType": "poi", "stylers": [{ "visibility": "off" }] }, { "featureType": "landscape.natural", "elementType": "geometry.fill", "stylers": [{ "visibility": "on" }, { "color": "#b8cb93" }] }, { "featureType": "poi.park", "stylers": [{ "visibility": "on" }] }, { "featureType": "poi.sports_complex", "stylers": [{ "visibility": "on" }] }, { "featureType": "poi.medical", "stylers": [{ "visibility": "on" }] }, { "featureType": "poi.business", "stylers": [{ "visibility": "simplified" }] }]
    };

    var map1 = new google.maps.Map(mapElement1, mapOptions1);


    var marker = new google.maps.Marker({
        position: coordenadaUsuario,
        map: map1,
        draggable: false//true
    });

    google.maps.event.addListener(marker, 'drag', function (event) {
        $('#lattt').text(event.latLng.lat());
        $('#longgg').text(event.latLng.lng());
    });


    var card = document.getElementById('pac-card');
    var input = document.getElementById('pac-input');

    map1.controls[google.maps.ControlPosition.TOP_RIGHT].push(card);

    var autocomplete = new google.maps.places.Autocomplete(input);

    // Bind the map's bounds (viewport) property to the autocomplete object,
    // so that the autocomplete requests use the current map bounds for the
    // bounds option in the request.
    autocomplete.bindTo('bounds', map1);

    // Set the data fields to return when the user selects a place.
    autocomplete.setFields(
        ['address_components', 'geometry', 'icon', 'name']);

    autocomplete.addListener('place_changed', function () {
        marker.setVisible(false);
        var place = autocomplete.getPlace();
        if (!place.geometry) {
            // User entered the name of a Place that was not suggested and
            // pressed the Enter key, or the Place Details request failed.
            window.alert("No details available for input: '" + place.name + "'");
            return;
        }

        // If the place has a geometry, then present it on a map.
        if (place.geometry.viewport) {
            map1.fitBounds(place.geometry.viewport);
        } else {
            map1.setCenter(place.geometry.location);
            map1.setZoom(17);  // Why 17? Because it looks good.
        }
        marker.setPosition(place.geometry.location);

        var pos = place.geometry.location.lat();
        var pos1 = place.geometry.location.lng();

        $('#lattt').text(pos);
        $('#longgg').text(pos1);


        coordenadaUsuarioLat = pos;
        coordenadaUsuarioLng = pos1;

        //Cambio de direccion
        console.log(coordenadaUsuarioLat);
        console.log(coordenadaUsuarioLng);

        $('#Ubicacion').text($('#pac-input').val());
        $('#confirmar').prop("disabled",false);

        marker.setVisible(true);



        var address = '';
        if (place.address_components) {
            address = [
                (place.address_components[0] && place.address_components[0].short_name || ''),
                (place.address_components[1] && place.address_components[1].short_name || ''),
                (place.address_components[2] && place.address_components[2].short_name || '')
            ].join(' ');
        }
    });//autocomplete


    //var ConfigDR = {
    //    map: map1
    //}

    //var ConfigDS = {
    //    origin: coordenadaUsuario,
    //    destination: new google.maps.LatLng(15.728550760877717, -87.90162748021238),
    //    travelMode: 'DRIVING'
    //}

    //var ds = new google.maps.DirectionsService();

    //var ds = new google.maps.DirectionsService();
    ////var dr = new google.maps.DirectionsRenderer();

    //var dr = new google.maps.DirectionsRenderer(
    //    ConfigDR
    //);

    //ds.route(ConfigDS, function(result, status) {
    //    if (status == 'OK') {
    //        dr.setDirections(result);
    //    } else {
    //        alert('Ruta invalida' + status);
    //    }
    //});



}//init





//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

var pedidos = [];
var larrpedido;
var ideS;
var posPedido;
var actual_c;
function preformatear(ide) {
    //console.log('el id es: ' + ide); 
    var a = 0;

    var campo = $('#' + ide);

    anterior = campo.val();

    campo.keydown(
        function (l) {
            a = parseInt(codigo(l));
            campo.val(a);
            actual = a;
            return '';
        });

    campo.keyup(
        function (l) {
            //console.log('array: ' + arreglo);
            //console.log(arreglo);
            //console.log(ide);
            ideS = ide.toString();
            //console.log(typeof(ideS));
            //console.log(ideS);
            posPedido = arreglo.indexOf(ideS);
            console.log('esta en la pocision: = ' + posPedido);

            //larrpedido = pedidos.length;
            //if (larrpedido !== 0) {
            //    pedidos[larrpedido+1] = ide;
            //    console.log('el actual pedido ' + larrpedido+1 + ' es: ' + pedidos[larrpedido]);
            //} else {
            //    pedidos[larrpedido] = ide;
            //    console.log('el actual pedido ' + larrpedido +' es: ' + pedidos[larrpedido]);
            //}
            //let a = parseInt(codigo(l));
            if (campo.val() == 0) {
                campo.val(1);
                actual = 1;
            }
            if (actual !== 0) {
                actual_c = actual;
            }
            //console.log('actual: ' + actual_c);
            arregloCantidad[posPedido] = actual_c;
            //console.log(arreglo[posPedido]);
            //console.log(arregloCantidad[posPedido]);
            //console.log(arreglo);
            //console.log(arregloCantidad);
            //console.log('El id: ' + arreglo[posPedido] + ' fue solicitado: ' + arregloCantidad[posPedido] + ' veces');
            ////console.log('Anterior: ' + anterior);
            ////console.log('Actual: ' + actual);

            if (actual != anterior) {
                if (actual > anterior) {
                    r = actual - anterior;
                    incremento(r, campo, 1);
                } else if (actual < anterior) {
                    r = anterior - actual;
                    incremento(r, campo, 2);
                }
            }
            actual = 0;
            anterior = 0;
            campo.blur();
            return '';
        });
}

var res = 0;

function incremento(r, campoId, op) {
    var precio = parseFloat(campoId.data('precioproducto'));
    var total = parseFloat($('#lblTotalPagar').text());

    //console.log(precio);
    //alert(total);

    for (x = 1; x <= parseInt(r); x++) {
        res = res + precio;
    }

    if (op == 1) {
        //res = res - precio;
        total = total + res;
    } else if (op == 2) {
        //res = res - precio;
        total = total - res;
    }

    $('#lblTotalPagar').text(total + 'Lps');
    //console.log(r);
    //console.log(campoId);
    res = 0;
}

$('#confirmar').click(
    function () {
        //alert(coordenadaUsuarioLat);
        //alert(coordenadaUsuarioLng);
        var lugar = $('#pac-input').val();
        $.post("/Servicios/SolicitarPedido", {
            IdPedidos: arreglo,
            Cantidades: arregloCantidad,
            coordenadas: coordenadaUsuario,
            ubicacion: lugar,
            Lat: coordenadaUsuarioLat,
            Lng: coordenadaUsuarioLng,
        }, function (data, resultado) {
            console.log(data);
            console.log(resultado);
            //aregla en el punto success muestre un div de colores de exito y luego de eso a los 5 segundos redireccione pero bloqueando los botones
            }).done(function () {

                swal({
                    title: "Exito!",
                    text: "Solicitud de servicios realizada de forma exitosa!",
                    type: "success"
                });
                $('.confirm').css("display", "none");
                //window.location = "/Acceso/Index";

                setTimeout(
                    function () {
                        window.location = "/Acceso/Index";
                    },1500);
            })
            .fail(
                function () {
                    swal("Error", "A ocurrido un error favor intentarlo de nuevo mas tarde", "error");
                }
            )
            .always(
            function () {
                //
            }
            );
    }
);
var l = $('.animado').ladda();
l.click(function () { // Start loading
    l.ladda('start');
    // Do something in backend and then stop ladda
    // setTimeout() is only for demo purpose
    setTimeout(function () {
        l.ladda('stop');
    }, 2000)

      } );


//Esta es para la alerta
//swal({
//    title: "Are you sure?",
//    text: "You will not be able to recover this imaginary file!",
//    type: "warning",
//    showCancelButton: true,
//    confirmButtonColor: "#DD6B55",
//    confirmButtonText: "Yes, delete it!",
//    closeOnConfirm: false
//}, function () {
//    swal("Deleted!", "Your imaginary file has been deleted.", "success");
//});