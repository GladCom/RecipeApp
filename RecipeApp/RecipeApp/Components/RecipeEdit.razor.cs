using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using RecipeApp.Model;

namespace RecipeApp.Components;

public partial class RecipeEdit
{
  private Recipe? _recipe;

  [Parameter]
  public int Id { get; set; }

  protected override Task OnInitializedAsync()
  {
    this._recipe = this.RecipeService.GetRecipe(this.Id);

    return Task.CompletedTask;
  }

  private Task Save()
  {
    this.RecipeService.SaveDbContext();
    this.NavigationManager.NavigateTo($"/recipes/{this.Id}");

    return Task.CompletedTask;
  }

  private void Cancel()
  {
    this.NavigationManager.NavigateTo($"/recipes/{this.Id}");
  }
}