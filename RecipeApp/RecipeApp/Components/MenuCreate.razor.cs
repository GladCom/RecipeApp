using System.Threading.Tasks;
using RecipeApp.Model;

namespace RecipeApp.Components;

public partial class MenuCreate
{
  private readonly Menu _menu = new ()
  {
    Dishes = []
  };

  private Task Save()
  {
    this.MenuService.AddMenu(this._menu);
    this.NavigationManager.NavigateTo("/");

    return Task.CompletedTask;
  }

  private void Cancel()
  {
    this.NavigationManager.NavigateTo("/", forceLoad: true);
  }
}