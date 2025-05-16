using RecipeApp.Model;

namespace RecipeApp.Services;

using Microsoft.EntityFrameworkCore;

public class RecipesDbContext(DbContextOptions<RecipesDbContext> options)
  : DbContext(options)
{
  public DbSet<Recipe> Recipes { get; set; }
  public DbSet<Ingredient> Ingredients { get; set; }

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    modelBuilder.Entity<Recipe>()
      .HasMany(r => r.Ingredients)
      .WithOne()
      .HasForeignKey(i => i.RecipeId)
      .OnDelete(DeleteBehavior.Cascade);
  }
}