// Automatic Slider
var counter = 1;
setInterval(function () {
    document.getElementById('radio' + counter).checked = true;
    counter++;
    if (counter > 4) {
        counter = 1;
    }
}, 5000);


// Scroll to top btn
const btnScrollToTop = document.querySelector("#btn-scroll-to-top");
window.onscroll = function () { scrollFunction() };

function scrollFunction() {
    if (document.body.scrollTop > 200 || document.documentElement.scrollTop > 100) {
        btnScrollToTop.style.display = "block";
    } else {
        btnScrollToTop.style.display = "none";
    }
}
// When the user clicks on the button, scroll to the top of the document
function topFunction() {
    document.body.scrollTop = 0;
    document.documentElement.scrollTop = 0;
}


// Update Quantity
const resultOfQuantity = document.querySelector('#resultOfQuantity');
var defaultQuan = 1;
function upDateQuantity(button) {
    if (button == downQuantity) {
        if (defaultQuan > 1) {
            defaultQuan -= 1;
        }
    }
    else if (button == upQuantity) {
        defaultQuan += 1;
    }
    resultOfQuantity.innerHTML = defaultQuan;
}

// Slide show details product
function currentDiv(n) {
    showDivs(slideIndex = n);
}

function showDivs(n) {
    var i;
    var x = document.getElementsByClassName("main__slides-products");
    var dots = document.getElementsByClassName("main__slides-products-picker");
    if (n > x.length) { slideIndex = 1 }
    if (n < 1) { slideIndex = x.length }
    for (i = 0; i < x.length; i++) {
        x[i].style.display = "none";
    }
    for (i = 0; i < dots.length; i++) {
        dots[i].className = dots[i].className.replace(" demo-off", "");
    }
    x[slideIndex - 1].style.display = "block";
    dots[slideIndex - 1].className += " demo-off";
}


// Mobile menu button
function mobileMenuBtn() {
    var x = document.getElementById("header__navbar-items");
    if (x.style.display === "block") {
        x.style.display = "none";
    } else {
        x.style.display = "block";
    }
}

// Show more category
function showMoreCateBtn() {
    var x = document.getElementById("main__products-filters");
    if (x.style.display === "block") {
        x.style.display = "none";
    } else {
        x.style.display = "block";
    }
}