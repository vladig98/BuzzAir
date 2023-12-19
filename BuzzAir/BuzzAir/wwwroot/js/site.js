function HomePage() {
    document.getElementById('departureDate').value = "";
    document.getElementById('returnDate').value = "";

    $('#departureDate').pickadate({
        min: new Date(),
        max: new Date(new Date().setFullYear(new Date().getFullYear() + 1)),
        today: null,
        clear: null,
        close: null
    })

    $('#returnDate').pickadate({
        min: new Date(),
        max: new Date(new Date().setFullYear(new Date().getFullYear() + 1)),
        today: null,
        clear: null,
        close: null
    })

    let radioButtons = document.searchFlightsForm.isReturning;

    for (let i = 0; i < radioButtons.length; i++) {
        radioButtons[i].addEventListener('change', function () {
            if (this.value == "OneWay") {
                $("#returnGroup").hide();
            } else {
                $("#returnGroup").show();

                connection.invoke("GetReturnFlightDates", originId, destinationId).catch(function (err) {
                    return console.error(err.toString());
                });
            }
        })
    }
}

function PrintBoardingPass() {
    var original = document.body.innerHTML;
    var printable = document.getElementById('printableArea').innerHTML;

    var mywindow = window.open('', 'PRINT', 'height=400,width=600');

    mywindow.document.write('<html><head><title>' + document.title + '</title>');
    mywindow.document.write('</head><body >');
    mywindow.document.write('<h1>' + document.title + '</h1>');
    mywindow.document.write(printable);
    mywindow.document.write('</ body></html>');

    mywindow.document.close();
    mywindow.focus();

    mywindow.print();

    document.getElementById('printed').classList.remove('d-none');
    document.getElementById('printBoardingPassBtn').classList.add('d-none');
}

function CreateBooking() {
    let divs = document.getElementsByClassName('divButton');

    $("#Payment_ExpiryDate").on("input", function () {
        if ($("#Payment_ExpiryDate").val().length == 2 && !$("#Payment_ExpiryDate").val().includes("/")) {
            $("#Payment_ExpiryDate").val(`${$("#Payment_ExpiryDate").val()}/`)
        }
    })

    for (let i = 0; i < divs.length; i++) {
        divs[i].addEventListener("click", function () {
            this.children[1].children[1].children[0].checked = true;
        })
    }

    let radioDivs = document.getElementsByClassName('radioDiv');

    for (let i = 0; i < radioDivs.length; i++) {
        radioDivs[i].addEventListener('click', function () {
            this.getElementsByTagName('input')[0].checked = true;
            this.parentElement.style.outline = "solid 2px red"

            let attr = this.getElementsByTagName('input')[0].getAttribute("name");
            let elements = document.getElementsByName(attr);
            for (let j = 0; j < elements.length; j++) {
                if (elements[j].parentElement != this) {
                    elements[j].parentElement.parentElement.style.outline = "none";
                }
            }
        })
    }

    $("#flightsBtn").click(function (e) {
        e.preventDefault();

        $goingFlight = $("input[name='GoingFlightSelection']")
        $returnFlight = $("input[name='ReturnFlightSelection']")

        for (let i = 0; i < $goingFlight.length; i++) {
            if ($goingFlight[i].checked) {
                if ($returnFlight.length == 0) {
                    $("#flightsSection").addClass("d-none")
                    $("#passengersSection").removeClass("d-none")
                } else {
                    for (let j = 0; j < $returnFlight.length; j++) {
                        if ($returnFlight[j].checked) {
                            $("#flightsSection").addClass("d-none")
                            $("#passengersSection").removeClass("d-none")
                        }
                    }
                }
            }
        }
    })

    $("#passengersBtn").click(function (e) {
        e.preventDefault();

        $names = $(".paxName")

        for (let i = 0; i < $names.length; i++) {
            if (!$names[i].value) {
                return
            }
        }

        $gender = $(".paxGender")

        let genderNames = [];

        for (let i = 0; i < $gender.length; i++) {
            if (!genderNames.includes($($gender[i]).attr("name"))) {
                genderNames.push($($gender[i]).attr("name"))
            }
        }

        for (let i = 0; i < genderNames.length; i++) {
            let valid = false;

            $genderRadio = $(`input[name='${genderNames[i]}']`)

            for (let j = 0; j < $genderRadio.length; j++) {
                if ($genderRadio[j].checked) {
                    valid = true;
                }
            }

            if (!valid) {
                return
            }
        }

        $baggage = $(".paxBaggage")

        let baggageNames = [];

        for (let i = 0; i < $baggage.length; i++) {
            if (!baggageNames.includes($($baggage[i]).attr("name"))) {
                baggageNames.push($($baggage[i]).attr("name"))
            }
        }

        for (let i = 0; i < baggageNames.length; i++) {
            let valid = false;

            $baggageRadio = $(`input[name='${baggageNames[i]}']`)

            for (let j = 0; j < $baggageRadio.length; j++) {
                if ($baggageRadio[j].checked) {
                    valid = true;
                }
            }

            if (!valid) {
                return
            }
        }

        $("#passengersSection").addClass("d-none")
        $("#servicesSection").removeClass("d-none")
    })

    $("#servicesBtn").click(function (e) {
        e.preventDefault();

        $("#servicesSection").addClass("d-none")
        $("#paymentSection").removeClass("d-none")

        let totalPrice = 0;

        let paxCount = $("#PassengersCount").val()

        let goingFlightPrice = Number([...document.getElementsByName('GoingFlightSelection')].filter(x => x.checked)[0].parentElement.parentElement.children[0].innerHTML.trim().substring(1));
        let returnFlightPrice = document.getElementsByName('ReturnFlightSelection').length == 0 ? 0 : Number([...document.getElementsByName('ReturnFlightSelection')].filter(x => x.checked)[0].parentElement.parentElement.children[0].innerHTML.trim().substring(1));

        totalPrice += goingFlightPrice;
        totalPrice += returnFlightPrice;

        totalPrice *= paxCount;

        for (let i = 0; i < paxCount; i++) {
            let baggageId = [...document.getElementsByName(`Passengers[${i}].BaggageType`)].filter(x => x.checked)[0].id

            let priceValue = document.querySelectorAll(`[for="${baggageId}"]`)[0].children[2].innerHTML.trim().substring(1)

            let baggagePrice = priceValue == "ree" ? 0 : Number(priceValue)

            totalPrice += baggagePrice;
        }

        let servicesCount = document.getElementsByClassName('serviceCheck').length;

        for (let i = 0; i < paxCount; i++) {
            for (let j = 0; j < servicesCount / paxCount; j++) {
                let service = document.getElementsByName(`Passengers[${i}].Services[${j}].IsChecked`)[0]

                if (service.checked) {
                    totalPrice += Number(document.getElementsByName(`Passengers[${i}].Services[${j}].Price`)[0].value)
                }
            }
        }

        $("#totalAmount").text("USD " + totalPrice.toFixed(2))
        $("#Price").val(totalPrice.toFixed(2))
    })

    $("#Payment_Currency").on("change", function () {
        let currency = $("#Payment_Currency").val()
        let amount = $("#Price").val();

        connection.invoke("GetCurrencyValues", currency, amount).catch(function (err) {
            return console.error(err.toString());
        });
    })
}