using LanguageDemo.Web.Services;
using Sitecore.Analytics;
using Sitecore.Analytics.Data;
using Sitecore.Data.Fields;
using Sitecore.Data.Items;
using SitecoreCognitiveServices.Foundation.MSSDK.Enums;
using SitecoreCognitiveServices.Foundation.MSSDK.Language.Models.Luis;
using SitecoreCognitiveServices.Foundation.SCSDK.Services.MSSDK.Bing;
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
    public class SearchController : Controller
    {
        #region Constructor

        protected readonly IEventSearchService EventSearchService;
        protected readonly IUrlService UrlService;
        protected readonly ILuisService LuisService;
        protected readonly ILuisConversationService LuisConversationService;
        protected readonly IConversationContextFactory ConversationContextFactory;
        
        public SearchController(
            IEventSearchService eventSearchService,
            IUrlService urlService,
            ILuisService luisService,
            ILuisConversationService luisConversationService,
            IConversationContextFactory conversationContextFactory)
        {
            EventSearchService = eventSearchService;
            UrlService = urlService;
            LuisService = luisService;
            LuisConversationService = luisConversationService;
            ConversationContextFactory = conversationContextFactory;
        }

        #endregion
        
        #region Search Form
        
        public ActionResult SearchFormLuisPost(string id, string db, string language, string query)
        {
            var appId = Constants.AppId;
            if (appId == Guid.Empty)
                return Json(new { Failed = true });

            //call luis
            var luisResult = !string.IsNullOrWhiteSpace(query) ? LuisService.Query(appId, query, true) : null;
            var contextParams = new ItemContextParameters
            {
                Database = db,
                Id = id,
                Language = language
            };

            var conversationContext = ConversationContextFactory.Create(
                    appId,
                    "Clear",
                    "Confirm",
                    "decision - yes",
                    "decision - no",
                    "frustrated",
                    "quit",
                    contextParams,
                    luisResult);

            var response = LuisConversationService.ProcessUserInput(conversationContext);

            var events = EventSearchService.GetEventItems().Select(a =>
                new SearchResult
                {
                    Title = a.Fields["Title"].Value,
                    Url = UrlService.GetDomainAndUrl(a),
                    Description = TrimContent(a.Fields["Content"].Value),
                    MinAge = MinAge(a),
                    MaxAge = MaxAge(a),
                    Location = Location(a),
                    Date = GetDate(a).ToString("MMMM d, yyyy"),
                    Cost = GetCost(a)
                });

            if (response.Intent.Equals("event search"))
                events = FilterEvents(events, luisResult.Entities);
            
            var returnList = events;

            //return result
            return Json(new
            {
                Failed = false,
                Response = response,
                SpellCorrected = luisResult.AlteredQuery,
                Items = returnList,
                TotalResults = returnList.Count()
            });
        }

        public List<SearchResult> FilterEvents(IEnumerable<SearchResult> events, IList<EntityRecommendation> entities)
        {
            foreach (var e in entities)
            {
                var lowerName = e.Entity.ToLower();
                if (e.Type == "Age Indicator")
                {
                    // choose age range based on value (toddler, babies)
                    if(lowerName == "toddler")
                    {
                        events = events.Where(a => a.MinAge >= 2 || a.MaxAge <= 4);
                    }
                    else if (lowerName == "babies" || lowerName == "baby")
                    {
                        events = events.Where(a => a.MinAge >= 0 || a.MaxAge <= 1);
                    }
                }
                if (e.Type == "Age Range")
                {
                    // break up age range with dash
                    if (lowerName.Contains("-"))
                    {
                        var parts = lowerName.Split(new string[] { "-" }, StringSplitOptions.RemoveEmptyEntries);
                        events = events.Where(a => a.MinAge <= GetInt(parts[0]) && a.MaxAge >= GetInt(parts[1]));
                    }
                    else
                    {
                        var requestedAge = GetInt(lowerName);
                        events = events.Where(a => a.MinAge <= requestedAge && a.MaxAge >= requestedAge);
                    }
                }
                if (e.Type == "Location")
                {
                    // pick location
                    events = events.Where(a => a.Location.ToLower().Contains(lowerName));
                }
                if (e.Type == "Price")
                {
                    var requestedCost = GetInt(lowerName.Replace("$", "").Trim());
                    events = events.Where(a => a.Cost <= requestedCost);
                }
            }

            return events.ToList();
        }

        public DateTime GetDate(Item item)
        {
            DateField f = (DateField)item.Fields["Date"];
            if (f == null)
                return DateTime.MinValue;

            var d = f.DateTime;

            return d;
        }

        public int GetCost(Item item)
        {
            Field f = item.Fields["Cost"];
            if (f == null)
                return 0;

            var c = f.Value;

            return GetInt(c);
        }

        public int GetInt(string s)
        {
            int i = -1;
            if (int.TryParse(s, out i))
                return i;

            return -1;
        }

        public string Location(Item i)
        {
            return i.Fields["Location"].Value;
        }

        public int MinAge(Item i)
        {
            return GetIntValue(i.Fields["Age Min"]);
        }

        public int MaxAge(Item i)
        {
            return GetIntValue(i.Fields["Age Max"]);
        }

        public int GetIntValue(Field f)
        {
            if (f == null)
                return 0;

            var value = int.Parse(f.Value);

            return value;
        }

        public string TrimContent(string content)
        {
            StringBuilder sb = new StringBuilder();
            var parts = content.Split(new string[] { "</p>" }, StringSplitOptions.RemoveEmptyEntries);

            foreach (var p in parts)
            {
                if (sb.Length + p.Length > 175)
                    break;

                sb.Append(p + "</p>");
            }

            return sb.ToString();
        }
        
        public class SearchResult
        {
            public string Title { get; set; }
            public string Url { get; set; }
            public string Description { get; set; }
            public int MinAge { get; set; }
            public int MaxAge { get; set; }
            public int Cost { get; set; }
            public string Date { get; set; }
            public string Location { get; set; }
        }

        #endregion
    }
}