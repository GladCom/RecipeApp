using RecipeApp.Model;
using RecipeApp.Services;
using System.Collections.Generic;
using System.Linq;

namespace RecipeApp.Components
{
    public partial class MenuCreate  // или любой другой компонент
    {
        private List<Recipe> allRecipes = new();

        protected override void OnInitialized()
        {
            allRecipes = RecipeService.GetRecipes();
        }
    }
}
