"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/getSelectOptions").build();

connection.on("FlightDatesSelected", function (dates) {
    let datesToAdd = dates.split(';');

    var $input = $('#Departure')
    var picker = $input.pickadate('picker')

    picker.set('disable', true);

    for (let d of datesToAdd) {
        let dateA = JSON.parse(d);

        picker.set('disable', [
            [dateA[0], dateA[1], dateA[2]]
        ])
    }

    let radioButtons = document.searchFlightsForm.isReturning;

    for (let i = 0; i < radioButtons.length; i++) {
        if (radioButtons[i].checked) {
            if (radioButtons[i].value == "Return") {
                var originId = document.getElementById("Origin").value;
                var destinationId = document.getElementById("Destination").value;

                connection.invoke("GetReturnFlightDates", originId, destinationId).catch(function (err) {
                    return console.error(err.toString());
                });
            }
        }
    }
});

connection.on("ReturnFlightDatesSelected", function (dates) {
    let datesToAdd = dates.split(';');

    var $input = $('#Return')
    var picker = $input.pickadate('picker')

    picker.set('disable', true);

    for (let d of datesToAdd) {
        if (d) {
            let dateA = JSON.parse(d);

            picker.set('disable', [
                [dateA[0], dateA[1], dateA[2]]
            ])
        }
    }
});

connection.on("DestinationsHomePageSelected", function (cities) {
    let destinationSelect = document.getElementById("Destination")

    destinationSelect.innerHTML = ''

    let defaultOption = document.createElement("option");
    defaultOption.text = "";
    defaultOption.selected = true;
    defaultOption.hidden = true;
    defaultOption.disabled = true;

    destinationSelect.appendChild(defaultOption)

    let groups = [];

    for (let city of cities) {
        let option = document.createElement("option");
        option.value = city.id
        option.text = city.name

        if (!groups.some(x => x.getAttribute("label") == city.group)) {
            let optGroup = document.createElement("optgroup")
            optGroup.setAttribute("label", city.group)
            optGroup.appendChild(option)
            groups.push(optGroup)
        } else {
            let optGroup = groups.filter(x => x.getAttribute("label") == city.group)[0]

            optGroup.appendChild(option)
        }
    }

    for (var gr of groups) {
        destinationSelect.appendChild(gr)
    }
});

connection.start().then(function () {
    //document.getElementById("sendButton").disabled = false;
}).catch(function (err) {
    return console.error(err.toString());
});

document.getElementById("Origin").addEventListener("change", function (event) {
    var cityId = document.getElementById("Origin").value;

    document.getElementById('Departure').value = ''
    document.getElementById('Return').value = ''

    connection.invoke("SelectDestinationsForHomePage", cityId).catch(function (err) {
        return console.error(err.toString());
    });
});

document.getElementById("Destination").addEventListener("change", function (event) {
    var originId = document.getElementById("Origin").value;
    var destinationId = document.getElementById("Destination").value;

    document.getElementById('Departure').value = ''
    document.getElementById('Return').value = ''

    connection.invoke("GetFlightDates", originId, destinationId).catch(function (err) {
        return console.error(err.toString());
    });
});