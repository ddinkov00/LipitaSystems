(function ($) {
    "use strict";

    document.querySelector('#PrivacyCheck').addEventListener('change', function () {
        const btn = document.querySelector('#FinishBuying');
        if (document.querySelector('#PrivacyCheck').checked) {
            btn.removeAttribute('disabled');
        }
        else {
            btn.setAttribute('disabled', 'true');
        }
    });

    $("#deliveryType").on("change", function (e) {
        const type = e.target.value;
        const speedyAddress = document.querySelector('#officeSpeedy');
        const homeAddress = document.querySelector('#addressHome');
        if (type == 'address') {
            speedyAddress.setAttribute('hidden', true);
            homeAddress.removeAttribute('hidden');

        }
        else if (type == 'office') {
            homeAddress.setAttribute('hidden', true);
            speedyAddress.removeAttribute('hidden');
        }

    });

    $('#speedyOfficeDropdown').on('change', function (e) {

        document.querySelector('#addressInput').value = $("#speedyOfficeDropdown option:selected").text();
    });

})(jQuery);