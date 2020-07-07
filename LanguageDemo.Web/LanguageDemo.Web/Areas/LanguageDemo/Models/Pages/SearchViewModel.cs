using Sitecore.Analytics;
using Sitecore.Analytics.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LanguageDemo.Web.Areas.LanguageDemo.Models.Pages
{
    public interface ISearchViewModel : IBaseViewModel
    {
    }

    public class SearchViewModel : BaseViewModel, ISearchViewModel
    {
        public SearchViewModel()
        {

        }
    }
}