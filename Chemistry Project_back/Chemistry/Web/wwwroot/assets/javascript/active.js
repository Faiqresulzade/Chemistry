const scrollBtn = document.querySelector(".scroll-btn");
let hamburger = document.querySelector(".hamburger");
let sidebar = document.querySelector(".sidebar-nav");
let overlay = document.querySelector(".overlay-sidebar");


window.addEventListener('scroll', function () {
    const header = document.querySelector('.header');
    header.classList.toggle('sticky', window.scrollY > 0);
})



function refreshtop() {
    if (document.documentElement.scrollTop <= 100) {
        scrollBtn.style.display = "none";
    } else {
        scrollBtn.style.display = "block";
    }
};
refreshtop();
scrollBtn.addEventListener("click", () => {
    document.body.scrollTop = 0;
    document.documentElement.scrollTop = 0;
});

document.addEventListener("scroll", (e) => {
    refreshtop();
});





hamburger.addEventListener("click", function () {
    sidebar.classList.toggle("active-nav");
    if (overlay.style.display == "block") {
        overlay.style.display = "none";
    }
    else {
        overlay.style.display = "block";
    }
});

overlay.addEventListener("click", function () {
    sidebar.classList.remove("active-nav");
    overlay.style.display = "none";
})





