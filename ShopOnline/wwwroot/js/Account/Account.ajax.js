//#region Login
function login() {
    let form = $('#login-form').serializeArray();

    const ROLE =
    {
        CUSTOMER: 'customer',
        SHIPPER: 'shipper',
        ADMIN: 'admin',
        STAFF: 'staff',
    };

    $.ajax({
        url: '/account/login',
        type: 'post',
        contentType: "application/x-www-form-urlencoded",
        data: form,
        success: function (data) {
            const role = data.toLowerCase();

            switch (role) {
                case ROLE.CUSTOMER:
                    window.location.replace("/client/home");
                    break;
                case ROLE.SHIPPER:
                    window.location.replace("/shipper/home");
                    break;
                default:
                    window.location.replace("/staff/index");
                    break;
            }
        },
        error: function (XMLHttpRequest) {
            const idEmailMsg = 'email_msg';
            const idPasswordMsg = 'password_msg';
            const emailEle = document.getElementById(idEmailMsg);
            const passwordEle = document.getElementById(idPasswordMsg);
            const msg = XMLHttpRequest.responseJSON ? XMLHttpRequest.responseJSON.Email + `|${XMLHttpRequest.responseJSON.Password}` : XMLHttpRequest.responseText;
            const msgLower = msg.toLowerCase();
            const isErrorEmail = msgLower.includes('email');
            const isErrorPassword = msgLower.includes('password');

            emailEle.style.display = 'none';
            passwordEle.style.display = 'none';

            if (isErrorEmail && isErrorPassword) {
                let mess = msg.split('|');
                emailEle.style.display = 'block';
                emailEle.innerHTML = mess[0];
                passwordEle.style.display = 'block';
                passwordEle.innerHTML = mess[1];
            } else if (isErrorEmail) {
                emailEle.style.display = 'block';
                emailEle.innerHTML = msg;
            } else {
                passwordEle.style.display = 'block';
                passwordEle.innerHTML = msg;
            }
        },
    });
}

function enterLogin(e) {
    if (event.which == 13 || event.keyCode == 13) {
        login();
    }
}
//#endregion