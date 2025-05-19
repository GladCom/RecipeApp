using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace RecipeApp.Services;

public class RecipesDbContextFactory
  : IDesignTimeDbContextFactory<RecipesDbContext>
{
  public RecipesDbContext CreateDbContext(string[] args)
  {
    var configuration = new ConfigurationBuilder()
      .SetBasePath(Directory.GetCurrentDirectory())
      .AddJsonFile("appsettings.json")
      .Build();

    var connectionString = configuration.GetConnectionString("DefaultConnection");

    var optionsBuilder = new DbContextOptionsBuilder<RecipesDbContext>();
    optionsBuilder.UseSqlite(connectionString);

    return new RecipesDbContext(optionsBuilder.Options);
  }
}