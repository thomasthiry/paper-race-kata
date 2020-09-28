"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/carhub").build();

//Disable send button until connection is established
document.getElementById("sendButton").disabled = true;

connection.on("CarAdjusted", function (carId, direction, position) {
    var message = carId + " was adjusted to the " + direction + " at position " + position;
    var li = document.createElement("li");
    li.textContent = message;
    document.getElementById("adjustments").appendChild(li);
});

connection.on("RaceReset", function () {
    document.getElementById("adjustments").innerHTML = "";
});

connection.start().then(function () {
    document.getElementById("sendButton").disabled = false;
}).catch(function (err) {
    return console.error(err.toString());
});

document.getElementById("sendButton").addEventListener("click", function (event) {
    var carId = document.getElementById("carIdInput").value;
    var direction = document.getElementById("directionInput").value;
    connection.invoke("Adjust", carId, direction).catch(function (err) {
        return console.error(err.toString());
    });
    event.preventDefault();
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