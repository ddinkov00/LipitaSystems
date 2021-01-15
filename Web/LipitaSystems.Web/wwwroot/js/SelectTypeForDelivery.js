(function ($) {
    "use strict";

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

    })
})(jQuery);