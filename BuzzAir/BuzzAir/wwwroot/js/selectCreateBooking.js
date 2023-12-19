"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/getSelectOptions").build();

connection.on("CurrencyConvertionCompleted", function (curr, price) {
    $("#totalAmount").text(curr + " " + price.toFixed(2))
})

connection.start().then(function () {
    //document.getElementById("sendButton").disabled = false;
}).catch(function (err) {
    return console.error(err.toString());
});