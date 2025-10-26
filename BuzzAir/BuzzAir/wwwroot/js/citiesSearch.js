document.addEventListener("DOMContentLoaded", () => {

    const connection = new signalR.HubConnectionBuilder()
        .withUrl("/searchHub")
        .build();

    let pageIndex = 0;
    const itemsPerPage = 20;
    let currentSearch = "";
    let loading = false;

    const resultsDiv = document.getElementById("cityAirportResults");
    const searchInput = document.getElementById("cityAirport");
    const cityIdInput = document.getElementById("CityId");

    resultsDiv.style.display = "none";

    async function loadCities(reset = false) {
        if (loading) return;
        loading = true;

        if (reset) {
            pageIndex = 0;
            resultsDiv.innerHTML = "<div class='text-muted small text-center py-2'>Loading...</div>";
        }

        try {
            // Call your SignalR hub method with 3 params
            const result = await connection.invoke("GetCities", pageIndex, itemsPerPage, currentSearch);

            if (result && Object.keys(result).length > 0) {
                if (reset) resultsDiv.innerHTML = "";

                // Sort by country (keys)
                const sortedCountries = Object.keys(result).sort((a, b) => a.localeCompare(b));

                sortedCountries.forEach(country => {
                    const countryGroup = document.createElement("div");
                    countryGroup.className = "mb-2";

                    const countryHeader = document.createElement("div");
                    countryHeader.className = "fw-bold text-primary border-bottom pb-1 mb-1";
                    countryHeader.textContent = country;
                    countryGroup.appendChild(countryHeader);

                    const cities = Object.entries(result[country])
                        .sort((a, b) => a[1].localeCompare(b[1]));

                    cities.forEach(([id, name]) => {
                        const cityDiv = document.createElement("div");
                        cityDiv.className = "city-option p-1 rounded hover-bg-light";
                        cityDiv.textContent = name;
                        cityDiv.dataset.id = id;
                        cityDiv.style.cursor = "pointer";

                        cityDiv.addEventListener("click", () => {
                            cityIdInput.value = id;
                            searchInput.value = name;

                            // trigger validation update
                            cityIdInput.dispatchEvent(new Event("change", { bubbles: true }));

                            resultsDiv.style.display = "none";
                        });

                        countryGroup.appendChild(cityDiv);
                    });

                    resultsDiv.appendChild(countryGroup);
                });

                pageIndex++;

            } else if (reset) {
                resultsDiv.innerHTML = "<div class='text-muted small text-center py-2'>No results found.</div>";
            }

        } catch (err) {
            console.error(err);
        }

        loading = false;
    }

    // handle typing
    let typingTimer;
    searchInput.addEventListener("input", () => {
        clearTimeout(typingTimer);
        currentSearch = searchInput.value.trim();
        typingTimer = setTimeout(() => loadCities(true), 400);
        resultsDiv.style.display = "block";
    });

    searchInput.addEventListener("focus", () => {
        resultsDiv.style.display = "block";
    });

    searchInput.addEventListener("blur", () => {
        setTimeout(() => {
            resultsDiv.style.display = "none";
        }, 200);
    });

    resultsDiv.addEventListener("scroll", () => {
        if (resultsDiv.scrollTop + resultsDiv.clientHeight >= resultsDiv.scrollHeight - 20) {
            if (!currentSearch) loadCities();
        }
    });

    connection.start()
        .then(() => loadCities())
        .catch(console.error);
});
