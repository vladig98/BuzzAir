function initDatePickers(selector) {
    const elem = document.querySelector(selector);
    if (!elem) {
        return;
    }

    elem.value = '';
    new AirDatepicker(selector, {
        minDate: new Date(),
        maxDate: new Date(new Date().setFullYear(new Date().getFullYear() + 1)),
        buttons: [],
        locale: exports.default
    });
}

function HomePage() {
    // Initialize both date pickers
    ['#departureDate', '#returnDate'].forEach(initDatePickers);

    const returnGroup = document.getElementById('returnGroup');
    const form = document.forms['searchFlightsForm'];
    const radios = form.elements['isReturning'];
    const invokeGetReturn = () =>
        connection.invoke('GetReturnFlightDates', originId, destinationId)
            .catch(err => console.error(err));

    // Listen for changes on each radio button
    Array.from(radios).forEach(radio => {
        radio.addEventListener('change', ({ target }) => {
            if (target.value === 'OneWay') {
                $(returnGroup).hide();
            } else {
                $(returnGroup).show();
                invokeGetReturn();
            }
        });
    });
}

function PrintBoardingPass() {
    const printableArea = document.getElementById('printableArea');
    if (!printableArea) {
        return console.error('No printable area found.');
    }

    // Open a new window for printing
    const printWindow = window.open('', 'PRINT', 'height=600,width=800');
    if (!printWindow) {
        return console.error('Unable to open print window.');
    }

    // Build the printable document
    const title = document.title;
    const content = `
    <!DOCTYPE html>
    <html lang="en">
    <head>
      <meta charset="UTF-8">
      <title>${title}</title>
      <style>
        @media print {
          body { font-family: sans-serif; margin: 20px; }
          h1 { text-align: center; }
        }
      </style>
    </head>
    <body>
      <h1>${title}</h1>
      ${printableArea.innerHTML}
    </body>
    </html>
  `;

    // Write and print
    printWindow.document.open();
    printWindow.document.write(content);
    printWindow.document.close();
    printWindow.focus();
    printWindow.print();
    printWindow.close();

    // Toggle UI elements
    document.getElementById('printed')?.classList.remove('d-none');
    document.getElementById('printBoardingPassBtn')?.classList.add('d-none');
}

function CreateBooking() {
    // Element selectors
    const expiryInput = document.getElementById("Payment_ExpiryDate");
    const divButtons = [...document.getElementsByClassName("divButton")];
    const radioDivs = [...document.getElementsByClassName("radioDiv")];
    const flightsBtn = document.getElementById("flightsBtn");
    const passengersBtn = document.getElementById("passengersBtn");
    const servicesBtn = document.getElementById("servicesBtn");
    const currencySelect = document.getElementById("Payment_Currency");

    const section = id => document.getElementById(id);
    const formElements = name => [...document.getElementsByName(name)];
    const navSection = (hideId, showId) => {
        section(hideId)?.classList.add("d-none");
        section(showId)?.classList.remove("d-none");
    };

    // 1) Auto-insert slash in expiry date after two digits
    expiryInput.addEventListener("input", () => {
        let v = expiryInput.value.replace(/\D/g, "");
        if (v.length > 2) {
            v = v.slice(0, 2) + "/" + v.slice(2, 4);
        }
        expiryInput.value = v;
    });

    // 2) Make clicking on custom div toggles its checkbox
    divButtons.forEach(div => {
        div.addEventListener("click", () => {
            const checkbox = div.querySelector("input[type='radio'], input[type='checkbox']");
            if (checkbox) {
                checkbox.checked = true;
            }
        });
    });

    // 3) RadioDivs: check input + outline selected, clear others
    radioDivs.forEach(div => {
        div.addEventListener("click", () => {
            const inp = div.querySelector("input[type='radio']");
            if (!inp) {
                return;
            }

            inp.checked = true;
            // outline this
            div.style.outline = "solid 2px red";
            // clear outlines on siblings sharing same name
            formElements(inp.name).forEach(el => {
                const parent = el.closest(".radioDiv");
                if (parent && parent !== div) {
                    parent.style.outline = "none";
                }
            });
        });
    });

    // 4) Navigate from flights → passengers
    flightsBtn.addEventListener("click", e => {
        e.preventDefault();
        const going = formElements("GoingFlightSelection").some(i => i.checked);
        const returning = formElements("ReturnFlightSelection").length
            ? formElements("ReturnFlightSelection").some(i => i.checked)
            : true;

        if (going && returning) {
            navSection("flightsSection", "passengersSection");
        }
    });

    // 5) Validate passenger names, genders, baggage before showing services
    passengersBtn.addEventListener("click", e => {
        e.preventDefault();
        const nameInputs = [...document.getElementsByClassName("paxName")];
        if (nameInputs.some(inp => !inp.value.trim())) {
            return;
        }

        const validateGroup = prefix => {
            const names = new Set([...document.getElementsByClassName(prefix)]
                .map(el => el.getAttribute("name")));
            return [...names].every(n =>
                formElements(n).some(i => i.checked)
            );
        };

        if (!validateGroup("paxGender") || !validateGroup("paxBaggage")) {
            return;
        }

        navSection("passengersSection", "servicesSection");
    });

    // 6) Calculate total and navigate services → payment
    servicesBtn.addEventListener("click", e => {
        e.preventDefault();
        navSection("servicesSection", "paymentSection");

        let total = 0;
        const paxCount = Number(document.getElementById("PassengersCount").value);

        const getCheckedValue = name =>
            Number(
                document.querySelector(`input[name='${name}']:checked`)
                    ?.closest("label")
                    .textContent.trim()
                    .slice(1) || 0
            );

        // add flight prices
        total += getCheckedValue("GoingFlightSelection");
        if (formElements("ReturnFlightSelection").length) {
            total += getCheckedValue("ReturnFlightSelection");
        }
        total *= paxCount;

        // add baggage
        for (let i = 0; i < paxCount; i++) {
            total += getCheckedValue(`Passengers[${i}].BaggageType`);
        }

        // add services
        const serviceChecks = document.getElementsByClassName("serviceCheck");
        const servicesPerPax = serviceChecks.length / paxCount;
        for (let i = 0; i < paxCount; i++) {
            for (let j = 0; j < servicesPerPax; j++) {
                if (document.querySelector(
                    `input[name='Passengers[${i}].Services[${j}].IsChecked']:checked`
                )) {
                    total += Number(
                        document.querySelector(
                            `input[name='Passengers[${i}].Services[${j}].Price']`
                        ).value
                    );
                }
            }
        }

        document.getElementById("totalAmount").textContent =
            `USD ${total.toFixed(2)}`;
        document.getElementById("Price").value = total.toFixed(2);
    });

    // 7) Recalculate currency when changed
    currencySelect.addEventListener("change", () => {
        const currency = currencySelect.value;
        const amount = document.getElementById("Price").value;
        connection.invoke("GetCurrencyValues", currency, amount)
            .catch(err => console.error(err));
    });
}