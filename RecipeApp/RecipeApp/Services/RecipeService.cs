using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using RecipeApp.Model;

namespace RecipeApp.Services;

/// <summary>
/// Сервис для работы с рецептами.
/// </summary>
/// <param name="db">Контекст БД.</param>
public class RecipeService(RecipesDbContext db)
{
  /// <summary>
  /// Получить все рецепты.
  /// </summary>
  /// <returns>Все рецепты.</returns>
  [SuppressMessage("Usage", "CA1002:Do not expose generic lists", Justification = "Пока оставим как есть.")]
  public List<Recipe> GetRecipes()
  {
    return db.Recipes
      .Include(r => r.RecipeIngredients)
        .ThenInclude(ri => ri.Ingredient)
      .ToList();
  }

  /// <summary>
  /// Получить рецепт.
  /// </summary>
  /// <param name="id">ИД рецепта.</param>
  /// <returns>Найденный рецепт. Иначе <c>null</c>.</returns>
  public Recipe? GetRecipe(int id)
  {
    return db.Recipes
      .Include(r => r.RecipeIngredients)
        .ThenInclude(ri => ri.Ingredient)
      .FirstOrDefault(r => r.Id == id);
  }

  /// <summary>
  /// Сохранить изменения.
  /// </summary>
  public void SaveDbContext()
  {
    db.SaveChanges();
  }

  /// <summary>
  /// Добавить рецепт.
  /// </summary>
  /// <param name="recipe">Рецепт, который нужно добавить.</param>
  /// <param name="ingredientData">Данные об ингредиентах (название, количество, единица).</param>
  [SuppressMessage("Usage", "CA1002:Do notexpose generic lists", Justification = "Используется в компоненте Blazor для binding.")]
  public void AddRecipe(Recipe recipe, List<(string name, double amount, UnitType unit)> ingredientData)
  {
    db.Recipes.Add(recipe);
    db.SaveChanges();

    var groupedIngredients = ingredientData
      .Where(item => !string.IsNullOrWhiteSpace(item.name))
      .GroupBy(item => item.name.Trim(), StringComparer.OrdinalIgnoreCase)
      .Select(group => new
      {
        Name = group.Key,
        TotalAmount = group.Sum(item => item.amount),
        Unit = group.First().unit
      })
      .ToList();

    foreach (var ingredientGroup in groupedIngredients)
    {
      var existingIngredient = db.Ingredients.FirstOrDefault(i => i.Name == ingredientGroup.Name);

      if (existingIngredient == null)
      {
        existingIngredient = new Ingredient
        {
          Name = ingredientGroup.Name,
          Calories = 0,
          Protein = 0,
          Fat = 0,
          Carbs = 0
        };
        db.Ingredients.Add(existingIngredient);
        db.SaveChanges();
      }

      var existingRecipeIngredient = db.RecipeIngredients
        .FirstOrDefault(ri => ri.RecipeId == recipe.Id && ri.IngredientId == existingIngredient.Id);

      if (existingRecipeIngredient == null)
      {
        var recipeIngredient = new RecipeIngredient
        {
          RecipeId = recipe.Id,
          IngredientId = existingIngredient.Id,
          Amount = ingredientGroup.TotalAmount,
          Unit = ingredientGroup.Unit
        };

        db.RecipeIngredients.Add(recipeIngredient);
      }
      else
      {
        existingRecipeIngredient.Amount += ingredientGroup.TotalAmount;
        db.RecipeIngredients.Update(existingRecipeIngredient);
      }
    }

    db.SaveChanges();
  }

  /// <summary>
  /// Обновить рецепт.
  /// </summary>
  /// <param name="recipe">Обновленный рецепт.</param>
  /// <param name="ingredientData">Данные об ингредиентах (название, количество, единица).</param>
  [SuppressMessage("Usage", "CA1002:Do notexpose generic lists", Justification = "Используется в компоненте Blazor для binding.")]
  public void UpdateRecipe(Recipe recipe, List<(string name, double amount, UnitType unit)> ingredientData)
  {
    db.Recipes.Update(recipe);

    var oldRecipeIngredients = db.RecipeIngredients.Where(ri => ri.RecipeId == recipe.Id);
    db.RecipeIngredients.RemoveRange(oldRecipeIngredients);

    db.SaveChanges();

    var groupedIngredients = ingredientData
      .Where(item => !string.IsNullOrWhiteSpace(item.name))
      .GroupBy(item => item.name.Trim(), StringComparer.OrdinalIgnoreCase)
      .Select(group => new
      {
        Name = group.Key,
        TotalAmount = group.Sum(item => item.amount),
        Unit = group.First().unit
      })
      .ToList();

    foreach (var ingredientGroup in groupedIngredients)
    {
      var existingIngredient = db.Ingredients.FirstOrDefault(i => i.Name == ingredientGroup.Name);

      if (existingIngredient == null)
      {
        existingIngredient = new Ingredient
        {
          Name = ingredientGroup.Name,
          Calories = 0,
          Protein = 0,
          Fat = 0,
          Carbs = 0
        };
        db.Ingredients.Add(existingIngredient);
        db.SaveChanges();
      }

      var recipeIngredient = new RecipeIngredient
      {
        RecipeId = recipe.Id,
        IngredientId = existingIngredient.Id,
        Amount = ingredientGroup.TotalAmount,
        Unit = ingredientGroup.Unit
      };

      db.RecipeIngredients.Add(recipeIngredient);
    }

    db.SaveChanges();
  }

  /// <summary>
  /// Удалить рецепт.
  /// </summary>
  /// <param name="id">ИД удаляемого рецепта.</param>
  public void DeleteRecipe(int id)
  {
    var entity = this.GetRecipe(id);
    if (entity != null)
    {
      db.Recipes.Remove(entity);
      db.SaveChanges();
    }
  }
}
