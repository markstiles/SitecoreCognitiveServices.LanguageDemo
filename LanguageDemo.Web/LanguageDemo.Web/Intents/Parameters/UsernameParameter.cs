using SitecoreCognitiveServices.Foundation.SCSDK.Services.MSSDK.Language.Enums;
using SitecoreCognitiveServices.Foundation.SCSDK.Services.MSSDK.Language.Factories;
using SitecoreCognitiveServices.Foundation.SCSDK.Services.MSSDK.Language.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using LanguageDemo.Web.Statics;

namespace LanguageDemo.Web.Intents.Parameters
{
    public class UsernameParameter : IFieldParameter
    {
        #region Constructor

        public string ParamName { get; set; }
        protected string ParamMessage { get; set; }
        public bool IsOptional { get; set; }

        public IIntentInputFactory IntentInputFactory { get; set; }
        public IParameterResultFactory ResultFactory { get; set; }

        public UsernameParameter(
            string paramName, 
            IIntentInputFactory inputFactory, 
            IParameterResultFactory resultFactory)
        {
            ParamName = paramName;
            ParamMessage = "Enter an email";
            IntentInputFactory = inputFactory;
            ResultFactory = resultFactory;
            IsOptional = false;
        }

        #endregion

        public IParameterResult GetParameter(string paramValue, IConversationContext context)
        {
            if (string.IsNullOrWhiteSpace(paramValue))
                return ResultFactory.GetFailure(ParamMessage);

            try
            {
                MailAddress m = new MailAddress(paramValue);

                return ResultFactory.GetSuccess(paramValue, paramValue);
            }
            catch (FormatException) { }

            return ResultFactory.GetFailure("You need to provide an email");
        }

        public IntentInput GetInput(ItemContextParameters parameters, IConversation conversation)
        {
            return IntentInputFactory.Create(IntentInputType.None);
        }
    }
}