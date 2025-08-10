using System.Collections.Generic;
using RecipeApp.Model;

namespace RecipeApp.Interfaces;

/// <summary>
/// Интерфейс для сервиса, предоставляющего доступ к списку ингредиентов.
/// </summary>
public interface IIngredientService
{
  /// <summary>
  /// Возвращает список всех ингредиентов.
  /// </summary>
  /// <returns>Коллекция ингредиентов.</returns>
  IEnumerable<Ingredient> GetAllIngredient();
}
