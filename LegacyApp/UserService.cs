using LegacyApp.Abstractions.Repositories;
using LegacyApp.Abstractions.Validation;
using LegacyApp.Models;
using LegacyApp.Repositiories;
using LegacyApp.Services;
using LegacyApp.Validation;
using System;

namespace LegacyApp
{
    public class UserService
    {
        private readonly IUserDataValidator _userDataValidator;
        private readonly IClientRepository _clientRepository;

        public UserService() 
        { 
            _userDataValidator = new UserDataValidator();
            _clientRepository = new ClientRepository();
        }
        public bool AddUser(string firstName, string lastName, string email, DateTime dateOfBirth, int clientId)
        {
            if (_userDataValidator.NotValidNames(firstName, lastName)) return false;
            if (_userDataValidator.NotValidEmail(email)) return false;
            if (_userDataValidator.NotValidBirthDate(dateOfBirth)) return false;

            // DIP violation 
            var client = _clientRepository.GetById(clientId);

            var user = new User
            {
                Client = client,
                DateOfBirth = dateOfBirth,
                EmailAddress = email,
                FirstName = firstName,
                LastName = lastName
            };

            // DIP violation
            if (client.Type == "VeryImportantClient")
            {
                user.HasCreditLimit = false;
            }
            else if (client.Type == "ImportantClient")
            {
                using (var userCreditService = new UserCreditService())
                {
                    int creditLimit = userCreditService.GetCreditLimit(user.LastName, user.DateOfBirth);
                    creditLimit = creditLimit * 2;
                    user.CreditLimit = creditLimit;
                }
            }
            else
            {
                user.HasCreditLimit = true;
                using (var userCreditService = new UserCreditService())
                {
                    int creditLimit = userCreditService.GetCreditLimit(user.LastName, user.DateOfBirth);
                    user.CreditLimit = creditLimit;
                }
            }

            if (user.HasCreditLimit && user.CreditLimit < 500)
            {
                return false;
            }

            UserDataAccess.AddUser(user);
            return true;
        }
    }
}
