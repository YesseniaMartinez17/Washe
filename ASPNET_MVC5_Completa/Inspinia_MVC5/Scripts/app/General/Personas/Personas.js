//alert("si");


$("#btnDetalles").click(function (evento) {
    $("#modal-content").load("/Personas/Index.cshtml");
});

$(document).ready(
    function () {
        $(".btnDetails").click(
            function () {
                var id = $(this).data("value");
                $("#contenidoIndex").load("/Personas/Details/" + id,
                    function () {
                        $('#ModalDetalles').modal("show");
                        //$("#ModalDetalles").show();
                    }
                );
            });
        }
);

$("#target").click(function () {
    alert("Handler for .click() called.");
});