using Sitecore.Data.Fields;
using Sitecore.Data.Items;
using Sitecore.Links;
using Sitecore.Mvc.Presentation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LanguageDemo.Web.Areas.LanguageDemo.Models
{
    public interface IBaseViewModel
    {
        PageContext PageContext { get; set; }
        Rendering Rendering { get; set; }
        Item DatasourceItem { get; }
        string GetFieldValue(Item item, string fieldName);
        string GetFormattedDate(Item eventItem, string fieldName, string dateFormat);
    }

    public class BaseViewModel : IBaseViewModel
    {
        public PageContext PageContext { get; set; }
        public Rendering Rendering { get; set; }
        public Item DatasourceItem => PageContext.Database.GetItem(Rendering.DataSource);

        public string GetFieldValue(Item item, string fieldName)
        {
            var field = item.Fields[fieldName];
            if (field == null)
                return string.Empty;

            return field.Value;
        }
        
        public string GetFormattedDate(Item eventItem, string fieldName, string dateFormat)
        {
            var dateField = (DateField)eventItem.Fields[fieldName];
            if (dateField == null)
                return string.Empty;

            return dateField.DateTime.ToString(dateFormat);
        }
    }
}