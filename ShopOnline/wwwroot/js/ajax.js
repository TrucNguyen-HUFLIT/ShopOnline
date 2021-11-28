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
            if (errors == " Email is already ") {
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
        success: function () {
            window.location.replace("/staff/updatestaff");
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
            window.location.replace("/product/listbrand");
        },
        error: function (data) {
            console.log(data)
            var errors = data.responseText;
            if (errors == " Brand is already ") {
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
        success: function () {
            window.location.replace("/product/updatebrand");
        },
        error: function (data) {
            console.log(data)
            var errors = data.responseText;
            if (errors == " Brand is already ") {
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
            if (errors == " Product Type is already ") {
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
        success: function () {
            window.location.replace("/product/updateproducttype");
        },
        error: function (data) {
            console.log(data)
            var errors = data.responseText;
            if (errors == " Product Type is already ") {
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
        url: '/staff/profile',
        type: "post",
        async: false,
        cache: false,
        contentType: false,
        enctype: 'multipart/form-data',
        processData: false,
        data: formData,
        success: function () {
            window.location.replace("/staff/profile");
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