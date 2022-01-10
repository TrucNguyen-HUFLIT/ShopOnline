function toast(message, isSuccessfully) {
    let toast;
    let toastMessage;

    if (isSuccessfully) {
        toast = document.getElementById("toastSuccessfully");
        toastMessage = document.getElementById("toastSuccessfully--message");
    } else {
        toast = document.getElementById("toastFailed");
        toastMessage = document.getElementById("toastFailed--message");
    }

    toastMessage.innerHTML = message;
    // Add the "show" class to DIV
    toast.className = "show";

    // After 3 seconds, remove the show class from DIV
    setTimeout(function () { toast.className = toast.className.replace("show", ""); }, 4000);
}

//#region Create in Create View
function CreateReviewDetail(e) {
    e.preventDefault();

    let form = $('#create-review-detail-form').serializeArray();

    $.ajax({
        url: '/client/createReviewDetail',
        async: false,
        type: 'post',
        contentType: "application/x-www-form-urlencoded",
        data: form,
        success: function () {
            toast("Created review successfully", true);
            setTimeout(() => { }, 2000);
        },
        error: function (XMLHttpRequest) {
            let msg = XMLHttpRequest.responseJSON ? XMLHttpRequest.responseJSON : XMLHttpRequest.responseText;
            toast("Do not have permission to review, please login!", false)
            setTimeout(() => console.log(msg), 2000);
        }
    });
};
//#endregion