using System.Collections.Generic;
using System.Linq;
using RecipeApp.Interfaces;
using RecipeApp.Model;

namespace RecipeApp.Services;

/// <summary>
/// Сервис для работы с ингредиентами.
/// </summary>
/// <param name="db">Контекст БД.</param>
public class IngredientService : IIngredientService
{
  private readonly RecipesDbContext db;

  /// <summary>
  /// Initializes a new instance of the <see cref="IngredientService"/> class.
  /// Конструктор.
  /// </summary>
  /// <param name="db">Контекст БД.</param>
  public IngredientService(RecipesDbContext db)
  {
    this.db = db;
  }

  /// <summary>
  /// Получить все ингредиенты.
  /// </summary>
  /// <returns>Все ингредиенты.</returns>
  public IEnumerable<Ingredient> GetAllIngredient()
  {
    return this.db.Ingredients.AsEnumerable();
  }
}
