"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/carhub").build();

//Disable send button until connection is established
document.getElementById("sendButton").disabled = true;

connection.on("CarAdjusted", function (carId, direction) {
    var message = carId + " was adjusted to the " + direction;
    var li = document.createElement("li");
    li.textContent = message;
    document.getElementById("adjustments").appendChild(li);
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