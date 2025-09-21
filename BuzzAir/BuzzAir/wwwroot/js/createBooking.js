"use strict";

document.addEventListener("DOMContentLoaded", () => {
    console.log("[CreateBooking] Booting page...");

    // Grab all sections
    const sections = Array.from(document.querySelectorAll("[data-section]"));
    let currentIndex = 0;

    function showSection(index) {
        sections.forEach((sec, i) => {
            sec.classList.toggle("d-none", i !== index);
        });
    }

    // Show first section by default
    showSection(currentIndex);

    // Wire up navigation buttons
    const nextBtn = document.getElementById("nextBtn");
    const prevBtn = document.getElementById("prevBtn");

    function updateNavButtons() {
        prevBtn.disabled = currentIndex === 0;
        nextBtn.textContent = currentIndex === sections.length - 1 ? "Confirm & Pay" : "Next";
    }

    nextBtn.addEventListener("click", () => {
        if (currentIndex < sections.length - 1) {
            currentIndex++;
            showSection(currentIndex);
            updateNavButtons();
        } else {
            console.log("[CreateBooking] Submitting booking...");
            document.getElementById("bookingForm").submit();
        }
    });

    prevBtn.addEventListener("click", () => {
        if (currentIndex > 0) {
            currentIndex--;
            showSection(currentIndex);
            updateNavButtons();
        }
    });

    updateNavButtons();

    // Wire up Check-in Now toggles
    const checkInToggles = document.querySelectorAll("[id^='checkInNow']");
    checkInToggles.forEach(toggle => {
        toggle.addEventListener("change", (e) => {
            const index = toggle.id.replace("checkInNow", "");
            const hiddenSection = toggle.closest(".mb-3, .form-check").nextElementSibling;
            if (hiddenSection) {
                hiddenSection.classList.toggle("d-none", !e.target.checked);
            }
        });
    });

    let connection = new signalR.HubConnectionBuilder()
        .withUrl("/seatMapHub")
        .build();

    connection.start().then(() => console.log("SignalR connected."));

    document.querySelectorAll("input[name='OutboundId']").forEach(radio => {
        radio.addEventListener("change", (e) => {
            let flightId = e.target.value;
            connection.invoke("SendSeatMap", flightId, "outbound");
        });
    });

    document.querySelectorAll("input[name='InboundId']").forEach(radio => {
        radio.addEventListener("change", (e) => {
            let flightId = e.target.value;
            connection.invoke("SendSeatMap", flightId, "inbound");
        });
    });

    connection.on("ReceiveSeatMap", (seatMap, direction) => {
        document.querySelectorAll(`.seat-map[data-direction='${direction}']`).forEach(container => {
            renderSeatMap(container, seatMap);
            disableExtraLegRoomIfNeeded(container, seatMap);
        });
    });

    function disableExtraLegRoomIfNeeded(container, seatMap) {
        let extraAvailable = seatMap.some(s => s.type === "ExtraLegRoom" && !s.taken);
        if (!extraAvailable) {
            let passengerIndex = container.dataset.passengerIndex;
            let extraRadio = document.querySelector(
                `input[name='Passengers[${passengerIndex}].Seats'][data-seat-type='ExtraLegRoom']`
            );
            if (extraRadio) extraRadio.disabled = true;
        }
    }

    function renderSeatMap(container, seats) {
        container.innerHTML = "";
        seats.forEach((seat, idx) => {
            let seatDiv = document.createElement("div");
            seatDiv.classList.add("seat");
            seatDiv.dataset.seatType = seat.type;
            seatDiv.dataset.seatNumber = seat.number;
            seatDiv.textContent = seat.number;

            if (seat.taken) {
                seatDiv.classList.add("taken");
            } else {
                seatDiv.classList.add("available");
                seatDiv.addEventListener("click", () => selectSeat(container, seatDiv));
            }

            if ((idx + 1) % 6 === 4) {
                let gap = document.createElement("div");
                gap.classList.add("seat", "empty");
                container.appendChild(gap);
            }

            container.appendChild(seatDiv);
        });

        container.style.display = "none";
    }

    function selectSeat(container, seatDiv) {
        container.querySelectorAll(".seat.selected").forEach(s => s.classList.remove("selected"));
        seatDiv.classList.add("selected");

        let passengerIndex = container.dataset.passengerIndex;
        let direction = container.dataset.direction;
        document.querySelector(`#seatSelection${capitalize(direction)}-${passengerIndex}`).value =
            seatDiv.dataset.seatNumber;
    }

    function toggleSeatLocks(container, selectedType) {
        container.querySelectorAll(".seat.available").forEach(seat => {
            if (selectedType === "Normal" && seat.dataset.seatType === "ExtraLegRoom") {
                seat.classList.add("locked");
                seat.style.pointerEvents = "none";
            } else if (selectedType === "ExtraLegRoom" && seat.dataset.seatType === "Normal") {
                seat.classList.add("locked");
                seat.style.pointerEvents = "none";
            } else {
                seat.classList.remove("locked");
                seat.style.pointerEvents = "auto";
            }
        });
    }

    function capitalize(str) {
        return str.charAt(0).toUpperCase() + str.slice(1);
    }

    document.querySelectorAll(".seat-type").forEach(radio => {
        radio.addEventListener("change", (e) => {
            let seatType = e.target.dataset.seatType;
            let passengerIndex = e.target.name.match(/\d+/)[0];

            ["Outbound", "Inbound"].forEach(direction => {
                let container = document.querySelector(`#seatMap${direction}-${passengerIndex}`);
                if (!container) return;

                if (seatType === "None") {
                    // Hide map and clear selected seat value
                    container.style.display = "none";
                } else {
                    container.style.display = "grid"; // show again
                    toggleSeatLocks(container, seatType);
                }

                let hiddenInput = document.querySelector(
                    `#seatSelection${capitalize(direction)}-${passengerIndex}`
                );

                if (hiddenInput) hiddenInput.value = ""; // clear selected seat
                container.querySelectorAll(".seat.selected").forEach(s => s.classList.remove("selected"));
            });
        });
    });

});

