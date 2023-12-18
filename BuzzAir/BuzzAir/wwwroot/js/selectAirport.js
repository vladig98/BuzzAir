"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/getSelectOptions").build();

connection.on("CountrySelected", function (states) {
    document.getElementById("State").parentElement.classList.remove("d-none")
    let stateSelect = document.getElementById("State")

    stateSelect.innerHTML = ''
    document.getElementById("City").innerHTML = ""
    document.getElementById("City").parentElement.classList.add("d-none")

    let defaultOption = document.createElement("option");
    defaultOption.text = "Select State";
    defaultOption.selected = true;
    defaultOption.hidden = true;
    defaultOption.disabled = true;

    stateSelect.appendChild(defaultOption)

    for (let state of states) {
        let option = document.createElement("option");
        option.value = state.id
        option.text = state.name

        stateSelect.appendChild(option)
    }
});

connection.on("StateSelected", function (cities) {
    document.getElementById("City").parentElement.classList.remove("d-none")
    let citySelect = document.getElementById("City")

    citySelect.innerHTML = ''

    let defaultOption = document.createElement("option");
    defaultOption.text = "Select City";
    defaultOption.selected = true;
    defaultOption.hidden = true;
    defaultOption.disabled = true;

    citySelect.appendChild(defaultOption)

    for (let city of cities) {
        let option = document.createElement("option");
        option.value = city.id
        option.text = city.name

        citySelect.appendChild(option)
    }
});

connection.start().then(function () {
    //document.getElementById("sendButton").disabled = false;
}).catch(function (err) {
    return console.error(err.toString());
});

document.getElementById("Country").addEventListener("change", function (event) {
    var countryId = document.getElementById("Country").value;

    connection.invoke("SelectCountry", countryId).catch(function (err) {
        return console.error(err.toString());
    });
});

document.getElementById("State").addEventListener("change", function (event) {
    var stateId = document.getElementById("State").value;

    connection.invoke("SelectState", stateId).catch(function (err) {
        return console.error(err.toString());
    });
});