
   

    
        document.addEventListener("DOMContentLoaded", function () {
            document.querySelectorAll(".add-to-cart").forEach(button => {
                button.addEventListener("click", function () {
                    let productId = this.getAttribute("data-product-id");
                    let stock = parseInt(this.getAttribute("data-stock"));

                    if (stock > 0) {
                        fetch('/Cart/AddToCart', {
                            method: 'POST',
                            headers: { 'Content-Type': 'application/json' },
                            body: JSON.stringify({ productId: productId })
                        })
                        .then(response => response.json())
                        .then(data => {
                            if (data.success) {
                                alert("Item added to cart! 🎉");

                                // تحديث المخزون في الواجهة
                                let newStock = stock - 1;
                                this.setAttribute("data-stock", newStock);
                                this.closest(".card").querySelector(".fw-bold").textContent = 
                                    newStock > 0 ? `In Stock: ${newStock}` : "Out of Stock";

                                // تعطيل الزر عند نفاد المخزون
                                if (newStock === 0) {
                                    this.setAttribute("disabled", "true");
                                    this.classList.remove("btn-success");
                                    this.classList.add("btn-secondary");
                                    this.innerHTML = `<i class="bi bi-x-circle"></i> Out of Stock`;
                                }
                            }
                        })
                        .catch(error => console.error('Error:', error));
                    }
                });
            });
        });

        // Initialize Swiper
        var swiper = new Swiper(".mySwiper", {
            slidesPerView: 3,
            spaceBetween: 30,
            pagination: {
                el: ".swiper-pagination",
                clickable: true,
            },
            navigation: {
                nextEl: ".swiper-button-next",
                prevEl: ".swiper-button-prev",
            },
            autoplay: {
                delay: 3000,
                disableOnInteraction: false,
            },
        });
   

