using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LanguageDemo.Web.Areas.LanguageDemo.Models.Pages
{
    public interface IEventViewModel : IBaseViewModel
    {
        string EventCost { get; }
    }

    public class EventViewModel : BaseViewModel, IEventViewModel
    {
        public EventViewModel()
        {
            
        }

        private string _EventCost { get; set; }
        public string EventCost
        {
            get
            {
                if (!string.IsNullOrWhiteSpace(_EventCost))
                    return _EventCost;

                var c = GetFieldValue(PageContext.Item, "Cost");
                _EventCost = (c == "0") ? "Free" : $"${c}";

                return _EventCost;
            }
        }
    }
}