using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Hosting;
using RecipeApp.Model;
using RecipeApp.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace RecipeApp.Components;

/// <summary>
/// Компонент для отображения карточки рецепта.
/// </summary>
public partial class MenuForm
{
  #region Внедрение зависимостей

  [Inject]
  private MenuDbContext Db { get; set; } = default!;
  [Inject]
  private MenuService MenuService { get; set; } = default!;

  #endregion

  #region Поля и свойства

  private string? imagePreviewUrl;

  private IBrowserFile? selectedFile;

  [Parameter]
  public Menu Menu { get; set; } = new ();
  private Menu _menu => Menu;

  [Parameter]
  public EventCallback OnSave { get; set; }

  [Parameter]
  public EventCallback OnCancel { get; set; }

  [Parameter]
  public bool IsEditMode { get; set; }

  private int SelectedGroupId { get; set; } = 1;

  #endregion

  #region Базовый класс

  private List<Recipe> _recipes = new();
  protected override void OnInitialized()
  {
    this.imagePreviewUrl = this.Menu.ImagePath;
    this._recipes = this.RecipeService.GetRecipes().ToList();
  }

  #endregion

  #region Методы

  private static void HandleInvalidSubmit(){}

  private void AddDish()
  {
    this.NavigationManager.NavigateTo("/recipes/create");
  }

  private void RemoveDish(Dish dish)
  {
    this.Menu.Dishes.Remove(dish);
    this.Db.Dish.Remove(dish);
    this.Db.SaveChanges();
  }

  private int? SelectedRecipeId { get; set; }

  private void AddSelectedRecipe()
  {
    if (SelectedRecipeId.HasValue)
    {
      var recipe = RecipeService.GetRecipes().FirstOrDefault(r => r.Id == SelectedRecipeId.Value);
      if (recipe != null)
      {
        var dish = new Dish
        {
          Name = recipe.Title,
          recipe_id = recipe.Id,
          group_id = SelectedGroupId,
          menu_id = null
        };
        Menu.Dishes.Add(dish);
      }
    }
  }

  private async Task Save()
  {
    if (this.Menu.Dishes != null && this.Menu.Dishes.Any())
    {
      var dishNames = this.Menu.Dishes.Select(d => d.Name).ToList();
      this.Menu.Content = JsonSerializer.Serialize(dishNames);
    }
    else
    {
      this.Menu.Content = "[]";
    }

    await this.OnSave.InvokeAsync();
  }

  private async Task HandleFileSelected(InputFileChangeEventArgs e)
  {
    const string UploadsFolder = "uploads";
    this.selectedFile = e.File;

    var fileName = $"{Guid.NewGuid()}{Path.GetExtension(this.selectedFile.Name)}";
    var savePath = Path.Combine(this.Env.WebRootPath, UploadsFolder, fileName);

    await using var stream = File.Create(savePath);
    await this.selectedFile.OpenReadStream(maxAllowedSize: 10 * 1024 * 1024).CopyToAsync(stream);

    this.Menu.ImagePath = $"{UploadsFolder}/{fileName}";
    this.imagePreviewUrl = this.Menu.ImagePath;
  }

  #endregion
}