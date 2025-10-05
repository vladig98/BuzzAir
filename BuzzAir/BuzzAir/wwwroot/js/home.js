"use strict";

(function () {
    const log = (...args) => console.log("[Booking]", ...args);
    const warn = (...args) => console.warn("[Booking]", ...args);
    const error = (...args) => console.error("[Booking]", ...args);

    function formatISODate(d) {
        return d.toISOString().split("T")[0];
    }

    function toDateISOStringFromInput(date) {
        if (!date) return null;
        const dt = date instanceof Date ? date : new Date(date);
        return formatISODate(dt);
    }

    function destroyPickerIfExists(name) {
        if (window[name]) {
            try { window[name].destroy(); } catch (_) { }
            window[name] = null;
        }
    }

    function createDeparturePicker(allowedDates, el, onChangeCallback) {
        const sorted = allowedDates.slice().sort();
        destroyPickerIfExists("departurePicker");
        const instance = flatpickr(el, {
            enable: sorted,
            dateFormat: "Y-m-d",
            minDate: sorted[0],
            maxDate: sorted[sorted.length - 1],
            disableMobile: true,
            onChange: function (selectedDates, dateStr) {
                onChangeCallback(dateStr);
            }
        });
        // select first available and trigger onChange
        if (sorted.length) instance.setDate(sorted[0], true);
        window.departurePicker = instance;
        el.disabled = false;
    }

    function createReturnPicker(allowedDates, el) {
        const sorted = (allowedDates || []).slice().sort();
        destroyPickerIfExists("returnPicker");
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
        const originSelect = document.getElementById("fromSelect");
        const destinationSelect = document.getElementById("toSelect");
        const departureInput = document.getElementById("departureDate");
        const returnInput = document.getElementById("returnDate");
        const returnWrapper = document.getElementById("returnDateWrapper");
        const tripTypeRadios = document.querySelectorAll("input[name='tripType']");
        const bookingForm = document.getElementById("bookingForm");

        if (!originSelect || !destinationSelect || !departureInput || !returnInput || !bookingForm) {
            error("Required DOM elements missing");
            return;
        }

        originSelect.disabled = true;
        destinationSelect.disabled = true;
        departureInput.disabled = true;
        returnInput.disabled = true;

        let availableDepartureDatesSet = new Set();
        let availableReturnDatesSet = new Set();

        function updateTripTypeVisibility() {
            const oneWay = document.querySelector("input[name='tripType']:checked")?.value === "OneWay";
            returnWrapper.style.display = oneWay ? "none" : "block";
            if (oneWay) {
                if (window.returnPicker) {
                    window.returnPicker.clear();
                    window.returnPicker.set("enable", []);
                }
                returnInput.value = "";
            }
        }

        updateTripTypeVisibility();
        tripTypeRadios.forEach(r => r.addEventListener("change", updateTripTypeVisibility));

        if (!window.signalR) {
            error("SignalR client not loaded");
            return;
        }

        const flightHub = new signalR.HubConnectionBuilder()
            .withUrl("/flightHub")
            .withAutomaticReconnect()
            .configureLogging(signalR.LogLevel.Warning)
            .build();

        flightHub.onreconnecting(err => warn("SignalR reconnecting:", err?.message || err));
        flightHub.onreconnected(() => log("SignalR reconnected"));
        flightHub.onclose(() => warn("SignalR connection closed"));

        flightHub.on("DestinationsUpdated", (originId, destinations) => {
            if (originSelect.value === originId) populateDestinationOptions(destinations);
        });

        flightHub.on("AvailableDatesUpdated", (originId, destinationId, payload) => {
            if (originSelect.value === originId && destinationSelect.value === destinationId) {
                applyDepartureDates(payload, true);
            }
        });

        let pageIndex = 0;
        const itemsPerPage = 100;
        let isLoading = false;
        let hasMoreData = true;

        async function populateOriginOptions(pageIndex = 0, pageSize = 100, append = false) {
            if (isLoading) return;
            isLoading = true;

            try {
                const originsByCountry = await flightHub.invoke("GetOrigins", pageIndex, pageSize);
                if (!originsByCountry || !Object.keys(originsByCountry).length) {
                    hasMoreData = false;
                    return;
                }

                if (!append) {
                    originSelect.innerHTML = '<option selected disabled hidden value="">Choose departure...</option>';
                }

                // Sort countries alphabetically
                const sortedCountries = Object.keys(originsByCountry).sort();

                for (const country of sortedCountries) {
                    const group = document.createElement("optgroup");
                    group.label = country;

                    const airports = Object.entries(originsByCountry[country])
                        .sort(([, a], [, b]) => a.localeCompare(b));

                    for (const [id, name] of airports) {
                        const opt = document.createElement("option");
                        opt.value = id;
                        opt.textContent = name;
                        group.appendChild(opt);
                    }

                    originSelect.appendChild(group);
                }

                originSelect.disabled = false;
                log(`Loaded page ${pageIndex + 1}`);
            } catch (ex) {
                warn("Failed to load origins:", ex.message || ex);
            } finally {
                isLoading = false;
            }
        }

        function populateDestinationOptions(destinations) {
            destinationSelect.innerHTML = '<option selected disabled hidden value="">Choose destination...</option>';
            if (!destinations || !Object.keys(destinations).length) {
                destinationSelect.disabled = true;
                warn("No destinations available");
                return;
            }

            // Sort countries alphabetically
            const sortedCountries = Object.keys(destinations).sort();

            // Append countries and destinations
            sortedCountries.forEach(country => {
                const optgroup = document.createElement("optgroup");
                optgroup.label = country;

                const airports = Object.entries(destinations[country])
                    .sort(([, a], [, b]) => a.localeCompare(b));

                for (const [id, name] of airports) {
                    const opt = document.createElement("option");
                    opt.value = id;
                    opt.textContent = name;
                    optgroup.appendChild(opt);
                }

                destinationSelect.appendChild(optgroup);
            });

            destinationSelect.disabled = false;
            log("Destinations populated and grouped by country");
        }


        async function loadDestinationsForOrigin(originId) {
            destinationSelect.disabled = true;
            departureInput.value = "";
            returnInput.value = "";
            departureInput.disabled = true;
            returnInput.disabled = true;
            availableDepartureDatesSet.clear();
            availableReturnDatesSet.clear();
            destroyPickerIfExists("departurePicker");
            destroyPickerIfExists("returnPicker");

            try {
                const dests = await flightHub.invoke("GetDestinations", originId);
                populateDestinationOptions(dests);
            } catch (ex) {
                warn("Failed to load destinations:", ex.message || ex);
            }
        }

        function applyDepartureDates(payload, setDefault) {
            if (!payload) return;
            const dates = Object.values(payload).map(d => formatISODate(new Date(d))).sort();
            availableDepartureDatesSet = new Set(dates);
            createDeparturePicker(dates, departureInput, handleDepartureSelected);
            destroyPickerIfExists("returnPicker");
            availableReturnDatesSet.clear();
            returnInput.value = "";
            if (setDefault && dates.length) {
                // createDeparturePicker already set the first and triggered onChange if present,
                // but ensure departureInput shows the first date
                departureInput.value = dates[0];
            }
            log("Departure dates applied", { count: availableDepartureDatesSet.size });
        }

        async function handleDepartureSelected(isoDate) {
            if (!isoDate) return;
            const tripType = document.querySelector("input[name='tripType']:checked")?.value;
            if (tripType === "OneWay") return;
            const originId = originSelect.value;
            const destinationId = destinationSelect.value;
            if (!originId || !destinationId) return;

            try {
                const payload = await flightHub.invoke("GetAvailableReturnDates", destinationId, originId, isoDate);
                const returnDates = Object.values(payload).map(d => formatISODate(new Date(d))).sort();
                availableReturnDatesSet = new Set(returnDates);
                if (returnDates.length) {
                    createReturnPicker(returnDates, returnInput);
                    returnInput.value = returnDates[0];
                    if (window.returnPicker) window.returnPicker.setDate(returnDates[0], true);
                } else {
                    destroyPickerIfExists("returnPicker");
                    returnInput.value = "";
                    returnInput.disabled = true;
                    warn("No return dates available after selected departure");
                }
                log("Return dates applied", { count: availableReturnDatesSet.size });
            } catch (ex) {
                warn("Failed to load return dates:", ex.message || ex);
                destroyPickerIfExists("returnPicker");
                returnInput.value = "";
                returnInput.disabled = true;
            }
        }

        originSelect.addEventListener("change", async () => {
            const originId = originSelect.value;
            if (!originId) return;
            await loadDestinationsForOrigin(originId);
        });

        destinationSelect.addEventListener("change", async () => {
            const originId = originSelect.value;
            const destinationId = destinationSelect.value;
            if (!originId || !destinationId) return;
            try {
                const payload = await flightHub.invoke("GetAvailableDates", originId, destinationId);
                const returnDates = await flightHub.invoke("GetAvailableDates", destinationId, originId);
                applyDepartureDates(payload, true);

                // Check if there are any return dates
                const hasReturnDates = Object.keys(returnDates || {}).length > 0;

                // Find trip type radios
                const oneWayRadio = document.querySelector("input[name='tripType'][value='OneWay']");
                const returnRadio = document.querySelector("input[name='tripType'][value='Return']");

                if (!hasReturnDates) {
                    // Auto-select One Way and disable Return
                    if (oneWayRadio) oneWayRadio.checked = true;
                    if (returnRadio) {
                        returnRadio.disabled = true;
                        returnRadio.checked = false;
                    }

                    // Hide return date picker and clear any previous value
                    if (window.returnPicker) {
                        window.returnPicker.clear();
                        window.returnPicker.set("enable", []);
                    }
                    returnInput.value = "";
                    returnInput.disabled = true;
                    returnWrapper.style.display = "none";

                    log("No return dates available — switched to One Way mode");
                } else {
                    // Enable the Return radio if previously disabled
                    if (returnRadio) returnRadio.disabled = false;
                    returnWrapper.style.display = "block";
                }
            } catch (ex) {
                warn("Failed to load available departure dates:", ex.message || ex);
            }
        });

        bookingForm.addEventListener("submit", e => {
            const tripType = document.querySelector("input[name='tripType']:checked")?.value;
            if (!originSelect.value || !destinationSelect.value) { e.preventDefault(); alert("Please select origin and destination"); return; }
            if (!departureInput.value) { e.preventDefault(); alert("Please select a departure date"); return; }
            if (tripType !== "OneWay" && !returnInput.value) { e.preventDefault(); alert("Please select a return date or choose One Way"); return; }
            if (!availableDepartureDatesSet.has(departureInput.value)) { e.preventDefault(); alert("Departure date not allowed"); return; }
            if (tripType !== "OneWay" && !availableReturnDatesSet.has(returnInput.value)) { e.preventDefault(); alert("Return date not allowed"); return; }
        });

        originSelect.addEventListener("scroll", async () => {
            if (
                !hasMoreData ||
                isLoading ||
                originSelect.scrollTop + originSelect.clientHeight < originSelect.scrollHeight - 50
            ) return;

            pageIndex++;
            await populateOriginOptions(pageIndex, itemsPerPage, true);
        });

        try {
            await flightHub.start();
            log("SignalR connected");
            await populateOriginOptions(pageIndex, itemsPerPage);
        } catch (ex) {
            error("Failed to start SignalR:", ex.message || ex);
        }
    });
})();
