const ROUTE = {
    ACCOUNT: {
        LOGIN: '/account/login',
        REGISTER: '/account/register',
        RESET_PASSWORD: '/account/resetpassword',
    },
    PROFILE: '/profile/updatedetail',
}

const FORM_ID = {
    LOGIN: '#login-form',
    REGISTER: '#register-form',
    RESET_PASSWORD: '#reset-password-form',
}

function innerHTMLMsg(msg, EleErrorMsg) {
    if (typeof msg === 'string') {
        const msgLowerCase = msg.replace(/\s/g, '').toLowerCase();

        Object.entries(EleErrorMsg).forEach(([key, value]) => {
            const element = document.getElementById(value);
            element.style.display = 'none';
            if (msgLowerCase.includes(key.toLowerCase())) {
                element.innerHTML = msg;
                element.style.display = 'block';
            }
        });
    } else {
        Object.entries(EleErrorMsg).forEach(([key, value]) => {
            const element = document.getElementById(value);
            const mess = msg[key];
            element.style.display = 'none';
            if (mess) {
                element.innerHTML = mess;
                element.style.display = 'block';
            }
        });
    }
}

//#region Login

function login() {
    let formData = $(FORM_ID.LOGIN).serializeArray();

    $.ajax({
        url: ROUTE.ACCOUNT.LOGIN,
        type: 'post',
        contentType: "application/x-www-form-urlencoded",
        data: formData,
        success: function () {
            window.location.replace(ROUTE.PROFILE);
        },
        error: function (XMLHttpRequest) {
            const EleErrorMsgLogin = {
                Email: 'email_msg',
                Password: 'password_msg',
            }

            let msg = XMLHttpRequest.responseJSON ? XMLHttpRequest.responseJSON : XMLHttpRequest.responseText;

            innerHTMLMsg(msg, EleErrorMsgLogin);
        },
    });
}

function enterLogin(e) {
    if (event.which == 13 || event.keyCode == 13) {
        login();
    }
}

//#endregion

//#region Register

function register() {
    let formData = $(FORM_ID.REGISTER).serializeArray();

    $.ajax({
        url: ROUTE.ACCOUNT.REGISTER,
        type: 'post',
        contentType: "application/x-www-form-urlencoded",
        data: formData,
        success: function (data) {
            window.location.replace("/account/login");
        },
        error: function (XMLHttpRequest) {
            const EleErrorMsgRegister = {
                FullName: 'fullname_msg',
                Email: 'email_msg',
                PhoneNumber: 'phonenumber_msg',
                Password: 'password_msg',
                ConfirmPassword: 'confirmpassword_msg',
            }

            let msg = XMLHttpRequest.responseJSON ? XMLHttpRequest.responseJSON : XMLHttpRequest.responseText;

            innerHTMLMsg(msg, EleErrorMsgRegister);
        },
    });
}

//#endregion

//#region Reset Password

function resetPassword() {
    let formData = $(FORM_ID.RESET_PASSWORD).serializeArray();

    $.ajax({
        url: ROUTE.ACCOUNT.RESET_PASSWORD,
        type: 'post',
        contentType: "application/x-www-form-urlencoded",
        data: formData,
        success: function (data) {
            //Show mess reset success, please check your email
            window.location.replace("/account/login");
        },
        error: function (XMLHttpRequest) {
            const EleErrorMsgResetPassword = {
                Email: 'email_msg',
                PhoneNumber: 'phonenumber_msg',
            }
            let msg = XMLHttpRequest.responseJSON ? XMLHttpRequest.responseJSON : XMLHttpRequest.responseText;

            if (typeof msg === 'string') {
                const msgLowerCase = msg.replace(/\s/g, '').toLowerCase();
                const serverNotResponse = 'notrespond';
                if (msgLowerCase.includes(serverNotResponse)) {
                    const element = document.getElementById(EleErrorMsgResetPassword.PhoneNumber);
                    element.innerHTML = msg;
                    element.style.display = 'block';
                }
            }

            innerHTMLMsg(msg, EleErrorMsgResetPassword);
        },
    });
}

//#endregion