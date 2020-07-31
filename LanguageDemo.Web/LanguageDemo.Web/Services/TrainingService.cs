using LanguageDemo.Web.Services.Models;
using SitecoreCognitiveServices.Foundation.MSSDK.Decision.Models.Personalizer;
using SitecoreCognitiveServices.Foundation.SCSDK.Services.MSSDK.Decision;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LanguageDemo.Web.Services
{
    public interface ITrainingService
    {
        List<TrainingResult> TrainOneDimension();
        List<TrainingResult> TrainTwoDimensions();
        List<TrainingResult> TrainThreeDimensions();
    }

    public class TrainingService : ITrainingService
    {
        #region Constructor

        protected readonly IPersonalizerService PersonalizerService;
        protected readonly IActionService ActionService;

        public TrainingService(
            IPersonalizerService personalizerService,
            IActionService actionService)
        {
            PersonalizerService = personalizerService;
            ActionService = actionService;

        }

        #endregion

        //how many clicks does it take to reach 10, 20, 30... 95 percent accuracy

        //train with different action sets
        //can one trained system be used with two different action sets and get still work

        //train by three dimensions (content type, page location and time of day)

        //train by two dimensions (content type and page location)

        //train by two dimensions (page location and 

        //train by two dimensions (page location and time of day)

        //train one dimension (time of day)
        
        public List<TrainingResult> TrainOneDimension()
        {
            //for 10000 click events

            //click 1000 times on one value

            //log confidence as the clicks progress


            var results = new List<TrainingResult>();
            var dimSet = Constants.Dimensions.ContextFeatures.PageLocationFeatureList;
            var preferredValue = Constants.Dimensions.ContextFeatures.PageLocationFeatures.Banner;

            var rewardCount = 0;
            var rewardLimit = 10;
            var runLimit = 10 * rewardLimit;

            var rankResponse = new RankResponse();
            for (int j = 0; j < runLimit; j++)
            {
                if (rewardCount >= rewardLimit)
                    break; 

                var value = dimSet[j % dimSet.Count];

                var request = new RankRequest
                {
                    actions = ActionService.GetComponentActions(),
                    contextFeatures = new List<object>()
                    {
                        new { pagelocation = value }
                    },
                    excludedActions = new List<string>(),
                    eventId = Guid.NewGuid().ToString(),
                    deferActivation = false
                };

                rankResponse = PersonalizerService.Rank(request);
                PersonalizerService.ActivateEvent(rankResponse.eventId);

                //if it recommends media for the banner
                var shouldReward = Constants.Dimensions.ContextFeatures.PageLocationFeatures.Banner == value &&  
                    Constants.ContentIds.MediaBanners.Contains(rankResponse.rewardActionId);
                if (shouldReward)
                {
                    rewardCount++;
                    PersonalizerService.Reward(rankResponse.eventId, 1);
                }

                if (rewardCount % 100 == 0)
                {
                    var preferredItem = rankResponse.ranking.Where(a => a.id.Equals(preferredValue)).First();
                    var pair = new TrainingResult {
                        Clicks = j,
                        Confidence = preferredItem.probability,
                        RewardActionId = rankResponse.rewardActionId,
                        Rewarded = shouldReward
                    };
                    results.Add(pair);
                }
            }
            
            return results;
        }

        public List<TrainingResult> TrainTwoDimensions()
        {
            //    var rewardCount = 0;
            //    var rankResponse = new RankResponse();
            //    for (int j = 0; j < 10000; j++)
            //    {
            //        var tod = timeOfDayFeatures[j % 4];
            //        var t = "";
            //        if (tod == "morning")
            //            t = "sweet";
            //        else if (tod == "afternoon")
            //            t = "bland";
            //        else if (tod == "evening")
            //            t = "salty";
            //        else if (tod == "night")
            //            t = "savory";

            //        var request = new RankRequest
            //        {
            //            actions = ActionService.GetActions(),
            //            contextFeatures = new List<object>() {
            //                        new { time = tod },
            //                        new { taste = t }
            //                    },
            //            excludedActions = new List<string>(),
            //            eventId = Guid.NewGuid().ToString(),
            //            deferActivation = false
            //        };

            //        // Rank the actions
            //        rankResponse = PersonalizerService.Rank(request);

            //        var isJuice = rankResponse.rewardActionId == "juice";
            //        var isIceCream = rankResponse.rewardActionId == "ice cream";
            //        var isSalad = rankResponse.rewardActionId == "salad";
            //        var isPasta = rankResponse.rewardActionId == "pasta";
            //        var isMorning = tod == "morning";
            //        var isAfternoon = tod == "afternoon";
            //        var isEvening = tod == "evening";
            //        var isNight = tod == "night";

            //        PersonalizerService.ActivateEvent(rankResponse.eventId);

            //        if (isMorning && isJuice)
            //            PersonalizerService.Reward(rankResponse.eventId, 1);
            //        else if (isAfternoon && isSalad)
            //            PersonalizerService.Reward(rankResponse.eventId, 1);
            //        else if (isEvening && isPasta)
            //            PersonalizerService.Reward(rankResponse.eventId, 1);
            //        else if (isNight && isIceCream)
            //            PersonalizerService.Reward(rankResponse.eventId, 1);
            //        else
            //            PersonalizerService.Reward(rankResponse.eventId, 0);
            //    }

            //    Sitecore.Diagnostics.Log.Info($"RankController Reward Count: {rewardCount}", this);
            //    foreach (var j in rankResponse.ranking)
            //    {
            //        Sitecore.Diagnostics.Log.Info($"RankController Ranking: {j.id} - {j.probability}", this);
            //    }
            return new List<TrainingResult>();
        }

        public List<TrainingResult> TrainThreeDimensions()
        {
            return new List<TrainingResult>();
        }
    }
}