using System.Collections.Generic;
using Eco.EM.Framework.Resolvers;
using Eco.Gameplay.Components;
using Eco.Gameplay.Items;
using Eco.Shared.Localization;

namespace Eco.Mods.TechTree
{
    public class WashPoweredCartRecipe : RecipeFamily, IConfigurableRecipe
    {
        static RecipeDefaultModel Defaults => new()
        {
            ModelType = typeof(WashPoweredCartRecipe).Name,
            Assembly = typeof(WashPoweredCartRecipe).AssemblyQualifiedName,
            HiddenName = "Clean Powered Cart",
            LocalizableName = Localizer.DoStr("Clean Powered Cart"),
            IngredientList = new()
            {
                new EMIngredient("ColoredPoweredCart", true, 1, true),
                new EMIngredient("ClothItem", false, 1, true),
                new EMIngredient("BucketOfWaterItem", false, 1, true),
            },
            ProductList = new()
            {
                new EMCraftable("PoweredCartItem"),
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

        static WashPoweredCartRecipe() { EMRecipeResolver.AddDefaults(Defaults); }

        public WashPoweredCartRecipe()
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
