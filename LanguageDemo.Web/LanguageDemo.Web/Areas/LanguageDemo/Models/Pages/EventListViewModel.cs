using LanguageDemo.Web.Services;
using Sitecore.Data.Fields;
using Sitecore.Data.Items;
using Sitecore.Links;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LanguageDemo.Web.Areas.LanguageDemo.Models.Pages
{
    public interface IEventListViewModel : IBaseViewModel
    {
        List<Item> Events { get; }
        string GetLink(Item eventItem);
    }

    public class EventListViewModel : BaseViewModel, IEventListViewModel
    {
        protected readonly IEventSearchService EventSearchService;
        protected readonly IUrlService UrlService; 

        public EventListViewModel(
            IEventSearchService eventSearchService,
            IUrlService urlService)
        {
            EventSearchService = eventSearchService;
            UrlService = urlService;
        }

        private List<Item> _Events { get; set; }
        public List<Item> Events
        {
            get
            {
                if (_Events != null)
                    return _Events;

                _Events = EventSearchService.GetEventItems();

                return _Events;
            }
        }

        public string GetLink(Item eventItem)
        {
            var url = UrlService.GetUrl(eventItem);
            
            return url;
        }
    }
}