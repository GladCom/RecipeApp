using System.Collections.Generic;
using System.Linq;
using RecipeApp.Interface;
using RecipeApp.Model;

namespace RecipeApp.Services;

/// <summary>
/// Сервис для работы с ингредиентами.
/// </summary>
/// <param name="db">Контекст БД.</param>
public class IngredientService(RecipesDbContext db) : IIngredientService
{
  /// <summary>
  /// Получить все ингредиенты.
  /// </summary>
  /// <returns>Все ингредиенты.</returns>
  public IEnumerable<Ingredient> GetAllIngredient()
  {
    return db.Ingredients.AsEnumerable();
  }
}
