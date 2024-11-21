function getCookie(cookieName) {
    const name = cookieName + "=";
    const decodedCookie = decodeURIComponent(document.cookie);
    const cookieArray = decodedCookie.split(';');
    for (let i = 0; i < cookieArray.length; i++) {
        let cookie = cookieArray[i].trim();
        if (cookie.indexOf(name) === 0) {
            return cookie.substring(name.length, cookie.length);
        }
    }
    return null;
}
document.getElementById('reservationForm').addEventListener('submit', function (event) {
    event.preventDefault();

    try {
        const userCookie = getCookie("userCookie");
        if (!userCookie) {
            alert("You must be logged in to make a reservation.");
            return;
        }
    } catch (error) {
        console.error("Error checking user login:", error);
        alert("An error occurred while checking your session. Please log in again.");
        return;
    }

    const dateInput = document.getElementById("date").value;
    const timeInput = document.getElementById("time").value;

    if (!dateInput || !timeInput) {
        alert("Please provide both date and time.");
        return;
    }

    const selectedDateTime = new Date(`${dateInput}T${timeInput}:00`);
    const currentDateTime = new Date();

    if (selectedDateTime < currentDateTime) {
        alert("The reservation date and time cannot be in the past.");
        return;
    }

    alert("Reservation booked!");

    event.target.submit();
});

document.getElementById('guests').addEventListener('input', function () {
    let value = parseInt(this.value, 10);

    if (value < 1) {
        this.value = 1;
    } else if (value > 20) {
        this.value = 20;
    }
});

function populateTimeOptions(dateInput, timeSelect, currentDateTime) {
    const intervals = ['00', '15', '30', '45'];
    const startHour = 0;
    const endHour = 23;

    const selectedDate = new Date(dateInput.value);
    const isToday = selectedDate.toDateString() === currentDateTime.toDateString();

    timeSelect.innerHTML = '';

    // Loop through each hour of the day and create options
    for (var i = 0; i <= (endHour * 4 + 3); i++) { // (24 * 4) + 3 for 00:00 to 23:45
        const hour = Math.floor(i / 4); // Calculate hour (0-23)
        const interval = intervals[i % 4]; // Get corresponding interval (00, 15, 30, 45)

        const hourStr = String(hour).padStart(2, '0');
        const timeValue = `${hourStr}:${interval}`;
        const optionText = `${hourStr}:${interval}`;

        const option = document.createElement('option');
        option.value = timeValue;
        option.textContent = optionText;

        if (isToday) {
            const currentHour = currentDateTime.getHours();
            const currentMinute = currentDateTime.getMinutes();

            if (hour > currentHour || (hour === currentHour && parseInt(interval) >= currentMinute)) {
                timeSelect.appendChild(option);
            }
        } else {
            timeSelect.appendChild(option);
        }
    }
}

document.addEventListener('DOMContentLoaded', function () {
    const currentDateTime = new Date();

    // Date
    const year = currentDateTime.getFullYear();
    const month = String(currentDateTime.getMonth() + 1).padStart(2, '0');
    const day = String(currentDateTime.getDate()).padStart(2, '0');
    const formattedDate = `${year}-${month}-${day}`;
    const dateInput = document.getElementById('date');
    dateInput.value = formattedDate;

    // Time
    const timeSelect = document.getElementById('time');

    populateTimeOptions(dateInput, timeSelect, currentDateTime);
    dateInput.addEventListener('change', function () {
        populateTimeOptions(dateInput, timeSelect, currentDateTime);
    });
});