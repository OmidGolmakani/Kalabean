/********************** search-banner ************************************/
var $eventLocationId = $("#Location_Id");

$eventLocationId.on("select2:select", function (e) {
});
$eventLocationId.on("select2:unselect", function (e) {
    //console.log(this.value);
});


var $eventRegionId = $("#Region_Id");

$eventRegionId.on("select2:select", function (e) {
});
$eventRegionId.on("select2:unselect", function (e) {
});

/************************************************************************/
var swiper1;
var swiper2;
var swiper3;
var swiper4;
var swiper5;
var swiper6;

$(document).ready(function () {

    swiper1 = new Swiper({
        el: '#swiper01',
        direction: 'vertical',
        initialSlide: 1,
        slidesPerGroup: 1,
        spaceBetween: 25,
        slidesPerView: 4,
        centeredSlides: false,
        slideToClickedSlide: true,
        grabCursor: false,
        loop: true,
        centerInsufficientSlides: true,
        centeredSlidesBounds: true,
        //effect: 'flip', 'coverflow' , 'fade',
        autoplay: {
            delay: 6000,
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
            nextEl: '#swiper-button-next01',
            prevEl: '#swiper-button-prev01',
        },
        scrollbar: {
            el: '.swiper-scrollbar',
        },
        breakpoints: {
            320: {
                slidesPerView: 2,
                slidesPerGroup: 1,
            },
            450: {
                slidesPerView: 2,
                slidesPerGroup: 1,
            },
            768: {
                slidesPerView: 3,
                slidesPerGroup: 1,
            },
            1199: {
                slidesPerView: 4,
                slidesPerGroup: 1,
            }
        }

    });
    $('#swiper01').hover(function () { swiper1.autoplay.stop(); }, function () { swiper1.autoplay.start(); });

    /******************************************************************/
    swiper2 = new Swiper({
        el: '#swiper02',
        initialSlide: 1,
        slidesPerGroup: 1,
        spaceBetween: 20,
        slidesPerView: 1,
        centeredSlides: false,
        slideToClickedSlide: true,
        grabCursor: false,
        loop: true,
        centerInsufficientSlides: true,
        centeredSlidesBounds: true,
        //effect: 'flip', 'coverflow' , 'fade',
        autoplay: {
            delay: 7000,
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

    /******************************************************************/
    swiper3 = new Swiper({
        el: '#swiper03',
        initialSlide: 1,
        slidesPerGroup: 1,
        spaceBetween: 20,
        slidesPerView: 5,
        centeredSlides: false,
        slideToClickedSlide: true,
        grabCursor: false,
        loop: true,
        centerInsufficientSlides: true,
        centeredSlidesBounds: true,
        //effect: 'flip', 'coverflow' , 'fade',
        autoplay: {
            delay: 3000,
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
            nextEl: '#swiper-button-next03',
            prevEl: '#swiper-button-prev03',
        },
        breakpoints: {
            320: {
                slidesPerView: 1,
                slidesPerGroup: 1,
            },
            450: {
                slidesPerView: 2,
                slidesPerGroup: 1,
            },
            768: {
                slidesPerView: 3,
                slidesPerGroup: 1,
            },
            1199: {
                slidesPerView: 5,
                slidesPerGroup: 1,
            }
        }

    });
    $('#swiper03').hover(function () { swiper3.autoplay.stop(); }, function () { swiper3.autoplay.start(); });

    /******************************************************************/
    swiper4 = new Swiper({
        el: '#swiper04',
        initialSlide: 1,
        slidesPerGroup: 1,
        spaceBetween: 20,
        slidesPerView: 4,
        centeredSlides: false,
        slideToClickedSlide: true,
        grabCursor: false,
        loop: true,
        centerInsufficientSlides: true,
        centeredSlidesBounds: true,
        //effect: 'flip', 'coverflow' , 'fade',
        autoplay: {
            delay: 4000,
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
            nextEl: '#swiper-button-next04',
            prevEl: '#swiper-button-prev04',
        },
        breakpoints: {
            320: {
                slidesPerView: 1,
                slidesPerGroup: 1,
            },
            450: {
                slidesPerView: 2,
                slidesPerGroup: 1,
            },
            768: {
                slidesPerView: 3,
                slidesPerGroup: 1,
            },
            1199: {
                slidesPerView: 5,
                slidesPerGroup: 1,
            }
        }

    });
    $('#swiper04').hover(function () { swiper4.autoplay.stop(); }, function () { swiper4.autoplay.start(); });

    /******************************************************************/
    swiper5 = new Swiper({
        el: '#swiper05',
        initialSlide: 1,
        slidesPerGroup: 1,
        spaceBetween: 20,
        slidesPerView: 5,
        centeredSlides: false,
        slideToClickedSlide: true,
        grabCursor: false,
        loop: true,
        centerInsufficientSlides: true,
        centeredSlidesBounds: true,
        //effect: 'flip', 'coverflow' , 'fade',
        autoplay: {
            delay: 5000,
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
            nextEl: '#swiper-button-next05',
            prevEl: '#swiper-button-prev05',
        },
        breakpoints: {
            320: {
                slidesPerView: 1,
                slidesPerGroup: 1,
            },
            450: {
                slidesPerView: 2,
                slidesPerGroup: 1,
            },
            768: {
                slidesPerView: 3,
                slidesPerGroup: 1,
            },
            1199: {
                slidesPerView: 5,
                slidesPerGroup: 1,
            }
        }

    });
    $('#swiper05').hover(function () { swiper5.autoplay.stop(); }, function () { swiper5.autoplay.start(); });

    /******************************************************************/
    swiper6 = new Swiper({
        el: '#swiper06',
        initialSlide: 1,
        slidesPerGroup: 1,
        spaceBetween: 20,
        slidesPerView: 3,
        centeredSlides: false,
        slideToClickedSlide: true,
        grabCursor: false,
        loop: true,
        centerInsufficientSlides: true,
        centeredSlidesBounds: true,
        //effect: 'flip', 'coverflow' , 'fade',
        autoplay: {
            delay: 7000,
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
            nextEl: '#swiper-button-next06',
            prevEl: '#swiper-button-prev06',
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
                slidesPerView: 2,
                slidesPerGroup: 1,
            },
            1199: {
                slidesPerView: 3,
                slidesPerGroup: 1,
            }
        }

    });
    $('#swiper06').hover(function () { swiper6.autoplay.stop(); }, function () { swiper6.autoplay.start(); });

    /********************** search-banner ************************************/
    $eventLocationId.select2({
        placeholder: "همه موارد"
        , allowClear: true
    });

    $eventRegionId.select2({
        placeholder: "در مشهد"
        , allowClear: true
    });
    /*****************************************************************/
    
});