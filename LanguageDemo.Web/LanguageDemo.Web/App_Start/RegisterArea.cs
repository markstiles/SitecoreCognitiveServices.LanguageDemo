using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LanguageDemo.Web.App_Start
{
    public class LanguageDemoAreaRegistration : AreaRegistration
    {
        public override string AreaName => "LanguageDemo";

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(AreaName, "LanguageDemo/{controller}/{action}", new
            {
                area = AreaName
            });
        }
    }
}