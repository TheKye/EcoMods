namespace Eco.Mods.TechTree
{
    using System.Collections.Generic;
    using Eco.EM.Framework.Resolvers;
    using Eco.Gameplay.Components;
    using Eco.Gameplay.Items;
    using Eco.Shared.Localization;
    
    public class WashScooterRecipe : RecipeFamily, IConfigurableRecipe
    {
        static RecipeDefaultModel Defaults => new()
        {
            ModelType = typeof(WashScooterRecipe).Name,
            Assembly = typeof(WashScooterRecipe).AssemblyQualifiedName,
            HiddenName = "Wash Scooter Colored",
            LocalizableName = Localizer.DoStr("Wash Scooter Colored"),
            IngredientList = new()
            {
                new EMIngredient("ColoredScooter", true, 1, true),
                new EMIngredient("PlantFibersItem", false, 10, true),
                new EMIngredient("BucketOfWaterItem", false, 1, true),
            },
            ProductList = new()
            {
                new EMCraftable("ScooterItem"),
                new EMCraftable("BucketItem"),
            },
            BaseExperienceOnCraft = 0.05f,
            BaseLabor = 50,
            LaborIsStatic = false,
            BaseCraftTime = 1,
            CraftTimeIsStatic = false,
            CraftingStation = "PrimitivePaintingTableItem",
            RequiredSkillType = typeof(BasicEngineeringSkill),
            RequiredSkillLevel = 0,
        };

        static WashScooterRecipe() { EMRecipeResolver.AddDefaults(Defaults); }

        public WashScooterRecipe()
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
