using LegacyApp.Abstractions.Repositories;
using LegacyApp.Abstractions.Validation;
using LegacyApp.Enums;
using LegacyApp.Models;
using LegacyApp.Repositiories;
using LegacyApp.Validation;
using System;
using System.Collections.Generic;

namespace LegacyApp
{
    public class UserService
    {
        private readonly IUserDataValidator _userDataValidator;
        private readonly IClientRepository _clientRepository;
        private readonly IReadOnlyDictionary<ClientType, ClientValidatorBase> _clientValidatorFactory;

        public UserService() 
        { 
            _userDataValidator = new UserDataValidator();
            _clientRepository = new ClientRepository();
            _clientValidatorFactory = ClientValidatorBase.Factory;
        }
        public bool AddUser(string firstName, string lastName, string email, DateTime dateOfBirth, int clientId)
        {
            var inputNotValid = _userDataValidator.NotValidNames(firstName, lastName) ||
                _userDataValidator.NotValidEmail(email) ||
                _userDataValidator.NotValidBirthDate(dateOfBirth);
            if (inputNotValid) return false;

            var client = _clientRepository.GetById(clientId);

            var user = new User
            {
                Client = client,
                DateOfBirth = dateOfBirth,
                EmailAddress = email,
                FirstName = firstName,
                LastName = lastName
            };

            var clientValidator = _clientValidatorFactory[client.Type];
            clientValidator.CheckCredit(ref user);
            if (!clientValidator.ValidateCredit(user)) return false;

            UserDataAccess.AddUser(user);
            return true;
        }
    }
}
