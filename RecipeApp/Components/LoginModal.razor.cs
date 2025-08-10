using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using RecipeApp.Services;

namespace RecipeApp.Components;

public partial class LoginModal : ComponentBase
{
    [Parameter]
    public bool IsVisible { get; set; }

    [Parameter]
    public EventCallback<bool> IsVisibleChanged { get; set; }

    [Inject]
    private Services.AuthService AuthService { get; set; } = null!;

    [Inject]
    private NavigationManager NavigationManager { get; set; } = null!;

    private bool IsRegistrationMode { get; set; }
    private string Username { get; set; } = string.Empty;
    private string Password { get; set; } = string.Empty;
    private string ConfirmPassword { get; set; } = string.Empty;
    private string ErrorMessage { get; set; } = string.Empty;
    private bool IsProcessing { get; set; }

    private async Task Close()
    {
        if (IsProcessing) return;
        
        IsVisible = false;
        await IsVisibleChanged.InvokeAsync(false);
        ResetForm();
    }

    private void ToggleMode()
    {
        if (IsProcessing) return;
        
        IsRegistrationMode = !IsRegistrationMode;
        ErrorMessage = string.Empty;
    }

    private void ResetForm()
    {
        Username = string.Empty;
        Password = string.Empty;
        ConfirmPassword = string.Empty;
        ErrorMessage = string.Empty;
        IsProcessing = false;
    }

    private async Task SubmitForm()
    {
        if (IsProcessing) return;
        
        IsProcessing = true;
        ErrorMessage = string.Empty;

        try
        {
            if (IsRegistrationMode)
            {
                await Register();
            }
            else
            {
                await Login();
            }
        }
        finally
        {
            IsProcessing = false;
            StateHasChanged();
        }
    }

    private bool ValidateRegistrationForm()
    {
        if (string.IsNullOrWhiteSpace(Username))
        {
            ErrorMessage = "Логин не может быть пустым";
            return false;
        }

        if (Username.Length < 3)
        {
            ErrorMessage = "Логин должен содержать минимум 3 символа";
            return false;
        }

        if (Username.Length > 20)
        {
            ErrorMessage = "Логин не может быть длиннее 20 символов";
            return false;
        }

        if (!System.Text.RegularExpressions.Regex.IsMatch(Username, @"^[a-zA-Z0-9]+$"))
        {
            ErrorMessage = "Логин может содержать только латинские буквы и цифры";
            return false;
        }

        if (string.IsNullOrWhiteSpace(Password))
        {
            ErrorMessage = "Пароль не может быть пустым";
            return false;
        }

        if (Password.Length < 6)
        {
            ErrorMessage = "Пароль должен содержать минимум 6 символов";
            return false;
        }

        if (Password != ConfirmPassword)
        {
            ErrorMessage = "Пароли не совпадают";
            ConfirmPassword = string.Empty;
            return false;
        }

        return true;
    }

    private async Task Login()
    {
        if (string.IsNullOrWhiteSpace(Username) || string.IsNullOrWhiteSpace(Password))
        {
            ErrorMessage = "Заполните все поля";
            return;
        }

        var success = await AuthService.LoginAsync(Username, Password);
        
        if (success)
        {
            await Close();
            NavigationManager.NavigateTo("/", forceLoad: true);
        }
        else
        {
            ErrorMessage = "Неверный логин или пароль";
            Password = string.Empty;
        }
    }

    private async Task Register()
    {
        if (!ValidateRegistrationForm())
        {
            return;
        }

        var result = await AuthService.RegisterAsync(Username, Password);
        
        if (result)
        {
            IsRegistrationMode = false;
            ErrorMessage = "Регистрация успешно выполнена! Теперь вы можете войти.";
            Password = string.Empty;
            ConfirmPassword = string.Empty;
        }
        else
        {
            ErrorMessage = "Ошибка регистрации. Возможно, такой пользователь уже существует.";
        }
    }
}