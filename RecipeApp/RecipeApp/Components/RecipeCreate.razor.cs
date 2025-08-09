using System.Collections.Generic;
using System.Threading.Tasks;
using RecipeApp.Model;

namespace RecipeApp.Components;

/// <summary>
/// Компонента для создания рецепта.
/// </summary>
public partial class RecipeCreate
{
  #region Поля и свойства

  private readonly Recipe _recipe = new ();

  #endregion

  #region Методы

  /// <summary>
  /// Сохранить рецепт.
  /// </summary>
  /// <param name="ingredientData">Данные об ингредиентах.</param>
  /// <returns>Задача выполнения.</returns>
  private Task Save(List<(string name, double amount, UnitType unit)> ingredientData)
  {
    this.RecipeService.AddRecipe(this._recipe, ingredientData);
    this.NavigationManager.NavigateTo("/");

    return Task.CompletedTask;
  }

  /// <summary>
  /// Отменить создание рецепта.
  /// </summary>
  private void Cancel()
  {
    this.NavigationManager.NavigateTo("/");
  }

  #endregion
}