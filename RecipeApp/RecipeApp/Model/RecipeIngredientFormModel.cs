namespace RecipeApp.Model;

/// <summary>
/// Модель ингредиента для формы рецепта.
/// </summary>
public class RecipeIngredientFormModel
{
  /// <summary>
  /// Название ингредиента.
  /// </summary>
  public string Name { get; set; } = string.Empty;

  /// <summary>
  /// Количество.
  /// </summary>
  public double Amount { get; set; }

  /// <summary>
  /// Единицы измерения.
  /// </summary>
  public UnitType Unit { get; set; }
}