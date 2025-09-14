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
