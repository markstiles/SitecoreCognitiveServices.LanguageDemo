using Sitecore.Data;
using Sitecore.Data.Fields;
using Sitecore.Data.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LanguageDemo.Web.Services
{
    public interface IEventSearchService
    {
        List<Item> GetEventItems();
    }

    public class EventSearchService : IEventSearchService
    {
        public EventSearchService()
        {

        }

        public List<Item> GetEventItems()
        {
            var eventListItemID = new ID("{5FB07B32-5BB7-494B-A401-28379F17068E}");
            var eventListItem = Sitecore.Context.Database.GetItem(eventListItemID);

            var events = eventListItem.GetChildren().OrderBy(GetDate).ToList();

            return events;
        }

        public DateTime GetDate(Item item)
        {
            var dateField = (DateField)item.Fields["Date"];
            if (dateField == null)
                return DateTime.MinValue;

            return dateField.DateTime;
        }
    }
}