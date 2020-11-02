﻿"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/carhub").build();

connection.onCreate( () => console.log)

connection.on("CarAdjusted", function (carId, direction, position) {
    var message = carId + " was adjusted to the " + direction + " at position " + position.x + ", " + position.y;
    var li = document.createElement("li");
    li.textContent = message;
    document.getElementById("adjustments").appendChild(li);

    drawCar(position);
});

function drawCar(position) {
    var canvas = document.getElementById('track');
    var ctx = canvas.getContext('2d');

    ctx.clearRect(0, 0, 600, 600);
    ctx.fillStyle = 'rgb(200, 0, 0)';
    ctx.fillRect(300 + 10*position.x, 300 - 10*position.y, 10, 10);
}

connection.on("RaceReset", function () {
    document.getElementById("adjustments").innerHTML = "";
});

connection.start()
    .then( (args) => console.log(`args`) )
    .catch(function (err) {
    return console.error(err.toString());
});

document.getElementById("resetButton").addEventListener("click", function (event) {
    connection.invoke("Reset").catch(function (err) {
        return console.error(err.toString());
    });
});

function adjustDirection(direction) {
    var carId = document.getElementById("carIdInput").value;
    connection.invoke("Adjust", carId, direction).catch(function (err) {
        return console.error(err.toString());
    });
}
