(function ($) {
    "use strict";

    document.querySelector('#minisQuanityProduct').addEventListener('click', changeTotalSum);
    document.querySelector('#plusQuanityProduct').addEventListener('click', changeTotalSum);

    const quantity = document.querySelector('#productQuantity');
    const totalSumField = document.querySelector('#totalSumField');
    const price = document.querySelector('#totalSumField').textContent.split(' ')[0];
    function changeTotalSum() {
        totalSumField.textContent = (Number(price) * Number(quantity.value)).toFixed(2) + " лв.";
    }
})(jQuery);