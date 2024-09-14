
    $('input[type="File"]').on('change', function () {
            var ext = $(this).val().split('.').pop();
    if ($(this).val() != '') {
                if (ext != "jpg" && ext !="png" ) {

        $(this).val('');

    $('#lblimage').text('Tipo de archivo incorrecto, inserte una imagen');
           
                }
    else{
        $('#lblimage').text('');

                }
                 
                
            }
        });

