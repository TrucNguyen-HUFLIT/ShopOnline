const ROUTE_CART = {
    CART: {
        ADD: '/Cart/AddProductToCart',
        REDUCE: '/Cart/ReduceProductFromCart',
        REMOVE: '/Cart/RemoveProductFromCart',
        REMOVE_ALL: '/Cart/RemoveAllProductFromCart',
        PRODUCT_CART: '/Cart/ProductCart',
        DIGITAL_PAYMENT: '/Cart/DigitalPayment?id={id}',
        SHIP_COD_PAYMENT: '/Cart/ShipCODPayment?id={id}',
        E_WALLET_PAYMENT: '/Cart/EWalletPayment',
        CHECK_OUT: '/Cart/CheckOut',
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
            let msg = XMLHttpRequest.responseJSON ? XMLHttpRequest.responseJSON : XMLHttpRequest.responseText;
            console.log(msg);
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
            let msg = XMLHttpRequest.responseJSON ? XMLHttpRequest.responseJSON : XMLHttpRequest.responseText;
            console.log(msg);
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
            let msg = XMLHttpRequest.responseJSON ? XMLHttpRequest.responseJSON : XMLHttpRequest.responseText;
            console.log(msg);
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
            let msg = XMLHttpRequest.responseJSON ? XMLHttpRequest.responseJSON : XMLHttpRequest.responseText;
            console.log(msg);
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
    const address = document.getElementById('currentAddress').innerHTML;
    const formData = {
        paymentMethod: paymentMethodSelected.substring(3),
        address: address
    };

    $.ajax({
        url: ROUTE_CART.CART.CHECK_OUT,
        type: 'post',
        contentType: "application/x-www-form-urlencoded",
        data: formData,
        success: function (data) {
            switch (paymentMethodSelected) {
                case PAYMENT_METHODS.BANK_TRANSFER:
                    window.location.replace(ROUTE_CART.CART.DIGITAL_PAYMENT.replace('{id}', data));
                    break;
                case PAYMENT_METHODS.E_WALLET:
                    window.location.replace(ROUTE_CART.CART.SHIP_COD_PAYMENT.replace('{id}', data));

                    //console.log(ROUTE_CART.CART.E_WALLET);
                    break;
                default:
                    window.location.replace(ROUTE_CART.CART.SHIP_COD_PAYMENT.replace('{id}', data));
                    break;
            }
        },
        error: function (XMLHttpRequest) {
            let msg = XMLHttpRequest.responseJSON ? XMLHttpRequest.responseJSON : XMLHttpRequest.responseText;
            console.log(msg);
        },
    });
}
//#endregion