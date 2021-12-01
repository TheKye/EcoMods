using System.Collections.Generic;
using Eco.EM.Framework.Resolvers;
using Eco.Gameplay.Components;
using Eco.Gameplay.Items;
using Eco.Shared.Localization;

namespace Eco.Mods.TechTree
{
    public class WashSmallWoodCartRecipe : RecipeFamily, IConfigurableRecipe
    {
        static RecipeDefaultModel Defaults => new()
        {
            ModelType = typeof(WashSmallWoodCartRecipe).Name,
            Assembly = typeof(WashSmallWoodCartRecipe).AssemblyQualifiedName,
            HiddenName = "Clean Small Wood Cart",
            LocalizableName = Localizer.DoStr("Clean Small Wood Cart"),
            IngredientList = new()
            {
                new EMIngredient("ColoredSmallWoodCart", true, 1, true),
                new EMIngredient("PlantFibersItem", false, 10, true),
                new EMIngredient("BucketOfWaterItem", false, 1, true),
            },
            ProductList = new()
            {
                new EMCraftable("SmallWoodCartItem"),
                new EMCraftable("BucketItem"),
            },
            BaseExperienceOnCraft = 0.05f,
            BaseLabor = 50,
            LaborIsStatic = false,
            BaseCraftTime = 2,
            CraftTimeIsStatic = false,
            CraftingStation = "PrimitivePaintingTableItem",
            RequiredSkillType = typeof(BasicEngineeringSkill),
            RequiredSkillLevel = 0,
        };

        static WashSmallWoodCartRecipe() { EMRecipeResolver.AddDefaults(Defaults); }

        public WashSmallWoodCartRecipe()
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
