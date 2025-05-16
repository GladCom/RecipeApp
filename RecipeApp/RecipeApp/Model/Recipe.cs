using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Html;

namespace RecipeApp.Model;

public class Recipe
{
  public int Id { get; set; }

  public string Title { get; set; } = string.Empty;

  public string Content { get; set; } = string.Empty;

  [NotMapped]
  public string ContentHTML => new HtmlString(Content).ToString();

  public List<Ingredient> Ingredients { get; set; } = [];
}