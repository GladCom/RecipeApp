using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RecipeApp.Model;

public class Recipe
{
  public int Id { get; set; }

  [Required(ErrorMessage = "Название обязательно")]
  public string Title { get; set; } = string.Empty;

  public string Content { get; set; } = string.Empty;

  [NotMapped]
  public string ContentHtml => Markdig.Markdown.ToHtml(this.Content);

  public string? ImagePath { get; set; }

  public List<Ingredient> Ingredients { get; set; } = [];
}