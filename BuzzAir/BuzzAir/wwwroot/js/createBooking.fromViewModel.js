"use strict";

(function () {
    const $ = id => document.getElementById(id);
    const log = (...a) => console.log("[CreateBooking]", ...a);
    const warn = (...a) => console.warn("[CreateBooking]", ...a);
    const error = (...a) => console.error("[CreateBooking]", ...a);

    function formatMoney(v) { return `€${(Number(v) || 0).toFixed(2)}`; }
    function toLocalTimeShort(iso) {
        try { return new Date(iso).toLocaleTimeString([], { hour: "2-digit", minute: "2-digit" }); }
        catch (e) { return ""; }
    }

    const outboundGrid = $("outboundGrid");
    const inboundGrid = $("inboundGrid");
    const inboundSection = $("inboundSection");
    const flightsNextBtn = $("flightsNextBtn");

    const passengersCount = $("passengersCount");
    const passengersContainer = $("passengersContainer");
    const passengersBackBtn = $("passengersBackBtn");
    const passengersNextBtn = $("passengersNextBtn");

    const servicesContainer = $("servicesContainer");
    const servicesBackBtn = $("servicesBackBtn");
    const servicesNextBtn = $("servicesNextBtn");

    const paymentBackBtn = $("paymentBackBtn");
    const payBtn = $("payBtn");

    const summaryOutbound = $("summaryOutbound");
    const summaryInbound = $("summaryInbound");
    const summaryPax = $("summaryPax");
    const summaryServiceList = $("summaryServiceList");
    const summaryTotal = $("summaryTotal");
    const paymentTotal = $("paymentTotal");

    const templatesRoot = $("templates");
    const passengerTemplateNode = templatesRoot ? templatesRoot.querySelector("#templatePassenger").firstElementChild : null;
    const serviceTemplateContainer = templatesRoot ? templatesRoot.querySelector("#templateService") : null;

    // state
    const state = {
        outboundFlights: [],
        inboundFlights: [],
        servicesCatalog: [],
        selectedOutboundId: null,
        selectedInboundId: null,
        passengers: [],
        passengerServices: {}, // index => Set
        totals: { flights: 0, services: 0 }
    };

    function clearChildren(el) { while (el.firstChild) el.removeChild(el.firstChild); }

    // read flights and services from server-rendered DOM (data-* attributes)
    function loadFromDom() {
        state.outboundFlights = Array.from(outboundGrid.querySelectorAll(".flight-card")).map(el => ({
            id: el.dataset.id,
            flightNumber: el.dataset.flightNumber,
            originName: el.dataset.originName,
            destinationName: el.dataset.destinationName,
            departureUtc: el.dataset.departure,
            arrivalUtc: el.dataset.arrival,
            priceInEur: parseFloat(el.dataset.price || "0"),
            summary: el.dataset.summary || ""
        }));

        state.inboundFlights = Array.from(inboundGrid ? inboundGrid.querySelectorAll(".flight-card") : []).map(el => ({
            id: el.dataset.id,
            flightNumber: el.dataset.flightNumber,
            originName: el.dataset.originName,
            destinationName: el.dataset.destinationName,
            departureUtc: el.dataset.departure,
            arrivalUtc: el.dataset.arrival,
            priceInEur: parseFloat(el.dataset.price || "0"),
            summary: el.dataset.summary || ""
        }));

        // services: build from the service template container
        state.servicesCatalog = [];
        if (serviceTemplateContainer) {
            serviceTemplateContainer.querySelectorAll(".service-pill").forEach(el => {
                state.servicesCatalog.push({
                    id: el.dataset.serviceId,
                    name: el.textContent.trim(),
                    price: parseFloat((el.querySelector("small") || {}).textContent?.replace(/[^\d.,-]/g, "") || "0") || 0
                });
            });
        }

        // default select first outbound/inbound if available
        if (state.outboundFlights.length) {
            const first = state.outboundFlights[0];
            const firstCard = outboundGrid.querySelector(`.flight-card[data-id="${first.id}"]`);
            if (firstCard) {
                firstCard.classList.add("active");
                state.selectedOutboundId = first.id;
            }
        }
        if (state.inboundFlights.length) {
            const f = state.inboundFlights[0];
            const firstCard = inboundGrid.querySelector(`.flight-card[data-id="${f.id}"]`);
            if (firstCard) {
                firstCard.classList.add("active");
                state.selectedInboundId = f.id;
            }
        }
    }

    // wire click handlers for flight cards
    function wireFlightCardClicks() {
        outboundGrid.querySelectorAll(".flight-card").forEach(card => {
            card.addEventListener("click", () => {
                outboundGrid.querySelectorAll(".flight-card").forEach(c => c.classList.remove("active"));
                card.classList.add("active");
                state.selectedOutboundId = card.dataset.id;
                updateSummary();
            });
        });

        if (inboundGrid) {
            inboundGrid.querySelectorAll(".flight-card").forEach(card => {
                card.addEventListener("click", () => {
                    inboundGrid.querySelectorAll(".flight-card").forEach(c => c.classList.remove("active"));
                    card.classList.add("active");
                    state.selectedInboundId = card.dataset.id;
                    updateSummary();
                });
            });
        }
    }

    // render passengers by cloning server-rendered partial
    function renderPassengersUi() {
        clearChildren(passengersContainer);
        for (let i = 0; i < state.passengers.length; i++) {
            const node = passengerTemplateNode.cloneNode(true);
            // set title
            node.querySelector(".passenger-title").textContent = `Passenger ${i + 1}`;

            // toggle doc
            const toggle = node.querySelector(".toggle-doc");
            const docSection = node.querySelector(".travel-doc-section");
            toggle.addEventListener("click", () => {
                state.passengers[i].hasDoc = !state.passengers[i].hasDoc;
                docSection.classList.toggle("hidden", !state.passengers[i].hasDoc);
            });

            // bind fields (data-field)
            node.querySelectorAll("[data-field]").forEach(el => {
                const field = el.dataset.field;
                // preload if value exists
                if (state.passengers[i][field]) el.value = state.passengers[i][field];

                el.addEventListener("input", ev => {
                    state.passengers[i][field] = ev.target.value;
                });
            });

            passengersContainer.appendChild(node);
        }

        summaryPax.textContent = state.passengers.length;
        updateSummary();
    }

    // render services (clone service pills per passenger)
    function renderServicesUi() {
        clearChildren(servicesContainer);
        for (let i = 0; i < state.passengers.length; i++) {
            const header = document.createElement("div");
            header.className = "fw-bold mb-2";
            header.textContent = `Passenger ${i + 1}`;
            const row = document.createElement("div");
            row.className = "d-flex gap-2 flex-wrap mb-3";

            state.servicesCatalog.forEach(s => {
                // find the original pill HTML to reuse markup (keeps styling centralized)
                const proto = serviceTemplateContainer.querySelector(`.service-pill[data-service-id="${s.id}"]`);
                const pill = proto ? proto.cloneNode(true) : document.createElement("div");
                pill.classList.add("service-pill");
                pill.dataset.serviceId = s.id;
                pill.addEventListener("click", () => {
                    state.passengerServices[i] = state.passengerServices[i] || new Set();
                    if (state.passengerServices[i].has(s.id)) {
                        state.passengerServices[i].delete(s.id);
                        pill.classList.remove("active");
                    } else {
                        state.passengerServices[i].add(s.id);
                        pill.classList.add("active");
                    }
                    recalcTotals();
                    updateSummary();
                });
                row.appendChild(pill);
            });

            servicesContainer.appendChild(header);
            servicesContainer.appendChild(row);
        }
    }

    function recalcTotals() {
        const out = state.outboundFlights.find(f => f.id === state.selectedOutboundId);
        const inbound = state.inboundFlights.find(f => f.id === state.selectedInboundId);
        const flightsTotal = (out?.priceInEur || 0) + (inbound?.priceInEur || 0);
        let svcTotal = 0;
        Object.keys(state.passengerServices).forEach(k => {
            (state.passengerServices[k] || new Set()).forEach(sid => {
                const svc = state.servicesCatalog.find(x => x.id === sid);
                if (svc) svcTotal += svc.price || 0;
            });
        });
        state.totals.flights = flightsTotal;
        state.totals.services = svcTotal;
        const total = flightsTotal + svcTotal;
        summaryTotal.textContent = formatMoney(total);
        if (paymentTotal) paymentTotal.textContent = formatMoney(total);
    }

    function updateSummary() {
        const out = state.outboundFlights.find(f => f.id === state.selectedOutboundId);
        const inbound = state.inboundFlights.find(f => f.id === state.selectedInboundId);
        summaryOutbound.textContent = out ? `${out.flightNumber} (${toLocalTimeShort(out.departureUtc)})` : "—";
        summaryInbound.textContent = inbound ? `${inbound.flightNumber} (${toLocalTimeShort(inbound.departureUtc)})` : "—";
        summaryPax.textContent = state.passengers.length;
        clearChildren(summaryServiceList);
        const collected = new Set();
        Object.values(state.passengerServices).forEach(s => s && s.forEach(id => collected.add(id)));
        collected.forEach(id => {
            const svc = state.servicesCatalog.find(x => x.id === id);
            if (svc) {
                const li = document.createElement("li");
                li.textContent = `${svc.name} — ${formatMoney(svc.price)}`;
                summaryServiceList.appendChild(li);
            }
        });
        recalcTotals();
    }

    // navigation helpers
    function showStep(stepId) {
        ["stepFlights", "stepPassengers", "stepServices", "stepPayment"].forEach(s => {
            const el = document.getElementById(s);
            if (el) el.classList.add("hidden");
        });
        const target = document.getElementById(stepId);
        if (target) target.classList.remove("hidden");
        window.scrollTo({ top: 0, behavior: "smooth" });
    }

    // final booking POST; include antiforgery token from hidden form
    async function postBooking(payload) {
        const tokenEl = document.querySelector('input[name="__RequestVerificationToken"]');
        const token = tokenEl ? tokenEl.value : null;

        const headers = { "Content-Type": "application/json" };
        if (token) headers["RequestVerificationToken"] = token;

        const res = await fetch("/api/bookings", {
            method: "POST",
            headers,
            body: JSON.stringify(payload)
        });
        if (!res.ok) {
            const txt = await res.text();
            throw new Error(txt || `HTTP ${res.status}`);
        }
        return await res.json();
    }

    // event wiring
    function wireEvents() {
        flightsNextBtn.addEventListener("click", () => {
            if (!state.selectedOutboundId) return alert("Select an outbound flight");
            if (state.inboundFlights.length && !state.selectedInboundId) return alert("Select an inbound flight");
            showStep("stepPassengers");
        });

        passengersBackBtn.addEventListener("click", () => showStep("stepFlights"));

        passengersNextBtn.addEventListener("click", () => {
            for (let i = 0; i < state.passengers.length; i++) {
                const p = state.passengers[i];
                if (!p.firstName || !p.lastName) return alert(`Passenger ${i + 1}: please enter name`);
                if (!p.dob) return alert(`Passenger ${i + 1}: please enter date of birth`);
            }
            renderServicesUi();
            showStep("stepServices");
        });

        servicesBackBtn.addEventListener("click", () => showStep("stepPassengers"));
        servicesNextBtn.addEventListener("click", () => showStep("stepPayment"));
        paymentBackBtn && paymentBackBtn.addEventListener("click", () => showStep("stepServices"));

        payBtn && payBtn.addEventListener("click", async () => {
            const payload = {
                outboundId: state.selectedOutboundId,
                inboundId: state.selectedInboundId,
                passengers: state.passengers.map(p => ({
                    firstName: p.firstName,
                    lastName: p.lastName,
                    dateOfBirth: p.dob,
                    gender: p.gender,
                    travelDocument: p.hasDoc ? { number: p.docNumber, expiryDate: p.docExpiry } : null
                })),
                passengerServices: Object.fromEntries(Object.entries(state.passengerServices).map(([k, v]) => [k, Array.from(v)])),
                payment: {
                    cardHolder: document.getElementById("cardHolder") ? document.getElementById("cardHolder").value : "",
                    amountInEur: state.totals.flights + state.totals.services
                }
            };

            try {
                const result = await postBooking(payload);
                // redirect to confirmation or show success
                if (result?.bookingId) window.location.href = `/Booking/Confirmation/${result.bookingId}`;
                else alert("Booking completed");
            } catch (ex) {
                error("booking failed", ex);
                alert("Booking failed: " + (ex.message || ex));
            }
        });

        passengersCount && passengersCount.addEventListener("change", ev => {
            const n = Number(ev.target.value) || 1;
            state.passengers = new Array(n).fill(null).map(() => ({ firstName: "", lastName: "", dob: "", gender: "", hasDoc: false, docNumber: "", docExpiry: "" }));
            state.passengerServices = {};
            for (let i = 0; i < n; i++) state.passengerServices[i] = new Set();
            renderPassengersUi();
            renderServicesUi();
            updateSummary();
        });
    }

    // boot: load DOM-sourced flights/services, then init passengers default & wire events
    (function boot() {
        try {
            loadFromDom();
            wireFlightCardClicks();
            // default passengers count from server model (if present)
            const initialCount = Number(passengersCount ? passengersCount.value : 1) || 1;
            state.passengers = new Array(initialCount).fill(null).map(() => ({ firstName: "", lastName: "", dob: "", gender: "", hasDoc: false, docNumber: "", docExpiry: "" }));
            for (let i = 0; i < state.passengers.length; i++) state.passengerServices[i] = new Set();

            renderPassengersUi();
            renderServicesUi();
            updateSummary();
            wireEvents();
        } catch (ex) {
            error("init failed", ex);
        }
    })();

    // expose for debug
    window.__CreateBookingState = state;
})();
