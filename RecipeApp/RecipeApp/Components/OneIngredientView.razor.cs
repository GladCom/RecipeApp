using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using RecipeApp.Model;

namespace RecipeApp.Components;

/// <summary>
/// Компонента для редактирования ингредиента.
/// </summary>
public partial class OneIngredientView
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
    if (this.IngredientService != null)
      this.ingredient = this.IngredientService.GetIngredientById(this.Id);
    else
      this.ingredient = null;

    return Task.CompletedTask;
  }

  #endregion

  #region Методы

  /// <summary>
  /// Переход на страницу редактирования.
  /// </summary>
  private void EditIngredient()
  {
    this.NavigationManager.NavigateTo($"/ingredient/edit/{this.Id}");
  }

  /// <summary>
  /// Вернуться назад.
  /// </summary>
  private void GoBack()
  {
    this.NavigationManager.NavigateTo("/all_ingredients");
  }

  private void SaveIngredient()
  {
    // Здесь логика сохранения изменения, вызванная из IngredientForm
    this.IngredientService.SaveDbContext();
    this.NavigationManager.NavigateTo($"/ingredient/{this.Id}");
  }

  private void CancelEdit()
  {
    this.NavigationManager.NavigateTo($"/ingredient/{this.Id}");
  }

  #endregion
}
