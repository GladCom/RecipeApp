using Microsoft.EntityFrameworkCore;
using RecipeApp.Model;

namespace RecipeApp.Services;

public class RecipesDbContext(DbContextOptions<RecipesDbContext> options) 
  : DbContext(options)
{
  public DbSet<Recipe> Recipes => Set<Recipe>();

  public DbSet<Ingredient> Ingredients => Set<Ingredient>();
}