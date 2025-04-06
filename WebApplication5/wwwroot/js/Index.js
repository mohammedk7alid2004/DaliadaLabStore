    document.addEventListener("DOMContentLoaded", function () {
    var swiper = new Swiper(".swiper-container", {
        loop: true, // Infinite loop
    autoplay: {
        delay: 3000, // Change every 3 seconds
    disableOnInteraction: false, // Keep autoplay after user interaction
      },
    pagination: {
        el: ".swiper-pagination",
    clickable: true,
      },
    navigation: {
        nextEl: ".swiper-button-next",
    prevEl: ".swiper-button-prev",
      },
    });
  });
