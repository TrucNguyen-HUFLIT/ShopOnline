//#region Slide show
var counter = 1;
setInterval(function () {
    document.getElementById('radio' + counter).checked = true;
    counter++;
    if (counter > 4) {
        counter = 1;
    }
}, 5000);
//#endregion

//#region Scroll to top btn
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
// #endregion

//#region Update quantity
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
// #endregion

//#region Show detail divs
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
//#endregion

//#region Mobile Menu
function mobileMenuBtn() {
    var x = document.getElementById("header__navbar-items");
    if (x.style.display === "block") {
        x.style.display = "none";
    } else {
        x.style.display = "block";
    }
}
// #endregion

//#region Show more category
function showMoreCateBtn() {
    var x = document.getElementById("main__products-filters");
    if (x.style.display === "block") {
        x.style.display = "none";
    } else {
        x.style.display = "block";
    }
}
// #endregion

//#region Bolder for current sort Price Products

$('#sortDecrease').ready(function () {
    let currentSort = document.getElementById('currentSort').value;
    currentSort = currentSort.split('+');
    if (currentSort !== "") {
        addClassToSortProductsPrice(currentSort[0]);
    }
});

function addClassToSortProductsPrice(sortIncrease) {
    if (sortIncrease === "True") {
        document.getElementById('sortIncrease').className = "checked-bold";
        document.getElementById('sortDecrease').className = "";
    } else {
        document.getElementById('sortDecrease').className = "checked-bold";
        document.getElementById('sortIncrease').className = "";
    }
}

//#endregion

// #region Show change address
function showChangeAddress() {
    let change = document.getElementById("btn-change-address")
    var x = document.getElementById("change-address");
    if (x.style.display === "flex") {
        x.style.display = "none";
        change.textContent = "Change"
    } else {
        x.style.display = "flex";
        change.textContent = "Cancel"
    }
}

// #endregion

// #region Selected payment method

// #endregion
