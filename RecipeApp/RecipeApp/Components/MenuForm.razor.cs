using Microsoft.AspNetCore.Components;
using RecipeApp.Model;
using System.Collections.Generic;
using System.Linq;

namespace RecipeApp.Components
{
    public partial class MenuForm
    {
       

        [Parameter]
        public List<Recipe> AllRecipes { get; set; } = new();

        // Чек-боксы
        private bool isBreakfast = false;
        private bool isLunch = false;
        private bool isDinner = false;

        // Храним ID рецепта (int), а не весь объект
        private List<int> breakfastItems = new() { 0 };
        private List<int> lunchItems = new() { 0 };
        private List<int> dinnerItems = new() { 0 };

        private string message = "";

        private void AddBreakfastItem() => breakfastItems.Add(0);
        private void AddLunchItem() => lunchItems.Add(0);
        private void AddDinnerItem() => dinnerItems.Add(0);

        private void Save()
        {
            // Получаем полные объекты Recipe по ID
            var selectedBreakfast = AllRecipes
                .Where(r => breakfastItems.Contains(r.Id))
                .ToList();

            var selectedLunch = AllRecipes
                .Where(r => lunchItems.Contains(r.Id))
                .ToList();

            var selectedDinner = AllRecipes
                .Where(r => dinnerItems.Contains(r.Id))
                .ToList();

            message = "✅ Сохранено!\n";

            if (isBreakfast && selectedBreakfast.Any())
            {
                message += $"Завтрак: {string.Join(", ", selectedBreakfast.Select(r => r.Title))}\n";
            }

            if (isLunch && selectedLunch.Any())
                message += $"Обед: {string.Join(", ", selectedLunch.Select(r => r.Title))}\n";

            if (isDinner && selectedDinner.Any())
                message += $"Ужин: {string.Join(", ", selectedDinner.Select(r => r.Title))}\n";
        }

        private void GoBack() => NavigationManager.NavigateTo("/");
        private void Print() => NavigationManager.NavigateTo("/print", true);
    }
}