using LanguageDemo.Web.Services;
using Sitecore.Data.Fields;
using Sitecore.Data.Items;
using Sitecore.Links;
using SitecoreCognitiveServices.Foundation.MSSDK.Decision.Models.Personalizer;
using SitecoreCognitiveServices.Foundation.SCSDK.Services.MSSDK.Decision;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LanguageDemo.Web.Areas.LanguageDemo.Models.Components
{
    public interface IRankedContentViewModel : IBaseViewModel
    {
        RankResponse Ranking { get; set; }
    }

    public class RankedContentViewModel : BaseViewModel, IRankedContentViewModel
    {
        
        public RankResponse Ranking { get; set; }

        public RankedContentViewModel()
        {
        }
    }
}