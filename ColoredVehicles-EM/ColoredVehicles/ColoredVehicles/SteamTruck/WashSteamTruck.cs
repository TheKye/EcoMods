using System.Collections.Generic;
using Eco.EM.Framework.Resolvers;
using Eco.Gameplay.Components;
using Eco.Gameplay.Items;
using Eco.Shared.Localization;

namespace Eco.Mods.TechTree
{
    public class WashSteamTruckRecipe : RecipeFamily, IConfigurableRecipe
    {
        static RecipeDefaultModel Defaults => new()
        {
            ModelType = typeof(WashSteamTruckRecipe).Name,
            Assembly = typeof(WashSteamTruckRecipe).AssemblyQualifiedName,
            HiddenName = "Clean Steam Truck",
            LocalizableName = Localizer.DoStr("Clean Steam Truck"),
            IngredientList = new()
            {
                new EMIngredient("ColoredSteamTruck", true, 1, true),
                new EMIngredient("ClothItem", false, 2, true),
                new EMIngredient("BucketOfWaterItem", false, 2, true),
            },
            ProductList = new()
            {
                new EMCraftable("SteamTruckItem"),
                new EMCraftable("BucketItem", 2),
            },
            BaseExperienceOnCraft = 0.1f,
            BaseLabor = 200,
            LaborIsStatic = false,
            BaseCraftTime = 4,
            CraftTimeIsStatic = false,
            CraftingStation = "AdvancedPaintingTableItem",
            RequiredSkillType = typeof(BasicEngineeringSkill),
            RequiredSkillLevel = 0,
        };

        static WashSteamTruckRecipe() { EMRecipeResolver.AddDefaults(Defaults); }

        public WashSteamTruckRecipe()
        {
            this.Recipes = EMRecipeResolver.Obj.ResolveRecipe(this);
            this.LaborInCalories = EMRecipeResolver.Obj.ResolveLabor(this);
            this.CraftMinutes = EMRecipeResolver.Obj.ResolveCraftMinutes(this);
            this.ExperienceOnCraft = EMRecipeResolver.Obj.ResolveExperience(this);
            this.Initialize(Defaults.LocalizableName, GetType());
            CraftingComponent.AddRecipe(EMRecipeResolver.Obj.ResolveStation(this), this);
        }
    }
}
