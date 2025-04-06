document.addEventListener("DOMContentLoaded", function () {
    document.querySelectorAll(".add-to-cart").forEach(button => {
        button.addEventListener("click", function (e) {
            e.preventDefault(); // Prevent default navigation
            let productId = this.getAttribute("data-product-id");
            let stock = this.getAttribute("data-stock");

            if (parseInt(stock) <= 0) {
                alert("This product is out of stock!");
                return;
            }

            fetch(`/ShoppingCart/AddOrder?productId=${productId}`, {
                method: "POST",
                headers: { "Content-Type": "application/json" }
            })
                .then(response => response.json())
                .then(data => alert(data.message))
                .catch(error => console.error("Error:", error));
        });
    });
});
