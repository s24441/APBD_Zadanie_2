using System;

namespace LegacyApp.Abstractions
{
    public interface IUserCredit : IDisposable
    {
        int GetCreditLimit(string lastName, DateTime birthDate);
    }
}
