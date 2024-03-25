using LegacyApp.Abstractions;
using LegacyApp.Abstractions.Validation;
using LegacyApp.Models;
using System;

namespace LegacyApp.Validation.Client
{
    public class VeryImportantClientValidator : ClientValidatorBase
    {
        public VeryImportantClientValidator(Func<IUserCredit> userCreditServiceFactory) : base(userCreditServiceFactory) { }

        public override void CheckCredit(ref User user)
        {
            user.HasCreditLimit = false;
        }
    }
}
