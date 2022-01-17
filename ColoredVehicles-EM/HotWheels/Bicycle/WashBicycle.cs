namespace Eco.Mods.TechTree
{
    using System.Collections.Generic;
    using Eco.EM.Framework.Resolvers;
    using Eco.Gameplay.Components;
    using Eco.Gameplay.Items;
    using Eco.Shared.Localization;
    
    public class WashBicycleRecipe : RecipeFamily, IConfigurableRecipe
    {
        static RecipeDefaultModel Defaults => new()
        {
            ModelType = typeof(WashBicycleRecipe).Name,
            Assembly = typeof(WashBicycleRecipe).AssemblyQualifiedName,
            HiddenName = "Wash Bicycle Colored",
            LocalizableName = Localizer.DoStr("Wash Bicycle Colored"),
            IngredientList = new()
            {
                new EMIngredient("ColoredBicycle", true, 1, true),
                new EMIngredient("PlantFibersItem", false, 10, true),
                new EMIngredient("BucketOfWaterItem", false, 1, true),
            },
            ProductList = new()
            {
                new EMCraftable("BicycleItem"),
                new EMCraftable("BucketItem"),
            },
            BaseExperienceOnCraft = 0.1f,
            BaseLabor = 75,
            LaborIsStatic = false,
            BaseCraftTime = 2,
            CraftTimeIsStatic = false,
            CraftingStation = "AdvancedPaintingTableItem",
            RequiredSkillType = typeof(MechanicsSkill),
            RequiredSkillLevel = 0,
        };

        static WashBicycleRecipe() { EMRecipeResolver.AddDefaults(Defaults); }

        public WashBicycleRecipe()
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
