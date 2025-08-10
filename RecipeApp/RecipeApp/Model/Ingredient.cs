using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

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
  [Required(ErrorMessage = "Название обязательно")]
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
  [SuppressMessage("Usage", "CA1002:Do notexpose generic lists", Justification = "Используется в компоненте Blazor для binding.")]
  [SuppressMessage("Usage", "CA2227:Collection properties should be read only", Justification = "Должно быть с set, чтобы можно было инициализировать из данныхрецепта.")]
  public List<RecipeIngredient> RecipeIngredients { get; set; } = [];
}
