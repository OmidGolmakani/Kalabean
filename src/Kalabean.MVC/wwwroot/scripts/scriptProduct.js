
var swiper2;

$(document).ready(function () {

    swiper2 = new Swiper({
        el: '#swiper02',
        initialSlide: 1,
        slidesPerGroup: 1,
        spaceBetween: 10,
        slidesPerView: 1,
        centeredSlides: false,
        slideToClickedSlide: true,
        grabCursor: false,
        loop: true,
        centerInsufficientSlides: true,
        centeredSlidesBounds: true,
        //effect: 'flip', 'coverflow' , 'fade',
        autoplay: {
            delay: 1500,
            disableOnInteraction: true,
        },
        mousewheel: {
            enabled: false,
        },
        keyboard: {
            enabled: false,
        },
        pagination: {
            el: '.swiper-pagination',
            clickable: true,
        },
        navigation: {
            nextEl: '#swiper-button-next02',
            prevEl: '#swiper-button-prev02',
        },
        breakpoints: {
            320: {
                slidesPerView: 1,
                slidesPerGroup: 1,
            },
            450: {
                slidesPerView: 1,
                slidesPerGroup: 1,
            },
            768: {
                slidesPerView: 1,
                slidesPerGroup: 1,
            },
            1199: {
                slidesPerView: 1,
                slidesPerGroup: 1,
            }
        }

    });
    $('#swiper02').hover(function () { swiper2.autoplay.stop(); }, function () { swiper2.autoplay.start(); });

    /*****************************************************************/
    
});