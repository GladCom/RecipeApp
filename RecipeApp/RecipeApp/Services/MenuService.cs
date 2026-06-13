using Microsoft.EntityFrameworkCore;
using RecipeApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RecipeApp.Services;

public class MenuService(IDbContextFactory<MenuDbContext> dbFactory)
{
  public List<Menu> GetMenu()
  {
    using var db = dbFactory.CreateDbContext();

    if (!db.Menu.Any())
      return new List<Menu>();

    return db.Menu
      .Include(m => m.Dishes)
      .ToList();
  }

  public Menu? GetMenu(int id)
  {
    using var db = dbFactory.CreateDbContext();

    return db.Menu
      .Include(m => m.Dishes)
      .FirstOrDefault(d => d.Id == id);
  }

  public async Task UpdateMenuAsync(Menu menu)
  {
    using var db = dbFactory.CreateDbContext();

    var existingMenu = await db.Menu
        .Include(m => m.Dishes)
        .FirstOrDefaultAsync(m => m.Id == menu.Id);

    if (existingMenu != null)
    {
      existingMenu.Name = menu.Name;
      existingMenu.ImagePath = menu.ImagePath;
      existingMenu.Content = menu.Content;
      existingMenu.StartDate = DateTime.UtcNow;

      if (menu.Dishes != null && menu.Dishes.Any())
      {
        existingMenu.group_id = menu.Dishes.First().group_id;
      }
      else
      {
        existingMenu.group_id = 1;
      }

      if (existingMenu.Dishes != null)
      {
        db.Dish.RemoveRange(existingMenu.Dishes);
      }

      if (menu.Dishes != null)
      {
        foreach (var dish in menu.Dishes)
        {
          dish.Id = 0;
          dish.menu_id = existingMenu.Id;
          dish.group_id = existingMenu.group_id;
          existingMenu.Dishes.Add(dish);
        }
      }

      await db.SaveChangesAsync();
    }
  }

  public async Task AddMenu(Menu menu)
  {
    using var db = dbFactory.CreateDbContext();

    menu.StartDate = DateTime.UtcNow;

    if (menu.Dishes != null && menu.Dishes.Any())
    {
      menu.group_id = menu.Dishes.First().group_id;
    }
    else
    {
      menu.group_id = 1;
    }

    if (menu.Dishes != null)
    {
      foreach (var dish in menu.Dishes)
      {
        dish.group_id = menu.group_id;

        if (dish.Id != 0)
        {
          db.Entry(dish).State = EntityState.Unchanged;
        }
      }
    }

    await db.Menu.AddAsync(menu);
    await db.SaveChangesAsync();
  }

  public async Task DeleteMenuAsync(int id)
  {
    using var db = dbFactory.CreateDbContext();

    var entity = await db.Menu
        .Include(m => m.Dishes)
        .FirstOrDefaultAsync(d => d.Id == id);

    if (entity != null)
    {
      if (entity.Dishes != null && entity.Dishes.Any())
      {
        db.Dish.RemoveRange(entity.Dishes);
      }
      db.Menu.Remove(entity);
      await db.SaveChangesAsync();
    }
  }
}
