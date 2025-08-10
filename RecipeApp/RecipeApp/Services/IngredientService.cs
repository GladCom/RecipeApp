using System.Collections.Generic;
using System.Linq;
using RecipeApp.Interfaces;
using RecipeApp.Model;

namespace RecipeApp.Services;

/// <summary>
/// Сервис для работы с ингредиентами.
/// </summary>
public class IngredientService : IIngredientService
{
  private readonly RecipesDbContext db;

  /// <summary>
  /// Initializes a new instance of the <see cref="IngredientService"/> class.
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
    return this.db.Ingredients.OrderBy(i => i.Name);
  }

  /// <summary>
  /// Получить ингредиент.
  /// </summary>
  /// <param name="id">ИД ингредиента.</param>
  /// <returns>Найденный ингредиент. Иначе <c>null</c>.</returns>
  public Ingredient? GetIngredientById(int id)
  {
      return this.db.Ingredients.FirstOrDefault(x => x.Id == id);
  }

  /// <summary>
  /// Сохранить изменения.
  /// </summary>
  public void SaveDbContext()
  {
    this.db.SaveChanges();
  }
}
