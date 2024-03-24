using LegacyApp.Models;

namespace LegacyApp.Abstractions.Validation
{
    public abstract class ClientValidatorBase
    {
        private readonly IUserCredit _userCreditService;
        protected ClientValidatorBase(IUserCredit userCreditService)
        {
            _userCreditService = userCreditService;
        }
        public abstract void CheckCredit(ref User user);
        public virtual bool ValidateCredit(User user) => !(user.HasCreditLimit && user.CreditLimit < 500);
    }
}
