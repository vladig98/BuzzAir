"use strict";

const connection = new signalR.HubConnectionBuilder()
    .withUrl("/getSelectOptions")
    .build();

const totalAmountEl = document.getElementById("totalAmount");

// Update total amount when conversion completes
connection.on("CurrencyConvertionCompleted", (currency, price) => {
    if (totalAmountEl) {
        totalAmountEl.textContent = `${currency} ${price.toFixed(2)}`;
    }
});

// Start the SignalR connection
(async () => {
    try {
        await connection.start();
    } catch (err) {
        console.error("SignalR start failed:", err);
    }
})();