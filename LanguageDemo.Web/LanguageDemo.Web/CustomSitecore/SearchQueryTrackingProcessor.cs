using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using Sitecore;
using Sitecore.Data.Items;
using Sitecore.Diagnostics;
using Sitecore.Pipelines.HttpRequest;
using Sitecore.Analytics;
using Sitecore.Analytics.Data;
using Sitecore.Mvc.Pipelines.Request.RequestBegin;

namespace LanguageDemo.Web.CustomSitecore
{
    public class SearchQueryTrackingProcessor
    {
        public void Process(RequestBeginArgs args)
        {
            var qs = args.PageContext.RequestContext.HttpContext.Request.QueryString;
            if (!qs.AllKeys.Contains("q"))
                return;

            var query = qs["q"];
            var searchEvent = Tracker.DefinitionItems.PageEvents[AnalyticsIds.SearchEvent.Guid];
            Tracker.Current.CurrentPage.Register(new PageEventData(searchEvent.Name, searchEvent.ID.Guid)
            {
                Data = query,
                Text = query
            });
        }
    }
}