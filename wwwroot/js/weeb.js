"use strict";

window.addEventListener("resize", displayCanvas);

var canvas = document.querySelector('#Canvas');
var context = canvas.getContext('2d');

var totalHeight = 0;
var totalWidth = 0;

var urlParams = new URLSearchParams(window.location.search);
var idGame = urlParams.get('gameId');
var idUser = urlParams.get('userId');

//Rapport des cartes : 39/56
var basicWidth = 0;
var cardInHandHeight = 0;

var cardPath = 'src/Cards/';

var gameElements = [];
var drawPile = [];
var isPlayerCurrentTurn = false;
var cardLoaded = 0;
var currentHand = [];

function displayCanvas() {

    totalHeight = window.innerHeight * 0.997;
    totalWidth = window.innerWidth * 0.999;

    context.canvas.height = totalHeight;
    context.canvas.width = totalWidth;

    context.drawImage(cardImages[24], 0, 0, context.canvas.width, context.canvas.height);

    basicWidth = getCardInHandWidh();
    cardInHandHeight = basicWidth * 56 / 39;
    placeImages();
    placeHandCards(currentHand);
    drawImages();
}

canvas.addEventListener("mousedown", function (e) {
    if (isPlayerCurrentTurn) {
        var pos = getMousePosition(canvas, e);
        var x = pos[0];
        var y = pos[1];

        //Différents état : endTurn, putCard, selectPlayedCard, selectHandCard, selectEnnemyCard, etc
        var ge = getGameElementOfClick(x, y);
        handleClick(ge, x, y);
    }
});

//Classe qui permettra de stocker l'emplacement des images sur l'écran avec la place prise pour facilement gérer les evenements au click
var graphicElement = {
    type: "", //card //cardposition //enemycard //btnNext //ennemy
    info: "",
    image: null,
    posX: 0,
    posY: 0,
    width: 0,
    height:0
};

var connection = new signalR.HubConnectionBuilder().withUrl("/GameHub").build();

connection.on("InitializeGamePlayerSide", function (handCards) {
    currentHand = handCards;
    placeImages();
    placeHandCards(handCards);
    drawImages();
});

connection.on("TurnChanged", function (isPlayerTurn, handCards) {

    isPlayerCurrentTurn = isPlayerTurn;

    placeHandCards(handCards);
    drawImages();

    if (isPlayerCurrentTurn) {
        //permet d'envoyer un evenement qui finira le tour du joueur s'il prends trop longtemps
        connection.invoke("LaunchTimer", idGame, idUser).catch(function (err) {
            return console.error(err.toString());
        });
    }
});

connection.start().then(function () {
    console.log("Connexion établie");
}).catch(function (err) {
    return console.error(err.toString());
});

var cardImages = loadCardImages();

function getMousePosition(canvas, event) {
    let rect = canvas.getBoundingClientRect();
    let x = event.clientX - rect.left;
    let y = event.clientY - rect.top;
    return [x,y];
}

function getGameElementOfClick(x, y) {
    var res = null;
    gameElements.forEach(function (ge) {
        if (x > ge.posX && x < ge.posX + ge.width && y > ge.posY && y < ge.posY+ge.height) {
            res =  ge;
        }
    });

    return res;
}

function handleClick(state, x, y) {
    
}

