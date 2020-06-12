﻿"use strict";

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
var basicHeigth = 0;

var cardPath = 'src/Cards/';

var gameElements = [];
var drawPile = [];
var isPlayerCurrentTurn = false;
var cardLoaded = 0;
var currentHand = [];
var currentMode = "basic";
var selectedCardId = -1;
var selectedCardPlace = -1;

function displayCanvas() {

    totalHeight = window.innerHeight * 0.997;
    totalWidth = window.innerWidth * 0.999;

    context.canvas.height = totalHeight;
    context.canvas.width = totalWidth;

    context.drawImage(cardImages[24], 0, 0, context.canvas.width, context.canvas.height);

    basicWidth = getCardInHandWidth();
    basicHeigth = basicWidth * 56 / 39;
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
    info2: "",
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


connection.on("PlayerCardPlayed", function (handCards, pSide) {
    placeHandCards(handCards);
    placeCardsOnBattleField(pSide, false);
    drawImages();
});

connection.on("EnemyCardPlayedClient", function (pSide) {
    placeCardsOnBattleField(pSide, true);
    drawImages();
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

function handleClick(ge) {
    if (ge == null) {
        currentMode = "basic";
        selectedCardId = -1;
        return;
    }
    if (ge.type === "HandCard") {
        currentMode = "CanPlayCard";
        selectedCardId = ge.info;
        selectedCardPlace = ge.info2;
    }
    else if (ge.type === "PlayerCardPosition" && currentMode === "CanPlayCard") {
        connection.invoke("PlayCard", idGame, idUser, selectedCardId, ge.info, selectedCardPlace).catch(function(err) {
            return console.error(err.toString());
        });
    }
    else {
        currentMode = "basic";
        selectedCardId = -1;
    }
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
            if (cardLoaded === 26) {
                displayCanvas();
                connection.invoke("PlayerConnectedOnGame", idGame, idUser).catch(function (err) {
                    return console.error(err.toString());
                });
            }
        });
    }

    var back = new Image();
    back.src = 'src/Cards/Back.png';
    back.addEventListener('load', function () {
        cardLoaded += 1;
        if (cardLoaded === 26) {
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
        if (cardLoaded === 26) {
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
        if (cardLoaded === 26) {
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
        if (cardLoaded === 26) {
            displayCanvas();
            connection.invoke("PlayerConnectedOnGame", idGame, idUser).catch(function (err) {
                return console.error(err.toString());
            });

        }
    });

    result.push(cible);

    var plateau = new Image();
    plateau.src = 'src/plateau.png';
    plateau.addEventListener('load', function () {
        cardLoaded += 1;
        if (cardLoaded === 26) {
            displayCanvas();
            connection.invoke("PlayerConnectedOnGame", idGame, idUser).catch(function (err) {
                return console.error(err.toString());
            });

        }
    });

    result.push(plateau);

    var fond = new Image();
    fond.src = 'src/Cards/Fond.png';
    fond.addEventListener('load', function () {
        cardLoaded += 1;
        if (cardLoaded === 26) {
            displayCanvas();
            connection.invoke("PlayerConnectedOnGame", idGame, idUser).catch(function (err) {
                return console.error(err.toString());
            });

        }
    });

    result.push(fond);

    return result;
}

function initGraphicElement(type, info, image, x, y, w, h) {

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

function getCardInHandWidth() {
    var res = (totalWidth - 65) / 9;
    res = res - res % 39;
    return res;
}

function placeHandCards(cardsIds) {
    var currentCardInHandAmount = 0;

    var copy = [];

    gameElements.forEach(function(gel) {
        if (gel.type != "HandCard") {
            copy.push(gel);
        }
    });
    gameElements = copy;

    cardsIds.forEach(function (id) {
        var xPos = (currentCardInHandAmount+1) * (basicWidth + 5) + 10;
        var yPos = totalHeight - basicHeigth - 10;
        
        var ge = initGraphicElement("HandCard", id, cardImages[id], xPos, yPos, basicWidth, basicHeigth);
        ge.info2 = currentCardInHandAmount;
        gameElements.push(ge);

        currentCardInHandAmount += 1;
    });
}

function placeCardsOnBattleField(battlefield, enemy) {
    var currentPos = 0;

    battlefield.forEach(function (id) {
        if (id !== -1) {

            var offset2 = (totalWidth - 8 * (5 + basicWidth * 2 / 3) - 5) / 2;
            var xPos = offset2 + currentPos * (5 + basicWidth * 2 / 3);
            var yPos = totalHeight * 0.8 / 2 + 30;

            var type = enemy ? "EnemyPlayedCard" : "PlayerPlayedCard";
            var ge = initGraphicElement(type, id, cardImages[id], xPos, yPos, basicWidth * 2 / 3, basicHeigth * 2 / 3);
            gameElements.push(ge);

        }
        currentPos += 1;
    });
}

function placeImages() {

    //Place les emplacements de carte, la cible adverse et le panneau de vie de chaque joueurs etc...
    gameElements = [];

    //Emplacement des vies
    var xPos = 0;
    var yPos = totalHeight - basicHeigth;
    var id = 21;
    var ge = initGraphicElement("PlayerLife", id, cardImages[id], xPos, yPos, basicWidth, basicHeigth);

    gameElements.push(ge);

    xPos = totalWidth - basicWidth;
    yPos = 0;
    id = 22;
    ge = initGraphicElement("EnemyLife", id, cardImages[id], xPos, yPos, basicWidth, basicHeigth);

    gameElements.push(ge);

    //Cible
    xPos = totalWidth / 2 - basicWidth / 2;
    yPos = 2;
    id = 23;
    ge = initGraphicElement("target", id, cardImages[id], xPos, yPos, basicWidth, basicWidth*4/7);

    gameElements.push(ge);

    for (var i = 0; i < 8; i++) {
        var offset = (totalWidth - 8 * (5 + basicWidth * 2 / 3) - 5)/ 2;
        xPos = offset + i * (5 + basicWidth * 2 / 3);
        yPos = totalHeight * 0.8 / 2 - basicHeigth * 2 / 3 - 30;

        id = 25;
        ge = initGraphicElement("EnemyCardPosition", i, cardImages[id], xPos, yPos, basicWidth * 2 / 3, basicHeigth * 2 / 3);
        gameElements.push(ge);
    }

    for (var j = 0; j < 8; j++) {
        var offset2 = (totalWidth - 8 * (5 + basicWidth * 2 / 3) - 5)/ 2;
        xPos = offset2 + j * (5 + basicWidth * 2 / 3);
        yPos = totalHeight * 0.8 / 2 + 30;

        id = 25;
        ge = initGraphicElement("PlayerCardPosition", j, cardImages[id], xPos, yPos, basicWidth * 2 / 3, basicHeigth * 2 / 3);
        gameElements.push(ge);
    }

}

function drawImages() {
    context.drawImage(cardImages[24], 0, 0, context.canvas.width, context.canvas.height);
    gameElements.forEach(function (ge) {
        if (ge.type !== "EnemyPlayedCard") {
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
