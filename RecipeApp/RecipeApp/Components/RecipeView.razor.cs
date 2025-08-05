using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using RecipeApp.Model;

namespace RecipeApp.Components;

public partial class RecipeView
{
  [Parameter]
  public int Id { get; set; }

  private Recipe? _recipe;

  protected override Task OnInitializedAsync()
  {
    _recipe = RecipeService.GetRecipe(Id);

    return Task.CompletedTask;
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
      RecipeService.DeleteRecipe(Id);
      NavigationManager.NavigateTo("/");
    }
  }

  private void GoBack()
  {
    NavigationManager.NavigateTo("/");
  }
}