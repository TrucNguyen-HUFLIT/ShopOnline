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

$("#create-staff-form").submit(function (e) {
    e.preventDefault();

    let formData = new FormData($(this)[0]);
    $.ajax({
        url: '/staff/createstaff',
        type: "post",
        async: false,
        cache: false,
        contentType: false,
        enctype: 'multipart/form-data',
        processData: false,
        data: formData,
        success: function () {
            toast("Created staff successfully", true);
            setTimeout(() => window.location.replace("/staff/liststaff"), 2000);
        },
        error: function (data) {
            console.log(data)
            var errors = data.responseText;
            if (errors == "Email already exists") {
                document.getElementById("emailAlready").innerHTML = data.responseText;

            }
            try {

                var objectValid = data.responseJSON;

                if (objectValid["staffCreate.FullName"] != undefined)
                    document.getElementById("Err_FullName").innerHTML = objectValid["staffCreate.FullName"];
                else
                    document.getElementById("Err_FullName").innerHTML = "";

                if (objectValid["staffCreate.PhoneNumber"] != undefined)
                    document.getElementById("Err_PhoneNumber").innerHTML = objectValid["staffCreate.PhoneNumber"];
                else
                    document.getElementById("Err_PhoneNumber").innerHTML = "";

                if (objectValid["staffCreate.Email"] != undefined)
                    document.getElementById("Err_Email").innerHTML = objectValid["staffCreate.Email"];
                else
                    document.getElementById("Err_Email").innerHTML = "";

                if (objectValid["staffCreate.Password"] != undefined)
                    document.getElementById("Err_Password").innerHTML = objectValid["staffCreate.Password"];
                else
                    document.getElementById("Err_Password").innerHTML = "";

                if (objectValid["staffCreate.ConfirmPassword"] != undefined)
                    document.getElementById("Err_ConfirmPassword").innerHTML = objectValid["staffCreate.ConfirmPassword"];
                else
                    document.getElementById("Err_ConfirmPassword").innerHTML = "";

            }
            catch {
                document.getElementById("Err_FullName").innerHTML = "";
                document.getElementById("Err_PhoneNumber").innerHTML = "";
                document.getElementById("Err_Email").innerHTML = "";
                document.getElementById("Err_Password").innerHTML = "";
                document.getElementById("Err_ConfirmPassword").innerHTML = "";
            }
        },
    });
});

$("#edit-staff-form").submit(function (e) {
    e.preventDefault();

    let formData = new FormData($(this)[0]);
    $.ajax({
        url: '/staff/updatestaff',
        type: "post",
        async: false,
        cache: false,
        contentType: false,
        enctype: 'multipart/form-data',
        processData: false,
        data: formData,
        success: function (data) {
            toast("Updated staff successfully", true);
            setTimeout(() => window.location.replace(`/staff/updatestaff/${data}`), 2000);
        },
        error: function (data) {
            console.log(data)
            try {

                var objectValid = data.responseJSON;

                if (objectValid["staffEdit.FullName"] != undefined)
                    document.getElementById("Err_FullName").innerHTML = objectValid["staffEdit.FullName"];
                else
                    document.getElementById("Err_FullName").innerHTML = "";

                if (objectValid["staffEdit.PhoneNumber"] != undefined)
                    document.getElementById("Err_PhoneNumber").innerHTML = objectValid["staffEdit.PhoneNumber"];
                else
                    document.getElementById("Err_PhoneNumber").innerHTML = "";

            }
            catch {
                document.getElementById("Err_FullName").innerHTML = "";
                document.getElementById("Err_PhoneNumber").innerHTML = "";
     
            }
        },
    });
});

