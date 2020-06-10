"use strict";

//TODO pseudo limité
//TODO resize LOL (╯°□°）╯︵ ┻━┻

var canvas = document.querySelector('#Canvas');
var context = canvas.getContext('2d');

var totalHeight = window.innerHeight * 0.997;
var totalWidth = window.innerWidth * 0.999;

context.canvas.height = totalHeight;
context.canvas.width = totalWidth;

var contour = new Image();
contour.src = 'src/plateau.png';

var megumin = new Image();
megumin.src = 'src/Cards/0.png';

var urlParams = new URLSearchParams(window.location.search);
var idGame = urlParams.get('gameId');
var idUser = urlParams.get('userId');

//Rapport des cartes : 39/56
var cardInHandWidth = getCardInHandWidh();
var cardInHandHeight = cardInHandWidth * 56 / 39;

var cardPath = 'src/Cards/';

var gameElements = [];
var drawPile = [];
var isPlayerCurrentTurn = false;
var currentCardInHandAmount = 0;

contour.addEventListener('load', function () {
    context.drawImage(contour, 0, 0, context.canvas.width, context.canvas.height);
});

canvas.addEventListener("mousedown", function (e) {
    var pos = getMousePosition(canvas, e);
    x = pos[0];
    y = pos[1];

    //Différents état : endTurn, putCard, selectPlayedCard, selectHandCard, selectEnnemyCard, etc
    var state = getStateOfClick(x, y);
    handleClick(state, x, y);
});

//Classe qui permettra de stocker l'emplacement des images sur l'écran avec la place prise pour facilement gérer les evenements au click
const graphicElement = {
    type: "",  //card //cardposition //enemycard //btnNext //ennemy
    info: "",
    imagePath: "",
    posX: 0,
    posY: 0,
    width: 0,
    height:0
};

var connection = new signalR.HubConnectionBuilder().withUrl("/GameHub").build();

connection.on("OtherPlayerConnected", function () {
    console.log("Le joueur adverse s'est connecté");
});

connection.on("InitializeGamePlayerSide", function (drawpile) {
    drawPile = drawpile;
    //todo INIT les cardposition ennemy position etc
});

connection.on("GameReadyToStart", function (isPlayerTurn) {
    isPlayerCurrentTurn = isPlayerTurn;
    if (isPlayerCurrentTurn) {
        drawCards(5);

        //permet d'envoyer un evenement qui finira le tour du joueur s'il prends trop longtemps
        connection.invoke("LaunchTimer", idGame, idUser).catch(function (err) {
            return console.error(err.toString());
        });
    }
});

connection.start().then(function () {
    console.log("Connexion établie");

    connection.invoke("PlayerConnectedOnGame", idGame, idUser).catch(function (err) {
        return console.error(err.toString());
    });

}).catch(function (err) {
    return console.error(err.toString());
});

function getMousePosition(canvas, event) {
    let rect = canvas.getBoundingClientRect();
    let x = event.clientX - rect.left;
    let y = event.clientY - rect.top;
    return [x,y];
}

function getStateOfClick(x,y) {
    
}

function handleClick(state, x, y) {
    
}

function getGraphicElement(type, info, imagePath, x, y, w, h) {

    var obj = Object.create(graphicElement);
    obj.type = type;
    obj.info = info;
    obj.imagePath = imagePath;
    obj.posX = x;
    obj.posY = y;
    obj.width = w;
    obj.height = h;

    return obj;
}

function getCardInHandWidh(state, x, y) {
    var res = (totalWidth - 65) / 9;
    res = res - res % 39;
    return res;
}

function drawCards(amount) {
    for (var i = 0; i < amount; i++) {

        var xPos = (currentCardInHandAmount) * (cardInHandWidth + 5) + 10;
        var yPos = totalHeight - cardInHandHeight - 10;
        var cardId = drawPile.pop();

        var path = cardPath + cardId + '.png';

        var card = new Image();
        card.src = path;

        context.drawImage(megumin, xPos, yPos, cardInHandWidth,cardInHandHeight);

        var ge = getGraphicElement("HandCard", cardId, path, xPos, yPos, cardInHandWidth, cardInHandHeight);

        gameElements.push(ge);

        currentCardInHandAmount += 1;
    }
}
