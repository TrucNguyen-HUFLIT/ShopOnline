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
            window.location.replace("/staff/liststaff");
        },
        error: function (data) {
            console.log(data)
            var errors = data.responseText;
            if (errors == "Email already exists") {
                document.getElementById("emailAlready").innerHTML = data.responseText;

            }
            try {

                var objectValid = data.responseJSON;

                if (objectValid["StaffCreate.FullName"] != undefined)
                    document.getElementById("Err_FullName").innerHTML = objectValid["StaffCreate.FullName"];
                else
                    document.getElementById("Err_FullName").innerHTML = "";

                if (objectValid["StaffCreate.PhoneNumber"] != undefined)
                    document.getElementById("Err_PhoneNumber").innerHTML = objectValid["StaffCreate.PhoneNumber"];
                else
                    document.getElementById("Err_PhoneNumber").innerHTML = "";

                if (objectValid["StaffCreate.Email"] != undefined)
                    document.getElementById("Err_Email").innerHTML = objectValid["StaffCreate.Email"];
                else
                    document.getElementById("Err_Email").innerHTML = "";

                if (objectValid["StaffCreate.Password"] != undefined)
                    document.getElementById("Err_Password").innerHTML = objectValid["StaffCreate.Password"];
                else
                    document.getElementById("Err_Password").innerHTML = "";

                if (objectValid["StaffCreate.ConfirmPassword"] != undefined)
                    document.getElementById("Err_ConfirmPassword").innerHTML = objectValid["StaffCreate.ConfirmPassword"];
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
            window.location.replace(`/staff/updatestaff/${data}`);
        },
        error: function (data) {
            console.log(data)
            try {

                var objectValid = data.responseJSON;

                if (objectValid["StaffEdit.FullName"] != undefined)
                    document.getElementById("Err_FullName").innerHTML = objectValid["StaffEdit.FullName"];
                else
                    document.getElementById("Err_FullName").innerHTML = "";

                if (objectValid["StaffEdit.PhoneNumber"] != undefined)
                    document.getElementById("Err_PhoneNumber").innerHTML = objectValid["StaffEdit.PhoneNumber"];
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
            window.location.replace("/product/listbrand");
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
            window.location.replace(`/product/updatebrand/${data}`);
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
            window.location.replace("/product/listproducttype");
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
            window.location.replace(`/product/updateproducttype/${data}`);
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
            window.location.replace("/product/listProductDetail");
        },
        error: function (data) {
            console.log(data)
            var errors = data.responseText;
            if (errors == "Product Detail already exists") {
                document.getElementById("productDetailAlready").innerHTML = data.responseText;

            }
            try {

                var objectValid = data.responseJSON;

                if (objectValid["ProductDetailCreate.Name"] != undefined)
                    document.getElementById("Err_Name").innerHTML = objectValid["ProductDetailCreate.Name"];
                else
                    document.getElementById("Err_Name").innerHTML = "";

                if (objectValid["ProductDetailCreate.Price"] != undefined)
                    document.getElementById("Err_Price").innerHTML = objectValid["ProductDetailCreate.Price"];
                else
                    document.getElementById("Err_Price").innerHTML = "";

                if (objectValid["ProductDetailCreate.UploadPic1"] != undefined)
                    document.getElementById("Err_UploadPic1").innerHTML = objectValid["ProductDetailCreate.UploadPic1"];
                else
                    document.getElementById("Err_UploadPic1").innerHTML = "";

            }
            catch {
                document.getElementById("Err_Name").innerHTML = "";
                document.getElementById("Err_Price").innerHTML = "";
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
            window.location.replace(`/product/updateProductDetail/${data}`);
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

                if (objectValid["ProductDetailUpdate.UploadPic1"] != undefined)
                    document.getElementById("Err_UploadPic1").innerHTML = objectValid["ProductDetailUpdate.UploadPic1"];
                else
                    document.getElementById("Err_UploadPic1").innerHTML = "";

            }
            catch {
                document.getElementById("Err_Name").innerHTML = "";
                document.getElementById("Err_Price").innerHTML = "";
                document.getElementById("Err_UploadPic1").innerHTML = "";
            }
        },
    });
});