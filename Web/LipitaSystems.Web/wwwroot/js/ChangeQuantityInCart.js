(function ($) {
    "use strict";

    $(".changeSum").on("click", function (e) {
        const quantity = Number(this.parentNode.querySelector('input[type=number]').value);
        const id = this.parentNode.parentNode.parentNode.querySelector('#productId').textContent;
        const priceField = this.parentNode.parentNode.parentNode.parentNode.querySelector('#summary');
        const price = Number(priceField.getAttribute('title'));
        priceField.textContent = "Цена: " + (Number(price) * Number(quantity)).toFixed(2) + " лв.";
        const arrPrice = document.getElementById(id).querySelector('span').textContent.split(' ');
        const oldQuantity = Number(arrPrice[3]);
        arrPrice[3] = Number(quantity);
        document.getElementById(id).querySelector('span').textContent = arrPrice.join(' ');
        const totalSum = document.querySelector('#totalSumField');
        const totalPrice = Number(totalSum.textContent.split(' ')[0]) - oldQuantity * price + quantity * price;
        totalSum.textContent = totalPrice.toFixed(2) + " лв."
    });

})(jQuery);