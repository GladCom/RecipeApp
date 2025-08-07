using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using RecipeApp.Services;

namespace RecipeApp.Components;

/// <summary>
/// Модальное окно для авторизации и регистрации пользователей.
/// Предоставляет интерфейс для ввода логина и пароля с возможностью переключения между формами.
/// </summary>
public partial class LoginModal : ComponentBase
{
    #region Поля и свойства

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

    /// <summary>
    /// Получает или задает значение, указывающее, находится ли форма в режиме регистрации.
    /// </summary>
    private bool IsRegistrationMode { get; set; } = false;

    /// <summary>
    /// Получает или задает имя пользователя, введенное в поле логина.
    /// </summary>
    private string Username { get; set; } = string.Empty;

    /// <summary>
    /// Получает или задает пароль, введенный в поле пароля.
    /// </summary>
    private string Password { get; set; } = string.Empty;

    /// <summary>
    /// Получает или задает подтверждение пароля для регистрации.
    /// </summary>
    private string ConfirmPassword { get; set; } = string.Empty;

    /// <summary>
    /// Получает или задает сообщение об ошибке для отображения пользователю.
    /// </summary>
    private string ErrorMessage { get; set; } = string.Empty;

    #endregion

    #region Методы

    /// <summary>
    /// Переключает режим формы между входом и регистрацией.
    /// </summary>
    private void ToggleMode()
    {
        this.IsRegistrationMode = !this.IsRegistrationMode;
        this.ClearForm();
        this.StateHasChanged();
    }

    /// <summary>
    /// Очищает все поля формы и сообщения об ошибках.
    /// </summary>
    private void ClearForm()
    {
        this.Username = string.Empty;
        this.Password = string.Empty;
        this.ConfirmPassword = string.Empty;
        this.ErrorMessage = string.Empty;
    }

    /// <summary>
    /// Выполняет отправку формы в зависимости от текущего режима.
    /// </summary>
    /// <returns>
    /// Задача, представляющая асинхронную операцию отправки формы.
    /// </returns>
    private async Task SubmitForm()
    {
        if (this.IsRegistrationMode)
        {
            await this.Register();
        }
        else
        {
            await this.Login();
        }
    }

    /// <summary>
    /// Выполняет валидацию логина и пароля для регистрации.
    /// </summary>
    /// <returns>
    /// <c>true</c> если валидация прошла успешно; иначе <c>false</c>.
    /// </returns>
    private bool ValidateRegistrationForm()
    {
        const string ValidCharactersPattern = @"^[a-zA-Z0-9!@#$%^&*()_+\-=\[\]{};':""\\|,.<>\/?]+$";

        this.ErrorMessage = string.Empty;

        // Валидация логина
        if (string.IsNullOrWhiteSpace(this.Username))
        {
            this.ErrorMessage = "Логин не может быть пустым";
            return false;
        }

        if (this.Username.Length < 3)
        {
            this.ErrorMessage = "Логин должен содержать минимум 3 символа";
            return false;
        }

        if (this.Username.Length > 20)
        {
            this.ErrorMessage = "Логин не может быть длиннее 20 символов";
            return false;
        }

        // Проверка на использование только латиницы и цифр
        if (!System.Text.RegularExpressions.Regex.IsMatch(this.Username, @"^[a-zA-Z0-9]+$"))
        {
            this.ErrorMessage = "Логин может содержать только латинские буквы и цифры";
            return false;
        }

        // Валидация пароля
        if (string.IsNullOrWhiteSpace(this.Password))
        {
            this.ErrorMessage = "Пароль не может быть пустым";
            return false;
        }

        if (this.Password.Length < 6)
        {
            this.ErrorMessage = "Пароль должен содержать минимум 6 символов";
            return false;
        }

        if (this.Password.Length > 20)
        {
            this.ErrorMessage = "Пароль не может быть длиннее 20 символов";
            return false;
        }

        // Проверка пароля на латинские буквы, цифры и специальные символы
        if (!System.Text.RegularExpressions.Regex.IsMatch(this.Password, ValidCharactersPattern))
        {
            this.ErrorMessage = "Пароль может содержать только латинские буквы, цифры и специальные символы";
            return false;
        }

        // Валидация подтверждения пароля
        if (string.IsNullOrWhiteSpace(this.ConfirmPassword))
        {
            this.ErrorMessage = "Подтвердите пароль";
            return false;
        }

        if (this.Password != this.ConfirmPassword)
        {
            this.ErrorMessage = "Пароли не совпадают";
            this.ConfirmPassword = string.Empty;
            return false;
        }

        return true;
    }

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
        var success = await this.AuthService.LoginAsync(this.Username, this.Password);
        if (!success)
        {
            this.ErrorMessage = "Неверный логин или пароль";
            this.Username = string.Empty;
            this.Password = string.Empty;
        }
    }

    /// <summary>
    /// Выполняет попытку регистрации нового пользователя.
    /// </summary>
    /// <returns>
    /// Задача, представляющая асинхронную операцию регистрации.
    /// </returns>
    /// <remarks>
    /// Проверяет корректность введенных данных и выполняет регистрацию.
    /// </remarks>
    private async Task Register()
    {
        if (!this.ValidateRegistrationForm())
        {
            return;
        }

        // Захардкоженная логика регистрации - пока просто показываем успех
        await Task.Delay(1);
        this.IsRegistrationMode = false;
        this.ClearForm();
        this.ErrorMessage = "Регистрация успешно выполнена! Теперь вы можете войти.";
    }

    #endregion
}