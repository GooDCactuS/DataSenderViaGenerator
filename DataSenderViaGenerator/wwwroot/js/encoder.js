function onEncodeClick() {
    let taFrom = $("#taFrom")[0].value;
    let encodedText = hexEncode(taFrom);
    $("#taTo")[0].value = encodedText;
}

function onDecodeClick() {
    let taFrom = $("#taFrom")[0].value;
    let decodedText = hexDecode(taFrom);
    $("#taTo")[0].value = decodedText;
}

function hexEncode(str) {
    let result = "";

    for (let i = 0; i < str.length; i++) {
        let hex = str.charCodeAt(i).toString(16);
        result += ("\\x" + hex).slice(-4);
    }

    return result;
}

function hexDecode(bytes) {
    bytes = bytes.replaceAll("\\x", '00');
    let hexes = bytes.match(/.{1,4}/g) || [];
    let str = "";

    for (let i = 0; i < hexes.length; i++) {
        str += String.fromCharCode(parseInt(hexes[i], 16));
    }

    return str;
}