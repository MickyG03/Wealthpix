function getDate() {
        'use strict';
        var date = new Date();

        var options = {
            weekday: 'long',
            month: 'long',
            day: 'numeric',
            year: 'numeric',
            hour: 'numeric',
            minute: 'numeric',
            second: 'numeric',
            hour12: true
        };

        var showDate = date.toLocaleDateString('en-US', options);

        document.getElementById('date').textContent = showDate;


        requestAnimationFrame(getDate);
}
getDate();


jQuery(function($) {
    $(window).on('load', function() {
        $('.loading').delay(2000).fadeOut('slow', function() {
            $(this).remove();
        });
        setTimeout(function() {
            $('.landing').addClass('loaded');
            $('body').addClass('loaded');
        }, 2000);
    });

    var one = document.querySelector('.one');
    var two = document.querySelector('.two');
    var three = document.querySelector('.three');
    var delay = 2500;

    function animation() {
        setTimeout(function() {
            one.style.top = '50%';
        }, delay);
        setTimeout(function() {
            one.style.top = '100%';
        }, delay * 5);

        setTimeout(function() {
            two.style.top = '50%';
        }, delay * 6);
        setTimeout(function() {
            two.style.top = '100%';
        }, delay * 11);

        setTimeout(function() {
            three.style.top = '50%';
        }, delay * 12);
        setTimeout(function() {
            three.style.top = '100%';
        }, delay * 17);
    }
    animation();
    setInterval(animation, delay * 18);

    $(window).on('scroll', function() {
        $('.hideme').each(function(i) {
            var bottom_of_object = $(this).offset().top + $(this).outerHeight();
            var bottom_of_window = $(window).scrollTop() + $(window).height();
            if (bottom_of_window > bottom_of_object) {
                $(this).animate({
                    'opacity': '1'
                }, 1250);
            }
        });
    });

    $(".mouseScroll").click(function() {
        $('html, body').animate({
            scrollTop: $(".about").offset().top - 150
        }, 800);
    });

    $('.button_container').click(function() {
        $(this).toggleClass('active');
        $('.overlay').toggleClass('open');
        $('body').toggleClass('active');
    });
});


