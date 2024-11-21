function getCookie(cookieName) {
    const name = cookieName + "=";
    const decodedCookie = decodeURIComponent(document.cookie);
    const cookieArray = decodedCookie.split(';');
    for (let i = 0; i < cookieArray.length; i++) {
        let cookie = cookieArray[i];
        while (cookie.charAt(0) === ' ') {
            cookie = cookie.substring(1);
        }
        if (cookie.indexOf(name) === 0) {
            return cookie.substring(name.length, cookie.length);
        }
    }
    return null;
}

document.getElementById('orderForm').addEventListener('submit', function (event) {
    event.preventDefault();

    try {
        const userCookie = getCookie("userCookie");
        const users = JSON.parse(userCookie);
        if (!users || users.length === 0) {
            alert("You must be logged in to place an order.");
            return;
        }
    } catch (error) {
        console.error("Error parsing user cookie:", error);
        alert("Invalid session. Please log in again.");
        return;
    }

    const orderType = document.getElementById('orderType').value;
    if (!orderType) {
        alert("Please choose your order type.");
        return;
    }

    const orderItems = [];
    const orderItemsElements = document.querySelectorAll('.order-item');
    var valid = true;

    orderItemsElements.forEach(item => {
        const menuItemID = parseInt(item.querySelector('select[name="menuItem"]').value, 10);
        const quantity = parseInt(item.querySelector('input[name="quantity"]').value, 10);

        if (!menuItemID || menuItemID === "") {
            valid = false;
        }

        if (menuItemID && quantity) {
            orderItems.push({
                MenuItemID: menuItemID,
                Quantity: quantity
            });
        }
    });

    if (!valid) {
        alert("Please select a menu item for all order items.");
        return;
    }

    if (orderItems.length === 0) {
        alert("You must add at least one order item.");
        return;
    }

    const orderItemsJson = JSON.stringify(orderItems);
    const hiddenInput = document.createElement('input');
    hiddenInput.type = 'hidden';
    hiddenInput.name = 'OrderItemsJson';
    hiddenInput.value = orderItemsJson;

    this.appendChild(hiddenInput);
    alert("Your order has been successfully placed!");
    this.submit();
});

document.getElementById('addOrderItem').addEventListener('click', function () {
    const container = document.getElementById('orderItemContainer');
    const orderItem = document.createElement('div');
    orderItem.classList.add('order-item');

    const selectElement = document.createElement('select');
    selectElement.name = 'menuItem';
    selectElement.innerHTML = '<option value="" disabled selected>Select a menu item</option>';

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
        console.warn("Menu Items is empty or undefined");
    }

    const quantityInput = document.createElement('input');
    quantityInput.type = 'number';
    quantityInput.name = 'quantity';
    quantityInput.min = '1';
    quantityInput.value = '1';
    quantityInput.addEventListener('input', function (e) {
        if (this.value === '' || isNaN(this.value) || this.value === '0') {
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