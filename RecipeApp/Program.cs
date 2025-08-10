using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RecipeApp.Components;
using RecipeApp.Services;
using RecipeApp.Model;


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
    
    builder.Services.AddScoped<CustomAuthStateProvider>();
    builder.Services.AddScoped<AuthenticationStateProvider>(sp => sp.GetRequiredService<CustomAuthStateProvider>());
    builder.Services.AddScoped<Services.AuthService>();
    
    builder.Services.AddDbContext<RecipesDbContext>(options =>
      options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
    
    builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
      {
        options.Password.RequireDigit = true;
        options.Password.RequiredLength = 6;
        options.Password.RequireNonAlphanumeric = false;
        options.Password.RequireUppercase = false;
        options.Password.RequireLowercase = false;
        
        options.User.RequireUniqueEmail = true;
      })
      .AddEntityFrameworkStores<RecipesDbContext>()
      .AddDefaultTokenProviders();
    
    builder.Services.ConfigureApplicationCookie(options =>
    {
      options.Cookie.HttpOnly = true;
      options.ExpireTimeSpan = TimeSpan.FromMinutes(60);
      options.LoginPath = "/Identity/Account/Login";
      options.AccessDeniedPath = "/Identity/Account/AccessDenied";
      options.SlidingExpiration = true;
    });

    builder.Services.Configure<FormOptions>(options =>
    {
      options.MultipartBodyLengthLimit = 10 * 1024 * 1024;
    });
    builder.Services.AddRazorPages();
    builder.Services.AddServerSideBlazor();
    builder.Services.AddScoped<RecipeService>();

    var app = builder.Build();

    using (var scope = app.Services.CreateScope())
    {
      var db = scope.ServiceProvider.GetRequiredService<RecipesDbContext>();
      db.Database.Migrate();
    }

    if (!app.Environment.IsDevelopment())
    {
      app.UseExceptionHandler("/Error");
      app.UseHsts();
    }

    app.UseHttpsRedirection();

    app.UseStaticFiles();

    app.MapRazorComponents<App>()
      .AddInteractiveServerRenderMode();

    app.UseRouting();
    app.UseAntiforgery();

    app.MapControllers();
    app.MapBlazorHub();
    //app.MapFallbackToPage("/_Host");
    
    app.Run();
  }
}