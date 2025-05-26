"use strict";

const connection = new signalR.HubConnectionBuilder()
    .withUrl("/getSelectOptions")
    .build();

const disableDates = (selector, dates) => {
    const $input = $(selector);
    const picker = $input.pickadate("picker");
    picker.set("disable", true);

    for (const d of dates.split(";")) {
        if (!d) {
            continue;
        }
        const [year, month, day] = JSON.parse(d);
        picker.set("disable", [[year, month, day]]);
    }
};

const invokeHub = (method, ...args) =>
    connection.invoke(method, ...args).catch(err => console.error(err));

const populateDestinations = cities => {
    const select = document.getElementById("destinationSelect");
    select.innerHTML = "";

    const defaultOpt = new Option("", "", true, true);
    defaultOpt.hidden = defaultOpt.disabled = true;
    select.append(defaultOpt);

    // Group by country
    const groups = new Map();
    for (const { id, name, group } of cities) {
        if (!groups.has(group)) {
            groups.set(group, document.createElement("optgroup"));
            groups.get(group).label = group;
        }
        const opt = new Option(name, id);
        groups.get(group).append(opt);
    }

    for (const grp of groups.values()) {
        select.append(grp);
    }
};

// SignalR event handlers
connection.on("FlightDatesSelected", dates =>
    disableDates("#departureDate", dates)
);
connection.on("ReturnFlightDatesSelected", dates =>
    disableDates("#returnDate", dates)
);
connection.on("DestinationsHomePageSelected", populateDestinations);

// Start connection
connection.start().catch(err => console.error(err));

const originSelect = document.getElementById("originSelect");
const destinationSelect = document.getElementById("destinationSelect");
const clearDates = () => {
    document.getElementById("departureDate").value = "";
    document.getElementById("returnDate").value = "";
};

originSelect.addEventListener("change", () => {
    clearDates();
    invokeHub("SelectDestinationsForHomePage", originSelect.value);
});

destinationSelect.addEventListener("change", () => {
    clearDates();
    invokeHub(
        "GetFlightDates",
        originSelect.value,
        destinationSelect.value
    );
});

document
    .searchFlightsForm.elements.isReturning
    .forEach(radio =>
        radio.addEventListener("change", ({ target }) => {
            if (target.value === "Return" && target.checked) {
                invokeHub(
                    "GetReturnFlightDates",
                    originSelect.value,
                    destinationSelect.value
                );
            }
        })
    );