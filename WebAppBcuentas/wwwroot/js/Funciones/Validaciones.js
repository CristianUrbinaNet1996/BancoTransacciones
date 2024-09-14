
// Función para limpiar las clases y mensajes de validación
function clearValidation(inputElement) {
    // Eliminar las clases de estilo de validación
    inputElement.classList.remove("field-validation-error-input");
    inputElement.classList.remove("text-danger");

    // Buscar el elemento de mensaje de validación asociado por posición relativa
    var validationMessage = inputElement.closest(".form-group").querySelector(".text-danger");
    if (validationMessage) {
        validationMessage.textContent = "";
    }
}

// Función para quitar el estilo text-danger al escribir en un campo
function clearTextDangerOnInput(inputElement) {
    inputElement.addEventListener("input", function () {
        clearValidation(this);
    });
}

// Asignar el evento input a los campos de entrada y textarea que deseas supervisar
document.addEventListener("DOMContentLoaded", function () {
    var inputFields = document.querySelectorAll(".form-control"); // Selecciona todos los campos de formulario
    inputFields.forEach(function (input) {
        clearTextDangerOnInput(input);
    });

    var textareaFields = document.querySelectorAll("textarea"); // Selecciona todos los textarea
    textareaFields.forEach(function (textarea) {
        clearTextDangerOnInput(textarea);
    });
});