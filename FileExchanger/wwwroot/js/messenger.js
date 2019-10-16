$(document).ready(function () {
    $("#fileButton").click(function () {
        $("#fileInput").trigger('click');
    })

    connection.on("SendMessage", function (data) {
        RenderMessage(data.result)
        $("#txtContainer").val('');
    });

    connection.on("ReceiveMessage", function (data) {
        RenderMessage(data.result)
    });

})

function onDialogClick(e) {
    $("#To").val(e);
}

function RenderMessage(val) {
    document.getElementById("msgContainer").insertAdjacentHTML('beforeend', val);
    document.getElementById("msgContainer").lastElementChild.scrollIntoView();
}