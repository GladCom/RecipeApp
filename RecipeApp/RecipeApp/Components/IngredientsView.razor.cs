using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using RecipeApp.Model;

namespace RecipeApp.Components;

  /// <summary>
  /// Компонента для отображения всех ингредиентов.
  /// </summary>
  public partial class IngredientsView
  {
    #region Поля и свойства

    /// <summary>
    /// Список ингредиентов которые нужно отобразить.
    /// </summary>
    private List<Ingredient>? ingredients;

    #endregion

    #region Базовый класс

    /// <inheritdoc/>
    protected override Task OnInitializedAsync()
      {
        this.ingredients = this.IngredientService.GetAllIngredient();

        return Task.CompletedTask;
      }

    #endregion

    #region Методы

    /// <summary>
    /// Вернуться назад.
    /// </summary>
    private void GoBack()
      {
        this.NavigationManager.NavigateTo("/");
      }

    #endregion
  }