using LegacyApp.Abstractions;
using LegacyApp.Abstractions.Validation;
using LegacyApp.Models;
using System;

namespace LegacyApp.Validation.Client
{
    public class NormalClientValidator : ClientValidatorBase
    {
        public NormalClientValidator(Func<IUserCredit> userCreditServiceFactory) : base(userCreditServiceFactory) { }

        public override void CheckCredit(ref User user)
        {
            using var userCreditService = _userCreditServiceFactory();
            user.HasCreditLimit = true;
            int creditLimit = userCreditService.GetCreditLimit(user.LastName, user.DateOfBirth);
            user.CreditLimit = creditLimit;
        }
    }
}
