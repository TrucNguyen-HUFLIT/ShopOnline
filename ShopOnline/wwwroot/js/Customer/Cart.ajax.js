const ROUTE_CART = {
    CART: {
        ADD: '/Cart/AddProductToCart',
        REDUCE: '/Cart/ReduceProductFromCart',
        REMOVE: '/Cart/RemoveProductFromCart',
        REMOVE_ALL: '/Cart/RemoveAllProductFromCart',
        PRODUCT_CART: '/Cart/ProductCart',
        DIGITAL_PAYMENT: '/Cart/DigitalPayment',
        E_WALLET: '/Cart/EWallet',
    },
}

const ID_ELEMENT = {
    QUANTITY_PRODUCT_CART: 'quantity-product-cart',
}

const FORM_ID = {
    ADD_TO_CART: '#add-to-cart-form',
    REGISTER: '#register-form',
    RESET_PASSWORD: '#reset-password-form',
}

//#region Add to cart
function addToCart() {
    let formData = $(FORM_ID.ADD_TO_CART).serializeArray();

    $.ajax({
        url: ROUTE_CART.CART.ADD,
        type: 'post',
        contentType: "application/x-www-form-urlencoded",
        data: formData,
        success: function () {
            window.location.reload();
        },
        error: function (XMLHttpRequest) {
            //const EleErrorMsgLogin = {
            //    Email: 'email_msg',
            //    Password: 'password_msg',
            //}

            //let msg = XMLHttpRequest.responseJSON ? XMLHttpRequest.responseJSON : XMLHttpRequest.responseText;

            //innerHTMLMsg(msg, EleErrorMsgLogin);
        },
    });
}

function addMoreToCart(id) {
    const formData = {
        idProduct: id,
        quantity: 1
    }

    $.ajax({
        url: ROUTE_CART.CART.ADD,
        type: 'post',
        contentType: "application/x-www-form-urlencoded",
        data: formData,
        success: function () {
            window.location.reload();
        },
        error: function (XMLHttpRequest) {

        },
    });
}
//#endregion

//#region Remove from cart
function reduceFromCart(id) {
    const formData = {
        idProduct: id,
        quantity: 1
    }

    $.ajax({
        url: ROUTE_CART.CART.REDUCE,
        type: 'post',
        contentType: "application/x-www-form-urlencoded",
        data: formData,
        success: function () {
            window.location.reload();
        },
        error: function (XMLHttpRequest) {

        },
    });
}
//#endregion

//#region Remove from cart
function removeFromCart(id) {
    const formData = {
        idProduct: id
    }

    $.ajax({
        url: ROUTE_CART.CART.REMOVE,
        type: 'post',
        contentType: "application/x-www-form-urlencoded",
        data: formData,
        success: function () {
            window.location.reload();
        },
        error: function (XMLHttpRequest) {

        },
    });
}
//#endregion

//#region order
function order() {
    const PAYMENT_METHODS = {
        SHIP_COD: 'payShipCod',
        E_WALLET: 'payEWallet',
        BANK_TRANSFER: 'payBankTransfer'
    }
    const paymentMethodSelected = document.querySelector('input[name="choosePaymentMethod"]:checked').getAttribute('id');

    switch (paymentMethodSelected) {
        case PAYMENT_METHODS.BANK_TRANSFER:
            window.location.replace(ROUTE_CART.CART.DIGITAL_PAYMENT);
            break;
        case PAYMENT_METHODS.E_WALLET:
            console.log(ROUTE_CART.CART.E_WALLET);
            break;
        default:
            console.log(PAYMENT_METHODS.SHIP_COD);
            break;
    }

    //$.ajax({
    //    url: ROUTE.CART.REMOVE,
    //    type: 'post',
    //    contentType: "application/x-www-form-urlencoded",
    //    data: formData,
    //    success: function () {
    //        window.location.reload();
    //    },
    //    error: function (XMLHttpRequest) {

    //    },
    //});
}
//#endregion