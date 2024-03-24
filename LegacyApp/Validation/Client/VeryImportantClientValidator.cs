using LegacyApp.Abstractions;
using LegacyApp.Abstractions.Validation;
using LegacyApp.Models;

namespace LegacyApp.Validation.Client
{
    public class VeryImportantClientValidator : ClientValidatorBase
    {
        public VeryImportantClientValidator(IUserCredit userCreditService) : base(userCreditService) { }

        public override void CheckCredit(ref User user)
        {
            user.HasCreditLimit = false;
        }
    }
}
