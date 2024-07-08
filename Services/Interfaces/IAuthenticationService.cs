using TechNationFinanceiroClient.Models;

namespace TechNationFinanceiroClient.Services.Interfaces
{
    public interface IAuthenticationService
    {
        Task<string> GetTokenAsync(User user);
        Task<bool> Logout();
    }
}
