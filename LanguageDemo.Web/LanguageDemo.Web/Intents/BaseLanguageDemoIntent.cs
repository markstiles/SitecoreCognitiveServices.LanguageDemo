using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SitecoreCognitiveServices.Foundation.MSSDK.Language.Models.Luis;
using SitecoreCognitiveServices.Foundation.SCSDK.Services.MSSDK.Language.Factories;
using SitecoreCognitiveServices.Foundation.SCSDK.Services.MSSDK.Language.Models;

namespace LanguageDemo.Web.Intents
{
    public abstract class BaseLanguageDemoIntent : IIntent
    {
        public abstract string KeyName { get; }
        public abstract string DisplayName { get; }
        public abstract bool RequiresConfirmation { get; }
        
        public abstract ConversationResponse Respond(LuisResult result, ItemContextParameters parameters, IConversation conversation);

        #region Base Intent

        protected readonly IConversationResponseFactory ConversationResponseFactory;
        protected readonly IIntentInputFactory IntentInputFactory;

        public virtual Guid ApplicationId => new Guid("20d98668-5d89-40aa-b715-f6a994ae38c5");

        public virtual List<IConversationParameter> ConversationParameters { get; }
        
        protected BaseLanguageDemoIntent(
            IIntentInputFactory inputFactory,
            IConversationResponseFactory responseFactory)
        {
            ConversationResponseFactory = responseFactory;
            IntentInputFactory = inputFactory;
            ConversationParameters = new List<IConversationParameter>();
        }
        
        #endregion
    }
}