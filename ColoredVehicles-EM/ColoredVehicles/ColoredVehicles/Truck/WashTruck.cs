using System.Collections.Generic;
using Eco.Gameplay.Components;
using Eco.Gameplay.Items;
using Eco.Shared.Localization;

namespace Eco.Mods.TechTree
{
    public class WashTruckRecipe : RecipeFamily
    {
        public WashTruckRecipe()
        {
            this.Recipes = new List<Recipe>
            {
                new Recipe(
                    "Wash Truck Colored",
                    Localizer.DoStr("Wash Truck Colored"),
                    new IngredientElement[]
                    {
                        new IngredientElement("ColoredTruck", 1, true),
                        new IngredientElement(typeof(PlantFibersItem), 60, typeof(IndustrySkill), typeof(IndustryLavishResourcesTalent)),
                    },
                    new CraftingElement<TruckItem>()
                )
            };
            this.ExperienceOnCraft = 0.1f;
            this.LaborInCalories = CreateLaborInCaloriesValue(300, typeof(IndustrySkill));
            this.CraftMinutes = CreateCraftTimeValue(typeof(WashTruckRecipe), 6, typeof(IndustrySkill));

            this.Initialize(Localizer.DoStr("Wash Truck Colored"), typeof(WashTruckRecipe));
            CraftingComponent.AddRecipe(typeof(AdvancedPaintingTableObject), this);
        }
    }
}
