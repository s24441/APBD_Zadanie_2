using System;

namespace LegacyApp.Abstractions
{
    public interface IUserCredit
    {
        int GetCreditLimit(string lastName, DateTime birthDate);
    }
}
