using System;

namespace LegacyApp.Abstractions.Validation
{
    public interface IUserDataValidator
    {
        bool NotValidNames(params string[] names);
        bool NotValidEmail(string email);
        bool NotValidBirthDate(DateTime birthDate);
    }
}
