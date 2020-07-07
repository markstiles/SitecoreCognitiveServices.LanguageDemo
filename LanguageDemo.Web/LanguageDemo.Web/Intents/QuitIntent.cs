using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SitecoreCognitiveServices.Foundation.MSSDK.Language.Models.Luis;
using SitecoreCognitiveServices.Foundation.SCSDK.Services.MSSDK.Language.Factories;
using SitecoreCognitiveServices.Foundation.SCSDK.Services.MSSDK.Language.Models;

namespace LanguageDemo.Web.Intents
{
    public class QuitIntent : BaseLanguageDemoIntent
    {
        public override string KeyName => "quit";

        public override string DisplayName => "";

        public override bool RequiresConfirmation => false;

        public QuitIntent(
            IIntentInputFactory inputFactory,
            IConversationResponseFactory responseFactory) : base(inputFactory, responseFactory)
        {
        }

        public override ConversationResponse Respond(LuisResult result, ItemContextParameters parameters, IConversation conversation)
        {
            return ConversationResponseFactory.Create(KeyName, "");
        }
    }
}