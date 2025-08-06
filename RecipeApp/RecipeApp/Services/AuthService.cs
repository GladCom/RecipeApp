using System;
using System.Threading.Tasks;

namespace RecipeApp.Services;

/// <summary>
/// Сервис для управления аутентификацией пользователей.
/// Предоставляет функциональность для входа в систему и отслеживания состояния авторизации.
/// </summary>
public class AuthService
{
    #region Поля и свойства

    /// <summary>
    /// Получает значение, указывающее, авторизован ли текущий пользователь.
    /// </summary>
    /// <value>
    /// <c>true</c> если пользователь авторизован; иначе <c>false</c>.
    /// </value>
    public bool IsAuthenticated { get; private set; }

    #endregion

    #region Методы

    /// <summary>
    /// Выполняет попытку входа в систему с указанными учетными данными.
    /// </summary>
    /// <param name="username">Имя пользователя для входа в систему.</param>
    /// <param name="password">Пароль пользователя.</param>
    /// <returns>
    /// Задача, представляющая асинхронную операцию. Результат задачи содержит
    /// <c>true</c> если вход выполнен успешно; иначе <c>false</c>.
    /// </returns>
    /// <remarks>
    /// Всегда возвращает <c>false</c>, так как логика захардкожена.
    /// </remarks>
    public async Task<bool> LoginAsync(string username, string password)
    {
        // Захардкоженная логика - всегда возвращаем false
        await Task.Delay(1);
        IsAuthenticated = false;
        AuthenticationStateChanged?.Invoke(this, EventArgs.Empty);
        return false;
    }

    /// <summary>
    /// Выполняет выход пользователя из системы.
    /// </summary>
    /// <remarks>
    /// Устанавливает <see cref="IsAuthenticated"/> в <c>false</c> и вызывает
    /// событие <see cref="AuthenticationStateChanged"/>.
    /// </remarks>
    public void Logout()
    {
        IsAuthenticated = false;
        AuthenticationStateChanged?.Invoke(this, EventArgs.Empty);
    }

    #endregion

    #region События

    /// <summary>
    /// Событие, которое возникает при изменении состояния аутентификации.
    /// </summary>
    public event EventHandler? AuthenticationStateChanged;

    #endregion

}