
using Microsoft.EntityFrameworkCore;
using RecipeApp.Model;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace RecipeApp.Services;

public class DishService(MenuDbContext db)
{
  public List<Dish> GetDishes() => (List<Dish>)db.Dish
      .Include(r => r.Name)
      .ToList();
}
