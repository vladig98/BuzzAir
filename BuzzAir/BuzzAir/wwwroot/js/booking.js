function AddSlash() {
    var x = document.getElementById('expiryDatePayment').value;

    if (document.getElementById('expiryDatePayment').value.length == 2) {
        document.getElementById('expiryDatePayment').value = x + '/';
        console.log('changed');
    }
}
var price = 0;
var MP = document.getElementById('MP').value;
function toArray(obj) {
    var array = [];
    for (var i = obj.length >>> 0; i--;) {
        array[i] = obj[i];
    }
    return array;
}
function Convert(selected) {
    var dic = {
        "EUR": 0.87,
        "GBP": 0.79,
        "BGN": 1.71,
        "USD": 1,
        "BAM": 1.71,
        "HRK": 6.49,
        "CZK": 22.5,
        "GEL": 2.68,
        "HUF": 280.85,
        "CHF": 0.98,
        "NOK": 8.71,
        "PLN": 3.76,
        "RUB": 69.50,
        "SEK": 8.97,
        "TRY": 5.27,
        "UAH": 27.39,
        "INR": 69.93,
        "CNY": 6.88,
        "JPY": 110.28,
        "CAD": 1.36
    };
    document.getElementById('total').innerHTML = parseFloat(price * dic[selected]).toFixed(2);
}
function showNext() {
    var flights = document.getElementsByClassName('flights');
    var flightLabelsSpan = document.getElementsByClassName('flightLabelsSpan');
    var flightChecks = document.getElementsByClassName('flightChecks');
    var passengers = document.getElementsByClassName('passengers');
    var prices = document.getElementsByClassName('prices');
    var arr = toArray(flightChecks);
    for (var i = 0; i < arr.length; i++) {
        if (arr[i].checked == true) {
            price += parseFloat(prices[i].innerHTML * MP);
        }
    }
    if (arr.some(x => x.checked === true)) {
        for (var i = 0; i < passengers.length; i++) {
            passengers[i].classList.remove('d-none');
        }
    } else {
        window.alert('Please select a flight');
        return;
    }
    for (var i = 0; i < flightChecks.length; i++) {
        if (flightChecks[i].checked == false) {
            document.getElementById('flightT' + i).classList.add('d-none');
        }
    }
    for (var i = 0; i < flights.length; i++) {
        if (flightChecks[i].checked == true) {
            flightLabelsSpan[i].textContent = 'Selected';
            flightChecks[i].disabled = true;
        }
    }
    document.getElementById('buttonSubmit').setAttribute('href', 'javascript:showNames();');
    document.getElementById('total').innerHTML = parseFloat(price);
}
function formatDate(date) {
    var monthNames = [
        "January", "February", "March",
        "April", "May", "June", "July",
        "August", "September", "October",
        "November", "December"
    ];

    var day = date.getDate();
    var monthIndex = date.getMonth();
    var year = date.getFullYear();

    return day + ' ' + monthNames[monthIndex] + ' ' + year;
}
function showNames() {
    var genders = toArray(document.getElementsByClassName('genders'));
    var dobs = toArray(document.getElementsByClassName('dobs'));
    var firstNames = toArray(document.getElementsByClassName('firstNames'));
    var lastNames = toArray(document.getElementsByClassName('lastNames'));
    var labels = toArray(document.getElementsByClassName('labels'));
    if (genders.some(x => x.selectedIndex == 0) ||
        dobs.some(x => x.value == null || x.value == '') ||
        firstNames.some(x => x.value == null || x.value == '') ||
        lastNames.some(x => x.value == null || x.value == '')) {
        window.alert('Please fill out everything');
        return;
    } else {
        for (var i = 0; i < genders.length; i++) {
            var d = new Date(dobs[i].value);
            genders[i].classList.add('d-none');
            dobs[i].classList.add('d-none');
            firstNames[i].disabled = true;
            lastNames[i].classList.add('d-none');
            firstNames[i].value = " " + firstNames[i].value + ' | ' + lastNames[i].value + ' ' + formatDate(d) + ' – ' + genders[i].value;
            for (var l of labels) {
                l.classList.add('d-none');
            }
            var label = document.getElementById('firstLabel' + i);
            label.classList.remove('d-none');
            label.innerHTML = 'Passenger ' + (i + 1);
        }
    }
    var docs = document.getElementsByClassName('docs');
    for (var doc of docs) {
        doc.classList.remove('d-none');
    }
    document.getElementById('buttonSubmit').setAttribute('href', 'javascript:showDocs();');
    document.getElementById('total').innerHTML = price;
}
function getDocType(doc) {
    if (doc.value === 'Passport') {
        return 'P';
    } else {
        return 'ID';
    }
}
function getLabelDoc(doc) {
    if (doc.value === 'Passport') {
        return 'Passport';
    } else {
        return 'ID card';
    }
}
function compareDates(issue, expiry) {
    var d1 = issue.value.split('/');
    var month1 = d1[0];
    var day1 = d1[1];
    var year1 = d1[2];
    var d2 = expiry.value.split('/');
    var month2 = d2[0];
    var day2 = d2[1];
    var year2 = d2[2];
    if (year1 > year2) {
        return 1;
    } else if (year1 == year2) {
        if (month1 > month2) {
            return 1;
        } else if (month1 == month2) {
            if (day1 > day2) {
                return 1;
            } else if (day1 == day2) {
                return 0;
            } else {
                return -1;
            }
        } else {
            return -1;
        }
    } else {
        return -1;
    }
}
function showDocs() {
    var docTypes = toArray(document.getElementsByClassName('docTypes'));
    var docNumbers = toArray(document.getElementsByClassName('docNumbers'));
    var issueDates = toArray(document.getElementsByClassName('issueDates'));
    var expiryDates = toArray(document.getElementsByClassName('expiryDates'));
    var nationalities = toArray(document.getElementsByClassName('nationalities'));
    var birthCountries = toArray(document.getElementsByClassName('birthCountries'));
    var docGenders = toArray(document.getElementsByClassName('genderDocs'));
    var docResults = toArray(document.getElementsByClassName('docResults'));
    var docs = toArray(document.getElementsByClassName('docs'));
    if (docTypes.some(x => x.selectedIndex == 0) ||
        docNumbers.some(x => x.value == null || x.value == '') ||
        issueDates.some(x => x.value == null || x.value == '') ||
        expiryDates.some(x => x.value == null || x.value == '') ||
        nationalities.some(x => x.selectedIndex == 0) ||
        birthCountries.some(x => x.selectedIndex == 0) ||
        docGenders.some(x => x.selectedIndex == 0)) {
        window.alert('Please fill out everything');
        return;
    } else {
        for (var i = 0; i < issueDates.length; i++) {
            if (compareDates(issueDates[i], expiryDates[i]) > 0) {
                window.alert('Expiry date cannot be before the issue date.');
                return;
            }
            if (compareDates(issueDates[i], expiryDates[i]) == 0) {
                window.alert('Expiry date cannot be equal to the issue date.');
                return;
            }
        }
        for (var i = 0; i < docTypes.length; i++) {
            docTypes[i].classList.add('d-none');
            docNumbers[i].disabled = true;
            issueDates[i].classList.add('d-none');
            expiryDates[i].classList.add('d-none');
            nationalities[i].classList.add('d-none');
            birthCountries[i].classList.add('d-none');
            docGenders[i].classList.add('d-none');
            var number = docNumbers[i].value;
            var d1 = new Date(issueDates[i].value);
            var d2 = new Date(expiryDates[i].value);
            docNumbers[i].classList.add('d-none');
            docResults[i].classList.remove('d-none');
            document.getElementById('labelDocResult' + i).innerHTML = getLabelDoc(docTypes[i]);
            document.getElementById('docResult' + i).innerHTML = " " + getDocType(docTypes[i]) + ': ' +
                number + ' \r\nIssue Date: ' + formatDate(d1) +
                ' \r\nExpiry Date: ' + formatDate(d2) +
                ' \r\nNationality: ' + nationalities[i].value +
                ' \r\nBirth Country: ' + birthCountries[i].value +
                ' \r\nGender: ' + docGenders[i].value + " |";
            for (var doc of docs) {
                doc.classList.add('d-none');
            }
        }
    }
    if (docResults.every(x => !x.classList.contains('d-none'))) {
        var services = document.getElementsByClassName('services');
        for (var i = 0; i < services.length; i++) {
            services[i].classList.remove('d-none');
        }
        document.getElementById('buttonSubmit').setAttribute('href', 'javascript:showServices();');
        document.getElementById('total').innerHTML = price;
    }
}
function showServices() {
    var kilos = document.getElementsByClassName('baggageKilos');
    var seatsType = document.getElementsByClassName('seatsType');
    var airportCheckIns = toArray(document.getElementsByClassName('airportCheckIns'));
    var baggages = toArray(document.getElementsByClassName('baggages'));
    var flexibility = toArray(document.getElementsByClassName('flexibilities'));
    var onTimeArrivals = toArray(document.getElementsByClassName('onTimeArrivals'));
    var priorities = toArray(document.getElementsByClassName('priorities'));
    var seats = toArray(document.getElementsByClassName('seats'));
    var kgs20 = toArray(document.getElementsByClassName('20kgs'));
    var kgs32 = toArray(document.getElementsByClassName('32kgs'));
    var normalSeats = toArray(document.getElementsByClassName('normalSeat'));
    var extraLegRooms = toArray(document.getElementsByClassName('extraLegRoom'));
    var sResults = document.getElementsByClassName('sResults');
    for (var i = 0; i < baggages.length; i++) {
        airportCheckIns[i].disabled = true;
        baggages[i].disabled = true;
        flexibility[i].disabled = true;
        onTimeArrivals[i].disabled = true;
        priorities[i].disabled = true;
        seats[i].disabled = true;
    }
    for (var i = 0; i < baggages.length; i++) {
        if (baggages[i].checked === true) {
            kilos[i].classList.remove('d-none');
        }
        if (seats[i].checked) {
            seatsType[i].classList.remove('d-none');
        }
    }
    document.getElementById('buttonSubmit').setAttribute('href', 'javascript:listServices();');
    document.getElementById('total').innerHTML = price;
}
function listServices() {
    var kilos = document.getElementsByClassName('baggageKilos');
    var seatsType = document.getElementsByClassName('seatsType');
    var services = document.getElementsByClassName('services');
    var servicesResult = document.getElementsByClassName('servicesResult');
    var airportCheckIns = toArray(document.getElementsByClassName('airportCheckIns'));
    var baggages = toArray(document.getElementsByClassName('baggages'));
    var flexibility = toArray(document.getElementsByClassName('flexibilities'));
    var onTimeArrivals = toArray(document.getElementsByClassName('onTimeArrivals'));
    var priorities = toArray(document.getElementsByClassName('priorities'));
    var seats = toArray(document.getElementsByClassName('seats'));
    var kgs20 = toArray(document.getElementsByClassName('20kgs'));
    var kgs32 = toArray(document.getElementsByClassName('32kgs'));
    var normalSeats = toArray(document.getElementsByClassName('normalSeat'));
    var extraLegRooms = toArray(document.getElementsByClassName('extraLegRoom'));
    var sResults = document.getElementsByClassName('sResults');
    for (var i = 0; i < baggages.length; i++) {
        var result = ' ';
        if (kgs20[i].checked == true) {
            result += '– Baggage 20kg \r\n';
            price += parseFloat(document.getElementById('priceFor20kg').innerHTML);
        } else if (kgs32[i].checked == true) {
            result += '– Baggage 32kg \r\n';
            price += parseFloat(document.getElementById('priceFor32kg').innerHTML);
        }
        if (normalSeats[i].checked == true) {
            result += '– Normal Seat \r\n';
            price += parseFloat(document.getElementById('priceForNormalSeat').innerHTML);
        } else if (extraLegRooms[i].checked == true) {
            result += '– Extra Leg-Room Seat \r\n';
            price += parseFloat(document.getElementById('priceForExtraLegRoomSeat').innerHTML);
        }
        if (airportCheckIns[i].checked == true) {
            result += '– Airport Check-In \r\n'
            price += parseFloat(document.getElementById('priceForAirportCheckIn').innerHTML);
        }
        if (onTimeArrivals[i].checked == true) {
            result += '– On Time Arrival \r\n'
            price += parseFloat(document.getElementById('priceForOnTimeArrival').innerHTML);
        }
        if (priorities[i].checked == true) {
            result += '– Priority \r\n'
            price += parseFloat(document.getElementById('priceForPriority').innerHTML);
        }
        if (flexibility[i].checked == true) {
            result += '– Flexibility \r\n'
            price += parseFloat(document.getElementById('priceForFlexibility').innerHTML);
        }

        sResults[i].innerHTML = result.trimEnd('\r\n');
        var rows = result.split(/\r\n|\r|\n/).length - 1;
        sResults[i].rows = rows;
    }
    for (var i = 0; i < services.length; i++) {
        kilos[i].classList.add('d-none');
        seatsType[i].classList.add('d-none');
        services[i].classList.add('d-none');
        servicesResult[i].classList.remove('d-none');
    }
    document.getElementById('buttonSubmit').setAttribute('href', 'javascript:Payment();');
    document.getElementById('total').innerHTML = price;
    var payment = document.getElementsByClassName('payment');
    for (var p of payment) {
        p.classList.remove('d-none');
    }
    document.getElementById('buttonSubmit').onclick = null;
}
function Payment() {
    var failed = false;
    if (document.getElementById('cardNumber').value.length !== 16) {
        failed = true;
        console.log('carNumber length');
    }
    if (!/^\d+$/.test(document.getElementById('cardNumber').value)) {
        failed = true;
        console.log('carNumber value');
    }
    if (document.getElementById('cvc').value.length !== 3) {
        failed = true;
        console.log('cvc length');
    }
    if (!/^\d+$/.test(document.getElementById('cvc').value)) {
        failed = true;
        console.log('cvc value');
    }
    var ct = document.getElementById('cardType');
    if (ct.options[ct.selectedIndex].value == 0) {
        failed = true;
        console.log('cardTyp index');
    }
    var curr = document.getElementById('currency');
    if (curr.options[curr.selectedIndex].value == 0) {
        failed = true;
        console.log('currency index');
    }
    if (document.getElementById('expiryDatePayment').value.length !== 5) {
        failed = true;
        console.log('expiry length');
    }
    if (!/^[0-9\/]+$/.test(document.getElementById('expiryDatePayment').value)) {
        failed = true;
        console.log('expiry value');
    }
    if (document.getElementById('expiryDatePayment').value.split('/')[0] > 12) {
        failed = true;
        console.log('month limit');
    }
    if (failed) {
        window.alert('Please enter valid data.');
        document.getElementById('buttonSubmit').setAttribute('href', 'javascript:Payment();');
        document.getElementById('total').innerHTML = price;
        var payment = document.getElementsByClassName('payment');
        for (var p of payment) {
            p.classList.remove('d-none');
        }
        document.getElementById('buttonSubmit').onclick = null;
        return;
    }
    var payment = document.getElementsByClassName('payment');
    for (var p of payment) {
        p.classList.add('d-none');
    }
    var currency = document.getElementById('currency');
    document.getElementById('paymentSummaryDiv').classList.remove('d-none');
    document.getElementById('paymentSummary').innerHTML = document.getElementById('cardType').value +
        ' \r\n' + document.getElementById('cardNumber').value +
        ' \r\n' + document.getElementById('expiryDatePayment').value +
        ' \r\n' + document.getElementById('cardHolder').value +
        ' \r\n' + document.getElementById('cvc').value +
        ' \r\n' + currency.options[currency.selectedIndex].value + ' ' +
        document.getElementById('total').innerHTML;
    document.getElementById('buttonSubmit').classList.add('d-none');
    Submit();
    document.getElementById('btnSumbit').classList.remove('d-none');
    //document.getElementById('buttonSubmit').setAttribute('href', 'javascript:Submit();');
}
function Submit() {
    var firstNames = [];
    var docsResults = [];
    var servicesResults = [];
    var flightsResults = [];
    var flightChecks = document.getElementsByClassName('flightChecks');
    for (var i = 0; i < MP; i++) {
        firstNames.push(document.getElementById('firstName' + i).value);
        docsResults.push(document.getElementById('docResult' + i).value);
        servicesResults.push(document.getElementById('serviceResult' + i).value);
        servicesResults.push(' | ');
    }
    document.getElementById('namesResults').value = firstNames.join('\r\n');
    document.getElementById('passportResults').value = docsResults.join('\r\n');
    document.getElementById('servicesResults').value = servicesResults.join('\r\n\r\n');
    for (var i = 0; i < flightChecks.length; i++) {
        if (flightChecks[i].checked == true) {
            flightsResults.push(document.getElementById('flight' + i).innerHTML);
        }
    }
    document.getElementById('flightsResults').value = flightsResults.join('\r\n\r\n');
    document.getElementById('paymentsResults').value = document.getElementById('paymentSummary').innerHTML;
    console.log(document.getElementById('namesResults').value);
    console.log(document.getElementById('passportResults').value);
    console.log(document.getElementById('servicesResults').value);
    console.log(document.getElementById('flightsResults').value);
    console.log(document.getElementById('paymentsResults').value);
    //paymentsResults
    //document.getElementById('bookingForm').submit();
}
document.getElementById('buttonSubmit').onclick = function () {
    document.getElementById('total').innerHTML = price;
}