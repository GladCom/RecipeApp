using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using RecipeApp.Model;

namespace RecipeApp.Components;

public partial class RecipeView
{
  private Recipe? _recipe;

  [Parameter]
  public int Id { get; set; }

  protected override Task OnInitializedAsync()
  {
    this._recipe = this.RecipeService.GetRecipe(this.Id);

    return Task.CompletedTask;
  }

  private void EditRecipe()
  {
    this.NavigationManager.NavigateTo($"/recipes/edit/{this.Id}");
  }

  private async Task ConfirmDelete()
  {
    var confirmed = await this.Js.InvokeAsync<bool>("confirm", "Удалить рецепт?");
    if (confirmed)
    {
      this.RecipeService.DeleteRecipe(this.Id);
      this.NavigationManager.NavigateTo("/");
    }
  }

  private void GoBack()
  {
    this.NavigationManager.NavigateTo("/");
  }
}