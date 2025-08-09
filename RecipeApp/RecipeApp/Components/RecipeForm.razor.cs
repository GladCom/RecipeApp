using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using RecipeApp.Model;

namespace RecipeApp.Components;

/// <summary>
/// Компонент для отображения карточки рецепта.
/// </summary>
public partial class RecipeForm
{
  #region Поля и свойства

  /// <summary>
  /// Значение MarkDown текста описания рецепта.
  /// </summary>
  private string markdownValue = string.Empty;

  /// <summary>
  /// URL превью изображения рецепта.
  /// </summary>
  private string? imagePreviewUrl;

  /// <summary>
  /// Выбранный для изображения файл.
  /// </summary>
  private IBrowserFile? selectedFile;

  /// <summary>
  /// Отображаемый рецепт.
  /// </summary>
  [Parameter]
  public Recipe Recipe { get; set; } = new ();

  /// <summary>
  /// Обработчик события на сохранение.
  /// </summary>
  [Parameter]
  public EventCallback<List<(string name, double amount, UnitType unit)>> OnSave { get; set; }

  /// <summary>
  /// Обработчик события отмены.
  /// </summary>
  [Parameter]
  public EventCallback OnCancel { get; set; }

  /// <summary>
  /// Признак того, что рецепт открыт на редактирование.
  /// </summary>
  [Parameter]
  public bool IsEditMode { get; set; }

  /// <summary>
  /// Список ингредиентов для формы.
  /// </summary>
  public List<RecipeIngredientFormModel> FormIngredients { get; set; } = [];

  #endregion

  #region Базовый класс

  /// <inheritdoc/>
  protected override void OnInitialized()
  {
    this.markdownValue = this.Recipe.Content;
    this.imagePreviewUrl = this.Recipe.ImagePath;

    this.FormIngredients = this.Recipe.RecipeIngredients?.Select(ri => new RecipeIngredientFormModel
    {
      Name = ri.Ingredient.Name,
      Amount = ri.Amount,
      Unit = ri.Unit
    }).ToList() ?? new List<RecipeIngredientFormModel>();
  }

  #endregion

  #region Методы

  private static void HandleInvalidSubmit()
  {
  }

  /// <summary>
  /// Добавить ингредиент.
  /// </summary>
  private void AddIngredient()
  {
    this.FormIngredients.Add(new RecipeIngredientFormModel());
  }

  /// <summary>
  /// Удалить выбранный ингредиент.
  /// </summary>
  /// <param name="ingredient">Выбранный ингредиент.</param>
  private void RemoveIngredient(RecipeIngredientFormModel ingredient)
  {
    this.FormIngredients.Remove(ingredient);
  }

  /// <summary>
  /// Сохранить рецепт.
  /// </summary>
  private async Task Save()
  {
    this.Recipe.Content = this.markdownValue;

    var ingredientData = this.FormIngredients.Select(fi => (fi.Name, fi.Amount, fi.Unit)).ToList();
    await this.OnSave.InvokeAsync(ingredientData);
  }

  /// <summary>
  /// Обработчик для выбора файла для изображения рецепта.
  /// </summary>
  /// <param name="e">Аргументы события.</param>
  private async Task HandleFileSelected(InputFileChangeEventArgs e)
  {
    const string UploadsFolder = "uploads";
    this.selectedFile = e.File;

    var fileName = $"{Guid.NewGuid()}{Path.GetExtension(this.selectedFile.Name)}";
    var savePath = Path.Combine(this.Env.WebRootPath, UploadsFolder, fileName);

    await using var stream = File.Create(savePath);
    await this.selectedFile.OpenReadStream(maxAllowedSize: 10 * 1024 * 1024).CopyToAsync(stream);

    this.Recipe.ImagePath = $"{UploadsFolder}/{fileName}";
    this.imagePreviewUrl = this.Recipe.ImagePath;
  }

  #endregion
}