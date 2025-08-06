using Microsoft.AspNetCore.Components;
using RecipeApp.Services;

namespace RecipeApp.Components;

/// <summary>
/// Модальное окно для авторизации пользователей.
/// Предоставляет интерфейс для ввода логина и пароля.
/// </summary>
public partial class LoginModal : ComponentBase
{
    #region Поля и свойства

    /// <summary>
    /// Получает или задает имя пользователя, введенное в поле логина.
    /// </summary>
    private string Username { get; set; } = "";

    /// <summary>
    /// Получает или задает пароль, введенный в поле пароля.
    /// </summary>
    private string Password { get; set; } = "";

    /// <summary>
    /// Получает или задает сообщение об ошибке для отображения пользователю.
    /// </summary>
    private string ErrorMessage { get; set; } = "";

    /// <summary>
    /// Получает или задает значение, указывающее, видимо ли модальное окно.
    /// </summary>
    [Parameter]
    public bool IsVisible { get; set; }

    /// <summary>
    /// Получает или задает callback для уведомления об изменении видимости модального окна.
    /// </summary>
    [Parameter]
    public EventCallback<bool> IsVisibleChanged { get; set; }

    #endregion

    #region Методы

    /// <summary>
    /// Выполняет попытку входа в систему с введенными учетными данными.
    /// </summary>
    /// <returns>
    /// Задача, представляющая асинхронную операцию входа в систему.
    /// </returns>
    /// <remarks>
    /// При неуспешной попытке входа очищает поля ввода и отображает сообщение об ошибке.
    /// </remarks>
    private async Task Login()
    {
        // Захардкоженная логика - всегда считаем пользователя неавторизованным
        var success = await AuthService.LoginAsync(Username, Password);
        if (!success)
        {
            ErrorMessage = "Неверный логин или пароль";
            Username = "";
            Password = "";
        }
    }

    #endregion
}