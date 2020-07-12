using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LanguageDemo.Web
{
    public static class Constants
    {
        public static Guid AppId => new Guid("20d98668-5d89-40aa-b715-f6a994ae38c5");

        public static class Dimensions
        {
            public static readonly string TimeOfDay = "Time Of Day";
            public static readonly string PageLocation = "Page Location";
            public static readonly string ContentType = "Content Type";

            public static readonly string Morning = "Morning";
            public static readonly string Afternoon = "Afternoon";
            public static readonly string Evening = "Evening";
            public static readonly string Night = "Night";
            public static List<string> TimeOfDayFeatures = new List<string> { Morning, Afternoon, Evening, Night };


            public static readonly string Banner = "Banner";
            public static readonly string Sidebar = "Sidebar";
            public static readonly string Body = "Body";
            public static List<string> PageLocationFeatures = new List<string> { Banner, Sidebar, Body };

            public static readonly string Article = "Article";
            public static readonly string Media = "Media";
            public static readonly string Social = "Social";
            public static readonly string Form = "Form";
            public static List<string> ContentTypeFeatures = new List<string> { Article, Media, Social, Form };
        }
    }
}