"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/carhub").build();
var carId;

function joinGame(myCarId) {
    carId = myCarId;
    connection.start()
        .then(() => connection.invoke("JoinGame", carId))
        .catch(function (err) {
            return console.error(err.toString());
        });
}

connection.on("CarAdjusted", function (carId, direction, position) {
    var message = carId + " was adjusted to the " + direction + " at position " + position.x + ", " + position.y;
    var li = document.createElement("li");
    li.textContent = message;
    document.getElementById("adjustments").appendChild(li);

    cars[carId] = position;
    drawCars();
});

var cars = {};

connection.on("CarJoined", function (carId, position) {
    var message = carId + " joined at position " + position.x + ", " + position.y;
    var li = document.createElement("li");
    li.textContent = message;
    document.getElementById("adjustments").appendChild(li);
    cars[carId] = position;
    drawCars();
});

function drawCars() {
    var canvas = document.getElementById('track');
    var ctx = canvas.getContext('2d');

    ctx.clearRect(0, 0, 600, 600);

    var carIds = Object.keys(cars);

    for (var i = 0; i < carIds.length; i++) {
        var carId = carIds[i];
        var position = cars[carId];
        drawOneCar(ctx, carId, position);
    }

}

function drawOneCar(ctx, carId, position) {
    ctx.fillStyle = carId == 'red' ? 'rgb(200, 0, 0)': 'rgb(0, 0, 200)';
    ctx.fillRect(300 + 10 * position.x, 300 - 10 * position.y, 10, 10);
}

connection.on("RaceReset", function () {
    document.getElementById("adjustments").innerHTML = "";
});

document.getElementById("resetButton").addEventListener("click", function (event) {
    connection.invoke("Reset").catch(function (err) {
        return console.error(err.toString());
    });
});

function adjustDirection(direction) {
    connection.invoke("Adjust", carId, direction).catch(function (err) {
        return console.error(err.toString());
    });
}
