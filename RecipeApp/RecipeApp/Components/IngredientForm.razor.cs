using Microsoft.AspNetCore.Components;
using RecipeApp.Model;

namespace RecipeApp.Components;

/// <summary>
/// Компонента для редактирования ингредиента.
/// </summary>
public partial class IngredientForm
{
  #region Поля и свойства

  /// <summary>
  /// Ингредиент, который будет отредактирован.
  /// </summary>
  [Parameter]
  public Ingredient? Ingredient { get; set; }

  /// <summary>
  /// Обработчик события на сохранение.
  /// </summary>
  [Parameter]
  public EventCallback OnSave { get; set; }

  /// <summary>
  /// Обработчик события отмены.
  /// </summary>
  [Parameter]
  public EventCallback OnCancel { get; set; }

  /// <summary>
  /// Признак того, что рецепт открыт на редактирование.
  /// </summary>
  [Parameter]
  public bool IsEditMode { get; set; } = false;

  #endregion

  #region Методы

  /// <summary>
  /// Сохранить изменения.
  /// </summary>
  private void Save()
  {
    if (this.OnSave.HasDelegate)
      this.OnSave.InvokeAsync();
  }

  /// <summary>
  /// Отмена изменений.
  /// </summary>
  private void Cancel()
  {
    if (this.OnCancel.HasDelegate)
      this.OnCancel.InvokeAsync();
  }

  #endregion
}
