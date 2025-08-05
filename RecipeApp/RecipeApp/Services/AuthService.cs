using System;
using System.Threading.Tasks;

namespace RecipeApp.Services;

public class AuthService
{
    // Захардкоженная логика - всегда считаем пользователя неавторизованным
    public bool IsAuthenticated { get; private set; } = false;

    public event Action? AuthenticationStateChanged;

    public async Task<bool> LoginAsync(string username, string password)
    {
        // Захардкоженная логика - всегда возвращаем false
        IsAuthenticated = false;
        AuthenticationStateChanged?.Invoke();
        return false;
    }

    public void Logout()
    {
        IsAuthenticated = false;
        AuthenticationStateChanged?.Invoke();
    }
}