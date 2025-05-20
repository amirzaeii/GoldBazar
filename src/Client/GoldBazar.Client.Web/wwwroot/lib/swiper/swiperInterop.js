window.initializeSwiper = () => {
    console.log("Initializing Swiper...");

    const _ = new Swiper(".swiper", {
        speed: 400,
        spaceBetween: 10,
        loop: true, // optional
        pagination: {
            el: ".swiper-pagination",
            clickable: true,
        },
    });
};