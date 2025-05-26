"use strict";

const connection = new signalR.HubConnectionBuilder()
    .withUrl("/getSelectOptions")
    .build();

// Helper to populate a <select> with options
const populateSelect = (selectId, labelId, items, defaultText = "Select Airport") => {
    const select = document.getElementById(selectId);
    if (!select) {
        return;
    }
    // Show the select and its label
    select.classList.remove("d-none");
    document.getElementById(labelId)?.classList.remove("d-none");

    // Reset options
    select.innerHTML = "";

    // Default placeholder option
    const placeholder = new Option(defaultText, "", true, true);
    placeholder.hidden = placeholder.disabled = true;
    select.append(placeholder);

    // Add each item
    for (const { id, name } of items) {
        select.append(new Option(name, id));
    }
};

// SignalR event handlers
connection.on("CountryFlightSelectedOrigin", airports =>
    populateSelect("Origin", "originLabel", airports)
);

connection.on("CountryFlightSelectedDestination", airports =>
    populateSelect("Destination", "destinationLabel", airports)
);

// Start the connection
connection.start().catch(err => console.error(err));

// Invoke helper to call hub methods with logging
const invokeHub = (method, arg) =>
    connection.invoke(method, arg).catch(err => console.error(err));

// Wire up change events for country selects
document.getElementById("OriginCountry")?.addEventListener("change", e =>
    invokeHub("SelectCountryForFlightOrigin", e.target.value)
);

document.getElementById("DestinationCountry")?.addEventListener("change", e =>
    invokeHub("SelectCountryForFlightDestination", e.target.value)
);