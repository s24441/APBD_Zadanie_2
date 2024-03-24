using LegacyApp.Models;

namespace LegacyApp.Abstractions.Repositories
{
    public interface IClientRepository
    {
        Client GetById(int clientId);
    }
}
