$(document).ready(function () {
    $("#fileButton").click(function () {
        $("#fileInput").trigger('click');
    })
})

function onDialogClick(e) {
    $("#To").val(e);
}

connection.on("SendMessage", function () {
alert(data);
});

connection.on("ReceiveMessage", function (data) {
    alert(data);
});