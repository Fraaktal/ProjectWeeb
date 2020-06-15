"use strict";

var urlParams = new URLSearchParams(window.location.search);
var idInfo = urlParams.get('idInfo');

switch (idInfo) {
    case "1":
        alert("Inscription échouée. La confirmation du mot de passe doit être identique au mot de passe.");
        break;
    case "2":
        alert("Inscription échouée. Un utilisateur avec le même pseudo existe déjà.");
        break;
    case "3":
        alert("Inscription réussie!");
        break;
    case "4":
        alert("Echec de la connexion. Le pseudo ou le mot de passe est incorrect");
        break;
default:
}