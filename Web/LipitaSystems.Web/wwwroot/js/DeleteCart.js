(function ($) {
    "use strict";

    $("#FinishBuying").on("click", function (e) {

        setCookie("cartProducts", "", 1);

    });

    function setCookie(cname, cvalue, exhours) {
        var d = new Date();
        d.setTime(d.getTime() + (exhours * 1000));
        var expires = "expires=" + d.toUTCString();
        document.cookie = cname + "=" + cvalue + ";" + expires + ";path=/";
    }
})(jQuery);