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

  /// <summary>
  /// Получить ингредиент.
  /// </summary>
  /// <param name="id">ИД ингредиента.</param>
  /// <returns>Найденный ингредиент. Иначе <c>null</c>.</returns>
  Ingredient? GetIngredientById(int id);

  /// <summary>
  /// Сохранить изменения.
  /// </summary>
  void SaveDbContext();
}
