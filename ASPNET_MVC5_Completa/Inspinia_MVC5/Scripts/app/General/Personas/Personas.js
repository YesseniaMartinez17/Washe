//alert("si");



        $(".btnDetails").click(
            function () {
                var id = $(this).data("value");
                $("#contenidoIndex").load("/Personas/Details/" + id,
                    function () {
                        $('#ModalDetalles').modal("show");
                        //$("#ModalDetalles").show();
                    }
                );
            }
        );
//$("#target").click(function () {
//    alert("Handler for .click() called.");
//});