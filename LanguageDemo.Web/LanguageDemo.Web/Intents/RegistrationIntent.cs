using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Web.Security;
using Sitecore.Data.Managers;
using Sitecore.Security.Accounts;
using Sitecore.Security.Domains;
using SitecoreCognitiveServices.Foundation.MSSDK.Language.Models.Luis;
using SitecoreCognitiveServices.Foundation.SCSDK.Services.MSSDK.Language.Factories;
using SitecoreCognitiveServices.Foundation.SCSDK.Services.MSSDK.Language.Models;
using LanguageDemo.Web.Intents.Parameters;

namespace LanguageDemo.Web.Intents
{
    public class RegistrationIntent : BaseLanguageDemoIntent
    {
        public override string KeyName => "registration";

        public override string DisplayName => "";

        public override bool RequiresConfirmation => false;
        
        #region Local Properties

        protected string UsernameKey = "Username";
        protected string PasswordKey = "Password";

        #endregion

        public RegistrationIntent(
            IIntentInputFactory inputFactory,
            IConversationResponseFactory responseFactory,
            IParameterResultFactory resultFactory) : base(inputFactory, responseFactory)
        {
            ConversationParameters.Add(new UsernameParameter(UsernameKey, inputFactory, resultFactory));
            ConversationParameters.Add(new PasswordParameter(PasswordKey, inputFactory, resultFactory));
        }

        public override ConversationResponse Respond(LuisResult result, ItemContextParameters parameters, IConversation conversation)
        {
            var username = (string)conversation.Data[UsernameKey].Value;
            var password = (string)conversation.Data[PasswordKey].Value;
            //CreateUser(Sitecore.Context.Domain, username, password);
            
            return ConversationResponseFactory.Create(KeyName, "Congratulations, you're now registered");
        }

        public void CreateUser(Domain domain, string email, string password)
        {
            try
            {
                var domainUser = $"{domain}\\{email}";
                User sitecoreUser = User.Create(domainUser, password);
                MembershipUser membershipUser = Membership.GetUser(domainUser);
                if (sitecoreUser == null || membershipUser == null)
                    return;

                membershipUser.IsApproved = false;
                Membership.UpdateUser(membershipUser);

                sitecoreUser.Profile.Email = email;
                sitecoreUser.Profile.Save();

                Sitecore.Analytics.Tracker.Current.Session.Identify(domainUser);
            }
            catch (MembershipCreateUserException ex) { }
        }
    }
}