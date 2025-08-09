using Microsoft.EntityFrameworkCore;
using RecipeApp.Model;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace RecipeApp.Services;

/// <summary>
/// DBContext для рецептов.
/// </summary>
/// <param name="options">Настройка контекста.</param>
public class RecipesDbContext
  : IdentityDbContext<ApplicationUser>
{
  public RecipesDbContext(DbContextOptions<RecipesDbContext> options) 
    : base(options)
  {
    
  }
  #region Поля и свойства

  /// <summary>
  /// Рецепты.
  /// </summary>
  public DbSet<Recipe> Recipes { get; set; }

  /// <summary>
  /// Ингредиенты.
  /// </summary>
  public DbSet<Ingredient> Ingredients { get; set; }

  #endregion

  #region Базовый класс

  /// <inheritdoc/>
  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    base.OnModelCreating(modelBuilder);
    modelBuilder.Entity<Recipe>()
      .HasMany(r => r.Ingredients)
      .WithOne(i => i.Recipe)
      .HasForeignKey(i => i.RecipeId)
      .OnDelete(DeleteBehavior.Cascade);
  }

  #endregion
}