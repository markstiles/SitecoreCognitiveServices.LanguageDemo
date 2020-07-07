using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LanguageDemo.Web.Areas.LanguageDemo.Models.Pages
{
    public interface IPageViewModel : IBaseViewModel
    {
    }

    public class PageViewModel : BaseViewModel, IPageViewModel
    {
        public PageViewModel()
        {

        }
    }
}