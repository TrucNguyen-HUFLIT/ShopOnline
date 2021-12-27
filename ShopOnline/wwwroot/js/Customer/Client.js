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

//#region Mobile Menu Btn
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

//#region Auto hide paging
let paging = document.getElementById("products-paging");
let errorpage = document.getElementById("error__page");
if ($("#main__products-items").children().length == 0) {
    paging.style.display = "none";
    errorpage.style.display = "block";
} else {
    paging.style.display = "block";
    errorpage.style.display = "none";
}
//#endregion

//#region format Quantity
function quantityFormat(input) {//returns (###) ###-####
    input = input.replace(/\D/g, '').substring(0, 2); //Strip everything but 1st 10 digits
    let size = input.length;
    if (size > 2) { input = input.slice(0, 2) }

    while (input > 50) {
        input--;
    }

    while (input < 1) {
        input++;
    }

    return input;
}
//#enregion

//#region change Address
function changeAddress() {
    const newAddress = document.getElementById('newAddress').value.trim();
    if (newAddress && newAddress !== null && newAddress !== "") {
        document.getElementById('currentAddress').innerHTML = newAddress;
    }
}
//#enregion