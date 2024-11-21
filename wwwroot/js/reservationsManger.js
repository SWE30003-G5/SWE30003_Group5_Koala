    document.querySelector('input[name="Reservation.PartySize"]').addEventListener('input', function (e) {
    if (e.target.value < 1) {
        e.target.value = 1;
        }
        if (e.target.value > 20) {
            e.target.value = 20
        }
});