function loadCardImages() {
    var result = [];
    for (var i = 0; i < 20; i++) {
        var next = false;
        var card = new Image();
        card.src = 'src/Cards/' + i + '.png';
        result.push(card);

        card.addEventListener('load', function () {
            cardLoaded += 1;
            if (cardLoaded === 25) {
                displayCanvas();
                connection.invoke("PlayerConnectedOnGame", idGame, idUser).catch(function (err) {
                    return console.error(err.toString());
                });
            }
        });
    }

    var back = new Image();
    back.src = 'src/Cards/back.png';
    back.addEventListener('load', function () {
        cardLoaded += 1;
        if (cardLoaded === 25) {
            displayCanvas();
            connection.invoke("PlayerConnectedOnGame", idGame, idUser).catch(function (err) {
                return console.error(err.toString());
            });

        }
    });

    result.push(back);

    var playerLife = new Image();
    playerLife.src = 'src/Corner2.png';
    playerLife.addEventListener('load', function () {
        cardLoaded += 1;
        if (cardLoaded === 25) {
            displayCanvas();
            connection.invoke("PlayerConnectedOnGame", idGame, idUser).catch(function (err) {
                return console.error(err.toString());
            });

        }
    });

    result.push(playerLife);

    var enemylife = new Image();
    enemylife.src = 'src/CornerEnemy.png';
    enemylife.addEventListener('load', function () {
        cardLoaded += 1;
        if (cardLoaded === 25) {
            displayCanvas();
            connection.invoke("PlayerConnectedOnGame", idGame, idUser).catch(function (err) {
                return console.error(err.toString());
            });

        }
    });

    result.push(enemylife);

    var cible = new Image();
    cible.src = 'src/Cible.png';
    cible.addEventListener('load', function () {
        cardLoaded += 1;
        if (cardLoaded === 25) {
            displayCanvas();
            connection.invoke("PlayerConnectedOnGame", idGame, idUser).catch(function (err) {
                return console.error(err.toString());
            });

        }
    });

    result.push(cible);

    var contour = new Image();
    contour.src = 'src/plateau.png';
    contour.addEventListener('load', function () {
        cardLoaded += 1;
        if (cardLoaded === 25) {
            displayCanvas();
            connection.invoke("PlayerConnectedOnGame", idGame, idUser).catch(function (err) {
                return console.error(err.toString());
            });

        }
    });

    result.push(contour);

    return result;
}

function getGraphicElement(type, info, image, x, y, w, h) {

    var obj = Object.create(graphicElement);
    obj.type = type;
    obj.info = info;
    obj.image = image;
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

//TODO pas ça une fonction qui affiche tout les éléments des games elements et x autres qui créent les GE

function placeHandCards(cardsIds) {
    var currentCardInHandAmount = 0;

    cardsIds.forEach(function (id) {
        var xPos = (currentCardInHandAmount+1) * (basicWidth + 5) + 10;
        var yPos = totalHeight - cardInHandHeight - 10;
        
        var ge = getGraphicElement("HandCard", id, cardImages[id], xPos, yPos, basicWidth, cardInHandHeight);

        gameElements.push(ge);

        currentCardInHandAmount += 1;
    });
}

function placeImages() {

    //Place les emplacements de carte, la cible adverse et le panneau de vie de chaque joueurs etc...
    gameElements = [];

    //Emplacement des vies
    var xPos = 0;
    var yPos = totalHeight - cardInHandHeight;
    var id = 21;
    var ge = getGraphicElement("PlayerLife", id, cardImages[id], xPos, yPos, basicWidth, cardInHandHeight);

    gameElements.push(ge);

    xPos = totalWidth - basicWidth;
    yPos = 0;
    id = 22;
    ge = getGraphicElement("EnemyLife", id, cardImages[id], xPos, yPos, basicWidth, cardInHandHeight);

    gameElements.push(ge);

    //Cible
    xPos = totalWidth / 2 - basicWidth / 2;
    yPos = 5;
    id = 23;
    ge = getGraphicElement("target", id, cardImages[id], xPos, yPos, basicWidth, basicWidth*4/7);

    gameElements.push(ge);

}

function drawImages() {
    context.drawImage(cardImages[24], 0, 0, context.canvas.width, context.canvas.height);
    gameElements.forEach(function (ge) {
        if (ge.type !== "EnemyCard") {
            context.drawImage(ge.image, ge.posX, ge.posY, ge.width, ge.height);
        }
        else {
            context.save();
            context.rotate(Math.PI);
            context.drawImage(ge.image, ge.posX, ge.posY, ge.width, ge.height);
            context.restore();
        }
    });
}
