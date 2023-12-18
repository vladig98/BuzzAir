"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/getSelectOptions").build();

connection.on("FlightDatesSelected", function (dates) {
    let datesToAdd = dates.split(';');

    var $input = $('#departureDate')
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
                var originId = document.getElementById("originSelect").value;
                var destinationId = document.getElementById("destinationSelect").value;

                connection.invoke("GetReturnFlightDates", originId, destinationId).catch(function (err) {
                    return console.error(err.toString());
                });
            }
        }
    }
});

connection.on("ReturnFlightDatesSelected", function (dates) {
    let datesToAdd = dates.split(';');

    var $input = $('#returnDate')
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
    let destinationSelect = document.getElementById("destinationSelect")

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

document.getElementById("originSelect").addEventListener("change", function (event) {
    var cityId = document.getElementById("originSelect").value;

    document.getElementById('departureDate').value = ''
    document.getElementById('returnDate').value = ''

    connection.invoke("SelectDestinationsForHomePage", cityId).catch(function (err) {
        return console.error(err.toString());
    });
});

document.getElementById("destinationSelect").addEventListener("change", function (event) {
    var originId = document.getElementById("originSelect").value;
    var destinationId = document.getElementById("destinationSelect").value;

    document.getElementById('departureDate').value = ''
    document.getElementById('returnDate').value = ''

    connection.invoke("GetFlightDates", originId, destinationId).catch(function (err) {
        return console.error(err.toString());
    });
});