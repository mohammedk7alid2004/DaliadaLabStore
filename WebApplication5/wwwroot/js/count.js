document.addEventListener("DOMContentLoaded", function () {
    async function updateCartCount() {
        try {
            // إرسال طلب إلى الواجهة البرمجية
            const response = await fetch('/ShoppingCart/GetCount');
            console.log("Fetching cart count...");

            // التحقق من حالة الاستجابة
            if (!response.ok) {
                throw new Error(`HTTP error! Status: ${response.status}`);
            }

            // تحويل الاستجابة إلى JSON
            const data = await response.json();
            console.log("Cart data received:", data);

            // الوصول إلى خاصية `count` في الكائن
            if (data && data.count !== undefined) {
                console.log("Cart count:", data.count); // عرض القيمة في Console
                document.getElementById("cartCount").textContent = data.count;
            } else {
                console.error("Invalid data format:", data);
                document.getElementById("cartCount").textContent = "0";
            }
        } catch (error) {
            console.error('Error fetching cart count:', error);
            document.getElementById("cartCount").textContent = "0";
        }
    }

    // تحديث عدد العناصر في السلة عند تحميل الصفحة
    updateCartCount();

    // تحديث عدد العناصر في السلة كل 30 ثانية
    setInterval(updateCartCount, 3000);
});