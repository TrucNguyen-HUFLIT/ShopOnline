//#region Create in Create View
function CreateReviewDetail() {
    let form = $('#create-review-detail-form').serializeArray();

    $.ajax({
        url: '/client/createReviewDetail',
        type: 'post',
        contentType: "application/x-www-form-urlencoded",
        data: form,
        success: function () {
            location.reload(true);
        },
        error: function (data, a) {
            console.log(data);
        },
    });
};
//#endregion