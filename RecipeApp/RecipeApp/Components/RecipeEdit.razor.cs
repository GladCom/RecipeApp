using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using RecipeApp.Model;

namespace RecipeApp.Components;

public partial class RecipeEdit
{
  [Parameter] public int Id { get; set; }

  private Recipe? _recipe;

  protected override async Task OnInitializedAsync()
  {
    _recipe = await DbContext.Recipes
      .Include(r => r.Ingredients)
      .FirstOrDefaultAsync(r => r.Id == Id);
  }

  private async Task Save()
  {
    await DbContext.SaveChangesAsync();
    NavigationManager.NavigateTo($"/recipes/{Id}");
  }

  private void Cancel()
  {
    NavigationManager.NavigateTo($"/recipes/{Id}");
  }
}