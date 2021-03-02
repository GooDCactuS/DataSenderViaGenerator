function sendForm() {
    let textDataEl = $("#textData")[0];
    textDataEl.style.display = "none";
    $("#textResult")[0].style = "display:auto";

    $.ajax({
        url: "Sender",
        type: "Post",
        data: {
            data: textDataEl.value
        },
        success: function (result) {
            debugger;
        }
    });
}