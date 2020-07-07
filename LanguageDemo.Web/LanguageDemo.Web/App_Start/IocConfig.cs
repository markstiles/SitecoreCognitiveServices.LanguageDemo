using LanguageDemo.Web.Areas.LanguageDemo.Models.Components;
using LanguageDemo.Web.Areas.LanguageDemo.Models.Pages;
using LanguageDemo.Web.Controllers;
using LanguageDemo.Web.Factories;
using LanguageDemo.Web.Intents;
using LanguageDemo.Web.Services;
using Microsoft.Extensions.DependencyInjection;
using Sitecore.DependencyInjection;
using SitecoreCognitiveServices.Foundation.SCSDK.Services.MSSDK.Language.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LanguageDemo.Web.App_Start
{
    public class IocConfig : IServicesConfigurator
    {
        public void Configure(IServiceCollection serviceCollection)
        {
            //intents
            serviceCollection.AddTransient<IIntent, DefaultIntent>();
            serviceCollection.AddTransient<IIntent, EventSearchIntent>();
            serviceCollection.AddTransient<IIntent, QuitIntent>();
            serviceCollection.AddTransient<IIntent, RegistrationIntent>();

            //models
            serviceCollection.AddTransient<IHomeViewModel, HomeViewModel>();
            serviceCollection.AddTransient<IPageViewModel, PageViewModel>();
            serviceCollection.AddTransient<IEventViewModel, EventViewModel>();
            serviceCollection.AddTransient<IPromoViewModel, PromoViewModel>();
            serviceCollection.AddTransient<IRankedContentViewModel, RankedContentViewModel>();
            serviceCollection.AddTransient<ISearchViewModel, SearchViewModel>();
            serviceCollection.AddTransient<IEventListViewModel, EventListViewModel>();

            //factories
            serviceCollection.AddTransient<IViewModelFactory, ViewModelFactory>();

            //services
            serviceCollection.AddTransient<IUrlService, UrlService>();
            serviceCollection.AddTransient<IEventSearchService, EventSearchService>();
            
            serviceCollection.AddTransient(typeof(SearchController));
            serviceCollection.AddTransient(typeof(RankController));
        }
    }
}