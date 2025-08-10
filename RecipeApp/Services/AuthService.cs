﻿using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using RecipeApp.Model;

namespace RecipeApp.Services;

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
//     #region События
//
//     /// <summary>
//     /// Событие, которое возникает при изменении состояния аутентификации.
//     /// </summary>
//     public event EventHandler? AuthenticationStateChanged;
//
//     #endregion
//
//     #region Поля и свойства
//
//     /// <summary>
//     /// Получает значение, указывающее, авторизован ли текущий пользователь.
//     /// </summary>
//     /// <value>
//     /// <c>true</c> если пользователь авторизован; иначе <c>false</c>.
//     /// </value>
//     public bool IsAuthenticated { get; private set; }
//
//     #endregion
//
//     #region Методы
//
//     /// <summary>
//     /// Выполняет попытку входа в систему с указанными учетными данными.
//     /// </summary>
//     /// <param name="username">Имя пользователя для входа в систему.</param>
//     /// <param name="password">Пароль пользователя.</param>
//     /// <returns>
//     /// Задача, представляющая асинхронную операцию. Результат задачи содержит
//     /// <c>true</c> если вход выполнен успешно; иначе <c>false</c>.
//     /// </returns>
//     /// <remarks>
//     /// Всегда возвращает <c>false</c>, так как логика захардкожена.
//     /// </remarks>
//     public async Task<bool> LoginAsync(string username, string password)
//     {
//         // Захардкоженная логика - всегда возвращаем false
//         await Task.Delay(1);
//         this.IsAuthenticated = false;
//         this.AuthenticationStateChanged?.Invoke(this, EventArgs.Empty);
//         return false;
//     }
//
//     /// <summary>
//     /// Выполняет выход пользователя из системы.
//     /// </summary>
//     /// <remarks>
//     /// Устанавливает <see cref="IsAuthenticated"/> в <c>false</c> и вызывает
//     /// событие <see cref="AuthenticationStateChanged"/>.
//     /// </remarks>
//     public void Logout()
//     {
//         this.IsAuthenticated = false;
//         this.AuthenticationStateChanged?.Invoke(this, EventArgs.Empty);
//     }
//
//     #endregion
//
// }