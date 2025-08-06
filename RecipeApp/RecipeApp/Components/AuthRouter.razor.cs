using Microsoft.AspNetCore.Components;
using System;
using RecipeApp.Services;

namespace RecipeApp.Components;

/// <summary>
/// Компонент для маршрутизации на основе состояния аутентификации пользователя.
/// Отображает либо основное содержимое приложения, либо модальное окно авторизации
/// в зависимости от того, авторизован ли пользователь.
/// </summary>
public partial class AuthRouter : ComponentBase, IDisposable
{
    #region Поля и свойства

    /// <summary>
    /// Флаг, указывающий, был ли объект уже освобожден.
    /// </summary>
    private bool _disposed;

    #endregion

    #region Методы

    /// <summary>
    /// Освобождает ресурсы, используемые компонентом.
    /// </summary>
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
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
    /// Освобождает неуправляемые ресурсы и при необходимости освобождает управляемые ресурсы.
    /// </summary>
    /// <param name="disposing">
    /// Значение <c>true</c> для освобождения управляемых и неуправляемых ресурсов;
    /// значение <c>false</c> для освобождения только неуправляемых ресурсов.
    /// </param>
    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed)
        {
            if (disposing)
            {
                // Освобождение управляемых ресурсов
                AuthService.AuthenticationStateChanged -= OnAuthStateChanged;
            }

            _disposed = true;
        }
    }

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

    #endregion
}