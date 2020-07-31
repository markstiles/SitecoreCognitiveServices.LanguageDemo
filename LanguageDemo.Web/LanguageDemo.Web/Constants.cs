using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LanguageDemo.Web
{
    public static class Constants
    {
        public static Guid AppId => new Guid("20d98668-5d89-40aa-b715-f6a994ae38c5");

        public static class ContentIds
        {
            public static string ArticleBanner1 = "Article Banner 1";
            public static string ArticleBanner2 = "Article Banner 2";
            public static string ArticleBanner3 = "Article Banner 3";
            public static string ArticleBanner4 = "Article Banner 4";

            public static List<string> Articles = new List<string>
            {
                ArticleBanner1,
                ArticleBanner2,
                ArticleBanner3,
                ArticleBanner4
            };

            public static string SignupForm1 = "Signup Form 1";
            public static string SignupForm2 = "Signup Form 2";
            public static string SignupForm3 = "Signup Form 3";
            public static string SignupForm4 = "Signup Form 4";

            public static List<string> SignupForms = new List<string>
            {
                SignupForm1,
                SignupForm2,
                SignupForm3,
                SignupForm4
            };

            public static string MediaBanner1 = "Media Banner 1";
            public static string MediaBanner2 = "Media Banner 2";
            public static string MediaBanner3 = "Media Banner 3";
            public static string MediaBanner4 = "Media Banner 4";

            public static List<string> MediaBanners = new List<string>
            {
                MediaBanner1,
                MediaBanner2,
                MediaBanner3,
                MediaBanner4
            };

            public static string SocialBanner1 = "Social Banner 1";
            public static string SocialBanner2 = "Social Banner 2";
            public static string SocialBanner3 = "Social Banner 3";
            public static string SocialBanner4 = "Social Banner 4";

            public static List<string> SocialBanners = new List<string>
            {
                SocialBanner1,
                SocialBanner2,
                SocialBanner3,
                SocialBanner4
            };
        }

        public static class Dimensions
        {
            public static class ActionFeatures
            {
                public static readonly string ContentType = "Content Type";

                public static List<string> ContentTypeFeatureList = new List<string> {
                    ContentTypeFeatures.Article,
                    ContentTypeFeatures.Media,
                    ContentTypeFeatures.Social,
                    ContentTypeFeatures.Form
                };

                public static class ContentTypeFeatures
                {
                    public static readonly string Article = "Article";
                    public static readonly string Media = "Media";
                    public static readonly string Social = "Social";
                    public static readonly string Form = "Form";
                }
            }

            public static class ContextFeatures
            {
                public static readonly string TimeOfDay = "Time Of Day";
                
                public static List<string> TimeOfDayFeatureList = new List<string> {
                    TimeOfDayFeatures.Morning,
                    TimeOfDayFeatures.Afternoon,
                    TimeOfDayFeatures.Evening,
                    TimeOfDayFeatures.Night
                };

                public static class TimeOfDayFeatures
                {
                    public static readonly string Morning = "Morning";
                    public static readonly string Afternoon = "Afternoon";
                    public static readonly string Evening = "Evening";
                    public static readonly string Night = "Night";

                }

                public static readonly string PageLocation = "Page Location";

                public static List<string> PageLocationFeatureList = new List<string> {
                    PageLocationFeatures.Banner,
                    PageLocationFeatures.Sidebar,
                    PageLocationFeatures.Body
                };

                public static class PageLocationFeatures
                {
                    public static readonly string Banner = "Banner";
                    public static readonly string Sidebar = "Sidebar";
                    public static readonly string Body = "Body";
                }

                public static readonly string PageTopic = "Page Topic";

                public static List<string> PageTopicFeatureList = new List<string> {
                    PageTopicFeatures.Politics,
                    PageTopicFeatures.Sports,
                    PageTopicFeatures.Science
                };

                public static class PageTopicFeatures
                {
                    public static readonly string Politics = "Politics";
                    public static readonly string Sports = "Sports";
                    public static readonly string Science = "Science";
                }

                public static readonly string UserProfile = "User Profile";

                public static List<string> UserProfileFeatureList = new List<string> {
                    UserProfileFeatures.HighlyEngaged,
                    UserProfileFeatures.Moderate,
                    UserProfileFeatures.Niche
                };

                public static class UserProfileFeatures
                {
                    public static readonly string HighlyEngaged = "Highly Engaged";
                    public static readonly string Moderate = "Moderate";
                    public static readonly string Niche = "Niche";
                }
            }
        }
    }
}