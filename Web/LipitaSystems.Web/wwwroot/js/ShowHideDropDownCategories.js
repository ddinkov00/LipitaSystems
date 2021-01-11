﻿(function ($) {
    "use strict";

    document.querySelector('#showDropDown').addEventListener('click', DropDown);

    function DropDown(e) {
        const ul = e.target.parentElement.parentElement.querySelector('ul');
        if (ul.hidden == true) {
            ul.hidden = false;
            e.target.classList.remove("icofont-arrow-down");
            e.target.classList.add("icofont-arrow-up");
        }
        else {
            ul.hidden = true;
            e.target.classList.remove("icofont-arrow-up");
            e.target.classList.add("icofont-arrow-down");
        }
    }

})(jQuery);