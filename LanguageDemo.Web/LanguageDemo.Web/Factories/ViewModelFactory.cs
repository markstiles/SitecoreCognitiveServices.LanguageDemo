using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LanguageDemo.Web.Areas.LanguageDemo.Models;
using Microsoft.Extensions.DependencyInjection;
using Sitecore.Mvc.Presentation;

namespace LanguageDemo.Web.Factories
{
    public interface IViewModelFactory
    {
        object Create(Type type, PageContext pageContext, Rendering rendering);
    }

    public class ViewModelFactory : IViewModelFactory
    {
        protected readonly IServiceProvider Provider;

        public ViewModelFactory(IServiceProvider provider)
        {
            Provider = provider;
        }

        public object Create(Type type, PageContext pageContext, Rendering rendering)
        {
            var obj = Provider.GetService(type);

            if (obj is IBaseViewModel)
            {
                var newBaseModel = obj as IBaseViewModel;
                newBaseModel.PageContext = pageContext;
                newBaseModel.Rendering = rendering;
            }

            return obj;
        }
    }
}