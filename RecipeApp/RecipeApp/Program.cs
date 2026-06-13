using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RecipeApp.Components;
using RecipeApp.Services;

namespace RecipeApp;

/// <summary>
/// Программа.
/// </summary>
public static class Program
{
  /// <summary>
  /// Точка входа в программу.
  /// </summary>
  /// <param name="args">Аргументы командной строки.</param>
  public static void Main(string[] args)
  {
    var builder = WebApplication.CreateBuilder(args);

    builder.Services.AddRazorComponents()
      .AddInteractiveServerComponents();
    builder.Services.AddRazorPages();
    builder.Services.AddServerSideBlazor();

    builder.Services.AddDbContext<RecipesDbContext>(options =>
      options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
    builder.Services.AddDbContextFactory<MenuDbContext>(options =>
        options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

    builder.Services.Configure<FormOptions>(options =>
    {
      options.MultipartBodyLengthLimit = 10 * 1024 * 1024;
    });

    builder.Services.AddScoped<RecipeService>();
    builder.Services.AddScoped<MenuService>();

    var app = builder.Build();

    using (var scope = app.Services.CreateScope())
    {
      var dbRecipes = scope.ServiceProvider.GetRequiredService<RecipesDbContext>();
      dbRecipes.Database.Migrate();

      var factory = scope.ServiceProvider.GetRequiredService<IDbContextFactory<MenuDbContext>>();
      using var dbMenu = factory.CreateDbContext();
      dbMenu.Database.Migrate();
    }

    if (!app.Environment.IsDevelopment())
    {
      app.UseExceptionHandler("/Error");
      app.UseHsts();
    }

    app.UseHttpsRedirection();
    app.UseStaticFiles();
    app.UseRouting();
    app.UseAntiforgery();

    app.MapRazorComponents<App>()
      .AddInteractiveServerRenderMode();

    app.Run();
  }
}
