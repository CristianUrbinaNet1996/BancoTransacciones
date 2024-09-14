function eliminarRegla(id, urlaction) {
  
    $.ajax({
        url: urlaction,
        type: "POST",
        data: { OidRegla: id },
        success: function (response) {
            // Aquí puedes manejar la respuesta del servidor




            //var boton = document.getElementById("btn");
            //boton.style.display = "none";
            //  var butonEliminar = $(boton).closest('.removeInstanceButton')
            // // butonEliminar.style.display="none";
        },
        error: function (xhr, status, error) {
            // Aquí puedes manejar los errores de la solicitud
            console.error(error);
        }
    });
}
