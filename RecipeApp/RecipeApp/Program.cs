using Microsoft.AspNetCore.Http.Features;
using Microsoft.EntityFrameworkCore;
using RecipeApp.Components;
using RecipeApp.Services;

namespace RecipeApp;

public class Program
{
  public static void Main(string[] args)
  {
    var builder = WebApplication.CreateBuilder(args);

    builder.Services.AddRazorComponents()
      .AddInteractiveServerComponents();
    builder.Services.AddDbContext<RecipesDbContext>(options =>
      options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));
    builder.Services.Configure<FormOptions>(options => {
      options.MultipartBodyLengthLimit = 10 * 1024 * 1024;
    });

    var app = builder.Build();

    if (!app.Environment.IsDevelopment())
    {
      app.UseExceptionHandler("/Error");
      app.UseHsts();
    }

    app.UseHttpsRedirection();

    app.UseStaticFiles();
    app.UseAntiforgery();

    app.MapRazorComponents<App>()
      .AddInteractiveServerRenderMode();

    app.Run();
  }
}