using SitecoreCognitiveServices.Foundation.MSSDK.Decision.Models.Personalizer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LanguageDemo.Web.Services
{
    public interface IActionService
    {
        List<RankableAction> GetComponentActions();
        List<RankableAction> GetActions();
    }

    public class ActionService : IActionService
    {
        public List<RankableAction> GetComponentActions()
        {
            List<RankableAction> actions = new List<RankableAction>
            {
                new RankableAction
                {
                    id = Constants.ContentIds.ArticleBanner1,
                    features = new List<object>() { new {
                        contentType = Constants.Dimensions.ActionFeatures.ContentTypeFeatures.Article
                    } }
                },
                new RankableAction
                {
                    id = Constants.ContentIds.ArticleBanner2,
                    features = new List<object>() { new {
                        contentType = Constants.Dimensions.ActionFeatures.ContentTypeFeatures.Article
                    } }
                },
                new RankableAction
                {
                    id = Constants.ContentIds.ArticleBanner3,
                    features = new List<object>() { new {
                        contentType = Constants.Dimensions.ActionFeatures.ContentTypeFeatures.Article
                    } }
                },
                new RankableAction
                {
                    id = Constants.ContentIds.ArticleBanner4,
                    features = new List<object>() { new {
                        contentType = Constants.Dimensions.ActionFeatures.ContentTypeFeatures.Article
                    } }
                },
                new RankableAction
                {
                    id = Constants.ContentIds.SignupForm1,
                    features = new List<object>() { new {
                        contentType = Constants.Dimensions.ActionFeatures.ContentTypeFeatures.Form
                    } }
                },
                new RankableAction
                {
                    id = Constants.ContentIds.SignupForm2,
                    features = new List<object>() { new {
                        contentType = Constants.Dimensions.ActionFeatures.ContentTypeFeatures.Form
                    } }
                },
                new RankableAction
                {
                    id = Constants.ContentIds.SignupForm3,
                    features = new List<object>() { new {
                        contentType = Constants.Dimensions.ActionFeatures.ContentTypeFeatures.Form
                    } }
                },
                new RankableAction
                {
                    id = Constants.ContentIds.SignupForm4,
                    features = new List<object>() { new {
                        contentType = Constants.Dimensions.ActionFeatures.ContentTypeFeatures.Form
                    } }
                },
                new RankableAction
                {
                    id = Constants.ContentIds.MediaBanner1,
                    features  = new List<object>() { new {
                        contentType = Constants.Dimensions.ActionFeatures.ContentTypeFeatures.Media
                    } }
                },
                new RankableAction
                {
                    id = Constants.ContentIds.MediaBanner2,
                    features  = new List<object>() { new {
                        contentType = Constants.Dimensions.ActionFeatures.ContentTypeFeatures.Media
                    } }
                },
                new RankableAction
                {
                    id = Constants.ContentIds.MediaBanner3,
                    features  = new List<object>() { new {
                        contentType = Constants.Dimensions.ActionFeatures.ContentTypeFeatures.Media
                    } }
                },
                new RankableAction
                {
                    id = Constants.ContentIds.MediaBanner4,
                    features  = new List<object>() { new {
                        contentType = Constants.Dimensions.ActionFeatures.ContentTypeFeatures.Media
                    } }
                },
                new RankableAction
                {
                    id = Constants.ContentIds.SocialBanner1,
                    features  = new List<object>() { new {
                        contentType = Constants.Dimensions.ActionFeatures.ContentTypeFeatures.Social
                    } }
                },
                new RankableAction
                {
                    id = Constants.ContentIds.SocialBanner2,
                    features  = new List<object>() { new {
                        contentType = Constants.Dimensions.ActionFeatures.ContentTypeFeatures.Social
                    } }
                },
                new RankableAction
                {
                    id = Constants.ContentIds.SocialBanner3,
                    features  = new List<object>() { new {
                        contentType = Constants.Dimensions.ActionFeatures.ContentTypeFeatures.Social
                    } }
                },
                new RankableAction
                {
                    id = Constants.ContentIds.SocialBanner4,
                    features  = new List<object>() { new {
                        contentType = Constants.Dimensions.ActionFeatures.ContentTypeFeatures.Social
                    } }
                },
            };

            return actions;
        }

        public List<RankableAction> GetActions()
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
    }
}