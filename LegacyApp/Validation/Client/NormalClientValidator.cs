using LegacyApp.Abstractions;
using LegacyApp.Abstractions.Validation;
using LegacyApp.Models;
using LegacyApp.Services;

namespace LegacyApp.Validation.Client
{
    public class NormalClientValidator : ClientValidatorBase
    {
        public NormalClientValidator(IUserCredit userCreditService) : base(userCreditService) { }

        public override void CheckCredit(ref User user)
        {
            user.HasCreditLimit = true;
            int creditLimit = _userCreditService.GetCreditLimit(user.LastName, user.DateOfBirth);
            user.CreditLimit = creditLimit;
        }
    }
}
