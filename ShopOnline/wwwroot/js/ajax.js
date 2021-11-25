$("#create-staff-form").submit(function (e) {
    e.preventDefault();

    let formData = new FormData($(this)[0]);
    $.ajax({
        url: '/staff/create',
        type: "post",
        async: false,
        cache: false,
        contentType: false,
        enctype: 'multipart/form-data',
        processData: false,
        data: formData,
        success: function () {
            window.location.replace("/staff/index");
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
        url: '/staff/edit',
        type: "post",
        async: false,
        cache: false,
        contentType: false,
        enctype: 'multipart/form-data',
        processData: false,
        data: formData,
        success: function () {
            window.location.replace("/staff/edit");
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
        url: '/product/editbrand',
        type: "post",
        async: false,
        cache: false,
        contentType: false,
        enctype: 'multipart/form-data',
        processData: false,
        data: formData,
        success: function () {
            window.location.replace("/product/editbrand");
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