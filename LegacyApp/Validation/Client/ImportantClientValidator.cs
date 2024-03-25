using LegacyApp.Abstractions;
using LegacyApp.Abstractions.Validation;
using LegacyApp.Models;
using System;

namespace LegacyApp.Validation.Client
{
    public class ImportantClientValidator : ClientValidatorBase
    {
        public ImportantClientValidator(Func<IUserCredit> userCreditServiceFactory) : base(userCreditServiceFactory) { }

        public override void CheckCredit(ref User user)
        {
            using var userCreditService = _userCreditServiceFactory();
            int creditLimit = userCreditService.GetCreditLimit(user.LastName, user.DateOfBirth);
            creditLimit = creditLimit * 2;
            user.CreditLimit = creditLimit;
        }
    }
}
