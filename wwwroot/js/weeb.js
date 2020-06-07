"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/GameHub").build();

connection.on("OtherPlayerConnected", function () {
    alert("Le joueur adverse s'est connecté");
});

connection.start().then(function () {
    console.log("Connexion établie");

    const urlParams = new URLSearchParams(window.location.search);
    const idGame = urlParams.get('gameId');
    const idUser = urlParams.get('userId');

    connection.invoke("PlayerConnectedOnGame", idGame, idUser).catch(function (err) {
        return console.error(err.toString());
    });

}).catch(function (err) {
    return console.error(err.toString());
});

