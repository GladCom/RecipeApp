﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using RecipeApp.Interface;
using RecipeApp.Model;

namespace RecipeApp.Components;

/// <summary>
/// Компонента для отображения всех ингредиентов.
/// </summary>
public partial class IngredientView
{
  #region Поля и свойства

  /// <summary>
  /// Список ингредиентов которые нужно отобразить.
  /// </summary>
  private List<Ingredient>? ingredients;

  /// <summary>
  /// Сервис для управления ингредиентами.
  /// </summary>
  [Inject]
  private IIngredientService? IngredientService { get; set; }

  #endregion

  #region Базовый класс

  /// <inheritdoc/>
  protected override Task OnInitializedAsync()
  {
    this.ingredients = this.IngredientService.GetAllIngredient().ToList();

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