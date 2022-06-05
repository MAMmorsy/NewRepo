$(window).on('scroll', function () {
  if ($(window).scrollTop() > 50) {
    $('#navbar').addClass('active-header');
  } else {
    //remove the background property so it comes transparent again (defined in your css)
    $('#navbar').removeClass('active-header');
  }
});

$('.dropdown-menu').on('click', function (e) {
  e.stopPropagation();
});

// $(document).ready(function () {
//   $('.navbar .nav-item').click(function () {
//     $('.navbar .nav-item').removeClass('active');
//     $(this).addClass('active');
//   });
// });

// document.querySelectorAll('a[href^="#"]').forEach((anchor) => {
//   anchor.addEventListener('click', function (e) {
//     e.preventDefault();

//     document.querySelector(this.getAttribute('href')).scrollIntoView({
//       behavior: 'smooth',
//     });
//   });
// });

// $(document).ready(function () {
//   // Add smooth scrolling to all links
//   $('a').on('click', function (event) {
//     // Make sure this.hash has a value before overriding default behavior
//     if (this.hash !== '') {
//       // Prevent default anchor click behavior
//       event.preventDefault();

//       // Store hash
//       var hash = this.hash;

//       // Using jQuery's animate() method to add smooth page scroll
//       // The optional number (800) specifies the number of milliseconds it takes to scroll to the specified area
//       $('html, body').animate(
//         {
//           scrollTop: $(hash).offset().top,
//         },
//         1000,
//         function () {
//           // Add hash (#) to URL when done scrolling (default click behavior)
//           window.location.hash = hash;
//         }
//       );
//     } // End if
//   });
// });

$('a[class^=carousel-control-]').click(function (event) {
  event.preventDefault();
  console.log('ffdfd');
});

// scroll button
$(window).scroll(function () {
  if ($(this).scrollTop() > 200) {
    $('.back-to-top').fadeIn();
  } else {
    $('.back-to-top').fadeOut();
  }
});

// up arrow
$('.back-to-top').click(function () {
  $('html, body').animate(
    {
      scrollTop: 0,
    },

    1000
  );
  return false;
});

// $('.slick-carousel').slick({
//   slidesToShow: 6,
//   slidesToScroll: 1,
//   arrows: true,
//   dots: false,
//   infinite: true,
//   autoplay: true,
//   autoplaySpeed: 1000,
//   // centerMode: true,
//   speed: 600,
//   rtl: this.lang === 'ar',
//   responsive: [
//     {
//       breakpoint: 1024,
//       settings: {
//         slidesToShow: 3,
//         slidesToScroll: 1,
//       },
//     },
//     {
//       breakpoint: 480,
//       settings: {
//         slidesToShow: 1,
//         slidesToScroll: 1,
//       },
//     },
//   ],
// });

// const sections = document.querySelectorAll('section');
// const navLi = document.querySelectorAll('nav li');
// window.onscroll = () => {
//   var current = '';

//   sections.forEach((section) => {
//     const sectionTop = section.offsetTop;
//     if (pageYOffset >= sectionTop - 60) {
//       current = section.getAttribute('id');
//     }
//   });

//   navLi.forEach((li) => {
//     li.classList.remove('active');
//     if (li.classList.contains(current)) {
//       li.classList.add('active');
//     }
//   });
// };

$('.filter-button').click(function () {
  var value = $(this).attr('data-filter');

  if (value == 'all') {
    //$('.filter').removeClass('hidden');
    $('.filter').show('1000');
  } else {
    //            $('.filter[filter-item="'+value+'"]').removeClass('hidden');
    //            $(".filter").not('.filter[filter-item="'+value+'"]').addClass('hidden');
    $('.filter')
      .not('.' + value)
      .hide();
    $('.filter')
      .filter('.' + value)
      .show('2000');
  }
  $('.filter-button').removeClass('active');
  $(this).addClass('active');
});

// if ($('.filter-button').removeClass('active')) {
//   $(this).removeClass('active');
// }
// $(this).addClass('active');

$(document).ready(function () {
  $('.owl-carousel').owlCarousel({
    loop: true,
    margin: 10,
    nav: true,
    rtl: true,
    autoplay: true,
    autoplayTimeout: 3000,
    responsive: {
      0: {
        items: 1,
      },
      600: {
        items: 3,
      },
      1000: {
        items: 6,
      },
    },
  });
});
