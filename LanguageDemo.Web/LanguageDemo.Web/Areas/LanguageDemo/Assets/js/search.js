jQuery.noConflict();

//search form
jQuery(document).ready(function () {
    //search
    var hi = "hi";
    var searchForm = ".search-form";
    var searchChat = searchForm + " .search-chat";
    var searchInput = searchForm + " .search-input";
    var formSubmit = searchForm + " .search-submit";
    var dialogState = searchForm + " .dialog-state";
    var dialogWrap = searchForm + " .dialog-wrap";
    var showClass = "show";
    var progressIndicator = ".progress-indicator";
    var resultFailure = ".result-failure";
    var resultWrap = ".result-wrap";
    var resultCount = ".result-count";
    var searchResults = ".search-results";
    var searchNoResults = ".search-no-results";
    var searchTop = "#search-top";
    
    jQuery(formSubmit).click(function (event) {
        event.preventDefault();

        var queryValue = jQuery(searchInput).val();
        if (queryValue === "")
            return;

        if (jQuery(dialogState).hasClass(showClass))
            GetDialogResult(queryValue);
        else
            window.location = window.location.pathname + "?q=" + escape(queryValue) + searchTop;
    });

    //sends chat text on 'enter-press' on the form
    jQuery(searchInput).keydown(function (e)
    {
        if (e.which !== 13)
            return;
        
        e.preventDefault();
        jQuery(formSubmit).click();
    });
    
    jQuery(dialogState + " .cancel").click(function (event) {
        event.preventDefault();

        jQuery(searchChat).html("");

        ClearDialogState(true);

        GetDialogResult("cancel");

        jQuery(searchInput).focus();
    });

    function GetDialogResult(queryValue) {
        var idValue = jQuery(searchForm + " #id").val();
        var dbValue = jQuery(searchForm + " #db").val();
        var langValue = jQuery(searchForm + " #language").val();

        jQuery(searchChat).css('opacity', '0');
        //jQuery(searchInput).attr("type", "text");

        ClearDialogState(false);
        jQuery(progressIndicator).show();
        
        jQuery.post(
            jQuery(searchForm + " form").attr("action"),
            {
                id: idValue,
                db: dbValue,
                language: langValue,
                query: queryValue
            }
        ).done(function (r) {
            jQuery(progressIndicator).hide();
            ProcessDialogResult(queryValue, r);
        });
    }

    function ProcessDialogResult(queryValue, dialogResult)
    {        
        alert('asdf');
        var hasResponse = dialogResult.Response !== undefined && dialogResult.Response !== null;
        var isOver = !hasResponse || (hasResponse && dialogResult.Response.Ended);
        var isQuit = dialogResult.Response.Intent.indexOf("quit") > -1;
        var isSearch = dialogResult.Response.Intent.indexOf("event search") > -1;
        
        if (isOver && isSearch)
            ProcessSearchResult(dialogResult.Items, dialogResult.TotalResults);

        if (dialogResult.Failed) {
            jQuery(resultFailure).show();
            return;
        }

        jQuery(resultWrap).show();
        
        if (hasResponse)
        {
            jQuery(searchChat).html(dialogResult.Response.Message);
            if (dialogResult.Response.Message.length > 0)
                jQuery(searchChat).css('opacity', '1');

            if (dialogResult.Response.Input !== null)
                HandleInput(dialogResult.Response.Input);

            var isRegistration = dialogResult.Response.Intent.indexOf("registration") > -1;
            if (dialogResult.Response.Ended && isRegistration)
            {
                jQuery(searchInput).val("");
                setTimeout(function () { jQuery(searchChat).css('opacity', '0'); }, 2000);
                setTimeout(function () { window.location = "/membership/member home"; }, 2500);
            }
                
            if (!dialogResult.Response.Ended && !isQuit) {
                jQuery(searchInput).val("");
                jQuery(dialogState + " .value").html(dialogResult.Response.Intent);
                jQuery(dialogState).addClass(showClass);
                jQuery(dialogWrap).addClass(showClass);
            }
        }
        
        jQuery(searchInput).focus();
    }

    function HandleInput(formInput) {
        if (formInput.InputType === "Password")
            jQuery(searchInput).attr("type", "password");
    }

    function ClearDialogState(clearInput) {
        if(clearInput)
            jQuery(searchInput).val("");
        jQuery(searchResults).html("");
        jQuery(resultCount).hide();
        jQuery(searchNoResults).hide();
        jQuery(resultWrap).hide();
        jQuery(dialogState).removeClass(showClass);
        jQuery(dialogWrap).removeClass(showClass);
        jQuery(dialogState + " .value").html("");
    }
    
    function ProcessSearchResult(resultItems, totalResults)
    {   
        jQuery(resultWrap).show();

        if (resultItems.length > 0) {
            var searchHtml = "";
            for (var d = 0; d < resultItems.length; d++) {

                var costValue = resultItems[d].Cost > 0 ? "$" + resultItems[d].Cost : "Free";
                searchHtml += "<div class='result-item' data-path='" + resultItems[d].Url + "'>";
                searchHtml += "<div class='title'><a href='" + resultItems[d].Url + "'>" + resultItems[d].Title + "</a></div>";
                searchHtml += "<div class='location'>Location: <span>" + resultItems[d].Location + "</span></div>";
                searchHtml += "<div class='age-range'>Age: <span>" + resultItems[d].MinAge + " - " + resultItems[d].MaxAge + "</span></div>";
                searchHtml += "<div class='cost'>Price: <span>" + costValue + "</span></div>";
                searchHtml += "<div class='date'>" + resultItems[d].Date + "</div>";
                searchHtml += "<div class='description'>" + resultItems[d].Description + "</div>";
                searchHtml += "<a class='link' href='" + resultItems[d].Url + "'>" + resultItems[d].Url + "</a>";
                searchHtml += "</div>";
            }

            jQuery(searchResults).html(searchHtml);
            jQuery(resultCount).show();
            jQuery(resultCount).html("Total Results: " + totalResults);
        }
        else {
            jQuery(searchNoResults).show();
            jQuery(resultCount).hide();
            jQuery(resultCount).html("");
        }
    }
    
    jQuery.urlParam = function (name) {
        var results = new RegExp('[\?&]' + name + '=([^&#]*)')
            .exec(window.location.search);

        var value = (results !== null) ? results[1] : "";
        
        return value;
    };
    
    var qString = jQuery.urlParam('q').split("+").join(" ");
    var initQuery = (qString.length > 0)
        ? decodeURIComponent(qString)
        : hi;

    GetDialogResult(initQuery);
    if (qString.length > 0)
        jQuery(searchInput).val(initQuery);
    jQuery(searchInput).focus();
});