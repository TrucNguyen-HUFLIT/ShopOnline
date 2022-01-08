//#region Select Search Product
var formatterCurrency = new Intl.NumberFormat();

$(document).ready(function () {
    $('select').selectize({
        sortField: 'text'
    });
});

let listProductDetail;
const productDetailSelected = document.getElementById('product-detail');

$('#product-detail').ready(function () {
    if (productDetailSelected) {
        $.ajax({
            url: '/product/ListProductDetail',
            type: 'get',
            contentType: "application/x-www-form-urlencoded",
            success: function (data) {
                listProductDetail = data;
                onSelectProductDetail();
            },
            error: function (XMLHttpRequest) {
                let msg = XMLHttpRequest.responseJSON ? XMLHttpRequest.responseJSON : XMLHttpRequest.responseText;
                console.log(msg);
            },
        });
    }
});

function onSelectProductDetail(){
    let dataSelected = listProductDetail.find(x => x.id === +productDetailSelected.value);
    document.getElementById('base-price').value = formatterCurrency.format(dataSelected.basePrice);
    document.getElementById('price').value = formatterCurrency.format(dataSelected.price);
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