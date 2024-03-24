using LegacyApp.Abstractions;
using LegacyApp.Abstractions.Validation;
using LegacyApp.Models;
using LegacyApp.Services;

namespace LegacyApp.Validation.Client
{
    public class ImportantClientValidator : ClientValidatorBase
    {
        public ImportantClientValidator(IUserCredit userCreditService) : base(userCreditService) { }

        public override void CheckCredit(ref User user)
        {
            using (var userCreditService = new UserCreditService())
            {
                int creditLimit = userCreditService.GetCreditLimit(user.LastName, user.DateOfBirth);
                creditLimit = creditLimit * 2;
                user.CreditLimit = creditLimit;
            }
        }
    }
}
