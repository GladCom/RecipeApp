using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using RecipeApp.Model;

namespace RecipeApp.Components;

public partial class RecipeForm
{
  [Parameter]
  public Recipe Recipe { get; set; } = new ();

  [Parameter]
  public EventCallback OnSave { get; set; }

  [Parameter]
  public EventCallback OnCancel { get; set; }

  [Parameter]
  public bool IsEditMode { get; set; }

  private string _markdownValue = string.Empty;

  private string? _imagePreviewUrl;

  private IBrowserFile? _selectedFile;

  protected override void OnInitialized()
  {
    this._markdownValue = this.Recipe.Content;
    this._imagePreviewUrl = this.Recipe.ImagePath;
  }

  private void AddIngredient()
  {
    this.Recipe.Ingredients.Add(new Ingredient());
  }

  private void RemoveIngredient(Ingredient ing)
  {
    this.Recipe.Ingredients.Remove(ing);
  }

  private async Task Save()
  {
    this.Recipe.Content = this._markdownValue;

    await this.OnSave.InvokeAsync();
  }

  private async Task HandleFileSelected(InputFileChangeEventArgs e)
  {
    this._selectedFile = e.File;

    var fileName = $"{Guid.NewGuid()}{Path.GetExtension(this._selectedFile.Name)}";
    var savePath = Path.Combine(this.Env.WebRootPath, "uploads", fileName);

    await using var stream = File.Create(savePath);
    await this._selectedFile.OpenReadStream(maxAllowedSize: 10 * 1024 * 1024).CopyToAsync(stream);

    this.Recipe.ImagePath = $"uploads/{fileName}";
    this._imagePreviewUrl = this.Recipe.ImagePath;
  }

  private static void HandleInvalidSubmit()
  {
  }
}