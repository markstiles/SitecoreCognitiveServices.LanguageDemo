using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LanguageDemo.Web.Areas.LanguageDemo.Models.Pages
{
    public interface IHomeViewModel : IBaseViewModel
    {
    }
    
    public class HomeViewModel : BaseViewModel, IHomeViewModel
    {
        public HomeViewModel()
        {
            
        }
    }
}