namespace RecipeApp.Model;

/// <summary>
/// Связь между рецептом и ингредиентом с количеством.
/// </summary>
public class RecipeIngredient
{
  /// <summary>
  /// ИД рецепта.
  /// </summary>
  public int RecipeId { get; set; }

  /// <summary>
  /// Навигационное свойство рецепта.
  /// </summary>
  public Recipe Recipe { get; set; } = null!;

  /// <summary>
  /// ИД ингредиента.
  /// </summary>
  public int IngredientId { get; set; }

  /// <summary>
  /// Навигационное свойство ингредиента.
  /// </summary>
  public Ingredient Ingredient { get; set; } = null!;

  /// <summary>
  /// Количество.
  /// </summary>
  public double Amount { get; set; }

  /// <summary>
  /// Единицы измерения.
  /// </summary>
  public UnitType Unit { get; set; }
}