using System;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using RecipeApp.Model;
using RecipeApp.Services;

namespace RecipeApp.Components;

public partial class MenuView
{
  private Menu? menu;
  [Parameter]
  public int Id { get; set; }
  [Inject]
  private RecipeService RecipeService { get; set; } = default!;

  private float totalCalories = 0;
  private float totalProtein = 0;
  private float totalFat = 0;
  private float totalCarbs = 0;
  private Dictionary<int, Recipe> recipeMap = new();

  protected override Task OnInitializedAsync()
  {
    this.menu = this.MenuService.GetMenu(this.Id);

    if (this.menu?.Dishes != null)
    {
      foreach (var d in this.menu.Dishes)
      {
        if (d.recipe_id > 0)
        {
          var r = this.RecipeService.GetRecipe(d.recipe_id);
          if (r != null)
          {
            totalCalories += r.Calories;
            totalProtein += r.Protein;
            totalFat += r.Fat;
            totalCarbs += r.Carbs;
            if (!recipeMap.ContainsKey(d.recipe_id))
            {
              recipeMap[d.recipe_id] = r;
            }
          }
        }
      }
    }

    return Task.CompletedTask;
  }

  private void EditMenu()
  {
    this.NavigationManager.NavigateTo($"/menu/edit/{this.Id}");
  }

  private async Task ConfirmDelete()
  {
    var confirmed = await this.Js.InvokeAsync<bool>("confirm", "Удалить меню?");
    if (confirmed)
    {
      this.MenuService.DeleteMenuAsync(this.Id);
      this.NavigationManager.NavigateTo("/");
    }
  }

  private void GoBack()
  {
    this.NavigationManager.NavigateTo("/");
  }

  private void NavigateToRecipe(int recipeId)
  {
    if (recipeId > 0)
    {
      NavigationManager.NavigateTo($"/recipes/{recipeId}");
    }
  }
}