document.getElementById('reservationForm').addEventListener('submit', function (e) {
    const input = document.getElementById('reservationTime');
    const value = input.value;

    if (value) {
        // Parse the value into a Date object
        const date = new Date(value);

        // Extract the minutes
        const minutes = date.getMinutes();

        // Check if the minutes are one of the allowed intervals
        if (![0, 15, 30, 45].includes(minutes)) {
            e.preventDefault();  // Prevent form submission
            alert('Please select a time with a valid interval (e.g., :00, :15, :30, :45).');
        }
    }
});
document.querySelector('input[name="Reservation.PartySize"]').addEventListener('input', function (e) {
    if (e.target.value < 1) {
        e.target.value = 1;
        }
        if (e.target.value > 20) {
            e.target.value = 20
        }
});