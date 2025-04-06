document.addEventListener("DOMContentLoaded", function () {
    const removeForms = document.querySelectorAll("#remove_from_cart");

    removeForms.forEach(form => {
        form.addEventListener("submit", function (event) {
            event.preventDefault();

            // Show confirmation dialog before removing
            Swal.fire({
                title: 'Are you sure?',
                text: "Do you want to remove this item?",
                icon: 'warning',
                showCancelButton: true,
                confirmButtonColor: '#d33',
                cancelButtonColor: '#3085d6',
                confirmButtonText: 'Yes, remove it!',
                customClass: {
                    popup: 'swal-custom-popup',
                    title: 'swal-custom-title',
                    confirmButton: 'swal-custom-button'
                }
            }).then((result) => {
                if (result.isConfirmed) {
                    const formData = new FormData(form);
                    formData.append('productId', form.querySelector('input[name="productId"]').value);

                    fetch(form.action, {
                        method: form.method,
                        body: formData,
                        headers: {
                            'Accept': 'application/json'
                        }
                    })
                        .then(response => response.json())
                        .then(data => {
                            Swal.fire({
                                icon: data.success ? 'success' : 'error', // Set icon based on success
                                title: data.success ? 'Removed Successfully!' : 'Error',
                                text: data.message,
                                confirmButtonText: 'OK',
                                customClass: {
                                    popup: 'swal-custom-popup',
                                    title: 'swal-custom-title',
                                    confirmButton: 'swal-custom-button'
                                }
                            }).then(() => {
                                // Optionally refresh the page or update the UI
                                if (data.success) {
                                    location.reload();
                                }
                            });
                        })
                        .catch(error => {
                            Swal.fire({
                                icon: 'error',
                                title: 'Error',
                                text: 'An error occurred while removing the item.',
                                confirmButtonText: 'OK',
                                customClass: {
                                    popup: 'swal-custom-popup',
                                    title: 'swal-custom-title',
                                    confirmButton: 'swal-custom-button'
                                }
                            });
                        });
                }
            });
        });
    });
});