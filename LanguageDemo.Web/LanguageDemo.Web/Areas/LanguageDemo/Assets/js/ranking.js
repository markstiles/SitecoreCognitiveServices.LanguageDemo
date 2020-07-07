jQuery.noConflict();

//ranking form
jQuery(document).ready(function ()
{
    var rankingForm = ".ranking-form";
    var rankingSubmit = rankingForm + " .ranking-submit";
    var rewardSubmit = rankingForm + " .reward-submit";
    var trainSubmit = rankingForm + " .train-submit";
    var progressIndicator = ".progress-indicator";
    var resultWrap = ".result-wrap";
    
    jQuery(rankingSubmit).click(function (event)
    {
        event.preventDefault();

        var todValue = jQuery(rankingForm + " #time-of-day").val();
        var tasteValue = jQuery(rankingForm + " #taste").val();

        jQuery(progressIndicator).show();
        jQuery(resultWrap).hide();

        jQuery.post(
            jQuery(rankingForm + " form").attr("action"),
            {
                timeofDayFeature: todValue,
                tasteFeature: tasteValue,
                excludeActions: []
            }
        ).done(function (r) {
            jQuery(progressIndicator).hide();
            ProcessRanking(r.Response);
        }).fail(function () {
            jQuery(progressIndicator).hide();
        });
    });
    
    function ProcessRanking(response)
    {   
        if (response === null)
            return;

        jQuery(".event-id span").html(response.eventId);
        jQuery(".reward-action-id span").html(response.rewardActionId);

        if (response.ranking.length > 0) {
            var rankingHtml = "";
            for (var d = 0; d < response.ranking.length; d++)
            {
                var type = (d % 2 === 0) ? "even" : "odd";
                rankingHtml += "<div class='rank-item " + type + "'>";
                rankingHtml += "<span class='item-id'>" + response.ranking[d].id + " - </span>";
                rankingHtml += "<span class='item-probability'>" + (response.ranking[d].probability * 100) + "%</span>";
                rankingHtml += "</div>";
            }

            jQuery(resultWrap).html(rankingHtml);
            jQuery(resultWrap).show();
        }
    }

    jQuery(trainSubmit).click(function (event) {
        event.preventDefault();
        
        jQuery(progressIndicator).show();
        jQuery(resultWrap).hide();

        jQuery.post(
            jQuery(rankingForm + " form").attr("train-action"), { }
        ).done(function (r) {
            jQuery(progressIndicator).hide();
            jQuery(resultWrap).show();
        }).fail(function () {
            jQuery(progressIndicator).hide();
        });;
    });

    jQuery(rewardSubmit).click(function (event) {
        event.preventDefault();

        var rewardValue = jQuery(rankingForm + " #reward-value").val();
        var eventIdValue = jQuery(rankingForm + " .event-id span").text();
        
        jQuery(progressIndicator).show();
        jQuery(resultWrap).hide();

        jQuery.post(
            jQuery(rankingForm + " form").attr("reward-action"),
            {
                eventId: eventIdValue,
                reward: rewardValue
            }
        ).done(function (r) {
            jQuery(progressIndicator).hide();
            jQuery(resultWrap).show();
        }).fail(function () {
            jQuery(progressIndicator).hide();
        });
    });
});