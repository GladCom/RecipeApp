using System.Collections.Generic;
using System.Linq;
using RecipeApp.Model;

namespace RecipeApp.Services;

/// <summary>
/// Сервис для работы с ингредиентами.
/// </summary>
/// <param name="db">Контекст БД.</param>
public class IngredientService(RecipesDbContext db)
{
  /// <summary>
  /// Получить все ингредиенты.
  /// </summary>
  /// <returns>Все ингредиенты.</returns>
  public List<Ingredient> GetAllIngredient()
  {
    return db.Ingredients.ToList();
  }
}
