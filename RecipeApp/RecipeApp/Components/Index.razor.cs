using Microsoft.EntityFrameworkCore;
using RecipeApp.Model;

namespace RecipeApp.Components;

public partial class Index
{
  private List<Recipe>? _recipes;

  protected override async Task OnInitializedAsync()
  {
    _recipes = await DbContext.Recipes
      .Include(r => r.Ingredients)
      .ToListAsync();
  }

  private void AddRecipe()
  {
    NavigationManager.NavigateTo("/recipes/create");
  }

  private void ViewRecipe(int id)
  {
    NavigationManager.NavigateTo($"/recipes/{id}");
  }

  private string _searchText = "";

  private string SearchText
  {
    get => _searchText;
    set
    {
      if (_searchText != value)
      {
        _searchText = value;
        _currentPage = 1;
      }
    }
  }

  private IEnumerable<Recipe> FilteredRecipes
  {
    get
    {
      return string.IsNullOrWhiteSpace(_searchText)
        ? _recipes!
        : _recipes!.Where(r =>
          r.Title.Contains(_searchText, StringComparison.OrdinalIgnoreCase));
    }
  }

  private int _currentPage = 1;
  private const int PageSize = 10;

  private int TotalPages => (int)Math.Ceiling((double)(FilteredRecipes?.Count() ?? 0) / PageSize);

  private IEnumerable<Recipe> PagedRecipes =>
    FilteredRecipes?
      .Skip((_currentPage - 1) * PageSize)
      .Take(PageSize)
    ?? [];

  private void GoToPage(int page)
  {
    if (page >= 1 && page <= TotalPages)
    {
      _currentPage = page;
    }
  }
}