// Add this at the end of your existing createBooking.js (after DOMContentLoaded init or inside it)
(function () {
    // apply "selected" styling to the form-check container for each radio group
    function wireSelectionHighlights() {
        document.querySelectorAll('input[type="radio"]').forEach(radio => {
            radio.addEventListener('change', (ev) => {
                const name = ev.target.name;
                // remove .selected from all siblings in same radio group
                document.querySelectorAll(`input[type="radio"][name="${name}"]`).forEach(r => {
                    const container = r.closest('.form-check');
                    if (container) container.classList.remove('selected');
                });
                const selectedContainer = ev.target.closest('.form-check');
                if (selectedContainer) selectedContainer.classList.add('selected');
            });
            // check initially if it is checked and set class
            if (radio.checked) {
                const container = radio.closest('.form-check');
                if (container) container.classList.add('selected');
            }
        });
    }

    if (document.readyState === 'loading') {
        document.addEventListener('DOMContentLoaded', wireSelectionHighlights);
    } else {
        wireSelectionHighlights();
    }
})();


const totalPriceEl = document.getElementById("totalPrice");
const totalPriceInput = document.getElementById("totalPriceInput");

function updateTotalPrice() {
    let total = 0;

    // Outbound Flight
    const selectedOutbound = document.querySelector("input[name='OutboundId']:checked");
    if (selectedOutbound) total += parseFloat(selectedOutbound.dataset.price);

    // Inbound Flight
    const selectedInbound = document.querySelector("input[name='InboundId']:checked");
    if (selectedInbound) total += parseFloat(selectedInbound.dataset.price);

    // Services
    document.querySelectorAll("input[name^='Passengers'][name$='ServiceIds']:checked")
        .forEach(s => total += parseFloat(s.dataset.price));

    // Baggage
    document.querySelectorAll("input[name^='Passengers'][name$='Baggage']:checked")
        .forEach(b => total += parseFloat(b.dataset.price));

    // Seats
    document.querySelectorAll("input[name^='Passengers'][name$='Seats']:checked")
        .forEach(s => total += parseFloat(s.dataset.price));

    totalPriceEl.textContent = `€${total.toFixed(2)}`;
    totalPriceInput.value = total.toFixed(2);
}

// Attach listeners
document.querySelectorAll("input[type='radio'], input[type='checkbox']")
    .forEach(el => el.addEventListener("change", updateTotalPrice));

updateTotalPrice(); // Run once on page load
