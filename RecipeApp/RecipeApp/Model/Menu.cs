using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace RecipeApp.Model;

/// <summary>
/// Меню.
/// </summary>
public class Menu
{
  [Key]
  [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
  public int Id { get; set; }

  public string? Name { get; set; }
  public int group_id { get; set; }
  public DateTime? StartDate { get; set; }
  [Column(TypeName = "jsonb")]
  public string? Content { get; set; }
  public string? ImagePath { get; set; }
  public List<Dish> Dishes { get; set; } = [];
}