$("#create-manager-form").submit(function (e) {
    e.preventDefault();

    let formData = new FormData($(this)[0]);
    $.ajax({
        url: '/staff/createmanager',
        type: "post",
        async: false,
        cache: false,
        contentType: false,
        enctype: 'multipart/form-data',
        processData: false,
        data: formData,
        success: function () {
            toast("Created manager successfully", true);
            setTimeout(() => window.location.replace("/staff/listmanager"), 2000);
        },
        error: function (data) {
            console.log(data)
            var errors = data.responseText;
            if (errors == "Email already exists") {
                document.getElementById("emailAlready").innerHTML = data.responseText;

            }
            try {

                var objectValid = data.responseJSON;

                if (objectValid["staffCreate.FullName"] != undefined)
                    document.getElementById("Err_FullName").innerHTML = objectValid["staffCreate.FullName"];
                else
                    document.getElementById("Err_FullName").innerHTML = "";

                if (objectValid["staffCreate.PhoneNumber"] != undefined)
                    document.getElementById("Err_PhoneNumber").innerHTML = objectValid["staffCreate.PhoneNumber"];
                else
                    document.getElementById("Err_PhoneNumber").innerHTML = "";

                if (objectValid["staffCreate.Email"] != undefined)
                    document.getElementById("Err_Email").innerHTML = objectValid["staffCreate.Email"];
                else
                    document.getElementById("Err_Email").innerHTML = "";

                if (objectValid["staffCreate.Password"] != undefined)
                    document.getElementById("Err_Password").innerHTML = objectValid["staffCreate.Password"];
                else
                    document.getElementById("Err_Password").innerHTML = "";

                if (objectValid["staffCreate.ConfirmPassword"] != undefined)
                    document.getElementById("Err_ConfirmPassword").innerHTML = objectValid["staffCreate.ConfirmPassword"];
                else
                    document.getElementById("Err_ConfirmPassword").innerHTML = "";

            }
            catch {
                document.getElementById("Err_FullName").innerHTML = "";
                document.getElementById("Err_PhoneNumber").innerHTML = "";
                document.getElementById("Err_Email").innerHTML = "";
                document.getElementById("Err_Password").innerHTML = "";
                document.getElementById("Err_ConfirmPassword").innerHTML = "";
            }
        },
    });
});

$("#edit-manager-form").submit(function (e) {
    e.preventDefault();

    let formData = new FormData($(this)[0]);
    $.ajax({
        url: '/staff/updatemanager',
        type: "post",
        async: false,
        cache: false,
        contentType: false,
        enctype: 'multipart/form-data',
        processData: false,
        data: formData,
        success: function (data) {
            toast("Updated manager successfully", true);
            setTimeout(() => window.location.replace(`/staff/updatemanager/${data}`), 2000);
        },
        error: function (data) {
            console.log(data)
            try {

                var objectValid = data.responseJSON;

                if (objectValid["staffEdit.FullName"] != undefined)
                    document.getElementById("Err_FullName").innerHTML = objectValid["staffEdit.FullName"];
                else
                    document.getElementById("Err_FullName").innerHTML = "";

                if (objectValid["staffEdit.PhoneNumber"] != undefined)
                    document.getElementById("Err_PhoneNumber").innerHTML = objectValid["staffEdit.PhoneNumber"];
                else
                    document.getElementById("Err_PhoneNumber").innerHTML = "";

            }
            catch {
                document.getElementById("Err_FullName").innerHTML = "";
                document.getElementById("Err_PhoneNumber").innerHTML = "";

            }
        },
    });
});

