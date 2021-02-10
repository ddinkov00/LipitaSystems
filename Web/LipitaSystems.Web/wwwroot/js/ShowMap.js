(function ($) {
    "use strict";

    document.querySelector('#plovidvShow').addEventListener('click', showPld);
    document.querySelector('#sandaskiShow').addEventListener('click', showSand);
    const pldmap = document.querySelector('#plovdivMap');
    const sandMap = document.querySelector('#sandanskiMap');
    function showPld() {
        pldmap.removeAttribute('hidden');
        sandMap.setAttribute('hidden', 'true')
    }

    function showSand() {
        sandMap.removeAttribute('hidden');
        pldmap.setAttribute('hidden', 'true')
    }

})(jQuery);