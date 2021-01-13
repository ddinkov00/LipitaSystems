﻿(function ($) {
    "use strict";

    $(".removeFromCart").on("click", function (e) {

        const id = this.parentNode.parentNode.parentNode.querySelector('#productId').textContent;
        const cookie = getCookie("cartProducts").split('_');
        let newCookie = "";
        for (let i = 0; i < cookie.length; i += 2) {
            if (cookie[i] != id && newCookie.length == 0) {
                newCookie += cookie[i] + "_" + cookie[i + 1];
            }
            else if (cookie[i] != id && newCookie.length > 0) {
                newCookie += "_" + cookie[i] + "_" + cookie[i + 1];
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

})(jQuery);