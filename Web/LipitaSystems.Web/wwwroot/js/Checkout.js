(function ($) {
    "use strict";

    $("#toCheckout").on("click", function (e) {

        const items = this.parentNode.parentNode.querySelectorAll('ul li');
        const discountCode = document.querySelector('#discountCodeInput').value;
        console.log(discountCode);
        this.parentNode.querySelector('input').setAttribute('value', discountCode);
        let newCookie = "";
        for (let i = 0; i < items.length - 1; i++) {
            let id = items[i].getAttribute('id');
            let textContent = items[i].textContent.trim();
            let quantity = textContent[textContent.length - 1];
            if (newCookie.length == 0) {
                newCookie += id + "_" + quantity;
            }
            else if (newCookie.length > 0) {
                newCookie += "_" + id + "_" + quantity;
            }
        }

        setCookie("cartProducts", newCookie, 3);


    });

    function setCookie(cname, cvalue, exhours) {
        var d = new Date();
        d.setTime(d.getTime() + (exhours * 60 * 60 * 1000));
        var expires = "expires=" + d.toUTCString();
        document.cookie = cname + "=" + cvalue + ";" + expires + ";path=/Shop";
    }
})(jQuery);