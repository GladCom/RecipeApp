using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using RecipeApp.Model;

namespace RecipeApp.Components;

public partial class RecipeForm
{
  [Parameter]
  public Recipe Recipe { get; set; } = new();

  [Parameter]
  public EventCallback OnSave { get; set; }

  [Parameter]
  public EventCallback OnCancel { get; set; }

  [Parameter]
  public bool IsEditMode { get; set; }

  private string _markdownValue = "";

  private string? _imagePreviewUrl;

  private IBrowserFile? _selectedFile;

  protected override void OnInitialized()
  {
    _markdownValue = Recipe.Content;
    _imagePreviewUrl = Recipe.ImagePath;
  }

  private void AddIngredient()
  {
    Recipe.Ingredients.Add(new Ingredient());
  }

  private void RemoveIngredient(Ingredient ing)
  {
    Recipe.Ingredients.Remove(ing);
  }

  private async Task Save()
  {
    Recipe.Content = _markdownValue;

    await OnSave.InvokeAsync();
  }

  private async Task HandleFileSelected(InputFileChangeEventArgs e)
  {
    _selectedFile = e.File;

    var fileName = $"{Guid.NewGuid()}{Path.GetExtension(_selectedFile.Name)}";
    var savePath = Path.Combine(Env.WebRootPath, "uploads", fileName);

    await using var stream = File.Create(savePath);
    await _selectedFile.OpenReadStream(maxAllowedSize: 10 * 1024 * 1024).CopyToAsync(stream);

    Recipe.ImagePath = $"uploads/{fileName}";
    _imagePreviewUrl = Recipe.ImagePath;
  }

  private static void HandleInvalidSubmit()
  {
  }
}