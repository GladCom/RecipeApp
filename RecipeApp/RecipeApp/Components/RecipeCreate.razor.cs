using System.Threading.Tasks;
using RecipeApp.Model;

namespace RecipeApp.Components;

public partial class RecipeCreate
{
  private readonly Recipe _recipe = new()
  {
    Ingredients = []
  };

  private Task Save()
  {
    RecipeService.AddRecipe(_recipe);
    NavigationManager.NavigateTo("/");

    return Task.CompletedTask;
  }

  private void Cancel()
  {
    NavigationManager.NavigateTo("/");
  }
}