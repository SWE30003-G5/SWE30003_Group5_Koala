document.getElementById('addOrderItem').addEventListener('click', function () {
    const container = document.getElementById('orderItemContainer');
    const orderItem = document.createElement('div');
    orderItem.classList.add('order-item');

    const selectElement = document.createElement('select');
    selectElement.name = 'menuItem';
    selectElement.innerHTML = '<option value="" disabled selected>Select a menu item</option>';

    // Populate select options using the menuItems JSON data
    if (menuItems && menuItems.length > 0) {
        menuItems.forEach(item => {
            const option = document.createElement('option');
            option.value = item.ID;
            option.textContent = item.Name + " - $" + item.Price;
            option.dataset.price = item.Price;

            if (!item.IsAvailable) {
                option.disabled = true;
                option.textContent += " (Unavailable)";
            }

            selectElement.appendChild(option);
        });
    } else {
        console.warn("menuItems is empty or undefined");
    }

    const quantityInput = document.createElement('input');
    quantityInput.type = 'number';
    quantityInput.name = 'quantity';
    quantityInput.min = '1';
    quantityInput.value = '1';
    quantityInput.setAttribute('data-asp-net', 'Quantity');

    quantityInput.addEventListener('input', function (e) {
        if (this.value === '' || isNaN(this.value ) || this.value === '0') {
            this.value = '1';
        }
    });
    const deleteButton = document.createElement('button');
    deleteButton.type = 'button';
    deleteButton.textContent = 'Delete';
    deleteButton.classList.add('deleteOrderItem');

    orderItem.appendChild(selectElement);
    orderItem.appendChild(quantityInput);
    orderItem.appendChild(deleteButton);
    container.appendChild(orderItem);

    selectElement.addEventListener('change', updateTotal);
    quantityInput.addEventListener('input', updateTotal);
    deleteButton.addEventListener('click', function () {
        orderItem.remove();
        updateTotal();
    });
});

function updateTotal() {
    var total = 0;

    const orderItems = document.querySelectorAll('.order-item');

    orderItems.forEach(item => {
        const select = item.querySelector('select[name="menuItem"]');
        const quantity = item.querySelector('input[name="quantity"]').value;

        if (select && select.value) {
            const price = parseFloat(select.options[select.selectedIndex].dataset.price);
            total += price * quantity;
        }
    });

    document.getElementById('total').value = total.toFixed(2);
    document.getElementById('totalAmountInput').value = total.toFixed(2);
}

document.getElementById('cash').addEventListener('change', function () {
    document.getElementById('cardDetails').style.display = 'none';
});

document.getElementById('card').addEventListener('change', function () {
    document.getElementById('cardDetails').style.display = 'block';
});

document.getElementById('orderForm').addEventListener('submit', function () {
    updateTotal();
});