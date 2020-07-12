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
        void TrainOneDimension(string dimensionOne);
        void TrainTwoDimensions(string dimensionOne, string dimensionTwo);
        void TrainThreeDimensions(string dimensionOne, string dimensionTwo, string dimensionThree);
    }

    public class TrainingService : ITrainingService
    {
        protected readonly IPersonalizerService PersonalizerService;
        protected readonly IActionService ActionService;

        public TrainingService(
            IPersonalizerService personalizerService,
            IActionService actionService)
        {
            PersonalizerService = personalizerService;
            ActionService = actionService;

        }
        
        //how many clicks does it take to reach 10, 20, 30... 95 percent accuracy

        //train with different action sets
        //can one trained system be used with two different action sets and get still work

        //train by three dimensions (content type, page location and time of day)

        //train by two dimensions (content type and page location)

        //train by two dimensions (page location and 

        //train by two dimensions (page location and time of day)

        //train one dimension (time of day)
        public void TrainOneDimension(string dimensionOne)
        {


            var rewardCount = 0;
            var rankResponse = new RankResponse();
            for (int j = 0; j < 10000; j++)
            {
                var tod = timeOfDayFeatures[j % 4];
                var t = "";
                if (tod == "morning")
                    t = "sweet";
                else if (tod == "afternoon")
                    t = "bland";
                else if (tod == "evening")
                    t = "salty";
                else if (tod == "night")
                    t = "savory";

                var request = new RankRequest
                {
                    actions = ActionService.GetActions(),
                    contextFeatures = new List<object>() {
                                new { time = tod },
                                new { taste = t }
                            },
                    excludedActions = new List<string>(),
                    eventId = Guid.NewGuid().ToString(),
                    deferActivation = false
                };

                // Rank the actions
                rankResponse = PersonalizerService.Rank(request);

                var isJuice = rankResponse.rewardActionId == "juice";
                var isIceCream = rankResponse.rewardActionId == "ice cream";
                var isSalad = rankResponse.rewardActionId == "salad";
                var isPasta = rankResponse.rewardActionId == "pasta";
                var isMorning = tod == "morning";
                var isAfternoon = tod == "afternoon";
                var isEvening = tod == "evening";
                var isNight = tod == "night";

                PersonalizerService.ActivateEvent(rankResponse.eventId);

                if (isMorning && isJuice)
                    PersonalizerService.Reward(rankResponse.eventId, 1);
                else if (isAfternoon && isSalad)
                    PersonalizerService.Reward(rankResponse.eventId, 1);
                else if (isEvening && isPasta)
                    PersonalizerService.Reward(rankResponse.eventId, 1);
                else if (isNight && isIceCream)
                    PersonalizerService.Reward(rankResponse.eventId, 1);
                else
                    PersonalizerService.Reward(rankResponse.eventId, 0);
            }

            Sitecore.Diagnostics.Log.Info($"RankController Reward Count: {rewardCount}", this);
            foreach (var j in rankResponse.ranking)
            {
                Sitecore.Diagnostics.Log.Info($"RankController Ranking: {j.id} - {j.probability}", this);
            }
        }

        public void TrainTwoDimensions(string dimensionOne, string dimensionTwo)
        {
            var rewardCount = 0;
            var rankResponse = new RankResponse();
            for (int j = 0; j < 10000; j++)
            {
                var tod = timeOfDayFeatures[j % 4];
                var t = "";
                if (tod == "morning")
                    t = "sweet";
                else if (tod == "afternoon")
                    t = "bland";
                else if (tod == "evening")
                    t = "salty";
                else if (tod == "night")
                    t = "savory";

                var request = new RankRequest
                {
                    actions = ActionService.GetActions(),
                    contextFeatures = new List<object>() {
                                new { time = tod },
                                new { taste = t }
                            },
                    excludedActions = new List<string>(),
                    eventId = Guid.NewGuid().ToString(),
                    deferActivation = false
                };

                // Rank the actions
                rankResponse = PersonalizerService.Rank(request);

                var isJuice = rankResponse.rewardActionId == "juice";
                var isIceCream = rankResponse.rewardActionId == "ice cream";
                var isSalad = rankResponse.rewardActionId == "salad";
                var isPasta = rankResponse.rewardActionId == "pasta";
                var isMorning = tod == "morning";
                var isAfternoon = tod == "afternoon";
                var isEvening = tod == "evening";
                var isNight = tod == "night";

                PersonalizerService.ActivateEvent(rankResponse.eventId);

                if (isMorning && isJuice)
                    PersonalizerService.Reward(rankResponse.eventId, 1);
                else if (isAfternoon && isSalad)
                    PersonalizerService.Reward(rankResponse.eventId, 1);
                else if (isEvening && isPasta)
                    PersonalizerService.Reward(rankResponse.eventId, 1);
                else if (isNight && isIceCream)
                    PersonalizerService.Reward(rankResponse.eventId, 1);
                else
                    PersonalizerService.Reward(rankResponse.eventId, 0);
            }

            Sitecore.Diagnostics.Log.Info($"RankController Reward Count: {rewardCount}", this);
            foreach (var j in rankResponse.ranking)
            {
                Sitecore.Diagnostics.Log.Info($"RankController Ranking: {j.id} - {j.probability}", this);
            }
        }

        public void TrainThreeDimensions(string dimensionOne, string dimensionTwo, string dimensionThree) { }
    }
}