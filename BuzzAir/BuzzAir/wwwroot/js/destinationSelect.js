document.addEventListener("DOMContentLoaded", () => {

    const connection = new signalR.HubConnectionBuilder()
        .withUrl("/searchHub")
        .build();

    let pageIndex = 0;
    const itemsPerPage = 20;
    let currentSearch = "";
    let loading = false;

    const resultsDiv = document.getElementById("destinationAirportResults");
    const searchInput = document.getElementById("destinationAirport");
    const destinationId = document.getElementById("destinationId");
    const originIdInput = document.getElementById("originId");

    resultsDiv.style.display = "none";

    async function loadAirports(reset = false) {
        if (!originIdInput.value) {
            resultsDiv.innerHTML = "<div class='text-muted small text-center py-2'>Please select an origin first.</div>";
            resultsDiv.style.display = "block";
            return;
        }

        if (loading) return;
        loading = true;

        if (reset) {
            pageIndex = 0;
            resultsDiv.innerHTML = "<div class='text-muted small text-center py-2'>Loading...</div>";
        }

        try {
            const result = await connection.invoke(
                "GetDestinations",
                originIdInput.value,
                pageIndex,
                itemsPerPage,
                currentSearch
            );

            if (result && Object.keys(result).length > 0) {
                if (reset) resultsDiv.innerHTML = "";

                const sortedCountries = Object.keys(result).sort((a, b) => a.localeCompare(b));

                sortedCountries.forEach(country => {
                    const countryGroup = document.createElement("div");
                    countryGroup.className = "mb-2";

                    const countryHeader = document.createElement("div");
                    countryHeader.className = "fw-bold text-primary border-bottom pb-1 mb-1";
                    countryHeader.textContent = country;
                    countryGroup.appendChild(countryHeader);

                    const airports = Object.entries(result[country])
                        .sort((a, b) => a[1].localeCompare(b[1]));

                    airports.forEach(([id, name]) => {
                        const airportDiv = document.createElement("div");
                        airportDiv.className = "airport-option p-1 rounded hover-bg-light";
                        airportDiv.textContent = name;
                        airportDiv.dataset.id = id;
                        airportDiv.style.cursor = "pointer";

                        airportDiv.addEventListener("click", () => {
                            destinationId.value = id;
                            searchInput.value = name;

                            if (window.departurePicker) window.departurePicker.clear();
                            if (window.returnPicker) window.returnPicker.clear();

                            destinationId.dispatchEvent(new Event("change", { bubbles: true }));
                        });

                        countryGroup.appendChild(airportDiv);
                    });

                    resultsDiv.appendChild(countryGroup);
                });

                pageIndex++;

            } else if (reset) {
                resultsDiv.innerHTML = "<div class='text-muted small text-center py-2'>No results found.</div>";
            }

        } catch (err) {
        }

        loading = false;
    }

    let typingTimer;
    searchInput.addEventListener("input", () => {
        clearTimeout(typingTimer);
        currentSearch = searchInput.value.trim();
        typingTimer = setTimeout(() => loadAirports(true), 400);
        resultsDiv.style.display = "block";
    });

    searchInput.addEventListener("focus", () => {
        resultsDiv.style.display = "block";
        loadAirports(true);
    });

    searchInput.addEventListener("blur", () => {
        setTimeout(() => {
            resultsDiv.style.display = "none";
        }, 200);
    });

    resultsDiv.addEventListener("scroll", () => {
        if (resultsDiv.scrollTop + resultsDiv.clientHeight >= resultsDiv.scrollHeight - 20) {
            if (!currentSearch) loadAirports();
        }
    });

    connection.start()
        .then(() => {
            if (originIdInput.value) loadAirports();
        })
        .catch();
});
