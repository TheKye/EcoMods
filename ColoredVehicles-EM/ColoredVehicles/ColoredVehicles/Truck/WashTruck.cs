using System.Collections.Generic;
using Eco.EM.Framework.Resolvers;
using Eco.Gameplay.Components;
using Eco.Gameplay.Items;
using Eco.Shared.Localization;

namespace Eco.Mods.TechTree
{
    public class WashTruckRecipe : RecipeFamily, IConfigurableRecipe
    {
        static RecipeDefaultModel Defaults => new()
        {
            ModelType = typeof(WashTruckRecipe).Name,
            Assembly = typeof(WashTruckRecipe).AssemblyQualifiedName,
            HiddenName = "Clean Truck",
            LocalizableName = Localizer.DoStr("Clean Truck"),
            IngredientList = new()
            {
                new EMIngredient("ColoredTruck", true, 1, true),
                new EMIngredient("ClothItem", false, 2, true),
                new EMIngredient("BucketOfWaterItem", false, 2, true),
            },
            ProductList = new()
            {
                new EMCraftable("TruckItem"),
                new EMCraftable("BucketItem", 2),
            },
            BaseExperienceOnCraft = 0.1f,
            BaseLabor = 300,
            LaborIsStatic = false,
            BaseCraftTime = 2,
            CraftTimeIsStatic = false,
            CraftingStation = "AdvancedPaintingTableItem",
            RequiredSkillType = typeof(BasicEngineeringSkill),
            RequiredSkillLevel = 0,
        };

        static WashTruckRecipe() { EMRecipeResolver.AddDefaults(Defaults); }

        public WashTruckRecipe()
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