$("#create-shipper-form").submit(function (e) {
    e.preventDefault();

    let formData = new FormData($(this)[0]);
    $.ajax({
        url: '/staff/createshipper',
        type: "post",
        async: false,
        cache: false,
        contentType: false,
        enctype: 'multipart/form-data',
        processData: false,
        data: formData,
        success: function () {
            toast("Created shipper successfully", true);
            setTimeout(() => window.location.replace("/staff/listshipper"), 2000);
        },
        error: function (data) {
            console.log(data)
            var errors = data.responseText;
            if (errors == "Email already exists") {
                document.getElementById("emailAlready").innerHTML = data.responseText;

            }
            try {

                var objectValid = data.responseJSON;

                if (objectValid["staffCreate.FullName"] != undefined)
                    document.getElementById("Err_FullName").innerHTML = objectValid["staffCreate.FullName"];
                else
                    document.getElementById("Err_FullName").innerHTML = "";

                if (objectValid["staffCreate.PhoneNumber"] != undefined)
                    document.getElementById("Err_PhoneNumber").innerHTML = objectValid["staffCreate.PhoneNumber"];
                else
                    document.getElementById("Err_PhoneNumber").innerHTML = "";

                if (objectValid["staffCreate.Email"] != undefined)
                    document.getElementById("Err_Email").innerHTML = objectValid["staffCreate.Email"];
                else
                    document.getElementById("Err_Email").innerHTML = "";

                if (objectValid["staffCreate.Password"] != undefined)
                    document.getElementById("Err_Password").innerHTML = objectValid["staffCreate.Password"];
                else
                    document.getElementById("Err_Password").innerHTML = "";

                if (objectValid["staffCreate.ConfirmPassword"] != undefined)
                    document.getElementById("Err_ConfirmPassword").innerHTML = objectValid["staffCreate.ConfirmPassword"];
                else
                    document.getElementById("Err_ConfirmPassword").innerHTML = "";

            }
            catch {
                document.getElementById("Err_FullName").innerHTML = "";
                document.getElementById("Err_PhoneNumber").innerHTML = "";
                document.getElementById("Err_Email").innerHTML = "";
                document.getElementById("Err_Password").innerHTML = "";
                document.getElementById("Err_ConfirmPassword").innerHTML = "";
            }
        },
    });
});

$("#edit-shipper-form").submit(function (e) {
    e.preventDefault();

    let formData = new FormData($(this)[0]);
    $.ajax({
        url: '/staff/updateshipper',
        type: "post",
        async: false,
        cache: false,
        contentType: false,
        enctype: 'multipart/form-data',
        processData: false,
        data: formData,
        success: function (data) {
            toast("Updated shipper successfully", true);
            setTimeout(() => window.location.replace(`/staff/updateshipper/${data}`), 2000);
        },
        error: function (data) {
            console.log(data)
            try {

                var objectValid = data.responseJSON;

                if (objectValid["staffEdit.FullName"] != undefined)
                    document.getElementById("Err_FullName").innerHTML = objectValid["staffEdit.FullName"];
                else
                    document.getElementById("Err_FullName").innerHTML = "";

                if (objectValid["staffEdit.PhoneNumber"] != undefined)
                    document.getElementById("Err_PhoneNumber").innerHTML = objectValid["staffEdit.PhoneNumber"];
                else
                    document.getElementById("Err_PhoneNumber").innerHTML = "";

            }
            catch {
                document.getElementById("Err_FullName").innerHTML = "";
                document.getElementById("Err_PhoneNumber").innerHTML = "";

            }
        },
    });
});

$("#create-brand-form").submit(function (e) {
    e.preventDefault();

    let formData = new FormData($(this)[0]);
    $.ajax({
        url: '/product/createbrand',
        type: "post",
        async: false,
        cache: false,
        contentType: false,
        enctype: 'multipart/form-data',
        processData: false,
        data: formData,
        success: function () {
            toast("Created brand successfully", true);
            setTimeout(() => window.location.replace("/product/listbrand"), 2000);
        },
        error: function (data) {
            console.log(data)
            var errors = data.responseText;
            if (errors == "Brand already exists") {
                document.getElementById("brandAlready").innerHTML = data.responseText;

            }
            try {

                var objectValid = data.responseJSON;

                if (objectValid["brandCreate.BrandName"] != undefined)
                    document.getElementById("Err_BrandName").innerHTML = objectValid["brandCreate.BrandName"];
                else
                    document.getElementById("Err_BrandName").innerHTML = "";

            }
            catch {
                document.getElementById("Err_BrandName").innerHTML = "";

            }
        },
    });
});

