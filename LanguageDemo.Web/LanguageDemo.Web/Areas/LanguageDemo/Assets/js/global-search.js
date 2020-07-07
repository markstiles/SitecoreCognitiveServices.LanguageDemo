jQuery.noConflict();

//nav
function changeCss() {
    var bodyElement = document.querySelector("body");
    var navElement = document.querySelector("header");
    
    if (this.scrollY > 140)
        jQuery("header").addClass("hover");
    else
        jQuery("header").removeClass("hover");
}
window.addEventListener("scroll", changeCss, false);

//global search
jQuery(document).ready(function () {
    var searchForm = "#global-search-form";
    var searchInput = "#global-search-text";

    jQuery(searchForm + ' input')
        .keypress(function (e) {
            if (e.which === 13) {
                var queryValue = jQuery(searchInput).val();
                var t = jQuery(searchForm).attr("action") + "?q=" + escape(queryValue) + "#search-top";
                window.location = t;

                return false;   
            }
    });
});