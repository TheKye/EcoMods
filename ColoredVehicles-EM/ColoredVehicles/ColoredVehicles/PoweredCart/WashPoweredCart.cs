using System.Collections.Generic;
using Eco.Gameplay.Components;
using Eco.Gameplay.Items;
using Eco.Shared.Localization;

namespace Eco.Mods.TechTree
{
    public class WashPoweredCartRecipe : RecipeFamily
    {
        public WashPoweredCartRecipe()
        {
            this.Recipes = new List<Recipe>
            {
                new Recipe(
                    "Wash Powered Cart Colored",
                    Localizer.DoStr("Wash Powered Cart Colored"),
                    new IngredientElement[]
                    {
                        new IngredientElement("ColoredPoweredCart", 1, true),
                        new IngredientElement(typeof(PlantFibersItem), 20, typeof(BasicEngineeringSkill), typeof(BasicEngineeringLavishResourcesTalent)),
                    },
                    new CraftingElement<PoweredCartItem>()
                )
            };
            this.ExperienceOnCraft = 0.1f;
            this.LaborInCalories = CreateLaborInCaloriesValue(100, typeof(BasicEngineeringSkill));
            this.CraftMinutes = CreateCraftTimeValue(typeof(WashPoweredCartRecipe), 2, typeof(BasicEngineeringSkill));

            this.Initialize(Localizer.DoStr("Wash Powered Cart Colored"), typeof(WashPoweredCartRecipe));
            CraftingComponent.AddRecipe(typeof(PrimitivePaintingTableObject), this);
        }
    }
}
