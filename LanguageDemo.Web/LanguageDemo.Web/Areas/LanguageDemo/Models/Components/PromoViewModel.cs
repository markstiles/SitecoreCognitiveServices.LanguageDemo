using LanguageDemo.Web.Services;
using Sitecore.Data.Fields;
using Sitecore.Data.Items;
using Sitecore.Links;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LanguageDemo.Web.Areas.LanguageDemo.Models.Components
{
    public interface IPromoViewModel : IBaseViewModel
    {
    }

    public class PromoViewModel : BaseViewModel, IPromoViewModel
    {
        public PromoViewModel()
        {
        }
    }
}