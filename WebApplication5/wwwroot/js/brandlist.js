<script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>

    $(document).ready(function () {
        $("#brandDropdown").on("click", function () {
            $.ajax({
                url: "/Product/GetBrands", // استدعاء الـ Controller
                type: "GET",
                success: function (data) {
                    let brandList = $("#brandList");
                    brandList.empty();
                    data.forEach(function (brand) {
                        brandList.append(`<li><a class="dropdown-item" href="/Product/GetByBrand?id=${brand.id}">${brand.name}</a></li>`);
                    });
                }
            });
        });

        // البحث داخل قائمة الـ Brands
        $("#brandSearch").on("keyup", function () {
            var value = $(this).val().toLowerCase();
            $("#brandList li").filter(function () {
                $(this).toggle($(this).text().toLowerCase().indexOf(value) > -1);
            });
        });
    });

 