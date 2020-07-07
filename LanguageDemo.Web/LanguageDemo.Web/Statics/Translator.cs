using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sitecore.Data;
using Sitecore.Globalization;

namespace LanguageDemo.Web.Statics
{
    public static class Translator
    {
        public static string Text(string key)
        {
            var db = Sitecore.Configuration.Factory.GetDatabase("web");

            using (new DatabaseSwitcher(db))
            {
                return Translate.TextByDomain("Intelligent Search Dictionary", key) ?? string.Empty;
            }
        }
    }
}