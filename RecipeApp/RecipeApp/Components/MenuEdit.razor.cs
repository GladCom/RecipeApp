using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using RecipeApp.Model;

namespace RecipeApp.Components;

/// <summary>
/// Компонент для редактирования меню.
/// </summary>
public partial class MenuEdit
{
  #region Поля и свойства

  private Menu? _menu;

  [Parameter]
  public int Id { get; set; }

  #endregion

  #region Базовый класс

  protected override Task OnInitializedAsync()
  {
    this._menu = this.MenuService.GetMenu(this.Id);

    return Task.CompletedTask;
  }

  #endregion

  #region Методы

  private async Task Save()
  {
    if (this._menu != null)
    {
      await this.MenuService.UpdateMenuAsync(this._menu);
    }

    this.NavigationManager.NavigateTo($"/menu/{this.Id}");
  }

  private void Cancel()
  {
    this.NavigationManager.NavigateTo($"/menu/{this.Id}");
  }

  #endregion
}
