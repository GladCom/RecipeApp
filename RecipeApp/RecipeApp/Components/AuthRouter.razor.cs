using Microsoft.AspNetCore.Components;
using RecipeApp.Services;
using System;

namespace RecipeApp.Components;

/// <summary>
/// Компонент для маршрутизации на основе состояния аутентификации пользователя.
/// Отображает либо основное содержимое приложения, либо модальное окно авторизации
/// в зависимости от того, авторизован ли пользователь.
/// </summary>
public partial class AuthRouter : ComponentBase, IDisposable
{
    #region Методы

    /// <summary>
    /// Обработчик события изменения состояния аутентификации.
    /// Вызывает перерисовку компонента при изменении состояния авторизации.
    /// </summary>
    /// <param name="sender">Источник события.</param>
    /// <param name="e">Аргументы события.</param>
    private void OnAuthStateChanged(object? sender, EventArgs e)
    {
        StateHasChanged();
    }

    /// <summary>
    /// Вызывается при инициализации компонента.
    /// Подписывается на событие изменения состояния аутентификации.
    /// </summary>
    protected override void OnInitialized()
    {
        AuthService.AuthenticationStateChanged += OnAuthStateChanged;
    }

    /// <summary>
    /// Освобождает ресурсы, используемые компонентом.
    /// Отписывается от события изменения состояния аутентификации.
    /// </summary>
    public void Dispose()
    {
        AuthService.AuthenticationStateChanged -= OnAuthStateChanged;
    }

    #endregion
}