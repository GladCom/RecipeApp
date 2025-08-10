using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using RecipeApp.Model;

namespace RecipeApp.Components;

/// <summary>
/// Компонента для редактирования ингредиента.
/// </summary>
public partial class IngredientEdit
{
  #region Поля и свойства

  /// <summary>
  /// Ингредиент, который будет отредактирован.
  /// </summary>
  private Ingredient? ingredient;

  /// <summary>
  /// ИД ингредиента, который будет отредактирован.
  /// </summary>
  [Parameter]
  public int Id { get; set; }

  #endregion

  #region Базовый класс

  /// <inheritdoc/>
  protected override Task OnInitializedAsync()
  {
    this.ingredient = this.IngredientService.GetIngredientById(this.Id);
    return Task.CompletedTask;
  }

  #endregion

  #region Методы

  /// <summary>
  /// Вызов метода сохранить из IngredientForm.
  /// </summary>
  private void SaveIngredient()
  {
    this.IngredientService.SaveDbContext();
    this.NavigationManager.NavigateTo($"/ingredient/{this.Id}");
  }

  /// <summary>
  /// Вызов метода отмена из IngredientForm.
  /// </summary>
  private void CancelEdit()
  {
    this.NavigationManager.NavigateTo($"/ingredient/{this.Id}");
  }

  #endregion
}
