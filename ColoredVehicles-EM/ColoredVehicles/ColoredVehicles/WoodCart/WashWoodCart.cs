using System.Collections.Generic;
using Eco.Gameplay.Components;
using Eco.Gameplay.Items;
using Eco.Shared.Localization;

namespace Eco.Mods.TechTree
{
    public class WashWoodCartRecipe : RecipeFamily
    {
        public WashWoodCartRecipe()
        {
            this.Recipes = new List<Recipe>
            {
                new Recipe(
                    "Wash Wood Cart Colored",
                    Localizer.DoStr("Wash Wood Cart Colored"),
                    new IngredientElement[]
                    {
                        new IngredientElement("ColoredWoodCart", 1, true),
                        new IngredientElement(typeof(PlantFibersItem), 20, typeof(BasicEngineeringSkill), typeof(BasicEngineeringLavishResourcesTalent)),
                    },
                    new CraftingElement<WoodCartItem>()
                )
            };
            this.ExperienceOnCraft = 0.1f;
            this.LaborInCalories = CreateLaborInCaloriesValue(100, typeof(BasicEngineeringSkill));
            this.CraftMinutes = CreateCraftTimeValue(typeof(WashWoodCartRecipe), 2, typeof(BasicEngineeringSkill));

            this.Initialize(Localizer.DoStr("Wash Wood Cart Colored"), typeof(WashWoodCartRecipe));
            CraftingComponent.AddRecipe(typeof(PrimitivePaintingTableObject), this);
        }
    }
}
