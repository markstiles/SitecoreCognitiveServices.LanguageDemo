using LanguageDemo.Web.Areas.LanguageDemo.Models;
using LanguageDemo.Web.Factories;
using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.Diagnostics;
using Sitecore.Mvc.Pipelines.Response.GetModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Compilation;

namespace LanguageDemo.Web.CustomSitecore
{
    public class GetModelFromView : GetModelProcessor
    {
        protected readonly IViewModelFactory ViewModelFactory;
        public GetModelFromView(IViewModelFactory viewModelFactory)
        {
            ViewModelFactory = viewModelFactory;
        }
        
        public override void Process(GetModelArgs args)
        {
            if (!IsValidForProcessing(args))
                return;
            
            string path = GetViewPath(args);
            if (string.IsNullOrWhiteSpace(path))
                return;
            
            Type modelType = GetModel(args, path);
            if (modelType == null)
                return;
            
            var newModel = ViewModelFactory.Create(modelType, args.PageContext, args.Rendering);

            args.Result = newModel;
        }

        private string GetPathFromLayout(Database db, ID layoutId)
        {
            Item layout = db.GetItem(layoutId);

            return layout != null
                ? layout["path"]
                : null;
        }

        private string GetViewPath(GetModelArgs args)
        {
            string path = args.Rendering.RenderingItem.InnerItem["path"];

            if (string.IsNullOrWhiteSpace(path) && args.Rendering.RenderingType == "Layout")
                path = GetPathFromLayout(args.PageContext.Database, new ID(args.Rendering.LayoutId));
            
            return path;
        }

        private Type GetModel(GetModelArgs args, string path)
        {
            Type compiledViewType = BuildManager.GetCompiledType(path);
            Type baseType = compiledViewType.BaseType;

            if (baseType == null || !baseType.IsGenericType)
            {
                Log.Error(string.Format(
                    "View {0} compiled type {1} base type {2} does not have a single generic argument.",
                    args.Rendering.RenderingItem.InnerItem["path"],
                    compiledViewType,
                    baseType), this);
                return null;
            }

            Type proposedType = baseType.GetGenericArguments()[0];
            return proposedType == typeof(object)
                ? null
                : proposedType;
        }

        private static bool IsValidForProcessing(GetModelArgs args)
        {
            if (args.Result != null)
                return false;
            
            if (!String.IsNullOrEmpty(args.Rendering.RenderingItem.InnerItem["Model"]))
                return false;
            
            return args.Rendering.RenderingType == "Layout" ||
                   args.Rendering.RenderingType == "View" ||
                   args.Rendering.RenderingType == "r";
        }
    }
}