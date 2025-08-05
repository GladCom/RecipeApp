using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using RecipeApp.Model;

namespace RecipeApp.Components;

public partial class RecipeEdit
{
  [Parameter]
  public int Id { get; set; }

  private Recipe? _recipe;

  protected override Task OnInitializedAsync()
  {
    _recipe = RecipeService.GetRecipe(Id);

    return Task.CompletedTask;
  }

  private Task Save()
  {
    RecipeService.SaveDbContext();
    NavigationManager.NavigateTo($"/recipes/{Id}");

    return Task.CompletedTask;
  }

  private void Cancel()
  {
    NavigationManager.NavigateTo($"/recipes/{Id}");
  }
}