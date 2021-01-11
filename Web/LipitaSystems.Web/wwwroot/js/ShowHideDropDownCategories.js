(function ($) {
    "use strict";

    $('.showDropDown').on('click', function () {
        if ($(this).parent().parent().find("ul").eq(0).is(":visible")) {

            $(this).parent().parent().find("ul").eq(0).hide(500);
            $(this).removeClass("icofont-arrow-up");
            $(this).addClass("icofont-arrow-down");
        }
        else {
            $(this).parent().parent().find("ul").eq(0).show(500);
            $(this).removeClass("icofont-arrow-down");
            $(this).addClass("icofont-arrow-up");
        }
    });

})(jQuery);