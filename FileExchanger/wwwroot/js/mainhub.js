let connection = new signalR.HubConnectionBuilder()
    .withUrl("/hub").build();

connection.start().then(function () {
    console.log("connected");
});