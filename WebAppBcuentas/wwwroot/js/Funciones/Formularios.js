function noPuntoComa(e) {

    var code = e.which || e.keyCode,
        allowedKeys = [8, 9, 13, 27, 35, 36, 37, 38, 39, 46];

    if (allowedKeys.indexOf(code) > -1) {
        return;
    }

    if ((e.shiftKey || (code < 48 || code > 57)) && (code < 96 || code > 105)) {
        e.preventDefault();
    }
}

function siPuntoComa(e) {
    var code = e.which || e.keyCode,
        allowedKeys = [8, 9, 13, 27, 35, 36, 37, 38, 39, 46, 44, 46]; 

    if (allowedKeys.indexOf(code) > -1) {
        return;
    }

    if (
        (e.shiftKey || (code < 48 || code > 57)) && 
        (code < 96 || code > 105) && 
        code !== 188 && 
        code !== 190 
    ) {
        e.preventDefault();
    }
}

function letrasYNumeros(e) {
    var code = e.which || e.keyCode,
        allowedKeys = [8, 9, 13, 27, 35, 36, 37, 38, 39, 46];

    if (allowedKeys.indexOf(code) > -1) {
        return;
    }

    if (!((code >= 48 && code <= 57) || (code >= 65 && code <= 90) || (code >= 97 && code <= 122))) {
        e.preventDefault();
    }
}

