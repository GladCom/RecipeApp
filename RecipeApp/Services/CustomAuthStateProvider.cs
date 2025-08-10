using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using RecipeApp.Model;

namespace RecipeApp.Services;

public class CustomAuthStateProvider : AuthenticationStateProvider
{
    private readonly SignInManager<ApplicationUser> _signInManager;

    public CustomAuthStateProvider(SignInManager<ApplicationUser> signInManager)
    {
        _signInManager = signInManager;
    }

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        var user = await _signInManager.UserManager.GetUserAsync(_signInManager.Context.User);
        
        if (user == null)
        {
            return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
        }
        
        var claims = await _signInManager.UserManager.GetClaimsAsync(user);
        var identity = new ClaimsIdentity(claims, "Identity.Application");
        
        return new AuthenticationState(new ClaimsPrincipal(identity));
    }

    public void NotifyAuthenticationStateChanged()
    {
        NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
    }
}