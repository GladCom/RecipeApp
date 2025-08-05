using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using RecipeApp.Model;

namespace RecipeApp.Services;

public class RecipeService(RecipesDbContext db)
{
  public List<Recipe> GetRecipes()
  {
    return db.Recipes
      .Include(r => r.Ingredients)
      .ToList();
  }

  public Recipe? GetRecipe(int id)
  {
    return db.Recipes
      .Include(r => r.Ingredients)
      .FirstOrDefault(r => r.Id == id);
  }

  public void SaveDbContext()
  {
    db.SaveChanges();
  }

  public void AddRecipe(Recipe recipe)
  {
    db.Recipes.Add(recipe);
    db.SaveChanges();
  }

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