using SitecoreCognitiveServices.Foundation.MSSDK.Decision.Models.Personalizer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LanguageDemo.Web.Services
{
    public interface IActionService
    {
        List<RankableAction> GetActions();
    }

    public class ActionService : IActionService
    {
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