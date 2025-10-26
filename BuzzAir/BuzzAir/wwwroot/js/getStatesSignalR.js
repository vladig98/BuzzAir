const connection = new signalR.HubConnectionBuilder()
    .withUrl('/locationHub')
    .build();

/**
 * Helper function to force jQuery Unobtrusive Validation to re-parse the form.
 * @param {HTMLElement} elementInForm Any element inside the form to be re-parsed.
 */
function reparseFormValidation(elementInForm) {
    // Check if jQuery and the validator are loaded
    if (typeof $ === 'undefined' || !$.validator || !$.validator.unobtrusive) {
        return;
    }

    const form = $(elementInForm).closest('form');
    if (form.length > 0 && form.data('validator')) {
        // Remove the existing validation data
        form.removeData('validator');
        form.removeData('unobtrusiveValidation');
        // Re-parse the form
        $.validator.unobtrusive.parse(form);
    }
}

async function loadStates(countryId) {
    const states = await connection.invoke('GetStatesByCountry', countryId);

    const s = document.getElementById('stateSelect');
    const sWrapper = document.getElementById('stateWrapper');

    if (!s || !sWrapper) return;

    // always reset selection
    s.value = '';
    s.disabled = states.length === 0;

    // remove previous state options but keep the placeholder
    Array.from(s.options)
        .filter((_, i) => i > 0) // skip first (placeholder)
        .forEach(o => o.remove());

    // populate states if any
    if (states.length > 0) {
        for (var state of states) {
            var opt = document.createElement('option');
            opt.value = state.id;
            opt.innerHTML = state.name;
            s.appendChild(opt);
        }

        sWrapper.style.display = ''; // show
    } else {
        sWrapper.style.display = ''; // keep visible, but disabled
    }

    // NOTE: Removed re-parsing from here. It will be handled in the 'change' event.
}

connection.start().catch(console.error);

document.addEventListener('DOMContentLoaded', () => {
    const country = document.getElementById('countrySelect');
    const state = document.getElementById('stateSelect');

    if (country && state) {
        // --- FIX: Make the event listener async ---
        country.addEventListener('change', async e => { // <-- Add async
            try {
                const cid = e.target.value;
                if (cid) {
                    // --- FIX: Await the async function ---
                    await loadStates(cid); // <-- Add await
                } else {
                    // reset state without removing placeholder
                    state.value = '';
                    state.disabled = true;
                    // Clear any existing state options
                    Array.from(state.options)
                        .filter((_, i) => i > 0)
                        .forEach(o => o.remove());
                }
            } catch (err) {
                console.error(err);
                // Ensure state is disabled on error
                state.value = '';
                state.disabled = true;
            } finally {
                // --- FIX: Re-parse validation *after* all DOM changes are complete ---
                reparseFormValidation(state);
            }
        });
    }
});