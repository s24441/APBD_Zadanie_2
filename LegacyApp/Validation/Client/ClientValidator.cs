using LegacyApp.Abstractions;
using LegacyApp.Abstractions.Validation;
using LegacyApp.Models;
using LegacyApp.Services;

namespace LegacyApp.Validation.Client
{
    public class ClientValidator : ClientValidatorBase
    {
        public ClientValidator(IUserCredit userCreditService) : base(userCreditService) { }

        public override void CheckCredit(ref User user)
        {
            user.HasCreditLimit = true;
            using (var userCreditService = new UserCreditService())
            {
                int creditLimit = userCreditService.GetCreditLimit(user.LastName, user.DateOfBirth);
                user.CreditLimit = creditLimit;
            }
        }
    }
}
