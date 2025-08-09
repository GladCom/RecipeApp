using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using RecipeApp.Model;

namespace RecipeApp.Components;

/// <summary>
/// Компонента для редактирования рецепта.
/// </summary>
public partial class RecipeEdit
{
  #region Поля и свойства

  /// <summary>
  /// Рецепт, который будет отредактирован.
  /// </summary>
  private Recipe? _recipe;

  /// <summary>
  /// ИД рецепта, который будет редактирован.
  /// </summary>
  [Parameter]
  public int Id { get; set; }

  #endregion

  #region Базовый класс

  /// <inheritdoc/>
  protected override Task OnInitializedAsync()
  {
    this._recipe = this.RecipeService.GetRecipe(this.Id);

    return Task.CompletedTask;
  }

  #endregion

  #region Методы

  /// <summary>
  /// Сохранить изменения в рецепте.
  /// </summary>
  /// <param name="ingredientData">Данные об ингредиентах.</param>
  /// <returns>Задача по сохранению рецепта.</returns>
  private Task Save(List<(string name, double amount, UnitType unit)> ingredientData)
  {
    if (this._recipe != null)
    {
      this.RecipeService.UpdateRecipe(this._recipe, ingredientData);
    }

    this.NavigationManager.NavigateTo($"/recipes/{this.Id}");

    return Task.CompletedTask;
  }

  /// <summary>
  /// Отменить сохранение рецепта.
  /// </summary>
  private void Cancel()
  {
    this.NavigationManager.NavigateTo($"/recipes/{this.Id}");
  }

  #endregion
}