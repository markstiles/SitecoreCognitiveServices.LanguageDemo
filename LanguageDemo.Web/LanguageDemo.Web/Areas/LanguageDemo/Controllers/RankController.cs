using LanguageDemo.Web.Services;
using Sitecore.Analytics;
using Sitecore.Analytics.Data;
using Sitecore.Data.Fields;
using Sitecore.Data.Items;
using SitecoreCognitiveServices.Foundation.MSSDK.Decision.Models.Personalizer;
using SitecoreCognitiveServices.Foundation.MSSDK.Enums;
using SitecoreCognitiveServices.Foundation.MSSDK.Language.Models.Luis;
using SitecoreCognitiveServices.Foundation.SCSDK.Services.MSSDK.Bing;
using SitecoreCognitiveServices.Foundation.SCSDK.Services.MSSDK.Decision;
using SitecoreCognitiveServices.Foundation.SCSDK.Services.MSSDK.Language;
using SitecoreCognitiveServices.Foundation.SCSDK.Services.MSSDK.Language.Factories;
using SitecoreCognitiveServices.Foundation.SCSDK.Services.MSSDK.Language.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace LanguageDemo.Web.Controllers
{
    public class RankController : Controller
    {
        #region Constructor

        protected readonly IPersonalizerService PersonalizerService;

        public RankController(IPersonalizerService personalizerService)
        {
            PersonalizerService = personalizerService;
        }

        #endregion

        #region Rank
        
        public ActionResult GetRanking(string timeOfDayFeature, string tasteFeature, List<string> excludeActions)
        {
            var request = new RankRequest
            {
                actions = GetActions(),
                contextFeatures = new List<object>() {
                    new { time = timeOfDayFeature },
                    new { taste = tasteFeature }
                },
                excludedActions = excludeActions,
                eventId = Guid.NewGuid().ToString(),
                deferActivation = false
            };

            // Rank the actions
            var response = PersonalizerService.Rank(request);

            PersonalizerService.ActivateEvent(response.eventId);

            return Json(new { Response = response });
        }

        public ActionResult TrainRanking()
        {
            string[] timeOfDayFeatures = new string[] { "morning", "afternoon", "evening", "night" };
            string[] tasteFeatures = new string[] { "salty", "sweet", "savory", "bland" };
            
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
                    actions = GetActions(),
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
            
            return Json(new { });
        }

        public ActionResult RewardEvent(string eventId, float reward)
        {
            PersonalizerService.Reward(eventId, reward);

            return Json(new
            {
                Failed = false
            });
        }
        
        static List<RankableAction> GetActions()
        {
            List<RankableAction> actions = new List<RankableAction>
            {
                new RankableAction
                {
                    id = "pasta",
                    features =
                    new List<object>() {
                        new {
                            taste = "salty",
                            spiceLevel = "medium"
                        },
                        new {
                            nutritionLevel = 5,
                            cuisine = "italian"
                        }
                    }
                },

                new RankableAction
                {
                    id = "ice cream",
                    features  =
                    new List<object>() {
                        new {
                            taste = "savory",
                            spiceLevel = "none"
                        },
                        new {
                            nutritionalLevel = 2
                        }
                    }
                },

                new RankableAction
                {
                    id = "juice",
                    features  =
                    new List<object>() {
                        new {
                            taste = "sweet",
                            spiceLevel = "none"
                        },
                        new {
                            nutritionLevel = 5
                        },
                        new {
                            drink = true
                        }
                    }
                },

                new RankableAction
                {
                    id = "salad",
                    features  =
                    new List<object>() {
                        new {
                            taste = "bland",
                            spiceLevel = "low"
                        },
                        new {
                            nutritionLevel = 8
                        }
                    }
                }
            };

            return actions;
        }
        
        #endregion
    }
}