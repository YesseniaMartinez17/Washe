
var actual = 0;
var anterior = 0;
var lat1 = 0;
var long = 0;
var lat1_Original = 0;
var long_Original = 0;
var patron = new Array(2);

function codigo(event) {
    var codigo = event.key; //which || event.keyCode;
    if (codigo === 13) {
        console.log("Tecla ENTER");
    }
    return codigo;
}




navigator.geolocation.getCurrentPosition(function (position) {
    //var geolocate = new google.maps.LatLng(position.coords.latitude, position.coords.longitude);
    lat1 = position.coords.latitude;
    long = position.coords.longitude;
});


google.maps.event.addDomListener(window, 'load', init);

function init() {
    if (lat1 == 0) {
        coords = { lat: 15.5038827, lng: -88.01386190000001 };
        console.log(coords);
    } else {
        coords = { lat: lat1, lng: long }
        console.log(coords);
    }


    var mapOptions1 = {
        zoom: 10,
        center: coords,
        streetViewControl: false,
        // Style for Google Maps
        //styles: [{ "featureType": "water", "stylers": [{ "saturation": 43 }, { "lightness": -11 }, { "hue": "#0088ff" }] }, { "featureType": "road", "elementType": "geometry.fill", "stylers": [{ "hue": "#ff0000" }, { "saturation": -100 }, { "lightness": 99 }] }, { "featureType": "road", "elementType": "geometry.stroke", "stylers": [{ "color": "#808080" }, { "lightness": 54 }] }, { "featureType": "landscape.man_made", "elementType": "geometry.fill", "stylers": [{ "color": "#ece2d9" }] }, { "featureType": "poi.park", "elementType": "geometry.fill", "stylers": [{ "color": "#ccdca1" }] }, { "featureType": "road", "elementType": "labels.text.fill", "stylers": [{ "color": "#767676" }] }, { "featureType": "road", "elementType": "labels.text.stroke", "stylers": [{ "color": "#ffffff" }] }, { "featureType": "poi", "stylers": [{ "visibility": "off" }] }, { "featureType": "landscape.natural", "elementType": "geometry.fill", "stylers": [{ "visibility": "on" }, { "color": "#b8cb93" }] }, { "featureType": "poi.park", "stylers": [{ "visibility": "on" }] }, { "featureType": "poi.sports_complex", "stylers": [{ "visibility": "on" }] }, { "featureType": "poi.medical", "stylers": [{ "visibility": "on" }] }, { "featureType": "poi.business", "stylers": [{ "visibility": "simplified" }] }]
    };
    var mapElement1 = document.getElementById('map1');

    var map1 = new google.maps.Map(mapElement1, mapOptions1);

    var marker = new google.maps.Marker({
        position: coords,
        map: map1,
        draggable: true
    });

    google.maps.event.addListener(marker, 'drag', function (event) {
        $('#lattt').text(event.latLng.lat());
        $('#longgg').text(event.latLng.lng());
    });

    var card = document.getElementById('pac-card');
    var input = document.getElementById('pac-input');
    var types = 'changetype-address';

    var strictBounds = document.getElementById('strict-bounds-selector');

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

        var lat1_Original = pos;
        var long_Original = pos1;



        marker.setVisible(true);



        var address = '';
        if (place.address_components) {
            address = [
                (place.address_components[0] && place.address_components[0].short_name || ''),
                (place.address_components[1] && place.address_components[1].short_name || ''),
                (place.address_components[2] && place.address_components[2].short_name || '')
            ].join(' ');
        }
    });

}

$(document).ready(function () {
    //var totalAPagarProductos = parseFloat($("#lblTotalPagar").text().toFixed(2));

    //$("#tblProductosSeleccionados tbody :input").bind('keyup mouseup', function () {
    //    var precioProducto = parseFloat($(this).data('precioproducto'));

    //    var cantidadProducto = parseInt($(this).val());

    //    var totalPorElProducto = precioProducto * cantidadProducto;

    //    total = total + totalPorElProducto;

    //    totalAPagarProductos += totalPorElProducto;
    //    $("#lblTotalPagar").text();
    //    $("#lblTotalPagar").text(total);
    //});
});



function preformatear(ide) {
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
        //let a = parseInt(codigo(l));
        if (campo.val() == 0) {
            campo.val(1);
            actual = 1;
        }
        console.log('Anterior: ' + anterior);
        console.log('Actual: ' + actual);

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
        console.log('Se le suma: ' + res);
        //res = res - precio;
        total = total + res;
    } else if(op == 2){
        console.log('Se resta: ' + res + ' y se le concatena:' + precio);
        //res = res - precio;
        total = total - res;
    }

    $('#lblTotalPagar').text(total);
    console.log(r);
    console.log(campoId);
    res = 0;
}
