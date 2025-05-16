namespace RecipeApp.Model;

public class Recipe
{
  public int Id { get; set; }

  public string Title { get; set; } = string.Empty;

  public string Content { get; set; } = string.Empty;

  public List<Ingredient> Ingredients { get; set; } = [];
}