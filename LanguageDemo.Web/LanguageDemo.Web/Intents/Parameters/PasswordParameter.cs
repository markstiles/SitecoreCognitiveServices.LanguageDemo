using LanguageDemo.Web.Statics;
using SitecoreCognitiveServices.Foundation.SCSDK.Services.MSSDK.Language.Enums;
using SitecoreCognitiveServices.Foundation.SCSDK.Services.MSSDK.Language.Factories;
using SitecoreCognitiveServices.Foundation.SCSDK.Services.MSSDK.Language.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LanguageDemo.Web.Intents.Parameters
{
    public class PasswordParameter : IFieldParameter
    {
        #region Constructor

        public string ParamName { get; set; }
        protected string ParamMessage { get; set; }
        public bool IsOptional { get; set; }

        public IIntentInputFactory IntentInputFactory { get; set; }
        public IParameterResultFactory ResultFactory { get; set; }

        public PasswordParameter(
            string paramName, 
            IIntentInputFactory inputFactory,
            IParameterResultFactory resultFactory)
        {
            ParamName = paramName;
            ParamMessage = "Enter a password";
            IntentInputFactory = inputFactory;
            ResultFactory = resultFactory;
            IsOptional = false;
        }

        #endregion

        public IParameterResult GetParameter(string paramValue, IConversationContext context)
        {
            if (string.IsNullOrWhiteSpace(paramValue))
                return ResultFactory.GetFailure(ParamMessage);

            return (string.IsNullOrWhiteSpace(paramValue) || paramValue.Length < 8)
                   ? ResultFactory.GetFailure("The password needs to be complex")
                   : ResultFactory.GetSuccess(paramValue, paramValue);
        }

        public IntentInput GetInput(ItemContextParameters parameters, IConversation conversation)
        {
            return IntentInputFactory.Create(IntentInputType.Password);
        }
    }
}