"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/getSelectOptions").build();

connection.on("CountryFlightSelectedOrigin", function (airports) {
    document.getElementById("Origin").classList.remove("d-none")
    document.getElementById("originLabel").classList.remove("d-none")
    let airportSelect = document.getElementById("Origin")

    airportSelect.innerHTML = ''

    let defaultOption = document.createElement("option");
    defaultOption.text = "Select Airport";
    defaultOption.selected = true;
    defaultOption.hidden = true;
    defaultOption.disabled = true;

    airportSelect.appendChild(defaultOption)

    for (let airport of airports) {
        let option = document.createElement("option");
        option.value = airport.id
        option.text = airport.name

        airportSelect.appendChild(option)
    }
});

connection.on("CountryFlightSelectedDestination", function (airports) {
    document.getElementById("Destination").classList.remove("d-none")
    document.getElementById("destinationLabel").classList.remove("d-none")
    let airportSelect = document.getElementById("Destination")

    airportSelect.innerHTML = ''

    let defaultOption = document.createElement("option");
    defaultOption.text = "Select Airport";
    defaultOption.selected = true;
    defaultOption.hidden = true;
    defaultOption.disabled = true;

    airportSelect.appendChild(defaultOption)

    for (let airport of airports) {
        let option = document.createElement("option");
        option.value = airport.id
        option.text = airport.name

        airportSelect.appendChild(option)
    }
});

connection.start().then(function () {
    //document.getElementById("sendButton").disabled = false;
}).catch(function (err) {
    return console.error(err.toString());
});

document.getElementById("DestinationCountry").addEventListener("change", function (event) {
    var countryId = document.getElementById("DestinationCountry").value;

    connection.invoke("SelectCountryForFlightDestination", countryId).catch(function (err) {
        return console.error(err.toString());
    });
});

document.getElementById("OriginCountry").addEventListener("change", function (event) {
    var countryId = document.getElementById("OriginCountry").value;

    connection.invoke("SelectCountryForFlightOrigin", countryId).catch(function (err) {
        return console.error(err.toString());
    });
});