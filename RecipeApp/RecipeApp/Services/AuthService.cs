using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using RecipeApp.Model;

namespace RecipeApp.Services;

/// <summary>
/// Сервис для управления аутентификацией пользователей.
/// Предоставляет функциональность для входа в систему и отслеживания состояния авторизации.
/// </summary>
public class AuthService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly AuthenticationStateProvider _authStateProvider;
    
    public AuthService(
        UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager,
        AuthenticationStateProvider authStateProvider)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _authStateProvider = authStateProvider;
    }
    
    public async Task<bool> LoginAsync(string username, string password)
    {
        var result = await _signInManager.PasswordSignInAsync(username, password, false, false);
        
        if (result.Succeeded)
        {
            (_authStateProvider as CustomAuthStateProvider)?.NotifyAuthenticationStateChanged();
            return true;
        }

        return false;
    }
    public async Task<bool> RegisterAsync(string username, string password)
    {
        var user = new ApplicationUser { UserName = username };
        var result = await _userManager.CreateAsync(user, password);

        if (result.Succeeded)
        {
            await _signInManager.SignInAsync(user, isPersistent: false);
            (_authStateProvider as CustomAuthStateProvider)?.NotifyAuthenticationStateChanged();
            return true;
        }

        return false;
    }

    public async Task LogoutAsync()
    {
        await _signInManager.SignOutAsync();
        (_authStateProvider as CustomAuthStateProvider)?.NotifyAuthenticationStateChanged();
    }
}
