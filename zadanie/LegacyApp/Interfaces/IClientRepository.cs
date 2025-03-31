using LegacyApp;

namespace LegacyApp
{
    public interface IClientRepository
    {
        Client GetById(int clientId);
    }
}