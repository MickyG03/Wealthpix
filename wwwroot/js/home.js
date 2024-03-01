particlesJS("particles-js", {
    particles: {
      number: {
        value: 66,
        density: { enable: true, value_area: 1060.3387160299326 },
      },
      color: { value: "#bb6f6f" },
      shape: {
        type: "edge",
        stroke: { width: 0, color: "#000000" },
        polygon: { nb_sides: 5 },
        image: { src: "img/github.svg", width: 100, height: 100 },
      },
      opacity: {
        value: 0.3,
        random: false,
        anim: { enable: false, speed: 1, opacity_min: 0.1, sync: false },
      },
      size: {
        value: 3.0,
        random: true,
        anim: { enable: false, speed: 40, size_min: 0.1, sync: false },
      },
      line_linked: {
        enable: true,
        distance: 150,
        color: "#ffffff",
        opacity: 0.4,
        width: 1,
      },
      move: {
        enable: true,
        speed: 4,
        direction: "none",
        random: false,
        straight: false,
        out_mode: "out",
        bounce: false,
        attract: { enable: false, rotateX: 600, rotateY: 1200 },
      },
    },
    interactivity: {
      detect_on: "canvas",
      events: {
        onhover: { enable: false, mode: "grab" },
        onclick: { enable: true, mode: "repulse" },
        resize: true,
      },
      modes: {
        grab: { distance: 400, line_linked: { opacity: 1 } },
        bubble: { distance: 400, size: 40, duration: 2, opacity: 8, speed: 3 },
        repulse: { distance: 200, duration: 0.4 },
        push: { particles_nb: 4 },
        remove: { particles_nb: 2 },
      },
    },
    retina_detect: true,
  });


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


