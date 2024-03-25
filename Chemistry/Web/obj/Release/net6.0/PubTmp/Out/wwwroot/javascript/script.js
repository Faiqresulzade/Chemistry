// !loading


const loader = document.querySelector('#preloader');
window.addEventListener('load', () => {
  loader.style.display = 'none';
});
// !active filter

let btn = document.querySelector("#Alert");

setTimeout(function () {
    btn.classList.add("d-none")
}, 3000)


const sections = document.querySelectorAll('section');
const navActive = document.querySelectorAll('.nav-desktop li a');
//  ! filterButton
navActive.forEach((btn) => {
  btn.onclick = function () {
    //active
    navActive.forEach((li) => {
      li.className = '';
    });
    btn.className = 'active';
  };
});
// !navbar-active-link
window.onscroll = () => {
  sections.forEach((sec) => {
    let top = window.scrollY;
    let offset = sec.offsetTop - 150;
    let height = sec.offsetHeight;
    let id = sec.getAttribute('id');

    if (top >= offset && top < offset + height) {
      navActive.forEach((items) => {
        items.classList.remove('active');
        if (items.getAttribute('href') === '/index.html#' + id) {
          items.classList.add('active');
        }
      });
    }
  });
};
// !header
const closeIcon = document.querySelector('#close-icon');
let hamburger = document.querySelector('#hamburger');
let sidebar = document.querySelector('.sidebar-nav');
let overlay = document.querySelector('.overlay-sidebar');
const header = document.querySelector('.small-header');

window.addEventListener('scroll', () => {
  header.classList.toggle('site-header', window.scrollY > 250);
});

function toggleSidebar() {
  sidebar.classList.toggle('active-sidebar');
  hamburger.classList.remove('change');
  sidebar.classList.contains('active-sidebar')
    ? hamburger.classList.add('change')
    : hamburger.classList.remove('change');
  overlay.style.display = sidebar.classList.contains('active-sidebar')
    ? 'block'
    : 'none';
}
hamburger.addEventListener('click', toggleSidebar);
overlay.addEventListener('click', toggleSidebar);
closeIcon.addEventListener('click', toggleSidebar);
const scrollBtn = document.querySelector('.backToTop');

function refreshtop() {
  if (document.documentElement.scrollTop <= 600) {
    scrollBtn.style.display = 'none';
  } else {
    scrollBtn.style.display = 'block';
  }
}
refreshtop();
scrollBtn.addEventListener('click', () => {
  document.body.scrollTop = 0;
  document.documentElement.scrollTop = 0;
});

document.addEventListener('scroll', (e) => {
  refreshtop();
});

$(document).ready(function () {
  $('.slider-area').owlCarousel({
    loop: true,
    autoplay: true,
    autoplayTimeout: 3000,
    autoplayHoverPause: false,
    margin: 0,
    items: 1,
    responsiveClass: true,
    dots: false,
    nav: true,
  });
});

$('.graduate-wrapper').owlCarousel({
  loop: false,
  margin: 10,
  autoplay: true,
  autoplayTimeout: 3000,
  autoplayHoverPause: false,
  items: 5,
  responsiveClass: true,
  dots: false,
  nav: false,

  responsive: {
    0: {
      items: 1,
      dots: true,
    },
    600: {
      items: 2,
    },

    1000: {
      items: 3,
    },
    1200: {
      items: 4,
    },
  },
});

// !counter
const counters = document.querySelectorAll('.counter');
counters.forEach((counter) => {
  counter.innerHTML = '0';
  const uptadeCounter = () => {
    const target = +counter.getAttribute('data-target');
    const c = ++counter.innerHTML;
    const increment = target / 200;
    if (c < target) {
      counter.innerHTML = `${Math.ceil(c + increment)}`;
      setTimeout(uptadeCounter, 30);
    } else {
      counter.innerHTML = target;
    }
  };
  uptadeCounter();
});

$(document).ready(function () {
  $('.filterQuiz p button').on('click', function () {
    $('.filterQuiz p button').removeClass('filter-active');
    $(this).addClass('filter-active');
  });

  $('.filterQuiz p button').on('click', function () {
    var filterValue = $(this).data('filter');

    $('.quiz-card').fadeOut(200, function () {
      if (filterValue === 'all') {
        $('.quiz-card').fadeIn(500).css('transform', 'scale(1)');
      } else {
        $('.quiz-card[data-name="' + filterValue + '"]')
          .fadeIn(500)
          .css('transform', 'scale(1)');
      }
    });
  });
});

$(document).ready(function () {
  $('.filterLesson p button').on('click', function () {
    $('.filterLesson p button').removeClass('filter-active');
    $(this).addClass('filter-active');
  });

    $('.filterLesson p button').on('click', function () {
    var filterValue = $(this).data('filter');

    $('.all-lesson').fadeOut(0, function () {
      if (filterValue === 'all') {
        $('.all-lesson').fadeIn(0).css('transform', 'scale(1)');
      } else {
        $('.all-lesson[data-name="' + filterValue + '"]')
          .fadeIn(0)
          .css('transform', 'scale(1)');
      }
    });
  });
});


