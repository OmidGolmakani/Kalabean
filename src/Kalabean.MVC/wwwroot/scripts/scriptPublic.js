
$(document).ready(function () {

    /********************** ImageEffectck ************************************/
    $(this).ImageEffectck({});

    /**************************************************************************/
    $(window).scroll(function () {
        if ((($(window).scrollTop()) > 50)) {
            $('header').addClass('header-fixed');
        } else {
            $('header').removeClass('header-fixed');
        }
    });

    /**************************************************************************************/
    /* Back To Top Button */
    $('#button-to-top').hide();
    $('#button-to-top').click(function (e) {
        e.preventDefault();
        $('body,html').animate({ scrollTop: 0 }, 600);
    });
    /* Back To Top Button */
    $(window).scroll(function () { enableBackToTop(); });
    enableBackToTop();

    function enableBackToTop() {
        if ((($(window).scrollTop()) > 80)) {
            $('#button-to-top').fadeIn(300);


        } else {
            $('#button-to-top').fadeOut(300);
        }
    }

    /****************************** start menu *********************/
    $('#ToggleMenu').click(function () {
        $(this).toggleClass('active');
        $('#OverlayMenu').toggleClass('open');
    });
    /****************************** end menu *********************/

});