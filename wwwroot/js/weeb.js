"use strict";


window.addEventListener("resize", displayCanvas);

var canvas = document.querySelector('#Canvas');
var context = canvas.getContext('2d');

var totalHeight = 0;
var totalWidth = 0;

var urlParams = new URLSearchParams(window.location.search);
var idGame = urlParams.get('gameId');
var idUser = urlParams.get('userId');

var basicWidth = 0;
var basicHeigth = 0;

var cardPath = 'src/Cards/';

var gameElements = [];
var drawPile = [];
var isPlayerCurrentTurn = false;
var cardLoaded = 0;
var currentHand = [];
var currentBattleFieldCurrentPlayer = [];
var currentBattleFieldEnemyPlayer = [];
var currentMode = "basic";
var selectedCardId = -1;
var selectedCardPosition = -1;
var enemyName = "";
var playerName = "";
var enemyLife = "";
var playerLife = "";

function displayCanvas() {

    totalHeight = window.innerHeight * 0.997;
    totalWidth = window.innerWidth * 0.999;

    context.canvas.height = totalHeight;
    context.canvas.width = totalWidth;

    context.drawImage(cardImages[24], 0, 0, context.canvas.width, context.canvas.height);

    basicWidth = getCardInHandWidth();
    basicHeigth = basicWidth * 56 / 39;
    doPlaceAndDraw();
}

canvas.addEventListener("mousedown", function (e) {
    if (isPlayerCurrentTurn) {
        var pos = getMousePosition(canvas, e);
        var x = pos[0];
        var y = pos[1];

        var ge = getGameElementOfClick(x, y);
        handleClick(ge, x, y);
    }
});

//Classe qui permettra de stocker l'emplacement des images sur l'écran avec la place prise pour facilement gérer les evenements au click
var graphicElement = {
    type: "",
    info: "",
    position: "",
    life: 0,
    stregth: 0,
    image: null,
    posX: 0,
    posY: 0,
    width: 0,
    height:0
};

var connection = new signalR.HubConnectionBuilder().withUrl("/GameHub").build();

connection.on("InitializeGamePlayerSide", function (name, life, handCards, otherPlayerName, otherPlayerLife) {
    currentHand = handCards;
    playerName = name;
    enemyName = otherPlayerName;
    playerLife = life;
    enemyLife = otherPlayerLife !== -1 ? otherPlayerLife : "";
    doPlaceAndDraw();
});

connection.on("OtherPlayerConnected", function (name, life) {
    enemyName = name;
    enemyLife = life;
    doPlaceAndDraw();
});

connection.on("CardAttackedSuccessfuly", function (pSide, eSide) {
    currentBattleFieldEnemyPlayer = eSide;
    currentBattleFieldCurrentPlayer = pSide;
    doPlaceAndDraw();
});

connection.on("CardAttacked", function (pSide, eSide) {
    currentBattleFieldEnemyPlayer = eSide;
    currentBattleFieldCurrentPlayer = pSide;
    doPlaceAndDraw();
});

connection.on("PlayerAttacked", function (newLife) {
    playerLife = newLife;
    doPlaceAndDraw();
});

connection.on("EnemyAttacked", function (newLife) {
    enemyLife = newLife;
    doPlaceAndDraw();
});

connection.on("GameWon", function () {
    var res = confirm("Félicitation vous avez remporté la partie !");

    if (res || !res) {
        document.location.href = "Connexion";
    }
});

connection.on("GameLost", function () {
    var res = confirm("Dommage, vous avez perdu...");

    if (res || !res) {
        document.location.href = "Connexion";
    }
});

connection.on("TurnChanged", function (isPlayerTurn, handCards) {
    currentHand = handCards;
    isPlayerCurrentTurn = isPlayerTurn;

    doPlaceAndDraw();

    if (isPlayerCurrentTurn) {
        //permet d'envoyer un evenement qui finira le tour du joueur s'il prends trop longtemps
        connection.invoke("LaunchTimer", idGame).catch(function (err) {
            return console.error(err.toString());
        });
    }
});


connection.on("PlayerCardPlayed", function (handCards, pSide) {
    currentHand = handCards;
    currentBattleFieldCurrentPlayer = pSide;
    doPlaceAndDraw();
});

