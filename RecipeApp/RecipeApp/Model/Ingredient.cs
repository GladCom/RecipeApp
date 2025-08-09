using System.Collections.Generic;

namespace RecipeApp.Model;

/// <summary>
/// Ингредиент.
/// </summary>
public class Ingredient
{
  /// <summary>
  /// ИД ингредиента.
  /// </summary>
  public int Id { get; set; }

  /// <summary>
  /// Имя ингредиента.
  /// </summary>
  public string Name { get; set; } = string.Empty;

  /// <summary>
  /// Количество каллорий на 100г.
  /// </summary>
  public float Calories { get; set; }

  /// <summary>
  /// Количество протеина на 100г.
  /// </summary>
  public float Protein { get; set; }

  /// <summary>
  /// Количество жира на 100г.
  /// </summary>
  public float Fat { get; set; }

  /// <summary>
  /// Количество углеводов на 100г.
  /// </summary>
  public float Carbs { get; set; }

  /// <summary>
  /// Связи с рецептами через промежуточную таблицу.
  /// </summary>
  public List<RecipeIngredient> RecipeIngredients { get; set; } = [];
}