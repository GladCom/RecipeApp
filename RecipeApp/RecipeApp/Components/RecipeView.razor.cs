using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using Microsoft.JSInterop;
using RecipeApp.Model;

namespace RecipeApp.Components;

public partial class RecipeView
{
  [Parameter]
  public int Id { get; set; }

  private Recipe? _recipe;

  protected override async Task OnInitializedAsync()
  {
    _recipe = await DbContext.Recipes
      .Include(r => r.Ingredients)
      .FirstOrDefaultAsync(r => r.Id == Id);
  }

  private void EditRecipe()
  {
    NavigationManager.NavigateTo($"/recipes/edit/{Id}");
  }

  private async Task ConfirmDelete()
  {
    var confirmed = await Js.InvokeAsync<bool>("confirm", "Удалить рецепт?");
    if (confirmed)
    {
      DbContext.Recipes.Remove(_recipe!);
      await DbContext.SaveChangesAsync();
      NavigationManager.NavigateTo("/");
    }
  }

  private void GoBack()
  {
    NavigationManager.NavigateTo("/");
  }
}