using LegacyApp.Abstractions.Validation;
using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace LegacyApp.Validation
{
    public class UserDataValidator : IUserDataValidator
    {
        public bool NotValidNames(params string[] names) => names.Any(string.IsNullOrEmpty);
        public bool NotValidEmail(string email) => !Regex.IsMatch(email, "^[a-zA-Z0-9]@[a-zA-Z0-9]\\.[a-zA-Z0-9]$");
        public bool NotValidBirthDate(DateTime birthDate)
        {
            var now = DateTime.Now;
            int age = now.Year - birthDate.Year;
            if (now.Month < birthDate.Month || now.Month == birthDate.Month && now.Day < birthDate.Day) age--;

            return age < 21;
        }
    }
}
