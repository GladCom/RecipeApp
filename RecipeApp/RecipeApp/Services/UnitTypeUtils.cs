using System;
using System.Collections.Generic;
using System.Linq;
using RecipeApp.Model;

namespace RecipeApp.Services;

public static class UnitTypeUtils
{
  public static readonly List<UnitType> UnitTypes = Enum.GetValues(typeof(UnitType)).Cast<UnitType>().ToList();

  public static string GetUnitLabel(UnitType unit) => unit switch
  {
    UnitType.Gram => "г",
    UnitType.Piece => "шт",
    UnitType.Tablespoon => "ст. ложка",
    UnitType.Milliliter => "мл",
    UnitType.Liter => "л",
    _ => unit.ToString()
  };
}