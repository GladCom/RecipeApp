using RecipeApp.Model;

namespace RecipeApp.Components;

public partial class Index
{
  private const int PageSize = 10;

  private List<Recipe>? _recipes;

  private string _searchText = "";

  private int _currentPage = 1;

  private string SearchText
  {
    get
    {
      return _searchText;
    }
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

  private int TotalPages =>
    (int)Math.Ceiling((double)(FilteredRecipes?.Count() ?? 0) / PageSize);

  private IEnumerable<Recipe> PagedRecipes =>
    FilteredRecipes?
      .Skip((_currentPage - 1) * PageSize)
      .Take(PageSize)
    ?? [];

  protected override Task OnInitializedAsync()
  {
    _recipes = RecipeService.GetRecipes();

    return Task.CompletedTask;
  }

  private void AddRecipe()
  {
    NavigationManager.NavigateTo("/recipes/create");
  }

  private void ViewRecipe(int id)
  {
    NavigationManager.NavigateTo($"/recipes/{id}");
  }

  private void GoToPage(int page)
  {
    if (page >= 1 && page <= TotalPages)
      _currentPage = page;
  }
}