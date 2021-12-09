const ROUTE = {
    CART: {
        ADD: '/Cart/AddProductToCart',
        REDUCE: '/Cart/ReduceProductFromCart',
        REMOVE: '/Cart/RemoveProductFromCart',
        REMOVE_ALL: '/Cart/RemoveAllProductFromCart',
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
    let quantityProductCart = document.getElementById(ID_ELEMENT.QUANTITY_PRODUCT_CART);

    $.ajax({
        url: ROUTE.CART.ADD,
        type: 'post',
        contentType: "application/x-www-form-urlencoded",
        data: formData,
        success: function (data) {
            quantityProductCart.innerHTML = data;
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
//#endregion