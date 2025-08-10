using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using RecipeApp.Model;

namespace RecipeApp.Services;

public class CustomAuthStateProvider : AuthenticationStateProvider
{
    private readonly UserManager<ApplicationUser> _userManager;

    public CustomAuthStateProvider(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        var identity = new ClaimsIdentity();
        try
        {
            var user = await _userManager.GetUserAsync(ClaimsPrincipal.Current);
            if (user != null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(ClaimTypes.NameIdentifier, user.Id)
                };
                
                identity = new ClaimsIdentity(claims, "Server authentication");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to authenticate: {ex.Message}");
        }

        return new AuthenticationState(new ClaimsPrincipal(identity));
    }

    public void NotifyAuthenticationStateChanged()
    {
        NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
    }
}