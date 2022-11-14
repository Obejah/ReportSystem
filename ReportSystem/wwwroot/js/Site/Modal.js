//trigger deletemodal
function deletemodal() {
    let myModal = new bootstrap.Modal(document.getElementById('myModal'), {});
    myModal.show();
}

//fill deletemodal
$(document).ready(function () {

    // You'll probably won't to make .modal-click more specific
    $('.modal-click').on('click', function () {

        const name = $(this).attr('modal-name');
        const value = $(this).attr('modal-value');
        const form = $(this).attr('modal-form');
        const id = $(this).attr('modal-route-id');
        const href = $(this).attr('modal-href');
        const hiddenInput = $('.modal input#hidden');
        const modalForm = $('.modal form#form');

        modalForm.attr('action', window.location.origin + window.location.pathname + ((id ?? "").length > 0 ? "?id=" + id + "&" : "") + ((form ?? "").length > 0 ? "?handler=" + form : ""))

        $('.modal button#button').attr('formaction', href);

        hiddenInput.attr('name', name)
        hiddenInput.attr('value', value)

        let myModal = new bootstrap.Modal(document.getElementById('myModal'), {});
        myModal.show();

    });

});
