"use strict";

(function () {
    const log = (...args) => console.log("[Booking]", ...args);
    const warn = (...args) => console.warn("[Booking]", ...args);
    const error = (...args) => console.error("[Booking]", ...args);

    function formatISODate(d) {
        return d.toISOString().split("T")[0];
    }

    function destroyPickerIfExists(name) {
        if (window[name]) {
            try { window[name].destroy(); } catch (_) { }
            window[name] = null;
        }
    }

    function createDeparturePicker(allowedDates, el, onChangeCallback) {
        destroyPickerIfExists("departurePicker");
        const sorted = allowedDates.slice().sort();
        const instance = flatpickr(el, {
            enable: sorted,
            dateFormat: "Y-m-d",
            minDate: sorted[0],
            maxDate: sorted[sorted.length - 1],
            disableMobile: true,
            onChange: (selectedDates, dateStr) => {
                if (onChangeCallback) onChangeCallback(dateStr);
            }
        });
        if (sorted.length) instance.setDate(sorted[0], true);
        window.departurePicker = instance;
        el.disabled = false;
    }

    function createReturnPicker(allowedDates, el) {
        destroyPickerIfExists("returnPicker");
        const sorted = (allowedDates || []).slice().sort();
        const instance = flatpickr(el, {
            enable: sorted,
            dateFormat: "Y-m-d",
            minDate: sorted[0] ?? null,
            maxDate: sorted[sorted.length - 1] ?? null,
            disableMobile: true
        });
        if (sorted.length) instance.setDate(sorted[0], false);
        window.returnPicker = instance;
        el.disabled = sorted.length === 0;
        if (!sorted.length) el.value = "";
    }

    document.addEventListener("DOMContentLoaded", async () => {
        const departureInput = document.getElementById("departureDate");
        const returnInput = document.getElementById("returnDate");
        const returnWrapper = document.getElementById("returnDateWrapper");
        const bookingForm = document.getElementById("bookingForm");
        const tripTypeRadios = document.querySelectorAll("input[name='tripType']");
        const originIdInput = document.getElementById("originId");
        const destinationIdInput = document.getElementById("destinationId");

        let availableDepartureDatesSet = new Set();
        let availableReturnDatesSet = new Set();

        const hub = new signalR.HubConnectionBuilder()
            .withUrl("/flightHub")
            .withAutomaticReconnect()
            .configureLogging(signalR.LogLevel.Warning)
            .build();

        hub.onreconnecting(err => warn("SignalR reconnecting:", err?.message || err));
        hub.onreconnected(() => log("SignalR reconnected"));
        hub.onclose(() => warn("SignalR connection closed"));

        try {
            await hub.start();
            log("SignalR connected");
        } catch (err) {
            error("Failed to start SignalR:", err);
        }

        function updateTripTypeVisibility() {
            const oneWay = document.querySelector("input[name='tripType']:checked")?.value === "OneWay";

            if (availableReturnDatesSet.size === 0) {
                // No return dates
                returnWrapper.style.display = "none";

                // Force OneWay selection
                tripTypeRadios.forEach(r => {
                    if (r.value === "Return") r.disabled = true;
                    if (r.value === "OneWay") {
                        r.checked = true;
                        r.disabled = true;
                    }
                });

                if (window.returnPicker) {
                    window.returnPicker.clear();
                    window.returnPicker.set("enable", []);
                }
                returnInput.value = "";

            } else {
                // Return dates exist
                returnWrapper.style.display = oneWay ? "none" : "block";

                tripTypeRadios.forEach(r => {
                    r.disabled = false; // all clickable
                });

                if (oneWay && window.returnPicker) {
                    window.returnPicker.clear();
                    window.returnPicker.set("enable", []);
                    returnInput.value = "";
                }
            }
        }

        tripTypeRadios.forEach(r => r.addEventListener("change", updateTripTypeVisibility));

        async function fetchDates() {
            const originId = originIdInput.value;
            const destinationId = destinationIdInput.value;
            if (!originId || !destinationId) return; // only run if both chosen

            try {
                // Fetch departure dates
                const depPayload = await hub.invoke("GetAvailableDates", originId, destinationId);
                const departureDates = Object.values(depPayload || {}).map(d => formatISODate(new Date(d))).sort();
                availableDepartureDatesSet = new Set(departureDates);
                createDeparturePicker(departureDates, departureInput, handleDepartureSelected);
                if (departureDates.length) departureInput.value = departureDates[0];

                // Fetch return dates
                const retPayload = await hub.invoke("GetAvailableDates", destinationId, originId);
                const returnDates = Object.values(retPayload || {}).map(d => formatISODate(new Date(d))).sort();
                availableReturnDatesSet = new Set(returnDates);

                updateTripTypeVisibility();

                log("Dates applied", { departures: availableDepartureDatesSet.size, returns: availableReturnDatesSet.size });

            } catch (err) {
                warn("Failed to fetch dates:", err);
            }
        }

        async function handleDepartureSelected(isoDate) {
            if (!isoDate) return;
            const tripType = document.querySelector("input[name='tripType']:checked")?.value;
            if (tripType === "OneWay") return;

            const originId = originIdInput.value;
            const destinationId = destinationIdInput.value;
            if (!originId || !destinationId) return;

            try {
                const retPayload = await hub.invoke("GetAvailableReturnDates", destinationId, originId, isoDate);
                const returnDates = Object.values(retPayload || {}).map(d => formatISODate(new Date(d))).sort();
                availableReturnDatesSet = new Set(returnDates);

                if (returnDates.length) {
                    createReturnPicker(returnDates, returnInput);
                    returnInput.value = returnDates[0];
                    if (window.returnPicker) window.returnPicker.setDate(returnDates[0], true);
                } else {
                    destroyPickerIfExists("returnPicker");
                    returnInput.value = "";
                    returnInput.disabled = true;
                }

                updateTripTypeVisibility();
                log("Return dates applied", { count: availableReturnDatesSet.size });

            } catch (err) {
                warn("Failed to fetch return dates:", err);
            }
        }

        originIdInput.addEventListener("change", fetchDates);
        destinationIdInput.addEventListener("change", fetchDates);

        bookingForm.addEventListener("submit", e => {
            const tripType = document.querySelector("input[name='tripType']:checked")?.value;
            const originId = originIdInput.value;
            const destinationId = destinationIdInput.value;

            if (!originId || !destinationId) { e.preventDefault(); alert("Please select origin and destination"); return; }
            if (!departureInput.value) { e.preventDefault(); alert("Please select a departure date"); return; }
            if (tripType !== "OneWay" && !returnInput.value) { e.preventDefault(); alert("Please select a return date or choose One Way"); return; }
            if (!availableDepartureDatesSet.has(departureInput.value)) { e.preventDefault(); alert("Departure date not allowed"); return; }
            if (tripType !== "OneWay" && !availableReturnDatesSet.has(returnInput.value)) { e.preventDefault(); alert("Return date not allowed"); return; }
        });

        log("Home booking script loaded");
    });
})();
