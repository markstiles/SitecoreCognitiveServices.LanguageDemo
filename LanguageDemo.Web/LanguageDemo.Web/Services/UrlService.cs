using Sitecore.Data.Items;
using Sitecore.Links;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LanguageDemo.Web.Services
{
    public interface IUrlService
    {
        string GetUrl(Item item);
        string GetDomainAndUrl(Item item);
    }

    public class UrlService : IUrlService
    {
        public string GetUrl(Item item)
        {
            var options = new UrlOptions
            {
                LanguageEmbedding = LanguageEmbedding.Never
            };

            var url = LinkManager.GetItemUrl(item, options);

            return url;
        }

        public string GetDomainAndUrl(Item item)
        {
            var options = new UrlOptions
            {
                LanguageEmbedding = LanguageEmbedding.Never,
                AlwaysIncludeServerUrl = true
            };

            var url = LinkManager.GetItemUrl(item, options);

            return url;
        }
    }
}