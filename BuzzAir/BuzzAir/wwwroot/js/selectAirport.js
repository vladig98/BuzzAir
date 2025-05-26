"use strict";

const connection = new signalR.HubConnectionBuilder()
    .withUrl("/getSelectOptions")
    .build();

// Helper to populate a <select> with a placeholder and items
const populateSelect = (selectId, placeholderText, items) => {
    const select = document.getElementById(selectId);
    if (!select) {
        return;
    }

    // Show the select
    select.parentElement?.classList.remove("d-none");

    // Reset options
    select.innerHTML = "";

    // Add placeholder
    const placeholder = new Option(placeholderText, "", true, true);
    placeholder.hidden = placeholder.disabled = true;
    select.append(placeholder);

    // Add each item
    for (const { id, name } of items) {
        select.append(new Option(name, id));
    }
};

// SignalR event handlers
connection.on("CountrySelected", states =>
    populateSelect("State", "Select State", states)
);

connection.on("StateSelected", cities =>
    populateSelect("City", "Select City", cities)
);

// Start the SignalR connection
(async () => {
    try {
        await connection.start();
    } catch (err) {
        console.error("SignalR start failed:", err);
    }
})();

// Invoke helper to call hub methods with logging
const invokeHub = (method, arg) =>
    connection.invoke(method, arg).catch(err => console.error(err));

// Cache selectors
const countrySelect = document.getElementById("Country");
const stateSelect = document.getElementById("State");

// Wire up change events
countrySelect?.addEventListener("change", e =>
    invokeHub("SelectCountry", e.target.value)
);

stateSelect?.addEventListener("change", e =>
    invokeHub("SelectState", e.target.value)
);