$("#edit-brand-form").submit(function (e) {
    e.preventDefault();

    let formData = new FormData($(this)[0]);
    $.ajax({
        url: '/product/updatebrand',
        type: "post",
        async: false,
        cache: false,
        contentType: false,
        enctype: 'multipart/form-data',
        processData: false,
        data: formData,
        success: function (data) {
            toast("Updated brand successfully", true);
            setTimeout(() => window.location.replace(`/product/updatebrand/${data}`), 2000);
        },
        error: function (data) {
            console.log(data)
            var errors = data.responseText;
            if (errors == "Brand already exists") {
                document.getElementById("brandAlready").innerHTML = data.responseText;

            }
            try {

                var objectValid = data.responseJSON;

                if (objectValid["BrandInfor.BrandName"] != undefined)
                    document.getElementById("Err_BrandName").innerHTML = objectValid["BrandInfor.BrandName"];
                else
                    document.getElementById("Err_BrandName").innerHTML = "";

            }
            catch {
                document.getElementById("Err_BrandName").innerHTML = "";

            }
        },
    });
});

$("#create-product-type-form").submit(function (e) {
    e.preventDefault();

    let formData = new FormData($(this)[0]);
    $.ajax({
        url: '/product/createproducttype',
        type: "post",
        async: false,
        cache: false,
        contentType: false,
        enctype: 'multipart/form-data',
        processData: false,
        data: formData,
        success: function () {
            toast("Created product type successfully", true);
            setTimeout(() => window.location.replace("/product/listproducttype"), 2000);
        },
        error: function (data) {
            console.log(data)
            var errors = data.responseText;
            if (errors == "Product Type already exists") {
                document.getElementById("productTypeAlready").innerHTML = data.responseText;

            }
            try {

                var objectValid = data.responseJSON;

                if (objectValid["productType.Name"] != undefined)
                    document.getElementById("Err_ProductTypeName").innerHTML = objectValid["productType.Name"];
                else
                    document.getElementById("Err_ProductTypeName").innerHTML = "";

            }
            catch {
                document.getElementById("Err_ProductTypeName").innerHTML = "";

            }
        },
    });
});

$("#update-product-type-form").submit(function (e) {
    e.preventDefault();

    let formData = new FormData($(this)[0]);
    $.ajax({
        url: '/product/updateproducttype',
        type: "post",
        async: false,
        cache: false,
        contentType: false,
        enctype: 'multipart/form-data',
        processData: false,
        data: formData,
        success: function (data) {
            toast("Updated product type successfully", true);
            setTimeout(() => window.location.replace(`/product/updateproducttype/${data}`), 2000);
        },
        error: function (data) {
            console.log(data)
            var errors = data.responseText;
            if (errors == "Product Type already exists") {
                document.getElementById("productTypeAlready").innerHTML = data.responseText;

            }
            try {

                var objectValid = data.responseJSON;

                if (objectValid["productType.Name"] != undefined)
                    document.getElementById("Err_ProductTypeName").innerHTML = objectValid["productType.Name"];
                else
                    document.getElementById("Err_ProductTypeName").innerHTML = "";

            }
            catch {
                document.getElementById("Err_ProductTypeName").innerHTML = "";

            }
        },
    });
});

$("#profile-form").submit(function (e) {
    e.preventDefault();

    let formData = new FormData($(this)[0]);
    $.ajax({
        url: '/profile/updatedetail',
        type: "post",
        async: false,
        cache: false,
        contentType: false,
        enctype: 'multipart/form-data',
        processData: false,
        data: formData,
        success: function () {
            toast("Updated profile successfully", true);
            setTimeout(() => window.location.replace("/profile/updatedetail"), 2000);
        },
        error: function (data) {
            console.log(data)
            try {

                var objectValid = data.responseJSON;

                if (objectValid["staffEdit.FullName"] != undefined)
                    document.getElementById("Err_FullName").innerHTML = objectValid["staffEdit.FullName"];
                else
                    document.getElementById("Err_FullName").innerHTML = "";

                if (objectValid["staffEdit.PhoneNumber"] != undefined)
                    document.getElementById("Err_PhoneNumber").innerHTML = objectValid["staffEdit.PhoneNumber"];
                else
                    document.getElementById("Err_PhoneNumber").innerHTML = "";

            }
            catch {
                document.getElementById("Err_FullName").innerHTML = "";
                document.getElementById("Err_PhoneNumber").innerHTML = "";
            }
            toast("Updated profile failed", false);
            setTimeout(() => { }, 2000);
        },
    });
});

