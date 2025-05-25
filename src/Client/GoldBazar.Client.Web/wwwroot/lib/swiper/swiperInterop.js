window.initializeBazarSwiper = () => {
    console.log("Initializing Bazar Swiper...");

    const _ = new Swiper(".home-page .swiper", {
        speed: 400,
        spaceBetween: 10,
        loop: true, // optional
        pagination: {
            el: ".home-page .swiper-pagination",
            clickable: true,
        },
    });
};

window.initializeProductDetailsSwiper = () => {
    console.log("Initializing Product Details Swiper...");

    const _ = new Swiper(".product-details .swiper", {
        speed: 400,
        spaceBetween: 10,
        loop: true, // optional
        pagination: {
            el: ".product-details .swiper-pagination",
            clickable: true,
        },
    });
};