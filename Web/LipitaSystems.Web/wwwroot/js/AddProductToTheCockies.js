(function ($) {
    "use strict";

    document.querySelector('#shoppingCartBtn').addEventListener('click', addToCookie)

    function addToCookie() {
        const productId = document.querySelector('#productId').textContent;
        const prodructQuantity = document.querySelector('#productQuantity').value;
        if (getCookie("cartProducts") != "") {
            let cookieValue = getCookie("cartProducts");
            setCookie("cartProducts", cookieValue + "_" + productId + "_" + prodructQuantity, 3)
        }
        else {
            setCookie("cartProducts", productId + "_" + prodructQuantity, 3);
        }
    }

    function getCookie(cname) {
        var name = cname + "=";
        var decodedCookie = decodeURIComponent(document.cookie);
        var ca = decodedCookie.split(';');
        for (var i = 0; i < ca.length; i++) {
            var c = ca[i];
            while (c.charAt(0) == ' ') {
                c = c.substring(1);
            }
            if (c.indexOf(name) == 0) {
                return c.substring(name.length, c.length);
            }
        }
        return "";
    }

    function setCookie(cname, cvalue, exhours) {
        var d = new Date();
        d.setTime(d.getTime() + (exhours * 60 * 60 * 1000));
        var expires = "expires=" + d.toUTCString();
        document.cookie = cname + "=" + cvalue + ";" + expires + ";path=/Shop";
    }
})(jQuery);