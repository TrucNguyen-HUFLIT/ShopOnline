const swalWithBootstrapButtons = Swal.mixin({
    customClass: {
        confirmButton: 'btn btn-success',
        cancelButton: 'btn btn-danger'
    },
    buttonsStyling: false
})

//SweetAlert

function ConfirmDelete(id, url) {
    swalWithBootstrapButtons.fire({
        title: 'Are you sure?',
        text: "You won't be able to revert this!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonText: 'Yes, delete it!',
        cancelButtonText: 'No, cancel!',
        reverseButtons: true
    }).then((result) => {
        if (result.isConfirmed) {
            const formData = {
                id: id
            };
            $.ajax({
                url: url,
                data: formData,
                success: function () {
                    toast("Delete successfully", true);
                    document.getElementById(`id-${id}`).remove();
                },
                error: function (XMLHttpRequest) {
                    toast(XMLHttpRequest.responseText, false);
                }
            })
        } else if (
            result.dismiss === Swal.DismissReason.cancel
        ) {
            swalWithBootstrapButtons.fire(
                'Cancelled',
                'Your data is safe :)',
                'error'
            )
        }
    })
};

