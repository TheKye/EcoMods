using System.Collections.Generic;
using Eco.Gameplay.Components;
using Eco.Gameplay.Items;
using Eco.Shared.Localization;

namespace Eco.Mods.TechTree
{
    public class WashSmallWoodCartRecipe : RecipeFamily
    {
        public WashSmallWoodCartRecipe()
        {
            this.Recipes = new List<Recipe>
            {
                new Recipe(
                    "Wash Small Wood Cart Colored",
                    Localizer.DoStr("Wash Small Wood Cart Colored"),
                    new IngredientElement[]
                    {
                        new IngredientElement("ColoredSmallWoodCart", 1, true),
                        new IngredientElement(typeof(PlantFibersItem), 10, typeof(CarpentrySkill), typeof(CarpentryLavishResourcesTalent)),
                    },
                    new CraftingElement<SmallWoodCartItem>()
                )
            };
            this.ExperienceOnCraft = 0.05f;
            this.LaborInCalories = CreateLaborInCaloriesValue(50, typeof(CarpentrySkill)); 
            this.CraftMinutes = CreateCraftTimeValue(typeof(WashSmallWoodCartRecipe), 1, typeof(CarpentrySkill));    

            this.Initialize(Localizer.DoStr("Wash Small Wood Cart Colored"), typeof(WashSmallWoodCartRecipe));
            CraftingComponent.AddRecipe(typeof(PrimitivePaintingTableObject), this);
        }
    }
}
