/* wwwroot/js/selectHome.js */
"use strict";

(function () {
    const log = (...args) => console.log("[selectHome]", ...args);
    const warn = (...args) => console.warn("[selectHome]", ...args);
    const error = (...args) => console.error("[selectHome]", ...args);

    // Small helper: format Date -> YYYY-MM-DD
    function toISODate(d) {
        return d.toISOString().split("T")[0];
    }

    // check dd in allowedDatesSet (strings "YYYY-MM-DD")
    function isAllowed(dateStr, allowedSet) {
        if (!allowedSet) return false;
        return allowedSet.has(dateStr);
    }

    document.addEventListener("DOMContentLoaded", async () => {
        const fromSelect = document.getElementById("fromSelect");
        const toSelect = document.getElementById("toSelect");
        const departureDate = document.getElementById("departureDate");
        const returnDate = document.getElementById("returnDate");
        const returnWrapper = document.getElementById("returnDateWrapper");
        const tripTypeRadios = document.querySelectorAll("input[name='tripType']");
        const form = document.getElementById("bookingForm");

        if (!fromSelect || !toSelect || !departureDate || !returnDate || !form) {
            error("Missing expected DOM elements. Check element IDs.");
            return;
        }

        // Disable UI until ready
        fromSelect.disabled = true;
        toSelect.disabled = true;
        departureDate.disabled = true;
        returnDate.disabled = true;

        // prepare allowed dates store
        let allowedDatesSet = null;
        let allowedMin = null;
        let allowedMax = null;

        // 1-year global maximum (today + 1 year)
        const today = new Date();
        const oneYear = new Date(today);
        oneYear.setFullYear(today.getFullYear() + 1);
        const oneYearISO = toISODate(oneYear);

        // Trip type toggle initial behaviour
        function updateTripTypeUI() {
            const oneWay = document.querySelector("input[name='tripType']:checked")?.value === "OneWay";
            if (oneWay) {
                returnWrapper.style.display = "none";
                returnDate.value = "";
            } else {
                returnWrapper.style.display = "block";
            }
        }
        updateTripTypeUI();
        tripTypeRadios.forEach(r => r.addEventListener("change", updateTripTypeUI));

        // Build SignalR connection
        if (!window.signalR) {
            error("SignalR client is not loaded. Ensure you included the signalr script before selectHome.js.");
            // Still try to load fallback UI (we will populate dummy origins)
            populateOriginsFallback();
            return;
        }

        const connection = new signalR.HubConnectionBuilder()
            .withUrl("/flightHub")
            .withAutomaticReconnect()
            .configureLogging(signalR.LogLevel.Warning)
            .build();

        connection.onreconnecting(err => {
            warn("SignalR reconnecting:", err?.message || err);
        });
        connection.onreconnected(() => {
            log("SignalR reconnected.");
        });
        connection.onclose(() => {
            warn("SignalR connection closed.");
        });

        // Optional server push listeners: update lists live
        connection.on("DestinationsUpdated", (from, dests) => {
            log("Server push dests update for", from, dests);
            if (fromSelect.value === from) populateDestinations(dests);
        });
        connection.on("AvailableDatesUpdated", (from, to, payload) => {
            log("Server push available dates update", { from, to, payload });
            // if current pair matches, refresh allowed dates
            if (fromSelect.value === from && toSelect.value === to) {
                applyAvailableDates(payload);
            }
        });

        // helper to populate origins (fallback if hub method missing)
        async function populateOrigins() {
            try {
                const origins = await connection.invoke("GetOrigins");
                if (Array.isArray(origins) && origins.length) {
                    fromSelect.innerHTML = '<option value="">Choose departure...</option>';
                    origins.forEach(o => {
                        const opt = document.createElement("option");
                        opt.value = o.code ?? o.value ?? o.id ?? o;
                        opt.textContent = o.name ?? o.text ?? o;
                        fromSelect.appendChild(opt);
                    });
                    fromSelect.disabled = false;
                    log("Loaded origins from hub.");
                    return;
                }
                throw new Error("GetOrigins returned empty or invalid payload");
            } catch (ex) {
                warn("GetOrigins failed, using fallback origins. Error:", ex?.message || ex);
                populateOriginsFallback();
            }
        }

        function populateOriginsFallback() {
            const fallback = [
                { code: "SOF", name: "Sofia (SOF)" },
                { code: "LON", name: "London (LON)" },
                { code: "NYC", name: "New York (NYC)" }
            ];
            fromSelect.innerHTML = '<option value="">Choose departure...</option>';
            fallback.forEach(o => {
                const opt = document.createElement("option");
                opt.value = o.code;
                opt.textContent = o.name;
                fromSelect.appendChild(opt);
            });
            fromSelect.disabled = false;
            log("Populated fallback origins.");
        }

        function populateDestinations(dests) {
            toSelect.innerHTML = '<option value="">Choose destination...</option>';
            if (!Array.isArray(dests) || !dests.length) {
                toSelect.disabled = true;
                warn("No destinations returned for selected origin.");
                return;
            }
            dests.forEach(d => {
                const opt = document.createElement("option");
                opt.value = d.code ?? d.value ?? d.id ?? d;
                opt.textContent = d.name ?? d.text ?? d;
                toSelect.appendChild(opt);
            });
            toSelect.disabled = false;
            log("Destinations populated.", dests);
        }

        async function loadDestinationsForOrigin(origin) {
            toSelect.disabled = true;
            departureDate.value = "";
            returnDate.value = "";
            departureDate.disabled = true;
            returnDate.disabled = true;
            allowedDatesSet = null;
            try {
                const dests = await connection.invoke("GetDestinations", origin);
                populateDestinations(dests);
            } catch (ex) {
                warn("GetDestinations failed, using fallback destinations. Error:", ex?.message || ex);
                // fallback
                populateDestinations([
                    { code: "LON", name: "London" },
                    { code: "PAR", name: "Paris" },
                    { code: "BER", name: "Berlin" }
                ]);
            }
        }

        function applyAvailableDates(payload) {
            // payload expected: { allowedDates: ["YYYY-MM-DD", ...], min: "YYYY-MM-DD", max: "YYYY-MM-DD" }
            if (!payload) {
                error("applyAvailableDates requires a payload");
                return;
            }
            allowedDatesSet = new Set(Array.isArray(payload.allowedDates) ? payload.allowedDates : []);
            allowedMin = payload.min ?? null;
            allowedMax = payload.max ?? null;

            // Respect 1 year global max
            const effectiveMax = allowedMax ? (allowedMax <= oneYearISO ? allowedMax : oneYearISO) : oneYearISO;
            const effectiveMin = allowedMin ?? toISODate(today);

            departureDate.min = effectiveMin;
            departureDate.max = effectiveMax;
            departureDate.disabled = false;

            returnDate.min = effectiveMin;
            returnDate.max = effectiveMax;
            returnDate.disabled = false;

            log("Available dates applied", { effectiveMin, effectiveMax, allowedCount: allowedDatesSet.size });

            // If a user already has a date selected, validate it now:
            if (departureDate.value && !isAllowed(departureDate.value, allowedDatesSet)) {
                warn("Current departure date not available anymore, clearing it.");
                departureDate.value = "";
            }
            if (returnDate.value && !isAllowed(returnDate.value, allowedDatesSet)) {
                warn("Current return date not available anymore, clearing it.");
                returnDate.value = "";
            }
        }

        // Attach change handlers
        fromSelect.addEventListener("change", async () => {
            const from = fromSelect.value;
            if (!from) {
                toSelect.innerHTML = '<option value="">Choose destination...</option>';
                toSelect.disabled = true;
                return;
            }
            await loadDestinationsForOrigin(from);
        });

        toSelect.addEventListener("change", async () => {
            const from = fromSelect.value;
            const to = toSelect.value;
            if (!from || !to) return;

            // ask server for available dates for this pair
            try {
                const payload = await connection.invoke("GetAvailableDates", from, to);
                // expected payload shape: { allowedDates: [...], min: "YYYY-MM-DD", max: "YYYY-MM-DD" }
                applyAvailableDates(payload);
            } catch (ex) {
                warn("GetAvailableDates failed, using fallback date window. Error:", ex?.message || ex);
                // fallback: allow next 90 days on even days
                const fallbackAllowed = [];
                const start = new Date();
                for (let i = 0; i < 90; i += 1) {
                    const d = new Date(start);
                    d.setDate(start.getDate() + i);
                    const iso = toISODate(d);
                    if (d.getDate() % 2 === 0) fallbackAllowed.push(iso);
                }
                applyAvailableDates({
                    allowedDates: fallbackAllowed,
                    min: fallbackAllowed[0],
                    max: fallbackAllowed[fallbackAllowed.length - 1]
                });
            }
        });

        // Validate picked date is in allowedDatesSet and <= 1 year
        function dateInputHandler(e) {
            const val = e.target.value;
            if (!val) return;

            // block > 1 year
            if (val > oneYearISO) {
                alert("You cannot pick dates more than 1 year from today.");
                e.target.value = "";
                return;
            }

            if (!allowedDatesSet || allowedDatesSet.size === 0) {
                // no allowed info -> accept but warn
                warn("No allowed-dates info available. The date may not correspond to flights.");
                return;
            }

            if (!allowedDatesSet.has(val)) {
                alert("No flights on the selected date. Please choose a different date.");
                e.target.value = "";
            }
        }
        departureDate.addEventListener("input", dateInputHandler);
        returnDate.addEventListener("input", e => {
            dateInputHandler(e);

            // ensure return >= departure
            if (departureDate.value && returnDate.value && returnDate.value < departureDate.value) {
                alert("Return must be the same day or after departure.");
                returnDate.value = "";
            }
        });

        // Form submit validation
        form.addEventListener("submit", e => {
            // minimal client side checks
            const tripType = document.querySelector("input[name='tripType']:checked")?.value;
            if (!fromSelect.value || !toSelect.value) {
                e.preventDefault();
                alert("Please select both origin and destination.");
                return;
            }
            if (!departureDate.value) {
                e.preventDefault();
                alert("Please select a departure date.");
                return;
            }
            if (tripType !== "OneWay" && !returnDate.value) {
                e.preventDefault();
                alert("Please select a return date or choose One Way.");
                return;
            }
            if (allowedDatesSet && !allowedDatesSet.has(departureDate.value)) {
                e.preventDefault();
                alert("Departure date invalid (no flights).");
                return;
            }
            if (tripType !== "OneWay" && allowedDatesSet && !allowedDatesSet.has(returnDate.value)) {
                e.preventDefault();
                alert("Return date invalid (no flights).");
                return;
            }
            // allow submission — the form will post to server
        });

        // Start the hub connection and load origins afterwards
        try {
            await connection.start();
            log("SignalR connected.");
            // Load origins from server (fallbacks handled inside)
            await populateOrigins();
        } catch (ex) {
            error("Failed to start SignalR connection:", ex?.message || ex);
            // fallback origins if hub not available
            populateOriginsFallback();
        }
    });
})();
