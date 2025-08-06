const connection = new signalR.HubConnectionBuilder()
    .withUrl('/locationHub')
    .build();

async function loadStates(countryId) {
    const states = await connection.invoke('GetStatesByCountry', countryId);
    const s = document.getElementById('stateSelect');
    const sWrapper = document.getElementById('stateWrapper');
    const c = document.getElementById('citySelect');
    const cWrapper = document.getElementById('cityWrapper');

    if (states.length > 0) {
        // populate & show states
        s.innerHTML = '<option disabled hidden selected value="">Select a state</option>';
        states.forEach(x => s.append(new Option(x.name, x.id)));
        s.disabled = false;
        sWrapper.style.display = '';      // un-hide
        // clear cities until state chosen
        c.innerHTML = '<option disabled hidden selected value="">Select a city</option>';
        c.disabled = true;
        cWrapper.style.display = 'none';
    }
    else {
        // hide state, load all cities for country
        sWrapper.style.display = 'none';
        await loadCities(null, countryId);
    }
}

async function loadCities(stateId, countryId) {
    const cities = await connection.invoke('GetCitiesByStateAndCountry', stateId, countryId);
    const c = document.getElementById('citySelect');
    const cWrapper = document.getElementById('cityWrapper');
    c.innerHTML = '<option disabled hidden selected value="">Select a city</option>';
    cities.forEach(x => c.append(new Option(x.name, x.id)));
    c.disabled = false;
    cWrapper.style.display = '';
}

connection.start().catch(console.error);

document.addEventListener('DOMContentLoaded', () => {
    const country = document.getElementById('countrySelect');
    const state = document.getElementById('stateSelect');

    country.addEventListener('change', e => {
        const cid = e.target.value;
        if (cid) loadStates(cid);
        else {
            document.getElementById('stateWrapper').style.display = 'none';
            document.getElementById('cityWrapper').style.display = 'none';
        }
    });

    state.addEventListener('change', e => {
        const sid = e.target.value;
        const cid = country.value;
        if (cid) loadCities(sid || null, cid);
    });
});
