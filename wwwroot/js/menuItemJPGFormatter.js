document.getElementById('menuItemName').addEventListener('input', function () {
    const name = this.value.trim();
    if (name) {
        const formattedName = name.toLowerCase().replace(/\s+/g, '_') + ".jpg";
        document.getElementById('imageLocation').value = formattedName.charAt(0).toUpperCase() + formattedName.slice(1);
    } else {
        document.getElementById('imageLocation').value = '';
    }
});