using System.Collections.Generic;
using Eco.Gameplay.Components;
using Eco.Gameplay.Items;
using Eco.Shared.Localization;

namespace Eco.Mods.TechTree
{
    public class WashSteamTruckRecipe : RecipeFamily
    {
        public WashSteamTruckRecipe()
        {
            this.Recipes = new List<Recipe>
            {
                new Recipe(
                    "Wash Steam Truck Colored",
                    Localizer.DoStr("Wash Steam Truck Colored"),
                    new IngredientElement[]
                    {
                        new IngredientElement("ColoredSteamTruck", 1, true),
                        new IngredientElement(typeof(PlantFibersItem), 40, typeof(MechanicsSkill), typeof(MechanicsLavishResourcesTalent)),
                    },
                    new CraftingElement<SteamTruckItem>()
                )
            };
            this.ExperienceOnCraft = 0.1f;  
            this.LaborInCalories = CreateLaborInCaloriesValue(200, typeof(MechanicsSkill)); 
            this.CraftMinutes = CreateCraftTimeValue(typeof(WashSteamTruckRecipe), 4, typeof(MechanicsSkill));    

            this.Initialize(Localizer.DoStr("Wash Steam Truck Colored"), typeof(WashSteamTruckRecipe));
            CraftingComponent.AddRecipe(typeof(AdvancedPaintingTableObject), this);
        }
    }
}
