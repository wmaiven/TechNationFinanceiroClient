using System.Threading.Tasks;
using TechNationFinanceiroClient.Models;

namespace TechNationFinanceiroClient.Services.Interfaces
{
    public interface IAuthenticationService
    {
        Task<string> GetTokenAsync(User user);
        Task Logout();
        Task<bool> IsAuthenticated();
    }
}
