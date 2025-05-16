using System.ComponentModel.DataAnnotations;

namespace RecipeApp.Model;

public class Nutrition
{
  public int Id { get; set; }
    
  [Range(0, double.MaxValue)]
  public double Calories { get; set; }
    
  [Range(0, double.MaxValue)]
  public double Protein { get; set; }
    
  [Range(0, double.MaxValue)]
  public double Fat { get; set; }
    
  [Range(0, double.MaxValue)]
  public double Carbs { get; set; }

  public int IngredientId { get; set; }
  
  public Ingredient? Ingredient { get; set; }
}