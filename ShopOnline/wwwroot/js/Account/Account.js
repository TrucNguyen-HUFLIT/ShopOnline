// #region Check streng password
let parameters = {
    count: false,
    letters: false,
    numbers: false,
    special: false
}
let strengthBar = document.getElementById("strength-bar");
let msg = document.getElementById("msg");
let check = document.getElementById("check-confirm-pass")

function strengthChecker() {
    let password = document.getElementById("password").value;
    parameters.letters = (/[A-Za-z]+/.test(password)) ? true : false;
    parameters.numbers = (/[0-9]+/.test(password)) ? true : false;
    parameters.special = (/[!\"$%&/()=?@~`\\.\';:+=^*_-]+/.test(password)) ? true : false;
    parameters.count = (password.length > 7) ? true : false;

    if (password && password.length != 0) {
        strengthBar.style.display = "block";
    }
    else {
        strengthBar.style.display = "none";
        msg.textContent = ""
    }

    let barLength = Object.values(parameters).filter(value => value);

    console.log(Object.values(parameters), barLength);

    strengthBar.innerHTML = "";
    for (let i in barLength) {
        let span = document.createElement("span");
        span.classList.add("strength");
        strengthBar.appendChild(span);
    }

    let spanRef = document.getElementsByClassName("strength");
    for (let i = 0; i < spanRef.length; i++) {
        switch (spanRef.length - 1) {
            case 0:
                spanRef[i].style.background = "#ff3e36";
                msg.textContent = "Your password is very weak";
                msg.style.color = "#ff3e36";
                break;
            case 1:
                spanRef[i].style.background = "#ff691f";
                msg.textContent = "Your password is weak";
                msg.style.color = "#ff691f";
                break;
            case 2:
                spanRef[i].style.background = "#ffda36";
                msg.textContent = "Your password is good";
                msg.style.color = "#ffda36";
                break;
            case 3:
                spanRef[i].style.background = "#0be881";
                msg.textContent = "Your password is strong";
                msg.style.color = "#0be881";
                break;
        }
    }
}

function toggle() {
    let password = document.getElementById("password");
    let eye = document.getElementById("toggle");

    if (password.getAttribute("type") == "password") {
        password.setAttribute("type", "text");
        eye.style.color = "#444";
    }
    else {
        password.setAttribute("type", "password");
        eye.style.color = "#808080";
    }
}

function toggle1() {
    let confirmPassword = document.getElementById("confirm-password");
    let eye1 = document.getElementById("toggle1");

    if (confirmPassword.getAttribute("type") == "password") {
        confirmPassword.setAttribute("type", "text");
        eye1.style.color = "#444";
    }
    else {
        confirmPassword.setAttribute("type", "password");
        eye1.style.color = "#808080";
    }
}

// #endregion

// #region Check match password
function checkPassword() {
    let password = document.getElementById("password").value;
    let confirmPassword = document.getElementById("confirm-password").value;

    if (password == confirmPassword) {
        check.style.display = "block";
        check.textContent = "Confirm password match"
        check.style.color = "#0be881"
    }

    if (password != confirmPassword) {
        check.style.display = "block";
        check.textContent = "Confirm password didn't match"
        check.style.color = "#ff3e36"
    }
}
// #endregion