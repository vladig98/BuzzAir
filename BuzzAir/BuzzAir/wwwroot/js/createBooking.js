"use strict";

/*
  createBooking.js
  - No Razor in JS. Reads everything from the DOM.
  - Keeps all existing input names and hidden fields intact.
  - Seat map is 3 + aisle + 3 columns.
  - Locked seats show padlock; seats with price show money bag badge.
  - Detailed summary on the right shows selected flights + passenger-by-passenger info.
*/

document.addEventListener("DOMContentLoaded", () => {
    const form = document.getElementById("bookingForm");
    if (!form) return;

    const passengerCount = Number(form.dataset.passengers || 0);
    const hasInboundAttr = form.dataset.hasInbound || "false";
    const hasInbound = hasInboundAttr.toLowerCase() === "true" || hasInboundAttr === "1";

    // DOM references
    const sections = Array.from(form.querySelectorAll("[data-section]"));
    const steps = Array.from(form.querySelectorAll(".bw-step"));
    const prevBtn = document.getElementById("prevBtn");
    const nextBtn = document.getElementById("nextBtn");
    const detailedSummary = document.getElementById("detailedSummary");
    const passengersSummaryList = document.getElementById("passengersSummaryList");
    const summaryOutboundShort = document.getElementById("summaryOutboundShort");
    const summaryInboundShort = document.getElementById("summaryInboundShort");
    const summaryPassengersShort = document.getElementById("summaryPassengersShort");
    const summaryTotalShort = document.getElementById("summaryTotalShort");
    const totalPriceEl = document.getElementById("totalPrice");
    const totalPriceInput = document.getElementById("totalPriceInput");
    const progressFill = document.getElementById("bwProgressFill");

    let currentIndex = 0;
    const maxIndex = sections.length - 1;

    // show section helper
    function showSection(idx) {
        sections.forEach((s, i) => s.classList.toggle("bw-hidden", i !== idx));
        steps.forEach((st, i) => st.classList.toggle("active", i <= idx));
        if (progressFill) progressFill.style.width = `${(idx / maxIndex) * 100}%`;
        prevBtn.disabled = idx === 0;
        nextBtn.textContent = idx === maxIndex ? "Confirm & Pay" : "Next";
        currentIndex = idx;
        updateSummary();
        if (idx === 2) requestSeatMapsForSelectedFlights();
    }

    // initial
    showSection(0);

    // step clicking
    steps.forEach(s => s.addEventListener("click", () => {
        const idx = Number(s.dataset.step);
        if (!isNaN(idx)) showSection(idx);
    }));

    prevBtn.addEventListener("click", () => { if (currentIndex > 0) showSection(currentIndex - 1); });
    nextBtn.addEventListener("click", () => {
        if (currentIndex < maxIndex) showSection(currentIndex + 1);
        else form.submit();
    });

    // prettier gender segmented control (labels act as toggles)
    document.querySelectorAll(".gender-toggle").forEach(gt => {
        const labels = gt.querySelectorAll(".gender-btn");
        labels.forEach(lbl => {
            lbl.addEventListener("click", () => {
                // find corresponding hidden input and check it
                const forId = lbl.getAttribute("for");
                const inp = document.getElementById(forId);
                if (inp) {
                    inp.checked = true;
                    // style - make sibling labels reflect state
                    labels.forEach(l => l.classList.toggle("active", l === lbl));
                }
            });
        });
        // init active state if radio pre-checked
        gt.querySelectorAll("input[type='radio']").forEach(inp => {
            if (inp.checked) {
                const lbl = gt.querySelector(`label[for="${inp.id}"]`);
                if (lbl) lbl.classList.add("active");
            }
        });
    });

    // check-in toggle -> show travel doc block for passenger
    form.querySelectorAll(".check-in-toggle").forEach(t => {
        t.addEventListener("change", (e) => {
            const container = e.target.closest("[data-passenger-index]");
            if (!container) return;
            const travel = container.querySelector(".travel-doc");
            if (travel) travel.classList.toggle("bw-hidden", !e.target.checked);
            updateSummary();
        });
    });

    // highlight selected cards (flight/service/baggage)
    form.addEventListener("change", (e) => {
        if (!(e.target instanceof HTMLInputElement)) return;
        const el = e.target;
        if (el.type === "radio") {
            const name = el.name;
            form.querySelectorAll(`input[type="radio"][name="${CSS.escape(name)}"]`).forEach(r => {
                const card = r.closest(".bw-flight-card") || r.closest(".baggage-item") || r.closest(".seat-type-item");
                if (card) card.classList.toggle("selected", r.checked);
            });
        }
        // update totals and summary on any change
        updateTotalPrice();
        updateSummary();
    });

    // initial highlight
    form.querySelectorAll("input[type='radio']").forEach(r => {
        if (r.checked) {
            const card = r.closest(".bw-flight-card") || r.closest(".baggage-item") || r.closest(".seat-type-item");
            if (card) card.classList.add("selected");
        }
    });

    // ------------------- Seat maps -------------------
    // Desired layout: 3 + aisle + 3 => grid-template-columns: repeat(3, size) 16px repeat(3, size)
    function seatGridColumnsCss(sizePx = 48) {
        return `repeat(3, ${sizePx}px) 18px repeat(3, ${sizePx}px)`;
    }

    // SignalR optional connection
    let connection = null;
    if (window.signalR) {
        try {
            connection = new signalR.HubConnectionBuilder().withUrl("/seatMapHub").build();
            connection.start().then(() => console.log("SignalR connected")).catch(err => console.warn("SignalR start:", err));
            connection.on("ReceiveSeatMap", (seatMap, direction, flightId) => {
                document.querySelectorAll(`.seat-map[data-direction='${direction}']`).forEach(container => {
                    renderSeatMap(container, seatMap);
                    // apply locks depending on seat-type
                    const pi = container.dataset.passengerIndex;
                    const seatTypeRadio = form.querySelector(`input[name='Passengers[${pi}].Seats']:checked`);
                    if (seatTypeRadio) toggleSeatLocks(container, seatTypeRadio.dataset.seatType);
                });
            });
        } catch (err) {
            console.warn("SignalR init error", err);
            connection = null;
        }
    }

    // Request seat maps for selected flights
    function requestSeatMapsForSelectedFlights() {
        const out = form.querySelector("input[name='OutboundId']:checked");
        const inb = form.querySelector("input[name='InboundId']:checked");
        if (connection) {
            if (out) connection.invoke("SendSeatMap", out.value, "outbound").catch(console.error);
            if (inb) connection.invoke("SendSeatMap", inb.value, "inbound").catch(console.error);
        } else {
            // fallback: generate a deterministic demo map by flight id so UI works offline
            if (out) {
                document.querySelectorAll(`.seat-map[data-direction='outbound']`).forEach(container => {
                    renderSeatMap(container, generateSeatMapDemo(out.value));
                });
            }
            if (inb) {
                document.querySelectorAll(`.seat-map[data-direction='inbound']`).forEach(container => {
                    renderSeatMap(container, generateSeatMapDemo(inb.value));
                });
            }
        }
        updateSummary();
    }

    // deterministic demo generator: consistent per flightId
    function generateSeatMapDemo(flightId) {
        let hash = 0;
        for (let i = 0; i < flightId.length; i++) hash = (hash << 5) - hash + flightId.charCodeAt(i);
        hash = Math.abs(hash);
        const rows = 12; // show 12 rows
        const seats = [];
        for (let r = 1; r <= rows; r++) {
            // left block A,B,C
            for (let c = 0; c < 3; c++) {
                const seatNumber = `${r}${String.fromCharCode(65 + c)}`;
                const seed = (hash + r * 17 + c * 23) % 100;
                const taken = seed < 18; // ~18% taken
                const type = (c === 1 && r % 4 === 0) ? "ExtraLegRoom" : "Normal"; // occasional extra leg
                const price = (type === "ExtraLegRoom") ? 20.0 : 0;
                seats.push({ number: seatNumber, type, taken, price });
            }
            // aisle marker
            seats.push({ aisleGapBefore: true });
            // right block D,E,F
            for (let c = 3; c < 6; c++) {
                const seatNumber = `${r}${String.fromCharCode(65 + c)}`;
                const seed = (hash + r * 13 + c * 19) % 100;
                const taken = seed < 16;
                const type = (c === 4 && r % 5 === 0) ? "ExtraLegRoom" : "Normal";
                const price = (type === "ExtraLegRoom") ? 20.0 : 0;
                seats.push({ number: seatNumber, type, taken, price });
            }
        }
        return seats;
    }

    // Render seat map using 3+aisle+3 columns
    function renderSeatMap(container, seats) {
        container.innerHTML = "";
        // set grid template
        container.style.display = "grid";
        container.style.gridTemplateColumns = seatGridColumnsCss(48);
        container.style.gap = "8px";

        seats.forEach(item => {
            if (item.aisleGapBefore) {
                const gap = document.createElement("div");
                gap.className = "seat empty";
                gap.style.background = "transparent";
                // ensure gap occupies the aisle column: create an empty placeholder sized like the 18px defined
                gap.style.width = "18px";
                gap.style.pointerEvents = "none";
                container.appendChild(gap);
                return;
            }

            const s = document.createElement("div");
            s.className = "seat";
            s.textContent = item.number;
            s.dataset.seatNumber = item.number;
            s.dataset.seatType = item.type || "Normal";
            if (item.price !== undefined) s.dataset.price = item.price;

            if (item.taken) {
                s.classList.add("taken");
            } else {
                s.classList.add("available");
                s.addEventListener("click", () => {
                    if (s.classList.contains("locked") || s.classList.contains("taken")) return;
                    selectSeat(container, s);
                });
            }

            // if priced show money bag badge
            if (item.price && Number(item.price) > 0) {
                const badge = document.createElement("span");
                badge.className = "seat-price-badge";
                badge.textContent = "💰";
                s.appendChild(badge);
            }

            container.appendChild(s);
        });

        // set visible/hidden based on seat-type radio for that passenger
        const pi = container.dataset.passengerIndex;
        const seatTypeRadio = form.querySelector(`input[name='Passengers[${pi}].Seats']:checked`);
        if (seatTypeRadio && seatTypeRadio.dataset.seatType === "None") {
            container.style.display = "none";
        } else {
            container.style.display = "grid";
            // apply locks
            if (seatTypeRadio) toggleSeatLocks(container, seatTypeRadio.dataset.seatType);
        }
    }

    function selectSeat(container, seatEl) {
        container.querySelectorAll(".seat.selected").forEach(s => s.classList.remove("selected"));
        seatEl.classList.add("selected");
        const pi = container.dataset.passengerIndex;
        const dir = container.dataset.direction;
        const hid = document.getElementById(`seatSelection${capitalize(dir)}-${pi}`);
        if (hid) hid.value = seatEl.dataset.seatNumber || "";
        updateTotalPrice();
        updateSummary();
    }

    function capitalize(s) { return s.charAt(0).toUpperCase() + s.slice(1); }

    // lock mismatched seats based on selected seat-type
    function toggleSeatLocks(container, selectedType) {
        container.querySelectorAll(".seat.available").forEach(seat => {
            const t = seat.dataset.seatType || "Normal";
            if ((selectedType === "Normal" && t === "ExtraLegRoom") || (selectedType === "ExtraLegRoom" && t === "Normal")) {
                seat.classList.add("locked");
                seat.style.pointerEvents = "none";
                // show padlock overlay (CSS will use ::after)
            } else {
                seat.classList.remove("locked");
                seat.style.pointerEvents = "auto";
            }
        });
    }

    // seat-type radio behavior
    form.querySelectorAll(".seat-type").forEach(radio => {
        radio.addEventListener("change", (e) => {
            const matches = radio.name.match(/\d+/);
            if (!matches) return;
            const pi = matches[0];
            ["Outbound", "Inbound"].forEach(dir => {
                const container = document.getElementById(`seatMap${dir}-${pi}`);
                if (!container) return;
                if (radio.dataset.seatType === "None") {
                    container.style.display = "none";
                    const hid = document.getElementById(`seatSelection${dir}-${pi}`);
                    if (hid) hid.value = "";
                    container.querySelectorAll(".seat.selected").forEach(s => s.classList.remove("selected"));
                } else {
                    container.style.display = "grid";
                    toggleSeatLocks(container, radio.dataset.seatType);
                }
            });
            updateTotalPrice();
            updateSummary();
        });

        // initial visibility
        if (radio.checked) {
            const matches = radio.name.match(/\d+/);
            if (matches) {
                const pi = matches[0];
                const cOut = document.getElementById(`seatMapOutbound-${pi}`);
                const cIn = document.getElementById(`seatMapInbound-${pi}`);
                const show = radio.dataset.seatType !== "None";
                if (cOut) cOut.style.display = show ? "grid" : "none";
                if (cIn) cIn.style.display = show ? "grid" : "none";
            }
        }
    });

    // when flights selection changes request seat maps
    form.querySelectorAll("input[name='OutboundId'], input[name='InboundId']").forEach(r => {
        r.addEventListener("change", () => {
            updateTotalPrice();
            requestSeatMapsForSelectedFlights();
        });
    });

    // initial seat maps if preselected flights exist
    requestSeatMapsForSelectedFlights();

    // ------------------- Pricing (sums data-price plus seat element prices) -------------------
    function updateTotalPrice() {
        let total = 0;
        const out = form.querySelector("input[name='OutboundId']:checked");
        if (out && out.dataset.price) total += Number(out.dataset.price || 0);
        const inb = form.querySelector("input[name='InboundId']:checked");
        if (inb && inb.dataset.price) total += Number(inb.dataset.price || 0);

        // all checked inputs carrying data-price
        form.querySelectorAll("input[data-price]").forEach(inp => {
            if ((inp.type === "checkbox" && inp.checked) || (inp.type === "radio" && inp.checked)) {
                total += Number(inp.dataset.price || 0);
            }
        });

        // add selected seat DOM price badges
        form.querySelectorAll(".seat.selected").forEach(s => {
            if (s.dataset.price) total += Number(s.dataset.price || 0);
        });

        if (!Number.isFinite(total)) total = 0;
        const text = `€${total.toFixed(2)}`;
        if (totalPriceEl) totalPriceEl.textContent = text;
        if (totalPriceInput) totalPriceInput.value = total.toFixed(2);
        if (summaryTotalShort) summaryTotalShort.textContent = text;
    }

    // hook price-carrying inputs
    form.querySelectorAll("input[data-price]").forEach(inp => inp.addEventListener("change", updateTotalPrice));
    updateTotalPrice();

    // ------------------- Summary building -------------------
    function updateSummary() {
        // top short summary
        const out = form.querySelector("input[name='OutboundId']:checked");
        const inb = form.querySelector("input[name='InboundId']:checked");
        summaryOutboundShort.textContent = out ? (out.closest("label")?.querySelector(".bw-route")?.textContent?.trim() || out.value) : "—";
        summaryInboundShort.textContent = inb ? (inb.closest("label")?.querySelector(".bw-route")?.textContent?.trim() || inb.value) : (hasInbound ? "—" : "N/A");
        summaryPassengersShort.textContent = String(passengerCount || 0);
        summaryTotalShort.textContent = totalPriceEl ? totalPriceEl.textContent : "€0.00";

        // detailed per-passenger summary
        passengersSummaryList.innerHTML = "";
        for (let i = 0; i < passengerCount; i++) {
            const pName = (form.querySelector(`input[name="Passengers[${i}].FirstName"]`)?.value || "—") +
                " " +
                (form.querySelector(`input[name="Passengers[${i}].LastName"]`)?.value || "");
            const checkedIn = !!form.querySelector(`#checkInNow${i}`)?.checked;
            const seatOut = form.querySelector(`#seatSelectionOutbound-${i}`)?.value || "—";
            const seatIn = form.querySelector(`#seatSelectionInbound-${i}`)?.value || (hasInbound ? "—" : "N/A");
            // services
            const servicesEls = Array.from(form.querySelectorAll(`input[name="Passengers[${i}].ServiceIds"]:checked`));
            const services = servicesEls.map(s => s.closest("label")?.querySelector(".service-name")?.textContent?.trim() || s.value);
            // baggage
            const bag = form.querySelector(`input[name="Passengers[${i}].Baggage"]:checked`);
            const baggageText = bag ? (bag.closest("label")?.querySelector(".baggage-text")?.textContent?.trim() || bag.value) : "—";
            // travel doc present?
            const docNumber = form.querySelector(`input[name="Passengers[${i}].TravelDocument.Number"]`)?.value;
            const travelDocPresent = docNumber ? "Yes" : (checkedIn ? "No details" : "No");

            // seat type selected
            const seatTypeRadio = form.querySelector(`input[name="Passengers[${i}].Seats"]:checked`);
            const seatType = seatTypeRadio ? seatTypeRadio.dataset.seatType : "None";

            const pCard = document.createElement("div");
            pCard.className = "pass-summary-card";
            pCard.innerHTML = `
                <div class="pass-summary-head"><strong>${escapeHtml(pName.trim())}</strong> <span class="small muted">#${i + 1}</span></div>
                <div class="pass-summary-row"><strong>Seat (Out):</strong> ${escapeHtml(seatOut)}</div>
                <div class="pass-summary-row"><strong>Seat (In):</strong> ${escapeHtml(seatIn)}</div>
                <div class="pass-summary-row"><strong>Seat Type:</strong> ${escapeHtml(seatType)}</div>
                <div class="pass-summary-row"><strong>Services:</strong> ${services.length ? escapeHtml(services.join(", ")) : "—"}</div>
                <div class="pass-summary-row"><strong>Baggage:</strong> ${escapeHtml(baggageText)}</div>
                <div class="pass-summary-row"><strong>Checked-in:</strong> ${checkedIn ? "Yes" : "No"}</div>
                <div class="pass-summary-row"><strong>Travel doc:</strong> ${escapeHtml(travelDocPresent)}</div>
            `;
            passengersSummaryList.appendChild(pCard);
        }
    }

    // helper to escape HTML when injecting text
    function escapeHtml(text) {
        if (!text && text !== 0) return "";
        return String(text)
            .replace(/&/g, "&amp;")
            .replace(/"/g, "&quot;")
            .replace(/'/g, "&#039;")
            .replace(/</g, "&lt;")
            .replace(/>/g, "&gt;");
    }

    // make summary update on input changes
    form.addEventListener("input", (e) => {
        // update only when relevant fields change
        if (!e.target) return;
        if (e.target.matches("input, select, textarea")) {
            updateSummary();
            updateTotalPrice();
        }
    });

    // initial summary build
    updateSummary();

    // expose helpful functions for debugging
    window.__bookingHelper = {
        requestSeatMapsForSelectedFlights,
        renderSeatMap,
        generateSeatMapDemo,
        updateTotalPrice,
        updateSummary
    };
});
