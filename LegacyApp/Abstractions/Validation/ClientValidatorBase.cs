using LegacyApp.Enums;
using LegacyApp.Models;
using LegacyApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace LegacyApp.Abstractions.Validation
{
    public abstract class ClientValidatorBase
    {
        public static IReadOnlyDictionary<ClientType, ClientValidatorBase> Factory { get; }

        static ClientValidatorBase()
        {
            Func<IUserCredit> userCreditServiceFactorySingleton = () => new UserCreditService();

            var registeredClientTypes = Enum.GetValues<ClientType>().Select(type => $"{type}ClientValidator");
            Factory = Assembly
                .GetAssembly(typeof(ClientValidatorBase))
                .GetTypes()
                .Where(type => 
                    !typeof(ClientValidatorBase).IsEquivalentTo(type) 
                    && 
                    typeof(ClientValidatorBase).IsAssignableFrom(type)
                    &&
                    registeredClientTypes.Contains(type.Name))
                .ToDictionary(
                    type => Enum.Parse<ClientType>(type.Name.Replace("ClientValidator", "")), 
                    type => (ClientValidatorBase)Activator.CreateInstance(type, userCreditServiceFactorySingleton)
                );
        }

        protected readonly Func<IUserCredit> _userCreditServiceFactory;
        protected ClientValidatorBase(Func<IUserCredit> userCreditServiceFactory)
        {
            _userCreditServiceFactory = userCreditServiceFactory;
        }
        public abstract void CheckCredit(ref User user);
        public virtual bool ValidateCredit(User user) => !(user.HasCreditLimit && user.CreditLimit < 500);
    }
}
