using System.Collections.Generic;
using Eco.EM.Framework.Resolvers;
using Eco.Gameplay.Components;
using Eco.Gameplay.Items;
using Eco.Shared.Localization;

namespace Eco.Mods.TechTree
{
    public class WashWoodCartRecipe : RecipeFamily, IConfigurableRecipe
    {
        static RecipeDefaultModel Defaults => new()
        {
            ModelType = typeof(WashWoodCartRecipe).Name,
            Assembly = typeof(WashWoodCartRecipe).AssemblyQualifiedName,
            HiddenName = "Clean Wood Cart",
            LocalizableName = Localizer.DoStr("Clean Wood Cart"),
            IngredientList = new()
            {
                new EMIngredient("ColoredWoodCart", true, 1, true),
                new EMIngredient("PlantFibersItem", false, 10, true),
                new EMIngredient("BucketOfWaterItem", false, 1, true),
            },
            ProductList = new()
            {
                new EMCraftable("WoodCartItem"),
                new EMCraftable("BucketItem"),
            },
            BaseExperienceOnCraft = 0.1f,
            BaseLabor = 100,
            LaborIsStatic = false,
            BaseCraftTime = 2,
            CraftTimeIsStatic = false,
            CraftingStation = "PrimitivePaintingTableItem",
            RequiredSkillType = typeof(BasicEngineeringSkill),
            RequiredSkillLevel = 0,
        };

        static WashWoodCartRecipe() { EMRecipeResolver.AddDefaults(Defaults); }

        public WashWoodCartRecipe()
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