$("#create-product-detail-form").submit(function (e) {
    e.preventDefault();

    let formData = new FormData($(this)[0]);
    $.ajax({
        url: '/product/createProductDetail',
        type: "post",
        async: false,
        cache: false,
        contentType: false,
        enctype: 'multipart/form-data',
        processData: false,
        data: formData,
        success: function () {
            toast("Created product detail successfully", true);
            setTimeout(() => window.location.replace("/product/listProductDetail"), 2000);
        },
        error: function (data) {
            console.log(data)
            var errors = data.responseText;
            if (errors == "Product Detail already exists") {
                document.getElementById("productDetailAlready").innerHTML = data.responseText;

            }
            try {

                var objectValid = data.responseJSON;

                if (objectValid["productDetailCreate.Name"] != undefined)
                    document.getElementById("Err_Name").innerHTML = objectValid["productDetailCreate.Name"];
                else
                    document.getElementById("Err_Name").innerHTML = "";

                if (objectValid["productDetailCreate.Price"] != undefined)
                    document.getElementById("Err_Price").innerHTML = objectValid["productDetailCreate.Price"];
                else
                    document.getElementById("Err_Price").innerHTML = "";

                if (objectValid["productDetailCreate.BasePrice"] != undefined)
                    document.getElementById("Err_BasePrice").innerHTML = objectValid["productDetailCreate.BasePrice"];
                else
                    document.getElementById("Err_BasePrice").innerHTML = "";

                if (objectValid["productDetailCreate.Quantity"] != undefined)
                    document.getElementById("Err_Quantity").innerHTML = objectValid["productDetailCreate.Quantity"];
                else
                    document.getElementById("Err_Quantity").innerHTML = "";

                if (objectValid["productDetailCreate.Brand"] != undefined)
                    document.getElementById("Err_Brand").innerHTML = objectValid["productDetailCreate.Brand"];
                else
                    document.getElementById("Err_Brand").innerHTML = "";

                if (objectValid["productDetailCreate.UploadPic1"] != undefined)
                    document.getElementById("Err_UploadPic1").innerHTML = objectValid["productDetailCreate.UploadPic1"];
                else
                    document.getElementById("Err_UploadPic1").innerHTML = "";

            }
            catch {

                document.getElementById("Err_Name").innerHTML = "";
                document.getElementById("Err_Price").innerHTML = "";
                document.getElementById("Err_Quantity").innerHTML = "";
                document.getElementById("Err_BasePrice").innerHTML = "";
                document.getElementById("Err_Brand").innerHTML = "";
                document.getElementById("Err_UploadPic1").innerHTML = "";
            }
        },
    });
});

