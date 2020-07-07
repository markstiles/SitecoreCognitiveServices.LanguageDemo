using System;
using SitecoreCognitiveServices.Foundation.MSSDK.Language.Models.Luis;
using SitecoreCognitiveServices.Foundation.SCSDK.Services.MSSDK.Language.Factories;
using SitecoreCognitiveServices.Foundation.SCSDK.Services.MSSDK.Language.Models;

namespace LanguageDemo.Web.Intents
{
    public class DefaultIntent : BaseLanguageDemoIntent
    {
        public override string KeyName => "none";

        public override string DisplayName => "";

        public override bool RequiresConfirmation => false;

        public DefaultIntent(
            IIntentInputFactory inputFactory,
            IConversationResponseFactory responseFactory) : base(inputFactory, responseFactory)
        {
        }
        
        public override ConversationResponse Respond(LuisResult result, ItemContextParameters parameters, IConversation conversation)
        {
            return ConversationResponseFactory.Create();
        }
    }
}