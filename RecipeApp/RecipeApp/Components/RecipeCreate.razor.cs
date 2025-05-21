using RecipeApp.Model;

namespace RecipeApp.Components;

public partial class RecipeCreate
{
  private readonly Recipe _recipe = new()
  {
    Ingredients = []
  };

  private async Task Save()
  {
    DbContext.Recipes.Add(_recipe);
    await DbContext.SaveChangesAsync();
    NavigationManager.NavigateTo("/");
  }

  private void Cancel() => NavigationManager.NavigateTo("/");
}