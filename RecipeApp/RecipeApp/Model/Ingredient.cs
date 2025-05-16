namespace RecipeApp.Model;

public class Ingredient
{
  public int Id { get; set; }

  public string Name { get; set; } = string.Empty;

  public double Amount { get; set; }
  
  public UnitType Unit { get; set; }
    
  public float Calories { get; set; }

  public float Protein { get; set; }

  public float Fat { get; set; }

  public float Carbs { get; set; }

  public int RecipeId { get; set; }

  public Recipe Recipe { get; set; } = null!;
}