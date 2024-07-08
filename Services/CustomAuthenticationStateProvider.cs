using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;


namespace TechNationFinanceiroClient.Services
{

    public class CustomAuthenticationStateProvider : AuthenticationStateProvider
    {
        private readonly IJSRuntime _jsRuntime;

        public CustomAuthenticationStateProvider(IJSRuntime jsRuntime)
        {
            _jsRuntime = jsRuntime;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var token = await _jsRuntime.InvokeAsync<string>("localStorage.getItem", "authToken");
            var isAuthenticated = !string.IsNullOrEmpty(token);

            Console.WriteLine($"IsAuthenticated: {isAuthenticated}");

            if (isAuthenticated)
            {
                Console.WriteLine($"Token: {token}");

                var claims = DecodeToken(token);
                Console.WriteLine($"Claims: {string.Join(", ", claims)}");

                var claimsIdentity = new ClaimsIdentity(claims, "jwt");
                var user = new ClaimsPrincipal(claimsIdentity);
                Console.WriteLine($"User: {user}");

                return new AuthenticationState(user);
            }
            else
            {
                Console.WriteLine("User is not authenticated");
                return new AuthenticationState(new ClaimsPrincipal());
            }
        }

        private IEnumerable<Claim> DecodeToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtToken = tokenHandler.ReadJwtToken(token);
            return jwtToken.Claims;
        }
    }
}
