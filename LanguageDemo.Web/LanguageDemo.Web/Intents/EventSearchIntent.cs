using System;
using System.Collections.Generic;
using SitecoreCognitiveServices.Foundation.MSSDK.Language.Models.Luis;
using SitecoreCognitiveServices.Foundation.SCSDK.Services.MSSDK.Language.Factories;
using SitecoreCognitiveServices.Foundation.SCSDK.Services.MSSDK.Language.Models;

namespace LanguageDemo.Web.Intents
{
    public class EventSearchIntent : BaseLanguageDemoIntent
    {
        public override string KeyName => "event search";

        public override string DisplayName => "";

        public override bool RequiresConfirmation => false;
        
        #region Local Properties
        

        #endregion

        public EventSearchIntent(
            IIntentInputFactory inputFactory,
            IConversationResponseFactory responseFactory,
            IParameterResultFactory resultFactory) : base(inputFactory, responseFactory)
        {
        }

        public override ConversationResponse Respond(LuisResult result, ItemContextParameters parameters, IConversation conversation)
        {
            return ConversationResponseFactory.Create(KeyName, "");
        }
    }
}