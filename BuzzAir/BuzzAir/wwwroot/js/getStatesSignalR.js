const connection = new signalR.HubConnectionBuilder()
    .withUrl('/locationHub')
    .build();

async function loadStates(countryId) {
    try {
        const states = await connection.invoke('GetStatesByCountry', countryId);
        const stateSelect = document.getElementById('stateSelect');
        stateSelect.innerHTML = '<option disabled hidden selected value="">Select a state</option>';
        for (const s of states) {
            const opt = document.createElement('option');
            opt.value = s.id;
            opt.text = s.name;
            stateSelect.appendChild(opt);
        }
        stateSelect.disabled = false;
    } catch (err) {
        console.error(err.toString());
    }
}

connection.start()
    .then(() => console.log('SignalR connected'))
    .catch(err => console.error('SignalR connection error: ', err));

document.addEventListener('DOMContentLoaded', () => {
    const countrySelect = document.getElementById('countrySelect');
    if (!countrySelect) {
        return;
    }

    countrySelect.addEventListener('change', e => {
        const countryId = e.target.value;
        const stateSelect = document.getElementById('stateSelect');
        if (countryId) {
            loadStates(countryId);
        } else {
            stateSelect.innerHTML = '<option disabled hidden selected value="">Select a state</option>';
            stateSelect.disabled = true;
        }
    });
});