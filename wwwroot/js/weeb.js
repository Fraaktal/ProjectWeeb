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

//TODO jouer avec les ids des cartes pour la pile et la main? ou alors juste affichage avec l'objet Game

var canvas = document.querySelector('#Canvas');
var context = canvas.getContext('2d');

var contour = new Image();
contour.src = 'src/plateau.png';
contour.addEventListener('load',function() {
        context.drawImage(contour, 0, 0, context.canvas.width, context.canvas.height);
});

function getMousePosition(canvas, event) {
    let rect = canvas.getBoundingClientRect();
    let x = event.clientX - rect.left;
    let y = event.clientY - rect.top;
    console.log("Coordinate x: " + x,
        "Coordinate y: " + y);
}


canvas.addEventListener("mousedown", function (e) {
    getMousePosition(canvas, e);
}); 
