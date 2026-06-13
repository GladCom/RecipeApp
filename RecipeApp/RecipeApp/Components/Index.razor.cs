using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RecipeApp.Model;

namespace RecipeApp.Components;

/// <summary>
/// Компонента отображения всех рецептов и меню.
/// </summary>
public partial class Index
{
  #region Поля и свойства глобальной конфигурации

  private const int PageSize = 10;
  private List<Recipe>? recipes;
  private List<Menu>? dishes;

  #endregion

  #region Логика рецептов (Колонка 1)

  private int currentPage = 1;
  private string searchText = string.Empty;

  private string SearchText
  {
    get => this.searchText;
    set
    {
      if (this.searchText != value)
      {
        this.searchText = value;
        this.currentPage = 1;
      }
    }
  }

  private IEnumerable<Recipe> FilteredRecipes
  {
    get
    {
      if (this.recipes == null) return Array.Empty<Recipe>();

      return string.IsNullOrWhiteSpace(this.searchText)
        ? this.recipes
        : this.recipes.Where(r => r.Title != null && r.Title.Contains(this.searchText, StringComparison.OrdinalIgnoreCase));
    }
  }

  private int TotalPages => (int)Math.Ceiling((double)this.FilteredRecipes.Count() / PageSize);

  private IEnumerable<Recipe> PagedRecipes => this.FilteredRecipes
    .Skip((this.currentPage - 1) * PageSize)
    .Take(PageSize);

  #endregion

  #region Логика меню и аккордеон-фильтров (Колонка 2)

  private int currentMenuPage = 1;
  private string searchMenuText = string.Empty;
  private int selectedGroupId { get; set; } = 0;
  private bool isMorningExpanded { get; set; } = false;
  private bool isDayExpanded { get; set; } = false;
  private bool isEveningExpanded { get; set; } = false;

  private string SearchMenuText
  {
    get => this.searchMenuText;
    set
    {
      if (this.searchMenuText != value)
      {
        this.searchMenuText = value;
        this.currentMenuPage = 1;
      }
    }
  }

  private IEnumerable<Menu> FilteredMenus
  {
    get
    {
      if (this.dishes == null) return Array.Empty<Menu>();

      var query = this.dishes.AsEnumerable();

      // 1. Фильтрация по строке поиска меню — ищем по имени меню, по содержимому (json) и по названиям блюд
      if (!string.IsNullOrWhiteSpace(this.searchMenuText))
      {
        var text = this.searchMenuText;
        query = query.Where(m =>
          (m.Name != null && m.Name.Contains(text, StringComparison.OrdinalIgnoreCase))
          || (m.Content != null && m.Content.Contains(text, StringComparison.OrdinalIgnoreCase))
          || (m.Dishes != null && m.Dishes.Any(d => d.Name != null && d.Name.Contains(text, StringComparison.OrdinalIgnoreCase)))
        );
      }

      // 2. Фильтрация по выбранной группе времени суток (1, 2, 3)
      if (this.selectedGroupId > 0)
      {
        query = query.Where(m => m.group_id == this.selectedGroupId);
      }

      return query;
    }
  }

  private int TotalMenuPages => (int)Math.Ceiling((double)this.FilteredMenus.Count() / PageSize);

  private IEnumerable<Menu> PagedMenu => this.FilteredMenus
    .Skip((this.currentMenuPage - 1) * PageSize)
    .Take(PageSize);

  #endregion

  #region Базовый класс жизненного цикла

  protected override Task OnInitializedAsync()
  {
    this.recipes = this.RecipeService.GetRecipes();
    this.dishes = this.MenuService.GetMenu();

    return Task.CompletedTask;
  }

  #endregion

  #region Методы навигации и базовой пагинации

  private void AddRecipe()
  {
    this.NavigationManager.NavigateTo("/recipes/create");
  }

  private void AddMenu()
  {
    this.NavigationManager.NavigateTo("/menu/create");
  }

  private void ViewRecipe(int id)
  {
    this.NavigationManager.NavigateTo($"/recipes/{id}");
  }

  private void MenuView(int id)
  {
    this.NavigationManager.NavigateTo($"/menu/{id}");
  }

  private void GoToPage(int page)
  {
    if (page >= 1 && page <= this.TotalPages)
      this.currentPage = page;
  }

  private void GoToMenuPage(int page)
  {
    if (page >= 1 && page <= this.TotalMenuPages)
      this.currentMenuPage = page;
  }

  #endregion

  #region Методы интерактивных сворачиваемых фильтров меню

  private void ToggleFilterSection(int group)
  {
    if (group == 1) this.isMorningExpanded = !this.isMorningExpanded;
    if (group == 2) this.isDayExpanded = !this.isDayExpanded;
    if (group == 3) this.isEveningExpanded = !this.isEveningExpanded;
  }

  private void SelectFilter(int group)
  {
    this.selectedGroupId = group;
    this.currentMenuPage = 1;
  }

  private int GetMenuCountByGroup(int group)
  {
    if (this.dishes == null) return 0;
    if (group == 0) return this.dishes.Count;

    return this.dishes.Count(m => m.group_id == group);
  }

  #endregion
}
