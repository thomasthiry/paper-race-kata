"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/carhub").build();

connection.on("CarAdjusted", function (carId, direction, position) {
    var message = carId + " was adjusted to the " + direction + " at position " + position;
    var li = document.createElement("li");
    li.textContent = message;
    document.getElementById("adjustments").appendChild(li);
});

connection.on("RaceReset", function () {
    document.getElementById("adjustments").innerHTML = "";
});

connection.start().catch(function (err) {
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