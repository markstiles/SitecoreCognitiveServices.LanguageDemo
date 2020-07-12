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
        protected readonly IActionService ActionService;
        protected readonly ITrainingService TrainingService;

        public RankController(
            IPersonalizerService personalizerService,
            IActionService actionService,
            ITrainingService trainingService)
        {
            PersonalizerService = personalizerService;
            ActionService = actionService;
            TrainingService = trainingService;
        }

        #endregion

        #region Rank
        
        public ActionResult GetRanking(string timeOfDayFeature, string tasteFeature, List<string> excludeActions)
        {
            var request = new RankRequest
            {
                actions = ActionService.GetActions(),
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
        
        public ActionResult TrainOneDimension(string dimensionOne)
        {
            TrainingService.TrainOneDimension(dimensionOne);

            return Json(new { /*return training values 2000 clicks = 10% confidence, 3000 clicks = 18% confidence */});
        }

        public ActionResult TrainTwoDimensions(string dimensionOne, string dimensionTwo)
        {
            TrainingService.TrainTwoDimensions(dimensionOne, dimensionTwo);

            return Json(new { });
        }

        public ActionResult TrainThreeDimensions(string dimensionOne, string dimensionTwo, string dimensionThree)
        {
            TrainingService.TrainThreeDimensions(dimensionOne, dimensionTwo, dimensionThree);

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
        
        #endregion
    }
}