connection.on("EnemyCardPlayedClient", function (pSide) {
    currentBattleFieldEnemyPlayer = pSide;
    doPlaceAndDraw();
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
        selectedCardPosition = ge.position;
    }
    else if (ge.type === "PlayerCardPosition" && currentMode === "CanPlayCard") {
        connection.invoke("PlayCard", idGame, idUser, selectedCardId, ge.info, selectedCardPosition).catch(function(err) {
            return console.error(err.toString());
        });
    }
    else if (ge.type === "Next") {
        connection.invoke("AskForNextTurn", idGame).catch(function (err) {
            return console.error(err.toString());
        });
    }
    else if (ge.type === "PlayerPlayedCard") {
        currentMode = "CanAttack";
        selectedCardId = ge.info;
        selectedCardPosition = ge.position;
    }
    else if (ge.type === "EnemyPlayedCard" && currentMode === "CanAttack") {
        connection.invoke("AttackCard", idGame, idUser, selectedCardPosition, ge.position).catch(function (err) {
            return console.error(err.toString());
        });
    }
    else if (ge.type === "Target" && currentMode === "CanAttack") {
        connection.invoke("AttackEnemy", idGame, idUser, selectedCardPosition).catch(function (err) {
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
        var card = new Image();
        card.src = 'src/Cards/' + i + '.png';
        result.push(card);

        card.addEventListener('load', function () {
            cardLoaded += 1;
            if (cardLoaded === 27) {
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
        if (cardLoaded === 27) {
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
        if (cardLoaded === 27) {
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
        if (cardLoaded === 27) {
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
        if (cardLoaded === 27) {
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
        if (cardLoaded === 27) {
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
        if (cardLoaded === 27) {
            displayCanvas();
            connection.invoke("PlayerConnectedOnGame", idGame, idUser).catch(function (err) {
                return console.error(err.toString());
            });

        }
    });

    result.push(fond);

    var next = new Image();
    next.src = 'src/btn_nextTurn.png';
    next.addEventListener('load', function () {
        cardLoaded += 1;
        if (cardLoaded === 27) {
            displayCanvas();
            connection.invoke("PlayerConnectedOnGame", idGame, idUser).catch(function (err) {
                return console.error(err.toString());
            });

        }
    });

    result.push(next);

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

function placeHandCards() {
    var currentCardInHandAmount = 0;

    var copy = [];

    gameElements.forEach(function(gel) {
        if (gel.type !== "HandCard") {
            copy.push(gel);
        }
    });
    gameElements = copy;

    currentHand.forEach(function (card) {
        var xPos = (currentCardInHandAmount+1) * (basicWidth + 5) + 10;
        var yPos = totalHeight - basicHeigth - 10;
        
        var ge = initGraphicElement("HandCard", card[0], cardImages[card[0]], xPos, yPos, basicWidth, basicHeigth);
        ge.position = currentCardInHandAmount;
        ge.life = card[1];
        ge.stregth = card[2];
        gameElements.push(ge);

        currentCardInHandAmount += 1;
    });
}

function placeCardsOnBattleField() {
    var currentPos = 0;
    currentBattleFieldEnemyPlayer.forEach(function(card) {
        if (card[0] !== -1) {
            var offset2 = (totalWidth - 8 * (5 + basicWidth * 2 / 3) - 5) / 2;
            var xPos = offset2 + currentPos * (5 + basicWidth * 2 / 3);
            var yPos = totalHeight * 0.8 / 2 - basicHeigth * 2 / 3 - 30;

            var type = "EnemyPlayedCard";
            var ge = initGraphicElement(type, card[0], cardImages[card[0]], xPos, yPos, basicWidth * 2 / 3, basicHeigth * 2 / 3);
            ge.position = currentPos;
            ge.life = card[1];
            ge.stregth = card[2];
            gameElements.push(ge);
        }
        currentPos += 1;
    });

    currentPos = 0;
    currentBattleFieldCurrentPlayer.forEach(function(card) {
        if (card[0] !== -1) {
            var offset2 = (totalWidth - 8 * (5 + basicWidth * 2 / 3) - 5) / 2;
            var xPos = offset2 + currentPos * (5 + basicWidth * 2 / 3);
            var yPos = totalHeight * 0.8 / 2 + 30;

            var type = "PlayerPlayedCard";
            var ge = initGraphicElement(type, card[0], cardImages[card[0]], xPos, yPos, basicWidth * 2 / 3, basicHeigth * 2 / 3);
            ge.position = currentPos;
            ge.life = card[1];
            ge.stregth = card[2];
            gameElements.push(ge);
        }
        currentPos += 1;
    });
}

function placeText() {
    //IL FAUT afficher les vies des cartes, les noms des joueurs et les vies des joueurs et le nombre d'action

    var textSize = 15;
    
    //Player Name

    var xPos = basicWidth * 0.2;
    var yPos = totalHeight - basicHeigth * 0.75;

    var ge = initGraphicElement("Text", playerName, null, xPos, yPos, textSize,0);
    gameElements.push(ge);

    //Player Life

    xPos = basicWidth * 0.4;
    yPos = totalHeight - basicHeigth * 0.35;

    ge = initGraphicElement("Text", playerLife, null, xPos, yPos, textSize, 0);
    gameElements.push(ge);


    //Enemy Name

    xPos = totalWidth - basicWidth * 0.8;
    yPos = basicHeigth * 0.25;

    ge = initGraphicElement("Text", enemyName, null, xPos, yPos, textSize, 0);
    gameElements.push(ge);
    
    //Enemy Life

    xPos = totalWidth - basicWidth * 0.6;
    yPos = basicHeigth * 0.65;

    ge = initGraphicElement("Text", enemyLife, null, xPos, yPos, textSize, 0);
    gameElements.push(ge);

    var currentPos = 0;

    currentBattleFieldCurrentPlayer.forEach(function (c) {

        var offset = (totalWidth - 8 * (5 + basicWidth * 2 / 3) - 5) / 2;

        if (c[0] !== -1) {
            xPos = offset + currentPos * (5 + basicWidth * 2 / 3) + basicWidth * 0.3;
            yPos = totalHeight * 0.8 / 2 + 30 + basicHeigth*0.55;
            ge = initGraphicElement("TextLife", c[1], null, xPos, yPos, textSize, 0);
            gameElements.push(ge);
        }
        currentPos++;
    });

    currentPos = 0;

    currentBattleFieldEnemyPlayer.forEach(function (c) {
        var offset = (totalWidth - 8 * (5 + basicWidth * 2 / 3) - 5) / 2;

        if (c[0] !== -1) {
            xPos = offset + currentPos * (5 + basicWidth * 2 / 3) + basicWidth*0.3;
            yPos = totalHeight * 0.8 / 2 - basicHeigth *0.6;
            ge = initGraphicElement("TextLife", c[1], null, xPos, yPos, textSize, 0);
            gameElements.push(ge);
        }
        currentPos++;
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
    ge = initGraphicElement("Target", id, cardImages[id], xPos, yPos, basicWidth, basicWidth*4/7);

    gameElements.push(ge);

    //btnNext
    xPos = totalWidth * 0.1;
    yPos = 10;
    id = 26;
    ge = initGraphicElement("Next", id, cardImages[id], xPos, yPos, basicWidth * 0.5, basicWidth * 0.5);

    gameElements.push(ge);

    //drawPile
    xPos = 9 * (basicWidth + 5);
    yPos = totalHeight - basicHeigth - 10;

    id = 20;
    ge = initGraphicElement("Next", id, cardImages[id], xPos, yPos, basicWidth, basicHeigth);

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

function doPlaceAndDraw() {
    gameElements = [];
    placeImages();
    placeHandCards();
    placeCardsOnBattleField();
    placeText();
    drawImagesOnCanvas();
}

function drawImagesOnCanvas() {
    context.drawImage(cardImages[24], 0, 0, context.canvas.width, context.canvas.height);
    gameElements.forEach(function (ge) {
        if (ge.type === "EnemyPlayedCard") {
            context.save();
            context.translate(ge.posX + ge.width, ge.posY + ge.height);
            context.rotate(Math.PI);
            context.drawImage(ge.image, 0, 0, ge.width, ge.height);
            context.restore();
        }
        else if (ge.type === "Text") {
            context.save();
            context.font = "bold 22pt Calibri,Geneva,Arial"; //TODO responsive
            context.fillStyle = "#f5be27";
            context.fillText(ge.info, ge.posX, ge.posY);
            context.restore();
        }
        else if (ge.type === "TextLife") {
            context.save();
            context.font = "bold 22pt Calibri,Geneva,Arial"; //TODO responsive
            context.fillStyle = "#510000";
            context.fillText(ge.info, ge.posX, ge.posY);
            context.restore();
        }
        else {
            context.drawImage(ge.image, ge.posX, ge.posY, ge.width, ge.height);
        }
    });
}