$("#update-product-detail-form").submit(function (e) {
    e.preventDefault();

    let formData = new FormData($(this)[0]);
    $.ajax({
        url: '/product/updateProductDetail',
        type: "post",
        async: false,
        cache: false,
        contentType: false,
        enctype: 'multipart/form-data',
        processData: false,
        data: formData,
        success: function (data) {
            toast("Updated product detail successfully", true);
            setTimeout(() => window.location.replace(`/product/updateProductDetail/${data}`), 2000);
        },
        error: function (data) {
            console.log(data)
            var errors = data.responseText;
            if (errors == "Product Detail already exists") {
                document.getElementById("productDetailAlready").innerHTML = data.responseText;

            }
            try {

                var objectValid = data.responseJSON;

                if (objectValid["ProductDetailUpdate.Name"] != undefined)
                    document.getElementById("Err_Name").innerHTML = objectValid["ProductDetailUpdate.Name"];
                else
                    document.getElementById("Err_Name").innerHTML = "";

                if (objectValid["ProductDetailUpdate.Price"] != undefined)
                    document.getElementById("Err_Price").innerHTML = objectValid["ProductDetailUpdate.Price"];
                else
                    document.getElementById("Err_Price").innerHTML = "";

                if (objectValid["ProductDetailUpdate.BasePrice"] != undefined)
                    document.getElementById("Err_BasePrice").innerHTML = objectValid["ProductDetailUpdate.BasePrice"];
                else
                    document.getElementById("Err_BasePrice").innerHTML = "";

                if (objectValid["ProductDetailUpdate.Quantity"] != undefined)
                    document.getElementById("Err_Quantity").innerHTML = objectValid["ProductDetailUpdate.Quantity"];
                else
                    document.getElementById("Err_Quantity").innerHTML = "";

                if (objectValid["ProductDetailUpdate.Brand"] != undefined)
                    document.getElementById("Err_Brand").innerHTML = objectValid["ProductDetailUpdate.Brand"];
                else
                    document.getElementById("Err_Brand").innerHTML = "";

                if (objectValid["ProductDetailUpdate.UploadPic1"] != undefined)
                    document.getElementById("Err_UploadPic1").innerHTML = objectValid["ProductDetailUpdate.UploadPic1"];
                else
                    document.getElementById("Err_UploadPic1").innerHTML = "";

            }
            catch {
                document.getElementById("Err_Name").innerHTML = "";
                document.getElementById("Err_Price").innerHTML = "";
                document.getElementById("Err_BasePrice").innerHTML = "";
                document.getElementById("Err_Quantity").innerHTML = "";
                document.getElementById("Err_Brand").innerHTML = "";
                document.getElementById("Err_UploadPic1").innerHTML = "";
            }
        },
    });
});

$("#create-product-form").submit(function (e) {
    e.preventDefault();

    let formData = $('#create-product-form').serializeArray();

    $.ajax({
        url: '/product/CreateProduct',
        type: "post",
        enctype: 'multipart/form-data',
        data: formData,
        success: function () {
            toast("Created product successfully", true);
            setTimeout(() => window.location.replace("/product/listproduct"), 2000);
        },
        error: function (data) {
            console.log(data)
            toast(data.responseText, false);
        },
    });
});

$("#update-product-form").submit(function (e) {
    e.preventDefault();

    let formData = $('#update-product-form').serializeArray();
    $.ajax({
        url: '/product/UpdateProduct',
        type: "post",
        enctype: 'multipart/form-data',
        data: formData,
        success: function (data) {
            toast("Created product successfully", true);
            setTimeout(() => window.location.replace(`/product/UpdateProduct/${data}`), 2000);
        },
        error: function (data) {
            console.log(data)
            toast(data.responseText, false);
        },
    });
});

function ApproveReview(id) {
    const formData = {
        id: id
    };
    $.ajax({
        url: '/review/approvereview',
        type: 'post',
        data: formData,
        success: function () {
            toast("Approve review successfully", true);
            setTimeout(() => window.location.reload("/review/listreview"), 50);
        },
        error: function () {
            notyfError.open({
                type: 'warning',
                message: 'Approve review failed'
            })
        }
    })
}

function RejectReview(id) {
    const formData = {
        id: id
    };
    $.ajax({
        url: '/review/rejectreview',
        type: 'post',
        data: formData,
        success: function () {
            toast("Approve review successfully", true);
            setTimeout(() => window.location.reload("/review/rejectreview"), 50);
        },
        error: function () {
            notyfError.open({
                type: 'warning',
                message: 'Reject review failed'
            })
        }
    })
}