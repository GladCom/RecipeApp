using System.Threading.Tasks;
using RecipeApp.Model;

namespace RecipeApp.Components;

public partial class RecipeCreate
{
  private readonly Recipe _recipe = new ()
  {
    Ingredients = []
  };

  private Task Save()
  {
    this.RecipeService.AddRecipe(this._recipe);
    this.NavigationManager.NavigateTo("/");

    return Task.CompletedTask;
  }

  private void Cancel()
  {
    this.NavigationManager.NavigateTo("/");
  }
}